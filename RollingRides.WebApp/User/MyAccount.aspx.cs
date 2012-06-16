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
                txtFirstName.Text = user.FirstName;
                txtLastName.Text = user.LastName;
                lblEmail.Text = user.Email;
                if (user.State != null)
                    ddlState.SelectedValue = user.State;
                if(user.Expires.HasValue)
                {
                    lblExpires.Text = user.Expires.Value.ToString("MM/dd/yyyy");
                }
                else
                {
                    lblExpires.Text = "Never";
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
                user.Street1 = txtStreet1.Text;
                user.Street2 = txtStreet2.Text;
                user.City = txtCity.Text;
                user.State = ddlState.SelectedValue;
                user.ZipCode = txtZipCode.Text;
                user.LastName = txtLastName.Text;
                user.FirstName = txtFirstName.Text;
                user.PhoneNumber = txtPhone.Text;
                user.CompanyName = txtCompanyName.Text;
                _userManager.AddUpdate(user, user.UserType);
            }
            catch (Exception ex)
            {
                lblError.Text = "Failed To Save your information, please contact the site administrator." + ex.Message;
            }
        }

       
    }
}