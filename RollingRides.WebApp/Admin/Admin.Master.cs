using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.Datalayer.Models;
namespace RollingRides.WebApp.Admin
{
    public partial class Admin : System.Web.UI.MasterPage
    {
        protected void Page_Init(object sender, EventArgs e)
        {
            var user = (Components.Datalayer.Models.User) Session["User"];
            if(user == null || user.UserType != UserType.Admin)
            {
                Response.StatusCode = 401;
                Response.Write("<h1>401 Forbidden</h1>");
                Response.Redirect("~/Login.aspx", true);
                return;
            }
        }
    }
}