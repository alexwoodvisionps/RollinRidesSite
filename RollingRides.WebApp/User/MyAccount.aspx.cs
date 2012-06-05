using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;

namespace RollingRides.WebApp.User
{
    public partial class MyAccount : System.Web.UI.Page
    {
        private readonly IUserManager _userManager;
        public MyAccount()
        {
            _userManager = new UserManager();
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
                lblUsername.Text = user.Username;
                lblAccountType.Text = user.UserType.ToString();
                txtCity.Text = user.City;
                txtZipCode.Text = user.ZipCode;
                txtStreet2.Text = user.Street2;
                txtStreet1.Text = user.Street1;
                txtCompanyName.Text = user.CompanyName;
                txtPhone.Text = user.PhoneNumber;
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}