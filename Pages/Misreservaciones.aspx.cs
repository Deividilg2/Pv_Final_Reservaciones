using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using LinqToDB;
using DataModels;

namespace Pv_Final_Reservaciones.Pages
{
    public partial class Misreservaciones : System.Web.UI.Page
    {
        //Realizamos la variable que contiene la conexion a la base de datos
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Colocamos un try catch en caso de cualquier error en nuestro código
            try
            {
                if (IsPostBack == false)
                {
                    // Recuperamos el idPersona de la URL
                    int idPersona = int.Parse(Request.QueryString["id"]);
                    
                    //Utilizamos using para realizar la conexion solamente cuando es necesario y que se desconecte cuando no está en uso
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        //Traemos los datos para rellenar la tabla con el store procedure correspondiente
                        var lista = db.SpMisReservaciones(idPersona).ToList();
                        grdMisreservaciones.DataSource = lista;
                        grdMisreservaciones.DataBind();
                        //Metodo sospechoso, preguntar al profe
                        var lista2 = db.SpMisReservaciones(idPersona).FirstOrDefault();
                        lblcliente.Text = lista2.NombreCompleto;
                    }
                }
                

            }
            catch (Exception ex)
            {

            }
        }
    }
}