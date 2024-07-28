using DataModels;
using LinqToDB;
using Pv_Final_Reservaciones.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Pv_Final_Reservaciones.Pages
{
    public partial class ModificarReservacion : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //validamos sesion del usuario
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }

            if (!IsPostBack)
            {
                //if (int.TryParse(Request.QueryString["idReservacion"], out int idReservacion))
                //{
                    try
                    {
                        int id = int.Parse(Request.QueryString["idReservacion"]);

                        using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                        {
                            // Llamar al procedimiento almacenado para obtener la reservación
                            var reservacion = db.SpConsultarReservacionPorID(id).FirstOrDefault();
                            //var reservacion = db.SpModificarReservacionYRegistrarBitacora(idReservacion).FirstOrDefault();

                            if (reservacion != null)
                            {
                                // Asignar valores a los controles de la página
                                hdnId.Value = reservacion.IdReservacion.ToString();
                                txtHotel.Text = reservacion.Nombre.ToString();
                                txtHabitacion.Text = reservacion.NumeroHabitacion.ToString();
                                txtCliente.Text = reservacion.NombreCompleto.ToString();
                                txtFechaEntrada.Text = reservacion.FechaEntrada.ToString("dd/MM/yyyy");
                                txtFechaSalida.Text = reservacion.FechaSalida.ToString("dd/MM/yyyy");
                                txtNumeroAdultos.Text = reservacion.NumeroAdultos.ToString();
                                txtNumeroNinhos.Text = reservacion.NumeroNinhos.ToString();
                            }
                            else
                            {
                            // Redireccionar si no se encuentra la reservación
                            Response.Redirect("~/Pages/ResultadoDeEditarReservacion.aspx");
                        }
                            ////else 
                            ////    {
                            ////        // Redireccionar si el idReservacion no está presente en QueryString
                            ////        // Response.Redirect("~/Pages/ErrorGeneral.aspx");
                            ////    }
                        }
                    }
                    catch
                    {

                    }
                }
            //}
        }

        protected void btnGuardarModificacion_Click(object sender, EventArgs e)
        {
            if (Page.IsValid == true)
            {
                try
                {
                    int idReservacion = int.Parse(hdnId.Value);
                    DateTime FechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
                    DateTime FechaSalida = DateTime.Parse(txtFechaSalida.Text);
                    int NumeroAdultos = int.Parse(txtNumeroAdultos.Text);
                    int NumeroNinhos = int.Parse(txtNumeroNinhos.Text);
                    

                    // Obtener el idPersona de la sesión
                    if (Session["idPersona"] != null)
                    {
                        int idPersona = (int)Session["idPersona"];

                        using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                        {
                            var reservacion = db.SpConsultarReservacionPorID(idReservacion).FirstOrDefault();

                            if (reservacion != null)
                            {
                                if (reservacion.Estado == 'I')
                                {
                                    RedirectUser();
                                    return;
                                }
                                if (reservacion.FechaSalida <= DateTime.Today)
                                {
                                    RedirectUser();
                                    return;
                                }
                                if (reservacion.FechaEntrada <= DateTime.Today && reservacion.FechaSalida > DateTime.Today)
                                {
                                    RedirectUser();
                                    return;
                                }

                                db.SpModificarReservacionYRegistrarBitacora(idReservacion, FechaEntrada, FechaSalida, NumeroAdultos, NumeroNinhos, idPersona);
                                Response.Redirect("~/Pages/ResultadoDeEditarReservacion.aspx");
                            }
                        }
                    }
                }
                catch
                {

                }
            }

        }

        protected void RedirectUser()
        {
            // Recuperamos el objeto Usuario de la sesión
            Usuario usuario = Session["Usuario"] as Usuario;

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


        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Recuperamos el objeto Usuario de la sesión
            Usuario usuario = Session["Usuario"] as Usuario;

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

        protected void cvFechaEntrada_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = false;
                if (args.Value != null)
                {
                    if (DateTime.Parse(args.Value) < DateTime.Today)
                    {
                        args.IsValid = true;

                    }
                }
            }
            catch
            {

            }
        }

        protected void cvFechaSalida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                DateTime fechaEntrada;
                DateTime fechaSalida;

                // Intentar convertir las fechas
                bool fechaEntradaValida = DateTime.TryParse(txtFechaEntrada.Text, out fechaEntrada);
                bool fechaSalidaValida = DateTime.TryParse(txtFechaSalida.Text, out fechaSalida);

                // Verificar si ambas fechas son válidas
                if (fechaEntradaValida && fechaSalidaValida)
                {
                    // Verificar que la fecha de salida no sea menor ni igual que la fecha de entrada
                    //esta fecha debe ser mayor
                    if (fechaSalida > fechaEntrada)
                    {
                        args.IsValid = true;
                    }
                    else
                    {
                        args.IsValid = false;
                    }
                }
                else
                {
                    // Si alguna de las fechas no es válida, la validación falla
                    args.IsValid = false;
                }
            }
            catch 
            {
                // Manejar cualquier excepción que pueda ocurrir
                args.IsValid = false;
                
            }

        }
    }
}