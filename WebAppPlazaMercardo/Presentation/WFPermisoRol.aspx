<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermisoRol.aspx.cs" Inherits="Presentation.WFPermisoRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form >
        <%--ID--%>
        <asp:HiddenField ID="HFPermisoRolID" runat="server" />
        <br />
        <%--ROLES--%>
        <asp:Label ID="Label1" runat="server" Text="Roles"></asp:Label>
        <asp:DropDownList ID="DDLRoles" runat="server"></asp:DropDownList>
        <br />
        <%--PERMISOS--%>
        <asp:Label ID="Label2" runat="server" Text="Permisos"></asp:Label>
        <asp:DropDownList ID="DDLPermisos" runat="server"></asp:DropDownList>
        <br />
        <%--FECHA--%>
        <asp:Label ID="Label3" runat="server" Text="Fecha de Asignación"></asp:Label>
        <asp:TextBox ID="TBFecha" runat="server" TextMode="Date"></asp:TextBox>
        <br />
        <div>
            <asp:Button ID="BtnGuardar" runat="server" Text="Guardar" OnClick="BtnGuardar_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>

    <%--Lista de permisos roles--%>
    <h2>Lista de Proveedores</h2>
    <table id="permisosRolesTable" class="display" style="width: 100%">
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

    <%--DataTables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Permisos rol--%>
    <script type="text/javascript">
        $(document).ready(function () {
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
                            return `<button class="edit-btn" data-id="${row.PermisoRolID}">Editar</button>
                                 <button class="delete-btn" data-id="${row.PermisoRolID}">Eliminar</button>`;
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
