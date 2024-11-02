<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFUsuario.aspx.cs" Inherits="Presentation.WFUsuario" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <%--EMAIL--%>
    <asp:Label ID="Label1" runat="server" Text="Ingrese el correo electronico "></asp:Label>
    <asp:TextBox ID="TBEmail" runat="server"></asp:TextBox>
    <br />
    <%--CONTRASEÑA--%>
    <asp:Label ID="Label2" runat="server" Text="Ingrese la contraseña"></asp:Label>
    <asp:TextBox ID="TBPassword" runat="server"></asp:TextBox>
    <br />
    <%--SALT--%>
    <asp:Label ID="Label3" runat="server" Text="Escriba el salt"></asp:Label>
    <asp:TextBox ID="TBSalt" runat="server"></asp:TextBox>
    <br />
    <%--STATE--%>
    <asp:Label ID="Label4" runat="server" Text="Escriba el estado"></asp:Label>
    <asp:TextBox ID="TBState" runat="server"></asp:TextBox>
    <br />
    <%--FECHA--%>
    <asp:Label ID="Label5" runat="server" Text="Ingrese la fecha (AAAA-MM-DD)"></asp:Label>
    <asp:TextBox ID="TBDate" runat="server"></asp:TextBox>
    <br />
    <%--ROL--%>
    <asp:Label ID="Label6" runat="server" Text="Seleccione el rol"></asp:Label>
    <asp:DropDownList ID="DDLRol" runat="server"></asp:DropDownList>
    <br />
    <%--EMPLEADO--%>
    <asp:Label ID="Label7" runat="server" Text="Seleccione el empleado"></asp:Label>
    <asp:DropDownList ID="DDLEmpleado" runat="server"></asp:DropDownList>
    <br />
    <%--CLIENTE--%>
    <asp:Label ID="Label8" runat="server" Text="Seleccione el cliente"></asp:Label>
    <asp:DropDownList ID="DDLCliente" runat="server"></asp:DropDownList>
    <br />

    <%--BOTON DE GUARDAR Y ACTUALIZAR--%>
    <div>
        <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
        <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" />
        <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
    </div>
    <br />
    <%--LISTA DE USUARIOS--%>
    <div>
        <asp:GridView ID="GVUsuarios" runat="server"></asp:GridView>
    </div>
</asp:Content>
