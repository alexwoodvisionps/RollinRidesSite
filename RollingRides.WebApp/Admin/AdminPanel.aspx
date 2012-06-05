<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AdminPanel.aspx.cs" Inherits="RollingRides.WebApp.Admin.AdminPanel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
<h2>
    <asp:HyperLink runat="server" NavigateUrl="~/Admin/UserManagementList.aspx" Text="User Management" />
</h2>
</div>
<div>
<h2>
    <asp:HyperLink runat="server" NavigateUrl="~/Admin/VehicleManagementList.aspx" Text="Vehicle Management" />
</h2>
</div>
<div>
    <h2><asp:HyperLink runat="server" NavigateUrl="~/User/MyAccount.aspx" Text="My Account" /></h2>
</div>
</asp:Content>
