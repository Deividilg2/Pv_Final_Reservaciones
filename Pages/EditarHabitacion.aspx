<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditarHabitacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.EditarHabitacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Editar habitación</h1>
    <asp:HiddenField ID="hdnID" runat="server" />

        <div>
       
            <asp:Label ID="lblHotel" runat="server" Text="Hotel" CssClass="form-label"></asp:Label>
            <br />
            <asp:TextBox ID="txtHotel" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>

            <br />
            <br />

        <asp:Label ID="lblNumHabitacion" runat="server" Text="Número de Habitación" CssClass="form-label"></asp:Label>
            <asp:Label ID="lblMensajeError" runat="server" Text="" CssClass="form-label"></asp:Label>
        <br />
        <%--Hay que revisar si es o no necesario usar alguna validacion especifica para datos alfanumericos, string deberia aceptar TODO--%>
        <asp:TextBox ID="txtNumHabitacion" runat="server" MaxLength="10" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rqNumHabitacion" runat="server" ErrorMessage="Campo Obligatorio" 
            ControlToValidate="txtNumHabitacion"></asp:RequiredFieldValidator>
        <asp:RegularExpressionValidator ID="revNumHabitacion" ValidationExpression="^[a-zA-Z0-9]+$" runat="server" ControlToValidate="txtNumHabitacion" ErrorMessage="Solo caracteres alfanuméricos"></asp:RegularExpressionValidator>
        <br />
        <br />
        <asp:Label ID="lblCapacidadMax" runat="server" Text="Capacidad Máxima" CssClass="form-label"></asp:Label>
        <br />
        <asp:TextBox ID="txtCapacidadMax" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rqCapacidadMax" runat="server" ErrorMessage="Campo Obligatorio"
            ControlToValidate="txtCapacidadMax"></asp:RequiredFieldValidator>
        <br />
        <asp:RangeValidator ID="rgvCapacidadMax" runat="server" ErrorMessage="Mínimo 1, Máximo 8, Solo se aceptan números enteros" 
            ControlToValidate="txtCapacidadMax" MinimumValue="1" MaximumValue="8" Type="Integer" ></asp:RangeValidator>
        <br />
        
        <asp:Label ID="lblDescripcion" runat="server" Text="Descripción" CssClass="form-label"></asp:Label>
        <br />
        <%--Hay que revisar si es o no necesario usar alguna validacion especifica para datos alfanumericos, string deberia aceptar TODO--%>
        <asp:TextBox ID="txtDescripcion" runat="server" MaxLength="500" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rqDescripcion" runat="server" ErrorMessage="Campo Obligatorio" 
            ControlToValidate="txtDescripcion"></asp:RequiredFieldValidator>
        <br />
        <br />
    </div>

    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />

    &nbsp;&nbsp;&nbsp;
    <%--Este botón no debe realizar ninguna validación de los elementos del formulario--%>
    <asp:Button ID="Inactivar" runat="server" Text="Inactivar" OnClick="Inactivar_Click" CausesValidation="false" CssClass="btn btn-warning" />

    &nbsp;&nbsp;&nbsp;

    <a href="ListaHabitaciones.aspx" class="btn btn-secondary" >Regresar</a>


</asp:Content>
