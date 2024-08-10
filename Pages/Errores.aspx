<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Errores.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Errores" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

      <div class="alert alert-danger">
      <asp:Label ID="lblMensaje" runat="server" Text=""></asp:Label>
  </div>

  <asp:Button ID="btnRegresar" runat="server" CssClass="btn-secondary" OnClick="btnRegresar_Click" Text="Regresar" />

</asp:Content>
