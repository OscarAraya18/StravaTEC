using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            GrupoCarrera = new HashSet<GrupoCarrera>();
            GrupoDeportistaAdmindeportistaNavigation = new HashSet<GrupoDeportista>();
            GrupoReto = new HashSet<GrupoReto>();
        }

        public string Nombre { get; set; }
        public string Admindeportista { get; set; }

        public virtual Deportista AdmindeportistaNavigation { get; set; }
        public virtual GrupoDeportista GrupoDeportistaNombregrupoNavigation { get; set; }
        public virtual ICollection<GrupoCarrera> GrupoCarrera { get; set; }
        public virtual ICollection<GrupoDeportista> GrupoDeportistaAdmindeportistaNavigation { get; set; }
        public virtual ICollection<GrupoReto> GrupoReto { get; set; }
    }
}
