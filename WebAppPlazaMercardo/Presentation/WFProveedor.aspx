<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFProveedor.aspx.cs" Inherits="Presentation.WFProveedor" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Proveedor.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Gestión De Proveedores</h1>

        </div>
        <!-- /.container-fluid -->

    </div>
    <%--formulario--%>
    <form id="FrmProveedor" runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFProveedorID" runat="server" />
        <br />
        <%--nit--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nit"></asp:Label>
        <asp:TextBox ID="TBNit" runat="server"></asp:TextBox>
        <br />
        <%--nombre--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--botones de guardar y actualizra--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>



    <%--Lista de Productos--%>
    <asp:Panel ID="PanelAdmin" runat="server">

        <h2>Lista de Proveedores</h2>
        <table id="proveedorTable" class="display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Nit</th>
                    <th>Nombre</th>

                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </asp:Panel>

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Proveedores--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#proveedorTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFProveedor.aspx/ListProveedor",// Se invoca el WebMethod Listar Productos
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de productos del resultado
                    }
                },
                "columns": [
                    { "data": "ProveedorID" },
                    { "data": "Nit" },
                    { "data": "Nombre" },

                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.ProveedorID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ProveedorID}">Eliminar</button>`;
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

            // Editar un proveedor
            $('#proveedorTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#proveedorTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadProveedorData(rowData);
            });

            // Eliminar un proveedor
            $('#proveedorTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del proveedor
                if (confirm("¿Estás seguro de que deseas eliminar este proveedor?")) {
                    DeleteProveedor(id);// Invoca a la función para eliminar el proveedor
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadProveedorData(rowData) {
            $('#<%= HFProveedorID.ClientID %>').val(rowData.ProveedorID);
            $('#<%= TBNit.ClientID %>').val(rowData.Nit);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);

        }

        // Función para eliminar un proveedor
        function DeleteProveedor(id) {
            $.ajax({
                type: "POST",
                url: "WFProveedor.aspx/DeleteProveedor",// Se invoca el WebMethod Eliminar un Proveedor
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#proveedorTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Proveedor eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el proveedor.");
                }
            });
        }
    </script>






</asp:Content>
