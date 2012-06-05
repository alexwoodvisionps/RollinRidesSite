using System.Collections.Generic;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces
{
	public interface IUserRepository
	{
		List<RollingRides.WebApp.Components.Datalayer.Models.User> GetAllUsers();
		RollingRides.WebApp.Components.Datalayer.Models.User ValidateLogin(string username, string password);
		string ResetPassword(string email, string newPassword);
		RollingRides.WebApp.Components.Datalayer.Models.User AddUpdate(RollingRides.WebApp.Components.Datalayer.Models.User user);
		bool Delete(int id);
		bool ChangePassword(int userId, string oldPassword, string newPassword); 
		RollingRides.WebApp.Components.Datalayer.Models.User GetById(int Id);
	}
}

