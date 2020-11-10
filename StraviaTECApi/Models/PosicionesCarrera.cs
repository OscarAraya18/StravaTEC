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
        public List<PosActividad> actividades { get; set; }

    }
}
