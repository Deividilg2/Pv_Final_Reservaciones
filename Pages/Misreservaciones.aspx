<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Misreservaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Misreservaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

     <!-- Colocamos todo dentro de un contenedor-->
    <div class="Contenedor">
        <h2>Mis reservaciónes</h2>
        <a href="#">Nueva reservación</a>
        <!--Colocamos las columnas respectivas de la tabla-->
        <!--Pasamos datos que necesitamos para detalles por medio de URL-->
        <asp:GridView ID="grdMisreservaciones" runat="server" AutoGenerateColumns="False">
         <Columns>
             <asp:BoundField DataField="idReservacion" HeaderText="# reservacion"/>
             <asp:BoundField DataField="nombre" HeaderText="Hotel"/>
             <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha Entrada"/>
             <asp:BoundField DataField="fechaSalida" HeaderText="Fecha Salida"/>
             <asp:BoundField DataField="costoTotal" HeaderText="Costo"/>
             <asp:BoundField DataField="estado" HeaderText="Estado"/>
             <asp:TemplateField>
                 <ItemTemplate>
                     <a href="Detalles.aspx?id=<%# Eval("idReservacion")%>">consultar</a>
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
            </asp:GridView>
        <!--Mandamos mediante la url el id para Consultar o modificar los detalles de las reservaciónes-->
    </div>

</asp:Content>
