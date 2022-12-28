using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trabajo_ipo
{
    internal class Rutas
    {
        public string Nombre { get; set; }
        public string Origen { get; set; }
        public string Destino { get; set; }
        public string Dificultad { get; set; }
        public int Duracion { get; set; }
        public string Fecha { get; set; }
        public int Num_excursionistas { get; set; }
        public bool Finalizada { get; set; }
        public string Incidencias { get; set; }

        //Añadir PDIs

        public Rutas(string nombre, string origen, string destino, string dificultad, int duracion, string fecha, int num_excursionistas, bool finalizada)
        {
            Nombre = nombre;
            Origen = origen;
            Destino = destino;
            Dificultad = dificultad;
            Duracion = duracion;
            Fecha = fecha;
            Num_excursionistas = num_excursionistas;
            Finalizada = finalizada;
        }

        public Rutas(string nombre, string origen, string destino, string dificultad, int duracion, string fecha, int num_excursionistas, bool finalizada, string incidencias)
        {
            Nombre = nombre;
            Origen = origen;
            Destino = destino;
            Dificultad = dificultad;
            Duracion = duracion;
            Fecha = fecha;
            Num_excursionistas = num_excursionistas;
            Finalizada = finalizada;
            Incidencias = incidencias;
        }
    }
}
