<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearReservacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.CrearReservacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <div>
        <h2>Crear reservación </h2>
    </div>
    <div>
        <br />
        <asp:Label ID="lblHotel" runat="server" Text="Hotel" CssClass="form-label" Font-Bold="True"></asp:Label><br />
        <asp:DropDownList ID="ddlHoteles" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
        <asp:RequiredFieldValidator ID="rfvHoteles" runat="server" ErrorMessage="El hotel es necesario para el registro" 
            CssClass="text-danger" ControlToValidate="ddlHoteles"></asp:RequiredFieldValidator>
    </div>
    <div>
    <asp:Label ID="lblClientes" runat="server" Text="Cliente" CssClass="form-label" Font-Bold="True"></asp:Label><br />
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True" CssClass="form-control"></asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="lblFechaEntrada" runat="server" Text="Fecha de entrada" CssClass="form-label" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtFechaEntrada" runat="server" TextMode="Date" placeholder="dd/MM/yyyy" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFechaEntrada" ControlToValidate="txtFechaEntrada" runat="server" 
            ErrorMessage="La fecha de entrada es necesaria para el registro" CssClass="text-danger"></asp:RequiredFieldValidator><br />
        <asp:CustomValidator ID="cvFechaEntrada" runat="server" ControlToValidate="txtFechaEntrada" 
            OnServerValidate="cvFechaEntrada_ServerValidate" ErrorMessage="No se permiten fechas menores a la actual" CssClass="text-danger"></asp:CustomValidator>
        <br />
        <asp:Label ID="lblFechaSalida" runat="server" Text="Fecha de salida" CssClass="form-label" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtFechaSalida" runat="server" TextMode="Date" placeholder="dd/MM/yyyy" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFechaSalida" ControlToValidate="txtFechaSalida" runat="server" 
            ErrorMessage="La fecha de salida es necesaria para el registro" CssClass="text-danger"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="cvFechaSalida" runat="server" ControlToValidate="txtFechaSalida" 
            OnServerValidate="cvFechaSalida_ServerValidate"
            ErrorMessage="La fecha de salida debe ser mayor o igual a la fecha de entrada" CssClass="text-danger"></asp:CustomValidator>
    </div>
    <div>
        <asp:Label ID="lblMensajeCapacidad" runat="server" Text="" CssClass="form-label"></asp:Label><br />
        <asp:Label ID="lblNadultos" runat="server" Text="Número de adultos" CssClass="form-label" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtNadultos" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNadultos" ControlToValidate="txtNadultos" runat="server" 
            ErrorMessage="Este valor es requerido" CssClass="text-danger"></asp:RequiredFieldValidator><br /> 
        <asp:RangeValidator ID="rvNadultos" ControlToValidate="txtNadultos" runat="server"
            MaximumValue="8" MinimumValue="1" ErrorMessage="Se necesita mínimo 1 adulto" CssClass="text-danger"></asp:RangeValidator>
        <br /> 
        <asp:Label ID="lblNnihos" runat="server" Text="Número de niños" CssClass="form-label" Font-Bold="True"></asp:Label>
        <asp:TextBox ID="txtNnihos" TextMode="Number" runat="server" CssClass="form-control"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNnihos" runat="server" ErrorMessage="Este valor es requerido"
            ControlToValidate="txtNnihos" CssClass="text-danger"></asp:RequiredFieldValidator><br />
        <asp:RangeValidator ID="rvNnihos" ControlToValidate="txtNnihos" runat="server" MaximumValue="7" MinimumValue="0" 
            ErrorMessage="El número ingresado no puede ser procesado" CssClass="text-danger"></asp:RangeValidator>
    </div>
    <div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" CssClass="btn btn-primary" />
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" CausesValidation="false" CssClass="btn btn-secondary" />
    </div>


</asp:Content>
