using System.Collections.Generic;
using System.Data;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.BusinessLogic.Interfaces
{
	/*
	 		public string Make{ set; get;}
		public string Model { get; set;}
		public int Year { get; set;}
		public string Description{ get; set;}
		public string Title{ get; set;}
		public string CarfaxReportPath{ get; set;}
		public List<string> ImagesPaths { get; set;}
		public Movie AutomobileVideoUrl { get;set;} 
		public Location Location { get; set;}
		public User Seller { get; set;} 
	 */
	public interface IAutomobileManager
	{
	    Automobile AddUpdate(Automobile auto, UserType type);
        Automobile AddUpdate(int Id, string make, string model, int year, string title,
	                         string description, string carfaxReportPath, byte[] carFaxContent,
	                         double price, string phoneNumber, List<string> fileNames, string mainImageName,
	                         List<byte[]> imageContents,
	                         string street1, string street2, bool isHighlight, bool isUsed,
	                         string city, string state, string zipCode, int userId, UserType utype, string contactName);
		void Delete(int id);
		List<Automobile> GetAllVehicles(bool onlyApproved);
		Automobile GetById(int id);
	    IEnumerable<Image> GetImagesByAutoId(int autoId, bool onlyMainImage);
	    IEnumerable<Automobile> GetByUserId(int userId);
	    void DeleteImage(int id, int autoId, int userId);
	    List<Automobile> Search(string make, string model, decimal? minPrice, decimal? maxPrice);
	}
}

