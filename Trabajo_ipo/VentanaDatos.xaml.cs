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
            gestor = new GestorDatos();
            
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
                gui = new VentanaGuias();
                gui.Show();
            }

        }
    }
}