using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Repositories;

namespace RollingRides.WebApp
{
    public partial class Contact : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var settingRepo = new SettingsRepository();
                var settings = settingRepo.GetSettings();
                if (settings == null)
                    return;
                lblPhoneNumber.Text = settings.CompanyPhoneNumber ?? "";
                lblFax.Text = settings.CompanyFax ?? "";
                lblAddress.Text = settings.Address ?? "";
                lblEmail.Text = settings.SiteMasterEmail ?? "";
            }
        }
    }
}