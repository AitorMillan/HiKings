using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;

namespace Trabajo_ipo
{
    /// <summary>
    /// Lógica de interacción para VentanaDatos.xaml
    /// </summary>
    public partial class VentanaDatos : Window
    {
        Window1 ex;

        VentanaGuias gui;

        VentanaRutas ru;
        GestorDatos gestor;


        public VentanaDatos()
        {
            InitializeComponent();
            // Preparamos todos los datos de prueba
            gestor = new GestorDatos();
            prepararDatosPrueba();
            
        }

        private void prepararDatosPrueba()
        {
            gestor.Excursionistas = leerExcursionistas();
            gestor.Guias = leerGuias();
            gestor.Rutas = leerRutas();
        }

        private List<Pdi> leerPdis()
        {
            List<Pdi> listado = new List<Pdi>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("pdis.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string nombre = node.Attributes["Nombre"].Value;
                string descripcion = node.Attributes["Descripcion"].Value;
                string tipologia = node.Attributes["Tipologia"].Value;
                List<String> ruta_fotos = node.Attributes["RutasFotos"].Value.Split(',').ToList();
                List<Uri> rutas_fotos = new List<Uri>();
                foreach (string ruta in ruta_fotos)
                {
                    rutas_fotos.Add(new Uri(ruta, UriKind.RelativeOrAbsolute));
                }
                Pdi pdi = new Pdi(nombre,descripcion, tipologia, rutas_fotos);
                listado.Add(pdi);

            }
            return listado;
        }
        private List<Rutas> leerRutas()
        {
            List<Pdi> pdis = leerPdis();
            int pdi_añadir = 2;
            List<Rutas> listado = new List<Rutas>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("rutas.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                Rutas ruta;
                string nombre = node.Attributes["Nombre"].Value;
                string origen = node.Attributes["Origen"].Value;
                string destino = node.Attributes["Destino"].Value;
                string dificultad = node.Attributes["Dificultad"].Value;
                int duracion = Convert.ToInt32(node.Attributes["Duracion"].Value);
                string fecha = node.Attributes["Fecha"].Value;
                int num_excursionistas = Convert.ToInt32(node.Attributes["Num_excursionistas"].Value);
                bool finalizada = Convert.ToBoolean(node.Attributes["Finalizada"].Value);
                if (finalizada)
                {
                    string incidencias = node.Attributes["Incidencias"].Value;
                    Guia guia = gestor.Guias[0];
                    guia.Rutas.Add(nombre);
                    ruta = new Rutas(nombre, origen, destino, dificultad, duracion, fecha, num_excursionistas, finalizada, incidencias, guia);
                    ruta.Pdis.Add(pdis[0]);
                    ruta.Pdis.Add(pdis[1]);
                    foreach (Excursionista ex in gestor.Excursionistas)
                    {
                        if (ruta.Excursionistas_apuntados.Count == 4)
                        {
                            break;
                        }
                        ruta.Excursionistas_apuntados.Add(ex);
                        ex.Rutas.Add(ruta);
                    }

                }
                else
                {
                    ruta = new Rutas(nombre, origen, destino, dificultad, duracion, fecha, num_excursionistas, finalizada);
                    ruta.Pdis.Add(pdis[pdi_añadir]);
                    pdi_añadir += 1;
                    if (pdi_añadir == 6)
                    {
                        ruta.Pdis.Add(pdis[pdi_añadir]);
                        pdi_añadir += 1;
                    }
                }
                listado.Add(ruta);
            }
            return listado;
        }
        private List<Excursionista> leerExcursionistas()
        {
            List<Excursionista> listado = new List<Excursionista>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("excursionistas.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string nombre = node.Attributes["Nombre"].Value;
                string apellidos = node.Attributes["Apellidos"].Value;
                int edad = Convert.ToInt32(node.Attributes["Edad"].Value);
                long telefono = Convert.ToInt64(node.Attributes["Telefono"].Value);
                Uri rutaFoto = new Uri(node.Attributes["RutaFoto"].Value, UriKind.RelativeOrAbsolute);
                Excursionista excursionista = new Excursionista(nombre, apellidos, edad, telefono, rutaFoto);
                listado.Add(excursionista);
            }
            return listado;
        }

        private List<Guia> leerGuias()
        {
            List<Guia> listado = new List<Guia>();
            XmlDocument doc = new XmlDocument();
            var fichero = Application.GetResourceStream(new Uri("guias.xml", UriKind.Relative));
            doc.Load(fichero.Stream);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string nombre = node.Attributes["Nombre"].Value;
                string apellidos = node.Attributes["Apellidos"].Value;
                long telefono = Convert.ToInt64(node.Attributes["Telefono"].Value);
                string email = node.Attributes["Email"].Value;
                Uri rutaFoto = new Uri(node.Attributes["RutaFoto"].Value, UriKind.Relative);
                string idiomas = node.Attributes["Idiomas"].Value;
                List<string> listaIdiomas = idiomas.Split(',').ToList();
                double valoracion = Convert.ToDouble(node.Attributes["Valoracion"].Value);
                Guia guia = new Guia(nombre, apellidos, telefono, email, rutaFoto, listaIdiomas, valoracion);
                listado.Add(guia);
            }
            return listado;
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void TextBlock_MouseEnter(object sender, MouseEventArgs e)
        {
            
        }

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void menuAcerca_Click(object sender, RoutedEventArgs e)
        {
            Acerca_De acerca = new Acerca_De();
            acerca.Show();
        }
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }
        private void menuExcursionista_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<Window1>())
            {
                this.Hide();
                Window ventanaExcursionistas = (Window)Application.Current.Windows.OfType<Window1>().FirstOrDefault();
                ventanaExcursionistas.Show();
            }
            else
            {
                ex = new Window1(gestor);
                ex.Show();
                this.Hide();
            }
        }

        private void menuRutas_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<VentanaRutas>())
            {
                this.Hide();
                Window ventanaRutas = (Window)Application.Current.Windows.OfType<VentanaRutas>().FirstOrDefault();
                ventanaRutas.Show();
            }
            else
            {
                ru = new VentanaRutas(gestor);
                ru.Show();

                this.Hide();
            }
        }

        private void menuGuias_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<VentanaGuias>())
            {
                this.Hide();
                Window ventanaGuias = (Window)Application.Current.Windows.OfType<VentanaGuias>().FirstOrDefault();
                ventanaGuias.Show();
            }
            else
            {
                gui = new VentanaGuias(gestor);
                gui.Show();
            }

        }
    }
}