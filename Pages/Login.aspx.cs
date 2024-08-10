﻿using DataModels;
using LinqToDB;
using Pv_Final_Reservaciones.Clases;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

/*
 * Deivid
 <connectionStrings>
		<add name="Conn" providerName="System.Data.SqlClient"
		connectionString="Data Source=DEIVIDILG\DEIVIDILG;Database=PV_ProyectoFinal;Integrated Security=True;TrustServerCertificate=True" />
	</connectionStrings>
Camila
	<connectionStrings>
<add name="Conn" providerName="System.Data.SqlClient"
		connectionString="Data Source=Camila;Database=PV_ProyectoFinal;Integrated Security=True;TrustServerCertificate=True" />
</connectionStrings>
	
//LoadSqlServerMetadata("Data Source=Camila;Database=PV_ProyectoFinal;Integrated Security=True");
//LoadSqlServerMetadata("Data Source=DEIVIDILG\\DEIVIDILG;Database=PV_ProyectoFinal;Integrated Security=True");
*/

namespace Pv_Final_Reservaciones.Pages
{
    public partial class Login : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {


        }

        protected void btnlogin_Click(object sender, EventArgs e)
        {
            //Tomamos la información de los textBox
            String email = txtemail.Text;
            String clave = txtclave.Text;
            //Colocamos try en caso de error
            try
            {
                //Validamos que se coloquen datos
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(clave))
                {
                    //Realizamos la conexión con la BD
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        //Utilizamos el StoreProcedure para validar la informacion del usuario y tomamos el primer resultado por defecto
                        var log = db.SpLogin(email, clave).FirstOrDefault();//Utilizamos FirstOrDefault para que la BD entienda
                                                                            //que deseamos traer la única fila que retorna
                       //Validamos que no venga nulo log
                        if (log != null)
                        {
                            //Validamos en caso de que la cuenta sea inactiva "I"
                            if (log.Estado != 'I')
                            {
                                //Creamos una instancia de usuario para utilizar sus atributos
                                Usuario usuario = new Usuario();
                                usuario.id = log.IdPersona;
                                usuario.nombreCompleto  = log.NombreCompleto;
                                usuario.esEmpleado = log.EsEmpleado;

                                // Guardar idPersona en la sesión
                                Session["idPersona"] = log.IdPersona;

                                //Realizamos una comprovación de si es o no empleado el usuario logeado
                                if (log.EsEmpleado)
                                {
                                    usuario.Estado = true;
                                    Session["Usuario"] = usuario;
                                    Response.Redirect("~/Pages/GestionarReservaciones.aspx");
                                    
                                }
                                else if (log.EsEmpleado == false)
                                {
                                    usuario.Estado = false;
                                    Session["Usuario"] = usuario;
                                    Response.Redirect($"~/Pages/Misreservaciones.aspx");
                                }
                            }                            
                        }
                        else
                        {
                            Response.Write("Credenciales incorrectas o el usuario se encuentra inactivo");
                        }

                    }
                }
                else
                {
                    Response.Write("Es requerido rellenar ambos campos con credenciales");
                }

            }
            catch
            {


            }

        }
    }
}