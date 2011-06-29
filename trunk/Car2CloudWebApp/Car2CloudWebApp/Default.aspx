<%@ Page Title="Overzicht" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ictlab.Default" %>

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
    <div class="titel2">Overzicht</div>
    <img id="line" src="Images/loginLine.gif" width="870" height="2" alt="line" />
    <p class="ptext">Welkom Thomas Hendriksen. U bevindt zich in het overzicht van de door u gereden ritten. Het is mogelijk om informatie over de ritten op te vragen door deze te selecteren en op "ophalen" te klikken.
     </p>
    <br />
    <div id="dataViewLeft">
        <asp:ListBox CssClass="padding" ID="ListBox1" runat="server" Height="210px" Width="180px"></asp:ListBox>
        <asp:Button CssClass="paddingTwo" ID="Button1" runat="server" Width="180px"  
            Text="Ophalen" onclick="Button1_Click" />
    </div>
    <div id="dataViewLeftTwo">
        <p class="padding">
            <asp:Label ID="Label1" runat="server" Width="180px" Text="Den Haag - Rotterdam" Font-Bold="True"></asp:Label>
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
            <asp:Label ID="Label8" runat="server" Width="180px" Text="Algemeen" Font-Bold="True"></asp:Label>
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
</asp:Content>
