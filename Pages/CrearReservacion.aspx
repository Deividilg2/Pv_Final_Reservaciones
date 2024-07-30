<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CrearReservacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.CrearReservacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        <h2>Crear reservación </h2>
    </div>
    <div>
        <asp:Label ID="lblHotel" runat="server" Text="Hotel"></asp:Label><br />
        <asp:DropDownList ID="ddlHoteles" runat="server" AutoPostBack="True"></asp:DropDownList>
    </div>
    <div>
    <asp:Label ID="lblClientes" runat="server" Text="Cliente"></asp:Label><br />
    <asp:DropDownList ID="ddlClientes" runat="server" AutoPostBack="True"></asp:DropDownList>
    </div>
    <div>
        <asp:Label ID="lblFechaEntrada" runat="server" Text="Fecha de entrada"></asp:Label>
        <asp:TextBox ID="txtFechaEntrada" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFechaEntrada" ControlToValidate="txtFechaEntrada" runat="server" ErrorMessage="La fecha de entrada es necesaria para el registro"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="cvFechaEntrada" runat="server" ControlToValidate="txtFechaEntrada" OnServerValidate="cvFechaEntrada_ServerValidate" ErrorMessage="No se permiten fechas menores a la actual"></asp:CustomValidator>
        <br />
        <asp:Label ID="lblFechaSalida" runat="server" Text="Fecha de salida"></asp:Label>
        <asp:TextBox ID="txtFechaSalida" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFechaSalida" ControlToValidate="txtFechaSalida" runat="server" ErrorMessage="La fecha de salida es necesaria para el registro"></asp:RequiredFieldValidator>
        <asp:CustomValidator ID="cvFechaSalida" runat="server" ControlToValidate="txtFechaSalida" OnServerValidate="cvFechaSalida_ServerValidate" ErrorMessage="La fecha de salida debe ser mayor o igual a la fecha de entrada"></asp:CustomValidator>
    </div>
    <div>
        <asp:Label ID="lblMensajeCapacidad" runat="server" Text=""></asp:Label>
        <asp:Label ID="lblNadultos" runat="server" Text="Número de adultos"></asp:Label>
        <asp:TextBox ID="txtNadultos" TextMode="Number" runat="server"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvNadultos" ControlToValidate="txtNadultos" runat="server" ErrorMessage="Este valor es requerido"></asp:RequiredFieldValidator>
        <asp:RangeValidator ID="rvNadultos" ControlToValidate="txtNadultos" runat="server" MaximumValue="4" MinimumValue="1" ErrorMessage="El número ingresado no puede ser procesado"></asp:RangeValidator>
        <br /> 
        <asp:Label ID="lblNnihos" runat="server" Text="Número de niños"></asp:Label>
        <asp:TextBox ID="txtNnihos" TextMode="Number" runat="server"></asp:TextBox>
        <asp:RangeValidator ID="rvNnihos" ControlToValidate="txtNnihos" runat="server" MaximumValue="3" MinimumValue="0" ErrorMessage="El número ingresado no puede ser procesado"></asp:RangeValidator>
    </div>
    <div>
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" OnClick="btnGuardar_Click" />
        <asp:Button ID="btnRegresar" runat="server" Text="Regresar" OnClick="btnRegresar_Click" />
    </div>

</asp:Content>
