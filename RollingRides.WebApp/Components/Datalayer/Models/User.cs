using System;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
	[Serializable]
	public class User
	{
		private int _accountType;
	
		public User ()
		{
		}

        //public int LockedOut { get; set; }
		public string LastName { get; set;}
		public string CompanyName{ get; set;}
		public string Password { get; set;}
		public string Username { get; set;}
		public string FirstName { get; set;}
		public string Email { get; set;}
		public string PhoneNumber { get; set;}
		public int Id { get; set;}		
		public string Street1 { get; set;}
		public string Street2 {get;set;}
		public string City {get;set;}
		public string State {get;set;}
		public string ZipCode{get;set;}
		//public Location Location { get; set;}
		public UserType UserType { get {
				if (_accountType == (int)(UserType.Admin)) {
					return UserType.Admin;
				} else if (_accountType == (int) UserType.Corporate)
					return UserType.Corporate;
				else 
					return UserType.User;
			} 
		}
		public DateTime DateJoined{ get; set;}
		public DateTime? Expires { get; set;}
		public int AccountType { get { return _accountType; } set { _accountType = value;}}
	}
}

