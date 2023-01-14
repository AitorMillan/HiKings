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
        List<Excursionista> excs_apuntados;
        List<Excursionista> excs;
        List<Excursionista> no_apuntados = new List<Excursionista>();
        List<Excursionista> apuntados = new List<Excursionista>();
        public VentanaContratar(List<Excursionista> exs, List<Excursionista> exs_apuntados)
        {
            excs = exs;
            excs_apuntados = exs_apuntados;
            InitializeComponent();
            anadirExcursionistas();
        }

        private void anadirExcursionistas()
        {
            foreach (Excursionista ex in excs)
            {   

                if(excs_apuntados.Contains(ex)){
                    lstBoxApuntados.Items.Add(ex.Nombre);
                    apuntados.Add(ex);
                }
                else
                {
                    lstBoxExcursionistas.Items.Add(ex.Nombre);
                    no_apuntados.Add(ex);
                }
            }
            if (lstBoxApuntados.Items.Count >= 4)
            {
                lblEstado.Content = "Se puede reservar la ruta";
                lblEstado.Foreground = new SolidColorBrush(Colors.Green);
                btnContratar.IsEnabled = true;
            }
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
            if (lstBoxApuntados.Items.Count >= 4 && btnContratar.IsEnabled == false)
            {
                lblEstado.Content = "Se puede reservar la ruta";
                lblEstado.Foreground = new SolidColorBrush(Colors.Green);
                btnContratar.IsEnabled = true;
            }
            else if (lstBoxApuntados.Items.Count < 4 && btnContratar.IsEnabled == true)
            {
                lblEstado.Content = "Se necesitan al menos 4 excursionistas para contratar la ruta";
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

        private void btnContratar_Click(object sender, RoutedEventArgs e)
        {
            excs_apuntados.Clear();
            foreach(Excursionista ex in apuntados)
            {
                excs_apuntados.Add(ex);
            }
            this.Close();
        }
    }
}
