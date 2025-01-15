<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermisoRol.aspx.cs" Inherits="Presentation.WFPermisoRol" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="card m-1">
        <div class="card-header">
            Gestion de Permisos Roles
        </div>
        <div class="card-body">
            <form id="FrmPermisoRol" runat="server">
                <%--ID--%>
                <asp:HiddenField ID="HFPermisoRolID" runat="server" />
                <div class="row m-1">
                    <div class="col">
                        <%--ROLES--%>
                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Roles"></asp:Label>
                        <asp:DropDownList ID="DDLRoles" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <br />
                    <div class="col">
                        <%--PERMISOS--%>
                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Permisos"></asp:Label>
                        <asp:DropDownList ID="DDLPermisos" CssClass="form-select" runat="server"></asp:DropDownList>
                    </div>
                    <div class="col-3">
                        <%--FECHA--%>
                        <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Fecha de Asignación"></asp:Label>
                        <asp:TextBox ID="TBFecha" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                        <br />
                    </div>
                </div>
                <div class="row m-2">
                    <div class="col">
                        <asp:Button ID="BtnSave" CssClass="btn btn-success" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" />
                        <asp:Button ID="BtnUpdate" CssClass="btn btn-success" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
                        <asp:Label ID="LblMsg" CssClass="form-label" runat="server" Text=""></asp:Label>
                    </div>
                </div>
            </form>
        </div>
    </div>

    <%--Lista de permisos roles--%>
    <div class="card m-1">
        <asp:Panel ID="PanelAdmin" runat="server">

            <div class="card-header">
                Lista de Permisos Roles
            </div>
            <div class="card-body">
                <table id="permisosRolesTable" class=" table table-hover display" style="width: 100%">
                    <thead>
                        <tr>
                            <th>ID</th>
                            <th>IdPermiso</th>
                            <th>Permiso</th>
                            <th>IdRol</th>
                            <th>Rol</th>
                            <th>Fecha Asiganación</th>
                        </tr>
                    </thead>
                    <tbody>
                    </tbody>
                </table>
            </div>
        </asp:Panel>
    </div>
    <%--DataTables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Permisos rol--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#permisosRolesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFPermisoRol.aspx/ListPermisoRol",// Se invoca el WebMethod Listar permiso rol
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de permiso rol del resultado
                    }
                },
                "columns": [
                    { "data": "PermisoRolID" },
                    { "data": "FkPermiso", "visible": false },
                    { "data": "namePermiso" },
                    { "data": "FkRol", "visible": false },
                    { "data": "nameRol" },
                    { "data": "Date" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.PermisoRolID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.PermisoRolID}">Eliminar</button>`;
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
            $('#permisosRolesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#permisosRolesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadPermisoRolData(rowData);
            });

            // Eliminar un producto
            $('#permisosRolesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este Permiso Rol?")) {
                    DeletePermisosRol(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadPermisoRolData(rowData) {
            $('#<%= HFPermisoRolID.ClientID %>').val(rowData.PermisoRolID);
            $('#<%= DDLPermisos.ClientID %>').val(rowData.FkPermiso);
            $('#<%= DDLRoles.ClientID %>').val(rowData.FkRol);
            $('#<%= TBFecha.ClientID %>').val(rowData.Date);
        }

        // Función para eliminar un producto
        function DeletePermisosRol(id) {
            $.ajax({
                type: "POST",
                url: "WFPermisoRol.aspx/DeletePermisosRol",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#productsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Permiso Rol eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Permiso Rol.");
                }
            });
        }
    </script>
</asp:Content>
