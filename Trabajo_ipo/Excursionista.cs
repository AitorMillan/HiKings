using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml;

namespace Trabajo_ipo
{

    public class Excursionista
    {
        public string Nombre { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public long Telefono { get; set; }

        //Atributo rutas de la clase rutas
        public Uri RutaFoto { get; set; }

        public BitmapImage Foto { get; set; }

        public List<Rutas> Rutas;

    public Excursionista(string nombre, string apellidos, int edad, long telefono, Uri rutaFoto)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            Telefono = telefono;
            RutaFoto = rutaFoto;
            Foto = new BitmapImage(RutaFoto);
            Rutas = new List<Rutas>();
        }
     public Excursionista(string nombre, string apellidos,int edad, long telefono)
        {
            Nombre = nombre;
            Apellidos = apellidos;
            Edad = edad;
            Telefono = telefono;
            Foto = new BitmapImage(new Uri("/Imagenes/persona_estandar.png", UriKind.Relative));
            Rutas = new List<Rutas>();
        }

    public override bool Equals(System.Object obj)
        {
            Excursionista ex = obj as Excursionista;
            return Nombre == ex.Nombre && Apellidos == ex.Apellidos && Telefono == ex.Telefono;
        }
    }

}
