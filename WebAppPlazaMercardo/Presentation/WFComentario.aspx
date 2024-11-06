<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFComentario.aspx.cs" Inherits="Presentation.WFComentario" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server"></asp:TextBox>
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

    <%-- Comentario --%>
    <asp:Label ID="Label6" runat="server" Text="Seleccione el comentario"></asp:Label>
    <asp:DropDownList ID="DDLComment" runat="server"></asp:DropDownList>

    <%-- Boton guardar --%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>

    <%-- Lista de comentarios --%>
    <div>
        <asp:GridView ID="GVComment" runat="server"></asp:GridView>
    </div>
</asp:Content>
