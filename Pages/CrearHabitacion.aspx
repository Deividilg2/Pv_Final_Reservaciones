<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearHabitacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.CrearHabitacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Crear habitación</h1>

    <div>

        <br />
        <asp:Label ID="lblHotel" runat="server" Text="Hotel"></asp:Label>
        <asp:DropDownList ID="ddlHoteles" runat="server" AutoPostBack="true" CssClass="form form-control" OnSelectedIndexChanged="ddlHoteles_SelectedIndexChanged"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="rqHotele" runat="server" ErrorMessage="Campo Obligatorio" 
            ControlToValidate="ddlHoteles"></asp:RequiredFieldValidator>
        <br />
        <asp:Label ID="lblNumHabitacion" runat="server" Text="Número de Habitación"></asp:Label>
        <br />
        <asp:TextBox ID="txtNumHabitacion" runat="server" MaxLength="10"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rqNumHabitacion" runat="server" ErrorMessage="Campo Obligatorio" 
            ControlToValidate="txtNumHabitacion"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="lblCapacidadMax" runat="server" Text="Capacidad Máxima"></asp:Label>
        <br />
        <asp:TextBox ID="txtCapacidadMax" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rqCapacidadMax" runat="server" ErrorMessage="Campo Obligatorio"
            ControlToValidate="txtCapacidadMax"></asp:RequiredFieldValidator>
        <br />
        <br />
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción"></asp:Label>
        <br />
        <asp:TextBox ID="txtDescripcion" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rqDescripcion" runat="server" ErrorMessage="Campo Obligatorio" 
            ControlToValidate="txtDescripcion"></asp:RequiredFieldValidator>
        <br />
        <br />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />

    &nbsp;&nbsp;&nbsp;

    <a href="ListaHabitaciones.aspx" class="btn btn-secondary" >Regresar</a>


</asp:Content>
