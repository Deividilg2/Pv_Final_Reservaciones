<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Misreservaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Misreservaciones" %>
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
<%--    Se debe verificar como aparece el Estado en las listas de Reservaciones--%>
     <!-- Colocamos todo dentro de un contenedor-->
    <div class="Contenedor">
        <h2>Mis reservaciones</h2>
        <asp:Button ID="btnNuevareservacion" runat="server" Text="Nueva reservación" OnClick="btnNuevareservacion_Click" CssClass="btn btn-primary" />
        <!--Colocamos las columnas respectivas de la tabla-->
        <!--Pasamos datos que necesitamos para detalles por medio de URL-->
        <br />
        <br />
        <asp:GridView ID="grdMisreservaciones" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered">
         <Columns>
             <asp:BoundField DataField="idReservacion" HeaderText="# Reservación" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
             <asp:BoundField DataField="nombre" HeaderText="Hotel" HeaderStyle-CssClass="text-left font-weight-bold" ItemStyle-CssClass="text-left"/>
             <asp:BoundField DataField="fechaEntrada" HeaderText="Fecha Entrada" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
             <asp:BoundField DataField="fechaSalida" HeaderText="Fecha Salida" DataFormatString="{0:dd/MM/yyyy}" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center"/>
             <asp:BoundField DataField="costoTotal" HeaderText="Costo" DataFormatString="{0:$#,##0.00}" HeaderStyle-CssClass="text-right font-weight-bold" ItemStyle-CssClass="text-right"/>

             <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="text-center font-weight-bold" ItemStyle-CssClass="text-center">
                <HeaderTemplate>Estado</HeaderTemplate>
             <ItemTemplate>
                 <%# ConvertEstado(Eval("estado").ToString(), Convert.ToDateTime(Eval("fechaEntrada")), Convert.ToDateTime(Eval("fechaSalida"))) %>
             </ItemTemplate>
             </asp:TemplateField>

             <asp:TemplateField ItemStyle-CssClass="text-center">
                 <ItemTemplate>
                     <a href="Detalles.aspx?id=<%# Eval("idReservacion")%>" class="btn btn-primary">Consultar</a>
                 </ItemTemplate>
             </asp:TemplateField>
         </Columns>
            </asp:GridView>
        <!--Mandamos mediante la url el id para Consultar o modificar los detalles de las reservaciónes-->
    </div>

</asp:Content>
