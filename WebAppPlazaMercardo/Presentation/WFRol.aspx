<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRol.aspx.cs" Inherits="Presentation.WFRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <link href="resources/css/datatables.min.css" rel="stylesheet" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--formulario--%>
    <form>
        <%--ID--%>
        <asp:HiddenField ID="HFRolID" runat="server" />
        <br />

        <%--nombre--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
        <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
        <br />
        <%--descripción--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
        <asp:TextBox ID="TBdescripcion" runat="server"></asp:TextBox>
        <br />

        <%--botones de guardar y actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
            
        </div>
        <br />

    </form>
    <%--Lista de Roles--%>
    <h2>Lista de Roles</h2>
    <table id="RolTable" class="display" style="width: 100%">
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

    <%--Datatables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>
    <%--Roles--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#RolTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFRol.aspx/ListRol",// Se invoca el WebMethod Listar roles
                    "type": "POST",
                    "contentType": "application/json",
                    "data": function (d) {
                        return JSON.stringify(d);// Convierte los datos a JSON
                    },
                    "dataSrc": function (json) {
                        return json.d.data;// Obtiene la lista de roles del resultado
                    }
                },
                "columns": [
                    { "data": "rolID" },
                    { "data": "Nombre" },
                    { "data": "Descripcion" },
                    
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.rolID}">Editar</button>
                                 <button class="delete-btn" data-id="${row.rolID}">Eliminar</button>`;
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

            // Editar un rol
            $('#RolTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#RolTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadRolData(rowData);
            });

            // Eliminar un rol
            $('#RolTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                    DeleteRol(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadRolData(rowData) {
            $('#<%= HFRolID.ClientID %>').val(rowData.rolID);
            $('#<%= TBNombre.ClientID %>').val(rowData.Nombre);
            $('#<%= TBdescripcion.ClientID %>').val(rowData.Descripcion);

        }
        // Función para eliminar un rol
        function DeleteRol(id) {
            $.ajax({
                type: "POST",
                url: "WFRol.aspx/DeleteRol",// Se invoca el WebMethod Eliminar un rol
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#RolTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Rol eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el Rol.");
                }
            });
        }
    </script>




</asp:Content>
