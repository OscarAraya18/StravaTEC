using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class GrupoDeportista
    {
        public string Usuariodeportista { get; set; }
        public string Nombregrupo { get; set; }
        public string Admindeportista { get; set; }

        public virtual Grupo Grupo { get; set; }
        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
