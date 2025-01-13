<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFInicio.aspx.cs" Inherits="Presentation.WFInicio" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <!-- Hotjar Tracking Code for Sitio 5214935 (falta el nombre) -->
<script>
    (function(h,o,t,j,a,r){
        h.hj=h.hj||function(){(h.hj.q=h.hj.q||[]).push(arguments)};
        h._hjSettings={hjid:5214935,hjsv:6};
        a=o.getElementsByTagName('head')[0];
        r=o.createElement('script');r.async=1;
        r.src=t+h._hjSettings.hjid+j+h._hjSettings.hjsv;
        a.appendChild(r);
    })(window,document,'https://static.hotjar.com/c/hotjar-','.js?sv=');
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    <div class="container-fluid">

    <!-- Page Heading -->
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Inicio</h1>
        <a href="#" class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm"><i
            class="fas fa-download fa-sm text-white-50"></i>Generate Report</a>

    </div>
    <!-- /.container-fluid -->

</div>


        <!-- Contenedor para el carrusel de imágenes -->
<div class="carousel-container">
    <div id="carouselExampleIndicators" class="carousel slide w-100">
        <div class="carousel-indicators">
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="0" class="active" aria-current="true" aria-label="Slide 1"></button>
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="1" aria-label="Slide 2"></button>
            <button type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide-to="2" aria-label="Slide 3"></button>
        </div>
        <div class="carousel-inner">
            <div class="carousel-item active">
                <img src="resources/images/Galería barrio Bolívar4.jpg" class="d-block w-100" alt="juan">
                <div class="carousel-caption d-none d-md-block">
                    <h1 style="font-size: 25px;"><i>Barrio Bolivar 1</i></h1>
                    <p style="font-size: 15px;"><i>Demo1</i></p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="resources/images/Maracuya.jpg" class="d-block w-100" alt="jeanpaul">
                <div class="carousel-caption d-none d-md-block">
                    <h1 style="font-size: 25px;"><i>Jean Paul Ibarra</i></h1>
                    <p style="font-size: 15px;"><i>Integrante del grupo</i></p>
                </div>
            </div>
            <div class="carousel-item">
                <img src="resources/images/Limon.jpg" class="d-block w-100" alt="messibarcelona">
                <div class="carousel-caption d-none d-md-block">
                    <h5 style="font-size: 25px;"><i>Invernadero</i></h5>
                    <p style="font-size: 15px;">Proyecto integrador del invernadero</p>
                </div>
            </div>
        </div>
       <button class="carousel-control-prev" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="prev">
    <span class="custom-prev-icon">‹</span> <!-- Reemplaza el icono con un símbolo o un SVG -->
    <span class="visually-hidden"></span>
</button>
<button class="carousel-control-next" type="button" data-bs-target="#carouselExampleIndicators" data-bs-slide="next">
    <span class="custom-next-icon">›</span> <!-- Reemplaza el icono con un símbolo o un SVG -->
    <span class="visually-hidden"></span>
</button>

    </div>
</div>


    <!-- /.container-fluid -->

<!-- End of Main Content -->
</asp:Content>
