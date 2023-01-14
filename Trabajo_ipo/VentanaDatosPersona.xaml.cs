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
    /// Lógica de interacción para VentanaDatosPersona.xaml
    /// </summary>
    public partial class VentanaDatosPersona : Window
    {
        public VentanaDatosPersona(String nombre, String apellidos, String telefono, Uri ruta_foto)
        {
            InitializeComponent();
            txtBoxNombre.Text = nombre;
            txtBoxApellidos.Text = apellidos;
            txtBoxTelefono.Text = telefono;
            imgPersona.Source = new BitmapImage(ruta_foto);
        }
    }
}
