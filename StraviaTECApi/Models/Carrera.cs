using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Carrera
    {
        public Carrera()
        {
            CarreraCategoria = new HashSet<CarreraCategoria>();
            CarreraCuentabancaria = new HashSet<CarreraCuentabancaria>();
            CarreraPatrocinador = new HashSet<CarreraPatrocinador>();
            DeportistaCarrera = new HashSet<DeportistaCarrera>();
            GrupoCarrera = new HashSet<GrupoCarrera>();
            InscripcionCarrera = new HashSet<InscripcionCarrera>();
        }

        public string Nombre { get; set; }
        public string Admindeportista { get; set; }
        public DateTime? Fecha { get; set; }
        public byte[] Recorrido { get; set; }
        public int? Costo { get; set; }
        public string Tipoactividad { get; set; }
        public bool? Privacidad { get; set; }

        public virtual Deportista AdmindeportistaNavigation { get; set; }
        public virtual ICollection<CarreraCategoria> CarreraCategoria { get; set; }
        public virtual ICollection<CarreraCuentabancaria> CarreraCuentabancaria { get; set; }
        public virtual ICollection<CarreraPatrocinador> CarreraPatrocinador { get; set; }
        public virtual ICollection<DeportistaCarrera> DeportistaCarrera { get; set; }
        public virtual ICollection<GrupoCarrera> GrupoCarrera { get; set; }
        public virtual ICollection<InscripcionCarrera> InscripcionCarrera { get; set; }
    }
}
