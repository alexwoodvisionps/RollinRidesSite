using System;
using System.Collections.Generic;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
	[Serializable]
	public class Automobile
	{
		public Automobile ()
		{
		}
		public int Id{get;set;}
		public string Make{ set; get;}
		public string Model { get; set;}
		public int Year { get; set;}
		public string Description{ get; set;}
		public string Title{ get; set;}
		public string CarfaxReportPath{ get; set;}
		public List<Image> Images { get; set;}
        public string ContactName { get; set; }
		//public Location Location { get; set;}
		public int IsHighlight {get;set;}
		public string Street1 { get; set;}
		public string Street2 {get;set;}
		public string City {get;set;}
		public string State {get;set;}
		public string ZipCode{get;set;}
		public User Seller { get; set;} 
		public int UserId {get;set;}
		public decimal Price{get;set;}
		public string PhoneNumber { get; set;}
		public int IsUsed { get; set;}
        public int IsApproved { get; set; }
        public int HasFinancing { get; set; }
        public decimal? MinimumDownPayment { get; set; }
	    private string _color;
        public string Color { set { _color = (value ?? "").ToLower(); } get { return _color; } }
	}
}

