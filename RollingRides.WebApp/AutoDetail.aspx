<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutoDetail.aspx.cs" Inherits="RollingRides.WebApp.AutoDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
Car Information:
<asp:Label ID="lblYearMakeModel" runat="server" />
</div>
<div>
    Has Financing Option? <asp:Literal ID="litFinancing" runat="server" />
</div>
<div>
   Price: <asp:Label ID="lblPrice" runat="server" />
</div>
<div>
    Minimum Down Payment: <asp:Label ID="lblMinimumDownPayment" runat="server" />
</div>
<div>
Contact Information:
</div>

<div>
Contact 
<asp:Label ID="lblContactName" runat="server" />
</div>
<div>
At 
    <asp:Label ID="lblPhone" runat="server" />
    <asp:Label ID="lblEmail" runat="server" Visible="false" />
</div>
<div>
    Movies:
</div>
<div>
    <asp:Panel ID="pnlYouTube" runat="server" />
</div>
<div>
    <asp:Panel ID="pnlMovies" runat="server" />
</div>
<div>
Pictures:
</div>
<div>
    <asp:Repeater ID="rptImages" runat="server" >
        <ItemTemplate>
            <asp:Image ID="img" runat="server" ImageUrl='<%# Eval("Url") %>' />
        </ItemTemplate>
    </asp:Repeater>
</div>
</asp:Content>
