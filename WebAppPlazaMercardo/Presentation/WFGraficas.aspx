<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFGraficas.aspx.cs" Inherits="Presentation.WFGraficas" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder2" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    <div class="container-fluid">


        <div class="row justify-content-center text-center">
            <div class="col-md-3">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <h5 class="card-title">Total Usuarios</h5>
                        <asp:Label ID="LblCantUsu" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFUsuario.aspx">Mas info
                            <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <h5 class="card-title">Total Empleados</h5>
                        <asp:Label ID="LblCantEmp" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>
                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFEmpleado.aspx">Mas info
                             <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
            <div class="col-md-3">
                <div class="card border-success mb-3" style="max-width: 18rem;">
                    <div class="card-body text-success">
                        <h5 class="card-title">Total Productos</h5>
                        <asp:Label ID="LblCantProd" runat="server" Text="" CssClass="fs-4 fw-bold"></asp:Label>

                    </div>
                    <div class="card-footer bg-transparent border-success text-center">
                        <a class="small-box-footer" href="WFProducto.aspx">Mas info
                             <i class="lni lni-chevron-right-circle"></i>
                        </a>
                    </div>
                </div>
            </div>
        </div>

        <div class="container-fluid">
            <div class="row">
                <!-- Gráfica 1 -->
                <div class="col-md-6">
                    <div class="card border-info mb-3">
                        <div class="card-header">
                            <i class="lni lni-bar-chart-4"></i>
                            Cantidad de productos por categoría
                        </div>
                        <div class="card-body">
                            <div id="piechart" style="width: 100%; height: 400px;"></div>
                        </div>
                    </div>
                </div>

                <!-- Gráfica 2 -->
                <div class="col-md-6">
                    <div class="card border-info mb-3">
                        <div class="card-header">
                            <i class="lni lni-bar-chart-4"></i>
                            Cantidad de productos por fecha
                        </div>
                        <div class="card-body">
                            <div id="barchart" style="width: 100%; height: 400px;"></div>
                        </div>
                    </div>
                </div>
            </div>

            <!-- Gráfica 3 -->
            <div class="row justify-content-center">
                <div class="col-md-6">
                    <div class="card border-info mb-3">
                        <div class="card-header">
                            <i class="lni lni-bar-chart-4"></i>
                            Ventas por Categoría a lo largo del Tiempo
                        </div>
                        <div class="card-body">
                            <div id="linechart" style="width: 100%; height: 400px;"></div>
                        </div>
                    </div>
                </div>
            </div>
        </div>




    </div>
    <%--JQuery--%>
    <script src="https://code.jquery.com/jquery-3.7.1.js" integrity="sha256-eKhayi8LEQwp4NKxN+CfCh+3qOVUtJn3QNZ0TciWLP4=" crossorigin="anonymous"></script>
    <!--Load the AJAX API-->
    <script src="https://www.gstatic.com/charts/loader.js"></script>
    <%--Grafica 1--%>
    <script type="text/javascript">
        // Carga la API de Google Charts
        google.charts.load('current', { 'packages': ['corechart'] });

        // Llama al WebMethod y dibuja el gráfico al cargar la API
        google.charts.setOnLoadCallback(fetchDataAndDrawChart);

        // Función para obtener datos desde el WebMethod
        function fetchDataAndDrawChart() {
            $.ajax({
                url: 'WFGraficas.aspx/ListCountProductsCategories', // Ajustar con el nombre de tu archivo ASPX
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Procesar los datos devueltos por el WebMethod
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Categoría');
                    data.addColumn('number', 'TotalProductos');

                    // Llenar la tabla con los datos del WebMethod
                    rawData.forEach(function (item) {
                        data.addRow([item.CategoryName, parseInt(item.TotalProducts)]);
                    });

                    // Configuración del gráfico
                    var options = {
                        title: '',
                        width: '100%',
                        height: '100%',
                        chartArea: { width: '90%', height: '80%' }
                    };

                    // Dibuja la gráfica
                    var chart = new google.visualization.PieChart(document.getElementById('piechart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }
        // Redibuja la gráfica al redimensionar la ventana
        window.addEventListener('resize', fetchDataAndDrawChart);
    </script>

    <script>
        // Cargar el paquete de Google Charts y dibujar el gráfico cuando esté listo
        google.charts.load('current', { packages: ['corechart'] });
        google.charts.setOnLoadCallback(fetchDataAndDrawBarChart);

        function fetchDataAndDrawBarChart() {
            $.ajax({
                url: 'WFGraficas.aspx/ListProductsByDate', // Ajusta con el nombre de tu archivo ASPX
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    // Procesar los datos devueltos por el WebMethod
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Producto y Fecha'); // Combinamos producto y fecha en una columna
                    data.addColumn('number', 'Cantidad'); // Cantidad en el eje horizontal

                    // Llenar la tabla con los datos del WebMethod
                    rawData.forEach(function (item) {
                        // Asegurarse de que los tipos de datos sean correctos
                        if (typeof item.NombreProducto === 'string' && typeof item.Cantidad === 'number' && typeof item.Fecha === 'string') {
                            // Combinar la fecha y el nombre del producto
                            var label = item.Fecha + " - " + item.NombreProducto;
                            data.addRow([label, item.Cantidad]);
                        }
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Productos por Fecha',
                        width: '100%',
                        height: '500px',
                        chartArea: { width: '80%', height: '70%' },
                        vAxis: { title: 'Cantidad', minValue: 0 },
                        hAxis: { title: 'Producto y Fecha' },
                        bars: 'vertical', // Tipo de barras: horizontal
                        legend: { position: 'none' }
                    };

                    // Dibujar la gráfica
                    var chart = new google.visualization.ColumnChart(document.getElementById('barchart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }

        // Redibuja la gráfica al redimensionar la ventana
        window.addEventListener('resize', fetchDataAndDrawBarChart);
    </script>

    <script>
        // Cargar el paquete de Google Charts y dibujar el gráfico cuando esté listo
        google.charts.load('current', { packages: ['corechart'] });
        google.charts.setOnLoadCallback(fetchDataAndDrawLineChart);

        function fetchDataAndDrawLineChart() {
            $.ajax({
                url: 'WFGraficas.aspx/ListCategorySalesByDate',
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                dataType: 'json',
                success: function (response) {
                    var rawData = response.d.data;

                    // Crear la tabla de datos para Google Charts
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Fecha');
                    data.addColumn('number', 'Cantidad');

                    // Agrupar los datos por categoría y fechas
                    var groupedData = {};
                    rawData.forEach(function (item) {
                        if (!groupedData[item.Categoria]) {
                            groupedData[item.Categoria] = [];
                        }
                        groupedData[item.Categoria].push([item.Fecha, item.CantidadVendida]);
                    });

                    // Crear filas para cada categoría
                    Object.keys(groupedData).forEach(function (category) {
                        groupedData[category].forEach(function (entry) {
                            data.addRow([entry[0] + " - " + category, entry[1]]);
                        });
                    });

                    // Configuración del gráfico
                    var options = {
                        title: 'Ventas por Categoría a lo largo del Tiempo',
                        width: '100%',
                        height: '500px',
                        hAxis: { title: 'Fecha' },
                        vAxis: { title: 'Cantidad Vendida' },
                        legend: { position: 'bottom' }
                    };

                    // Dibujar el gráfico
                    var chart = new google.visualization.LineChart(document.getElementById('linechart'));
                    chart.draw(data, options);
                },
                error: function (error) {
                    console.error('Error al obtener los datos: ', error);
                }
            });
        }
    </script>





</asp:Content>
