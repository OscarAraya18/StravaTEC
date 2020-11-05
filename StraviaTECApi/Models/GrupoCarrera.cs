using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class GrupoCarrera
    {
        public string Nombrecarrera { get; set; }
        public string Nombregrupo { get; set; }
        public string Admindeportista { get; set; }

        public virtual Carrera Carrera { get; set; }
        public virtual Grupo NombregrupoNavigation { get; set; }
    }
}
