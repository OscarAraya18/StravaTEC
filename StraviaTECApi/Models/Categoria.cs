using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Categoria
    {
        public Categoria()
        {
            CarreraCategoria = new HashSet<CarreraCategoria>();
            Deportista = new HashSet<Deportista>();
        }

        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        // muchos a muchos
        public virtual ICollection<CarreraCategoria> CarreraCategoria { get; set; }
        public virtual ICollection<Deportista> Deportista { get; set; }
    }
}
