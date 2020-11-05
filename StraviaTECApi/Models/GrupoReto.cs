using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class GrupoReto
    {
        public string Nombrereto { get; set; }
        public string Nombregrupo { get; set; }
        public string Admindeportista { get; set; }

        public virtual Grupo NombregrupoNavigation { get; set; }
        public virtual Reto Reto { get; set; }
    }
}
