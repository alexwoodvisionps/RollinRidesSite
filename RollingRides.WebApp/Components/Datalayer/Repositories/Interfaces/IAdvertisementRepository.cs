using System.Collections.Generic;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces
{
	public interface IAdvertisementRepository
	{
		Advertisement Add(Advertisement ad);
		void Delete(int id);
		Advertisement GetAdvertisement();
		List<Advertisement> GetAllAdvertisements();
	}
}

