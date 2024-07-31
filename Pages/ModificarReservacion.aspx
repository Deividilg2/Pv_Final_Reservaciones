<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarReservacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.ModificarReservacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>

        <h1>Modificar Reservación</h1>

<asp:HiddenField ID="hdnId" runat="server" />
<br />
<br />

<asp:Label ID="Label1" runat="server" Text="Label">Hotel</asp:Label>
<br>
<asp:TextBox ID="txtHotel" runat="server" ReadOnly="True"></asp:TextBox>
<br><br>
<asp:Label ID="Label2" runat="server" Text="Label">Número de habitación</asp:Label>
<br>
<asp:TextBox ID="txtHabitacion" runat="server" ReadOnly="true"></asp:TextBox>
<br><br>
<asp:Label ID="Label3" runat="server" Text="Label">Cliente</asp:Label>
<br>
<asp:TextBox ID="txtCliente" runat="server" ReadOnly="true"></asp:TextBox>
<br><br>

<asp:Label ID="Label4" runat="server" Text="Label">Fecha de Entrada</asp:Label> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:Label ID="Label5" runat="server" Text="Label">Fecha de Salida</asp:Label>
<br>
<asp:TextBox ID="txtFechaEntrada" runat="server" TextMode="Date" ></asp:TextBox>&nbsp;&nbsp;
<asp:TextBox ID="txtFechaSalida" runat="server"  placeholder="dd/MM/YYYY"></asp:TextBox>
<br><br>

        <asp:CustomValidator ID="cvFechaEntrada" runat="server" ErrorMessage="CustomValidator" 
            ControlToValidate="txtFechaEntrada" OnServerValidate="cvFechaEntrada_ServerValidate" ></asp:CustomValidator>
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CustomValidator ID="cvFechaSalida" runat="server" ErrorMessage="CustomValidator" 
            ControlToValidate="txtFechaSalida" OnServerValidate="cvFechaSalida_ServerValidate"></asp:CustomValidator>

        <br />
        <br />

<asp:Label ID="Label6" runat="server" Text="Label">Número de Adultos</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:Label ID="Label7" runat="server" Text="Label">Número de Niños</asp:Label>
<br>
<asp:TextBox ID="txtNumeroAdultos" runat="server" TextMode="Number"></asp:TextBox> &nbsp;&nbsp;
<asp:TextBox ID="txtNumeroNinhos" runat="server" TextMode="Number"></asp:TextBox>

<br><br>
<asp:RangeValidator ID="rgvNumAdultos" runat="server" ErrorMessage="Mínimo 1 adulto"
    ControlToValidate="txtNumeroAdultos" MinimumValue="1" MaximumValue="8" Type="Integer" ></asp:RangeValidator>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
<asp:RangeValidator ID="rgvNumNinhos" runat="server" ErrorMessage="" 
    ControlToValidate="txtNumeroNinhos" MinimumValue="0" MaximumValue="7" Type="Integer"></asp:RangeValidator>

    </div>
<asp:Button ID="btnGuardarModificacion" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarModificacion_Click" /> &nbsp;&nbsp; 
<asp:Button ID="btnRegresar" runat="server" Text="Regresar"  CssClass="btn btn-secondary" OnClick="btnRegresar_Click"/>

</asp:Content>
