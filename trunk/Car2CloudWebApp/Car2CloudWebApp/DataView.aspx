<%@ Page Title="Overzicht" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="DataView.aspx.cs" Inherits="WebApplication1.DataView" %>

<%@ Register Assembly="WebChart" Namespace="WebChart" TagPrefix="Web" %>
<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LoginContent" runat="server">
    <a href="Uitloggen.aspx">Uitloggen</a>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="titel2">Overzicht</div>
    <img id="line" src="Images/loginLine.gif" width="870" height="2" alt="" />
    <p class="ptext">De hieronderstaande tabel bevat alle data die vanuit de auto naar de cloud zijn geupload.</p>
    <br />
    <Web:ChartControl ID="ChartControl1" runat="server" BorderStyle="Outset" 
        BorderWidth="0" Height="300px" Width="400px" Border-Color="Black" PlotBackground-ForeColor="Black">
    </Web:ChartControl>
</asp:Content>
