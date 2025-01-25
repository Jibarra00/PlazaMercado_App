<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFProductoCliente.aspx.cs" Inherits="Presentation.WFProductoCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/ProductosCliente.css" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <h1 class="h3 mb-4 text-gray-800">Productos para el Cliente</h1>

        <!-- Contenedor de productos -->
        <form runat="server">
        <div class="row">
            <!-- Producto 1 -->
            <div class="col-md-4">
                <div class="card">
                    <img src="resources/images/maracuya Primera.png" alt="Producto 1" class="card-img-top">
                    <div class="card-body">
                        <h5 class="card-title">Maracuyá Primera</h5>
                        <p class="card-text">Conoce más sobre el producto haciendo clic en el botón.</p>
                        <asp:Button ID="btnVerProducto1" runat="server" Text="Ver Más" PostBackUrl="~/WFMaracuyaPrimera.aspx" CssClass="btn btn-primary" />
                        <!-- Enlace a WhatsApp -->
                        <a href="https://api.whatsapp.com/send?phone=573159024242&text=Hola,%20quiero%20realizar%20un%20pedido%20de%20Maracuyá%20Primera" 
                           class="btn btn-secondary" target="_blank">Realizar Pedido</a>
                    </div>
                </div>
            </div>

            <!-- Producto 2 -->
            <div class="col-md-4">
                <div class="card">
                    <img src="resources/images/maracuya segunda def.jpg" alt="Producto 2" class="card-img-top">
                    <div class="card-body">
                        <h5 class="card-title">Maracuyá Segunda</h5>
                        <p class="card-text">Conoce más sobre el producto haciendo clic en el botón.</p>
                         <asp:Button ID="Button2" runat="server" Text="Ver Más" PostBackUrl="~/WFMaracuyaSegunda.aspx" CssClass="btn btn-primary" />
                        <a href="https://api.whatsapp.com/send?phone=573159024242&text=Hola,%20quiero%20realizar%20un%20pedido%20de%20Maracuyá%20Segunda" 
                           class="btn btn-secondary" target="_blank">Realizar Pedido</a>
                    </div>
                </div>
            </div>

            <!-- Producto 3 -->
            <div class="col-md-4">
                <div class="card">
                    <img src="resources/images/maracuya pulpa.jpg" alt="Producto 3" class="card-img-top">
                    <div class="card-body">
                        <h5 class="card-title">Maracuyá Pulpa</h5>
                        <p class="card-text">Conoce más sobre el producto haciendo clic en el botón.</p>
                         <asp:Button ID="Button3" runat="server" Text="Ver Más" PostBackUrl="~/WFMaracuyaPulpa.aspx" CssClass="btn btn-primary" />
                        <a href="https://api.whatsapp.com/send?phone=573159024242&text=Hola,%20quiero%20realizar%20un%20pedido%20de%20Maracuyá%20Pulpa" 
                           class="btn btn-secondary" target="_blank">Realizar Pedido</a>
                    </div>
                </div>
            </div>

            <!-- Producto 4 -->
            <div class="col-md-4">
                <div class="card">
                    <img src="resources/images/limon-tahiti.jpg" alt="Limón Tahití" class="card-img-top">
                    <div class="card-body">
                        <h5 class="card-title">Limón Tahití</h5>
                        <p class="card-text">Conoce más sobre el producto haciendo clic en el botón.</p>
                         <asp:Button ID="Button4" runat="server" Text="Ver Más" PostBackUrl="~/WFLimonTahiti.aspx" CssClass="btn btn-primary" />
                        <a href="https://api.whatsapp.com/send?phone=573159024242&text=Hola,%20quiero%20realizar%20un%20pedido%20de%20Limón%20Tahití" 
                           class="btn btn-secondary" target="_blank">Realizar Pedido</a>
                    </div>
                </div>
            </div>

            <!-- Producto 5 -->
            <div class="col-md-4">
                <div class="card">
                    <img src="resources/images/LimonPajarito.jpg" alt="Limón Pajarito" class="card-img-top">
                    <div class="card-body">
                        <h5 class="card-title">Limón Pajarito</h5>
                        <p class="card-text">Conoce más sobre el producto haciendo clic en el botón.</p>
                         <asp:Button ID="Button5" runat="server" Text="Ver Más" PostBackUrl="~/WFLimonPajarito.aspx" CssClass="btn btn-primary" />
                        <a href="https://api.whatsapp.com/send?phone=573159024242&text=Hola,%20quiero%20realizar%20un%20pedido%20de%20Limón%20Pajarito" 
                           class="btn btn-secondary" target="_blank">Realizar Pedido</a>
                    </div>
                </div>
            </div>

            <!-- Producto 6 -->
            <div class="col-md-4">
                <div class="card">
                    <img src="resources/images/zapallo-1.jpg" alt="Zapallo" class="card-img-top">
                    <div class="card-body">
                        <h5 class="card-title">Zapallo</h5>
                        <p class="card-text">Conoce más sobre el producto haciendo clic en el botón.</p>
                         <asp:Button ID="Button1" runat="server" Text="Ver Más" PostBackUrl="~/WFZapallo.aspx" CssClass="btn btn-primary" />
                        <a href="https://api.whatsapp.com/send?phone=573159024242&text=Hola,%20quiero%20realizar%20un%20pedido%20de%20Zapallo" 
                           class="btn btn-secondary" target="_blank">Realizar Pedido</a>
                    </div>
                </div>
            </div>
        </div>
        </form>
    </div>
</asp:Content>

