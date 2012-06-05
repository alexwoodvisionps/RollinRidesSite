using System;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
	[Serializable]
	public class Advertisement
	{
		public Advertisement ()
		{
		}
		
		public string Link{ get; set;}
		public string DisplayObjectUrl{ get; set;}
		public int Id{ get; set;}
        public string CompanyName { get; set; }
	}
}

