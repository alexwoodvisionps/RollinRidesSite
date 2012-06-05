using System.Collections.Generic;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.BusinessLogic.Interfaces
{
	public interface IUserManager
	{
		RollingRides.WebApp.Components.Datalayer.Models.User RegisterUser(string username, string password, string email, string phoneNumber,
		                  string firstName, string lastName,
		                  string street1, string street2,
		                  string city, string state, string zipcode, 
		                  UserType type, string companyName);
		RollingRides.WebApp.Components.Datalayer.Models.User ValidateLogin(string username, string password);
		string ResetPassword(string email);
		void Delete(int userId);
		List<RollingRides.WebApp.Components.Datalayer.Models.User> GetAllUsers();
		bool ChangePassword(int userId, string oldPassword, string newPassword); 
		RollingRides.WebApp.Components.Datalayer.Models.User GetById(int Id);
	    RollingRides.WebApp.Components.Datalayer.Models.User AddUpdate(RollingRides.WebApp.Components.Datalayer.Models.User user, UserType type = UserType.User);
	    void ChangePassword(int userId, string newPassword);
	}
}

