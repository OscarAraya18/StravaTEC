using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class DeportistaCarrera
    {
        public string Usuariodeportista { get; set; }
        public string Nombrecarrera { get; set; }
        public string Admindeportista { get; set; }
        public bool? Completada { get; set; }

        // uno a muchos
        public virtual Carrera Carrera { get; set; }
        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
