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

namespace Trabajo_ipo
{
    /// <summary>
    /// Lógica de interacción para VentanaContratar.xaml
    /// </summary>
    public partial class VentanaContratar : Window
    {
        List<Excursionista> excs;
        List<Excursionista> no_apuntados = new List<Excursionista>();
        List<Excursionista> apuntados = new List<Excursionista>();
        Rutas Ruta;
        bool siguiente_pulsado = false;
        Guia guia_actual;
        List<Guia> Guias = new List<Guia>();
        public VentanaContratar(List<Excursionista> exs, Rutas ruta, List<Guia> guias)
        {
            excs = exs;
            Ruta = ruta;
            Guias = guias;
            guia_actual = ruta.Guia;
            InitializeComponent();
            anadirExcursionistas();
        }

        private void anadirExcursionistas()
        {
            foreach (Excursionista ex in excs)
            {   

                if(Ruta.Excursionistas_apuntados.Contains(ex)){
                    lstBoxApuntados.Items.Add(ex.Nombre);
                    apuntados.Add(ex);
                }
                else
                {
                    lstBoxExcursionistas.Items.Add(ex.Nombre);
                    no_apuntados.Add(ex);
                }
            }
            compruebaContratar();
        }

        private void btnSalir_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnAnadir_Click(object sender, RoutedEventArgs e)
        {
            if (lstBoxExcursionistas.SelectedIndex == -1)
            {
                MessageBox.Show("Por favor seleccione un excursionista primero", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
            lstBoxApuntados.Items.Add(lstBoxExcursionistas.SelectedItem);
            apuntados.Add(no_apuntados[(lstBoxExcursionistas.SelectedIndex)]);
            no_apuntados.RemoveAt(lstBoxExcursionistas.SelectedIndex);
            lstBoxExcursionistas.Items.RemoveAt(lstBoxExcursionistas.SelectedIndex);
            compruebaContratar();
            }


        }

        private void compruebaContratar()
        {
            if (lstBoxApuntados.Items.Count >= 4 && lstBoxApuntados.Items.Count <= 20)
            {
                lblEstado.Content = "Se puede reservar la ruta";
                lblEstado.Foreground = new SolidColorBrush(Colors.Green);
                btnContratar.IsEnabled = true;
            }
            else if (lstBoxApuntados.Items.Count < 4)
            {
                lblEstado.Content = "Se necesitan al menos 4 excursionistas para contratar la ruta";
                lblEstado.Foreground = new SolidColorBrush(Colors.Red);
                btnContratar.IsEnabled = false;
            }
            else
            {
                lblEstado.Content = "No se pueden añadir más de 20 excursionistas a la ruta";
                lblEstado.Foreground = new SolidColorBrush(Colors.Red);
                btnContratar.IsEnabled = false;
            }
        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
                if (lstBoxApuntados.SelectedIndex == -1)
                {
                   MessageBox.Show("Por favor seleccione un excursionista primero", "Error al añadir el usuario", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                lstBoxExcursionistas.Items.Add(lstBoxApuntados.SelectedItem);
                no_apuntados.Add(apuntados[lstBoxApuntados.SelectedIndex]);
                apuntados.RemoveAt(lstBoxApuntados.SelectedIndex);
                lstBoxApuntados.Items.RemoveAt(lstBoxApuntados.SelectedIndex);
                compruebaContratar();
            }

        }

        private void anadirGuias()
        {
            foreach(Guia g in Guias)
            {
                lstBoxGuias.Items.Add(g.Nombre);
            }
        }
        private void btnContratar_Click(object sender, RoutedEventArgs e)
        {
            if (!siguiente_pulsado)
            {
                btnContratar.Content = "Contratar";
                lstBoxApuntados.Visibility = Visibility.Hidden;
                lstBoxExcursionistas.Visibility = Visibility.Hidden;
                lstBoxGuias.Visibility = Visibility.Visible;
                btnAnadir.Visibility = Visibility.Hidden;
                btnEliminar.Visibility = Visibility.Hidden;
                lblEstado.Content = "Para finalizar seleccione el guía que guiará la ruta";
                lblApuntados.Visibility = Visibility.Hidden;
                lblExcursionistas.Visibility= Visibility.Hidden;
                anadirGuias();
                btnContratar.IsEnabled = false;
                siguiente_pulsado = true;
            }
            else
            {
                Ruta.Excursionistas_apuntados.Clear();
                Ruta.Guia = Guias[lstBoxGuias.SelectedIndex];
                Guias[lstBoxGuias.SelectedIndex].Rutas.Add(Ruta.Nombre);
                foreach (Excursionista ex in apuntados)
                {
                    Ruta.Excursionistas_apuntados.Add(ex);
                    ex.Rutas.Add(Ruta);
                }
                if(guia_actual != null)
                {
                    guia_actual.Rutas.Remove(Ruta.Nombre);
                }
                this.Close();
            }
        }

        private void lstBoxGuias_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnContratar.IsEnabled = true;
        }
    }
}
