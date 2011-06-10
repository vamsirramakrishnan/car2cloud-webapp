<%@ Page Title="" Language="C#" MasterPageFile="Site.Master" AutoEventWireup="true" CodeBehind="Inloggen.aspx.cs" Inherits="ictlab.Inloggen" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LoginContent" runat="server">
    <a href="Inloggen.aspx">Inloggen</a>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div id="loginBox">
        <p class="titel">Log in</p>
        <img id="loginLine" src="Images/loginLine.gif" alt="" />
        <table class="text">
            <tr><td align="left" class="loginText" width="150">Gebruikersnaam</td><td align="right" class="loginField"><asp:TextBox id="Gebruikersnaam" runat="server" Width="100%"></asp:TextBox></td></tr>
            <tr><td align="left" class="loginText" width="150">Wachtwoord</td><td align="right" class="loginField"><asp:TextBox id="Wachtwoord" TextMode="Password" runat="server" Width="100%"></asp:TextBox></td></tr>
        </table>
        <table class="text">
            <tr><td width="250"><asp:Label ID="Melding" runat="server" Width="250" Height="19"></asp:Label></td><td align="right"><asp:Button ID="Button1" runat="server" onclick="Button1_Click" CssClass="inlogButton" Width="110px" Height="40px"/></td></tr>
        </table>
    </div>
</asp:Content>
