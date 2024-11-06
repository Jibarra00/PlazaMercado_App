<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPedidos.aspx.cs" Inherits="Presentation.WFPedidos" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <%--fecha--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha (YYYY-MM-DD)"></asp:Label>
    <asp:TextBox ID="TBFecha" runat="server"></asp:TextBox>
    <br />
    <%--estado--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el estado"></asp:Label>
    <asp:TextBox ID="TBEstado" runat="server"></asp:TextBox>
    <br />
    <%--especificacion--%>
    <asp:Label ID="Label3" runat="server" Text="Especifique el estado"></asp:Label>
    <asp:TextBox ID="TBEspecificacion" runat="server"></asp:TextBox>
    <br />
    <%--fkcliente--%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione el cliente"></asp:Label>
    <asp:DropDownList ID="DDLCliente" runat="server"></asp:DropDownList>
    <br />
    <%--fkproducto--%>
    <asp:Label ID="Label5" runat="server" Text="Seleccione el producto"></asp:Label>
    <asp:DropDownList ID="DDLProducto" runat="server"></asp:DropDownList>

    <%--BOTON DE GUARDAR Y ACTUALIZAR--%>
    <div>
        <asp:Button ID="BTSave" runat="server" Text="Guardar" OnClick="BTSave_Click" />
        <asp:Button ID="BTUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>

    <%--lista de pedidos--%>
    <div>
        <asp:GridView ID="GVPedidos" runat="server"></asp:GridView>
    </div>

</asp:Content>
