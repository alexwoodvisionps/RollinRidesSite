using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using  RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.Datalayer.Models;
using Image = System.Web.UI.WebControls.Image;

namespace RollingRides.WebApp.Admin
{
    public partial class VehicleManagementList : System.Web.UI.Page
    {
         private readonly IAutomobileManager _autoManager; 
        public VehicleManagementList()
        {
            _autoManager = new AutomobileManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            
            BindData();
            gvAutos.Sorting += new GridViewSortEventHandler(gvAutos_Sorting);
            gvAutos.PageIndexChanging += new GridViewPageEventHandler(gvAutos_PageIndexChanging);
            gvAutos.RowCreated += new GridViewRowEventHandler(gvAutos_RowCreated);
        }

        protected void btnApprove_Click(object sender, EventArgs e)
        {
            var btn = sender as Button;
            Update(btn, 1);
        }
        private void Update(Button btn, int value)
        {
            var id = int.Parse(btn.CommandArgument);
            var auto = _autoManager.GetById(id);
            auto.IsApproved = value;
            var user = Session["User"] as RollingRides.WebApp.Components.Datalayer.Models.User;

            _autoManager.AddUpdate(auto, user.UserType);
        }

        protected void btnUnapprove_Click(object sender, EventArgs e)
        {
            Update((Button)sender, 0);
        }

        protected void gvAutos_RowCreated(object sender, GridViewRowEventArgs e)
        {

            if (e.Row.Cells.Count < 11 || e.Row.Cells[10].Controls.Count < 5)
                return;
            var isApproved = e.Row.Cells[3].Text;
            e.Row.Cells[3].Text = isApproved == "1" ? "Yes" : "No";
            var btnA = e.Row.Cells[10].FindControl("btnApprove");
            var btnU = e.Row.Cells[10].FindControl("btnUnapprove");
            btnU.Visible = isApproved == "1";
            btnA.Visible = isApproved == "0";
            
            var btnMh = e.Row.Cells[10].FindControl("btnMakeHighlight");
            var btnRh = e.Row.Cells[10].FindControl("btnRemoveHighlight");
            var isHighlight = e.Row.Cells[4].Text;
            e.Row.Cells[4].Text = isHighlight == "1" ? "Yes" : "No";
            btnMh.Visible = isHighlight == "0";
            btnRh.Visible = isHighlight == "1";
            var img = e.Row.Cells[9].FindControl("imgMain") as Image;
            var image =
                _autoManager.GetImagesByAutoId(int.Parse(((Button) btnU).CommandArgument), true).SingleOrDefault();
            img.ImageUrl = image.Url;

        }

        protected void MakeHighlight(object sender, EventArgs e)
        {
            var auto = _autoManager.GetById(int.Parse(((Button) sender).CommandArgument));
            auto.IsHighlight = 1;
            _autoManager.AddUpdate(auto, ((RollingRides.WebApp.Components.Datalayer.Models.User)(Session["User"])).UserType);
            BindData();
        }
        protected void RemoveHighlight(object sender, EventArgs e)
        {
            var auto = _autoManager.GetById(int.Parse(((Button)sender).CommandArgument));
            auto.IsHighlight = 0;
            _autoManager.AddUpdate(auto, ((RollingRides.WebApp.Components.Datalayer.Models.User)(Session["User"])).UserType);
            BindData();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _autoManager.Delete(int.Parse(((Button)sender).CommandArgument));
           
            BindData();
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var id = int.Parse(((Button) sender).CommandArgument);
            Response.Redirect("~/Admin/UserManagementForm.aspx?id=" + id);
        }

      

        protected void gvAutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            gvAutos.PageIndex = e.NewPageIndex;
        }

        protected void gvAutos_Sorting(object sender, GridViewSortEventArgs e)
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


        private void BindData()
        {
            var autos = _autoManager.GetAllVehicles(false);
            autos = ViewState["sortDirection"].ToString() == "ASC"
                        ? autos.OrderBy(x => x.GetProperty(ViewState["sortField"].ToString())).ToList()
                        : autos.OrderByDescending(x => x.GetProperty(ViewState["sortField"].ToString())).ToList();
            gvAutos.DataSource = autos;
            gvAutos.DataBind();
        }

       
    }
}