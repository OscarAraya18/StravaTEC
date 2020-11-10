using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class InscripcionCarrera
    {
        public string Estadoinscripcion { get; set; }
        public string Deportistainscripcion { get; set; }
        public string Nombrecarrera { get; set; }
        public string Admincarrera { get; set; }

        public virtual Carrera Carrera { get; set; }
        public virtual Inscripcion Inscripcion { get; set; }
    }
}
