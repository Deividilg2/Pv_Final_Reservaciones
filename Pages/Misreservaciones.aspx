<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Misreservaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Misreservaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <!-- Colocamos todo dentro de un contenedor-->
    <div class="Contenedor">
        <h2>Mis reservaciónes</h2>
        <asp:Label ID="lblcliente" runat="server" Text=""></asp:Label>
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
                     <a href=<%# Eval("idReservacion", "Detalles.aspx?id={0}") + "&costoPorCadaAdulto=" + Eval("costoPorCadaAdulto") + "&costoPorCadaNinho=" + Eval("costoPorCadaNinho") %> >Consultar</a>
                 </ItemTemplate>
             </asp:TemplateField>

         </Columns>
            </asp:GridView>
    </div>
    
</asp:Content>
