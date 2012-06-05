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
            movieControl.Url = settings.HomePageMovieUrl;
            pnlMovie.Controls.Add(movieControl);
        }
        
    }
}