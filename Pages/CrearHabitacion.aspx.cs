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
    public partial class CrearHabitacion : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Page.IsPostBack == false)
            {
                try
                {
                    //creamos una lista
                    var lista = new List<ListItem>();

                    //para que aparezca un mensaje en el dropdown como primer item de la lista
                    lista.Add(new ListItem("Seleccione un hotel", "0"));

                    //Lista Dinamica usando la BD
                    //Realizamos la conexión con la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        var query = db.SpConsultarHoteles().Select(H => new ListItem(H.Nombre,H.IdHotel.ToString())).ToList();

                        lista.AddRange(query);
                    }

                    ddlHoteles.DataSource = lista;
                    //antes de hacer el databind hay que agregar el datatextfield, indicando cual es el campo que
                    //queremos se muestre como texto y cual como valor
                    ddlHoteles.DataTextField = "Text";
                    ddlHoteles.DataValueField = "Value";
                    ddlHoteles.DataBind();

                    //para que se coloque ya una de las opciones predeterminadas
                    ddlHoteles.Items.FindByValue("0").Selected = true;
                }
                catch
                {

                }
            }
        }

        protected void ddlHoteles_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            //confirmamos que las validaciones de la página se cumplan para poder ejecutar la accion de crear habitacion
            if (Page.IsValid == true)
            {
                try
                {
                    //capturamos los datos
                    int hotel = int.Parse(ddlHoteles.Text);
                    string numeroHabitacion = txtNumHabitacion.Text;
                    int capacidadMax = int.Parse(txtCapacidadMax.Text);  
                    string descripcion = txtDescripcion.Text;
                    char estado = 'A';

                    //realizamos la conexion a la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        //llamamos al procedimiento almacenado de la BD y le pasamos los parametros necesarios
                        db.SpCrearHabitacion(hotel,numeroHabitacion,capacidadMax,descripcion,estado);
                    }

                }
                catch
                {
                    //agregar un error
                }
            }
        }

    }
}