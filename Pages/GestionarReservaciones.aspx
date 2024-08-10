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

    .textbox-date {
        padding: 10px;
        font-size: 20px;
        border-radius: 5px;
        border: 1px solid #ccc;
        box-shadow: inset 0 1px 3px rgba(0, 0, 0, 0.1);
        background-color: #f9f9f9;
        width: 100%;
        max-width: 250px;
    }

    .textbox-date:hover {
        border-color: #007bff;
    }

    .textbox-date:focus {
        outline: none;
        border-color: #007bff;
        box-shadow: 0 0 8px rgba(0, 123, 255, 0.25);
    }

    .dropdown-custom {
    padding: 10px;
    font-size: 20px;
    border-radius: 5px;
    border: 1px solid #ccc;
    background-color: #f9f9f9;
    width: 100%;
    max-width: 250px;
    appearance: none; /* Oculta el estilo por defecto */
    -webkit-appearance: none;
    -moz-appearance: none;
    /*Añade una pequeña flecha como ícono para la lista desplegable*/
    background-image: url('data:image/svg+xml;charset=US-ASCII,<svg xmlns="http://www.w3.org/2000/svg" width="14" height="8"><path fill="%23666" d="M7 8L0 0h14z"/></svg>');
    background-repeat: no-repeat;
    background-position: right 10px center;
    background-size: 10px 10px;
}

.dropdown-custom:hover {
    border-color: #007bff;
}

.dropdown-custom:focus {
    outline: none;
    border-color: #007bff;
    box-shadow: 0 0 8px rgba(0, 123, 255, 0.25);
}


</style>


     <div class="Contenedor">
     <h1>Gestionar reservaciones</h1>
         <!--Colocamos el filtrado correspondiente para nuestro empleado-->
         <!--Es necesario tener el AutoPostBack en true porque de lo contrario la página no puede cargar y realizar la consulta -->
         <asp:DropDownList ID="ddlClientes" runat="server" OnSelectedIndexChanged="ddlClientes_SelectedIndexChanged" AutoPostBack="True" CssClass="dropdown-custom" ></asp:DropDownList>
         <asp:TextBox ID="txtFechaEntrada" runat="server" TextMode="Date" placeholder="Fecha Entrada" CssClass="textbox-date"></asp:TextBox>
         <asp:TextBox ID="txtFechaSalida" runat="server" TextMode="Date" placeholder="Fecha Salida" CssClass="textbox-date"></asp:TextBox>
         &nbsp;
         <asp:Button ID="btnFiltrar" runat="server" Text="Filtrar" OnClick="btnFiltrar_Click" CssClass="btn btn-secondary"/>
         <br />
         <asp:CustomValidator ID="cvFechaSalida" runat="server" ControlToValidate="txtFechaSalida" OnServerValidate="cvFechaSalida_ServerValidate" ErrorMessage="La fecha de salida debe ser mayor o igual a la fecha de entrada"></asp:CustomValidator>
         <br />
         

         <asp:Button ID="btnNuevareservacion" runat="server" Text="Nueva reservación" OnClick="btnNuevareservacion_Click" CssClass="btn btn-primary"/>
         <!--Colocamos las columnas respectivas de la tabla-->
         <br />
         <br />
         
     <asp:GridView ID="grdReservaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
      <Columns>
          
          <asp:BoundField DataField="idReservacion" HeaderText="# Reservación" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:BoundField DataField="nombreCompleto" HeaderText="Cliente" HeaderStyle-CssClass="text-left font-weight-bold" ItemStyle-CssClass="text-left"/>
          <asp:BoundField DataField="nombre" HeaderText="Hotel" HeaderStyle-CssClass="text-left font-weight-bold" ItemStyle-CssClass="text-left"/>
          <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha Entrada" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:BoundField DataField="fechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
          <asp:BoundField DataField="costoTotal" HeaderText="Costo" DataFormatString="{0:$#,##0.00}" HeaderStyle-CssClass="text-right font-weight-bold" ItemStyle-CssClass="text-right"/>

           <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="text-center font-weight-bold">
               <HeaderTemplate>Estado</HeaderTemplate>
            <ItemTemplate>
                <%# ConvertEstado(Eval("estado").ToString(), Convert.ToDateTime(Eval("fechaEntrada")), Convert.ToDateTime(Eval("fechaSalida"))) %>
            </ItemTemplate>
            </asp:TemplateField>

          <asp:TemplateField>
              <ItemTemplate>
                  <a href="Detalles.aspx?id=<%# Eval("idReservacion")%>" class="btn btn-primary">Consultar</a>
              </ItemTemplate>
          </asp:TemplateField>

      </Columns>
         </asp:GridView>
         
             <asp:Label ID="lblnulo" CssClass="alert alert-warning" Visible="false"  runat="server" Text="No se encontraron registros en ese rango de fechas"></asp:Label>
             

<!--Mandamos mediante la url el id para Consultar o modificar los detalles de las reservaciónes-->
 </div>

</asp:Content>
