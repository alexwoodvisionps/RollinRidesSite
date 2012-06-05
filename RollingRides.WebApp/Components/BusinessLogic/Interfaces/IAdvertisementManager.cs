using System.Collections.Generic;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.BusinessLogic.Interfaces
{
	public interface IAdvertisementManager
	{
		Advertisement GetAdvertisement(string[] keywords);
		Advertisement AddUpdate(string link, string mediaObjectPath, byte[] content, string[] keywords);
		void Delete(long Id);
		List<Advertisement> GetAllAdvertisements();
	}
	
}

