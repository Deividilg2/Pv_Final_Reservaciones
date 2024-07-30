<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Detalles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Colocamos un detailsview que es parecido al Gridview-->
    <asp:DetailsView ID="dvDetalles" runat="server" AutoGenerateRows="False">
    <Fields>
        <asp:BoundField DataField="idReservacion" HeaderText="# reservación" />
        <asp:BoundField DataField="nombre" HeaderText="Hotel" />
        <%--¿porque el idHabitacion esta funcionando como numero de habitacion?--%>
        <%--<asp:BoundField DataField="idHabitacion" HeaderText="Número habitación?" />--%>
        <asp:BoundField DataField="numeroHabitacion" HeaderText="Número habitación" />
        <asp:BoundField DataField="nombreCompleto" HeaderText="Cliente" />
        <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha de entrada" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="fechaSalida" HeaderText="Fecha de salida" DataFormatString="{0:dd/MM/yyyy}" />
        <asp:BoundField DataField="totalDiasReservacion" HeaderText="Dias de la reserva" />
        <asp:BoundField DataField="numeroAdultos" HeaderText="Número de adultos" />
        <asp:BoundField DataField="numeroNinhos" HeaderText="Número de niños" />
        <asp:BoundField DataField="costoTotal" HeaderText="Costo total" />
        
        </Fields>
</asp:DetailsView>
    
    <div>
       <%-- <a href="ModificarReservacion.aspx?idReservacion=<%# Eval("idReservacion")%>" >Editar reservación</a>--%>
        <asp:LinkButton ID="lnkEditar" runat="server" Text="Editar reservación"  OnClick="lnkEditar_Click"/>
        <asp:Button ID="btncancelar" runat="server" Text="Cancelar reservación" OnClick="btncancelar_Click" />
        <asp:Button ID="btnregresar" runat="server" Text="Regresar" OnClick="btnregresar_Click" />

    </div>

    <div>
        <h2>Lista de acciones realizadas</h2>
        <!--Tabla de acciones -->
        <asp:GridView ID="grdacciones" runat="server" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField datafield="fechaDeLaAccion" DataFormatString="{0:dd/MM/yyyy HH:mm}" HeaderText="Fecha"/>
                <asp:BoundField datafield="accionRealizada" HeaderText="Acción"/>
                <asp:BoundField datafield="nombreCompleto" HeaderText="Realizada por"/>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
