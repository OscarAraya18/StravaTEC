using System;
using System.Collections.Generic;

namespace StraviaTECApi.Models
{
    public partial class Patrocinador
    {
        public Patrocinador()
        {
            CarreraPatrocinador = new HashSet<CarreraPatrocinador>();
            RetoPatrocinador = new HashSet<RetoPatrocinador>();
        }

        public string Nombrecomercial { get; set; }
        public string Logo { get; set; }
        public string Nombrerepresentante { get; set; }
        public string Numerotelrepresentante { get; set; }

        public virtual ICollection<CarreraPatrocinador> CarreraPatrocinador { get; set; }
        public virtual ICollection<RetoPatrocinador> RetoPatrocinador { get; set; }
    }
}
