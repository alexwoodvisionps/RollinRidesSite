using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace RollingRides.WebApp.User
{
    public partial class User : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User)Session["User"];
            if(user == null)
            {
                Response.Redirect("~/Login.aspx");
                return;
            }
            //var user = Session["User"];
            //if (user == null) return;
            var name = user.Username;
            if (user.LastName != null && user.FirstName != null)
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