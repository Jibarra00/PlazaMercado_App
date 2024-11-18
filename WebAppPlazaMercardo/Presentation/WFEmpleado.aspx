<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFEmpleado.aspx.cs" Inherits="Presentation.WFEmpleado" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <%--Estilos--%>
    <link href="resources/css/datatables.min.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <form>
        <%--Id--%>
        <asp:HiddenField ID="HFEmployeeID" runat="server" />

        <%--Identificacion--%>
        <asp:Label ID="Label1" runat="server" Text="Ingrese su identificación"></asp:Label>
        <asp:TextBox ID="TBCC" runat="server"></asp:TextBox>
        <br />
        <%--Nombre--%>
        <asp:Label ID="Label2" runat="server" Text="Ingrese su Nombre"></asp:Label>
        <asp:TextBox ID="TBname" runat="server"></asp:TextBox>
        <br />
        <%--Apellido--%>
        <asp:Label ID="Label3" runat="server" Text="Ingrese su apellido"></asp:Label>
        <asp:TextBox ID="TBlastname" runat="server"></asp:TextBox>
        <br />
        <%--Telefono--%>
        <asp:Label ID="Label4" runat="server" Text="Ingrese su Numero de Telefono/Celular"></asp:Label>
        <asp:TextBox ID="TBphone" runat="server"></asp:TextBox>
        <br />
        <%--Proveedor--%>
        <asp:Label ID="Label5" runat="server" Text="Ingrese su direccion"></asp:Label>
        <asp:TextBox ID="TBaddres" runat="server"></asp:TextBox>
        <br />
        
        <%--Botones Guardar y Actualizar--%>
        <div>
            <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
            <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
            <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        </div>
        <br />
    </form>


    <%--Lista de Empleados--%>
    <h2>Lista de Empleados</h2>
    <table id="employeesTable" class="display" style="width: 100%">
        <thead>
            <tr>
                <th>ID</th>
                <th>Identificacion</th>
                <th>Nombre</th>
                <th>Apellido</th>
                <th>Telefono</th>
                <th>Direccion</th>
               
            </tr>
        </thead>
        <tbody>
        </tbody>
    </table>

    <script src="resources/js/datatables.min.js" type="text/javascript"></script>

    <%--Empleados--%>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#employeesTable').DataTable({
                "processing": true,
                "serverSide": false,
                "ajax": {
                    "url": "WFEmpleado.aspx/ListEmployees",
                    "type": "POST",
                    "contentType": "application/json; charset=utf-8",
                    "dataType": "json",
                    "dataSrc": function (json) {
                        if (json.d.success) {
                            return json.d.data;
                        } else {
                            alert(json.d.message); // Mostrar mensaje de error si hay problemas
                            return [];
                        }
                    }
                },
                "columns": [
                    { "data": "EmployeeID" },
                    { "data": "Identification" },
                    { "data": "Name" },
                    { "data": "lastName" },
                    { "data": "Phone" },
                    { "data": "Addres" },
                    {
                        "data": null,
                        "render": function (data, type, row) {
                            return `<button class="edit-btn" data-id="${row.EmployeeID}">Editar</button>
                            <button class="delete-btn" data-id="${row.EmployeeID}">Eliminar</button>`;
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
        });


            // Editar un producto
            $('#employeesTable').on('click', '.edit-btn', function () {
                //const id = $(this).data('id');
                const rowData = $('#employeesTable').DataTable().row($(this).parents('tr')).data();
                //alert(JSON.stringify(rowData, null, 2));
                loadEmployeeData(rowData);
            });

            // Eliminar un producto
            $('#employeesTable').on('click', '.delete-btn', function () {
                const id = $(this).data('id');// Obtener el ID del producto
                if (confirm("¿Estás seguro de que deseas eliminar este producto?")) {
                    deleteEmployee(id);// Invoca a la función para eliminar el producto
                }
            });
        

        // Cargar los datos en los TextBox y DDL para actualizar
        function loadEmployeeData(rowData) {
            $('#<%= HFEmployeeID.ClientID %>').val(rowData.EmployeeID);
            $('#<%= TBCC.ClientID %>').val(rowData.Identification);
            $('#<%= TBname.ClientID %>').val(rowData.Name);
            $('#<%= TBlastname.ClientID %>').val(rowData.lastName);
          $('#<%= TBphone.ClientID %>').val(rowData.Phone);
          $('#<%= TBaddres.ClientID %>').val(rowData.Addres);
        }

        // Función para eliminar un producto
        function deleteEmployee(id) {
            $.ajax({
                type: "POST",
                url: "WFEmpleado.aspx/DeleteEmployee",// Se invoca el WebMethod Eliminar un Producto
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ id: id }),
                success: function (response) {
                    $('#employeesTable').DataTable().ajax.reload();// Recargar la tabla después de eliminar
                    alert("Producto eliminado exitosamente.");
                },
                error: function () {
                    alert("Error al eliminar el producto.");
                }
            });
        }
    </script>
</asp:Content>
