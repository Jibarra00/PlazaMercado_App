<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFProducto.aspx.cs" Inherits="Presentation.WFProducto" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <asp:Button ID="BTnUpdate" runat="server" Text="Actializar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de productos--%>
    <div>
        <asp:GridView ID="GVProductos" runat="server"></asp:GridView>
    </div>


</asp:Content>