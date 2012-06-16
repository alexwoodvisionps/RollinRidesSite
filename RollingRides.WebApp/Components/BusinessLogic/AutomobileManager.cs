using System;
using System.Collections.Generic;
using System.IO;
using System.Configuration;
using System.Linq;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;

namespace RollingRides.WebApp.Components.BusinessLogic
{
	public class AutomobileManager : IAutomobileManager
	{
		private readonly IAutomobileRepository _autoRepo;
		public AutomobileManager ()
		{
			_autoRepo = new AutomobileRepository ();
		}
        public IEnumerable<Automobile> GetByUserId(int userId)
        {
            return _autoRepo.GetByUserId(userId);
        }

	    public void DeleteImage(int id, int autoId, int userId)
	    {
	        var auto = _autoRepo.GetById(autoId);
            if (auto.UserId != userId)
                throw new Exception("You don't own this car");
	        
	        foreach (var image in auto.Images.Where(image => image.Id == id))
	        {
	            _autoRepo.DeleteImage(id);
	        }
	    }

	    public List<Automobile> Search(string make, string model, decimal? minPrice, decimal? maxPrice)
	    {
	        return _autoRepo.Search(make, model, minPrice, maxPrice);
	    }

	    public Automobile AddUpdate(Automobile auto, UserType type)
        {
            return _autoRepo.AddUpdate(auto, type);
        }
        public IEnumerable<Image> GetImagesByAutoId(int autoId, bool onlyMainImage)
        {
            return _autoRepo.GetImagesByAutoId(autoId, onlyMainImage);
        }

	    public Automobile AddUpdate (int Id, string make, string model, int year, string title, 
		                     string description, string carfaxReportPath, byte[] carFaxContent,
		                     double price, string phoneNumber, List<string> fileNames, string mainImageName, List<byte[]> imageContents, 
		                     string street1, string street2, bool isHighlight, bool isUsed,
		                     string city, string state, string zipCode, int userId, UserType utype, string contactName)
		{
			var auto = new Automobile ();
			if ((!string.IsNullOrEmpty (carfaxReportPath)) && (!StringHelper.IsValidCarFax (carfaxReportPath))) {
				throw new Exception ("Car Fax Report is not in the right format only pdfs and MS Word documents allowed!");	
			}
		    auto.ContactName = StringHelper.RemovePossibleXSS(contactName);
			auto.CarfaxReportPath = carfaxReportPath;
			auto.City = StringHelper.RemovePossibleXSS (city);
			auto.Description = StringHelper.RemovePossibleXSS (description);
			auto.IsHighlight = isHighlight ? 1 : 0;
			auto.IsUsed = isUsed ? 1 : 0;
			auto.Make = StringHelper.RemovePossibleXSS (make);
			auto.Model = StringHelper.RemovePossibleXSS (model);
			auto.Price = (decimal) price;
			auto.UserId = userId;
			auto.Title = StringHelper.RemovePossibleXSS (title);
			auto.State = StringHelper.RemovePossibleXSS (state);
			auto.Street1 = StringHelper.RemovePossibleXSS (street1);
			if (!StringHelper.IsValidTelephone (phoneNumber))
				throw new Exception ("Phone Number is Invalid!");
			auto.PhoneNumber = phoneNumber;
			auto.ZipCode = StringHelper.RemovePossibleXSS (zipCode);
			auto = _autoRepo.AddUpdate (auto, utype);
			var imgs = new List<Image> ();
			var imgCount = 0;
			foreach (var imgFile in fileNames) {
				try {
					var type = StringHelper.GetMediaType (imgFile);
					var img = new Image
					              {
					                  IsMainImage = imgFile.EndsWith(mainImageName) ? 1 : 0,
					                  Type = (int) type
					              };
				    if (type != MediaType.Youtube) {
						img.Url = "/images/automobiles/" + auto.Id + "/" + StringHelper.MakeFileSafe (imgFile);
					} else {
						try {
							var uri = new Uri (imgFile);
							img.Url = imgFile;
						} catch {
							continue;
						}
					}
					img.AutomobileId = auto.Id;
					imgs.Add (img);
					//imgCount++;
				} catch {
					
				}
			}
			_autoRepo.AddImages (imgs);
			foreach (var img in imgs) {
                if (img.MediaType != MediaType.Youtube)
                {
                    var lastSlash = img.Url.LastIndexOf('/');
                    var path = ConfigurationManager.AppSettings["MediaRootDir"] + img.Url.Substring(0, lastSlash);
                    if (!Directory.Exists(path))
                        Directory.CreateDirectory(path);
                    File.WriteAllBytes(ConfigurationManager.AppSettings["MediaRootDir"] + img.Url,
                                       imageContents[imgCount]);
                }
			    imgCount++;	
			}
			imgs = _autoRepo.GetImagesByAutoId (auto.Id, true);
			auto.Images = imgs;
			
			return auto;
		}
	
		public void Delete (int id)
		{
			List<Image> imgs = null;
			_autoRepo.Delete (id, out imgs);
            //foreach(var img in imgs)
            //{
            //    if(((int)img.Type) != ((int)MediaType.Youtube))
            //        if(File.Exists(ConfigurationManager.AppSettings["MediaRootDir"] + img.Url))
            //        {
            //            File.Delete(ConfigurationManager.AppSettings["MediaRootDir"] + img.Url);
            //        }
            //}
		}
		
		public List<Automobile> GetAllVehicles (bool onlyApproved)
		{
			return _autoRepo.GetAllVehicles (onlyApproved);
		}
		
		public Automobile GetById (int Id)
		{
			return _autoRepo.GetById(Id);
		}
	}
}

