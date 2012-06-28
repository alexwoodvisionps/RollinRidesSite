<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AutoDetail.aspx.cs" Inherits="RollingRides.WebApp.AutoDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

<style type="text/css">

    .image_carousel {
	    padding: 15px 0 15px 40px;
	    position: relative;
	}
	.image_carousel img {
	    border: 1px solid #ccc;
	    background-color: white;
	    padding: 9px;
	    margin: 7px;
	    display: block;
	    float: left;
	}
	a.prev, a.next {
	    background: url(../images/miscellaneous_sprite.png) no-repeat transparent;
	    width: 45px;
	    height: 50px;
	    display: block;
	    position: absolute;
	    top: 85px;
	}
	a.prev {            left: -22px;
	                    background-position: 0 0; }
	a.prev:hover {      background-position: 0 -50px; }
	a.prev.disabled {   background-position: 0 -100px !important;  }
	a.next {            right: -22px;
	                    background-position: -50px 0; }
	a.next:hover {      background-position: -50px -50px; }
	a.next.disabled {   background-position: -50px -100px !important;  }
	a.prev.disabled, a.next.disabled {
	    cursor: default;
	}
	 
	a.prev span, a.next span {
	    display: none;
	}
	.pagination {
	    text-align: center;
	}
	.pagination a {
	    background: url(../images/miscellaneous_sprite.png) 0 -300px no-repeat transparent;
	    width: 15px;
	    height: 15px;
      margin: 0 5px 0 0;
	    display: inline-block;
	}
	.pagination a.selected {
	    background-position: -25px -300px;
	    cursor: default;
	}
	.pagination a span {
	    display: none;
	}
	.clearfix {
	    float: none;
	    clear: both;
	}
</style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script type="text/javascript" src="js/jquery.carouFredSel-5.6.1.js" ></script>
<script type="text/javascript">
    function shareLinkFacebook() {
        FB.login(function (response) {
            if (response.authResponse) {
                console.log('Welcome!  Fetching your information.... ');
                var body = window.location.href;
                FB.api('/me/feed', 'post', { message: body }, function (response) {
                    if (!response || response.error) {
                        alert('Error occured sharing the link');
                    } else {
                        alert("Linked Shared Successful!");
                    }
                });
            }
            else
                alert("Cannot Share This Link You Did Not Authorize Facebook To Allow It");
        });
        return true;
    }
    $(document).ready(function () {
        $('#carousel').carouFredSel();//{
//            circular: false,
//            infinite: false,
//            auto: false,
//            scroll: {
//                items: "page"
//            },
//            prev: {
//                button: "#prevBtn",
//                key: "left"
//            },
//            next: {
//                button: "#nextBtn",
//                key: "right"
//            },
//            pagination: "#pagination"
//        });
    });
</script>
<div>
    <a onclick="shareLinkFacebook();"><img alt="Share On Facebook" src="/img/facebookShare.jpg" /></a>
    
</div>
<div>
<asp:HiddenField ID="hfId" runat="server" />
Car Information:
<asp:Label ID="lblYearMakeModel" runat="server" />
</div>
<div>

    Color: <asp:Label ID="lblColor" runat="server" />
</div>
<div>
    <asp:Literal ID="litCarFax" runat="server" />
</div>
<div>
    <asp:Button ID="btnDownloadCarfax" runat="server" Text="Download Carfax" 
        onclick="btnDownloadCarfax_Click"/>
</div>
<div>
   * Has Financing Option? <asp:Literal ID="litFinancing" runat="server" />
</div>
<div>
   Price: <asp:Label ID="lblPrice" runat="server" />
</div>

<div>
    Minimum Down Payment: <asp:Label ID="lblMinimumDownPayment" runat="server" />
</div>
<div>
Contact Information:
</div>

<div>
Contact 
<asp:Label ID="lblContactName" runat="server" />
</div>
<div>
At 
    <asp:Label ID="lblPhone" runat="server" />
    <asp:Label ID="lblEmail" runat="server" Visible="false" />
</div>
<div>
    Movies:
</div>
<div>
    <asp:Panel ID="pnlYouTube" runat="server" />
</div>
<div>
    <asp:Panel ID="pnlMovies" runat="server" />
</div>
<div>
Pictures:
</div>
<div id="carousel">
    <asp:Repeater ID="rptImages" runat="server" >
        <ItemTemplate>
            <asp:Image Width="200" Height="100" ID="img" runat="server" ImageUrl='<%# Eval("Url") %>' />
        </ItemTemplate>
        
    </asp:Repeater>
     <br />
     <br />
	        <a class="prev" id="prevBtn" href="#"><span>prev</span></a>
	        <a class="next" id="nextBtn" href="#"><span>next</span></a>
	        <div class="pagination" id="pagination"></div>
    </div>
<%--<div>
    Tweet about this automobile!
</div>
<div>
    <asp:Label ID="lblTwitterResponse" runat="server" />
</div>
<div>
    Twitter Username: <asp:TextBox ID="txtTwitterUsername" runat="server" />
</div>
<div>
    Twitter Password: <asp:TextBox ID="txtTwitterPassword" runat="server" />
</div>
<div>
Comment: <asp:TextBox ID="txtComment" runat="server" Width="400" Height="300" />
</div>
<div>
    <asp:Button ID="btnTweet" runat="server" Text="Tweet It!"  
         Height="58px" Width="67px" CssClass="twitter" onclick="btnTweet_Click" />
</div>--%>
</asp:Content>
