using Pv_Final_Reservaciones.Clases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pv_Final_Reservaciones
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] != null)
            {//Si la sesion no viene nula entonce entra
                Usuario usuario = (Usuario)Session["Usuario"];//Instancia de la clase Usuario
                lblnombre.Text = usuario.nombreCompleto;//Colocamos el nombre de la persona en el nav de la pagina
                lnkbtnCierresesion.Visible = true;//Mostramos la opcion para cerrar sesion
                lblnombre.Visible = true;//Mostramos el nombre de la persona
                if (usuario.esEmpleado)
                {//Mostramos estas opciones en el nav en caso de que sea empleado
                    alistahabitaciones.Visible = true;
                    lnkMisreservaciones.Visible = true ;
                    lnkGestionarReservacion.Visible = true;
                }
            }
            
        }

        protected void lnkbtnCierresesion_Click(object sender, EventArgs e)
        {//Boton que nos permite borrar los atributos asignados al usuario
            Session.RemoveAll();
            Response.Redirect("~/Pages/Login.aspx");
        }


        protected void btnBooking_Click(object sender, EventArgs e)
        {//Nos permite hacer una redireccion a la pagina principal
            Usuario usuario = (Usuario)Session["Usuario"];

            if (usuario != null)
            {

                // Realizamos una comprobación de si es o no empleado el usuario logeado
                if (usuario.esEmpleado)
                {
                    Response.Redirect("~/Pages/GestionarReservaciones.aspx");
                    usuario.Estado = true;
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

        protected void lnkMisreservaciones_Click(object sender, EventArgs e)
        {//Permite navegar sobre la pagina de misreservaciones al empleado
            Usuario usuario = (Usuario)Session["Usuario"];
            usuario.Estado= false;//Colocamos el estado en false para poder usar las diferentes acciones que hay como si fueran clientes
            Response.Redirect("~/Pages/Misreservaciones.aspx");
        }

        protected void lnkGestionarReservacion_Click(object sender, EventArgs e)
        {// Permite navegar sobre la pagina de Gestionarreservaciones al empleado
            Usuario usuario = (Usuario)Session["Usuario"];
            usuario.Estado = true;//Colocamos el estado en true para poder usar las diferentes acciones que hay como si fueran empleado
            Response.Redirect("~/Pages/GestionarReservaciones.aspx");
        }
    }
}