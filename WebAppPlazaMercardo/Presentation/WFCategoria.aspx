<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCategoria.aspx.cs" Inherits="Presentation.WFCategoria" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:TextBox ID="TBId" runat="server" Visible="false"></asp:TextBox>
    <%--Descripción--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese la descripción de la categoría"></asp:Label>
    <asp:TextBox ID="TBDescription" runat="server" OnTextChanged="TBDescription_TextChanged"></asp:TextBox>
    <br />
    <%--Categoria--%>
    <asp:Label ID="Label6" runat="server" Text="Seleccione la categoría"></asp:Label>
    <asp:DropDownList ID="DDLCategory" runat="server"></asp:DropDownList>
    <br />
    <%--Botón guardar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--Lista de categorías--%>
    <div>
        <asp:GridView ID="GVCategories" runat="server"></asp:GridView>
    </div>


</asp:Content>
