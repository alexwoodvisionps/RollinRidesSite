using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Common;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;

namespace RollingRides.WebApp
{
    public partial class AutoListing : System.Web.UI.Page
    {
        private readonly IAutomobileManager _autoManager;
        public AutoListing()
        {
            _autoManager = new AutomobileManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                ViewState["sortDirection"] = "ASC";
                ViewState["sortField"] = "Make";
            }
            BindData();
            gvAutos.Sorting += new GridViewSortEventHandler(gvAutos_Sorting);
            gvAutos.PageIndexChanging += new GridViewPageEventHandler(gvAutos_PageIndexChanging);
            gvAutos.RowCreated += new GridViewRowEventHandler(gvAutos_RowCreated);
        }

        protected void gvAutos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count < 10)
                return;
            var hf = e.Row.Cells[9].FindControl("hfId") as HiddenField;
            if (hf == null)
                return;
            var img = e.Row.Cells[9].FindControl("imgMain") as Image;
// ReSharper disable PossibleNullReferenceException
            var image =
                _autoManager.GetImagesByAutoId(int.Parse(hf.Value), true).SingleOrDefault();
            

            img.ImageUrl = image == null ? "" : image.Url;
// ReSharper restore PossibleNullReferenceException

        }



        protected void btnDetails_Click(object sender, EventArgs e)
        {
            var id = int.Parse(((Button) sender).CommandArgument);
            Response.Redirect("~/AutoDetail.aspx?id=" + id);
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
                        ? autos.OrderByDescending(x => x.IsHighlight).ThenBy(x => x.GetProperty(ViewState["sortField"].ToString())).ToList()
                        : autos.OrderByDescending(x => x.IsHighlight).ThenByDescending(x => x.GetProperty(ViewState["sortField"].ToString())).ToList();
            gvAutos.DataSource = autos;
            gvAutos.DataBind();
        }
    }
}