using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            CarreraCategoriaAdmindeportistaNavigation = new HashSet<CarreraCategoria>();
            CarreraCategoriaNombrecarreraNavigation = new HashSet<CarreraCategoria>();
            CarreraCuentabancariaAdmindeportistaNavigation = new HashSet<CarreraCuentabancaria>();
            CarreraCuentabancariaNombrecarreraNavigation = new HashSet<CarreraCuentabancaria>();
            CarreraPatrocinadorAdmindeportistaNavigation = new HashSet<CarreraPatrocinador>();
            CarreraPatrocinadorNombrecarreraNavigation = new HashSet<CarreraPatrocinador>();
            DeportistaCarreraAdmindeportistaNavigation = new HashSet<DeportistaCarrera>();
            DeportistaCarreraNombrecarreraNavigation = new HashSet<DeportistaCarrera>();
            GrupoCarreraAdmindeportistaNavigation = new HashSet<GrupoCarrera>();
            GrupoCarreraNombrecarreraNavigation = new HashSet<GrupoCarrera>();
            InscripcionCarrera = new HashSet<InscripcionCarrera>();
        }

        public string Nombre { get; set; }
        public string Admindeportista { get; set; }
        public DateTime Fecha { get; set; }
        public byte[] Recorrido { get; set; }
        public int Costo { get; set; }
        public string Tipoactividad { get; set; }
        public bool Privacidad { get; set; }

        public virtual Deportista AdmindeportistaNavigation { get; set; }
        public virtual ICollection<CarreraCategoria> CarreraCategoriaAdmindeportistaNavigation { get; set; }
        public virtual ICollection<CarreraCategoria> CarreraCategoriaNombrecarreraNavigation { get; set; }
        public virtual ICollection<CarreraCuentabancaria> CarreraCuentabancariaAdmindeportistaNavigation { get; set; }
        public virtual ICollection<CarreraCuentabancaria> CarreraCuentabancariaNombrecarreraNavigation { get; set; }
        public virtual ICollection<CarreraPatrocinador> CarreraPatrocinadorAdmindeportistaNavigation { get; set; }
        public virtual ICollection<CarreraPatrocinador> CarreraPatrocinadorNombrecarreraNavigation { get; set; }
        public virtual ICollection<DeportistaCarrera> DeportistaCarreraAdmindeportistaNavigation { get; set; }
        public virtual ICollection<DeportistaCarrera> DeportistaCarreraNombrecarreraNavigation { get; set; }
        public virtual ICollection<GrupoCarrera> GrupoCarreraAdmindeportistaNavigation { get; set; }
        public virtual ICollection<GrupoCarrera> GrupoCarreraNombrecarreraNavigation { get; set; }
        public virtual ICollection<InscripcionCarrera> InscripcionCarrera { get; set; }
    }
}
