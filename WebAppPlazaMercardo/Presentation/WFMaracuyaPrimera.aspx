<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFMaracuyaPrimera.aspx.cs" Inherits="Presentation.WFMaracuyaPrimera" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="resources/css/MaracuyaPrimera.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form runat="server">
    <div class="contenedor">
        <asp:Label ID="lblTitle" runat="server" Text="Maracuyá del Sur de Colombia" CssClass="titulo"></asp:Label>
        <img src="resources/images/MaracuyaPrimeraWF.jpg" class="d-block w-100" alt="juan">
        <asp:Label ID="lblDescription" runat="server" Text="La maracuyá, también conocida como fruta de la pasión, es una fruta tropical originaria de América Latina. En el sur de Colombia, la maracuyá se cultiva en regiones de clima cálido, donde el suelo y las condiciones climáticas favorecen su crecimiento y le dan un sabor único, dulce y ácido a la vez. Esta fruta es rica en vitaminas A y C, y es conocida por sus propiedades refrescantes y su versatilidad en jugos, postres y platos gourmet. Esta maracuya es elegida por los comerciantes, por su belleza y sabor, este tipo de maracuya tiene mas durabilidad." CssClass="descripcion"></asp:Label>
        <br /><br />

        <asp:Button ID="btnPedido" runat="server" Text="Hacer pedido ya" CssClass="btn-pedido" OnClick="btnPedido_Click" />
    </div>
        </form>
</asp:Content>
