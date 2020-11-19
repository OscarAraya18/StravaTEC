using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class CarreraPatrocinador
    {
        public string Nombrepatrocinador { get; set; }
        public string Nombrecarrera { get; set; }
        public string Admindeportista { get; set; }

        // uno a muchos
        public virtual Carrera Carrera { get; set; }
        public virtual Patrocinador NombrepatrocinadorNavigation { get; set; }
    }
}
