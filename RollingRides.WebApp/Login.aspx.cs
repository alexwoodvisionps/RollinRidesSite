using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp
{
    public partial class Login : System.Web.UI.Page
    {
        private readonly IUserManager _userManager;
        public Login()
        {
            _userManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            loginFrm.Authenticate += new AuthenticateEventHandler(loginFrm_Authenticate);
            loginFrm.FailureText = "Username or Password is Invalid!";
        }

        protected void loginFrm_Authenticate(object sender, AuthenticateEventArgs e)
        {
            var user = _userManager.ValidateLogin(loginFrm.UserName, loginFrm.Password);
            if(user != null)
            {
                Session["User"] = user;
            }
            e.Authenticated = user != null;
            if (user == null) return;
            Response.Redirect(user.UserType == UserType.Admin ? "~/Admin/AdminPanel.aspx" : "~/User/MyAccount.aspx");
           
        }
    }
}