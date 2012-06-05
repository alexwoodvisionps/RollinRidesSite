<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AutoSearch.aspx.cs" Inherits="RollingRides.WebApp.AutoSearch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<asp:Label ID="lblError" runat="server" />
<div>
Choose A Make: <asp:DropDownList ID="ddlMake" runat="server" AutoPostBack="true" />
</div>
<div>
Choose A Model: <asp:DropDownList ID="ddlModel" runat="server" />
</div>
<div>
Choose A Price Range: Minimum: <asp:TextBox ID="txtMin" runat="server" /> Maximum: <asp:TextBox ID="txtMax" runat="server" />
</div>
<div>
    <asp:Button ID="btnSearch" runat="server" Text="Search" 
        onclick="btnSearch_Click" />
</div>

<div>
    

<asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  EmptyDataText="No Results">
    <Columns>
        
        <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make"/>
        <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
        
        <asp:BoundField DataField="Seller.CompanyName" HeaderText="Company Name" SortExpression="Seller.CompanyName" />
        <asp:BoundField DataField="Seller.LastName" HeaderText="Seller Last Name" SortExpression="Seller.LastName" />
        <asp:BoundField DataField="Seller.FirstName" HeaderText="Seller First Name" SortExpression="Seller.FirstName" />
        <asp:BoundField DataField="Seller.PhoneNumber" HeaderText="Seller Phone" />
        <asp:BoundField DataField="Seller.Email" HeaderText="Seller Email" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="imgMain" runat="server" />
                <asp:HiddenField ID="hfId" Value='<%# Eval("Id") %>' runat="server" />
                <asp:Button ID="btnDetails" OnClick="btnDetails_Click" Text="Details" runat="server" CommandArgument='<%# Eval("Id") %>' />              
                
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</div>
</asp:Content>
