<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarReservaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.GestionarReservaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <div class="Contenedor">
     <h1>Gestionar reservaciónes</h1>
         <!--Colocamos el filtrado correspondiente para nuestro empleado-->
         <!--Es necesario tener el AutoPostBack en true porque de lo contrario la página no puede cargar y realizar la consulta -->
         <asp:DropDownList ID="ddlClientes" runat="server" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="true" ></asp:DropDownList>
         <asp:TextBox ID="txtFechaEntrada" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
         <asp:TextBox ID="txtFechaSalida" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
         <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click"/>

         <br />

     <a href="#">Nueva reservación</a>
         <!--Colocamos las columnas respectivas de la tabla-->
     <asp:GridView ID="grdReservaciones" runat="server" AutoGenerateColumns="False">
      <Columns>
          
          <asp:BoundField DataField="idReservacion" HeaderText="# reservacion"/>
          <asp:BoundField DataField="nombreCompleto" HeaderText="Cliente"/>
          <asp:BoundField DataField="nombre" HeaderText="Hotel"/>
          <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha Entrada"/>
          <asp:BoundField DataField="fechaSalida" HeaderText="Fecha Salida"/>
          <asp:BoundField DataField="costoTotal" HeaderText="Costo"/>
          <asp:BoundField DataField="estado" HeaderText="Estado"/>
          <asp:CommandField SelectText="consultar" ShowSelectButton="true"/>

      </Columns>
         </asp:GridView>
 </div>

</asp:Content>
