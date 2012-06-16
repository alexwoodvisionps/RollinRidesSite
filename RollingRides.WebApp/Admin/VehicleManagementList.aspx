<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="VehicleManagementList.aspx.cs" Inherits="RollingRides.WebApp.Admin.VehicleManagementList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript" >
    function ConfirmIt()
    {
        return confirm("Are you sure you want to delete this vehicle?");
    }
</script>

<asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  EmptyDataText="No Vehicles">
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="Automobile System Id" SortExpression="Id" />
        <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make"/>
        <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
        <asp:BoundField DataField="Color" HeaderText="Color" SortExpression="Color" />
        <asp:BoundField DataField="IsApproved" HeaderText="Is Approved?" SortExpression="IsApproved" />
        <asp:BoundField DataField="IsHighlight" HeaderText="Is Highlight?" SortExpression="IsHighlight" />
        <asp:BoundField DataField="ContactName" HeaderText="Contact Name"  />
        <asp:BoundField DataField="PhoneNumber" HeaderText="Seller's Phone" />
        <asp:BoundField DataField="UserId" HeaderText="Seller's Id" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Image ID="imgMain" runat="server" />
                <asp:Button ID="btnApprove" runat="server" CommandArgument='<%# Eval("Id")  %>' Text="Approve" OnClick="btnApprove_Click" />
                <asp:Button ID="btnUnapprove" runat="server" CommandArgument='<%# Eval("Id")  %>' Text="Unapprove" OnClick="btnUnapprove_Click"/>
                <asp:Button ID="btnMakeHighlight" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Make Highlight" OnClick="MakeHighlight" />
                <asp:Button ID="btnRemoveHighlight" runat="server" CommandArgument='<%# Eval("Id") %>' Text="Remove Highlight" OnClick="RemoveHighlight" />
              <%--  
                <asp:Button ID="btnDetail" OnClick="btnDetails_Click" Text="Details" runat="server" CommandArgument='<%# DataBinder.Eval(Container, "Id") %>' />
               --%>
                <asp:Button ID="btnDelete" OnClick="btnDelete_Click" OnClientClick="return ConfirmIt();" Text="Delete" CommandArgument='<%# Eval("Id")  %>' runat="server" />

            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>


</asp:Content>
