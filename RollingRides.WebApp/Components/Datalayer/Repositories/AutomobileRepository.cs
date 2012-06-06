using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using RollingRides.WebApp.Components.Datalayer.Common;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;
using RollingRides.WebApp.Components.BusinessLogic.Common;
namespace RollingRides.WebApp.Components.Datalayer.Repositories
{
	public class AutomobileRepository : GenericRepository<Automobile>, IAutomobileRepository
	{
		public AutomobileRepository ()
		{
		}
		public int DeleteByUserId (int userId, out List<Image> imgsDeleted)
		{
			var dt = ExecuteQuery ("SELECT Id FROM Automobile WHERE UserId = " + userId, null);
		    imgsDeleted = new List<Image> ();
			foreach (DataRow dr in dt.Rows) {
			    List<Image> imgs;
			    DeleteImages (int.Parse (dr ["Id"].ToString ()), out imgs);
				imgsDeleted.AddRange (imgs);
			}
			return ExecuteNonQuery ("DELETE FROM Automobile WHERE UserId = " + userId, null);
		}

	    public void DeleteImage(int id)
	    {
	        var completePath = ConfigurationManager.AppSettings["MediaPath"];
	        var dt = ExecuteQuery("SELECT * FROM Image WHERE Id = " + id).DataTableToList<Image>(new RollingRides.WebApp.Components.Datalayer.Models.Image());
            if(dt.Count > 0)
            {
                if(dt.Single().Type == (int) MediaType.Image || dt.Single().Type == (int) MediaType.Server)
                {
                    completePath += dt.Single().Url;
                    if(File.Exists(completePath))
                    {
                        File.Delete(completePath);
                    }
                }
            }
	        ExecuteNonQuery("DELETE FROM Images WHERE Id = " + id);
	    }

	    public Automobile AddUpdate (Automobile mobile, UserType type)
		{
			var dt = ExecuteQuery ("SELECT * FROM Automobile WHERE Id = " + mobile.Id, null);
			var sql = "";
			
			if (dt.Rows.Count == 1) {
				sql = "UPDATE Automobile Set ContactName = @cname, Make = @make, Model = @model," +
					"Street1 = @s1, Street2 = @s2, City = @city, [State] = @state, " +
					"ZipCode = @zip, year = @year, description = @des, Title = @title," +
                    "PhoneNumber = @phone, Pricing = @price, MinimumDownPayment = @minpayment, HasFinancing = @financing,CarfaxReportPath = @report, IsUsed = @used, HasFinancing =  " + mobile.HasFinancing + ", MinimumDownPayment = " + (mobile.MinimumDownPayment.HasValue ? mobile.MinimumDownPayment.Value.ToString() : "NULL") + (type == UserType.Admin ? "IsHighlight = @h1 " : "") + (type == UserType.Admin ? ", IsApproved = @approved" : "") + "WHERE Id = " + mobile.Id;
				var paramList = type == UserType.Admin ? new SqlParameter[]{
					new SqlParameter("@cname", mobile.ContactName), 
                    new SqlParameter ("@make", mobile.Make),
					new SqlParameter ("@model", mobile.Model),
					new SqlParameter ("@s1", mobile.Street1),
					new SqlParameter ("@s2", mobile.Street2),
					new SqlParameter ("@city", mobile.City),
					new SqlParameter ("@state", mobile.State),
					new SqlParameter ("@zip", mobile.ZipCode),
					new SqlParameter ("@year", mobile.Year),
					new SqlParameter ("@des", mobile.Description),
					new SqlParameter ("@title", mobile.Title),
					new SqlParameter ("@phone", mobile.PhoneNumber),
                    new SqlParameter ("@minpayment", mobile.MinimumDownPayment),
                    new SqlParameter ("@financing", mobile.HasFinancing),
                    new SqlParameter ("@price", mobile.Price), 
					new SqlParameter ("@report", mobile.CarfaxReportPath),
					new SqlParameter ("@used", mobile.IsUsed),
					new SqlParameter ("@h1", mobile.IsHighlight),
                    new SqlParameter("@approved", mobile.IsApproved) 
				} : new[]{
					new SqlParameter("@cname", mobile.ContactName), 
                    new SqlParameter ("@make", mobile.Make),
					new SqlParameter ("@model", mobile.Model),
					new SqlParameter ("@s1", mobile.Street1),
					new SqlParameter ("@s2", mobile.Street2),
					new SqlParameter ("@city", mobile.City),
					new SqlParameter ("@state", mobile.State),
					new SqlParameter ("@zip", mobile.ZipCode),
					new SqlParameter ("@year", mobile.Year),
					new SqlParameter ("@des", mobile.Description),
					new SqlParameter ("@title", mobile.Title),
					new SqlParameter ("@phone", mobile.PhoneNumber),
                    new SqlParameter ("@minpayment", mobile.MinimumDownPayment),
                    new SqlParameter ("@financing", mobile.HasFinancing),
                    new SqlParameter ("@price", mobile.Price),
                    new SqlParameter ("@report", mobile.CarfaxReportPath),
					new SqlParameter ("@used", mobile.IsUsed)
				};
				var rowsAffected = ExecuteNonQuery (sql, paramList);
				return rowsAffected == 1 ? mobile : null;
			}
			sql = "INSERT INTO Automobile(Make, Model, Street1, Street2," +
				"City, [State], ZipCode, Year, Description, Title, " +
				"PhoneNumber, CarfaxReportPath, UserId, ContactName, IsUsed, IsApproved, Price, MinimumDownPayment, HasFianancing) Values(" +
				"@make, @model, @s1, @s2, @city, @state, @zip, @year, @des," +
				"@title, @phone, @report, @userId, @cname, @used, @approved, @price, @minpayment, @financing)";
			var paramList2 = new SqlParameter[]{
					new SqlParameter ("@make", mobile.Make),
					new SqlParameter ("@model", mobile.Model),
					new SqlParameter ("@s1", mobile.Street1),
					new SqlParameter ("@s2", mobile.Street2),
					new SqlParameter ("@city", mobile.City),
					new SqlParameter ("@state", mobile.State),
					new SqlParameter ("@zip", mobile.ZipCode),
					new SqlParameter ("@year", mobile.Year),
					new SqlParameter ("@des", mobile.Description),
					new SqlParameter ("@title", mobile.Title),
					new SqlParameter ("@phone", mobile.PhoneNumber),
					new SqlParameter ("@report", mobile.CarfaxReportPath),
					new SqlParameter ("@userId", mobile.UserId),
                    new SqlParameter("@cname", mobile.ContactName),
                    new SqlParameter ("@used", mobile.IsUsed),
                    new SqlParameter("@approved", mobile.IsApproved),
                     new SqlParameter ("@minpayment", mobile.MinimumDownPayment),
                    new SqlParameter ("@financing", mobile.HasFinancing),
                    new SqlParameter ("@price", mobile.Price)
				};
			var rowsAffected2 = ExecuteNonQuery (sql, paramList2);
			
			return rowsAffected2 == 1 ? mobile : null;
		}
		public bool Delete (int id, out List<Image> imgsDeleted)
		{
			DeleteImages (id, out imgsDeleted);
			var rowsAffected = ExecuteNonQuery ("DELETE FROM Automobile WHERE Id = " + id);
			return rowsAffected == 1;
		}
		public List<Automobile> GetAllVehicles (bool approvedOnly)
		{
			var sql = "SELECT * FROM Automobile";
			if (approvedOnly)
				sql += " WHERE IsApproved = 1";
			var autos = ExecuteQuery (sql).DataTableToList<Automobile> (new Automobile ());
			foreach (var auto in autos) {
				auto.Images = GetImagesByAutoId (auto.Id, true);
			}
			return autos;
		}
		public Automobile GetById (int Id)
		{
			var auto = GetById (Id, "Automobile", new Automobile ());
			auto.Images = GetImagesByAutoId (auto.Id, false);
		    auto.Seller = new UserRepository().GetById(auto.UserId);
            return auto;
		}
      

	    public List<Automobile> Search (string make, string model, decimal? minPrice, decimal? maxPrice)
		{
			var sql = "SELECT * FROM Automobile ";
			var sqlRest = "";
			var paramList = new List<SqlParameter> ();
			if (make != null) {
				paramList.Add (new SqlParameter ("@make", make));
				sqlRest += " and ToUpper(Make) = ToUpper(@make) ";
			}
			if (model != null) {
				paramList.Add (new SqlParameter ("@model", model));
				sqlRest += " and ToUpper(Model) = ToUpper(@model)";
			}
			if (minPrice.HasValue) {
				paramList.Add (new SqlParameter ("@minPrice", minPrice.Value));
				sqlRest += " and price > @minPrice";
			}
			if (maxPrice.HasValue) {
                paramList.Add(new SqlParameter("@maxPrice", maxPrice.Value));
				sqlRest += " and price < @maxPrice ";
			}
			if (sqlRest.Length > 0) {
				sqlRest = sqlRest.Substring (4, sqlRest.Length - 4);
				sql += " WHERE " + sqlRest;
			}
			
			var autos = ExecuteQuery (sql, paramList).DataTableToList<Automobile> (new Automobile ());
			foreach (var auto in autos) {
				auto.Images = GetImagesByAutoId (auto.Id, true);
			}
		    return autos;
		}
		public List<Image> GetImagesByAutoId (int autoId, bool onlyMainImage)
		{
			var sql = "SELECT * FROM Image WHERE AutomobileId = " + autoId;
			if (onlyMainImage) {
				sql += " AND IsMainImage = 1";
			}
			return ExecuteQuery (sql, null).DataTableToList<Image>(new Image());
		}
		public int AddImages (List<Image> images)
		{
		    return (from img in images
		            let sql = "INSERT INTO Image(Url, Type, IsMainImage, AutomobileId) Values(@url, @type, @IsMain, @AutoId)"
		            let paramList = new SqlParameter[]
		                                {
		                                    new SqlParameter("@url", img.Url), new SqlParameter("@type", img.IsMainImage), new SqlParameter("@IsMain", img.IsMainImage), new SqlParameter("@AutoId", img.AutomobileId)
		                                }
		            select ExecuteNonQuery(sql, paramList)).Sum();
		}
		public bool DeleteImages (int autoId, out List<Image> imgsDeleted)
		{
			imgsDeleted = GetImagesByAutoId (autoId, true);
			return ExecuteNonQuery ("DELETE FROM IMAGE WHERE AUTOMOBILEID = " + autoId) > 0;
		}
		public List<Automobile> GetByUserId (int userId)
		{
			var autos = ExecuteQuery ("SELECT * FROM Automobile WHERE UserId = " + userId).DataTableToList<Automobile>(new Automobile());
		    foreach (var automobile in autos)
		    {
		        automobile.Images = GetImagesByAutoId(automobile.Id, true);
		    }
		    return autos;
		}
	}
}

