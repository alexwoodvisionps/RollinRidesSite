<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Contact.aspx.cs" Inherits="RollingRides.WebApp.Contact" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <h2>Contact Information:</h2>
</div>
<div>
    Phone Number: <asp:Label ID="lblPhoneNumber" runat="server" />
</div>
<div>
    Fax Number: <asp:Label ID="lblFax" runat="server" />
</div>
<div>
    Address: <asp:Label ID="lblAddress" runat="server" />
</div>
<div>
    Site Administrator Email: <asp:Label ID="lblEmail" runat="server" />
</div>
</asp:Content>
