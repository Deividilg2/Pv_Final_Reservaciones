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

namespace Pv_Final_Reservaciones.Pages
{
    public partial class CrearReservacion : System.Web.UI.Page
    {
        String conn = ConfigurationManager.ConnectionStrings["Conn"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["Usuario"] == null)
            {
                Response.Redirect("~/Pages/Login.aspx");
            }
            if (Page.IsPostBack == false)
            {
                try
                {
                    CargarHoteles();
                    CargarClientes();
                }
                catch
                {

                }
            }
        }

        protected void CargarHoteles() {
            //creamos una lista
            var lista = new List<ListItem>();

            //para que aparezca un mensaje en el dropdown como primer item de la lista
            lista.Add(new ListItem("Seleccione un hotel", "0"));

            //Lista Dinamica usando la BD
            //Realizamos la conexión con la BD
            using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
            {
                var query = db.SpConsultarHoteles().Select(H => new ListItem(H.Nombre, H.IdHotel.ToString())).ToList();

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

        protected void CargarClientes()
        {
            Usuario usuario = (Usuario)Session["Usuario"];
            //creamos una lista
            var lista = new List<ListItem>();
            //para que aparezca un mensaje en el dropdown como primer item de la lista
            lista.Add(new ListItem("Seleccione un cliente", "0"));
           
                //Realizamos la conexión con la BD
                using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                {
                    var query = db.SpConsuntarPersonas().Select(S => new ListItem(S.NombreCompleto, S.IdPersona.ToString()))
                        .ToList();
                    lista.AddRange(query);
                }

                ddlClientes.DataSource = lista;
                //antes de hacer el databind hay que agregar el datatextfield, indicando cual es el campo que
                //queremos se muestre como texto y cual como valor
                ddlClientes.DataTextField = "Text";
                ddlClientes.DataValueField = "Value";
                ddlClientes.DataBind();
             if (usuario.esEmpleado)
            {
                //para que se coloque ya una de las opciones predeterminadas
                ddlClientes.Items.FindByValue("0").Selected = true;
            }
            else
            {
                //Cargamos el id del cliente para cargar su nombre
                string ddlusuario = usuario.id.ToString();
                //Seleccionamos su nombre por defecto
                ddlClientes.Items.FindByValue(ddlusuario).Selected = true;
                //Bloqueamos la opci[on de utilizar el DDL
                ddlClientes.Enabled= false;
            }
        }

        protected void cvFechaEntrada_ServerValidate(object source, ServerValidateEventArgs args)
        {
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
            }
        }

        protected void cvFechaSalida_ServerValidate(object source, ServerValidateEventArgs args)
        {
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
        {
            if (Page.IsValid == true)
            {
                try
                {
                    int idHotel = Int32.Parse(ddlHoteles.SelectedItem.Value);
                    int idpersona = Int32.Parse(ddlClientes.SelectedItem.Value);
                    DateTime fechaEntrada = DateTime.Parse(txtFechaEntrada.Text);
                    DateTime fechaSalida = DateTime.Parse(txtFechaSalida.Text);
                    int numeroAdultos = Int32.Parse(txtNadultos.Text);
                    int numeroNihos = Int32.Parse(txtNnihos.Text);
                    int sumaCapacidad = numeroAdultos + numeroNihos;
                    int totalDiasReservacion = (int)(fechaSalida - fechaEntrada).TotalDays;
                    //Validamos que no sobrepase la capacidad de las habitaciones
                    if (sumaCapacidad > 4)
                    {
                        lblMensajeCapacidad.Text = "La cantidad de personas sobre pasa la capacidad de las habitaciones";
                    }
                    using (PvProyectoFinalDB db = new PvProyectoFinalDB(new DataOptions().UseSqlServer(conn)))
                    {
                        var precio = db.SpConsultarDetallePorId(idpersona).FirstOrDefault();

                        decimal costoTotal = (decimal)precio.CostoTotal;
                        if (precio != null)
                        {
                            db.SpCrearReservacion(idpersona,idHotel, fechaEntrada, fechaSalida,numeroAdultos,numeroNihos,totalDiasReservacion,costoTotal);
                        }





                        /*
                        var precio= db.SpConsultarHoteles()
                        decimal costoPorCadaAdulto = 
                        decimal costoTotal;
                        //Tomamos los valores que requerimos para la creacion de la reservacion
                        var hotel = db.SpConsultarHabitacionesDeHotel(sumaCapacidad, idHotel).FirstOrDefault();
                        var datosReservacion = db.SpCrearReservacion(idHotel,hotel.IdHabitacion,fechaEntrada,fechaSalida,numeroAdultos,numeroNihos, totalDiasReservacion, costoTotal);
                        var idReservacion = db.SpConsultarReservacionPorID
                        var bitacora = db.SpInsertarReservacionEnBitacora(idpersona,);
                        */
                    }
                }
                catch
                {
                  
                }
            }
             

            
        }
    }
}
/*
 ALTER PROCEDURE [dbo].[spCrearReservacion]
@idPersona INT,
@idHotel INT,
@fechaEntrada DATETIME,
@fechaSalida DATETIME,
@numeroAdultos INT,
@numeroNinhos INT,
@totalDiasReservacion INT,
@costoTotal NUMERIC(14, 2)
AS
BEGIN
DECLARE @fechaCreacion DATETIME = GETDATE();

INSERT INTO Reservacion (idPersona, fechaEntrada, fechaSalida, 
numeroAdultos, numeroNinhos, totalDiasReservacion, costoTotal, fechaCreacion, estado)
VALUES (@idPersona, @fechaEntrada, @fechaSalida, @numeroAdultos, 
@numeroNinhos, @totalDiasReservacion,@costoTotal,@fechaCreacion, 'A'); 
END;
 */
//---------------------------------------------------------------------------
/*
 ALTER PROCEDURE [dbo].[spConsultarHoteles]
AS
BEGIN
SELECT idHotel, nombre
FROM Hotel
ORDER BY nombre asc;
END;
 */

/*
 CREATE PROCEDURE spInsertarReservacionEnBitacora
--Sp que nos permite agregar acciones de creacion de reservaciones a la bitacora
@idPersona INT,
@idReservacion INT -- Si es necesario para el registro
AS
BEGIN
-- Insertar un nuevo registro en la tabla Bitacora
INSERT INTO Bitacora (fechaDeLaAccion, accionRealizada, idPersona, idReservacion)
VALUES (GETDATE(), 'CREADA', @idPersona, @idReservacion);
END;
 */
//---------------------------------------------------------------------------------------------------------
/*
CREATE PROCEDURE spConsultarHabitacionesDeHotel
--Sp que nos permite seleccionar la habitacion que tenga suficiente capacidad y que sea la menos reservada
@totalPersonas INT,
@idHotel INT
AS
BEGIN
DECLARE @idHabitacion INT;
-- Seleccionamos la habitación con estado "A" que tenga capacidad suficiente y tenga menos reservaciones
SELECT TOP 1 @idHabitacion = h.idHabitacion
FROM Habitacion h
LEFT JOIN Reservacion R ON h.idHabitacion = r.idHabitacion
WHERE h.estado = 'A'
AND h.capacidadMaxima >= @totalPersonas AND h.idHotel = @idHotel
GROUP BY h.idHabitacion, h.capacidadMaxima, h.estado
ORDER BY COUNT(r.idReservacion) ASC;

-- Si es que encuentra una habitación disponible
IF @idHabitacion IS NOT NULL
BEGIN
SELECT @idHabitacion AS idHabitacion;
END
END;
 */
//-------------------------------------------------------------------
//Este lo cambie porque solo debo mostrar en el ddlclientes las personas activas
/*
 USE [PV_ProyectoFinal]
GO
 StoredProcedure [dbo].[spConsuntarPersonas]
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER procedure [dbo].[spConsuntarPersonas]
AS
BEGIN
--Seleccionamos las personas para el dropdownlist
SELECT idPersona,nombreCompleto,estado
FROM Persona
WHERE estado = 'A'
ORDER BY idPersona asc
END;
 */