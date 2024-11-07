<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFPermiso.aspx.cs" Inherits="Presentation.WFPermiso" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
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
        <asp:Button ID="BTSave" runat="server" Text="Guardar" OnClick="BTSave_Click" />
        <asp:Button ID="BTUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsj" runat="server" Text=""></asp:Label>
    </div>
    <br />
<%--Lista de permisos--%>
    <div>
        <asp:GridView ID="GVPermisos" runat="server"></asp:GridView>

    </div>
</asp:Content>
