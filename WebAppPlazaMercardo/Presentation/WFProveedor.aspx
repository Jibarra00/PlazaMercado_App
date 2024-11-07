<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFProveedor.aspx.cs" Inherits="Presentation.WFProveedor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <%--nit--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nit"></asp:Label>
    <asp:TextBox ID="TBNit" runat="server"></asp:TextBox>
    <br />
    <%--nombre--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese el nombre"></asp:Label>
    <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
    <br />
    <%--botones de guardar y actualizra--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--lista de roles--%>
    <asp:GridView ID="GVProveedor" runat="server"></asp:GridView>



</asp:Content>
