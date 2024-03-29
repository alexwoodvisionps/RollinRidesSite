﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Repositories;

namespace RollingRides.WebApp.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User)Session["User"];
            if(user == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            var settings = new SettingsRepository().GetSettings();
            if (settings != null)
            {
                companyLogo.ImageUrl = settings.CompanyLogoUrl;
            }
            //var user = Session["User"];
            //if (user == null) return;
            var name = user.Username;
            if (!string.IsNullOrEmpty(user.LastName) && !string.IsNullOrEmpty(user.FirstName))
                name = user.FirstName + " " + user.LastName;
            lblUsername.Text = "Hello " + name;
        }
        protected void SignOut(object sender, EventArgs ev)
        {
            Session["User"] = null;
            Session.Abandon();
            Response.Redirect("~/Login.aspx");
        }
    }
}