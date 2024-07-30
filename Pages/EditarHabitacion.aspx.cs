using DataModels;
using LinqToDB;
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
                {
                    //llamamos al procedimiento almacenado correspondiente, indicandole el parametro necesario para ejecutarse
                    db.SpInactivarHabitacion(id);
                }

            }
            catch
            {

            }
        }
    }
}

/*
 --Procedimiento almacenado para consultar habitaciones por ID
CREATE PROCEDURE spConsultarHabitacionPorID
--indicamos el parametro de entrada 
	@idHabitacion int
AS
BEGIN
	SET NOCOUNT ON;   
--Seleccionamos toda la información de la tabla Habitacion y la informacion que requerimos de la tabla hotel
	SELECT  
	   ha.[idHabitacion]
      ,ha.[idHotel]
      ,ha.[numeroHabitacion]
      ,ha.[capacidadMaxima]
      ,ha.[descripcion]
      ,ha.[estado],
	   h.nombre --Nombre del hotel
	   --Indicamos de donde tomamos la informacion 
  FROM   [Habitacion] ha
    INNER JOIN --conectamos a las tablas Habitacion y Hotel a traves del idHotel
        [Hotel] h ON ha.idHotel = h.idHotel
		--indicamos que debe ser donde el id de la habitacion sea igual
		 WHERE 
        ha.idHabitacion = @idHabitacion;
END
GO
 */

/*
 --Procedimiento almacenado para inactivar una habitación
CREATE PROCEDURE spInactivarHabitacion 
	--indicamos el parametro de entrada necesario para inactivar la habitacion
	@idHabitacion int
AS
BEGIN
	--comenzamos con la transacción
	SET NOCOUNT ON;
	--Indicamos la actualizacion de "Estado" en la tabla habitacion
	UPDATE Habitacion 
	set 
	estado = 'I'
    --indicamos que debe ser donde el id de la habitacion sea igual
	where idHabitacion = @idHabitacion
END
GO
 */