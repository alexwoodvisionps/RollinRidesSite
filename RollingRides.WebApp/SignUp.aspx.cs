using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp
{
    public partial class SignUp : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ddlState.SelectedValue = "IL";
            }
        }
        protected void CreateAccount(object sender, EventArgs e)
        {
            if (!cbxAgree.Checked)
            {
                lblError.Text = "You must agree to the terms and conditions!";
                return;
            }
            var userManager = new UserManager();
            if(txtPassword.Text != txtPasswordConfirm.Text)
            {
                lblError.Text = "Passwords Do Not Match";
                return;
            }
            try
            {
                userManager.RegisterUser(txtUsername.Text, txtPassword.Text, txtEmail.Text, txtPhone.Text,
                                         txtFirstName.Text,
                                         txtLastName.Text,
                                         txtStreet1.Text, txtStreet2.Text, txtCity.Text, ddlState.SelectedValue,
                                         txtZipCode.Text, UserType.User, txtCompanyName.Text);
                lblError.Text = "<strong>Account Sucessfully Created! Click <a href='/Login.aspx' >here</a> To Login</strong>";
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}