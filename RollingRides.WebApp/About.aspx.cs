using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Repositories;

namespace RollingRides.WebApp
{
    public partial class About : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (IsPostBack) 
                return;
            var settingRepo = new SettingsRepository();
            var settings = settingRepo.GetSettings();
            if (settings == null)
                return;
            lblDescription.Text = settings.AboutUsDescription;
        }
    }
}