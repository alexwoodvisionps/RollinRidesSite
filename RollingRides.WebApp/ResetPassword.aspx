<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="RollingRides.WebApp.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
    <asp:Label ID="lblMessage" runat="server" />
</div>
<div>
Enter Your Email: <asp:TextBox ID="txtEmail" runat="server" />
</div>
<div>
<asp:Button ID="btnSubmit" runat="server" Text="Reset Password" 
        onclick="btnSubmit_Click"/>
</div>
</asp:Content>
