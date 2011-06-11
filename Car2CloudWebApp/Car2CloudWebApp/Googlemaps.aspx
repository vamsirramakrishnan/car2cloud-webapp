<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Googlemaps.aspx.cs" Inherits="ictlab.Googlemaps" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadContent" runat="server">
<title>Google maps</title>
    <meta name="viewport" content="initial-scale=1.0, user-scalable=no" />
    <script type="text/javascript" src="http://maps.google.com/maps/api/js?sensor=false"></script>
    <script type="text/javascript">
    //<![CDATA[
        function loadScript() {
            var script = document.createElement("script");
            script.type = "text/javascript";
            script.src = "http://maps.google.com/maps/api/js?sensor=false&callback=initialize";
            document.body.appendChild(script);
        }

        window.onload = loadScript;
    //]]>
    </script>

<!--Literal Control for runtime javascript-->
<asp:Literal ID="js" runat="server"></asp:Literal>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server">
<a href="Uitloggen.aspx">Uitloggen</a>
</asp:Content>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div id="map_canvas"></div>
</asp:Content>
