<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="RollingRides.WebApp.User.ChangePassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <asp:Label ID="lblError" runat="server" /> 
</div>
<div>
    Old Password: <asp:TextBox ID="txtOldPassword" runat="server" TextMode="Password" />
 </div>
<div>
New Password:
<asp:TextBox ID="txtPassword" runat="server" TextMode="Password" />
</div>
<div>
Confirm New Password:
<asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password" />
</div>
<div>
    <asp:Button ID="btnUpdate" runat="server" Text="Update Password" 
        onclick="btnUpdate_Click" />
</div>
</asp:Content>
