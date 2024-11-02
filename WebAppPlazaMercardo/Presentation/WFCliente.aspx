<%@ Page Title="" Language="C#" MasterPageFile="~/Main.Master" AutoEventWireup="true" CodeBehind="WFCliente.aspx.cs" Inherits="Presentation.WFCliente" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <%--Nombre del Cliente--%>
 <asp:Label ID="Lbl" runat="server" Text="Ingrese el Nombre"></asp:Label>
 <asp:TextBox ID="TBName" runat="server"></asp:TextBox>
 <%--Apellido del cliente--%>
 <asp:Label ID="Label1" runat="server" Text="Ingrese el Apellido"></asp:Label>
 <asp:TextBox ID="TBlastname" runat="server"></asp:TextBox>
 <br />
<%-- Correo del cliente--%>
 <asp:Label ID="Label2" runat="server" Text="Ingrese el Correo"></asp:Label>
 <asp:TextBox ID="TBMail" runat="server"></asp:TextBox>
 <br />
<%-- Telefono del cliente--%>
 <asp:Label ID="Label3" runat="server" Text="Ingrese el Telefono/Celular"></asp:Label>
 <asp:TextBox ID="TBPhone" runat="server"></asp:TextBox>
 <br />
 <%--Direccion del cliente--%>
 <asp:Label ID="Label4" runat="server" Text="Ingrese la Direccion "></asp:Label>
 <asp:TextBox ID="TBAddres" runat="server"></asp:TextBox>
 <br />
 
 <%--Botones del Producto Guardar y Actualizar--%>
 <div>  
     <asp:Button ID="BtnSave" runat="server" Text="Guardar" OnClick="BtnSave_Click" />
     <asp:Button ID="BtnUpdate" runat="server" Text="Actualizar" OnClick="BtnUpdate_Click" />
     <asp:Label ID="LblMsg" runat="server" Text=""></asp:Label>
 </div>
 <br />
 <%--Lista de Productos--%>
 <div> 
     <asp:GridView ID="GVClients" runat="server"></asp:GridView>

 </div>
</asp:Content>
