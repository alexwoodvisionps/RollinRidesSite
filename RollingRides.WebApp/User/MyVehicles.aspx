<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="MyVehicles.aspx.cs" Inherits="RollingRides.WebApp.User.MyVehicles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        function ConfirmIt() {
            return confirm("Are you sure that you want to delete this vehicle?");
        }
    </script>
    
    <asp:GridView ID="gvAutos" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  EmptyDataText="No Vehicles">
    <Columns>
        
        <asp:BoundField DataField="Make" HeaderText="Make" SortExpression="Make"/>
        <asp:BoundField DataField="Model" HeaderText="Model" SortExpression="Model" />
        <asp:BoundField DataField="Year" HeaderText="Year" SortExpression="Year" />
        <asp:TemplateField>
            <HeaderTemplate>
                Has Been Approved?
            </HeaderTemplate>
            <ItemTemplate><%#Eval("IsApproved").ToString() == "1" ? "Yes" : "No" %></ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField>
            
            <ItemTemplate>
                
                <asp:Image ID="imgMain" runat="server" />
                <asp:HiddenField ID="hfId" Value='<%# Eval("Id") %>' runat="server" />
                <asp:Button ID="btnEdit" OnClick="btnEdit_Click" Text="Edit" runat="server" CommandArgument='<%# Eval("Id") %>' />              
                <asp:Button ID="btnDelete" OnClientClick="return ConfirmIt()" OnClick="btnDelete_Click" Text="Delete" runat="server" CommandArgument='<%# Eval("Id") %>' />              
                
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Button ID="btnAdd" runat="server" Text="Add New Vehicle" 
    onclick="btnAdd_Click" />
</asp:Content>
