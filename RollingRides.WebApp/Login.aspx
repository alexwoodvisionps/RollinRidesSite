<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="RollingRides.WebApp.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<script type="text/javascript">
    $(document).ready(function () { 
        var obj = $('#<%=loginFrm.ClientID %>_RememberMe');
        obj.parent().hide();
    });
</script>
<div>
<div>
<asp:Login ID="loginFrm" runat="server" RememberMeSet="false" FailureText="Username or Password is Incorrect" PasswordRecoveryUrl="~/ResetPassword.aspx" PasswordRecoveryText="Reset Password">
</asp:Login>
</div>
<div>
<h2>
    <asp:HyperLink ID="ToSignUp" NavigateUrl="~/SignUp.aspx" runat="server" Text="Sign Up" ></asp:HyperLink>
</h2>
</div>
</div>
</asp:Content>

