<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detalles.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Detalles" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <!--Colocamos un detailsview que es parecido al Gridview-->
    <asp:DetailsView ID="dvDetalles" runat="server" AutoGenerateRows="False">
    <Fields>
        <asp:BoundField DataField="idReservacion" HeaderText="# reservación" />
        <asp:BoundField DataField="nombre" HeaderText="Hotel" />
        <asp:BoundField DataField="idHabitacion" HeaderText="Número habitación" />
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
        <a href="#" >Editar reservación</a>
        <asp:Button ID="btncancelar" runat="server" Text="Cancelar reservación" />
        <asp:Button ID="btnregresar" runat="server" Text="Regresar" />

    </div>

    <div>
        <!--Tabla de acciones -->
        <asp:GridView ID="grdacciones" runat="server">
            <Columns>
                <asp:BoundField datafield="" HeaderText="Fecha"/>
                <asp:BoundField datafield="" HeaderText="Acción"/>
                <asp:BoundField datafield="" HeaderText="Realizada por"/>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
