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
        public VentanaDatos()
        {
            InitializeComponent(); 
            
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
            this.Close();
        }

        private void menuAcerca_Click(object sender, RoutedEventArgs e)
        {
            Acerca_De acerca = new Acerca_De();
            acerca.Show();
        }

        private void menuExcursionista_Click(object sender, RoutedEventArgs e)
        {
            Window1 excursionistas = new Window1();
            excursionistas.Show();
            this.Close();
        }
    }
}
