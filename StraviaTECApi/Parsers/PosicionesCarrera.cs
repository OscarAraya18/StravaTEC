using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public class PosicionesCarrera
    {

        public PosicionesCarrera()
        {
            actividades = new List<PosActividad>();
        }

        public string nombreCarrera { get; set; }
        public string adminCarrera { get; set; }
        public string recorridoGPX { get; set; }
        public DateTime fecha { get; set; }
        public bool completada { get; set; }
        public List<PosActividad> actividades { get; set; }
        public List<CarreraCuentabancaria> CarreraCuentabancaria { get; set; }
        public  List<CarreraPatrocinador> CarreraPatrocinador { get; set; }

    }
}
