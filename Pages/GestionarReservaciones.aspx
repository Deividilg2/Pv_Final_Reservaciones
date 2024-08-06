<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="GestionarReservaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.GestionarReservaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>
    .text-left {
        text-align: left;
    }

    .text-center {
        text-align: center;
    }

    .text-right {
        text-align: right;
    }

    .font-weight-bold {
        font-weight: bold;
    }

    .table-striped tbody tr:nth-of-type(odd) {
        background-color: rgba(0,0,0,.05);
    }

    .form-group {
        margin-bottom: 15px;
    }

    .btn {
        margin-right: 10px;
    }

    .container {
        max-width: 1100px;
    }

    h1 {
        margin-bottom: 20px;
    }

</style>


     <div class="Contenedor">
     <h1>Gestionar reservaciones</h1>
         <!--Colocamos el filtrado correspondiente para nuestro empleado-->
         <!--Es necesario tener el AutoPostBack en true porque de lo contrario la página no puede cargar y realizar la consulta -->
         <asp:DropDownList ID="ddlClientes" runat="server" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True" ></asp:DropDownList>
         <asp:TextBox ID="txtFechaEntrada" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
         <asp:TextBox ID="txtFechaSalida" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
         &nbsp;
         <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn btn-secondary"/>
         <br />
         <asp:CustomValidator ID="cvFechaSalida" runat="server" ControlToValidate="txtFechaSalida" OnServerValidate="cvFechaSalida_ServerValidate" ErrorMessage="La fecha de salida debe ser mayor o igual a la fecha de entrada"></asp:CustomValidator>
         <br />
         

         <asp:Button ID="btnNuevareservacion" runat="server" Text="Nueva reservación" OnClick="btnNuevareservacion_Click" CssClass="btn btn-primary"/>
         <!--Colocamos las columnas respectivas de la tabla-->
         <br />
         <br />
     <asp:GridView ID="grdReservaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
      <Columns>
          
          <asp:BoundField DataField="idReservacion" HeaderText="# Reservación" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:BoundField DataField="nombreCompleto" HeaderText="Cliente" HeaderStyle-CssClass="text-left font-weight-bold" ItemStyle-CssClass="text-left"/>
          <asp:BoundField DataField="nombre" HeaderText="Hotel" HeaderStyle-CssClass="text-left font-weight-bold" ItemStyle-CssClass="text-left"/>
          <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha Entrada" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:BoundField DataField="fechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:BoundField DataField="costoTotal" HeaderText="Costo" DataFormatString="{0:$#,##0.00}" HeaderStyle-CssClass="text-right font-weight-bold" ItemStyle-CssClass="text-right"/>
          <asp:BoundField DataField="estado" HeaderText="Estado" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:TemplateField>
              <ItemTemplate>
                  <a href="Detalles.aspx?id=<%# Eval("idReservacion")%>" class="btn btn-info btn-sm">Consultar</a>
              </ItemTemplate>
          </asp:TemplateField>

      </Columns>
         </asp:GridView>
<!--Mandamos mediante la url el id para Consultar o modificar los detalles de las reservaciónes-->
 </div>

</asp:Content>
