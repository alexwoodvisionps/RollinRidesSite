using System.Collections.Generic;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces
{
	public interface IAutomobileRepository
	{
		Automobile AddUpdate(Automobile mobile, UserType type);
		bool Delete(int Id, out List<Image> imgsDeleted);
		List<Automobile> GetAllVehicles (bool ApprovedOnly);
		Automobile GetById(int Id);
		//List<Automobile> Search(string make, string model, int? year, bool usedOnly);
		List<Image> GetImagesByAutoId(int AutoId, bool onlyMainImage);
		int AddImages(List<Image> images);
		bool DeleteImages(int autoId, out List<Image> imgsDeleted);
		List<Automobile> GetByUserId(int userId);
		int DeleteByUserId(int userId, out List<Image> imgsDeleted);
	    void DeleteImage(int id);
        List<Automobile> Search(string make, string model, decimal? minPrice, decimal? maxPrice);
	}
	
}

