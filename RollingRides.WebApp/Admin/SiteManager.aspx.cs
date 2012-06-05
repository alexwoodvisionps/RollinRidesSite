using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.Datalayer.Models;
using RollingRides.WebApp.Components.Datalayer.Repositories;
using RollingRides.WebApp.Components.Datalayer.Repositories.Interfaces;

namespace RollingRides.WebApp.Admin
{
    public partial class SiteManager : System.Web.UI.Page
    {
        private readonly ISettingsRepository _settingRepository;
        private readonly IAdvertisementRepository _adRepo;
        public SiteManager()
        {
            _adRepo = new AdvertisementRepository();
            _settingRepository = new SettingsRepository();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var settings = _settingRepository.GetSettings();
                if (settings != null)
                {
                    if (!string.IsNullOrEmpty(settings.CompanyPhoneNumber))
                        txtPhone.Text = settings.CompanyPhoneNumber;
                    if (!string.IsNullOrEmpty(settings.CompanyFax))
                        txtFax.Text = settings.CompanyFax;
                    if (!string.IsNullOrEmpty(settings.AboutUsDescription))
                        txtDescription.Text = settings.AboutUsDescription;
                    if (!string.IsNullOrEmpty(settings.Address))
                        txtAddress.Text = settings.Address;
                    if (!string.IsNullOrEmpty(settings.CompanyLogoUrl))
                        imgCurrentLogo.ImageUrl = settings.CompanyLogoUrl;
                }
             
            }
            BindAds();
        }
        private void BindAds()
        {
            var ads = _adRepo.GetAllAdvertisements();
            if (ads.Count == 0)
            {
                return;
            }
            gvAdvertisers.DataSource = ads;
            gvAdvertisers.DataBind();
        }
        protected void DeleteAdvertisement(object sender, EventArgs ev)
        {
            _adRepo.Delete(int.Parse(((Button)sender).CommandArgument));
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            
            var settings = _settingRepository.GetSettings() ?? new Settings();
            settings.Address = StringHelper.RemovePossibleXSS(txtAddress.Text);
            settings.AboutUsDescription = StringHelper.RemovePossibleXSS(txtDescription.Text);
            //ConfigurationManager.AppSettings["RollinSettingsDir"]
            if(fuMovie.HasFile)
            {
                settings.HomePageMovieUrl = ConfigurationManager.AppSettings["RollinSettingsDir"] +
                                           StringHelper.MakeFileSafe(fuMovie.FileName);
                fuMovie.SaveAs(ConfigurationManager.AppSettings["RollinSettingsDir"] + fuMovie.FileName);
            }
            if(fuCompanyLogo.HasFile)
            {
                settings.CompanyLogoUrl = ConfigurationManager.AppSettings["RollinSettingsDir"] + fuCompanyLogo.FileName;
                fuCompanyLogo.SaveAs(ConfigurationManager.AppSettings["RollinSettingsDir"] + fuCompanyLogo.FileName);
            }
            if(fuCoupon.HasFile)
            {
                settings.CouponOfTheMonthUrl = ConfigurationManager.AppSettings["RollinSettingsDir"] + fuCoupon.FileName;
                fuCoupon.SaveAs(ConfigurationManager.AppSettings["RollinSettingsDir"] + fuCoupon.FileName);
            }
            _settingRepository.Save(settings);
        }
    }
}