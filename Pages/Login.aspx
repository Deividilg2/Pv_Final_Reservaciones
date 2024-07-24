<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div>
        
        <h2>Inicie sesión</h2>
        <div>
            <asp:Label ID="lblemail" runat="server" Text="Ingrese email"></asp:Label>
            <asp:TextBox ID="txtemail" runat="server"></asp:TextBox>
        </div>

        <div>
            <asp:Label ID="lblclave" runat="server" Text="Ingrese la clave"></asp:Label>
            <asp:TextBox ID="txtclave" runat="server" TextMode="Password"></asp:TextBox>
        </div>

        <asp:Button ID="btnlogin" runat="server" Text="Iniciar sesión" OnClick="btnlogin_Click" />
        
    </div>

</asp:Content>
