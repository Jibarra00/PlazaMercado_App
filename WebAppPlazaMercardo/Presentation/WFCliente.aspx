<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCliente.aspx.cs" Inherits="Presentation.WFCliente" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%-- Estilos--%>

<link href="resources/css/datatables.min.css" rel="stylesheet" />
    </asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form>

        <%-- Id --%>
        <asp:HiddenField ID="HFClientID" runat="server" />

        <%--Nombre del Cliente--%>
        <asp:Label ID="Lbl" runat="server" Text="Ingrese el Nombre"></asp:Label>
        <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
        <br />
        <%--Apellido del cliente--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese el Apellido"></asp:Label>
        <asp:TextBox ID="TBlastname" runat="server"></asp:TextBox>
        <br />
        <%-- Correo del cliente--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese el Correo"></asp:Label>
        <asp:TextBox ID="TBMail" runat="server"></asp:TextBox>
        <br />
        <%-- Telefono del cliente--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese el Telefono/Celular"></asp:Label>
        <asp:TextBox ID="TBPhone" runat="server"></asp:TextBox>
        <br />
        <%--Direccion del cliente--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese la Direccion "></asp:Label>
        <asp:TextBox ID="TBAddres" runat="server"></asp:TextBox>
        <br />

        <%--Botones del Producto Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>
        
    <%--Lista de Clientes--%>

    <h2>Lista de Clientes</h2>
    <table id="clientsTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Nombres</th>
                <th>Apellidos</th>
                <th>Correo</th>
                <th>Telefono</th>
                <th>Direccion</th>

            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>
    
    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Clientes--%>
<script type="text/javascript">
    $(document).ready(function () {
        $('#clientsTable').DataTable({
            "processing": true,
            "serverSide": false,
            "ajax": {
                "url": "WFCliente.aspx/ListClients",// Se invoca el WebMethod Listar Productos
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
                { "data": "ClientID" },
                { "data": "Name" },
                { "data": "lastName" },
                { "data": "Mail" },
                { "data": "Phone" },
                { "data": "Addres" },
                {
                    "data": null,
                    "render": function (data, type, row) {
                        return `<button class="edit-btn" data-id="${row.ClientID}">Editar</button>
                         <button class="delete-btn" data-id="${row.ClientID}">Eliminar</button>`;
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
        $('#clientsTable').on('click', '.edit-btn', function () {
            //const id = $(this).data('id');
            const rowData = $('#clientsTable').DataTable().row($(this).parents('tr')).data();
            //alert(JSON.stringify(rowData, null, 2));
            loadClientData(rowData);
        });

        // Eliminar un producto
        $('#clientsTable').on('click', '.delete-btn', function () {
            const id = $(this).data('id');// Obtener el ID del producto
            if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                deleteClient(id);// Invoca a la función para eliminar el producto
            }
        });
    });

    // Cargar los datos en los TextBox y DDL para actualizar
    function loadClientData(rowData) {
        $('#<%= HFClientID.ClientID %>').val(rowData.ClientID);
        $('#<%= TBName.ClientID %>').val(rowData.Name);
        $('#<%= TBlastname.ClientID %>').val(rowData.lastName);
        $('#<%= TBMail.ClientID %>').val(rowData.Mail);
        $('#<%= TBPhone.ClientID %>').val(rowData.Phone);
        $('#<%= TBAddres.ClientID %>').val(rowData.Addres);
    }
    // Función para eliminar un producto
    function deleteClient(id) {
        $.ajax({
            type: "POST",
            url: "WFCliente.aspx/DeleteClient",// Se invoca el WebMethod Eliminar un Producto
            contentType: "application/json; charset=utf-8",
            data: JSON.stringify({ id: id }),
            success: function (response) {
                $('#clientsTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                alert("CLiente eliminado exitosamente.");
            },
            error: function () {
                alert("Error al eliminar el Cliente.");
            }
        });
    }



</script>
</asp:Content>
