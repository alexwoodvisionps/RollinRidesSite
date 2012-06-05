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
            if (user == null) return;
            var name = user.Username;
            if (user.LastName != null && user.FirstName != null)
                name = user.FirstName + " " + user.LastName;
            lblUsername.Text = "Hello "+ name;
            var adRepo = new AdvertisementRepository();
            var ad1 = adRepo.GetAdvertisement();
            var ad2 = adRepo.GetAdvertisement();
            var ad3 = adRepo.GetAdvertisement();
            if(ad1 != null)
            {
                litAd.Text = "<a href='" + ad1.Link + "' > <img src='" +
                             ConfigurationManager.AppSettings["AdvertisementDir"] + ad1.DisplayObjectUrl + "' />";
            }
            if(ad2 != null)
            {
                litSideAd.Text = "<a href='" + ad2.Link + "' > <img src='" +
                             ConfigurationManager.AppSettings["AdvertisementDir"] + ad2.DisplayObjectUrl + "' />";
            }
            if(ad3 != null)
            {
                litAd3.Text = "<a href='" + ad3.Link + "' > <img src='" +
                             ConfigurationManager.AppSettings["AdvertisementDir"] + ad3.DisplayObjectUrl + "' />";
            }
        }
        protected void SignOut(object sender, EventArgs ev)
        {
            Session["User"] = null;
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}