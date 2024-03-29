﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.Datalayer.Common;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;

namespace RollingRides.WebApp.Components.Datalayer.Repositories
{
    public class AdvertisementRepository : GenericRepository<Advertisement>, IAdvertisementRepository
    {
        public Advertisement Add(Advertisement ad)
        {
            const string sql = "INSERT INTO Advertisements (CompanyName, Link, DisplayObjectUrl, Location) Values(@CompanyName, @Link, @Url, @Loc)";
            var parameters = new SqlParameter[]
                                 {
                                     new SqlParameter("@CompanyName", StringHelper.RemovePossibleXSS(ad.CompanyName)),
                                     new SqlParameter("@Link", StringHelper.RemovePossibleXSS(ad.Link)), 
                                     new SqlParameter("@Url", StringHelper.RemovePossibleXSS(ad.DisplayObjectUrl)),
                                     new SqlParameter("@Loc", ad.Location) 
                                 };
            ExecuteNonQuery(sql, parameters);
            //ad.Id = int.Parse(ExecuteQuery("SELECT @@IDENTITY").Rows[0][0].ToString());
            return ad;
        }

        public void Delete(int id)
        {
            ExecuteNonQuery("DELETE FROM ADVERTISEMENTS WHERE Id = " + id);
        }

        public Advertisement GetAdvertisement(int location)
        {
            var dt = ExecuteQuery("SELECT * FROM Advertisements WHERE Location =  " + location);
            if(dt.Rows.Count > 0)
            {
                var rnd = new Random();
                var index = Math.Abs(rnd.Next()) % dt.Rows.Count;
                return dt.Rows[index].DataRowToModel<Advertisement>(new Advertisement());
            }
            return null;
        }

        public List<Advertisement> GetAllAdvertisements()
        {
            return ExecuteQuery("SELECT * FROM Advertisements").DataTableToList<Advertisement>(new Advertisement());
        }
    }
}