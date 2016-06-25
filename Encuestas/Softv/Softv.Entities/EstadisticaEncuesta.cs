using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Softv.Entities
{
    public class EstadisticaEncuesta
    {

        public string NombreEncuesta { get; set; }
        public string pregunta { get; set; }
        public string respuesta { get; set; }
        public int  cantidad { get; set; }

        public int IdPregunta { get; set; }
    }

}
