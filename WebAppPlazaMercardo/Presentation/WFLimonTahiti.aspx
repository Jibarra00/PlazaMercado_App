<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFLimonTahiti.aspx.cs" Inherits="Presentation.WFLimonTahiti" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/LimonTahiti.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div class="contenedor">
        <asp:Label ID="lblTitle" runat="server" Text="Limón Tahití" CssClass="titulo"></asp:Label>
        <img src="resources/images/limon-tahiti.jpg" class="d-block w-100" alt="limon tahiti">
        <asp:Label ID="lblDescription" runat="server" Text="El limón Tahití es conocido por su jugosidad, sabor fresco y su tamaño grande. Cultivado en las mejores condiciones, es ideal para bebidas, platos salados y postres." CssClass="descripcion"></asp:Label>
        <br /><br />
        <asp:Button ID="btnPedido" runat="server" Text="Hacer pedido ya" CssClass="btn-pedido" OnClick="btnPedido_Click" />
    </div>
    </form>
</asp:Content>
