using StraviaTECApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StraviaTECApi.Parsers
{
    public class CarreraParser
    {
        public CarreraParser()
        {
            carreraPatrocinador = new List<PatrocinadorParser>();
            carreraCuentabancaria = new List<string>();
            carreraCategorias = new List<string>();
            actividades = new List<PosActividad>();
        }

        public string nombreCarrera { get; set; }
        public string adminDeportista { get; set; }
        public DateTime fecha { get; set; }
        public string recorridoGPX { get; set; }
        public int costo { get; set; }
        public string tipoActividad { get; set; }
        public bool privacidad { get; set; }
        public int diasFaltantes { get; set; }
        public bool finalizada { get; set; }
        public List<PatrocinadorParser> carreraPatrocinador { get; set; }
        public List<string> carreraCuentabancaria { get; set; }
        public List<string> carreraCategorias { get; set; }
        public List<PosActividad> actividades { get; set; }
    }
}
