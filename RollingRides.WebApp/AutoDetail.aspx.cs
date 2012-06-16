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
using RollingRides.WebApp.Controls;

namespace RollingRides.WebApp
{
    public partial class AutoDetail : System.Web.UI.Page
    {
        private readonly IAutomobileManager _autoManager;

        public AutoDetail()
        {
            _autoManager = new AutomobileManager();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if(!IsPostBack)
            {
                var auto = _autoManager.GetById(int.Parse(Request.QueryString["id"]));
                lblYearMakeModel.Text = auto.Year + " " + auto.Make + " " + auto.Model + " ";
                lblPhone.Text = auto.PhoneNumber?? auto.Seller.PhoneNumber;
                lblEmail.Text = auto.Seller.Email;
                lblContactName.Text = auto.ContactName ?? auto.Seller.LastName + ", " + auto.Seller.FirstName;
                litFinancing.Text = auto.HasFinancing == 1 ? "Yes" : "No";
                lblMinimumDownPayment.Text = auto.MinimumDownPayment.HasValue ? StringHelper.FormatCurrency(auto.MinimumDownPayment.Value) : "N/A";
                lblPrice.Text = StringHelper.FormatCurrency(auto.Price);
                lblColor.Text = auto.Color;
                ViewState["auto"] = auto;
                hfId.Value = auto.Id.ToString();
                if (string.IsNullOrEmpty(auto.CarfaxReportPath))
                    btnDownloadCarfax.Visible = false;
                if(!string.IsNullOrEmpty(auto.CarfaxReportPath))
                    litCarFax.Text = "<a href='" + auto.CarfaxReportPath + "'>View Carfax</a>";
            }
            BindRepeater();
        }
        private void BindRepeater()
        {
            var auto = ViewState["auto"] as Automobile;
            if (auto == null || auto.Images == null)
                return;
            rptImages.DataSource = auto.Images.Where(x => x.MediaType == MediaType.Image).ToList();
            rptImages.DataBind();
            var youtubemovies = auto.Images.Where(x => x.MediaType == MediaType.Youtube).ToList();
            var serverMovies = auto.Images.Where(x => x.MediaType == MediaType.Server);
            foreach (var serverMovie in serverMovies)
            {
                var movieControl = new MovieControl
                                       {
                                           Url = serverMovie.Url,
                                           Width = 480,
                                           Height = 360 
                                       };
                pnlMovies.Controls.Add(movieControl);
                /**/
                //var btn = new Button();
                //btn.CommandArgument = serverMovie.Id.ToString();
                //btn.Click += new EventHandler(btn_Click);
                //pnlMovies.Controls.Add(btn);
            }
            foreach (var youtubeMov in youtubemovies)
            {
                var youtubeMovControl = new YoutubeMovieControl {Url = youtubeMov.Url, Width = 480, Height = 360 };
                pnlYouTube.Controls.Add(youtubeMovControl);
                //var btn = new Button();
                //btn.CommandArgument = youtubeMov.Id.ToString();
                //btn.Click += new EventHandler(btn_Click);
                //pnlYouTube.Controls.Add(btn);
            }
        }

        protected void btnDownloadCarfax_Click(object sender, EventArgs e)
        {
            var id = int.Parse(hfId.Value);
            var auto = _autoManager.GetById(id);
            Response.Clear();
            Response.ContentType = "application/octet-stream";
            Response.AddHeader("Content-Disposition","attachment;filename=\"" + StringHelper.MakeFileSafe(auto.CarfaxReportPath)  + "\"");
            Response.TransmitFile(auto.CarfaxReportPath);
            Response.End();
        }
    }
}