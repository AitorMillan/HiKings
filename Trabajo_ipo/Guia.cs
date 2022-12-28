using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Trabajo_ipo
{
    internal class Guia
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public long Telefono { get; set; }
        public string Email { get; set; }
        public Uri RutaFoto { get; set; }

        public BitmapImage Foto { get; set; }

        public List<string> Idiomas { get; set; }
        public double Valoracion { get; set; }

        //Añadir rutas

        public Guia(string nombre, string apellidos, long telefono, string email, Uri rutafoto, List<string> idiomas, double valoracion)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Telefono = telefono;
            Email = email;
            RutaFoto = rutafoto;
            Foto = new BitmapImage(RutaFoto);
            Idiomas = idiomas;
            Valoracion = valoracion;
        }

    }
}
