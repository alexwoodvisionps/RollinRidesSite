using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Admin
{
    public partial class UserManagementList : System.Web.UI.Page
    {
        private readonly IUserManager _userManager; 
        public UserManagementList()
        {
            _userManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if(!Page.IsPostBack)
            {
                ddlUserType.Items.Clear();
                ddlUserType.Items.Add(new ListItem("Choose A User Account Type",""));
                ddlUserType.Items.Add(new ListItem("Administrator", "1"));
                ddlUserType.Items.Add(new ListItem("Corporate","2"));
                ddlUserType.Items.Add(new ListItem("Basic User", "3"));
                ViewState["sortField"] = "Username";
                ViewState["sortDirection"] = "ASC";
                BindOriginalData();
            }
            
            BindData();
            gvUsers.Sorting += new GridViewSortEventHandler(gvUsers_Sorting);
            gvUsers.PageIndexChanging += new GridViewPageEventHandler(gvUsers_PageIndexChanging);
            //gvUsers.RowCommand += new GridViewCommandEventHandler(gvUsers_RowCommand);
            //gvUsers.RowCreated += new GridViewRowEventHandler(gvUsers_RowCreated);
        }

       

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _userManager.Delete(int.Parse(((Button)sender).CommandArgument));
            BindOriginalData();
            BindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var id = int.Parse(((Button) sender).CommandArgument);
            Response.Redirect("~/Admin/UserManagementForm.aspx?id=" + id, true);
        }

      

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            gvUsers.PageIndex = e.NewPageIndex;
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            if(ViewState["sortField"].ToString().Equals(e.SortExpression))
            {
                ViewState["sortDirection"] = ViewState["sortDirection"].ToString() == "ASC" ? "DESC" : "ASC";
            }
            else
            {
                ViewState["sortField"] = e.SortExpression;
                ViewState["sortDirection"] = "ASC";
            }
            BindData();
        }

        private void BindOriginalData()
        {
           
            if(ViewState["sortDirection"].ToString().Equals("ASC"))
                ViewState["Users"] = _userManager.GetAllUsers().OrderBy(x => x.GetProperty(ViewState["sortField"].ToString())).ToList();
            else
            {
                ViewState["Users"] =
                    _userManager.GetAllUsers().OrderByDescending(x => x.GetProperty(ViewState["sortField"].ToString())).
                        ToList();
            }
            
        }
        private void BindData()
        {
            var users = ViewState["Users"] as List<Components.Datalayer.Models.User>;
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }

        protected void btnAddUser_Click(object sender, EventArgs e)
        {
            lblError.Text = "";
            var sb = new StringBuilder("<ul>");
            var errorFound = false;
            if(txtPassword.Text.Length < 6 && !cbxAutoPass.Checked)
            {
                errorFound = true;
                sb.AppendLine("<li><b>A Password of 6 characters or more is required or Autogenerate must be checked!</b></li>");                
            }
            var pass = cbxAutoPass.Checked ? Guid.NewGuid().ToString().Substring(0, 10) : txtPassword.Text;
            if(!StringHelper.IsValidEmail(txtEmail.Text))
            {
                errorFound = true;
                sb.AppendLine("<li><b>The Email Address entered is not in a valid email address format!</b></li>");
            }
            if(ddlUserType.SelectedValue == "" || int.Parse(ddlUserType.SelectedValue) > 3 || int.Parse(ddlUserType.SelectedValue) < 1)
            {
                errorFound = true;
                sb.AppendLine("<li><b>You must choose an Account Type</b></li>");
            }
            lblError.Text = sb.ToString() + "</ul>";
            if(errorFound)
            {
                return;
            }
            try
            {
                var user = new RollingRides.WebApp.Components.Datalayer.Models.User
                               {
                                   Password = txtPassword.Text,
                                   Username = txtUsername.Text,
                                   Email = txtEmail.Text,
                                   AccountType = int.Parse(ddlUserType.SelectedValue),
                                   DateJoined = DateTime.Now,
                                   State = "IL",
                                   City = "",
                                   ZipCode = "",
                                   Street1 = "",
                                   Street2 = "",
                                   CompanyName = "",
                                   Expires = ddlUserType.SelectedValue == ((int)UserType.Corporate).ToString() ? DateTime.Now.AddDays(31) : (DateTime?) null,
                                   FirstName = "",
                                   LastName = "",
                                   PhoneNumber = ""
                               };
                _userManager.AddUpdate(user, UserType.Admin);
                BindOriginalData();
                BindData();
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
            }
        }
    }
}