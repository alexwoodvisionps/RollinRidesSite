using System;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
	[Serializable]
	public class Image
	{
		public Image ()
		{
		}
		public string Url { get; set;}
		public Automobile Vehicle{ get; set;}
		public int AutomobileId { get; set;}
		public int IsMainImage { get; set;}
		public int Id{ get; set;}
		public int Type{ get; set;}
		public MediaType  MediaType{ get { return Type == (int) MediaType.Youtube ? MediaType.Youtube : Type == (int) MediaType.Server ? MediaType.Server : MediaType.Image; }}
		
	}
}

