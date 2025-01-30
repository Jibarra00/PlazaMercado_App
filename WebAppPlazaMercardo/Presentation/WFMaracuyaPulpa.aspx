<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaracuyaPulpa.aspx.cs" Inherits="Presentation.WFMaracuyaPulpa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/MaracuyaPulpa.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div class="contenedor">
        <asp:Label ID="lblTitle" runat="server" Text="Pulpa de Maracuyá" CssClass="titulo"></asp:Label>
        <img src="resources/images/maracuya pulpa.jpg" class="d-block w-100" alt="pulpa maracuya">
        <asp:Label ID="lblDescription" runat="server" Text="La pulpa de maracuyá es un producto 100% natural, lista para preparar jugos, helados y postres. Extraída de las mejores frutas de la región, garantiza frescura y un sabor auténtico que encantará a todos." CssClass="descripcion"></asp:Label>
        <br /><br />
        <asp:Button ID="btnPedido" runat="server" Text="Hacer pedido ya" CssClass="btn-pedido" OnClick="btnPedido_Click" />
    </div>
    </form>
</asp:Content>
