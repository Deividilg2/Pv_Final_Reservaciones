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
    public partial class EditarHabitacion : System.Web.UI.Page
    {
        //cadena de conexion a la base de datos
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Validamos la sesion del usuario activa
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            //Creamos una instancia de Usuario para tomar los datos  del usuario
            Usuario usuario = (Usuario)Session["Usuario"];
            if (!usuario.esEmpleado)//Validamos que el usuario sea un empleado para poder entrar
            {
                Response.Redirect("~/Pages/Errores.aspx?source=ErrorUrl", false);
            }
            // Verifica si la página no es una postback (es decir, si se está cargando por primera vez)
            if (!IsPostBack)
            {
                try
                {
                    // Obtiene el id de la habitación de la query string
                    int id = int.Parse(Request.QueryString["idHabitacion"]);
                    //realizamos la conexion a la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        // Llamamos al procedimiento almacenado para obtener los detalles de la habitación por ID
                        var habitacion = db.SpConsultarHabitacionPorID(id).FirstOrDefault();

                        // Si se encuentra la habitación, asignamos sus valores a los controles del formulario
                        if (habitacion != null)
                        {
                            // Asignamos los valores de la habitación a los controles de texto para que puedan ser editados
                            hdnID.Value = habitacion.IdHabitacion.ToString();
                            txtHotel.Text = habitacion.Nombre.ToString();
                            txtNumHabitacion.Text = habitacion.NumeroHabitacion.ToString();
                            txtCapacidadMax.Text = habitacion.CapacidadMaxima.ToString();
                            txtDescripcion.Text = habitacion.Descripcion.ToString();

                        }
                    }

                }
                catch { }
            }
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //Realizamos las validaciones respectivas para poder seguir
            if (Page.IsValid == true)
            {
                try
                {//Try para evitar errores

                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {//Levantamos la conexion con la base de datos
                        //Tomamos el id de la habitacion
                        int idHabitacion = int.Parse(Request.QueryString["idHabitacion"]);
                        //Nos sirve para evaluar el estado de la habitacion y tomar el el idHotel
                        var habitacion = db.SpConsultarHabitacionPorID(idHabitacion).FirstOrDefault();
                        //Creamos una instancia de usuario para hacer uso de sus atributos
                        Usuario usuario = (Usuario)Session["Usuario"];
                        //Asignamos a las variables los datos respectivos
                        String numeroHabitacion = txtNumHabitacion.Text;
                        int idpersona = usuario.id;
                        int capacidadmMaxima = Int32.Parse(txtCapacidadMax.Text);
                        String descripcion = txtDescripcion.Text;

                        // Verificamos duplicidad del número de habitación para el mismo hotel
                        var habitacionesDuplicadas = db.SpVerificarHabitacionDuplicada(habitacion.IdHotel, numeroHabitacion).FirstOrDefault();
                        if (habitacionesDuplicadas != null && habitacionesDuplicadas.HabitacionesDuplicadas > 0 && habitacion.NumeroHabitacion != numeroHabitacion)
                        {
                            lblMensajeError.Text = "Ya existe una habitación con el mismo número en el hotel seleccionado.";
                            return;
                        }
                        //Evaluamos
                        if (habitacion.Estado == 'A')
                        {
                            //Nos permite verificar cuantos id de nuestra habitacion estan en uso de estado 'A'
                            var reservaciones = db.SpVerificarReservacionesActivas(idHabitacion).FirstOrDefault();
                            //Si encuentra uno o más manda error
                            if (reservaciones.ReservacionesActivas != 0)
                            {
                                Response.Redirect("~/Pages/Errores.aspx?source=ErrorActivo", false);
                            }//Sino entonces logra editar la habitacion y manda a pantalla de resultado
                            else
                            {
                                db.SpEditarHabitacion(idHabitacion, habitacion.IdHotel, numeroHabitacion, idpersona, capacidadmMaxima, descripcion);
                                Response.Redirect("~/Pages/Resultado.aspx?source=EditarHabitacion", false);
                            }
                        }
                        else
                        {//En caso dee que el estado sea 'I' manda error de inactivo 
                            Response.Redirect("~/Pages/Errores.aspx?source=ErrorInactivo", false);
                        }
                    }
                }
                catch
                {

                }
            }
        }

        protected void Inactivar_Click(object sender, EventArgs e)
        {
            //abrimos un try catch para manejar errores, en este botón no es necesario realizar validaciones de elementos
            //en el formulario
            try
            {
                //tomamos el id de la habitacion a traves del hiddenfield en el formulario
                int id = int.Parse(hdnID.Value);

                //realizamos la conexion a la BD
                using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                {   //En caso de que la habitacion ya se encuentre inactiva entonces manda error controlado
                    var errorHabitacion = db.SpConsultarHabitacionPorID(id).FirstOrDefault();
                    if(errorHabitacion.Estado != 'I')
                    {
                        //llamamos al procedimiento almacenado correspondiente, indicandole el parametro necesario para ejecutarse
                        db.SpInactivarHabitacion(id);
                        Response.Redirect("~/Pages/Resultado.aspx?source=Inactivarhabitacion", false);
                    }else
                    {
                        Response.Redirect("~/Pages/Errores.aspx?source=ErrorInactivar", false);
                    }
                    
                }

            }
            catch
            {
                
            }
        }
    }
}
