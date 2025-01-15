<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFGestion.aspx.cs" Inherits="Presentation.WFGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Gestion2.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Gestión</h1>

        </div>
        <!-- /.container-fluid -->

    </div>
    <form id="FrmGestion" runat="server">

    <%-- Id --%>
    <asp:HiddenField ID="HFManagementID" runat="server" />
    <br />
    <%-- Fecha --%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server"></asp:TextBox>
    <br />
    <%--Descripción--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
    <%-- Empleado --%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione el empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmployee" runat="server"></asp:DropDownList>
    <br />
    <%-- Producto --%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione el producto"></asp:Label>
    <asp:DropDownList ID="DDLProduct" runat="server"></asp:DropDownList>

    <%-- Boton guardar y actualizar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    </form>
    <%-- Lista de gestiones --%>
    <asp:Panel ID="PanelAdmin" runat="server">

    <h2>Lista de Gestiones</h2>
    <table id="managementTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Fecha</th>
                <th>Descripción</th>
                <th>Empleado</th>
                <th>Producto</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

     <%--Gestiones--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#managementTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFGestion.aspx/ListManagement",// Se invoca el WebMethod Listar Productos
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
                    { "data": "ManagementID" },
                    { "data": "Date" },
                    { "data": "Description" },
                    { "data": "Employee" },
                    { "data": "Product" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.ManagementID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ManagementID}">Eliminar</button>`;
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
            // Editar un producto
            $('#managementTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#managementTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadManagementData(rowData);
            });
            // Eliminar un producto
            $('#managementTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID de la categoria
                if (confirm("¿Estás seguro de que deseas eliminar esta gestión?")) {
                    deleteManagement(id);// Invoca a la función para eliminar el producto
                }
            });
        });
        // Cargar los datos en los TextBox y DDL para actualizar
        function loadManagementData(rowData) {
            $('#<%= HFManagementID.ClientID %>').val(rowData.ManagementID);
            $('#<%= TBDescription.ClientID %>').val(rowData.Text);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= DDLEmployee.ClientID %>').val(rowData.Client);
            $('#<%= DDLProduct.ClientID %>').val(rowData.Product);
        }
        // Función para eliminar un producto
        function deleteManagement(id) {
            $.ajax({
                type: "POST",
                url: "WFGestion.aspx/DeleteManagement",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#managementTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Gestión eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar la gestión");
                }
            });
        }
    </script>
</asp:Content>
