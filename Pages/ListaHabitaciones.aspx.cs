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
    public partial class Lista_Habitaciones : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                //Realizamos la conexión con la BD
                using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                {
                    //creamos una lista para almacenar la informacion de la tabla Habitaciones de la BD
                    var lista = db.SpConsultarHabitaciones();

                    //pasamos los datos al GriedView 
                    grdHabitaciones.DataSource = lista;
                    grdHabitaciones.DataBind();
                }
            }
            catch
            {

            }
        }

        // Método para convertir el estado de "A" a "Activa" y de "I" a "Inactiva"
        // Este método toma un string como parámetro llamado "estado"
        // y devuelve una cadena de texto ("Activa" o "Inactiva")
        // dependiendo del valor del parámetro "estado".
        protected string ConvertEstado(string estado)
        {
            //utilizamos una expresión condicional para verificar el valor del párametro "estado" 
            // Si el valor de "estado" es igual a "A"
            if (estado == "A")
            {
                // Devuelve "Activa"
                return "Activa";
            }
            else
            {
                // Si no, devuelve "Inactiva"
                return "Inactiva";
            }
        }

    }
}

/*
 --Procedimiento almacenado para consultar Habitaciones
CREATE PROCEDURE spConsultarHabitaciones
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
        [Hotel] h ON ha.idHotel = h.idHotel;
END
GO
 */