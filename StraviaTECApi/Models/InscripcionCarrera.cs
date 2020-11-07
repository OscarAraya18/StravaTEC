using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class InscripcionCarrera
    {
        public string Estadoinscripcion { get; set; }
        public string Deportistainscripcion { get; set; }
        public string Nombrecarrera { get; set; }

        public virtual Inscripcion Inscripcion { get; set; }
        public virtual Carrera NombrecarreraNavigation { get; set; }
    }
}
