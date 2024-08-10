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
            {
                Usuario usuario = (Usuario)Session["Usuario"];
                lblnombre.Text = usuario.nombreCompleto;
                lnkbtnCierresesion.Visible = true;
                lblnombre.Visible = true;
                if (usuario.esEmpleado)
                {
                    alistahabitaciones.Visible = true;
                    lnkMisreservaciones.Visible = true ;
                    lnkGestionarReservacion.Visible = true;
                }
            }
            
        }

        protected void lnkbtnCierresesion_Click(object sender, EventArgs e)
        {
            Session.RemoveAll();
            Response.Redirect("~/Pages/Login.aspx");
        }


        protected void btnBooking_Click(object sender, EventArgs e)
        {
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
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            usuario.Estado= false;
            Response.Redirect("~/Pages/Misreservaciones.aspx");
        }

        protected void lnkGestionarReservacion_Click(object sender, EventArgs e)
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            usuario.Estado = true;
            Response.Redirect("~/Pages/GestionarReservaciones.aspx");
        }
    }
}