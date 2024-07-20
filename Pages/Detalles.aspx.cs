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
    public partial class Detalles : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
            if (IsPostBack == false)
                //Comprobamos que la carga de página no venga de un boton y no realice la accion dentro
                {
                    int id = int.Parse(Request.QueryString["ID"]);
                    
                    //Tomamos el id mediante la url, la cual enviamos desde las páginas principales (Gestionar/Misreservaciones )
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        var detalle = db.SpConsultarDetallePorId(id).ToList();
                        //FirstOrDefault es necesario ya que aunque el sp solo devuelva una fila el código por sí solo no lo entiende.
                        

                        if (detalle != null)
                        {                           
                            // Asignar los datos al DetailsView
                            dvDetalles.DataSource = detalle;
                            dvDetalles.DataBind();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                
            }
        }
        //Metodo para calcular la diferencia de días que hay entre cada fecha
        public int CalcularDiasReservacion(DateTime fechaEntrada, DateTime fechaSalida)//Los parametros los obtenemos de Detalles.aspx
        {
     
                // Calcula la diferencia en días entre las fechas
                int diferenciaDias = (fechaSalida - fechaEntrada).Days;

                // Si la fecha de entrada y salida son iguales, los días de reserva deben ser 1
                // De lo contrario, los días de reserva deben ser (n + 1), donde n es la diferencia en días
                return diferenciaDias == 0 ? 1 : diferenciaDias + 1;
            
        }

        //Metodo que nos funciona para calcular el total del costo adquirido por reservacion entre el costo de los niños y adultos
        public string CalcularCostoTotal(int numeroAdultos, int numeroNinhos)//Los parámetros los obtenemos de Detalles.aspx en costo total
        {
        //Le indicamos al DetailsView llamado dvDetalles que busque el DataItem llamado "fechaEntrada" con el formato "{0:dd/MM/yyyy}" y además que lo convierta a DateTime
            DateTime fechaEntrada = Convert.ToDateTime(DataBinder.Eval(dvDetalles.DataItem, "fechaEntrada", "{0:dd/MM/yyyy}"));
            DateTime fechaSalida = Convert.ToDateTime(DataBinder.Eval(dvDetalles.DataItem, "fechaSalida", "{0:dd/MM/yyyy}"));
            //Pasamos las fechas al tipo de dato para poder utilizar el método CalcularDiasReservacion(); los tomamos desde 
            float adulto = float.Parse(Request.QueryString["costoPorCadaAdulto"]);
            float nino = float.Parse(Request.QueryString["costoPorCadaNinho"]);
            //Tomamos los datos previamente enviados mediante la url desde Misreservaciones.aspx
            int diasTotal = CalcularDiasReservacion(fechaEntrada,fechaSalida);
            //Realizamos el cálculo final
            float costo = diasTotal * ((numeroAdultos * adulto) + (numeroNinhos * nino));
            //Agregamos simbolo de $ al costo total
            String costoTotal = "$" + costo.ToString();
            return costoTotal;
        }

    }
}