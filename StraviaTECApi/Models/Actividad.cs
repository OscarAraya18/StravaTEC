using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Actividad
    {
        public string Usuariodeportista { get; set; }
        public DateTime Fechahora { get; set; }
        public TimeSpan? Duracion { get; set; }
        public double? Kilometraje { get; set; }
        public string Tipoactividad { get; set; }
        public string Recorridogpx { get; set; }
        public string Nombreretocarrera { get; set; }
        public string Adminretocarrera { get; set; }
        public int Banderilla { get; set; }

        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
