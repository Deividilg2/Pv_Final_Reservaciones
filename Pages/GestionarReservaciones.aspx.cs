using DataModels;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;


namespace Pv_Final_Reservaciones.Pages
{
    
    public partial class GestionarReservaciones : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            //Validamos la sesión del usuario
            //Usamos un try en caso de errores
            try
            {
                //Realizamos la conexión con la BD
                using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                {
                    //Código momentaneo para recargar lista con el boton filtrar (Borrar a futuro)
                    var lista = db.SpConsultarReservaciones().ToList();
                    grdReservaciones.DataSource = lista;
                    grdReservaciones.DataBind();

                    if (!IsPostBack)
                    {
                        try
                        {
                            //Cargamos todas las reservaciones
                            



                            //Cargamos a las personas en el DropDownList
                            var personas = db.SpConsuntarPersonas().ToList();
                            //Asignamos la fuente de datos con DataSource
                            ddlClientes.DataSource = personas;
                            //Asignamos que campo queremos extraer con DataTextField
                            ddlClientes.DataTextField = "nombreCompleto";
                            //Asignamos un valor para darle a cada campo extraido con DataValueField
                            //Esto se usa en la linea 108
                            ddlClientes.DataValueField = "idPersona";
                            //Unificamos o relacionamos los dos campos de DataTextField y DataValueField
                            ddlClientes.DataBind();
                        }
                        catch
                        {


                        }

                    }
                }
                

            }
            catch
            {

            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            try
            {               

                //Variables para realizar el filtro de busqueda
                DateTime fechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
                DateTime fechaSalida = DateTime.Parse(txtFechaSalida.Text);

                //Condicion para evitar error de busqueda
                if (fechaSalida < fechaEntrada)
                {
                    Response.Redirect("~/Pages/Misreservaciones.aspx");
                    //Esto lo utilizo de momento porque no hay validación de error para mostrar
                }
                else
                {     
                    
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        var lista = db.SpFiltrar(fechaEntrada, fechaSalida).ToList();
                        grdReservaciones.DataSource = lista;
                        grdReservaciones.DataBind();
                        //Esta parte no funciona de momento--Preguntar al profe
                        ddlClientes.Items.Insert(0, "Clientes");

                    }
                }
                
            }
            catch
            {

            }
        }

        //Acción que permite seleccionar el cliente que se desea filtrar utilizando el DropDownList 
        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
            {
                //Tomamos el id de la persona seleccionada y lo parseamos a int
                    int idPersona = Int32.Parse(ddlClientes.SelectedValue);
                //Utilizamos el int para pasarlo como parametro al StoreProcedure y filtrar los datos de la persona
                    var filtroPersonas = db.SpFiltroPorID(idPersona);
                //Mostramos los datos de la persona en el gridview
                    grdReservaciones.DataSource= filtroPersonas;
                    grdReservaciones.DataBind();
            }

        }
    }
}