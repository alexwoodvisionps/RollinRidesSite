using System;
using System.Collections.Generic;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;

namespace RollingRides.WebApp.Components.BusinessLogic
{
	public class UserManager : IUserManager
	{
		private readonly IUserRepository _userRepository;
		public UserManager ()
		{
			_userRepository = new UserRepository ();
		}
        public RollingRides.WebApp.Components.Datalayer.Models.User RegisterUser(string username, string password, string email, string phoneNumber,
		                  string firstName, string lastName,
		                  string street1, string street2,
		                  string city, string state, string zipcode, 
		                  UserType type, string companyName)
		{
			var user = new Components.Datalayer.Models.User();
			user.AccountType =(int) type;
			user.City = city;
			user.DateJoined = DateTime.Now;
			user.Email = email;
			user.CompanyName = companyName;
			user.Expires = null;
			user.FirstName = firstName;
			user.LastName = lastName;
			user.Id = -1;
			user.State = state;
			user.Street1 = street1;
			user.Street2 = street2;
			user.ZipCode = zipcode;
			user.PhoneNumber = phoneNumber;
			user.Password = password;
			return _userRepository.AddUpdate (user); 
		}
        public RollingRides.WebApp.Components.Datalayer.Models.User ValidateLogin(string username, string password)
		{
			return _userRepository.ValidateLogin (username, password);
		}
		public string ResetPassword (string email)
		{
			var randomPass = Guid.NewGuid ().ToString ().Substring (0, 10);
			return _userRepository.ResetPassword (email, randomPass);
		}
		public void Delete (int userId)
		{
		     _userRepository.Delete (userId);
		}
        public List<RollingRides.WebApp.Components.Datalayer.Models.User> GetAllUsers()
		{
			return _userRepository.GetAllUsers ();
		}
		public bool ChangePassword (int userId, string oldPassword, string newPassword)
		{
			return _userRepository.ChangePassword (userId, oldPassword, newPassword);
		}
        public RollingRides.WebApp.Components.Datalayer.Models.User GetById(int userId)
		{
			return _userRepository.GetById (userId);
		}

	    public Datalayer.Models.User AddUpdate(Datalayer.Models.User user, UserType type = UserType.User)
	    {
	        return _userRepository.AddUpdate(user, type);
	    }

	    public void ChangePassword(int userId, string newPassword)
	    {
	        _userRepository.ChangePassword(userId, newPassword);
	    }
	}
}

