using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;
using Image = System.Web.UI.WebControls.Image;

namespace RollingRides.WebApp.User
{
    public partial class MyVehicles : System.Web.UI.Page
    {
        private readonly IAutomobileManager _autoManager;
        public MyVehicles()
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

        protected void gvAutos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count < 5)
                return;
            var hf = e.Row.Cells[4].FindControl("hfId") as HiddenField;
            if (hf == null)
                return;
            var img = e.Row.Cells[4].FindControl("imgMain") as Image;
// ReSharper disable PossibleNullReferenceException
            var image =
                _autoManager.GetImagesByAutoId(int.Parse(hf.Value), true).SingleOrDefault();            

            img.ImageUrl = image == null ? "" : image.Url;
// ReSharper restore PossibleNullReferenceException

        }



        protected void btnEdit_Click(object sender, EventArgs e)
        {
            var id = int.Parse(((Button) sender).CommandArgument);
            Response.Redirect("~/User/MyAutoEdit.aspx?id=" + id);
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            _autoManager.Delete(int.Parse(((Button)sender).CommandArgument));

            BindData();
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
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
            if(user == null)
                return;
            
            var autos = _autoManager.GetByUserId(user.Id);
            autos = ViewState["sortDirection"].ToString() == "ASC"
                        ? autos.OrderBy(x => x.GetProperty(ViewState["sortField"].ToString())).ToList()
                        : autos.OrderByDescending(x => x.GetProperty(ViewState["sortField"].ToString())).ToList();
            gvAutos.DataSource = autos;
            gvAutos.DataBind();
        }

        protected void btnAdd_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/User/MyAutoEdit.aspx", true);
            var auto = new Automobile();
            
        }
    }
}