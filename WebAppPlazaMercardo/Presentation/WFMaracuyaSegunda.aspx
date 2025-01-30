<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaracuyaSegunda.aspx.cs" Inherits="Presentation.WFMaracuyaSegunda" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/MaracuyaSegunda.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div class="contenedor">
        <asp:Label ID="lblTitle" runat="server" Text="Maracuyá Segunda Calidad" CssClass="titulo"></asp:Label>
        <img src="resources/images/maracuya segunda def.jpg" class="d-block w-100" alt="maracuya segunda">
        <asp:Label ID="lblDescription" runat="server" Text="La maracuyá de segunda calidad es ideal para usos industriales y gastronómicos. Aunque presenta pequeñas imperfecciones estéticas, mantiene el delicioso sabor tropical que caracteriza a esta fruta. Perfecta para jugos, mermeladas y productos procesados." CssClass="descripcion"></asp:Label>
        <br /><br />
        <asp:Button ID="btnPedido" runat="server" Text="Hacer pedido ya" CssClass="btn-pedido" OnClick="btnPedido_Click" />
    </div>
    </form>
</asp:Content>
