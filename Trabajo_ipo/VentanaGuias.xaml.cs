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
        private Guia guia_seleccionado;
        VentanaDatos datos; 
        Window1 ex;
        VentanaRutas ru;
        GestorDatos Gestor;
        public VentanaGuias(GestorDatos gestor)
        {
            InitializeComponent();
            Gestor = gestor;
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
            foreach (Guia guia in Gestor.Guias)
            {
                lstBoxGuias.Items.Add(guia.Nombre);
            }
        }

        private void lstBoxGuias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxGuias.SelectedItem is null) { return; }
            string nombre = lstBoxGuias.SelectedItem.ToString();
            int posicion = lstBoxGuias.SelectedIndex + 1;
            int posGuia = 0;
            foreach (Guia guia in Gestor.Guias)
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
                    lstBoxRutas.Items.Clear();
                    foreach (string idioma in guia.Idiomas) { 
                        lstBoxIdiomas.Items.Add(idioma);
                    }
                    foreach (string nombre_ruta in guia.Rutas)
                    {
                        lstBoxRutas.Items.Add(nombre_ruta);
                    }
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
            lstBoxRutas.Items.Clear();
        }
        private void btnEliminarGuia_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres eliminar a esta persona? " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string nombre = txtBoxNombre.Text;
                int pos = lstBoxGuias.SelectedIndex;
                lstBoxGuias.Items.RemoveAt(pos);
                Gestor.Guias.Remove(guia_seleccionado);
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
                catch (System.FormatException)
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
                        Gestor.Guias.Add(guia);
                        lstBoxGuias.Items.Add(nombre);
                        lstBoxGuias.SelectedIndex = -1;
                        btnEliminarGuia.IsEnabled = false;
                        btnModificarDatos.IsEnabled = false;
                        limpiarTxtBox();

                    }
                    catch (System.FormatException)
                    {
                        MessageBox.Show("Por favor introduzca un número de teléfono válido", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
        }

        private void btnModificarDatos_Click(object sender, RoutedEventArgs e)
        {
                try
                {
            if (Convert.ToDouble(txtBoxValoracion.Text) <= 0 || Convert.ToDouble(txtBoxValoracion.Text) > 5)
            {
                MessageBox.Show("Por favor introduzca una valoración válida", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                return;

            }
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
                    Guia guia = new Guia(nombre, apellidos, telefono, email, rutaFoto, idiomas, valoracion);
                    int pos = lstBoxGuias.SelectedIndex;
                    lstBoxGuias.Items.RemoveAt(pos);
                    Gestor.Guias.Remove(guia_seleccionado);
                    Gestor.Guias.Add(guia);
                    lstBoxGuias.Items.Add(nombre);
                    lstBoxGuias.SelectedIndex = -1;
                    btnEliminarGuia.IsEnabled = false;
                    btnModificarDatos.IsEnabled = false;
                    limpiarTxtBox();
                }
            }
                catch (System.FormatException)
                {
                    MessageBox.Show("Por favor no introduzca valores alfabéticos en los siguientes campos:\n-Teléfono\n-Valoración", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private void MenuRutas_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<VentanaRutas>())
            {
                this.Hide();
                VentanaRutas ventanaRutas = (VentanaRutas)Application.Current.Windows.OfType<VentanaRutas>().FirstOrDefault();
                ventanaRutas.Show();
            }
            else
            {
                ru = new VentanaRutas(Gestor);
                ru.Show();
                this.Hide();
            }
        }

        private void menuExcursionistas_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<Window1>())
            {
                this.Hide();
                Window1 ventanaExcursionistas = (Window1)Application.Current.Windows.OfType<Window1>().FirstOrDefault();
                ventanaExcursionistas.Show();
            }
            else
            {
                ex = new Window1(Gestor);
                ex.Show();
                this.Hide();
            }
        }
    }
}
