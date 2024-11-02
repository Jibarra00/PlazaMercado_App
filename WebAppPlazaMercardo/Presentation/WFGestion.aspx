<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFGestion.aspx.cs" Inherits="Presentation.WFGestion" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
    <%-- Fecha --%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la fecha"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server"></asp:TextBox>
    <br />
    <%--Descripción--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descripción"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server"></asp:TextBox>
    <br />
    <%-- Empleado --%>
    <asp:Label ID="Label3" runat="server" Text="Seleccione el empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmployee" runat="server"></asp:DropDownList>
    <br />
    <%-- Producto --%>
    <asp:Label ID="Label4" runat="server" Text="Seleccione el producto"></asp:Label>
    <asp:DropDownList ID="DDLProduct" runat="server"></asp:DropDownList>

    <%-- Boton guardar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%-- Lista de gestiones --%>
    <div>
        <asp:GridView ID="GVManagement" runat="server"></asp:GridView>
    </div>
</asp:Content>
