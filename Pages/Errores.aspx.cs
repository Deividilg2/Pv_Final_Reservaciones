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
                    case "CrearHabitacion":
                        lblMensaje.Text = "Ha modificado correctamente la habitación";
                        break;
                    case "EditarHabitacion":
                        lblMensaje.Text = "Se ha logrado modificar de forma correcta la habitación";
                        break;
                    case "Inactivarhabitacion":
                        lblMensaje.Text = "Se ha logrado modificar de forma correcta la habitación";
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
                    if (source == "CrearHabitacion" || source == "EditarHabitacion" || source == "Inactivarhabitacion")
                    {
                        Response.Redirect("~/Pages/ListaHabitaciones.aspx");
                    }
                    else
                    {
                        Response.Redirect("~/Pages/GestionarReservaciones.aspx");
                    }
                }
                if (!usuario.esEmpleado)
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