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
            try
            {
                //Validamos que se coloquen datos
                if (!String.IsNullOrEmpty(email) && !String.IsNullOrEmpty(clave))
                {
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        //Utilizamos el StoreProcedure para validar la informacion del usuario y tomamos el primer resultado por defecto
                        var log = db.SpLogin(email, clave).FirstOrDefault();
                        //Validamos en caso de que la cuenta sea inactiva "I"
                        if (log != null)
                        {
                            
                            if (log.Estado != 'I')
                            {
                                Usuario usuario = new Usuario();
                                usuario.id = log.IdPersona;
                                usuario.nombreCompleto  = log.NombreCompleto;
                                usuario.esEmpleado = log.EsEmpleado;
                                if (log.EsEmpleado)
                                {
                                    Session["Usuario"] = usuario;
                                    Response.Redirect("~/Pages/GestionarReservaciones.aspx");
                                    
                                }
                                else if (log.EsEmpleado == false)
                                {
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
                    Response.Write("El correo o la contraseña son incorrectas, por favor verificar credenciales");
                }

            }
            catch
            {


            }

        }
    }
}