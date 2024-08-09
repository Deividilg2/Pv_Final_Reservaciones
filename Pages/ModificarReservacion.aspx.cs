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
            Usuario usuario = (Usuario)Session["Usuario"];
            if (IsPostBack == false)
            {
                try
                {
                    int id = int.Parse(Request.QueryString["idReservacion"]);

                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        // Llamar al procedimiento almacenado para obtener la reservación
                        var reservacion = db.SpConsultarReservacionPorID(id).FirstOrDefault();
                        //Validamos que la reservacion sea del usuario o un empleado quiera entrar
                        if (reservacion.NombreCompleto == usuario.nombreCompleto || usuario.esEmpleado)
                        {
                            // Asignar valores a los controles de la página
                            txtHotel.Text = reservacion.Nombre.ToString();
                            txtHabitacion.Text = reservacion.NumeroHabitacion.ToString();
                            txtCliente.Text = reservacion.NombreCompleto.ToString();
                            txtFechaEntrada.Text = reservacion.FechaEntrada.ToString("yyyy-MM-dd");
                            txtFechaSalida.Text = reservacion.FechaSalida.ToString("yyyy-MM-dd");
                            txtNumeroAdultos.Text = reservacion.NumeroAdultos.ToString();
                            txtNumeroNinhos.Text = reservacion.NumeroNinhos.ToString();
                        }
                        else
                        {
                            // Redireccionar si no se encuentra la reservación
                            Response.Redirect("~/Pages/Errores.aspx?source=Errormodificacion", false);
                        }
                    }

                }
                catch
                {

                }
            }
        }

        protected void btnGuardarModificacion_Click(object sender, EventArgs e)
        {//Hay que colocarle metodo o algo para que no sobrepase la cant max de personas en habitacion
            if (Page.IsValid == true)
            {
                try
                {//Tomamos los datos
                    int id = int.Parse(Request.QueryString["idReservacion"]);
                    DateTime fechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
                    DateTime fechaSalida = DateTime.Parse(txtFechaSalida.Text);
                    int numeroAdultos = int.Parse(txtNumeroAdultos.Text);
                    int numeroNihos = int.Parse(txtNumeroNinhos.Text);
                    int totalPersonas = numeroAdultos + numeroNihos;
                    int totalDiasReservacion = (int)(fechaSalida - fechaEntrada).TotalDays;
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {//Buscamos la reservacion para poder extraer el numeroHabitacion
                        var reservacion = db.SpConsultarReservacionPorID(id).FirstOrDefault();

                        if (reservacion != null)
                        {
                            if (reservacion.Estado == 'I')
                            {
                                RedirectUser();
                                return;
                            }
                           else if (reservacion.FechaSalida <= DateTime.Today)
                            {
                                RedirectUser();
                                return;
                            }
                            else if (reservacion.FechaEntrada <= DateTime.Today && reservacion.FechaSalida > DateTime.Today)
                            {
                                RedirectUser();
                                return;
                            }
                            //Buscamos la capacidad que tiene la habitacion
                            var capacidadHabitacion = db.SpConsultarCapacidadHabitacionPorID(reservacion.NumeroHabitacion).FirstOrDefault();
                            //Validamos que no pase la capacidad de la habitacion
                            if (capacidadHabitacion.CapacidadMaxima >= totalPersonas)
                            {//Si cumple la validacion se modifica la reservacion
                                db.SpModificarReservacionYRegistrarBitacora(id, capacidadHabitacion.IdHotel, fechaEntrada, fechaSalida, numeroAdultos, numeroNihos);
                                Response.Redirect("~/Pages/Resultado.aspx?source=ModificarReservacion", false);
                            }
                            else//Si se pasa del maximo de la habitacion
                            {
                                lblMensajeCapacidad.Text = "Demasiadas personas para la habitacón, máximo alcanzan " + capacidadHabitacion.CapacidadMaxima;

                            }
                        }
                    }
                }
                catch
                {
                    Response.Redirect("~/Pages/Errores.aspx?source=Errormodificar");
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
                    if (DateTime.Parse(args.Value) > DateTime.Today)
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
                    // Verificar que la fecha de salida no sea menor a la fecha de entrada
                    //esta fecha debe ser mayor o igual
                    if (fechaSalida >= fechaEntrada)
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