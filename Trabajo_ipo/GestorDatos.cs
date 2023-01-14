using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_ipo
{
    public class GestorDatos
    {
        public List<Excursionista> Excursionistas { get; set; }
        public List<Rutas> Rutas { get; set; }
        public List<Guia> Guias { get; set; }

        public GestorDatos()
        {
            Excursionistas = new List<Excursionista>();
            Rutas = new List<Rutas>();
            Guias = new List<Guia>();
        }
    }

}
