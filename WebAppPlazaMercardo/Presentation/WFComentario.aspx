<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFComentario.aspx.cs" Inherits="Presentation.WFComentario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos --%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form id="FrmComentario" runat="server">

        <%-- Id --%>
        <asp:HiddenField ID="HFCommentID" runat="server" />
        <br />
        <%--Texto--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el texto"></asp:Label>
        <asp:TextBox ID="TBText" runat="server"></asp:TextBox>
        <br />
        <%-- Fecha --%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la fecha"></asp:Label>
        <asp:TextBox ID="TBDate" runat="server"></asp:TextBox>
        <br />
        <%-- Clasificación --%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese la clasificación"></asp:Label>
        <asp:TextBox ID="TBClassification" runat="server"></asp:TextBox>
        <br />
        <%-- Cliente --%>
        <asp:Label ID="Label4" runat="server" Text="Seleccione el cliente"></asp:Label>
        <asp:DropDownList ID="DDLClient" runat="server"></asp:DropDownList>
        <br />
        <%-- Producto --%>
        <asp:Label ID="Label5" runat="server" Text="Seleccione el producto"></asp:Label>
        <asp:DropDownList ID="DDLProducto" runat="server"></asp:DropDownList>
        <br />

        <%-- Boton guardar --%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
    </form>
    <asp:Panel ID="PanelAdmin" runat="server">

        <h2>Lista de Comentarios</h2>
        <table id="commentsTable" class="display" style="width: 100%">
            <thead>
                <tr>
                    <th>ID</th>
                    <th>Texto</th>
                    <th>Fecha</th>
                    <th>Clasificación</th>
                    <th>Cliente</th>
                    <th>Producto</th>
                </tr>
            </thead>
            <tbody>
            </tbody>
        </table>
    </asp:Panel>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Comentarios--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#commentsTable').DataTable({
                
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFComentario.aspx/ListComments",// Se invoca el WebMethod Listar Productos
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
                    { "data": "CommentID" },
                    { "data": "Text" },
                    { "data": "Date" },
                    { "data": "Classification" },
                    { "data": "Client" },
                    { "data": "Product" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.CommentID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.CommentID}">Eliminar</button>`;
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
            $('#commentsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#commentsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadCommentData(rowData);
            });

            // Eliminar un producto
            $('#commentsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID de la categoria
                if (confirm("¿Estás seguro de que deseas eliminar este comentario?")) {
                    deleteComment(id);// Invoca a la función para eliminar el producto
                }
            });
        });
        // Cargar los datos en los TextBox y DDL para actualizar
        function loadCommentData(rowData) {
            $('#<%= HFCommentID.ClientID %>').val(rowData.CommentID);
            $('#<%= TBText.ClientID %>').val(rowData.Text);
            $('#<%= TBClassification.ClientID %>').val(rowData.Classification);
            $('#<%= TBDate.ClientID %>').val(rowData.Date);
            $('#<%= DDLClient.ClientID %>').val(rowData.Client);
            $('#<%= DDLProducto.ClientID %>').val(rowData.Product);
        }
        // Función para eliminar un producto
        function deleteComment(id) {
            $.ajax({
                type: "POST",
                url: "WFComentario.aspx/DeleteComment",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#commentsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Comentario eliminada exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el comentario.");
                }
            });
        }
    </script>
</asp:Content>
