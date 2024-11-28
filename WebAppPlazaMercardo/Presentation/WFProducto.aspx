<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFProducto.aspx.cs" Inherits="Presentation.WFProducto" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--formulario--%>
    <form id="FrmProducto" runat="server">
        <%--ID--%>
        <asp:HiddenField ID="HFProductoID" runat="server" />
        <br />
        <%--codigo--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el codigo"></asp:Label>
        <asp:TextBox ID="TBCodigo" runat="server"></asp:TextBox>
        <br />
        <%--Descripcion--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
        <asp:TextBox ID="TBDescripcion" runat="server"></asp:TextBox>
        <br />
        <%--Catidad--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese la cantidad"></asp:Label>
        <asp:TextBox ID="TBCantidad" runat="server"></asp:TextBox>
        <br />
        <%--Precio--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese el precio"></asp:Label>
        <asp:TextBox ID="TBPrecio" runat="server"></asp:TextBox>
        <br />
        <%--Proveedor--%>
        <asp:Label ID="Label5" runat="server" Text="Seleccione el proveedor"></asp:Label>
        <asp:DropDownList ID="DDLProveedor" runat="server"></asp:DropDownList>
        <br />
        <%--Categorias--%>
        <asp:Label ID="Label6" runat="server" Text="Seleccione la categoria"></asp:Label>
        <asp:DropDownList ID="DDLCategoria" runat="server"></asp:DropDownList>
        <br />
        <%--Botones guardar y actualizar--%>
        <div>
            <asp:Button ID="BTnSave" runat="server" Text="Guardar" OnClick="BTnSave_Click" />
            <asp:Button ID="BTnUpdate" runat="server" Text="Actializar" OnClick="BTnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>


    <%--Lista de productos--%>
    <asp:Panel ID="PanelAdmin" runat="server">

    <h2>Lista de Productos</h2>
    <table id="productsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Codigo</th>
                <th>Descripcion</th>
                <th>Cantidad</th>
                <th>Precio</th>
                <th>FkCategoria</th>
                <th>Categoria</th>
                <th>FkProveedor</th>
                <th>Proveedor</th>
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    </asp:Panel>

    <%--DataTables--%>
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Productos--%>
    <script type="text/javascript">
        $(document).ready(function () {
            const showEditButton = '<%= _showEditButton %>' === 'True';
            const showDeleteButton = '<%= _showDeleteButton %>' === 'True';
            $('#productsTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFProducto.aspx/ListProducts",// Se invoca el WebMethod Listar Productos
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
                    { "data": "ProductID" },
                    { "data": "Code" },
                    { "data": "Description" },
                    { "data": "Quantity" },
                    { "data": "Price" },
                    { "data": "FkCategory", "visible": false },
                    { "data": "NameCategory" },
                    { "data": "FkProvider", "visible": false },
                    { "data": "NameProvider" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            let buttons = '';
                            if (showEditButton) {
                                buttons += `<button class="btn btn-info edit-btn" data-id="${row.ProductID}">Editar</button>`;
                            }
                            if (showDeleteButton) {
                                buttons += `<button class="btn btn-danger delete-btn" data-id="${row.ProductID}">Eliminar</button>`;
                            }
                            return buttons;
                        }
                    }
                ],
                "language": {
                    "lengthMenu": "Mostrar MENU registros por página",
                    "zeroRecords": "No se encontraron resultados",
                    "info": "Mostrando página PAGE de PAGES",
                    "infoEmpty": "No hay registros disponibles",
                    "infoFiltered": "(filtrado de MAX registros totales)",
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
            $('#productsTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#productsTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadProductData(rowData);
            });

            // Eliminar un producto
            $('#productsTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                    deleteProducto(id);// Invoca a la función para eliminar el producto
                }
            });
        });

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadProductData(rowData) {
            $('#<%= HFProductoID.ClientID %>').val(rowData.ProductID);
            $('#<%= TBCodigo.ClientID %>').val(rowData.Code);
            $('#<%= TBDescripcion.ClientID %>').val(rowData.Description);
            $('#<%= TBCantidad.ClientID %>').val(rowData.Quantity);
            $('#<%= TBPrecio.ClientID %>').val(rowData.Price);
            $('#<%= DDLProveedor.ClientID %>').val(rowData.FkProvider);
            $('#<%= DDLCategoria.ClientID %>').val(rowData.FkCategory);
        }

        // Función para eliminar un producto
        function deleteProduct(id) {
            $.ajax({
                type: "POST",
                url: "WFProducto.aspx/DeleteProduct",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#productsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Producto eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el producto.");
                }
            });
        }
    </script>
</asp:Content>