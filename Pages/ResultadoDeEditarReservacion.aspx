<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ResultadoDeEditarReservacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.ResultadoDeEditarReservacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

        <h1>Se ha completado el proceso</h1>

<div class="alert alert-success">
    Se ha editado la información de una reservación exitosamente.
</div>

    <br />

<%--<a class="btn btn-success" href="ListaProvincias.aspx">Regresar</a>--%>
    <asp:LinkButton ID="lnkbtnRegresar" runat="server" CssClass="btn btn-secondary" OnClick="lnkbtnRegresar_Click">Regresar</asp:LinkButton>

</asp:Content>
