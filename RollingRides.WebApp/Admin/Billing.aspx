<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="Billing.aspx.cs" Inherits="RollingRides.WebApp.Admin.Billing" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
Corporate Users That Should Be Billed:
</div>
<asp:GridView ID="gvUsers" runat="server" AllowSorting="true" AllowPaging="true" AutoGenerateColumns="false" >
    <Columns>
        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName"/>
        <asp:BoundField DataField="CompanyName" HeaderText="Company Name" SortExpression="CompanyName" />
        <asp:BoundField DataField="Expires" HeaderText="Bill Due On" SortExpression="Expires" />
        <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username" />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number" />
        <asp:BoundField DataField="Email" HeaderText="Email" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button ID="btnEmail" runat="server" Text="Remind Customer" OnClick="EmailCustomer" CommandArgument='<%# Eval("Id") %>' />
                <asp:Button ID="btnMarkPaid" runat="server" Text="Mark Paid For The Month" OnClick="MarkPaid" CommandArgument='<%# Eval("Id") %>' />
            </ItemTemplate>
        </asp:TemplateField>
     </Columns>
</asp:GridView>
</asp:Content>
