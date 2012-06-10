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
        public int Location { get; set; }
        public LocationType LocationType
        {
            get
            {
                switch (Location)
                {
                    case (int)LocationType.Top:
                        return LocationType.Top;
                    case (int)LocationType.SideBar:
                        return LocationType.SideBar;
                    default:
                        return LocationType.Footer;
                }

            }
        }
	}
    [Serializable]
    public enum LocationType : int
    {Top =1, SideBar = 2, Footer = 3 }
}

