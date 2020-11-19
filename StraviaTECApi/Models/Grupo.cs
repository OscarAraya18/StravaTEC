using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Grupo
    {
        public Grupo()
        {
            GrupoCarrera = new HashSet<GrupoCarrera>();
            GrupoDeportista = new HashSet<GrupoDeportista>();
            GrupoReto = new HashSet<GrupoReto>();
        }

        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Admindeportista { get; set; }

        // uno a muchos
        public virtual Deportista AdmindeportistaNavigation { get; set; }

        // muchos a muchos
        public virtual ICollection<GrupoCarrera> GrupoCarrera { get; set; }
        public virtual ICollection<GrupoDeportista> GrupoDeportista { get; set; }
        public virtual ICollection<GrupoReto> GrupoReto { get; set; }
    }
}
