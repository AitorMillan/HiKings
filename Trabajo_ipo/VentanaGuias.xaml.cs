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
    public partial class VentanaGuias : Window
    {
        List<Guia> listadoGuias;
        private Guia guia_seleccionado;
        VentanaDatos datos;
        public VentanaGuias()
        {
            InitializeComponent();
            listadoGuias = CargarArchivoXML();
            añadirGuias();
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

        private void añadirGuias()
        {
            foreach (Guia guia in listadoGuias)
            {
                lstBoxGuias.Items.Add(guia.Nombre);
            }
        }

        private List<Guia> CargarArchivoXML()
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

        private void lstBoxGuias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxGuias.SelectedItem is null) { return; }
            string nombre = lstBoxGuias.SelectedItem.ToString();
            int posicion = lstBoxGuias.SelectedIndex + 1;
            int posGuia = 0;
            foreach (Guia guia in listadoGuias)
            {
                posGuia++;
                if (guia.Nombre == nombre && posGuia == posicion)
                {
                    txtBoxNombre.Text = nombre;
                    txtBoxApellido.Text = guia.Apellidos;
                    txtBoxEmail.Text = Convert.ToString(guia.Email);
                    txtBoxTelefono.Text = Convert.ToString(guia.Telefono);
                    imgUsuario.Source = guia.Foto;
                    txtBoxRutaImagen.Text = Convert.ToString(guia.RutaFoto);
                    txtBoxValoracion.Text = Convert.ToString(guia.Valoracion);
                    lstBoxIdiomas.Items.Clear();
                    foreach (string idioma in guia.Idiomas) { 
                        lstBoxIdiomas.Items.Add(idioma);
                    }
                    //Falta por añadir las rutas relacionadas al guia
                    btnEliminarGuia.IsEnabled = true;
                    btnModificarDatos.IsEnabled = true;
                    guia_seleccionado = guia;
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
            txtBoxNombre.Text = "";
            txtBoxApellido.Text = "";
            txtBoxEmail.Text = "";
            txtBoxTelefono.Text = "";
            txtBoxValoracion.Text = "";
            //Falta añadir lo de limpiar las rutas
            imgUsuario.Source = new BitmapImage(new Uri("/Imagenes/persona_estandar.png", UriKind.Relative));
            txtBoxRutaImagen.Text = "/Imagenes/persona_estandar.png";
            lstBoxIdiomas.Items.Clear();
        }
        private void btnEliminarGuia_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres eliminar a esta persona? " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string nombre = txtBoxNombre.Text;
                int pos = lstBoxGuias.SelectedIndex;
                lstBoxGuias.Items.RemoveAt(pos);
                listadoGuias.Remove(guia_seleccionado);
                btnEliminarGuia.IsEnabled = false;
                btnModificarDatos.IsEnabled = false;
                limpiarTxtBox();
            }
        }

        private void btnAñadirGuia_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxApellido.Text == "" || txtBoxNombre.Text == "" || txtBoxEmail.Text == "" || txtBoxTelefono.Text == "" || txtBoxRutaImagen.Text == "" || txtBoxValoracion.Text == "" || lstBoxIdiomas.Items.IsEmpty)
            {
                MessageBox.Show("Por favor rellene todos los cambios antes de añadir un usuario", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                try
                {
                    if (Convert.ToDouble(txtBoxValoracion.Text) <= 0 || Convert.ToDouble(txtBoxValoracion.Text) > 5)
                    {
                        MessageBox.Show("Por favor introduzca una valoración válida", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;

                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Por favor introduzca una valoración válida", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres añadir a esta persona?: " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (messageBoxResult == MessageBoxResult.Yes)
                {
                    try
                    {
                        string nombre = txtBoxNombre.Text;
                        string apellidos = txtBoxApellido.Text;
                        string email = txtBoxEmail.Text;
                        long telefono = Convert.ToInt64(txtBoxTelefono.Text);
                        double valoracion = Convert.ToDouble(txtBoxValoracion.Text);
                        List<string> idiomas = lstBoxIdiomas.Items.Cast<String>().ToList();
                        Uri rutaFoto = new Uri(txtBoxRutaImagen.Text, UriKind.RelativeOrAbsolute);
                        Guia guia = new Guia(nombre, apellidos, telefono, email, rutaFoto, idiomas, valoracion);
                        listadoGuias.Add(guia);
                        lstBoxGuias.Items.Add(nombre);
                        lstBoxGuias.SelectedIndex = -1;
                        btnEliminarGuia.IsEnabled = false;
                        btnModificarDatos.IsEnabled = false;
                        limpiarTxtBox();

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
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que desea editar los datos de esta persona?: " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string nombre = txtBoxNombre.Text;
                string apellidos = txtBoxApellido.Text;
                string email = txtBoxEmail.Text;
                long telefono = Convert.ToInt64(txtBoxTelefono.Text);
                double valoracion = Convert.ToDouble(txtBoxValoracion.Text);
                Uri rutaFoto = new Uri(txtBoxRutaImagen.Text, UriKind.RelativeOrAbsolute);
                List<string> idiomas = lstBoxIdiomas.Items.Cast<String>().ToList();
                Guia guia= new Guia(nombre, apellidos, telefono, email, rutaFoto, idiomas, valoracion);
                int pos = lstBoxGuias.SelectedIndex;
                lstBoxGuias.Items.RemoveAt(pos);
                listadoGuias.Remove(guia_seleccionado);
                listadoGuias.Add(guia);
                lstBoxGuias.Items.Add(nombre);
                lstBoxGuias.SelectedIndex = -1;
                btnEliminarGuia.IsEnabled = false;
                btnModificarDatos.IsEnabled = false;
                limpiarTxtBox();
            }
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
            txtBoxRutaImagen.Text = "Imagenes/persona_estandar.png";
        }

        private void btnAñadirIdioma_Click(object sender, RoutedEventArgs e)
        {
            if (txtBoxIdioma.Text == "")
            {
                MessageBox.Show("Por favor rellene el campo de idioma antes de añadirla", "Error al añadir el idioma", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBoxIdioma.Clear();
            }
            else if (lstBoxIdiomas.Items.Contains(txtBoxIdioma.Text)) {
                MessageBox.Show("El idioma que desea introducir ya se encuentra contenido", "Error al añadir el idioma", MessageBoxButton.OK, MessageBoxImage.Error);
                txtBoxIdioma.Clear();
            }
            else
            {
                lstBoxIdiomas.Items.Add(Convert.ToString(txtBoxIdioma.Text));
                txtBoxIdioma.Clear();
            }
        }

        private void lstBoxIdiomas_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEliminarIdioma.IsEnabled = true;
        }

        private void btnEliminarIdioma_Click(object sender, RoutedEventArgs e)
        {
            lstBoxIdiomas.Items.Remove(lstBoxIdiomas.SelectedItem);
        }
    }
}
