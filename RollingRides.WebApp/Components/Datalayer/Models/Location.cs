using System;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
	[Serializable]
	public class Location
	{
		public Location ()
		{
		}
		public string Street1 { get; set;}
		public string Street2 {get;set;}
		public string City {get;set;}
		public string State {get;set;}
		public string ZipCode{get;set;}
		
	}
}

