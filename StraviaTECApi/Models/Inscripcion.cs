using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Inscripcion
    {
        public string Usuariodeportista { get; set; }
        public string Estado { get; set; }
        public byte[] Recibopago { get; set; }
        public string Nombrecarrera { get; set; }
        public string Admincarrera { get; set; }

        public virtual Carrera Carrera { get; set; }
        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
