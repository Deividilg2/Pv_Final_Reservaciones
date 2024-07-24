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

    }
}