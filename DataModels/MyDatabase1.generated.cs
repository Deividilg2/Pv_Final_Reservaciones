//---------------------------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated by T4Model template for T4 (https://github.com/linq2db/linq2db).
//    Changes to this file may cause incorrect behavior and will be lost if the code is regenerated.
// </auto-generated>
//---------------------------------------------------------------------------------------------------

#pragma warning disable 1573, 1591

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

using LinqToDB;
using LinqToDB.Common;
using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB.Mapping;

namespace DataModels
{
	/// <summary>
	/// Database       : PV_ProyectoFinal
	/// Data Source    : Camila
	/// Server Version : 16.00.1000
	/// </summary>
	public partial class PvProyectoFinalDB : LinqToDB.Data.DataConnection
	{
		public ITable<Bitacora>    Bitacoras    { get { return this.GetTable<Bitacora>(); } }
		public ITable<Habitacion>  Habitacions  { get { return this.GetTable<Habitacion>(); } }
		public ITable<Hotel>       Hotels       { get { return this.GetTable<Hotel>(); } }
		public ITable<Persona>     Personas     { get { return this.GetTable<Persona>(); } }
		public ITable<Reservacion> Reservacions { get { return this.GetTable<Reservacion>(); } }

		public PvProyectoFinalDB()
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PvProyectoFinalDB(string configuration)
			: base(configuration)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PvProyectoFinalDB(DataOptions options)
			: base(options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		public PvProyectoFinalDB(DataOptions<PvProyectoFinalDB> options)
			: base(options.Options)
		{
			InitDataContext();
			InitMappingSchema();
		}

		partial void InitDataContext  ();
		partial void InitMappingSchema();
	}

	[Table(Schema="dbo", Name="Bitacora")]
	public partial class Bitacora
	{
		[Column("idBitacora"),      PrimaryKey, Identity] public int      IdBitacora      { get; set; } // int
		[Column("idReservacion"),   NotNull             ] public int      IdReservacion   { get; set; } // int
		[Column("idPersona"),       NotNull             ] public int      IdPersona       { get; set; } // int
		[Column("accionRealizada"), NotNull             ] public string   AccionRealizada { get; set; } // varchar(25)
		[Column("fechaDeLaAccion"), NotNull             ] public DateTime FechaDeLaAccion { get; set; } // datetime

		#region Associations

		/// <summary>
		/// FK_Bitacora_Persona (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Persona { get; set; }

		/// <summary>
		/// FK_Bitacora_Reservacion (dbo.Reservacion)
		/// </summary>
		[Association(ThisKey="IdReservacion", OtherKey="IdReservacion", CanBeNull=false)]
		public Reservacion Reservacion { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Habitacion")]
	public partial class Habitacion
	{
		[Column("idHabitacion"),     PrimaryKey, Identity] public int    IdHabitacion     { get; set; } // int
		[Column("idHotel"),          NotNull             ] public int    IdHotel          { get; set; } // int
		[Column("numeroHabitacion"), NotNull             ] public string NumeroHabitacion { get; set; } // varchar(10)
		[Column("capacidadMaxima"),  NotNull             ] public int    CapacidadMaxima  { get; set; } // int
		[Column("descripcion"),      NotNull             ] public string Descripcion      { get; set; } // varchar(500)
		[Column("estado"),           NotNull             ] public char   Estado           { get; set; } // varchar(1)

		#region Associations

		/// <summary>
		/// FK_Habitacion_Hotel (dbo.Hotel)
		/// </summary>
		[Association(ThisKey="IdHotel", OtherKey="IdHotel", CanBeNull=false)]
		public Hotel Hotel { get; set; }

		/// <summary>
		/// FK_Reservacion_Habitacion_BackReference (dbo.Reservacion)
		/// </summary>
		[Association(ThisKey="IdHabitacion", OtherKey="IdHabitacion", CanBeNull=true)]
		public IEnumerable<Reservacion> Reservacions { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Hotel")]
	public partial class Hotel
	{
		[Column("idHotel"),            PrimaryKey,  Identity] public int     IdHotel            { get; set; } // int
		[Column("nombre"),             NotNull              ] public string  Nombre             { get; set; } // varchar(150)
		[Column("direccion"),             Nullable          ] public string  Direccion          { get; set; } // varchar(500)
		[Column("costoPorCadaAdulto"), NotNull              ] public decimal CostoPorCadaAdulto { get; set; } // numeric(10, 2)
		[Column("costoPorCadaNinho"),  NotNull              ] public decimal CostoPorCadaNinho  { get; set; } // numeric(10, 2)

		#region Associations

		/// <summary>
		/// FK_Habitacion_Hotel_BackReference (dbo.Habitacion)
		/// </summary>
		[Association(ThisKey="IdHotel", OtherKey="IdHotel", CanBeNull=true)]
		public IEnumerable<Habitacion> Habitacions { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Persona")]
	public partial class Persona
	{
		[Column("idPersona"),      PrimaryKey, Identity] public int    IdPersona      { get; set; } // int
		[Column("nombreCompleto"), NotNull             ] public string NombreCompleto { get; set; } // varchar(250)
		[Column("email"),          NotNull             ] public string Email          { get; set; } // varchar(150)
		[Column("clave"),          NotNull             ] public string Clave          { get; set; } // varchar(15)
		[Column("esEmpleado"),     NotNull             ] public bool   EsEmpleado     { get; set; } // bit
		[Column("estado"),         NotNull             ] public char   Estado         { get; set; } // varchar(1)

		#region Associations

		/// <summary>
		/// FK_Bitacora_Persona_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoras { get; set; }

		/// <summary>
		/// FK_Reservacion_Persona_BackReference (dbo.Reservacion)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=true)]
		public IEnumerable<Reservacion> Reservacions { get; set; }

		#endregion
	}

	[Table(Schema="dbo", Name="Reservacion")]
	public partial class Reservacion
	{
		[Column("idReservacion"),        PrimaryKey,  Identity] public int       IdReservacion        { get; set; } // int
		[Column("idPersona"),            NotNull              ] public int       IdPersona            { get; set; } // int
		[Column("idHabitacion"),         NotNull              ] public int       IdHabitacion         { get; set; } // int
		[Column("fechaEntrada"),         NotNull              ] public DateTime  FechaEntrada         { get; set; } // datetime
		[Column("fechaSalida"),          NotNull              ] public DateTime  FechaSalida          { get; set; } // datetime
		[Column("numeroAdultos"),        NotNull              ] public int       NumeroAdultos        { get; set; } // int
		[Column("numeroNinhos"),         NotNull              ] public int       NumeroNinhos         { get; set; } // int
		[Column("totalDiasReservacion"), NotNull              ] public int       TotalDiasReservacion { get; set; } // int
		[Column("costoPorCadaAdulto"),   NotNull              ] public decimal   CostoPorCadaAdulto   { get; set; } // numeric(10, 2)
		[Column("costoPorCadaNinho"),    NotNull              ] public decimal   CostoPorCadaNinho    { get; set; } // numeric(10, 2)
		[Column("costoTotal"),           NotNull              ] public decimal   CostoTotal           { get; set; } // numeric(14, 2)
		[Column("fechaCreacion"),        NotNull              ] public DateTime  FechaCreacion        { get; set; } // datetime
		[Column("fechaModificacion"),       Nullable          ] public DateTime? FechaModificacion    { get; set; } // datetime
		[Column("estado"),               NotNull              ] public char      Estado               { get; set; } // varchar(1)

		#region Associations

		/// <summary>
		/// FK_Bitacora_Reservacion_BackReference (dbo.Bitacora)
		/// </summary>
		[Association(ThisKey="IdReservacion", OtherKey="IdReservacion", CanBeNull=true)]
		public IEnumerable<Bitacora> Bitacoras { get; set; }

		/// <summary>
		/// FK_Reservacion_Habitacion (dbo.Habitacion)
		/// </summary>
		[Association(ThisKey="IdHabitacion", OtherKey="IdHabitacion", CanBeNull=false)]
		public Habitacion Habitacion { get; set; }

		/// <summary>
		/// FK_Reservacion_Persona (dbo.Persona)
		/// </summary>
		[Association(ThisKey="IdPersona", OtherKey="IdPersona", CanBeNull=false)]
		public Persona Persona { get; set; }

		#endregion
	}

	public static partial class PvProyectoFinalDBStoredProcedures
	{
		#region ConsultarBitacoras

		public static IEnumerable<ConsultarBitacorasResult> ConsultarBitacoras(this PvProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<ConsultarBitacorasResult>("[dbo].[ConsultarBitacoras]");
		}

		public partial class ConsultarBitacorasResult
		{
			[Column("fechaDeLaAccion")] public DateTime FechaDeLaAccion { get; set; }
			[Column("accionRealizada")] public string   AccionRealizada { get; set; }
			[Column("nombreCompleto") ] public string   NombreCompleto  { get; set; }
		}

		#endregion

		#region SpCancelarReservacionYRegistrarBitacora

		public static IEnumerable<SpCancelarReservacionYRegistrarBitacoraResult> SpCancelarReservacionYRegistrarBitacora(this PvProyectoFinalDB dataConnection, int? @idReservacion, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idReservacion", @idReservacion, LinqToDB.DataType.Int32),
				new DataParameter("@idPersona",     @idPersona,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpCancelarReservacionYRegistrarBitacoraResult>("[dbo].[spCancelarReservacionYRegistrarBitacora]", parameters);
		}

		public partial class SpCancelarReservacionYRegistrarBitacoraResult
		{
			public string Resultado { get; set; }
		}

		#endregion

		#region SpConsultarBitacora

		public static IEnumerable<SpConsultarBitacoraResult> SpConsultarBitacora(this PvProyectoFinalDB dataConnection, int? @idReservacion, int? @idUsuario)
		{
			var parameters = new []
			{
				new DataParameter("@idReservacion", @idReservacion, LinqToDB.DataType.Int32),
				new DataParameter("@idUsuario",     @idUsuario,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarBitacoraResult>("[dbo].[spConsultarBitacora]", parameters);
		}

		public partial class SpConsultarBitacoraResult
		{
			[Column("fechaDeLaAccion")] public DateTime FechaDeLaAccion { get; set; }
			[Column("accionRealizada")] public string   AccionRealizada { get; set; }
			[Column("nombreCompleto") ] public string   NombreCompleto  { get; set; }
			[Column("idReservacion")  ] public int?     IdReservacion   { get; set; }
			[Column("idBitacora")     ] public int      IdBitacora      { get; set; }
		}

		#endregion

		#region SpConsultarBitacoraEmpleado

		public static IEnumerable<SpConsultarBitacoraEmpleadoResult> SpConsultarBitacoraEmpleado(this PvProyectoFinalDB dataConnection, int? @idReservacion)
		{
			var parameters = new []
			{
				new DataParameter("@idReservacion", @idReservacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarBitacoraEmpleadoResult>("[dbo].[spConsultarBitacoraEmpleado]", parameters);
		}

		public partial class SpConsultarBitacoraEmpleadoResult
		{
			[Column("fechaDeLaAccion")] public DateTime FechaDeLaAccion { get; set; }
			[Column("accionRealizada")] public string   AccionRealizada { get; set; }
			[Column("nombreCompleto") ] public string   NombreCompleto  { get; set; }
			[Column("idReservacion")  ] public int?     IdReservacion   { get; set; }
			[Column("idBitacora")     ] public int      IdBitacora      { get; set; }
		}

		#endregion

		#region SpConsultarCapacidadMaximadeHotel

		public static IEnumerable<SpConsultarCapacidadMaximadeHotelResult> SpConsultarCapacidadMaximadeHotel(this PvProyectoFinalDB dataConnection, int? @idHotel, int? @capacidadTotal)
		{
			var parameters = new []
			{
				new DataParameter("@idHotel",        @idHotel,        LinqToDB.DataType.Int32),
				new DataParameter("@capacidadTotal", @capacidadTotal, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarCapacidadMaximadeHotelResult>("[dbo].[spConsultarCapacidadMaximadeHotel]", parameters);
		}

		public partial class SpConsultarCapacidadMaximadeHotelResult
		{
			[Column("hotelCapacidadMx")] public int? HotelCapacidadMx { get; set; }
		}

		#endregion

		#region SpConsultarDetallePorId

		public static IEnumerable<SpConsultarDetallePorIdResult> SpConsultarDetallePorId(this PvProyectoFinalDB dataConnection, int? @idreservacion, int? @idUsuario)
		{
			var parameters = new []
			{
				new DataParameter("@idreservacion", @idreservacion, LinqToDB.DataType.Int32),
				new DataParameter("@idUsuario",     @idUsuario,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarDetallePorIdResult>("[dbo].[spConsultarDetallePorId]", parameters);
		}

		public partial class SpConsultarDetallePorIdResult
		{
			[Column("idReservacion")       ] public int      IdReservacion        { get; set; }
			[Column("idHabitacion")        ] public int?     IdHabitacion         { get; set; }
			[Column("numeroHabitacion")    ] public string   NumeroHabitacion     { get; set; }
			[Column("idHotel")             ] public int?     IdHotel              { get; set; }
			[Column("nombre")              ] public string   Nombre               { get; set; }
			[Column("fechaEntrada")        ] public DateTime FechaEntrada         { get; set; }
			[Column("fechaSalida")         ] public DateTime FechaSalida          { get; set; }
			[Column("numeroAdultos")       ] public int      NumeroAdultos        { get; set; }
			[Column("numeroNinhos")        ] public int      NumeroNinhos         { get; set; }
			[Column("totalDiasReservacion")] public int?     TotalDiasReservacion { get; set; }
			[Column("costoTotal")          ] public decimal? CostoTotal           { get; set; }
			[Column("estado")              ] public char     Estado               { get; set; }
			[Column("nombreCompleto")      ] public string   NombreCompleto       { get; set; }
		}

		#endregion

		#region SpConsultarDetallePorIdEmpleado

		public static IEnumerable<SpConsultarDetallePorIdEmpleadoResult> SpConsultarDetallePorIdEmpleado(this PvProyectoFinalDB dataConnection, int? @idreservacion)
		{
			var parameters = new []
			{
				new DataParameter("@idreservacion", @idreservacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarDetallePorIdEmpleadoResult>("[dbo].[spConsultarDetallePorIdEmpleado]", parameters);
		}

		public partial class SpConsultarDetallePorIdEmpleadoResult
		{
			[Column("idReservacion")       ] public int      IdReservacion        { get; set; }
			[Column("idHabitacion")        ] public int?     IdHabitacion         { get; set; }
			[Column("numeroHabitacion")    ] public string   NumeroHabitacion     { get; set; }
			[Column("idHotel")             ] public int?     IdHotel              { get; set; }
			[Column("nombre")              ] public string   Nombre               { get; set; }
			[Column("fechaEntrada")        ] public DateTime FechaEntrada         { get; set; }
			[Column("fechaSalida")         ] public DateTime FechaSalida          { get; set; }
			[Column("numeroAdultos")       ] public int      NumeroAdultos        { get; set; }
			[Column("numeroNinhos")        ] public int      NumeroNinhos         { get; set; }
			[Column("totalDiasReservacion")] public int?     TotalDiasReservacion { get; set; }
			[Column("costoTotal")          ] public decimal? CostoTotal           { get; set; }
			[Column("estado")              ] public char     Estado               { get; set; }
			[Column("nombreCompleto")      ] public string   NombreCompleto       { get; set; }
		}

		#endregion

		#region SpConsultarHabitaciones

		public static IEnumerable<SpConsultarHabitacionesResult> SpConsultarHabitaciones(this PvProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<SpConsultarHabitacionesResult>("[dbo].[spConsultarHabitaciones]");
		}

		public partial class SpConsultarHabitacionesResult
		{
			[Column("idHabitacion")    ] public int    IdHabitacion     { get; set; }
			[Column("idHotel")         ] public int    IdHotel          { get; set; }
			[Column("numeroHabitacion")] public string NumeroHabitacion { get; set; }
			[Column("capacidadMaxima") ] public int    CapacidadMaxima  { get; set; }
			[Column("descripcion")     ] public string Descripcion      { get; set; }
			[Column("estado")          ] public char   Estado           { get; set; }
			[Column("nombre")          ] public string Nombre           { get; set; }
		}

		#endregion

		#region SpConsultarHabitacionesDeHotel

		public static IEnumerable<SpConsultarHabitacionesDeHotelResult> SpConsultarHabitacionesDeHotel(this PvProyectoFinalDB dataConnection, int? @totalPersonas, int? @idHotel)
		{
			var parameters = new []
			{
				new DataParameter("@totalPersonas", @totalPersonas, LinqToDB.DataType.Int32),
				new DataParameter("@idHotel",       @idHotel,       LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarHabitacionesDeHotelResult>("[dbo].[spConsultarHabitacionesDeHotel]", parameters);
		}

		public partial class SpConsultarHabitacionesDeHotelResult
		{
			[Column("idHabitacion")     ] public int? IdHabitacion      { get; set; }
			[Column("personasRestantes")] public int? PersonasRestantes { get; set; }
			[Column("capacidad")        ] public int? Capacidad         { get; set; }
		}

		#endregion

		#region SpConsultarHabitacionPorID

		public static IEnumerable<SpConsultarHabitacionPorIDResult> SpConsultarHabitacionPorID(this PvProyectoFinalDB dataConnection, int? @idHabitacion)
		{
			var parameters = new []
			{
				new DataParameter("@idHabitacion", @idHabitacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarHabitacionPorIDResult>("[dbo].[spConsultarHabitacionPorID]", parameters);
		}

		public partial class SpConsultarHabitacionPorIDResult
		{
			[Column("idHabitacion")    ] public int    IdHabitacion     { get; set; }
			[Column("idHotel")         ] public int    IdHotel          { get; set; }
			[Column("numeroHabitacion")] public string NumeroHabitacion { get; set; }
			[Column("capacidadMaxima") ] public int    CapacidadMaxima  { get; set; }
			[Column("descripcion")     ] public string Descripcion      { get; set; }
			[Column("estado")          ] public char   Estado           { get; set; }
			[Column("nombre")          ] public string Nombre           { get; set; }
		}

		#endregion

		#region SpConsultarHoteles

		public static IEnumerable<SpConsultarHotelesResult> SpConsultarHoteles(this PvProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<SpConsultarHotelesResult>("[dbo].[spConsultarHoteles]");
		}

		public partial class SpConsultarHotelesResult
		{
			[Column("idHotel")] public int    IdHotel { get; set; }
			[Column("nombre") ] public string Nombre  { get; set; }
		}

		#endregion

		#region SpConsultarReservaciones

		public static IEnumerable<SpConsultarReservacionesResult> SpConsultarReservaciones(this PvProyectoFinalDB dataConnection, int? @idEmpleado)
		{
			var parameters = new []
			{
				new DataParameter("@idEmpleado", @idEmpleado, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarReservacionesResult>("[dbo].[spConsultarReservaciones]", parameters);
		}

		public partial class SpConsultarReservacionesResult
		{
			[Column("idReservacion")     ] public int      IdReservacion      { get; set; }
			[Column("nombreCompleto")    ] public string   NombreCompleto     { get; set; }
			[Column("nombre")            ] public string   Nombre             { get; set; }
			[Column("costoPorCadaAdulto")] public decimal? CostoPorCadaAdulto { get; set; }
			[Column("costoPorCadaNinho") ] public decimal? CostoPorCadaNinho  { get; set; }
			[Column("fechaEntrada")      ] public DateTime FechaEntrada       { get; set; }
			[Column("fechaSalida")       ] public DateTime FechaSalida        { get; set; }
			[Column("costoTotal")        ] public decimal  CostoTotal         { get; set; }
			[Column("estado")            ] public char     Estado             { get; set; }
		}

		#endregion

		#region SpConsultarReservacionPorID

		public static IEnumerable<SpConsultarReservacionPorIDResult> SpConsultarReservacionPorID(this PvProyectoFinalDB dataConnection, int? @idReservacion)
		{
			var parameters = new []
			{
				new DataParameter("@idReservacion", @idReservacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpConsultarReservacionPorIDResult>("[dbo].[spConsultarReservacionPorID]", parameters);
		}

		public partial class SpConsultarReservacionPorIDResult
		{
			[Column("idReservacion")   ] public int      IdReservacion    { get; set; }
			[Column("nombre")          ] public string   Nombre           { get; set; }
			[Column("numeroHabitacion")] public string   NumeroHabitacion { get; set; }
			[Column("nombreCompleto")  ] public string   NombreCompleto   { get; set; }
			[Column("fechaEntrada")    ] public DateTime FechaEntrada     { get; set; }
			[Column("fechaSalida")     ] public DateTime FechaSalida      { get; set; }
			[Column("numeroAdultos")   ] public int      NumeroAdultos    { get; set; }
			[Column("numeroNinhos")    ] public int      NumeroNinhos     { get; set; }
			[Column("estado")          ] public char     Estado           { get; set; }
		}

		#endregion

		#region SpConsultarTodasLasPersonas

		public static IEnumerable<Persona> SpConsultarTodasLasPersonas(this PvProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<Persona>("[dbo].[spConsultarTodasLasPersonas]");
		}

		#endregion

		#region SpConsuntarPersonas

		public static IEnumerable<SpConsuntarPersonasResult> SpConsuntarPersonas(this PvProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<SpConsuntarPersonasResult>("[dbo].[spConsuntarPersonas]");
		}

		public partial class SpConsuntarPersonasResult
		{
			[Column("idPersona")     ] public int    IdPersona      { get; set; }
			[Column("nombreCompleto")] public string NombreCompleto { get; set; }
			[Column("estado")        ] public char   Estado         { get; set; }
		}

		#endregion

		#region SpCrearHabitacion

		public static int SpCrearHabitacion(this PvProyectoFinalDB dataConnection, int? @idHotel, string @numeroHabitacion, int? @capacidadMaxima, string @descripcion, char? @estado)
		{
			var parameters = new []
			{
				new DataParameter("@idHotel",          @idHotel,          LinqToDB.DataType.Int32),
				new DataParameter("@numeroHabitacion", @numeroHabitacion, LinqToDB.DataType.VarChar)
				{
					Size = 10
				},
				new DataParameter("@capacidadMaxima",  @capacidadMaxima,  LinqToDB.DataType.Int32),
				new DataParameter("@descripcion",      @descripcion,      LinqToDB.DataType.VarChar)
				{
					Size = 500
				},
				new DataParameter("@estado",           @estado,           LinqToDB.DataType.VarChar)
				{
					Size = 1
				}
			};

			return dataConnection.ExecuteProc("[dbo].[spCrearHabitacion]", parameters);
		}

		#endregion

		#region SpCrearReservacion

		public static int SpCrearReservacion(this PvProyectoFinalDB dataConnection, int? @idPersona, int? @idHotel, int? @idHabitacion, DateTime? @fechaEntrada, DateTime? @fechaSalida, int? @numeroAdultos, int? @numeroNinhos)
		{
			var parameters = new []
			{
				new DataParameter("@idPersona",     @idPersona,     LinqToDB.DataType.Int32),
				new DataParameter("@idHotel",       @idHotel,       LinqToDB.DataType.Int32),
				new DataParameter("@idHabitacion",  @idHabitacion,  LinqToDB.DataType.Int32),
				new DataParameter("@fechaEntrada",  @fechaEntrada,  LinqToDB.DataType.DateTime),
				new DataParameter("@fechaSalida",   @fechaSalida,   LinqToDB.DataType.DateTime),
				new DataParameter("@numeroAdultos", @numeroAdultos, LinqToDB.DataType.Int32),
				new DataParameter("@numeroNinhos",  @numeroNinhos,  LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spCrearReservacion]", parameters);
		}

		#endregion

		#region SpEditarHabitacion

		public static int SpEditarHabitacion(this PvProyectoFinalDB dataConnection, int? @idHabitacion, int? @idHotel, string @numeroHabitacion, int? @idPersona, int? @capacidadMaxima, string @descripcion)
		{
			var parameters = new []
			{
				new DataParameter("@idHabitacion",     @idHabitacion,     LinqToDB.DataType.Int32),
				new DataParameter("@idHotel",          @idHotel,          LinqToDB.DataType.Int32),
				new DataParameter("@numeroHabitacion", @numeroHabitacion, LinqToDB.DataType.VarChar)
				{
					Size = 10
				},
				new DataParameter("@idPersona",        @idPersona,        LinqToDB.DataType.Int32),
				new DataParameter("@capacidadMaxima",  @capacidadMaxima,  LinqToDB.DataType.Int32),
				new DataParameter("@descripcion",      @descripcion,      LinqToDB.DataType.VarChar)
				{
					Size = 500
				}
			};

			return dataConnection.ExecuteProc("[dbo].[spEditarHabitacion]", parameters);
		}

		#endregion

		#region SpFiltrar

		public static IEnumerable<SpFiltrarResult> SpFiltrar(this PvProyectoFinalDB dataConnection, DateTime? @fechaInicial, DateTime? @fechaFinal)
		{
			var parameters = new []
			{
				new DataParameter("@fechaInicial", @fechaInicial, LinqToDB.DataType.DateTime),
				new DataParameter("@fechaFinal",   @fechaFinal,   LinqToDB.DataType.DateTime)
			};

			return dataConnection.QueryProc<SpFiltrarResult>("[dbo].[spFiltrar]", parameters);
		}

		public partial class SpFiltrarResult
		{
			[Column("idReservacion") ] public int      IdReservacion  { get; set; }
			[Column("nombreCompleto")] public string   NombreCompleto { get; set; }
			[Column("nombre")        ] public string   Nombre         { get; set; }
			[Column("fechaEntrada")  ] public DateTime FechaEntrada   { get; set; }
			[Column("fechaSalida")   ] public DateTime FechaSalida    { get; set; }
			[Column("costoTotal")    ] public decimal  CostoTotal     { get; set; }
			[Column("estado")        ] public char     Estado         { get; set; }
		}

		#endregion

		#region SpFiltroPorID

		public static IEnumerable<SpFiltroPorIDResult> SpFiltroPorID(this PvProyectoFinalDB dataConnection, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idPersona", @idPersona, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpFiltroPorIDResult>("[dbo].[spFiltroPorID]", parameters);
		}

		public partial class SpFiltroPorIDResult
		{
			[Column("idReservacion") ] public int      IdReservacion  { get; set; }
			[Column("nombreCompleto")] public string   NombreCompleto { get; set; }
			[Column("nombre")        ] public string   Nombre         { get; set; }
			[Column("fechaEntrada")  ] public DateTime FechaEntrada   { get; set; }
			[Column("fechaSalida")   ] public DateTime FechaSalida    { get; set; }
			[Column("costoTotal")    ] public decimal  CostoTotal     { get; set; }
			[Column("estado")        ] public char     Estado         { get; set; }
		}

		#endregion

		#region SpInactivarHabitacion

		public static int SpInactivarHabitacion(this PvProyectoFinalDB dataConnection, int? @idHabitacion)
		{
			var parameters = new []
			{
				new DataParameter("@idHabitacion", @idHabitacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spInactivarHabitacion]", parameters);
		}

		#endregion

		#region SpInsertarReservacionEnBitacora

		public static int SpInsertarReservacionEnBitacora(this PvProyectoFinalDB dataConnection, int? @idPersona, int? @idReservacion)
		{
			var parameters = new []
			{
				new DataParameter("@idPersona",     @idPersona,     LinqToDB.DataType.Int32),
				new DataParameter("@idReservacion", @idReservacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spInsertarReservacionEnBitacora]", parameters);
		}

		#endregion

		#region SpLogin

		public static IEnumerable<Persona> SpLogin(this PvProyectoFinalDB dataConnection, string @Email, string @Clave)
		{
			var parameters = new []
			{
				new DataParameter("@Email", @Email, LinqToDB.DataType.VarChar)
				{
					Size = 150
				},
				new DataParameter("@Clave", @Clave, LinqToDB.DataType.VarChar)
				{
					Size = 15
				}
			};

			return dataConnection.QueryProc<Persona>("[dbo].[spLogin]", parameters);
		}

		#endregion

		#region SpMisReservaciones

		public static IEnumerable<SpMisReservacionesResult> SpMisReservaciones(this PvProyectoFinalDB dataConnection, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idPersona", @idPersona, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpMisReservacionesResult>("[dbo].[spMisReservaciones]", parameters);
		}

		public partial class SpMisReservacionesResult
		{
			[Column("idReservacion")     ] public int      IdReservacion      { get; set; }
			[Column("idHabitacion")      ] public int?     IdHabitacion       { get; set; }
			[Column("idHotel")           ] public int?     IdHotel            { get; set; }
			[Column("nombre")            ] public string   Nombre             { get; set; }
			[Column("costoPorCadaAdulto")] public decimal? CostoPorCadaAdulto { get; set; }
			[Column("costoPorCadaNinho") ] public decimal? CostoPorCadaNinho  { get; set; }
			[Column("fechaEntrada")      ] public DateTime FechaEntrada       { get; set; }
			[Column("fechaSalida")       ] public DateTime FechaSalida        { get; set; }
			[Column("costoTotal")        ] public decimal  CostoTotal         { get; set; }
			[Column("estado")            ] public char     Estado             { get; set; }
			[Column("nombreCompleto")    ] public string   NombreCompleto     { get; set; }
		}

		#endregion

		#region SpModificarReservacion

		public static int SpModificarReservacion(this PvProyectoFinalDB dataConnection, int? @idReservacion, DateTime? @fechaEntrada, DateTime? @fechaSalida, int? @numeroAdultos, int? @numeroNinhos, DateTime? @fechaModificacion, int? @totalDiasReservacion, decimal? @costoTotal, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idReservacion",        @idReservacion,        LinqToDB.DataType.Int32),
				new DataParameter("@fechaEntrada",         @fechaEntrada,         LinqToDB.DataType.DateTime),
				new DataParameter("@fechaSalida",          @fechaSalida,          LinqToDB.DataType.DateTime),
				new DataParameter("@numeroAdultos",        @numeroAdultos,        LinqToDB.DataType.Int32),
				new DataParameter("@numeroNinhos",         @numeroNinhos,         LinqToDB.DataType.Int32),
				new DataParameter("@fechaModificacion",    @fechaModificacion,    LinqToDB.DataType.DateTime),
				new DataParameter("@totalDiasReservacion", @totalDiasReservacion, LinqToDB.DataType.Int32),
				new DataParameter("@costoTotal",           @costoTotal,           LinqToDB.DataType.Decimal),
				new DataParameter("@idPersona",            @idPersona,            LinqToDB.DataType.Int32)
			};

			return dataConnection.ExecuteProc("[dbo].[spModificarReservacion]", parameters);
		}

		#endregion

		#region SpModificarReservacionYRegistrarBitacora

		public static IEnumerable<SpModificarReservacionYRegistrarBitacoraResult> SpModificarReservacionYRegistrarBitacora(this PvProyectoFinalDB dataConnection, int? @idReservacion, DateTime? @fechaEntrada, DateTime? @fechaSalida, int? @numeroAdultos, int? @numeroNinhos, int? @idPersona)
		{
			var parameters = new []
			{
				new DataParameter("@idReservacion", @idReservacion, LinqToDB.DataType.Int32),
				new DataParameter("@fechaEntrada",  @fechaEntrada,  LinqToDB.DataType.DateTime),
				new DataParameter("@fechaSalida",   @fechaSalida,   LinqToDB.DataType.DateTime),
				new DataParameter("@numeroAdultos", @numeroAdultos, LinqToDB.DataType.Int32),
				new DataParameter("@numeroNinhos",  @numeroNinhos,  LinqToDB.DataType.Int32),
				new DataParameter("@idPersona",     @idPersona,     LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpModificarReservacionYRegistrarBitacoraResult>("[dbo].[spModificarReservacionYRegistrarBitacora]", parameters);
		}

		public partial class SpModificarReservacionYRegistrarBitacoraResult
		{
			public string Resultado { get; set; }
		}

		#endregion

		#region SpMostrarReservaciones

		public static IEnumerable<Reservacion> SpMostrarReservaciones(this PvProyectoFinalDB dataConnection)
		{
			return dataConnection.QueryProc<Reservacion>("[dbo].[spMostrarReservaciones]");
		}

		#endregion

		#region SpVerificarHabitacionDuplicada

		public static IEnumerable<SpVerificarHabitacionDuplicadaResult> SpVerificarHabitacionDuplicada(this PvProyectoFinalDB dataConnection, int? @idHotel, string @numeroHabitacion)
		{
			var parameters = new []
			{
				new DataParameter("@idHotel",          @idHotel,          LinqToDB.DataType.Int32),
				new DataParameter("@numeroHabitacion", @numeroHabitacion, LinqToDB.DataType.VarChar)
				{
					Size = 10
				}
			};

			return dataConnection.QueryProc<SpVerificarHabitacionDuplicadaResult>("[dbo].[SpVerificarHabitacionDuplicada]", parameters);
		}

		public partial class SpVerificarHabitacionDuplicadaResult
		{
			public int? HabitacionesDuplicadas { get; set; }
		}

		#endregion

		#region SpVerificarReservacionesActivas

		public static IEnumerable<SpVerificarReservacionesActivasResult> SpVerificarReservacionesActivas(this PvProyectoFinalDB dataConnection, int? @idHabitacion)
		{
			var parameters = new []
			{
				new DataParameter("@idHabitacion", @idHabitacion, LinqToDB.DataType.Int32)
			};

			return dataConnection.QueryProc<SpVerificarReservacionesActivasResult>("[dbo].[spVerificarReservacionesActivas]", parameters);
		}

		public partial class SpVerificarReservacionesActivasResult
		{
			public int? ReservacionesActivas { get; set; }
		}

		#endregion
	}

	public static partial class TableExtensions
	{
		public static Bitacora Find(this ITable<Bitacora> table, int IdBitacora)
		{
			return table.FirstOrDefault(t =>
				t.IdBitacora == IdBitacora);
		}

		public static Habitacion Find(this ITable<Habitacion> table, int IdHabitacion)
		{
			return table.FirstOrDefault(t =>
				t.IdHabitacion == IdHabitacion);
		}

		public static Hotel Find(this ITable<Hotel> table, int IdHotel)
		{
			return table.FirstOrDefault(t =>
				t.IdHotel == IdHotel);
		}

		public static Persona Find(this ITable<Persona> table, int IdPersona)
		{
			return table.FirstOrDefault(t =>
				t.IdPersona == IdPersona);
		}

		public static Reservacion Find(this ITable<Reservacion> table, int IdReservacion)
		{
			return table.FirstOrDefault(t =>
				t.IdReservacion == IdReservacion);
		}
	}
}
 