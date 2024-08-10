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
    public partial class CrearReservacion : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {//Validamos la sesion del usuario
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            //Validamos que la pagina se carga por primera vez
            if (Page.IsPostBack == false)
            {
                try
                {
                    //Asignamos valores por defecto
                    txtNadultos.Text = "1";
                    txtNnihos.Text = "0";

                    CargarHoteles();//Cargamos  el DDL de los hoteles
                    CargarClientes();//Cargamos el DDL de los clientes 
                }
                catch
                {
                    Response.Redirect("~/Pages/Errores.aspx");
                }
            }
        }
        //Metodo para cargar la lista de los hoteles en un DDL
        protected void CargarHoteles() {
            //creamos una lista para asignar a los hoteles
            var lista = new List<ListItem>();

            //para que aparezca un mensaje en el dropdown como primer item de la lista
            lista.Add(new ListItem("Seleccione un hotel", ""));

            //Lista Dinamica usando la BD
            //Realizamos la conexión con la BD
            using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
            {//Consulta especifica para traer los datos necesarios
                var query = db.SpConsultarHoteles().Select(H => new ListItem(H.Nombre, H.IdHotel.ToString())).ToList();
                //Agregamos los datos a la lista
                lista.AddRange(query);
            }

            ddlHoteles.DataSource = lista;//Pasamos la lista 
            //antes de hacer el databind hay que agregar el datatextfield, indicando cual es el campo que
            //queremos se muestre como texto y cual como valor
            ddlHoteles.DataTextField = "Text";
            ddlHoteles.DataValueField = "Value";
            ddlHoteles.DataBind();

            //para que se coloque ya una de las opciones predeterminadas
            ddlHoteles.Items.FindByValue("").Selected = true;
        }
        //Metodo para cargar la lista de los clientes en un DDL
        protected void CargarClientes()
        {
            //Creamos una instancia del usuario para hacer uso de sus atributos
            Usuario usuario = (Usuario)Session["Usuario"];
            //creamos una lista
            var lista = new List<ListItem>();
            //para que aparezca un mensaje en el dropdown como primer item de la lista
            lista.Add(new ListItem("Seleccione un cliente", ""));
           
            //Realizamos la conexión con la BD
            using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
            {
                var query = db.SpConsultarPersonas(usuario.id).Select(S => new ListItem(S.NombreCompleto, S.IdPersona.ToString()))
                    .ToList();
                lista.AddRange(query);
            }
            //Si el usuario es empleado y tiene el estado de cliente en false entramos
            if (usuario.esEmpleado && usuario.Estado == true)
            {   //Eliminamos de la lista al empleado para que no pueda seleccionar/filtrar sus propias reservaciones
                lista = lista.Where(item => item.Value != usuario.id.ToString()).ToList();
                ddlClientes.DataSource = lista;
                //antes de hacer el databind hay que agregar el datatextfield, indicando cual es el campo que
                //queremos se muestre como texto y cual como valor
                ddlClientes.DataTextField = "Text";
                ddlClientes.DataValueField = "Value";
                ddlClientes.DataBind();
                //para que se coloque ya una de las opciones predeterminadas
                ddlClientes.Items.FindByValue("").Selected = true;
            }
            else
            {
                ddlClientes.DataSource = lista;
                //antes de hacer el databind hay que agregar el datatextfield, indicando cual es el campo que
                //queremos se muestre como texto y cual como valor
                ddlClientes.DataTextField = "Text";
                ddlClientes.DataValueField = "Value";
                ddlClientes.DataBind();
                //Cargamos el id del cliente para cargar su nombre
                string ddlusuario = usuario.id.ToString();
                //Seleccionamos su nombre por defecto
                ddlClientes.Items.FindByValue(ddlusuario).Selected = true;
                //Bloqueamos la opcion de utilizar el DDL
                ddlClientes.Enabled = false;
            }
        }

        protected void cvFechaEntrada_ServerValidate(object source, ServerValidateEventArgs args)
        {//Validamos que la fecha de entrada sea mayor o igual a la actual
            try
            {
                //Tomamos por falso el valor desde un inicio
                args.IsValid = false;
                //Si el valor no es nulo validamos la condición
                if (args.Value != null) 
                { //Si la fecha es mayor o igual entonces es valida
                    if(DateTime.Parse(args.Value) >= DateTime.Today) 
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

        protected void cvFechaSalida_ServerValidate(object source, ServerValidateEventArgs args)
        {//Validamos que la fecha de salida sea mayor o igual a la fecha de entrada
            try
            {
                //Validamos como falso el argumento desde el principio
                args.IsValid = false;
                //Validamos que no venga nulo
                if (args.Value != null)
                {//Realizamos la accion de la condicional y validamos true si pasa
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

        protected void btnRegresar_Click(object sender, EventArgs e)
        {
            // Recuperamos el objeto Usuario de la sesión
            Usuario usuario = (Usuario)Session["Usuario"];
            //En caso de que el usuario no se logre tomar 
            if (usuario != null)
            {
                // Realizamos una comprobación de si es o no empleado el usuario logeado
                if (usuario.esEmpleado)
                {
                    Response.Redirect("~/Pages/GestionarReservaciones.aspx");
                }
                else
                {
                    Response.Redirect("~/Pages/Misreservaciones.aspx");
                }
            }
            else
            {
                // Si no se puede recuperar el usuario de la sesión, redirigimos a la página de inicio de sesión
                Response.Redirect("~/Pages/Login.aspx");
            }
        }

        protected void btnGuardar_Click(object sender, EventArgs e)
        {//Comprobamos que entre si las validaciones son correctas entre
            if (Page.IsValid == true)
            {
                try
                {//Tomamos los datos
                    int idHotel = Int32.Parse(ddlHoteles.SelectedItem.Value);
                    int idPersona = Int32.Parse(ddlClientes.SelectedItem.Value);
                    DateTime fechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
                    DateTime fechaSalida = DateTime.Parse(txtFechaSalida.Text);
                    int numeroAdultos = Int32.Parse(txtNadultos.Text);
                    int numeroNihos = Int32.Parse(txtNnihos.Text);
                    int totalPersonas = numeroAdultos + numeroNihos;
                    int totalDiasReservacion = (int)(fechaSalida - fechaEntrada).TotalDays;

                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {//Consultamos una habitacion que pueda almacenar al total de personas
                        var habitacion = db.SpConsultarHabitacionesDeHotel(totalPersonas, idHotel).FirstOrDefault();
                        if (habitacion != null)//Si viene null controlamos el error
                        {//Si hay una habitacion que pueda almacenar entonces que entre
                            if (habitacion.Capacidad >= totalPersonas)
                            {//Llamamos al procedimiento spCrearReservacion
                                var idReservacion = db.SpCrearReservacion(idPersona, idHotel, habitacion.IdHabitacion, fechaEntrada, fechaSalida, numeroAdultos, numeroNihos).FirstOrDefault();
                                int tomarID = (int)idReservacion.IdReservacion;//Tomamos el id de la reservacion recien ingresada
                                Response.Redirect("~/Pages/Resultado?id=" + tomarID + "&source=CrearReservacion", false);
                            }
                            else
                            {
                                lblMensajeCapacidad.Text = "Demasiadas personas para la habitación, máximo alcanzan " + habitacion.Capacidad;
                            }
                        }
                        else
                        {//Error de cantidad maxima en la habitacion superada
                            Response.Redirect("~/Pages/Errores?source=ErrorCrearReservacion", false);
                        }
                    }
                }
                catch
                {
                    // Manejo de errores
                    Response.Redirect("~/Pages/Errores.aspx?source=ErrorCrearReservacion");
                }
            }
        }
    }
    
}
