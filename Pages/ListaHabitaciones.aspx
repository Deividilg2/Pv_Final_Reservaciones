<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaHabitaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Lista_Habitaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <style>

    .text-center {
        text-align: center;
    }

    .text-left {
        text-align: left;
    }

    .font-bold {
        font-weight: bold;
    }

    .btn {
        display: inline-block;
        font-weight: 400;
        color: #212529;
        text-align: center;
        vertical-align: middle;
        user-select: none;
        background-color: transparent;
        border: 1px solid transparent;
        padding: 0.375rem 0.75rem;
        font-size: 1rem;
        line-height: 1.5;
        border-radius: 0.25rem;
        transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
    }

    .btn-link {
        font-weight: 500;
        color: #007bff;
        text-decoration: underline;
        font-size: 1.5rem;
    }

    .btn-primary {
        color: #fff;
        background-color: #007bff;
        border-color: #007bff;
    }

    .btn-primary:hover {
        color: #fff;
        background-color: #0056b3;
        border-color: #004085;
    }
</style>


    <h1>Lista de Habitaciones</h1>
     <br />
    <div>
        <a href="CrearHabitacion.aspx" class="btn btn-primary">Crear habitación</a>
    </div>
    <br />
    <div>

        <asp:GridView ID="grdHabitaciones" runat="server" CssClass="table table-striped" AutoGenerateColumns="False">
            <Columns>
                <asp:BoundField HeaderText="ID" ReadOnly="True" DataField="idHabitacion" 
                    ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center font-bold" />
                <asp:BoundField HeaderText="Hotel" ReadOnly="True" DataField="nombre" ItemStyle-CssClass="text-left"
                    HeaderStyle-CssClass="text-left font-bold" />
                <asp:BoundField HeaderText="Número Habitación" ReadOnly="True" DataField="numeroHabitacion" 
                    ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center font-bold" />
                <asp:BoundField HeaderText="Capacidad Máxima" ReadOnly="True" DataField="capacidadMaxima"
                    ItemStyle-CssClass="text-center" HeaderStyle-CssClass="text-center font-bold" />
                <asp:TemplateField HeaderText="Estado" HeaderStyle-CssClass="text-center font-bold">
                    <ItemTemplate>
                  <%--  Método para convertir la A y la I del Estado en Activo o Inactivo--%>
                   <asp:Label ID="lblEstado" runat="server" Text='<%# ConvertEstado(Eval("estado").ToString()) %>' CssClass="text-center"></asp:Label>
                    </ItemTemplate>
                    
                </asp:TemplateField>

                <asp:TemplateField>
                    <ItemTemplate>

                        <a href="EditarHabitacion.aspx?idHabitacion=<%# Eval("idHabitacion") %>" class="btn btn-primary">Modificar</a>

                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>

        </asp:GridView>

    </div>

</asp:Content>
