<%@ Page Title="" Language="C#" MasterPageFile="~/User/User.Master" AutoEventWireup="true" CodeBehind="MyAutoEdit.aspx.cs" Inherits="RollingRides.WebApp.User.MyAutoEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <script type="text/javascript">
        var counter = 0;
        function AddFileUpload() {
            
            var html = '<div id="div_' + counter + '"><input id="file' + counter + '" name = "file' + counter + '" type="file" />'  +

                     '<input id="Button' + counter + '" type="button" ' +

                     ' value="Remove" onclick = "RemoveFileUpload(' + counter + ')" /></div>';
            
            $("#imgContainer").html($("#imgContainer").html() + html);
           
            counter++;

        }

        function RemoveFileUpload(divNumber) {
         
            $('#div_' + divNumber).remove();
           
        }
        $(document).ready(function () {
            $('#<%=cbxUserMyInfo.ClientID %>').click(function () {
                if ($(this).attr('checked') || $(this).attr('checked') == 'true') {
                    $('#ContactPanel').hide();
                }
                else {
                    $('#ContactPanel').show();
                }
            });
            $('#btnAddMoreImgs').click(function () {

                AddFileUpload();
                return false;
            });
        });
        
</script>
<div>
* means required
</div>
<asp:Label ID="lblError" runat="server" />
<div>
Id: <asp:Label ID="lblId" runat="server" />
</div>
<div>
* Make: <asp:TextBox ID="txtMake" runat="server" />
</div>
<div>
* Model: <asp:TextBox ID="txtModel" runat="server" />
</div>
<div>
* Year: <asp:DropDownList ID="ddlYear" runat="server" />
</div>
<div>
 * Price: <asp:TextBox ID="txtPrice" runat="server" />
</div>
<div>
  * Color: <asp:TextBox ID="txtColor" runat="server" />
</div>
<div>
  *  Financing Available? Yes: <asp:CheckBox ID="cbxFianacing" runat="server" /> 
</div>
<div>
    Minimum Down Payment: <asp:TextBox ID="txtMinDownPayment" runat="server" />
</div>
<div>
Check if the car is used? <asp:CheckBox ID="cbxUsed" Checked="true" runat="server" />
</div>
<div>
* Title: <asp:TextBox ID="txtTitle" runat="server" />
</div>
<div>
Description: <asp:TextBox TextMode="MultiLine" ID="txtDescription" runat="server" MaxLength="1000" Width="150" Height="300"  />
</div>
<div>
    Previous Carfax: <asp:Label ID="lblCarfax" runat="server" />
</div>
<div>
Upload Carfax: <asp:FileUpload ID="fuCarFax" runat="server"  />
</div>
<div>
    Vehicle Contact Information:
</div>
<div>
    Use My Account Contact Information: <asp:CheckBox ID="cbxUserMyInfo" runat="server" />
</div>
<div id="ContactPanel">
<div>
    Contact Name: <asp:TextBox ID="txtContactName" runat="server" />
</div>
<div>
    Phone Number: <asp:TextBox ID="txtPhoneNumber" runat="server" />
</div>
<div>
    Street Address 1: <asp:TextBox ID="txtStreet1" runat="server" />
</div>
<div>
    Street Address 2: <asp:TextBox ID="txtStreet2" runat="server" />
</div>
<div>
    City: <asp:TextBox ID="txtCity" runat="server" />
</div>
<div>
    State: <asp:DropDownList ID="ddlState" runat="server">
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
</div>
<div>
Youtube Video Link: <asp:TextBox ID="txtYoutube" runat="server" />
</div>
<div>
    Previous Video: <asp:Label ID="lblVideo" runat="server" /> 
</div>
<div>

  Car Video File: <asp:FileUpload ID="fuVideo" runat="server" />  (10mb max)
</div>
<div>
Main Image: <asp:Image runat="server" ID="imgMainImage" Visible="false" />
    <asp:FileUpload ID="fuMainImage" runat="server" /> (Will be shruken to 200px X 100px to be displayed correctly on the auto listing page) 
</div>
<div>
    Other Images:
</div>
<asp:Panel ID="pnlOldImages" runat="server" />


<div id="imgContainer">

</div>
<div>
<input type="button" id="btnAddMoreImgs" value="Add More Images" />
</div>
<div>
    <asp:Button ID="btnSave" runat="server" Text="Save Vehicle" 
        onclick="btnSave_Click"/>
</div>
</asp:Content>
