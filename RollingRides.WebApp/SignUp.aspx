<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SignUp.aspx.cs" Inherits="RollingRides.WebApp.SignUp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
<div>
    <asp:Label runat="server" ID="lblError" />
</div>

<div>
  Username:  <asp:TextBox ID="txtUsername" runat="server" />
</div>
<div>
   Password: <asp:TextBox ID="txtPassword" runat="server" />
    
</div>
<div>
   Confirm Password:  <asp:TextBox ID="txtPasswordConfirm" runat="server" />
</div>
<div>
Email: <asp:TextBox ID="txtEmail" runat="server" />
</div>
<div>
Phone Number: <asp:TextBox ID="txtPhone" runat="server" />    
</div>`
<div>
First Name: <asp:TextBox ID="txtFirstName" runat="server" />
</div>`
<div>
Last Name: <asp:TextBox ID="txtLastName" runat="server" />
</div>`
<div>
Company Name: <asp:TextBox ID="txtCompanyName" runat="server" />
</div>`
<div>
  Street Address:  <asp:TextBox ID="txtStreet1" runat="server" />
</div>
<div>
  Street Address 2:  <asp:TextBox ID="txtStreet2" runat="server" />
</div>
<div>
  City:  <asp:TextBox ID="txtCity" runat="server" />
</div>
<div>
State: 
<asp:DropDownList ID="ddlState" runat="server">
	<asp:ListItem Value="AL">Alabama</asp:ListItem>
	<asp:ListItem Value="AK">Alaska</asp:ListItem>
	<asp:ListItem Value="AZ">Arizona</asp:ListItem>
	<asp:ListItem Value="AR">Arkansas</asp:ListItem>
	<asp:ListItem Value="CA">California</asp:ListItem>
	<asp:ListItem Value="CO">Colorado</asp:ListItem>
	<asp:ListItem Value="CT">Connecticut</asp:ListItem>
	<asp:ListItem Value="DC">District of Columbia</asp:ListItem>
	<asp:ListItem Value="DE">Delaware</asp:ListItem>
	<asp:ListItem Value="FL">Florida</asp:ListItem>
	<asp:ListItem Value="GA">Georgia</asp:ListItem>
	<asp:ListItem Value="HI">Hawaii</asp:ListItem>
	<asp:ListItem Value="ID">Idaho</asp:ListItem>
	<asp:ListItem Value="IL">Illinois</asp:ListItem>
	<asp:ListItem Value="IN">Indiana</asp:ListItem>
	<asp:ListItem Value="IA">Iowa</asp:ListItem>
	<asp:ListItem Value="KS">Kansas</asp:ListItem>
	<asp:ListItem Value="KY">Kentucky</asp:ListItem>
	<asp:ListItem Value="LA">Louisiana</asp:ListItem>
	<asp:ListItem Value="ME">Maine</asp:ListItem>
	<asp:ListItem Value="MD">Maryland</asp:ListItem>
	<asp:ListItem Value="MA">Massachusetts</asp:ListItem>
	<asp:ListItem Value="MI">Michigan</asp:ListItem>
	<asp:ListItem Value="MN">Minnesota</asp:ListItem>
	<asp:ListItem Value="MS">Mississippi</asp:ListItem>
	<asp:ListItem Value="MO">Missouri</asp:ListItem>
	<asp:ListItem Value="MT">Montana</asp:ListItem>
	<asp:ListItem Value="NE">Nebraska</asp:ListItem>
	<asp:ListItem Value="NV">Nevada</asp:ListItem>
	<asp:ListItem Value="NH">New Hampshire</asp:ListItem>
	<asp:ListItem Value="NJ">New Jersey</asp:ListItem>
	<asp:ListItem Value="NM">New Mexico</asp:ListItem>
	<asp:ListItem Value="NY">New York</asp:ListItem>
	<asp:ListItem Value="NC">North Carolina</asp:ListItem>
	<asp:ListItem Value="ND">North Dakota</asp:ListItem>
	<asp:ListItem Value="OH">Ohio</asp:ListItem>
	<asp:ListItem Value="OK">Oklahoma</asp:ListItem>
	<asp:ListItem Value="OR">Oregon</asp:ListItem>
	<asp:ListItem Value="PA">Pennsylvania</asp:ListItem>
	<asp:ListItem Value="RI">Rhode Island</asp:ListItem>
	<asp:ListItem Value="SC">South Carolina</asp:ListItem>
	<asp:ListItem Value="SD">South Dakota</asp:ListItem>
	<asp:ListItem Value="TN">Tennessee</asp:ListItem>
	<asp:ListItem Value="TX">Texas</asp:ListItem>
	<asp:ListItem Value="UT">Utah</asp:ListItem>
	<asp:ListItem Value="VT">Vermont</asp:ListItem>
	<asp:ListItem Value="VA">Virginia</asp:ListItem>
	<asp:ListItem Value="WA">Washington</asp:ListItem>
	<asp:ListItem Value="WV">West Virginia</asp:ListItem>
	<asp:ListItem Value="WI">Wisconsin</asp:ListItem>
	<asp:ListItem Value="WY">Wyoming</asp:ListItem>
</asp:DropDownList>
</div>
<div>
Zip Code: <asp:TextBox ID="txtZipCode" runat="server" />
</div>
<div> 
    By using this site, you agree to the <a href="/TermsAndConditions.aspx">Terms and Conditions</a> of using this site.
</div>
<div>
    <asp:CheckBox ID="cbxAgree" runat="server" Text="I Agree" />
</div>
<div>
    <asp:Button runat="server" Text="Create Account" ID="btnSave" OnClick="CreateAccount" />
</div>
</asp:Content>
