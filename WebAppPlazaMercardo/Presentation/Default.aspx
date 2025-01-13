<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="Presentation.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title>Inicio de sesion</title>
    <%--CSS Bootstrap--%>
<link href="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/css/bootstrap.min.css" rel="stylesheet" integrity="sha384-QWTKZyjpPEjISv5WaRU9OFeRpok6YctnYmDr5pNlyT2bRjXh0JMhjY6hW+ALEwIH" crossorigin="anonymous" />
<style>
        body {
            background-image: url('resources/images/Galería barrio Bolívar4.jpg'); /* Cambia la ruta de la imagen */
            background-size: cover;
            background-position: center;
            height: 100vh;
            margin: 0;
            display: flex;
            justify-content: center;
            align-items: center;
        }
        .card {
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0px 4px 12px rgba(0, 0, 0, 0.1);
            background-color: rgba(255, 255, 255, 0.8); /* Fondo semitransparente */
            width: 100%;
            max-width: 400px;
        }
        .card-header {
            text-align: center;
            font-size: 24px;
            font-weight: bold;
            color: #333;
        }
        .form-control {
            margin-bottom: 15px;
        }
        .btn {
            width: 100%;
            padding: 10px;
        }
        #loadingGif {
            text-align: center;
        }
        .error-message {
            color: red;
            font-size: 14px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="d-flex justify-content-center align-items-center vh-100">
            <div class="card" style="width: 24rem;">
                <!-- Imagen de cargando... -->
                <div id="loadingGif" class="text-center" style="display: none;">
                    <img src="resources/images/loading-7528_128.gif" alt="Cargando..." />
                </div>
                <div class="card-body">
                    <h5 class="card-title text-center mb-4">Inicio Sesión</h5>
                    <div class="mb-3">
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="">Correo</asp:Label>
                        <asp:TextBox ID="TBCorreo" CssClass="form-control" TextMode="Email" runat="server"></asp:TextBox><br />
                    </div>
                    <div class="mb-3">
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="">Contraseña</asp:Label>
                        <asp:TextBox ID="TBContrasena" CssClass="form-control" runat="server" TextMode="Password"></asp:TextBox><br />
                    </div>
                    <div class="mb-3 form-check">
                        <input type="checkbox" class="form-check-input" id="exampleCheck1">
                        <label class="form-check-label" for="exampleCheck1">Olvidó su contraseña</label>
                    </div>
                    <asp:Button ID="BtnIniciar" CssClass="btn btn-success btn-lg w-100" runat="server" Text="Iniciar" OnClick="BtnIniciar_Click" OnClientClick="showLoading();" /><br />
                    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
                </div>
            </div>
        </div>
    </form>
    <%-- JS Bootstrap--%>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.3.3/dist/js/bootstrap.bundle.min.js" integrity="sha384-YvpcrYf0tY3lHB60NNkmXc5s9fDVZLESaAA55NDzOxhy9GkcIdslK1eN7N6jIeHz" crossorigin="anonymous"></script>
    <%--Script para mostrar y ocultar la animacion de cargando...--%>
    <script type="text/javascript">
        function showLoading() {
            document.getElementById("loadingGif").style.display = "block";
        }

        function hideLoading() {
            document.getElementById("loadingGif").style.display = "none";
        }
    </script>
</body>
</html>
