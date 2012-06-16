using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;

namespace RollingRides.WebApp.User
{
    public partial class ChangePassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            var userLogic = new UserManager();
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
            var u2 = userLogic.ValidateLogin(user.Username, txtOldPassword.Text);
            var attempts = Session["attempts"] == null ? 3 : int.Parse(Session["attempts"].ToString());
            if(u2 == null)
            {                
                lblError.Text = "Old Password Incorrect: " + (--attempts) + " attempts remain";
                Session["attemmpts"] = attempts;
                if(attempts == 0)
                {
                    Session.Abandon();
                    Response.Redirect("~/login.aspx");
                }
                return;
            }
            if(txtPassword.Text != txtConfirmPassword.Text)
            {
                lblError.Text = "New Passwords Do Not Match!";
                return;
            }
            try
            {
                userLogic.ChangePassword(user.Id, txtOldPassword.Text, txtPassword.Text);

                lblError.Text = "Password Successfully Changed!";
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}