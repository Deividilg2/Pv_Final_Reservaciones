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
    public partial class CrearHabitacion : System.Web.UI.Page
    {
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
            if (Page.IsPostBack == false)
            {
                try
                {
                    //creamos una lista
                    var lista = new List<ListItem>();

                    //para que aparezca un mensaje en el dropdown como primer item de la lista
                    lista.Add(new ListItem("Seleccione un hotel", ""));

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
                    ddlHoteles.Items.FindByValue("").Selected = true;
                }
                catch
                {

                }
            }
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
                        // Verificamos si ya existe una habitación con el mismo número en el mismo hotel
                        var habitacionesDuplicadas = db.SpVerificarHabitacionDuplicada(hotel, numeroHabitacion).FirstOrDefault();

                        if (habitacionesDuplicadas.HabitacionesDuplicadas > 0)
                        {
                            lblMensajeError.Text = "Ya existe una habitación con el mismo número en el hotel seleccionado.";
                        }
                        else
                        {
                            // Llamamos al procedimiento almacenado de la BD y le pasamos los parámetros necesarios
                            db.SpCrearHabitacion(hotel, numeroHabitacion, capacidadMax, descripcion, estado);
                            Response.Redirect("~/Pages/Resultado.aspx?source=CrearHabitacion", false);
                        }
                    }

                }
                catch
                {
                    //agregar un error
                    Response.Redirect("~/Pages/Errores.aspx?source=Errorhabitacion", false);
                }
            }
        }

    }
}