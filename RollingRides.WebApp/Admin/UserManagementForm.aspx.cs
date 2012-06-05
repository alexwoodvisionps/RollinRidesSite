using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Admin
{
    public partial class UserManagementForm : System.Web.UI.Page
    {
        private readonly IUserManager _userManager;
        public UserManagementForm()
        {
            _userManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack)
            {
                return;
            }
            if (Request.QueryString["id"] == null)
            {
                Response.Redirect("~/Admin/UserManagementList.aspx");
                return;
            }
            PopulateForm(int.Parse(Request.QueryString["id"]));
            
        }
        private void PopulateForm(int id)
        {
            var user = _userManager.GetById(id);
            if(user == null)
            {
                Response.Redirect("~/Admin/UserManagementList.aspx");
                return;
            }
            txtEmail.Text = user.Email;
            txtCity.Text = user.City;
            try
            {
                ddlState.SelectedValue = user.State;
            }
            catch
            {
            }
            //txtPassword.Text = user.Password;
            txtZipCode.Text = user.ZipCode;
            txtStreet1.Text = user.Street1;
            txtStreet2.Text = user.Street2;
            txtExpiresOn.Text = user.Expires == null ? "Never" : user.Expires.Value.ToString("MM/dd/yyyy");
            lblUsername.Text = user.Username;
            lblDateJoined.Text = user.DateJoined.ToString("MM/dd/yyyy");
            txtFirstName.Text = user.FirstName;
            txtLastName.Text = user.LastName;
            txtCompanyName.Text = user.CompanyName;
            hfId.Value = user.Id.ToString();
            ddlAccountType.FillAccountType(false, ((RollingRides.WebApp.Components.Datalayer.Models.User)(Session["User"])).UserType);
            ddlAccountType.SelectedValue = user.AccountType.ToString();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Admin/UserManagementList.aspx", true);
            
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var id = int.Parse(hfId.Value);
            var u1 = _userManager.GetById(id);
            var copyUser = new Components.Datalayer.Models.User
                               {
                                   Id = int.Parse(hfId.Value),
                                   LastName = txtLastName.Text,
                                   FirstName = txtFirstName.Text,
                                   PhoneNumber = txtPhone.Text,
                                   ZipCode = txtZipCode.Text,
                                   Street1 = txtStreet1.Text,
                                   Street2 = txtStreet2.Text,
                                   Username = u1.Username,
                                   State = ddlState.SelectedValue,
                                   Password = u1.UserType == UserType.Admin ? u1.Password : txtPassword.Text,
                                   CompanyName = txtCompanyName.Text,
                                   AccountType = int.Parse(ddlAccountType.SelectedValue),
                                   City = txtCity.Text

                               };

            if(copyUser.UserType == UserType.Corporate)
            {
                DateTime dateTime;
                copyUser.Expires = DateTime.TryParse(txtExpiresOn.Text, out dateTime) ? dateTime : DateTime.Now.AddDays(30);
            }
            else
            {
                copyUser.Expires = null;
            }
            //DateTime dt;
            //string strDate = null;
            //if(DateTime.TryParse(txtExpiresOn.Text,out dt))
            //{
            //    strDate = dt.ToString("MM/dd/yyyy");
            //}
            //copyUser.Expires = u1.UserType == UserType.Admin ? u1.Expires : strDate == null ? (DateTime?) null : DateTime.Parse(strDate);
           
            copyUser.Email = u1.UserType == UserType.Admin ? u1.Email : txtEmail.Text;
            var myself = (RollingRides.WebApp.Components.Datalayer.Models.User)Session["User"];
            var use1 = _userManager.AddUpdate(copyUser, myself.UserType);
            
            if(myself.UserType == UserType.Admin)
            {
                if(!string.IsNullOrEmpty(txtPassword.Text))
                {
                    _userManager.ChangePassword(copyUser.Id, txtPassword.Text);
                }

            }
            if (use1 == null)
            {
                lblError.Text = "Failed To Update User!";
            }
            else
            {
                lblError.Text = "User Successfully Updated!";
            }
        }
    }
}