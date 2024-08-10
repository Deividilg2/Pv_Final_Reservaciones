<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Detalles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <!--Colocamos un Script para mandar una alerta de confirmación en caso de querer cancelar una reservación-->
    <script type="text/javascript">
        function confirmCancel() {
            return confirm('¿Estás seguro de que deseas cancelar?');
        }
    </script>



    <h1>Detalles de la Reservación</h1>
    <!--Colocamos un detailsview que es parecido al Gridview-->
    <asp:DetailsView ID="dvDetalles" runat="server" AutoGenerateRows="False" CssClass="table table-bordered table-striped">
    <Fields>
        <asp:BoundField DataField="idReservacion" HeaderText="# Reservación" HeaderStyle-Font-Bold="true"/>
        <asp:BoundField DataField="nombre" HeaderText="Hotel" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="numeroHabitacion" HeaderText="Número habitación" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="nombreCompleto" HeaderText="Cliente" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha de entrada" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="fechaSalida" HeaderText="Fecha de salida" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="totalDiasReservacion" HeaderText="Dias de la reserva" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="numeroAdultos" HeaderText="Número de adultos" HeaderStyle-Font-Bold="true" />
        <asp:BoundField DataField="numeroNinhos" HeaderText="Número de niños" HeaderStyle-Font-Bold="true"/>
        <asp:BoundField DataField="costoTotal" HeaderText="Costo total" DataFormatString="{0:$#,##0.00}" HeaderStyle-Font-Bold="true" />
        
        </Fields>
</asp:DetailsView>
    
    <br />
    
    <div>
       <%-- <a href="ModificarReservacion.aspx?idReservacion=<%# Eval("idReservacion")%>" >Editar reservación</a>--%>
        <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar reservación"  OnClick="lnkEditar_Click" CssClass="btn btn-primary"/>
        &nbsp;
        <asp:Button ID="btncancelar" runat="server" Text="Cancelar reservación" OnClientClick="return confirmCancel();" OnClick="btncancelar_Click" CssClass="btn btn-warning" />
        &nbsp;
        <asp:Button ID="btnregresar" runat="server" Text="Regresar" OnClick="btnregresar_Click" CssClass="btn btn-secondary"/>

    </div>

    <div>
        <h2>&nbsp;</h2>
        <h2>Lista de acciones realizadas</h2>
        <!--Tabla de acciones -->
        <asp:GridView ID="grdacciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped">
            <Columns>
                <asp:BoundField datafield="fechaDeLaAccion" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Fecha"/>
                <asp:BoundField datafield="accionRealizada" HeaderText="Acción"/>
                <asp:BoundField datafield="nombreCompleto" HeaderText="Realizada por"/>
            </Columns>
        </asp:GridView>
    </div>


   
</asp:Content>
