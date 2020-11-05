using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Inscripcion
    {
        public Inscripcion()
        {
            InscripcionCarrera = new HashSet<InscripcionCarrera>();
        }

        public int Id { get; set; }
        public string Usuariodeportista { get; set; }
        public string Estado { get; set; }
        public byte[] Recibopago { get; set; }

        public virtual Deportista UsuariodeportistaNavigation { get; set; }
        public virtual ICollection<InscripcionCarrera> InscripcionCarrera { get; set; }
    }
}
