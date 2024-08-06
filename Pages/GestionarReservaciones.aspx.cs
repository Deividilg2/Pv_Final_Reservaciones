using DataModels;
using LinqToDB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using Pv_Final_Reservaciones.Clases;


namespace Pv_Final_Reservaciones.Pages
{

    public partial class GestionarReservaciones : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //Validamos la sesión del usuario activa
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");

            }
            Usuario usuario = (Usuario)Session["Usuario"];
            if (!usuario.esEmpleado)
            {
                Response.Redirect("~/Pages/Errores.aspx?source=ErrorUrl", false);
            }

            if (Page.IsPostBack == false)
            {
                //Usamos un try en caso de errores
                try
                {
                    //Creamos la lista para mostrar en el DDL
                    var listaddl = new List<ListItem>();
                    //Añadimos la opción por defecto
                    listaddl.Add(new ListItem("Seleccione un cliente", "0"));
                    //Realizamos la conexión con la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        //Código momentaneo para Cargar las reservaciones
                        var listareservaciones = db.SpConsultarReservaciones().ToList();
                        grdReservaciones.DataSource = listareservaciones;
                        grdReservaciones.DataBind();

                        if (!IsPostBack)
                        {
                            //Cargamos a las personas en el DropDownList
                            var personas = db.SpConsuntarPersonas()
                                .Select(S => new ListItem(S.NombreCompleto, S.IdPersona.ToString())).ToList();
                            //Agregamos lo anterior a la lista
                            listaddl.AddRange(personas);
                        }
                    }


                    //Asignamos la fuente de datos con DataSource
                    ddlClientes.DataSource = listaddl;
                    //Asignamos que campo queremos extraer con DataTextField
                    ddlClientes.DataTextField = "Text";
                    //Asignamos un valor para darle a cada campo extraido con DataValueField
                    ddlClientes.DataValueField = "Value";
                    //Unificamos o relacionamos los dos campos de DataTextField y DataValueField
                    ddlClientes.DataBind();
                    //Seleccionamos una opción a mostrar por defecto al cargar
                    ddlClientes.Items.FindByValue("0").Selected = true;

                }
                catch
                {

                }
            }
        }

        protected void btnFiltrar_Click(object sender, EventArgs e)
        {
            if(Page.IsValid == true)
            {
                try
                {
                    //Variables para realizar el filtro de busqueda
                    DateTime fechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
                    DateTime fechaSalida = DateTime.Parse(txtFechaSalida.Text);
                    //Carga el filtro después de la validación realizada
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        var lista = db.SpFiltrar(fechaEntrada, fechaSalida).ToList();
                        grdReservaciones.DataSource = lista;
                        grdReservaciones.DataBind();
                    }
                }
                catch
                {

                }
            }
            
        }

        //Acción que permite seleccionar el cliente que se desea filtrar utilizando el DropDownList 
        protected void ddlClientes_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            String Personaselec = ddlClientes.SelectedItem.Value;
            using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
            {
                var lista = db.SpFiltroPorID(Int32.Parse(Personaselec));
                grdReservaciones.DataSource = lista;
                grdReservaciones.DataBind();
                //Excepción para recargar las reservaciones
                if (Personaselec == "0")
                {
                    //Código Para recargar las reservaciones
                    var listareservaciones = db.SpConsultarReservaciones().ToList();
                    grdReservaciones.DataSource = listareservaciones;
                    grdReservaciones.DataBind();
                }
            }
        }

        protected void cvFechaSalida_ServerValidate(object source, ServerValidateEventArgs args)
        {
            try
            {
                args.IsValid = false;
                if (args.Value != null)
                {
                    if (DateTime.Parse(args.Value) >= DateTime.Parse(txtFechaEntrada.Text))
                    {
                        args.IsValid = true;
                    }
                }
            }
            catch
            {
                args.IsValid = false;
            }
        }

        protected void btnNuevareservacion_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Pages/CrearReservacion.aspx");
        }
    }
}
