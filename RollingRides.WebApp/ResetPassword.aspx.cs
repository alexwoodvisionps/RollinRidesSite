using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Common;

namespace RollingRides.WebApp
{
    public partial class ResetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                var userLogic = new UserManager();
                var newPass = userLogic.ResetPassword(txtEmail.Text);
                if(newPass == null)
                {
                    lblMessage.Text = "This Email Address wasn't found in our system!";
                    return;
                }
                var emailer = EmailerFactory.NewDefaultInstance();
                emailer.SendHtmlEmail(ConfigurationManager.AppSettings["FromEmail"], txtEmail.Text, "The Rolling Rides Team - Password Reset", "Hello," + "<br/>Your new password is "+newPass + "<br/>Thank you,<br/>The Rolling Rides Team");
                lblMessage.Text = "Your new temporary password was emailed to your email address provided";
            }
            catch(Exception ex)
            {
                lblMessage.Text = ex.Message;
            }
        }
    }
}