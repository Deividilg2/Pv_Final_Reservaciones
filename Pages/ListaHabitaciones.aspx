<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ListaHabitaciones.aspx.cs" Inherits="Pv_Final_Reservaciones.Pages.Lista_Habitaciones" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <h1>Lista de Habitaciones</h1>

    <div>
        <a href="CrearHabitacion.aspx" class="btn btn-link">Crear habitación</a>
    </div>

    <div>

        <asp:GridView ID="grdHabitaciones" runat="server" CssClass="table table-striped-columns">
            <Columns>
                <asp:BoundField HeaderText="ID" ReadOnly="True" />
                <asp:BoundField HeaderText="Hotel" ReadOnly="True" />
                <asp:BoundField HeaderText="Número Habitación" ReadOnly="True" />
                <asp:BoundField HeaderText="Capacidad Máxima" ReadOnly="True" />
                <asp:BoundField HeaderText="Estado" ReadOnly="True" />
                <asp:TemplateField>
                    <ItemTemplate>
                  <%--  Hay que hacer metodo para convertir la A y la I del Estado en Activo o Inactivo--%>
                   
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
