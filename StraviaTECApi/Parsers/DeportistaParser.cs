using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaTECApi.Parsers
{
    public class DeportistaParser
    {
        public string Usuario { get; set; }
        public string Claveacceso { get; set; }
        public DateTime? Fechanacimiento { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Nombrecategoria { get; set; }
        public string Nacionalidad { get; set; }
        public string Foto { get; set; }
    }
}
