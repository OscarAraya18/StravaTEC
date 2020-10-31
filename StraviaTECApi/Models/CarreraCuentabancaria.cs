using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class CarreraCuentabancaria
    {
        public string Nombrecarrera { get; set; }
        public string Admindeportista { get; set; }
        public string Cuentabancaria { get; set; }

        public virtual Carrera AdmindeportistaNavigation { get; set; }
        public virtual Carrera NombrecarreraNavigation { get; set; }
    }
}
