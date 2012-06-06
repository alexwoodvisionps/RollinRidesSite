using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Repositories;

namespace RollingRides.WebApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
            var settings = new SettingsRepository().GetSettings();
            if(settings != null)
            {
                companyLogo.ImageUrl = settings.CompanyLogoUrl;
            }
            var adRepo = new AdvertisementRepository();
            var ad1 = adRepo.GetAdvertisement();
            var ad2 = adRepo.GetAdvertisement();
            var ad3 = adRepo.GetAdvertisement();
            if (ad1 != null)
            {
                litAd.Text = "<a href='" + ad1.Link + "' > <img src='" +
                              ad1.DisplayObjectUrl + "' /></a>";
            }
            if (ad2 != null)
            {
                litSideAd.Text = "<a href='" + ad2.Link + "' > <img src='" +
                              ad2.DisplayObjectUrl + "' /></a>";
            }
            if (ad3 != null)
            {
                litAd3.Text = "<a href='" + ad3.Link + "' > <img src='" +
                              ad3.DisplayObjectUrl + "' /></a>";
            }
            if (user == null)
            {
                lblUsername.Text = "Hello Guest";
                btnSignOut.Visible = false;
                return;
            }
            var name = user.Username;
            
            if (user.LastName != null && user.FirstName != null)
                name = user.FirstName + " " + user.LastName;
            lblUsername.Text = "Hello "+ name;
            
        }
        protected void SignOut(object sender, EventArgs ev)
        {
            Session["User"] = null;
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}