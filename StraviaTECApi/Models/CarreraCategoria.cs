using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class CarreraCategoria
    {
        public string Nombrecategoria { get; set; }
        public string Nombrecarrera { get; set; }
        public string Admindeportista { get; set; }

        public virtual Carrera AdmindeportistaNavigation { get; set; }
        public virtual Carrera NombrecarreraNavigation { get; set; }
        public virtual Categoria NombrecategoriaNavigation { get; set; }
    }
}
