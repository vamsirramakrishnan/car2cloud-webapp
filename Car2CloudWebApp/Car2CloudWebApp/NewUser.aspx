<%@ Page Title="Nieuwe Gebruiker" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="ictlab.NewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LoginContent" runat="server">
    <a href="Uitloggen.aspx">Uitloggen</a>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MainContent" runat="server">
    <asp:Literal id="lit" runat="server" />
<table id="tableNewUser" runat="server">
    <tr>
        <td>Voornaam</td><td><asp:TextBox ID="tbFirstName" runat="server" /></td>
    </tr>
    <tr>
        <td>Achternaam</td><td><asp:TextBox ID="tbLastName" runat="server" /></td>
    </tr>
    <tr>
        <td>Emailadres</td><td><asp:TextBox ID="tbEmail" runat="server" /></td>
    </tr>
    <tr>
        <td>Gebruikersniveau</td>
        <td>
            <asp:DropDownList ID="ddlRoleID" runat="server">
                <asp:ListItem Text="Normale Gebruiker" Selected="True" Value="2" /> 
                <asp:ListItem Text="Administrator" Value="1" />
            </asp:DropDownList>
        </td>
    </tr>
        <!--Admin-1 Gewonegebruiker-2 -->
        <!--CompanyID niet invullen, is automatisch gelijk aan de gebruiker die hem aanmaakt.-->
    <tr>
        <td><asp:Button ID="btnSendData" runat="server" Text="Gebruiker toevoegen" 
                onclick="btnSendData_Click" /></td>
    </tr>
</table>
</asp:Content>