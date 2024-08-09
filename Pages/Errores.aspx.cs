using Pv_Final_Reservaciones.Clases;
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
            if (!IsPostBack)
            {
                string source = Request.QueryString["source"];
                switch (source)
                {
                    case "Errormodificacion":
                        lblMensaje.Text = "Error la reservación que busca no existe";
                        break;
                    case "ErrorUrl":
                        lblMensaje.Text = "La dirección URL que busca no ha sido encontrada";
                        break;
                    case "ErrorId":
                        lblMensaje.Text = "El id que se solicita no corresponde al usuario o no existe";
                        break;
                    case "ErrorInactivo":
                        lblMensaje.Text = "La habitación no puede ser modificada debido a que está inactiva";
                        break;
                    case "ErrorActivo":
                        lblMensaje.Text = "Actualmente existen reservaciones asociadas en proceso o espera para esta habitación";
                        break;
                    case "ErrorInactivar":
                        lblMensaje.Text = "Habitación ya se encuentra inactiva";
                        break;
                    case "Errorhabitacion":
                        lblMensaje.Text = "No se encontro una habitación";
                        break;
                    case "ErrorEstadoHabitacion":
                        lblMensaje.Text = "La habitación seleccionada tiene una reservación 'En proceso' o 'En Espera' ";
                        break;
                    default:
                        lblMensaje.Text = "Operación realizada.";
                        break;
                }
            }
        }

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)
            {

                // Realizamos una comprobación de si es o no empleado el usuario logeado
                if (usuario.esEmpleado)
                {
                    string source = Request.QueryString["source"];
                    switch (source)
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
                            lblMensaje.Text = "Error desconocido";
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