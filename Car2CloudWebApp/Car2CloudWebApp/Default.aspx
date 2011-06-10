<%@ Page Title="Home Page" Language="C#" MasterPageFile="Site.master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ictlab._Default" EnableEventValidation="false"%>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="HeaderContent" runat="server" ContentPlaceHolderID="HeadContent">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="LoginContent" runat="server">
    <a href="Uitloggen.aspx">Uitloggen</a>
</asp:Content>
<asp:Content ID="BodyContent" runat="server" ContentPlaceHolderID="MainContent">
    <div class="titel2">Datastore</div>
    <img id="line" src="Images/loginLine.gif" width="870" height="2" alt="" />
    <p class="ptext">De hieronderstaande tabel bevat alle data die vanuit de auto naar de cloud zijn geupload.</p>
    <br /><br /><br />
    <telerik:RadGrid ID="Grd1" runat="server">
        <PagerStyle Mode="NumericPages"></PagerStyle>
    </telerik:RadGrid>
</asp:Content>
