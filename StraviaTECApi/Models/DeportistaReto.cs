using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class DeportistaReto
    {
        public string Usuariodeportista { get; set; }
        public string Nombrereto { get; set; }
        public string Admindeportista { get; set; }
        public bool Completado { get; set; }
        public double Kmacumulados { get; set; }

        public virtual Reto Reto { get; set; }
        public virtual Deportista UsuariodeportistaNavigation { get; set; }
    }
}
