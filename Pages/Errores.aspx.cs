﻿using Pv_Final_Reservaciones.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pv_Final_Reservaciones.Pages
{
    public partial class Errores : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //validamos sesion del usuario
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            if (!IsPostBack)
            {//Tomamos el source que viene de la URL para realizar seguimiento de errores
                string source = Request.QueryString["source"];
                switch (source)//Segun lo que venga mostramos un error
                {
                    case "Errormodificacion":
                        lblMensaje.Text = "Error la reservación que busca no existe";
                        break;
                    case "ErrorUrl":
                        lblMensaje.Text = "La dirección URL que busca no ha sido encontrada";
                        break;
                    case "ErrorId":
                        lblMensaje.Text = "El id que se solicita no corresponde o no existe";
                        break;
                    case "ErrorInactivo":
                        lblMensaje.Text = "La habitación no puede ser modificada debido a que está inactiva";
                        break;
                    case "ErrorInactivar":
                        lblMensaje.Text = "Habitación ya se encuentra inactiva";
                        break;
                    case "ErrorModificarHabitacion":
                        lblMensaje.Text = "No se ha logrado cargar la habitación para su modificación";
                        break;
                    case "Errorhabitacion":
                        lblMensaje.Text = "Error, no se ha logrado crear la habitación";
                        break;
                    case "ErrorEstadoHabitacion":
                        lblMensaje.Text = "La habitación seleccionada tiene una reservación 'En proceso' o 'En Espera' ";
                        break;
                    case "Errorcancelar":
                        lblMensaje.Text = "La reservación no se ha logrado cancelar correctamente ";
                        break;
                    case "ErrorCrearReservacion":
                        lblMensaje.Text = "La reservación no se ha logrado crear correctamente, inténtelo de nuevo";
                        break;
                    default:
                        lblMensaje.Text = "Fallo al cargar";
                        break;
                }
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {//Creamos una instancia de la clase Usuario
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)//Si viene null no entra
            {

                // Realizamos una comprobación de si es o no empleado el usuario logeado
                if (usuario.esEmpleado)
                {//Tomamos el dato que venga de source
                    string source = Request.QueryString["source"];
                    switch (source)//Comparamos lo que venga en source para realizar acciones distintas
                    {
                        case "CrearHabitacion":
                            Response.Redirect("~/Pages/ListaHabitaciones.aspx");
                            break;
                        case "ErrorInactivo":
                            Response.Redirect("~/Pages/ListaHabitaciones.aspx");
                            break;
                        case "Inactivarhabitacion":
                            Response.Redirect("~/Pages/ListaHabitaciones.aspx");
                            break;
                        default:
                            Response.Redirect("~/Pages/GestionarReservaciones.aspx");
                            break;
                    }
                }
                else
                {
                    Response.Redirect("~/Pages/Misreservaciones.aspx");
                }
                
            }
            else
            {
                // Si no se puede recuperar el usuario de la sesión, redirigimos a la página de inicio de sesión
                Response.Redirect("~/Pages/Login.aspx");
                Session.RemoveAll();
            }
        }
    }
}