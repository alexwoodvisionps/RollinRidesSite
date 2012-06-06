<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TermsAndConditions.aspx.cs" Inherits="RollingRides.WebApp.TermsAndConditions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<h2>Terms And Conditions</h2>
<div>
<asp:TextBox ID="txtTerms" runat="server" TextMode="MultiLine" Width="400" Height="400" ReadOnly="true" /> 
</div>
</asp:Content>
