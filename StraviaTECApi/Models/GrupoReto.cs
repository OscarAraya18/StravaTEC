using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class GrupoReto
    {
        public string Nombrereto { get; set; }
        public string Adminreto { get; set; }
        public string Admingrupo { get; set; }
        public string Nombregrupo { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual Reto Reto { get; set; }
    }
}
