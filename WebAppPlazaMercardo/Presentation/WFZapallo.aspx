<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFZapallo.aspx.cs" Inherits="Presentation.WFZapallo" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/Zapallo.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div class="contenedor">
        <asp:Label ID="lblTitle" runat="server" Text="Zapallo" CssClass="titulo"></asp:Label>
        <img src="resources/images/zapallo-1.jpg" class="d-block w-100" alt="zapallo">
        <asp:Label ID="lblDescription" runat="server" Text="El zapallo es un vegetal altamente nutritivo, rico en vitaminas y minerales. Su sabor dulce y textura cremosa lo hacen perfecto para sopas, cremas y postres." CssClass="descripcion"></asp:Label>
        <br /><br />
        <asp:Button ID="btnPedido" runat="server" Text="Hacer pedido ya" CssClass="btn-pedido" OnClick="btnPedido_Click" />
    </div>
    </form>
</asp:Content>
