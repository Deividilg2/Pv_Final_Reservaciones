using Pv_Final_Reservaciones.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pv_Final_Reservaciones.Pages
{
    public partial class Resultado : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string source = Request.QueryString["source"];
                switch (source)
                {
                    case "CrearReservacion":
                        lblMensaje.Text = "Ha registrado correctamente una nueva reservación en el sistema";
                        break;
                    case "":
                        lblMensaje.Text = "Ha modificado correctamente un nuevo teléfono en la base de datos";
                        break;
                    case "EditarTelefonoElim":
                        lblMensaje.Text = "Ha eliminado correctamente la información de un teléfono en la base de datos";
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
                    Response.Redirect("~/Pages/GestionarReservaciones.aspx");
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
            }
        }
    }
}