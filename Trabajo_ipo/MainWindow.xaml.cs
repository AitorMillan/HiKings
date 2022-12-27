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
        bool click_user = false;
        bool click_pass = false;
        public MainWindow()
        {
            InitializeComponent();
            this.WindowState = WindowState.Maximized;
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if (txtboxUsuario.Text == "admin") {
                if (passBox.Password == "admin") { 
                    VentanaDatos datos = new VentanaDatos();
                    datos.Show();
                    this.Close();
                }
                else
                {
                    imgErrorPass.Visibility = Visibility.Visible;
                    lblEstado.Content = "La contraseña introducida no pertenece a este usuario";
                    lblEstado.Visibility = Visibility.Visible;
                    imgUser.Source = new BitmapImage(new Uri("/Imagenes/img_tick.png", UriKind.Relative));
                    imgErrorPass.Source = new BitmapImage(new Uri("/Imagenes/img_error.png",UriKind.Relative));
                }
            }
            
            else {
                imgErrorPass.Visibility = Visibility.Hidden;
                lblEstado.Content = "El usuario introducido es incorrecto";
                lblEstado.Visibility = Visibility.Visible;
                imgUser.Source = new BitmapImage(new Uri("/Imagenes/img_error.png", UriKind.Relative));
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
            if (!click_user)
            {
                click_user = true;
                txtboxUsuario.Text = "";
            }
        }

        private void passBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!click_pass)
            {
                click_pass = true;
                passBox.Password = "";
            }
        }

        private void menuAcerca_Click(object sender, RoutedEventArgs e)
        {
            Acerca_De acerca = new Acerca_De();
            acerca.Show();
        }

        private void txtboxUsuario_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Return)
            {
                passBox.Focus();
            }
        }
    }
}
