using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Deportista
    {
        public Deportista()
        {
            AmigoDeportistaAmigo = new HashSet<AmigoDeportista>();
            AmigoDeportistaUsuariodeportistaNavigation = new HashSet<AmigoDeportista>();
            Carrera = new HashSet<Carrera>();
            DeportistaCarrera = new HashSet<DeportistaCarrera>();
            DeportistaReto = new HashSet<DeportistaReto>();
            Grupo = new HashSet<Grupo>();
            GrupoDeportista = new HashSet<GrupoDeportista>();
            Inscripcion = new HashSet<Inscripcion>();
            Reto = new HashSet<Reto>();
        }

        public string Usuario { get; set; }
        public string Claveacceso { get; set; }
        public DateTime Fechanacimiento { get; set; }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }
        public string Nombrecategoria { get; set; }
        public string Nacionalidad { get; set; }
        public byte[] Foto { get; set; }

        public virtual Categoria NombrecategoriaNavigation { get; set; }
        public virtual Actividad Actividad { get; set; }
        public virtual ICollection<AmigoDeportista> AmigoDeportistaAmigo { get; set; }
        public virtual ICollection<AmigoDeportista> AmigoDeportistaUsuariodeportistaNavigation { get; set; }
        public virtual ICollection<Carrera> Carrera { get; set; }
        public virtual ICollection<DeportistaCarrera> DeportistaCarrera { get; set; }
        public virtual ICollection<DeportistaReto> DeportistaReto { get; set; }
        public virtual ICollection<Grupo> Grupo { get; set; }
        public virtual ICollection<GrupoDeportista> GrupoDeportista { get; set; }
        public virtual ICollection<Inscripcion> Inscripcion { get; set; }
        public virtual ICollection<Reto> Reto { get; set; }
    }
}
