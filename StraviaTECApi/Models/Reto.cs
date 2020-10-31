using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Reto
    {
        public Reto()
        {
            DeportistaRetoAdmindeportistaNavigation = new HashSet<DeportistaReto>();
            DeportistaRetoNombreretoNavigation = new HashSet<DeportistaReto>();
            GrupoRetoAdmindeportistaNavigation = new HashSet<GrupoReto>();
            GrupoRetoNombreretoNavigation = new HashSet<GrupoReto>();
            RetoPatrocinadorAdmindeportistaNavigation = new HashSet<RetoPatrocinador>();
            RetoPatrocinadorNombreretoNavigation = new HashSet<RetoPatrocinador>();
        }

        public string Nombre { get; set; }
        public string Admindeportista { get; set; }
        public string Fondoaltitud { get; set; }
        public string Tipoactividad { get; set; }
        public DateTime Periododisponibilidad { get; set; }
        public bool Privacidad { get; set; }
        public double? Kmacumulados { get; set; }
        public double Kmtotales { get; set; }
        public string Descripcion { get; set; }

        public virtual Deportista AdmindeportistaNavigation { get; set; }
        public virtual ICollection<DeportistaReto> DeportistaRetoAdmindeportistaNavigation { get; set; }
        public virtual ICollection<DeportistaReto> DeportistaRetoNombreretoNavigation { get; set; }
        public virtual ICollection<GrupoReto> GrupoRetoAdmindeportistaNavigation { get; set; }
        public virtual ICollection<GrupoReto> GrupoRetoNombreretoNavigation { get; set; }
        public virtual ICollection<RetoPatrocinador> RetoPatrocinadorAdmindeportistaNavigation { get; set; }
        public virtual ICollection<RetoPatrocinador> RetoPatrocinadorNombreretoNavigation { get; set; }
    }
}
