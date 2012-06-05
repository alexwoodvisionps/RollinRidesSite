using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;

namespace RollingRides.WebApp.Admin
{
    public partial class Billing : System.Web.UI.Page
    {
        private readonly IUserManager _userManager;
        public Billing()
        {
            _userManager = new UserManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["sortField"] = "LastName";
                ViewState["sortDirection"] = "ASC";
            }
            BindData();
            gvUsers.Sorting += new GridViewSortEventHandler(gvUsers_Sorting);
            gvUsers.PageIndexChanging += new GridViewPageEventHandler(gvUsers_PageIndexChanging);
        }

        protected void gvUsers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            gvUsers.PageIndex = e.NewPageIndex;
        }

        protected void gvUsers_Sorting(object sender, GridViewSortEventArgs e)
        {
            if(ViewState["sortField"].ToString() == e.SortExpression)
            {
                if (ViewState["sortDirection"].ToString() == "ASC")
                    ViewState["sortDirection"] = "DESC";
                else
                {
                    ViewState["sortDirection"] = "ASC";
                }
            }
            else
            {
                ViewState["sortDirection"] = "ASC";
                ViewState["sortField"] = e.SortExpression;
            }
        }
        private void BindData()
        {
            var users = _userManager.GetAllUsers().Where(x => x.UserType == UserType.Corporate);
            users = ViewState["sortDirection"].ToString() == "ASC"
                        ? users.OrderBy(x => x.GetProperty(ViewState["sortField"].ToString())).ToList()
                        : users.OrderByDescending(x => x.GetProperty(ViewState["sortField"].ToString())).ToList();
            gvUsers.DataSource = users;
            gvUsers.DataBind();
        }

        protected void MarkPaid(object sender, EventArgs e)
        {
            var user = _userManager.GetById(int.Parse(((Button)sender).CommandArgument));
            user.Expires = user.Expires == null || DateTime.Now > user.Expires.Value
                               ? DateTime.Now.AddDays(30)
                               : user.Expires.Value.AddDays(30);
            _userManager.AddUpdate(user);
        }
        protected void EmailCustomer(object sender, EventArgs e)
        {
            var user = _userManager.GetById(int.Parse(((Button) sender).CommandArgument));
            var emailer = EmailerFactory.NewDefaultInstance();
            var days = "Past Due!";
            if(DateTime.Now <= user.Expires.Value)
            {
                var timespan = DateTime.Now.Subtract(user.Expires.Value);
                days = "due in " + timespan.Days;
            }
            
           
            var name = !string.IsNullOrEmpty(user.CompanyName)
                           ? user.CompanyName
                           : !string.IsNullOrEmpty(user.LastName) && !string.IsNullOrEmpty(user.FirstName)
                                 ? user.FirstName + " " + user.LastName
                                 : user.Username; 
            var body = "Hello " + name + ", <br/>This email is to let you know your bill for your coporate account is " + days
            +"<br/>Please Visit Us At www.RollingRides.com/Contact.aspx and Contact Us to make a payment.<br/>Thank you<br/>Sincerely,<br/>The Rolling Rides Team!";
            var subject = "The Rolling Rides Team - Your bill is " + days;
            emailer.SendHtmlEmail(ConfigurationManager.AppSettings["FromEmail"], user.Email, subject, body);
        }
    }
}