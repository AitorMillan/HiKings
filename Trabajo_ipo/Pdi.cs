using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Trabajo_ipo
{
    internal class Pdi
    {
        public string Nombre { get; set; }
        public string Descripcion { get; set; }
        public string Tipologia { get; set; }
        public List<Uri> RutasFotos { get; set; } 
        public BitmapImage Foto { get; set; }
        public List<Rutas> Rutas_realcionadas { get; set; }

        public Pdi(string nombre, string descripcion, string tipologia, List<Uri> rutasFotos)
        {
            Nombre = nombre;
            Descripcion = descripcion;
            Tipologia = tipologia;
            RutasFotos = rutasFotos;
            Foto = new BitmapImage(rutasFotos[0]);
        }
    }
}
