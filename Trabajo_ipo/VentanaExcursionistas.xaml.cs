using Microsoft.Win32;
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
    /// Lógica de interacción para Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        List<Excursionista> listadoExcursionistas;
        private Excursionista excursionista_seleccionado;
        VentanaDatos datos;
        public Window1()
        {
            InitializeComponent();
            listadoExcursionistas = CargarArchivoXML();
            añadirExcursionistas();
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

        private void menuAcerca_Click(object sender, RoutedEventArgs e)
        {
            Acerca_De acerca = new Acerca_De();
            acerca.Show();
        }

        private void menuSalir_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void añadirExcursionistas()
        {
            foreach (Excursionista excursionista in listadoExcursionistas)
            {
                lstBoxExcursionistas.Items.Add(excursionista.Nombre);
            }
        }

        private List<Excursionista> CargarArchivoXML()
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
                Uri rutaFoto = new Uri(node.Attributes["RutaFoto"].Value, UriKind.Relative);
                Excursionista excursionista = new Excursionista(nombre,apellidos,edad ,telefono,rutaFoto);
                listado.Add(excursionista);
            }
            return listado;
        }

        private void lstBoxExcursionistas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxExcursionistas.SelectedItem is null)
            {
                txtBoxApellido.Text = "f";
                return;
            }
            string nombre = lstBoxExcursionistas.SelectedItem.ToString();
            int posicion = lstBoxExcursionistas.SelectedIndex + 1;
            int posExcursionista = 0;
            foreach (Excursionista excursionista in listadoExcursionistas)
            {
                posExcursionista++;
                if (excursionista.Nombre == nombre && posExcursionista == posicion)
                {
                    txtBoxNombre.Text = nombre;
                    txtBoxApellido.Text = excursionista.Apellidos;
                    txtBoxEdad.Text = Convert.ToString(excursionista.Edad);
                    txtBoxTelefono.Text = Convert.ToString(excursionista.Telefono);
                    imgUsuario.Source = excursionista.Foto;
                    txtBoxRutaImagen.Text =Convert.ToString(excursionista.RutaFoto);
                    btnEliminarUsuario.IsEnabled = true;
                    btnModificarDatos.IsEnabled = true;
                    excursionista_seleccionado = excursionista;
                    break;
                }
            }
        }

        private void btnBuscarImagen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog a = new OpenFileDialog();

            if (a.ShowDialog() == true)
            {
                txtBoxRutaImagen.Text = a.FileName;
                imgUsuario.Source = new BitmapImage(new Uri(a.FileName, UriKind.RelativeOrAbsolute));
            }
        }

        private void limpiarTxtBox()
        {
            txtBoxApellido.Text = "";
            txtBoxNombre.Text = "";
            txtBoxEdad.Text = "";
            txtBoxTelefono.Text = "";
            imgUsuario.Source = new BitmapImage(new Uri("/Imagenes/persona_estandar.png",UriKind.Relative));
            txtBoxRutaImagen.Text = "/Imagenes/persona_estandar.png";
        }
        private void btnEliminarUsuario_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres eliminar a esta persona? "+txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string nombre = txtBoxNombre.Text;
                lstBoxExcursionistas.Items.Remove(nombre);
                listadoExcursionistas.Remove(excursionista_seleccionado);
                btnEliminarUsuario.IsEnabled = false;
                btnModificarDatos.IsEnabled = false;
                limpiarTxtBox();
            }
        }

        private void btnAñadirExcursionista_Click(object sender, RoutedEventArgs e)
        {
            if(txtBoxApellido.Text == "" || txtBoxNombre.Text == "" || txtBoxEdad.Text == "" || txtBoxTelefono.Text == "" || txtBoxRutaImagen.Text == "")
            {
                MessageBox.Show("Por favor rellene todos los cambios antes de añadir un usuario", "Error al añadir el usuario",MessageBoxButton.OK,MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (Convert.ToInt32(txtBoxEdad.Text) <= 0 || Convert.ToInt32(txtBoxEdad.Text)>100)
                    {
                        MessageBox.Show("Por favor introduzca una edad válida", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Por favor introduzca una edad válida", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres añadir a esta persona?: " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        string nombre = txtBoxNombre.Text;
                        string apellidos = txtBoxApellido.Text;
                        int edad = Convert.ToInt32(txtBoxEdad.Text);
                        long telefono = Convert.ToInt64(txtBoxTelefono.Text);
                        Uri rutaFoto = new Uri(txtBoxRutaImagen.Text, UriKind.RelativeOrAbsolute);
                        Excursionista ex = new Excursionista(nombre, apellidos, edad, telefono, rutaFoto);
                        listadoExcursionistas.Add(ex);
                        lstBoxExcursionistas.Items.Add(nombre);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Por favor introduzca un número de teléfono válido", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnModificarDatos_Click(object sender, RoutedEventArgs e)
        {
            //POR IMPLEMENTAR
        }

        public static bool IsWindowOpen<T>(string name = "") where T : Window
        {
            return string.IsNullOrEmpty(name)
               ? Application.Current.Windows.OfType<T>().Any()
               : Application.Current.Windows.OfType<T>().Any(w => w.Name.Equals(name));
        }

        private void btnEliminarFoto_Click(object sender, RoutedEventArgs e)
        {
            imgUsuario.Source = new BitmapImage(new Uri("Imagenes/persona_estandar.png", UriKind.Relative));
        }
    }
}
