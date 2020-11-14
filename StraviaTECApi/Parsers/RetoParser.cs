using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaTECApi.Parsers
{
    public class RetoParser
    {
        public RetoParser()
        {
            RetoPatrocinador = new List<PatrocinadorParser>();
        }
        public string nombreReto { get; set; }
        public string adminReto { get; set; }
        public string fondoAltitud { get; set; }
        public string tipoActividad { get; set; }
        public int diasFaltantes { get; set; }
        public DateTime fechaLimite { get; set; }
        public bool privacidad { get; set; }
        public double kmTotales { get; set; }
        public string descripcion { get; set; }
        public double kmAcumulados{ get; set; }
        public bool completado { get; set; }

        public  List<PatrocinadorParser> RetoPatrocinador { get; set; }
    }
}
