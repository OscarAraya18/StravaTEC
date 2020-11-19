using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class AmigoDeportista
    {
        public string Usuariodeportista { get; set; }
        public string Amigoid { get; set; }

        // uno a muchos
        public virtual Deportista Amigo { get; set; }
        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
