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

namespace RollingRides.WebApp
{
    public partial class AutoSearch : System.Web.UI.Page
    {
        private readonly IAutomobileManager _autoManager;
        public AutoSearch()
        {
            _autoManager = new AutomobileManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                ViewState["sortField"] = "Make";
                ViewState["sortDirection"] = "ASC";
                BindDropDowns();
            }
            ddlMake.SelectedIndexChanged += ddlMake_SelectedIndexChanged;
            gvAutos.Sorting += gvAutos_Sorting;
            gvAutos.PageIndexChanging += gvAutos_PageIndexChanging;
            gvAutos.RowCreated += new GridViewRowEventHandler(gvAutos_RowCreated);
            BindData();
        }

        private void gvAutos_RowCreated(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.Cells.Count < 5)
                return;
            var hf = e.Row.Cells[4].FindControl("hfId") as HiddenField;
            if (hf == null)
                return;
            var auto = (Automobile)e.Row.DataItem;
            var img = e.Row.Cells[4].FindControl("imgMain") as Image;
            // ReSharper disable PossibleNullReferenceException
            var image =
                _autoManager.GetImagesByAutoId(auto.Id, true).FirstOrDefault();

            img.ImageUrl = image == null ? "" : image.Url;
            // ReSharper restore PossibleNullReferenceException
        }

        protected void ddlMake_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ddlMake.SelectedValue)) 
                return;
            var models =
                _autoManager.GetAllVehicles(true).Where(x => x.Make == ddlMake.SelectedValue).OrderBy(x => x.Model).Select(
                    x => new {Model = x.Model}).Distinct().ToList();
            ddlModel.Items.Clear();
            ddlModel.Items.Add(new ListItem("Choose A Model",""));
            ddlModel.AppendDataBoundItems = true;
            ddlModel.DataTextField = "Model";
            ddlModel.DataValueField = "Model";
            ddlModel.DataSource = models;
            ddlModel.DataBind();
            BindData();
        }
        private void BindDropDowns()
        {
            ddlMake.Items.Add(new ListItem("Choose Make", ""));
            ddlMake.AppendDataBoundItems = true;
            ddlMake.DataValueField = "Make";
            ddlMake.DataTextField = "Make";
            ddlMake.DataSource = _autoManager.GetAllVehicles(true).Select(x => new {Make = x.Make}).Distinct().ToList();
            ddlMake.DataBind();
        }

        private static decimal? GetValue(TextBox txt)
        {
            if (string.IsNullOrEmpty(txt.Text))
                return null;
            return decimal.Parse(txt.Text);
        }
        public static string GetValue(DropDownList ddl)
        {
            return ddl.Items.Count == 0 || ddl.SelectedValue == "" ? null : ddl.SelectedValue;
        }

        private void BindData()
        {
            if(ddlMake.SelectedValue == "" && ddlModel.SelectedValue == "" && txtMin.Text == "" && txtMax.Text == "")           
            {
                return;
            }
            var autos = _autoManager.Search(GetValue(ddlMake), GetValue(ddlModel), GetValue(txtMin), GetValue(txtMax));
            autos = ViewState["sortDirection"].ToString() == "ASC"
                        ? autos.OrderByDescending(x => x.IsHighlight).ThenBy(
                            x => x.GetProperty(ViewState["sortField"].ToString())).ToList()
                        : autos.OrderByDescending(x => x.IsHighlight).OrderByDescending(
                            x => x.GetProperty(ViewState["sortField"].ToString())).ToList();
            gvAutos.DataSource = autos;
            gvAutos.DataBind();
        }
        protected void btnDetails_Click(object sender, EventArgs e)
        {
            var id = int.Parse(((Button)sender).CommandArgument);
            Response.Redirect("~/AutoDetail.aspx?id=" + id);
        }



        protected void gvAutos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            BindData();
            gvAutos.PageIndex = e.NewPageIndex;
        }

        protected void gvAutos_Sorting(object sender, GridViewSortEventArgs e)
        {
            if (ViewState["sortField"].ToString().Equals(e.SortExpression))
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
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            if(ddlMake.SelectedValue == "" && ddlModel.SelectedValue == "" && txtMin.Text == "" && txtMax.Text == "")
            {
                lblError.Text = "You Entered No Search Criteria!";
                return;
            }
            BindData();
        }

        protected void ddlModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            BindData();
        }
    }
}