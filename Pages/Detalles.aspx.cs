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

//Arreglar validacion de id segun la reservacion del usuario pag 23 doc

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

            /*Verificamos la sesion del usuario*/
            try
            {
            if (IsPostBack == false)
                //Comprobamos que la carga de página no venga de un boton y no realice la accion dentro
                {
                    int id = int.Parse(Request.QueryString["id"]);
                    //Conectamos con la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        Usuario usuario = Session["Usuario"] as Usuario;
                        if (usuario.esEmpleado)
                        {
                            //Pasamos el id al Storeprocedure
                            var detalle = db.SpConsultarDetallePorIdEmpleado(id).FirstOrDefault();  //Revisar en bd
                            var listabitacora = db.SpConsultarBitacoraEmpleado(id).ToList();
                            if (detalle != null)
                            {
                                // Asignar los datos al DetailsView con un objeto Ienumerable list
                                dvDetalles.DataSource = new List<dynamic> { detalle };
                                dvDetalles.DataBind();
                                // Asignar los datos al DetailsView
                                grdacciones.DataSource = listabitacora;
                                grdacciones.DataBind();

                                // Logica para mostrar/ocultar el boton de editar
                                lnkEditar.Visible = detalle.Estado == 'A' &&
                                                    ((usuario.esEmpleado && detalle.FechaSalida > DateTime.Today) ||
                                                     (!usuario.esEmpleado && detalle.FechaEntrada > DateTime.Today));

                                // Logica para mostrar/ocultar el boton de cancelar
                                btncancelar.Visible = detalle.Estado == 'A' && detalle.FechaEntrada > DateTime.Today;

                            }
                            else
                            {
                                Response.Redirect("~/Pages/Errores.aspx?source=ErrorId", false);
                            }
                        }
                        else 
                        {
                            //Pasamos el id al Storeprocedure
                            var detalle = db.SpConsultarDetallePorId(id, usuario.id).FirstOrDefault();  //Revisar en bd
                            var listabitacora = db.SpConsultarBitacora(id, usuario.id).ToList();
                            if (detalle != null)
                            {
                                // Asignar los datos al DetailsView con un objeto Ienumerable list
                                dvDetalles.DataSource = new List<dynamic> { detalle };
                                dvDetalles.DataBind();
                                // Asignar los datos al DetailsView
                                grdacciones.DataSource = listabitacora;
                                grdacciones.DataBind();


                                // Logica para mostrar/ocultar el boton de editar
                                lnkEditar.Visible = detalle.Estado == 'A' &&
                                                    ((usuario.esEmpleado && detalle.FechaSalida > DateTime.Today) ||
                                                     (!usuario.esEmpleado && detalle.FechaEntrada > DateTime.Today));

                                // Logica para mostrar/ocultar el boton de cancelar
                                btncancelar.Visible = detalle.Estado == 'A' && detalle.FechaEntrada > DateTime.Today;

                            }
                            else
                            {
                                Response.Redirect("~/Pages/Errores.aspx?source=ErrorId", false);
                            }
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
                // Consultar el id de la reservación desde la query string
                int idReservacion = int.Parse(Request.QueryString["id"]);
                // Obtener el usuario actual desde la sesión
                Usuario usuario = (Usuario)Session["Usuario"];
                //si el usuario es diferente de nulo proceda con la ejecucion 
                if (usuario != null)
                {
                    //realizamos la conexión a la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        // Llamar al procedimiento almacenado para cancelar la reservación y registrar la acción en la bitácora
                        var resultado = db.SpCancelarReservacionYRegistrarBitacora(idReservacion, usuario.id).FirstOrDefault();
                        // Obtener el mensaje de resultado del procedimiento almacenado
                        string mensaje = resultado?.Resultado;
                        // Verificar si la cancelación fue exitosa
                        if (mensaje == "Reservación cancelada exitosamente")
                        {
                            // Redirigir al usuario a la página correspondiente según su rol
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
                            // Mostrar el mensaje de error si la cancelación no fue exitosa
                            Response.Write(mensaje);
                        }
                    }
                }
                else
                {
                    // Redirigir al usuario a la página de inicio de sesión si no está autenticado
                    Response.Redirect("~/Pages/Login.aspx");
                }
            }
            catch (Exception ex)
            {
                // Mostrar el mensaje de error si ocurre una excepción durante el proceso
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
/*
 CREATE PROCEDURE [dbo].[spConsultarDetallePorIdEmpleado]
@idreservacion int
AS
BEGIN
SELECT r.idReservacion,ha.idHabitacion,ha.numeroHabitacion,h.idHotel,h.nombre,r.fechaEntrada,r.fechaSalida,r.numeroAdultos,r.numeroNinhos,
totalDiasReservacion = CASE 
            WHEN DATEDIFF(DAY, r.fechaEntrada, r.fechaSalida) = 0 THEN 1
            ELSE DATEDIFF(DAY, r.fechaEntrada, r.fechaSalida)
END,
costoTotal =totalDiasReservacion * ((r.numeroAdultos*r.costoPorCadaAdulto)+(r.numeroNinhos*r.costoPorCadaNinho)),
r.estado,p.nombreCompleto
FROM Reservacion r left join Habitacion ha on r.idHabitacion = ha.idHabitacion
--Seleccionamos a la tabla habitación para conectarnos con la tabla hotel y poder traer el nombre del hotel
left join Hotel h on h.idHotel = ha.idHotel
join Persona p on r.idPersona = p.idPersona
WHERE r.idReservacion = @idreservacion
--Ordenamos de manera descendente
Order by r.idreservacion desc
END;
 


CREATE PROCEDURE [dbo].[spConsultarBitacoraEmpleado]
--Procedimiento para verificar los cambios que se han realizado 
--en las reservaciones mediante la Bitacora
@idReservacion int
AS
BEGIN
SELECT b.fechaDeLaAccion, b.accionRealizada, p.nombreCompleto, r.idReservacion, b.idBitacora
from Bitacora b left join Persona p on p.idPersona=b.idPersona
left join Reservacion r on r.idReservacion=b.idReservacion
WHERE r.idReservacion=@idReservacion
ORDER BY idBitacora DESC;
END
 */