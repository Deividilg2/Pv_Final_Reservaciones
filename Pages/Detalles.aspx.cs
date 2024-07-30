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
    public partial class Detalles : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            /*Verificamos la sesi[on del usuario*/
            try
            {
            if (IsPostBack == false)
                //Comprobamos que la carga de página no venga de un boton y no realice la accion dentro
                {
                   //Conectamos con la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
            //Tomamos el id mediante la url, la cual enviamos desde las páginas principales (Gestionar/Misreservaciones)
                        int id = int.Parse(Request.QueryString["id"]);
                        //Pasamos el id al Storeprocedure
                        var detalle = db.SpConsultarDetallePorId(id).ToList();                        
                        if (detalle != null)
                        {                           
                            // Asignar los datos al DetailsView
                            dvDetalles.DataSource = detalle;
                            dvDetalles.DataBind();
                        }
                    }
                }
            }
            catch
            {
                
            }
        }



        protected void lnkEditar_Click(object sender, EventArgs e)
        {
            int id = int.Parse(Request.QueryString["id"]);
            Response.Redirect($"ModificarReservacion.aspx?idReservacion={id}");
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            try
            {
                //Consultar este int idReservacion?
                int idReservacion = int.Parse(Request.QueryString["id"]);
                Usuario usuario = Session["Usuario"] as Usuario;

                if (usuario != null)
                {
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        var resultado = db.SpCancelarReservacionYRegistrarBitacora(idReservacion, usuario.id).FirstOrDefault();
                        string mensaje = resultado?.Resultado;

                        if (mensaje == "Reservación cancelada exitosamente")
                        {
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
                            Response.Write(mensaje);
                        }
                    }
                }
                else
                {
                    Response.Redirect("~/Pages/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                Response.Write("Ocurrió un error al cancelar la reservación: " + ex.Message);
            }
        }

        protected void btnregresar_Click(object sender, EventArgs e)
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

    }
}