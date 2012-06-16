<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="RollingRides.WebApp.Default" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
<h2>Welcome To Rollin Rides' Official Website</h2>
</div>
<div>
    You can contact sellers, read car descriptions, see carfaxes, see real movies in a variety 
    of formats of the used car or vehicle you wish to buy, take a virtual car drive, see images, browse, and see the car's origin all for free.
    If you wish to sell a vehicle yourself or a corporation <a href="/SignUp.aspx">click here </a> to sign up for a free or coporate account.
    If you wish to avertise with us <a href="/AdvertiseWithUs.aspx">click here</a> to become an advertiser. 
    Thank you for vising our site and we look forward to putting you in your next vehicle.
    <br />
    Sincerely,
    <br />
    The Rollin Rides Team  
</div>
<div>
Lot Tour:
</div>
<div>
    <asp:Panel ID="pnlMovie" runat="server" />
</div>
</asp:Content>
