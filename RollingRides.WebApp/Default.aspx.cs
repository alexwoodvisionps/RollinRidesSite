using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Repositories;
using RollingRides.WebApp.Controls;

namespace RollingRides.WebApp
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           
            LoadMovie();
            
        }
        private void LoadMovie()
        {
            var movieControl = new MovieControl {ID = "MainMovie", Height = 300, Width = 400};
            var settingsRepo = new SettingsRepository();
            var settings = settingsRepo.GetSettings();
            if (settings == null)
                return;
            movieControl.Url = settings.HomePageMovieUrl;
            if (!string.IsNullOrEmpty(settings.CouponOfTheMonthUrl))
                litCoupon.Text = "<a href='" + settings.CouponOfTheMonthUrl + "'>Check Here</a> To Download The Coupon Of The Month";
            pnlMovie.Controls.Add(movieControl);
        }
        
    }
}