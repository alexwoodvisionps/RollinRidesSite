<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="MyVehicles.aspx.cs" Inherits="RollingRides.WebApp.User.MyVehicles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  EmptyDataText="No Users">
    <Columns>
        
        <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make"/>
        <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
        <asp:BoundField DataField="IsApproved" HeaderText="Has Been Approved?" SortExpression="IsApproved" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="imgMain" runat="server" />
                <asp:HiddenField ID="hfId" Value='<%# Eval("Id") %>' runat="server" />
                <asp:Button ID="btnEdit" OnClick="btnEdit_Click" Text="Edit" runat="server" CommandArgument='<%# Eval("Id") %>' />              
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" Text="Delete" runat="server" CommandArgument='<%# Eval("Id") %>' />              
                
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Button ID="btnAdd" runat="server" Text="Add New Vehicle" 
    onclick="btnAdd_Click" />
</asp:Content>
