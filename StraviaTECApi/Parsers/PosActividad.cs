using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaTECApi.Models
{
    public class PosActividad
    {

        public string usuarioDeportista { get; set; }
        public string tipoActividad { get; set; }
        public string nombreActividad { get; set; }
        public TimeSpan duracion { get; set; }
    }
}
