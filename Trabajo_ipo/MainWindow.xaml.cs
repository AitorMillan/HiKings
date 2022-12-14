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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Trabajo_ipo
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        bool click = false;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtboxUsuario.Text == "Miguel" && passBox.Password == "HiKings2022") { 
            VentanaDatos datos = new VentanaDatos();
            datos.Show();
            this.Close();
            }
            else
            {
                lblEstado.Content = "La contraseña o usuario introducidos son incorrectos";
                lblEstado.Visibility = Visibility.Visible;
                //lblEstado.Foreground = 
            }
        }

        private void passBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return){
                btnLogin_Click(sender, e);
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void txtboxUsuario_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!click)
            {
                click = true;
                txtboxUsuario.Text = "";
            }
        }
    }
}
