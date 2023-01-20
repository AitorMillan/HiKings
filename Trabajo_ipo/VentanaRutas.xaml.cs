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
        VentanaGuias gui;
        Rutas ruta_seleccionada;
        Excursionista ex_seleccionado;
        GestorDatos Gestor;
        public VentanaRutas(GestorDatos gestor)
        {
            InitializeComponent();

            Gestor = gestor;
            anadirRutas();
        }

        private void anadirRutas()
        {
            foreach (Rutas ruta in Gestor.Rutas)
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
            foreach (Rutas ruta in Gestor.Rutas)
            {
                posRuta++;
                if (ruta.Nombre == nombre && posRuta == posicion)
                {
                    txtBoxNombre.Text = nombre;
                    txtBoxOrigen.Text = ruta.Origen;
                    txtBoxDestino.Text = ruta.Destino;
                    comboBoxDificultad.Text = ruta.Dificultad;
                    txtBoxDuracion.Text = Convert.ToString(ruta.Duracion);
                    if (ruta.Guia != null)
                    {
                        txtBoxGuia.Text = ruta.Guia.Nombre;
                        btnInfoGuia.IsEnabled = true;
                    }
                    else
                    {
                        txtBoxGuia.Text = "";
                        btnInfoGuia.IsEnabled = false;
                    }
                    dateFecha.Text = Convert.ToString(ruta.Fecha);
                    txtboxNumExcursionistas.Text = Convert.ToString(ruta.Excursionistas_apuntados.Count);
                    btnEliminarRuta.IsEnabled = true;
                    btnModificarRuta.IsEnabled = true;
                    btnPdis.IsEnabled = true;
                    ruta_seleccionada = ruta;
                    if (ruta.Finalizada == true)
                    {
                        btnModificarRuta.IsEnabled = false;
                        btnContratarRuta.IsEnabled = false;
                        btnIncidencias.IsEnabled = true;
                    }
                    else
                    {
                        btnIncidencias.IsEnabled=false;
                        btnContratarRuta.IsEnabled = true;
                    }
                    cargarApuntados();
                    break;
                }
            }

        }

        private void cargarApuntados()
        {
            lstBoxApuntados.Items.Clear();
            if(ruta_seleccionada.Excursionistas_apuntados.Count == 0)
            {
                lstBoxApuntados.Items.Add("No hay excursionistas para esta ruta");
                btnVerDatosExcursionista.IsEnabled = false;
            }
            else
            {
                foreach(Excursionista ex in ruta_seleccionada.Excursionistas_apuntados)
                {
                    lstBoxApuntados.Items.Add(ex.Nombre);
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
            else
            {
                try
                {
                    MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres añadir esta ruta?: " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (messageBoxResult == MessageBoxResult.Yes)
                    {
                        string nombre = txtBoxNombre.Text;
                        string origen = txtBoxOrigen.Text;
                        string destino = txtBoxDestino.Text;
                        int duracion = Convert.ToInt32(txtBoxDuracion.Text);
                        string fecha = dateFecha.Text;
                        string dificultad = comboBoxDificultad.Text;
                        Rutas ruta = new Rutas(nombre, origen, destino, dificultad, duracion, fecha, 0, false);
                        Gestor.Rutas.Add(ruta);
                        lstBoxRutas.Items.Add(nombre);
                        lstBoxRutas.SelectedIndex = -1;
                        limpiarTxtBox();
                    }

                } catch (System.FormatException)
                {
                    MessageBox.Show("Por favor no introduzca decimales u otros carácteres en la duración de la ruta", "Error al añadir la ruta", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void limpiarTxtBox()
        {
            btnEliminarRuta.IsEnabled = false;
            btnModificarRuta.IsEnabled = false;
            btnPdis.IsEnabled = false;
            txtBoxDestino.Text = "";
            txtBoxDuracion.Text = "";
            txtBoxNombre.Text = "";
            txtBoxGuia.Text = "";
            txtboxNumExcursionistas.Text = "";
            txtBoxOrigen.Text = "";
            comboBoxDificultad.Text = "";
            lstBoxApuntados.Items.Clear();
        }

        private void btnEliminarRuta_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que quieres eliminar esta ruta? " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                string nombre = txtBoxNombre.Text;
                string origen = txtBoxOrigen.Text;
                string destino = txtBoxDestino.Text;
                int duracion = Convert.ToInt32(txtBoxDuracion.Text);
                string fecha = dateFecha.Text;
                string dificultad = comboBoxDificultad.Text;
                int pos = lstBoxRutas.SelectedIndex;
                lstBoxRutas.Items.RemoveAt(pos);
                Gestor.Rutas.Remove(ruta_seleccionada);
                limpiarTxtBox();
            }
        }

        private void btnModificarRuta_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult messageBoxResult = MessageBox.Show("¿Estás seguro de que desea editar los datos de esta persona?: " + txtBoxNombre.Text, "Por favor confirma", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (messageBoxResult == MessageBoxResult.Yes)
            {
                try
                {
                string nombre = txtBoxNombre.Text;
                int num_excursionistas = Convert.ToInt32(txtboxNumExcursionistas.Text);
                string origen = txtBoxOrigen.Text;
                string destino = txtBoxDestino.Text;
                int duracion = Convert.ToInt32(txtBoxDuracion.Text);
                string fecha = dateFecha.Text;
                string dificultad = comboBoxDificultad.Text;
                Rutas ruta = new Rutas(nombre,origen,destino,dificultad,duracion,fecha,num_excursionistas,false,ruta_seleccionada.Guia,ruta_seleccionada.Pdis,ruta_seleccionada.Excursionistas_apuntados);
                int pos = lstBoxRutas.SelectedIndex;
                lstBoxRutas.Items.RemoveAt(pos);
                Gestor.Rutas.Remove(ruta_seleccionada);
                Gestor.Rutas.Add(ruta);
                lstBoxRutas.Items.Add(nombre);
                lstBoxRutas.SelectedIndex = -1;
                limpiarTxtBox();
                }
                catch (System.FormatException)
                {
                    MessageBox.Show("Por favor no introduzca decimales u otros carácteres en la duración de la ruta", "Completado", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void btnContratarRuta_Click(object sender, RoutedEventArgs e)
        {
            VentanaContratar ventana = new VentanaContratar(Gestor.Excursionistas,ruta_seleccionada,Gestor.Guias);
            ventana.Show();
            lstBoxRutas.SelectedIndex = -1;
            limpiarTxtBox();
        }

        private void btnVerDatosExcursionista_Click(object sender, RoutedEventArgs e)
        {
            VentanaDatosPersona datos_p = new VentanaDatosPersona(ex_seleccionado.Nombre,ex_seleccionado.Apellidos,
                                                                    Convert.ToString(ex_seleccionado.Telefono),
                                                                    ex_seleccionado.RutaFoto);
            datos_p.Show();
        }

        private void lstBoxApuntados_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxApuntados.SelectedItem is null)
            {
                return;
            }
            string nombre = lstBoxApuntados.SelectedItem.ToString();
            int posicion = lstBoxApuntados.SelectedIndex + 1;
            int posExcursionista = 0;
            foreach (Excursionista excursionista in ruta_seleccionada.Excursionistas_apuntados)
            {
                posExcursionista++;
                if (excursionista.Nombre == nombre && posExcursionista == posicion)
                {
                    ex_seleccionado = excursionista;
                    btnVerDatosExcursionista.IsEnabled = true;
                    break;
                }
            }
        }

        private void MenuGuias_Click(object sender, RoutedEventArgs e)
        {
            if (IsWindowOpen<VentanaGuias>())
            {
                this.Hide();
                VentanaGuias VentanaGuias = (VentanaGuias)Application.Current.Windows.OfType<VentanaGuias>().FirstOrDefault();
                VentanaGuias.Show();
            }
            else
            {
                gui = new VentanaGuias(Gestor);
                gui.Show();
                this.Hide();
            }
        }

        private void btnInfoGuia_Click(object sender, RoutedEventArgs e)
        {
            VentanaDatosPersona datos_p = new VentanaDatosPersona(ruta_seleccionada.Guia.Nombre, ruta_seleccionada.Guia.Apellidos,
                                                        Convert.ToString(ruta_seleccionada.Guia.Telefono),
                                                        ruta_seleccionada.Guia.RutaFoto);
            datos_p.Show();
        }

        private void btnPdis_Click(object sender, RoutedEventArgs e)
        {
            VentanaPDIs pdis = new VentanaPDIs(ruta_seleccionada.Pdis);
            pdis.Show();
        }

        private void menuAyuda_Click(object sender, RoutedEventArgs e)
        {
            VentanaAyuda ayuda = new VentanaAyuda();
            ayuda.Show();
        }
    }
}
