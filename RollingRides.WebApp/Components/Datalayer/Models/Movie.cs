using System;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
	[Serializable]
	public class Movie
	{
		public Movie ()
		{
		}
		
		public string Url { get; set;}
		public MediaType  MediaType{ get { return Type == (int) MediaType.Youtube ? MediaType.Youtube : Type == (int) MediaType.Server ? MediaType.Server : MediaType.Image; }}
		public int AutomobileId{ get; set;}
		public int Type{ get; set;}
		public int Id { get; set;}
	}
	
	[Serializable]
	public enum MediaType : int 
	{ 
		Youtube = 1, Server =2, Image = 3, MainPage = 4, AboutUs = 5 
	}
}

