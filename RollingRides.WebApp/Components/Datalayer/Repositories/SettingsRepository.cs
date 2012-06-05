using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using RollingRides.WebApp.Components.Datalayer.Common;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;

namespace RollingRides.WebApp.Components.Datalayer.Repositories
{
    public class SettingsRepository : GenericRepository<Settings>, ISettingsRepository
    {
        public Settings GetSettings()
        {
            var dt = ExecuteQuery("SELECT Top 1 * From Settings");
            if (dt.Rows.Count == 0)
                return null;
            return dt.Rows[0].DataRowToModel<RollingRides.WebApp.Components.Datalayer.Models.Settings>(new RollingRides.WebApp.Components.Datalayer.Models.Settings());
        }

        public void Save(Settings settings)
        {
            ExecuteNonQuery("DELETE FROM Settings");
            var parameters = new[]
                                 {
                                     new SqlParameter("@AboutUs", settings.AboutUsDescription),
                                     new SqlParameter("@Fax", settings.CompanyFax == null ? DBNull.Value : (object) settings.CompanyFax), 
                                     new SqlParameter("@Address", settings.Address),
                                     new SqlParameter("@CompanyLogoUrl", settings.CompanyLogoUrl),
                                     new SqlParameter("@Phone", settings.CompanyPhoneNumber),
                                     new SqlParameter("@Coupon", settings.CouponOfTheMonthUrl),
                                     new SqlParameter("@Movie", settings.HomePageMovieUrl) 
                                 };
            const string sql = "INSERT INTO SETTINGS (AboutUsDescription, CompanyFax, Address, CompanyLogoUrl, CompanyPhoneNumber, CouponOfTheMonthUrl, HomePageMovieUrl)" +
                               " VALUES(@AboutUs, @Fax, @Address, @CompanyLogoUrl, @Phone, @Coupon, @Movie)";
            ExecuteNonQuery(sql, parameters);
        }
    }
}