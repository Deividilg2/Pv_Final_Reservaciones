﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Login" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">


    <style>
    .login-container {
        max-width: 400px;
        margin: 50px auto;
        padding: 20px;
        background-color: #fff;
        box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        border-radius: 10px;
        text-align: center; /* Centrar todo el texto dentro del contenedor */
    }
    
    .login-container h2 {
        text-align: center;
        margin-bottom: 20px;
        width: 100%; /* Asegurar que el h2 ocupe todo el ancho del contenedor */
    }

    .form-group {
        margin-bottom: 15px;
        text-align:center;
    }

    .form-label {
        display: block;
        margin-bottom: 5px;
        font-weight: bold;
        text-align:center;
    }

    .form-control {
        width: 100%;
        padding: 10px;
        border: 1px solid #ccc;
        border-radius: 5px;
        font-size: 20px;
        text-align:center;
        margin: 0 auto; /* Centrará el TextBox */
    display: block; /* Asegura que se comporta como un bloque para centrarlo */
    }

    .btn-block {
        display: block;
        width: 100%;
        padding: 10px;
        font-size: 22px;
        border-radius: 5px;
        text-align:center;
        margin: 20px auto;   /* Centra el botón horizontalmente */
    }

    body {
        background-color: #f5f5f5;
    }
</style>

    <div class="login-container">
        
        <h2>Inicie sesión</h2>
        <div class="form-group">
            <asp:Label ID="lblemail" runat="server" Text="Ingrese email" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtemail" runat="server" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <asp:Label ID="lblclave" runat="server" Text="Ingrese la clave" CssClass="form-label"></asp:Label>
            <asp:TextBox ID="txtclave" runat="server" TextMode="Password" CssClass="form-control"></asp:TextBox>
        </div>

        <asp:Button ID="btnlogin" runat="server" Text="Iniciar sesión" OnClick="btnlogin_Click" CssClass="btn btn-primary 
            btn btn-block" />
        
    </div>

</asp:Content>
