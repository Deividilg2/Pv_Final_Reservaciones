<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ModificarReservacion.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.ModificarReservacion" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>

        <h2>Modificar Reservación</h2>

<asp:HiddenField ID="hdnId" runat="server" />
<br />

<asp:Label ID="Label1" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Hotel</asp:Label>
<br>
<asp:TextBox ID="txtHotel" runat="server" ReadOnly="True" CssClass="form-control"></asp:TextBox>
        <br>
<asp:Label ID="Label2" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Número de habitación</asp:Label>
<br>
<asp:TextBox ID="txtHabitacion" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        <br>
<asp:Label ID="Label3" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Cliente</asp:Label>
<br>
<asp:TextBox ID="txtCliente" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        <br>

<asp:Label ID="Label4" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Fecha de Entrada</asp:Label>
<br>
<asp:TextBox ID="txtFechaEntrada" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
 <asp:CustomValidator ID="cvFechaEntrada" runat="server" ErrorMessage="No se admiten fechas menores o iguales a la actual" 
     ControlToValidate="txtFechaEntrada" OnServerValidate="cvFechaEntrada_ServerValidate" ></asp:CustomValidator>
<br />
<asp:Label ID="Label5" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Fecha de Salida</asp:Label>
<asp:TextBox ID="txtFechaSalida" runat="server" TextMode="Date" CssClass="form-control"></asp:TextBox>
  <%-- el custom validator impide el redirect, hay que revisar eso--%>
     <asp:CustomValidator ID="cvFechaSalida" runat="server" ErrorMessage="No se admiten fechas menores o iguales a la actual" 
         ControlToValidate="txtFechaSalida" OnServerValidate="cvFechaSalida_ServerValidate"></asp:CustomValidator>
        <br>

        <asp:Label ID="lblMensajeCapacidad" runat="server" ForeColor="Red" Text=""></asp:Label><br />

<asp:Label ID="Label6" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Número de Adultos</asp:Label> 
        <asp:TextBox ID="txtNumeroAdultos" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox> 
<asp:RangeValidator ID="rgvNumAdultos" runat="server" ErrorMessage="Mínimo 1 adulto"
    ControlToValidate="txtNumeroAdultos" MinimumValue="1" MaximumValue="8" Type="Integer" ></asp:RangeValidator>

        <br />

<asp:Label ID="Label7" runat="server" Text="Label" CssClass="form-label" Font-Bold="True">Número de Niños</asp:Label>
<br>
<asp:TextBox ID="txtNumeroNinhos" runat="server" TextMode="Number" CssClass="form-control"></asp:TextBox>
<br>
<asp:RangeValidator ID="rgvNumNinhos" runat="server" ErrorMessage="" 
    ControlToValidate="txtNumeroNinhos" MinimumValue="0" MaximumValue="7" Type="Integer"></asp:RangeValidator>

    </div>
<asp:Button ID="btnGuardarModificacion" runat="server" Text="Guardar" CssClass="btn btn-primary" OnClick="btnGuardarModificacion_Click" /> &nbsp;&nbsp; 
<asp:Button ID="btnRegresar" runat="server" Text="Regresar"  CssClass="btn btn-secondary" OnClick="btnRegresar_Click" CausesValidation="false"/>

</asp:Content>
