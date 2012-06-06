using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using RollingRides.WebApp.Components.BusinessLogic;
using RollingRides.WebApp.Components.BusinessLogic.Interfaces;
using RollingRides.WebApp.Components.Datalayer.Models;
using Image = System.Web.UI.WebControls.Image;
using RollingRides.WebApp.Components.BusinessLogic.Common;
namespace RollingRides.WebApp.User
{
    public partial class MyAutoEdit : System.Web.UI.Page
    {
        private readonly IAutomobileManager _autoManager;
        public MyAutoEdit()
        {
            _autoManager = new AutomobileManager();
        }

        protected void Page_Load(object sender, EventArgs e) 
        {
            if(!IsPostBack)
            {
                for (var i = DateTime.Now.Year + 1; i > 1919; i--)
                {
                    ddlYear.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                if(!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    var auto = _autoManager.GetById(int.Parse(Request.QueryString["id"]));
                    //BindOldImages(auto.Images);
                    txtZipCode.Text = auto.ZipCode;
                    txtTitle.Text = auto.Title;
                    txtStreet1.Text = auto.Street1;
                    txtStreet2.Text = auto.Street2;
                    txtMake.Text = auto.Make;
                    txtModel.Text = auto.Model;
                   
                    ddlYear.SelectedValue = auto.Year.ToString();
                    txtPhoneNumber.Text = auto.PhoneNumber?? auto.Seller.PhoneNumber;
                    txtDescription.Text = auto.Description;
                    txtContactName.Text = auto.ContactName ?? auto.Seller.FirstName + " " + auto.Seller.LastName;
                    lblId.Text = auto.Id.ToString();
                    txtCity.Text = auto.City;
                    txtMinDownPayment.Text = auto.MinimumDownPayment.HasValue
                                                 ? StringHelper.FormatCurrency(auto.MinimumDownPayment.Value)
                                                 : "N/A";
                    ddlState.SelectedValue = auto.State;
                    txtPrice.Text = auto.Price.ToString();
                    txtMinDownPayment.Text = auto.MinimumDownPayment.ToString();
                    cbxFianacing.Checked = auto.HasFinancing == 1;
                    cbxUsed.Checked = auto.IsUsed == 1;
                    cbxUserMyInfo.Visible = false;
                }
            }
            int id;
            if(!string.IsNullOrEmpty(lblId.Text) && int.TryParse(lblId.Text, out id))
            {
                var auto = _autoManager.GetById(id);
                BindOldImages(auto.Images);
            }
            else
            {
                lblId.Text = "N/A";
            }
        }
        private void BindOldImages(IEnumerable<RollingRides.WebApp.Components.Datalayer.Models.Image> images)
        {
            foreach (var img in images)
            {
                if(img.IsMainImage == 1)
                {
                    imgMainImage.ImageUrl = ConfigurationManager.AppSettings["imagesFolder"] +  img.Url;
                    imgMainImage.Visible = true;
                    //fuMainImage.Visible = false;
                }
                switch (img.Type)
                {
                    case (int)MediaType.Image:
                        {
                            var imgControl = new Image
                                                 {
                                                     ImageUrl = ConfigurationManager.AppSettings["imagesFolder"] + img.Url,
                                                     ID = "img_" + img.Id
                                                 };
                            var btnDelImg = new Button {CommandArgument = img.Id.ToString()};
                            btnDelImg.Click += new EventHandler(btnDelImg_Click);
                            btnDelImg.Text = "Delete Image";
                            pnlOldImages.Controls.Add(imgControl);
                        }
                        break;
                    case (int)MediaType.Youtube:
                        txtYoutube.Text = img.Url;
                        break;
                    case (int)MediaType.Server:
                        lblVideo.Text = img.Url;
                        break;
                }
            }
        }

        protected void btnDelImg_Click(object sender, EventArgs e)
        {
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
            _autoManager.DeleteImage(int.Parse(((Button) sender).CommandArgument),int.Parse(lblId.Text), user.Id);
            var auto = _autoManager.GetById(int.Parse(lblId.Text));
            BindOldImages(auto.Images);
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            var id = 0;
            var auto = new Automobile();
            var autos =
                    _autoManager.GetByUserId(((RollingRides.WebApp.Components.Datalayer.Models.User)Session["User"]).Id);
            if(int.TryParse(lblId.Text, out id))
            {
                auto = _autoManager.GetById(id);
                
                var auto2 = autos.SingleOrDefault(x => x.Id == auto.Id);
                if(auto2 == null)
                {
                    lblError.Text = "The site is unable to process your request!";
                    return;
                }
               
                
            }
            
            auto.IsUsed = cbxUsed.Checked ? 1 : 0;
            auto.Make = txtMake.Text;
            auto.Model = txtModel.Text;
            auto.PhoneNumber = txtPhoneNumber.Text;
            decimal d;

            auto.MinimumDownPayment = decimal.TryParse(txtMinDownPayment.Text, out d) ? d : (decimal?) null;
            var user = (RollingRides.WebApp.Components.Datalayer.Models.User) Session["User"];
            if(autos.Count() > 4 && user.UserType == UserType.User)
            {
                lblError.Text = "You may not add anymore vehicles because you have a restriction as a basic user. Please <a href='/Contact.aspx'> Contact Us </a> to request a Corporate Account with unlimited vehicle advertisements!";
                return;
            }
            auto.UserId = user.Id;
            auto.HasFinancing = cbxFianacing.Checked ? 1 : 0;
            auto.IsHighlight = 0;
            auto.Year = int.Parse(ddlYear.SelectedValue);
            if(cbxUserMyInfo.Checked)
            {
                auto.Street1 = user.Street1;
                auto.Street2 = user.Street2;
                auto.City = user.City;
                auto.State = user.State;
                auto.ZipCode = user.ZipCode;
                auto.PhoneNumber = user.PhoneNumber;
                auto.ContactName = user.FirstName + " " + user.LastName;
            }
            else
            {
                auto.Street1 = txtStreet1.Text;
                auto.Street2 = txtStreet2.Text;
                auto.City = txtCity.Text;
                auto.State = ddlState.SelectedValue;
                auto.ZipCode = txtZipCode.Text;
                auto.PhoneNumber = txtPhoneNumber.Text;
                auto.ContactName = txtContactName.Text;
            }
            auto.Description = txtDescription.Text;
            auto.Title = txtTitle.Text;
            var youtubeVid = txtYoutube.Text;
            var imgYoutube = new RollingRides.WebApp.Components.Datalayer.Models.Image
                                 {
                                     Url = youtubeVid,
                                     Type = (int) MediaType.Youtube
                                 };
            try
            {
                if(auto.Images == null)
                    auto.Images = new List<Components.Datalayer.Models.Image>();
                if(!string.IsNullOrEmpty(imgYoutube.Url))
                    auto.Images.Add(imgYoutube);
                foreach (var file in
                    Request.Files.AllKeys.Select(fileStr => Request.Files[fileStr]).Where(file => file.ContentLength <= 5000000))
                {
                    
                    
                    if (string.IsNullOrEmpty(file.FileName))
                        continue;
                    if(StringHelper.IsValidCarFax(file.FileName))
                    {
                        var theG = Guid.NewGuid();
                        auto.CarfaxReportPath = ConfigurationManager.AppSettings["CarfaxPathUrl"] + theG.ToString() + file.FileName;
                        fuCarFax.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["CarfaxPathUrl"]) + theG.ToString() + file.FileName);
                        continue;
                        
                    }
                    var theGuid = Guid.NewGuid();
                    
                    var fileType = StringHelper.GetMediaType(file.FileName);
                    var img = new RollingRides.WebApp.Components.Datalayer.Models.Image
                                  {
                                      Type = (int) fileType,
                                      IsMainImage = file.FileName == fuMainImage.FileName ? 1 : 0,
                                      Url = ConfigurationManager.AppSettings["imagesFolder"] + theGuid + StringHelper.MakeFileSafe(file.FileName)
                                  };
                    if(fileType == MediaType.Server)
                    {
                        var serverVid = auto.Images.Where(x => x.Type == (int) MediaType.Server).ToList();
                        foreach (var image in serverVid)
                        {
                            _autoManager.DeleteImage(image.Id, auto.Id, user.Id);
                        }
                    }
                    auto.Images.Add(img);

                    file.SaveAs(Server.MapPath(ConfigurationManager.AppSettings["imagesFolder"]) + theGuid + StringHelper.MakeFileSafe(file.FileName));
                }
            }
            catch(Exception ex)
            {
                lblError.Text = ex.Message;
                return;
            }
            if (auto.CarfaxReportPath == null)
                auto.CarfaxReportPath = "";
            try
            {
                var temp = _autoManager.AddUpdate(auto, user.UserType);
                if (temp == null)
                    lblError.Text = "Failed to Save Vehicle Please Try Again.";
            }
            catch(Exception ex)
            {
                lblError.Text = "<strong>Missing Required Fields Failed To Save Vehicle</strong>";
            }
            //foreach (HttpPostedFile file in Request.Files)
            //{
            //    file.SaveAs(ConfigurationManager.AppSettings["imagesFolder"] + file.FileName);
            //}

        }
    }
}