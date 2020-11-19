using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class GrupoCarrera
    {
        public string Nombrecarrera { get; set; }
        public string Admincarrera { get; set; }
        public string Admingrupo { get; set; }
        public int Idgrupo { get; set; }

        // uno a muchos
        public virtual Carrera Carrera { get; set; }
        public virtual Grupo Grupo { get; set; }
    }
}
