using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class RetoPatrocinador
    {
        public string Nombrepatrocinador { get; set; }
        public string Nombrereto { get; set; }
        public string Admindeportista { get; set; }

        public virtual Patrocinador NombrepatrocinadorNavigation { get; set; }
        public virtual Reto Reto { get; set; }
    }
}
