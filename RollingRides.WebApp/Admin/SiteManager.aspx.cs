using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
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
                    if (!string.IsNullOrEmpty(settings.SiteMasterEmail))
                        txtSiteMasterEmail.Text = settings.SiteMasterEmail;
                    if (!string.IsNullOrEmpty(settings.TermsAndConditions))
                        txtTerms.Text = settings.TermsAndConditions;
                    if (!string.IsNullOrEmpty(settings.HomePageMovieUrl))
                        btnDownload.Visible = true;
                    if (!string.IsNullOrEmpty(settings.CouponOfTheMonthUrl))
                        btnDownloadCoupon.Visible = true;
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
            BindAds();
        }

        protected void btnSaveSettings_Click(object sender, EventArgs e)
        {
            
            var settings = _settingRepository.GetSettings() ?? new Settings();
            settings.Address = StringHelper.RemovePossibleXSS(txtAddress.Text);
            settings.AboutUsDescription = StringHelper.RemovePossibleXSS(txtDescription.Text);
            settings.CompanyFax = StringHelper.RemovePossibleXSS(txtFax.Text);
            settings.CompanyPhoneNumber = StringHelper.RemovePossibleXSS(txtPhone.Text);
            //ConfigurationManager.AppSettings["RollinSettingsDir"]
            if(fuMovie.HasFile)
            {
                settings.HomePageMovieUrl = ConfigurationManager.AppSettings["RollinSettingsDir"] +
                                           StringHelper.MakeFileSafe(fuMovie.FileName);
                fuMovie.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["RollinSettingsDir"]) + fuMovie.FileName);
            }
            else
            {
                if (settings.HomePageMovieUrl == null)
                    settings.HomePageMovieUrl = "";
            }
            if(fuCompanyLogo.HasFile)
            {
                settings.CompanyLogoUrl = ConfigurationManager.AppSettings["RollinSettingsDir"] + fuCompanyLogo.FileName;
                fuCompanyLogo.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["RollinSettingsDir"]) + fuCompanyLogo.FileName);
            }
            else
            {
                if (settings.CompanyLogoUrl == null)
                    settings.CompanyLogoUrl = "";
            }
            if(fuCoupon.HasFile)
            {
                settings.CouponOfTheMonthUrl = ConfigurationManager.AppSettings["RollinSettingsDir"] + fuCoupon.FileName;
                fuCoupon.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["RollinSettingsDir"]) + fuCoupon.FileName);
            }
            else
            {
                if(settings.CouponOfTheMonthUrl == null)
                    settings.CouponOfTheMonthUrl = "";
            }
            if(!string.IsNullOrWhiteSpace(txtSiteMasterEmail.Text))
            {
                if (StringHelper.IsValidEmail(txtSiteMasterEmail.Text))
                    settings.SiteMasterEmail = txtSiteMasterEmail.Text;
                else
                {
                    lblError.Text = "Site Administrator Email Address is invalid!";
                }
                
            }
            else
            {
                if (settings.SiteMasterEmail == null)
                    settings.SiteMasterEmail = "";
            }
            if(!string.IsNullOrEmpty(txtTerms.Text))
            {
                settings.TermsAndConditions = txtTerms.Text;
            }
            else
            {
                if (settings.TermsAndConditions == null)
                    settings.TermsAndConditions = "";
            }
            _settingRepository.Save(settings);
            lblError.Text = "Settings Successfully Updated!";
            BindAds();
        }

        protected void btnAddAvertiser_Click(object sender, EventArgs e)
        {
            _adRepo.Add(new Advertisement { CompanyName = StringHelper.RemovePossibleXSS(txtCompany.Text), Link = StringHelper.RemovePossibleXSS(txtCompanyUrl.Text), DisplayObjectUrl = ConfigurationManager.AppSettings["AdvertisementDir"] + fuAdvertiser.FileName });
            fuAdvertiser.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["AdvertisementDir"]) + fuAdvertiser.FileName);
            lblError.Text = "Advertiser Successfully Added";
            BindAds();
        }

        protected void btnDownload_Click(object sender, EventArgs e)
        {
            var settings = _settingRepository.GetSettings();
            if (settings == null || settings.HomePageMovieUrl == null)
                return;
            Response.Clear();
            //var ms = new MemoryStream(stringBytes);
            Response.ContentType = "text/csv";
            Response.AddHeader("content-disposition", "attachment;filename=" + settings.HomePageMovieUrl.Substring(settings.HomePageMovieUrl.LastIndexOf("/"), settings.HomePageMovieUrl.Length - settings.HomePageMovieUrl.LastIndexOf("/")));
            Response.Buffer = true;
            //ms.WriteTo(httpContext.Response.OutputStream);
            Response.TransmitFile((settings.HomePageMovieUrl));
            Response.End();   
        }
    }
}