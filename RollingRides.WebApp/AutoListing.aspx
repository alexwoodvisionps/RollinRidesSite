<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="AutoListing.aspx.cs" Inherits="RollingRides.WebApp.AutoListing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  EmptyDataText="No Results">
    <Columns>
        
        <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make"/>
        <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
        
        <asp:BoundField DataField="Seller.CompanyName" HeaderText="Company Name" />
        <asp:BoundField DataField="Price" HeaderText="Price" SortExpression="Price" DataFormatString="0.00" />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Contact Phone" />
        
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="imgMain" runat="server" />
                <asp:HiddenField ID="hfId" Value='<%# Eval("Id") %>' runat="server" />
                <asp:Button ID="btnDetails" OnClick="btnDetails_Click" Text="Details" runat="server" CommandArgument='<%# Eval("Id") %>' />              
                
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

</asp:Content>
