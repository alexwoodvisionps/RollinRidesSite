<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="SiteManager.aspx.cs" Inherits="RollingRides.WebApp.Admin.SiteManager" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
    function ConfirmIt() {
        return confirm("Are you sure you want to delete the advertisement?");
    }
</script>
<div>
    <asp:Label ID="lblError" runat="server" Text="" />
</div>
<div>
    Settings Menu:
</div>
<div>
    Company Logo: <asp:Image ID="imgCurrentLogo" runat="server" /> <asp:FileUpload ID="fuCompanyLogo" runat="server" />
</div>
<div>
    Contact Phone: <asp:TextBox ID="txtPhone" runat="server" />
</div>
<div>
    Site Master Email Address: <asp:TextBox ID="txtSiteMasterEmail" runat="server" />
</div>
<div>
    Contact Fax: <asp:TextBox ID="txtFax" runat="server" />
</div>
<div>
    Coupon Of The Month: <asp:FileUpload ID="fuCoupon" runat="server" /> <asp:Button Text="Download Last Coupon Uploaded" ID="btnDownloadCoupon" runat="server" Visible="false" /> 
</div>
<div>
    About Us Description: <asp:TextBox MaxLength="2000" runat="server" TextMode="MultiLine" ID="txtDescription" Width="300" Height="500" />
</div>
<div>
    Terms And Conditions: <asp:TextBox ID="txtTerms" runat="server" TextMode="MultiLine" MaxLength="7000" Width="300" Height="300" />
</div>
<div>
    Home Page Movie: <asp:Button ID="btnDownload" runat="server" 
        Text="Download Movie" Visible="false" onclick="btnDownload_Click" /> <asp:FileUpload ID="fuMovie" runat="server" />
</div>
<div>
    Rollin Rides Company Address: <asp:TextBox MaxLength="500" ID="txtAddress" runat="server" TextMode="MultiLine" Width="200" Height="200" />
</div>
<div>
    <asp:Button ID="btnSaveSettings" runat="server" Text="Save Settings" 
        onclick="btnSaveSettings_Click" />
</div>
<div>
<div>
    Advertisements For Browsers:
</div>
<div>   
    <asp:GridView ID="gvAdvertisers" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:BoundField DataField="CompanyName" HeaderText="Company Name" />
            <asp:BoundField DataField="Link" HeaderText="Advertisement Link" />
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="btnDeleteAdvert" runat="server" OnClientClick="ConfirmIt()" Text="Delete" OnClick="DeleteAdvertisement" CommandArgument='<%# Eval("Id") %>' />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</div>

<div>
    Add New Advertiser:
</div>
<div>
    Company Name: <asp:TextBox ID="txtCompany" runat="server" />
</div>
<div>
    Company Url: <asp:TextBox ID="txtCompanyUrl" runat="server" />
</div>
<div>
    Location: <asp:DropDownList ID="ddlLoc" runat="server">
                <asp:ListItem Text="Top" Value="1" />
                <asp:ListItem Text="Side Bar" Value="2" />
                <asp:ListItem Text="Footer" Value="3" />
    </asp:DropDownList>
</div>
<div>
  Upload Advertiser Graphic:  <asp:FileUpload ID="fuAdvertiser" runat="server" />
</div>
<div>
    <asp:Button ID="btnAddAvertiser" runat="server" Text="Add New Advertiser" 
        onclick="btnAddAvertiser_Click" />
</div>
</div>
</asp:Content>
