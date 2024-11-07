<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFRol.aspx.cs" Inherits="Presentation.WFRol" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <%--nombre--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el nombre"></asp:Label>
    <asp:TextBox ID="TBNombre" runat="server"></asp:TextBox>
    <br />
   <%--descripción--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la descrición"></asp:Label>
    <asp:TextBox ID="TBdescripcion" runat="server"></asp:TextBox>
    <br />

    <%--botones de guardar y actualizar--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
        <br />
    </div>
    <%--listar roles--%>
    <div>
        <asp:GridView ID="GVRol" runat="server"></asp:GridView>
    </div>

   
</asp:Content>
