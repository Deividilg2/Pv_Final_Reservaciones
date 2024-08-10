using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using LinqToDB;
using DataModels;
using Pv_Final_Reservaciones.Clases;

namespace Pv_Final_Reservaciones.Pages
{
    public partial class Misreservaciones : System.Web.UI.Page
    {
        //Realizamos la variable que contiene la conexion a la base de datos
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            Usuario usuarioP = (Usuario)Session["Usuario"];
            if (usuarioP.esEmpleado)
            {
                //Validacion que se usa en misreservaciones, CrearReservacion para los empleados y el cambio de pestañas
                usuarioP.Estado = false;
            }
            //Validamos la sesión del usuario
            //Colocamos un try catch en caso de cualquier error en nuestro código
            try
            {
                if (IsPostBack == false)
                {
                    
                    //Utilizamos using para realizar la conexion solamente cuando es necesario y que se desconecte cuando no está en uso
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        //Creamos una instancia de usuario para poder llamar el id del usuario logeado
                        //Traemos los datos para rellenar la tabla con el store procedure correspondiente
                        var lista = db.SpMisReservaciones(usuarioP.id).ToList();
                        grdMisreservaciones.DataSource = lista;
                        grdMisreservaciones.DataBind();
                     
                    }
                }
            }
            catch
            {
            }
        }

        // Método para convertir el estado segun se necesite
        // Este método toma un string como parámetro llamado "estado"
        // y devuelve una cadena de texto (“Cancelada”,“Finalizada”,“En proceso” o “En espera”)
        // dependiendo del valor del parámetro "estado".
        protected string ConvertEstado(string estado, DateTime fechaEntrada, DateTime fechaSalida)
        {
            DateTime today = DateTime.Today;

            if (estado == "I")
            {
                return "Cancelada";
            }
            else if (estado == "A")
            {
                if (fechaSalida < today)
                {
                    return "Finalizada";
                }
                else if (fechaEntrada <= today)
                {
                    return "En proceso";
                }
                else
                {
                    return "En espera";
                }
            }
            return "Estado desconocido";
        }

        protected void btnNuevareservacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/CrearReservacion.aspx");
        }
    }
}