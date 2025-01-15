<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermiso.aspx.cs" Inherits="Presentation.WFPermiso" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
    <link href="resources/css/Permiso.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container-fluid">

        <!-- Page Heading -->
        <div class="d-sm-flex align-items-center justify-content-between mb-4">
            <h1 class="h3 mb-0 text-gray-800">Gestión De Permisos</h1>

        </div>
        <!-- /.container-fluid -->

    </div>
    <form id="FrmPermiso" runat="server">
       <%-- ID--%>
        <asp:HiddenField ID="HFPermisoID" runat="server" />
        <br />
        <%-- Nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--descripcion--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
        <br />
        <%--Botones de guardar y actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BTSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BTUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>
    <%--Lista de permisos--%>
    <asp:Panel ID="PanelAdmin" runat="server">

    <h2>Lista de Permisos</h2>
    <table id="PermisosTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombre</th>
                <th>Descripcion</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    </asp:Panel>

    <%--DataTables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>


    <%--Permiso--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#PermisosTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermiso.aspx/ListPermiso",// Se invoca el WebMethod Listar Productos
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
                    { "data": "PermisoID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.PermisoID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.PermisoID}">Eliminar</button>`;
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

            // Editar un Permiso
            $('#PermisosTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#PermisosTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadPermisoData(rowData);
            });

            // Eliminar un Permiso
            $('#PermisosTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del Permiso
                if (confirm("¿Estás seguro de que deseas eliminar este Permiso?")) {
                    DeletePermiso(id);// Invoca a la función para eliminar el Permiso
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadPermisoData(rowData) {
            $('#<%= HFPermisoID.ClientID %>').val(rowData.PermisoID);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Descripcion);
            
        }

        // Función para eliminar un permiso
        function DeletePermiso(id) {
            $.ajax({
                type: "POST",
                url: "WFPermiso.aspx/DeletePermiso",// Se invoca el WebMethod Eliminar un permiso
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#PermisosTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Permiso se ha eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Permiso.");
                }
            });
        }
    </script>
</asp:Content>
