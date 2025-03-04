﻿<%@ Page Title="Overzicht" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ictlab.Default" EnableSessionState="True" %>

<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
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
<asp:Content ID="Content3" ContentPlaceHolderID="LoginContent" runat="server">
    <a href="Uitloggen.aspx">Uitloggen</a>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="titel2">
        <a href="Default.aspx"><asp:Label ID="paginaTitel" runat="server" Text=""></asp:Label></a>
    </div>
    <img id="line" src="Images/loginLine.gif" width="870" height="2" alt="line" />
    <asp:Panel ID="PnlSubMenu" runat="server" Visible="false">
    <div id="subMenu">
        <ul>
            <li><a href="Default.aspx">Medewerkers overzicht</a></li>
        </ul>
    </div>
    </asp:Panel>
    <p class="ptext">
        <asp:Label ID="welkomstTekst" runat="server" Text="Label"></asp:Label>
     </p>
    <br />
     <asp:Panel ID="PnlManager" runat="server" Visible="false">
    
    <div id="dataViewLeftEmployers">
        <asp:ListBox CssClass="padding" ID="ListBox2" runat="server" Height="210px" Width="180px"></asp:ListBox>
        <asp:Button CssClass="paddingTwo" ID="Button2" runat="server" Width="180px"  
            Text="Bekijken" onclick="Button2_Click" />
    </div>
    </asp:Panel>
    <asp:Panel ID="PnlTrips" runat="server" Visible="false">
   
    <div id="dataViewLeftTrips" >
        <asp:ListBox CssClass="padding" ID="ListBox1" runat="server" Height="210px" 
            Width="180px" AutoPostBack="True" 
            onselectedindexchanged="ListBox1_SelectedIndexChanged"></asp:ListBox>
    </div>
    </asp:Panel>
    <asp:Panel ID="PnlTripData" runat="server" Visible="false">
    
    <div id="dataViewLeftTwo">
        <p class="padding">
            <asp:Label ID="Label1" runat="server" Width="180px" Text="Huidige trip" Font-Bold="True"></asp:Label>
            <br /><br />
            <asp:Label ID="Label2" runat="server" Width="100px" Text="Datum:"></asp:Label>
            <asp:Label ID="Label3" runat="server" Text="11-06-2011"></asp:Label>
            <br /><br />
            <asp:Label ID="Label4" runat="server" Width="100px" Text="Gem. snelheid:"></asp:Label>
            <asp:Label ID="Label5" runat="server" Text="leeg"></asp:Label>
            <br />
            <asp:Label ID="Label6" runat="server" Width="100px" Text="Top snelheid:"></asp:Label>
            <asp:Label ID="Label7" runat="server" Text="leeg"></asp:Label>
            <br /><br /><br />
            <asp:Label ID="Label8" runat="server" Width="180px" Text="Samenvatting trips" Font-Bold="True"></asp:Label>
            <br /><br />
            <asp:Label ID="Label9" runat="server" Width="100px" Text="Gem. snelheid:"></asp:Label>
            <asp:Label ID="Label10" runat="server" Text="leeg"></asp:Label>
            <br />
            <asp:Label ID="Label11" runat="server" Width="100px" Text="Top snelheid:"></asp:Label>
            <asp:Label ID="Label12" runat="server" Text="leeg"></asp:Label>
        </p>
    </div>
    <div id="dataViewRight" runat="server">
        <Web:ChartControl ID="ChartControl1" runat="server" BorderStyle="Outset" 
            BorderWidth="0" Height="300px" Width="400px" Border-Color="Black" PlotBackground-ForeColor="Black">
        </Web:ChartControl>
    </div>
    <div id="dataViewCenter">
        <div id="map_canvas"></div>
    </div>
    </asp:Panel>
</asp:Content>
