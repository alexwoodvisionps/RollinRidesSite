using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RollingRides.WebApp.Components.Datalayer.Models
{
    [Serializable]
    public class Settings
    {
        public string CompanyPhoneNumber { get; set; }
        public string CompanyLogoUrl { get; set; }
        public string AboutUsDescription { get; set; }
        public string Address { get; set; }
        public string HomePageMovieUrl { get; set; }
        public string CouponOfTheMonthUrl { get; set; }
        public string CompanyFax { get; set; }
        public string SiteMasterEmail { get; set; }
        public string TermsAndConditions { get; set; }
    }
}