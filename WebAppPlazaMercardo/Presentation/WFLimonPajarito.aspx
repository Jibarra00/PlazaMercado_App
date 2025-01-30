<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFLimonPajarito.aspx.cs" Inherits="Presentation.WFLimonPajarito" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/LimonPajarito.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
        <div class="contenedor">
            <asp:Label ID="lblTitle" runat="server" Text="Limón Pajarito" CssClass="titulo"></asp:Label>
            <img src="resources/images/LimonPajarito.jpg" class="d-block w-100" alt="limon pajarito">
            <asp:Label ID="lblDescription" runat="server" Text="El limón pajarito es pequeño, con un sabor intenso y un aroma característico. Ideal para aderezos, marinadas y cócteles. Muy apreciado por su versatilidad en la cocina." CssClass="descripcion"></asp:Label>
            <br /><br />
            <asp:Button ID="btnPedido" runat="server" Text="Hacer pedido ya" CssClass="btn-pedido" OnClick="btnPedido_Click" />
        </div>
    </form>
</asp:Content>
