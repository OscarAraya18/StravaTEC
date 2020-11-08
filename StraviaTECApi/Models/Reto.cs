using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Reto
    {
        public Reto()
        {
            DeportistaReto = new HashSet<DeportistaReto>();
            GrupoReto = new HashSet<GrupoReto>();
            RetoPatrocinador = new HashSet<RetoPatrocinador>();
        }

        public string Nombre { get; set; }
        public string Admindeportista { get; set; }
        public string Fondoaltitud { get; set; }
        public string Tipoactividad { get; set; }
        public DateTime Periododisponibilidad { get; set; }
        public bool? Privacidad { get; set; }
        public double? Kmtotales { get; set; }
        public string Descripcion { get; set; }

        public virtual Deportista AdmindeportistaNavigation { get; set; }
        public virtual ICollection<DeportistaReto> DeportistaReto { get; set; }
        public virtual ICollection<GrupoReto> GrupoReto { get; set; }
        public virtual ICollection<RetoPatrocinador> RetoPatrocinador { get; set; }
    }
}
