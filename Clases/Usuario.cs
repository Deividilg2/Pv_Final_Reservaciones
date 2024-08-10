using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pv_Final_Reservaciones.Clases
{
    /*Creamos una clase de Usuario para poder utilizar sus atributos y almacenar en ellos datos vitales
     de una persona que son requeridos en cada página para validar una sesión*/
    public class Usuario
    {
        public int id { get; set; }
        public string nombreCompleto { get; set; }
        public bool esEmpleado { get; set; }
        public bool Estado { get; set; }
    }
}