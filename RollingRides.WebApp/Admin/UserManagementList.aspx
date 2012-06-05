<%@ Page Title="" ValidateRequest="false" Language="C#" MasterPageFile="~/Admin/Admin.Master" EnableEventValidation="false" AutoEventWireup="true" CodeBehind="UserManagementList.aspx.cs" Inherits="RollingRides.WebApp.Admin.UserManagementList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    function ConfirmIt() {
        return confirm("Are you sure you want to delete this user?");
    }
</script>
<asp:GridView ID="gvUsers" runat="server" AutoGenerateColumns="false" AllowPaging="true" AllowSorting="true"  EmptyDataText="No Users">
    <Columns>
        <asp:BoundField DataField="Username" HeaderText="Username" SortExpression="Username"/>
        <asp:BoundField DataField="Email" HeaderText="Email Address" SortExpression="Email" />
        <asp:BoundField DataField="LastName" HeaderText="Last Name" SortExpression="LastName" />
        <asp:BoundField DataField="FirstName" HeaderText="First Name" SortExpression="FirstName" />
        <asp:TemplateField>
            <ItemTemplate>
                <asp:Button CausesValidation="false" ID="btnEdit" runat="server" Text="Edit" CommandArgument='<%# Eval("Id") %>' OnClick="btnEdit_Click" />
                <asp:Button CausesValidation="false" OnClientClick="ConfirmIt()" ID="btnDelete" runat="server" Text="Delete" CommandArgument='<%# Eval("Id") %>' OnClick="btnDelete_Click" />
             </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>

<div>
    <div>Username: <asp:TextBox ID="txtUsername" runat="server" ValidationGroup="AddUser" />
         <asp:RequiredFieldValidator ID="usernameValidator" ControlToValidate="txtUsername" ErrorMessage="Username is required!" Display="Dynamic" runat="server" />
   
    </div>
    <div>
     Password: <asp:TextBox TextMode="Password" ID="txtPassword" runat="server" /> 
    <asp:CheckBox ID="cbxAutoPass" runat="server" Text="Autogenerate password?" />  
    </div>
</div>
<div>
    <div>
    Email Address: <asp:TextBox ID="txtEmail" runat="server" ValidationGroup="AddUser" />
    <asp:RequiredFieldValidator ID="emailValidator" ControlToValidate="txtEmail" ErrorMessage="Email is required!" Display="Dynamic" runat="server" />
    </div>
    <asp:DropDownList ID="ddlUserType" runat="server" />
    <div>
    <asp:Button ID="btnAddUser" runat="server" Text="Add New User" 
            ValidationGroup="AddUser" onclick="btnAddUser_Click" />
    </div>
    <div>
        <asp:Label ID="lblError" runat="server" />
    </div>
</div>
</asp:Content>
