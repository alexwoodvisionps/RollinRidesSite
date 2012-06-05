using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.IO;
using System.Text;
using RollingRides.WebApp.Components.Datalayer.Common;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;
using System.Security.Cryptography;
namespace RollingRides.WebApp.Components.Datalayer.Repositories
{
	public class UserRepository : GenericRepository<RollingRides.WebApp.Components.Datalayer.Models.User> ,IUserRepository
	{
		public UserRepository ()
		{
		}

        public RollingRides.WebApp.Components.Datalayer.Models.User GetById(int Id)
		{
			//var dt = ExecuteQuery ("SELECT * FROM [User] WHERE Id = " + Id);
			//if (dt.Rows.Count == 0)
			//	return null;
			//return ((DataRow)dt.Rows [0]).DataRowToModel (new User ());
            return GetById(Id, "User", new RollingRides.WebApp.Components.Datalayer.Models.User());
		}

	    public void ChangePassword(int userId, string newPassword)
	    {
	        var newPassHash = Convert.ToBase64String(new SHA256Cng().ComputeHash(Encoding.ASCII.GetBytes(newPassword)));
	        ExecuteNonQuery("Update [User] Set [Password] = @pass WHERE Id = " + userId, new SqlParameter[]{new SqlParameter("@pass", newPassHash)});
	    }

	    public List<RollingRides.WebApp.Components.Datalayer.Models.User> GetAllUsers()
		{
            return ExecuteQuery("SELECT * FROM [User]", null, CommandType.Text).DataTableToList<RollingRides.WebApp.Components.Datalayer.Models.User>(new RollingRides.WebApp.Components.Datalayer.Models.User());
			
		}
        public RollingRides.WebApp.Components.Datalayer.Models.User ValidateLogin(string username, string password)
        {
            var passwordHash = new SHA256Cng().ComputeHash(Encoding.ASCII.GetBytes(password));
            var paramList = new List<SqlParameter>
                                {
                                    new SqlParameter("@username", username.Trim()),
                                    new SqlParameter("@pass", Convert.ToBase64String(passwordHash))
			                    };
            var dt = ExecuteQuery (
				"SELECT * FROM [User] WHERE Upper(Username) = Upper(@username) AND [Password] = @pass",
				paramList
			);
			return dt.Rows.Count == 1 ? ((DataRow)dt.Rows[0]).DataRowToModel<RollingRides.WebApp.Components.Datalayer.Models.User>(new RollingRides.WebApp.Components.Datalayer.Models.User()) : null;
		}
		public bool ChangePassword (int userId, string oldPassword, string newPassword)
		{
		    var sha2 = new SHA256Cng();
		    var passOld = Convert.ToBase64String(sha2.ComputeHash(Encoding.ASCII.GetBytes(oldPassword)));
		    var passNew = Convert.ToBase64String(sha2.ComputeHash(Encoding.ASCII.GetBytes(newPassword)));
			var paramList = new List<SqlParameter>
			                    {
			                        new SqlParameter("@pass", passNew),
			                        new SqlParameter("@userId", userId),
			                        new SqlParameter("@oldPass", passOld)
			                    };
		    var rowsAffected = ExecuteNonQuery ("Update [User] Set [Password] = @pass " +
				"WHERE Id = @userId AND [Password] = @oldPass ", paramList);
			return rowsAffected == 1;
		}
		public string ResetPassword (string email, string newPassword)
		{
		    var sha2 = new SHA256Cng();
			var paramList = new List<SqlParameter>
			                    {
			                        new SqlParameter("@pass", Convert.ToBase64String(sha2.ComputeHash(Encoding.ASCII.GetBytes(newPassword)))),
			                        new SqlParameter("@email", email)
			                    };
		    var rowsAffected = ExecuteNonQuery ("Update [User] Set [Password] = @pass WHERE Email = @email", paramList);
			if (rowsAffected == 1)
				return newPassword;
			return null;
		}
        public RollingRides.WebApp.Components.Datalayer.Models.User AddUpdate(RollingRides.WebApp.Components.Datalayer.Models.User user, UserType type = UserType.User)
		{
			var dt = ExecuteQuery ("SELECT * FROM [User] WHERE Id = " + user.Id,null);
			var sql = "";
            var emailQueryParams = new SqlParameter[] {new SqlParameter("@email", user.Email)};
            var dt1 = ExecuteQuery("SELECT * FROM [User] WHERE Email = @email", emailQueryParams);
            if(dt1.Rows.Count > 0 && user.Id < 1)
                throw  new Exception("Email Address Already Used!");
			if (dt.Rows.Count == 1) {
				sql = "UPDATE [User] Set Street1 = @s1, Street2 = @s2, City = @city, Expires = @expires, " +
					"[state] =  @state, ZipCode = @zip, PhoneNumber = @phone, firstName = @first, lastName = @last, CompanyName = @Company" +
					" WHERE Id = " + user.Id;
				var paramList = new SqlParameter[]{
					new SqlParameter ("@s1", user.Street1),
					new SqlParameter ("@s2", user.Street2),
					new SqlParameter ("@city", user.City),
					new SqlParameter ("@state", user.State),
					new SqlParameter ("@zip", user.ZipCode),
					new SqlParameter ("@phone", user.PhoneNumber),
                    new SqlParameter("@expires",(type == UserType.Admin ?  (user.Expires.HasValue ? (object) user.Expires.Value.ToString("MM/dd/yyyy") : DBNull.Value) : DBNull.Value) ), 
					//new SqlParameter ("@email", user.Email),
					new SqlParameter ("@first", user.FirstName),
                    new SqlParameter("@Company", user.CompanyName), 
					new SqlParameter ("@last", user.LastName)
				};
				var rowsAffected = ExecuteNonQuery (sql, paramList);
				return rowsAffected == 0 ? null : user;
			}
			var paramList3 = new SqlParameter[]{
				new SqlParameter ("@username", user.Username)
			};
			sql = "SELECT Id FROM [User] WHERE Username = @username";
			dt = ExecuteQuery (sql, paramList3);
			if (dt.Rows.Count > 0)
				throw new Exception ("Username already exists!");
			sql = "INSERT INTO [User] (FirstName, LastName, Username, [Password], Street1, Street2, City, [State], " +
				"ZipCode, Email, PhoneNumber, AccountType, DateJoined, Expires) Values (@first, @last, @username, @pass, @s1, @s2," +
				"@city, @state, @zip, @email, @phone, @type, @dateJoined, " + (type == UserType.Admin ?  (user.Expires.HasValue ? "'"+user.Expires.Value.ToString("MM/dd/yyyy") + "' " : "NULL ") : "NULL ")  + ")";
            var passwordHash =
                Convert.ToBase64String(new SHA256Cng().ComputeHash(Encoding.ASCII.GetBytes(user.Password)));
            
            var paramList2 = new SqlParameter[]{
                    new SqlParameter("@username", user.Username),
                    new SqlParameter("@pass", passwordHash), 
					new SqlParameter ("@s1", user.Street1),
					new SqlParameter ("@s2", user.Street2),
					new SqlParameter ("@city", user.City),
					new SqlParameter ("@state", user.State),
					new SqlParameter ("@zip", user.ZipCode),
					new SqlParameter ("@phone", user.PhoneNumber),
					new SqlParameter ("@email", user.Email),
					new SqlParameter ("@first", user.FirstName),
					new SqlParameter ("@last", user.LastName),
				new SqlParameter ("@type", (int)user.UserType),
				new SqlParameter("@dateJoined", user.DateJoined)
				};
			var rowsAffected2 = ExecuteNonQuery (sql, paramList2);
			return rowsAffected2 == 0 ? null : ValidateLogin (user.Username, user.Password);
		}
		public bool Delete (int id)
		{
		    List<Image> imgsdeleted;
			var autoRepo = new AutomobileRepository ();
			var rows = autoRepo.DeleteByUserId (id, out imgsdeleted);
			var rowsAffected = ExecuteNonQuery ("Delete From [User] WHERE Id = " + id);
		    foreach (var image in imgsdeleted)
		    {
		        if(image.MediaType != MediaType.Youtube)
		        {
                    File.Delete(ConfigurationManager.AppSettings["MediaRootDir"] + image.Url);
		        }
		    }
			return rowsAffected == 1;
		}
	}
}

