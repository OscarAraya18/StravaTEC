using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Actividad
    {
        public string Usuariodeportista { get; set; }
        public DateTime Fechahora { get; set; }
        public string Duracion { get; set; }
        public double Kilometraje { get; set; }
        public string Tipoactividad { get; set; }
        public byte[] Recorridogpx { get; set; }

        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
