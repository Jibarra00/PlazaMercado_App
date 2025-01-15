<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPedidos.aspx.cs" Inherits="Presentation.WFPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Pedidos.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Gestión De Pedidos</h1>

        </div>
        <!-- /.container-fluid -->

    </div>
    <form id="FrmPedidos" runat="server">
    <%--ID--%>
        <asp:HiddenField ID="HFPedidosID" runat="server" />
    <br />
    <%--fecha--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha (YYYY-MM-DD)"></asp:Label>
    <asp:TextBox ID="TBFecha" runat="server"></asp:TextBox>
    <br />
    <%--estado--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el estado"></asp:Label>
    <asp:DropDownList ID="DDLEstado" runat="server">
            <asp:ListItem Value="0">Seleccione</asp:ListItem>
            <asp:ListItem Value="Activo">Activo</asp:ListItem>
            <asp:ListItem Value="Inactivo">Inactivo</asp:ListItem>
        </asp:DropDownList><br />
    <br />
    <%--especificacion--%>
    <asp:Label ID="Label3" runat="server" Text="Especifique el estado"></asp:Label>
    <asp:TextBox ID="TBEspecificacion" runat="server"></asp:TextBox>
    <br />
    <%--fkcliente--%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione el cliente"></asp:Label>
    <asp:DropDownList ID="DDLCliente" runat="server"></asp:DropDownList>
    <br />
    <%--fkproducto--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione el producto"></asp:Label>
    <asp:DropDownList ID="DDLProducto" runat="server"></asp:DropDownList>

    <%--BOTON DE GUARDAR Y ACTUALIZAR--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BTSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BTUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    </form>

    <%--lista de pedidos--%>
    <asp:Panel ID="PanelAdmin" runat="server">

    <h2>Lista de Pedidos</h2>
    <table id="PedidosTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Fecha</th>
                <th>Estado</th>
                <th>Especificacion</th>
                <th>FkCliente</th>
                <th>Cliente</th>
                <th>FkProducto</th>
                <th>Producto</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    </asp:Panel>
    <%--DataTables--%>
    <script src="resources/js/datatables.min.js"></script>
    <%--Pedidos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#PedidosTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPedidos.aspx/ListPedidos",// Se invoca el WebMethod Listar Pedidos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de pedidos del resultado
                    }
                },
                "columns": [
                    { "data": "PedidosID" },
                    { "data": "Fecha" },
                    { "data": "Estado" },
                    { "data": "Especificadion" },
                    { "data": "FkCliente" ,"visible": false },
                    { "data": "nameCliente"},
                    { "data": "FkProducto", "visible": false },
                    { "data": "nameProducto" },
                    
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.PedidosID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.PedidosID}">Eliminar</button>`;
                            }
                            return buttons;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar _MENU_ registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página _PAGE_ de _PAGES_",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de _MAX_ registros totales)",
                    "search": "Buscar:",
                    "paginate": {
                        "first": "Primero",
                        "last": "Último",
                        "next": "Siguiente",
                        "previous": "Anterior"
                    }
                }

            });

            // Editar un pedido
            $('#PedidosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#PedidosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadPedidoData(rowData);
            });

            // Eliminar un pedido
            $('#PedidosTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del pedido
                if (confirm("¿Estás seguro de que deseas eliminar este pedido?")) {
                    DeletePedido(id);// Invoca a la función para eliminar el pedido
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadPedidoData(rowData) {
            $('#<%= HFPedidosID.ClientID %>').val(rowData.PedidosID);
            $('#<%= TBFecha.ClientID %>').val(rowData.Fecha);
            $('#<%= DDLEstado.ClientID %>').val(rowData.Estado);
            $('#<%= TBEspecificacion.ClientID %>').val(rowData.Especificadion);
            $('#<%= DDLCliente.ClientID %>').val(rowData.FkCliente);
            $('#<%= DDLProducto.ClientID %>').val(rowData.FkProducto);
        }

        // Función para eliminar un pedido
        function DeletePedido(id) {
            $.ajax({
                type: "POST",
                url: "WFPedidos.aspx/DeletePedido",// Se invoca el WebMethod Eliminar un pedido
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#PedidosTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("El pedido fue eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el pedido.");
                }
            });
        }
    </script>

</asp:Content>
