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
    /// Lógica de interacción para VentanaRutas.xaml
    /// </summary>
    public partial class VentanaRutas : Window
    {
        VentanaDatos datos;
        Window1 ex;
        Rutas ruta_seleccionada;
        List<Rutas> listadoRutas;
        List<Excursionista> listadoExcursionistas;
        public VentanaRutas()
        {
            InitializeComponent();
            listadoRutas = CargarArchivoXML();
            anadirRutas();
        }

        private void anadirRutas()
        {
            foreach (Rutas ruta in listadoRutas)
            {
                if (ruta.Finalizada == true)
                {
                    lstBoxRutas.Items.Add(ruta.Nombre + "(Finalizada)");
                }
                else
                {

                    lstBoxRutas.Items.Add(ruta.Nombre);
                }
            }
        }
        private List<Rutas> CargarArchivoXML()
        {
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
                    ruta = new Rutas(nombre, origen, destino, dificultad, duracion, fecha, num_excursionistas, finalizada, incidencias);
                }
                else
                {
                    ruta = new Rutas(nombre, origen, destino, dificultad, duracion, fecha, num_excursionistas, finalizada);
                }
                listado.Add(ruta);
            }
            return listado;
        }
        private void menuAcerca_Click(object sender, RoutedEventArgs e)
        {
            Acerca_De acerca = new Acerca_De();
            acerca.Show();
        }

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MenuPerfil_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<VentanaDatos>())
            {
                this.Hide();
                Window VentanaDatos = (Window)Application.Current.Windows.OfType<VentanaDatos>().FirstOrDefault();
                VentanaDatos.Show();
            }
            else
            {
                datos = new VentanaDatos();
                datos.Show();
                this.Hide();
            }
        }
        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private void menuExcursionistas_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<Window1>())
            {
                this.Hide();
                Window ventanaExcursionistas = (Window)Application.Current.Windows.OfType<Window1>().FirstOrDefault();
                ventanaExcursionistas.Show();
            }
            else
            {
                ex = new Window1();
                ex.Show();
                this.Hide();
            }
        }

        private void lstBoxRutas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxRutas.SelectedItem is null)
            {
                return;
            }
            string nombre = lstBoxRutas.SelectedItem.ToString();
            nombre = nombre.Split('(')[0];
            int posicion = lstBoxRutas.SelectedIndex + 1;
            int posRuta = 0;
            foreach (Rutas ruta in listadoRutas)
            {
                posRuta++;
                if (ruta.Nombre == nombre && posRuta == posicion)
                {
                    txtBoxNombre.Text = nombre;
                    txtBoxOrigen.Text = ruta.Origen;
                    txtBoxDestino.Text = ruta.Destino;
                    comboBoxDificultad.Text = ruta.Dificultad;
                    txtBoxDuracion.Text = Convert.ToString(ruta.Duracion);
                    dateFecha.Text = Convert.ToString(ruta.Fecha);
                    txtboxNumExcursionistas.Text = Convert.ToString(ruta.Num_excursionistas);
                    btnEliminarRuta.IsEnabled = true;
                    btnModificarRuta.IsEnabled = true;
                    ruta_seleccionada = ruta;
                    if (ruta.Finalizada == true)
                    {
                        btnContratarRuta.IsEnabled = false;
                        btnIncidencias.IsEnabled = true;
                    }
                    else
                    {
                        btnIncidencias.IsEnabled=false;
                        btnContratarRuta.IsEnabled = true;
                    }
                    break;
                }
            }
        }

        private void btnIncidencias_Click(object sender, RoutedEventArgs e)
        {
            VentanaIncidencias incidencias = new VentanaIncidencias(ruta_seleccionada.Incidencias);
            incidencias.Show();
        }

        private void btnAnadirRuta_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxNombre.Text == "" || txtBoxOrigen.Text == "" || txtBoxDestino.Text == "" || txtBoxDuracion.Text == "" || dateFecha.Text == "" || comboBoxDificultad.Text == "")
            {
                MessageBox.Show("Por favor rellene todos los cambios antes de añadir un usuario", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
