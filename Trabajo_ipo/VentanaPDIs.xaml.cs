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
    /// Lógica de interacción para VentanaPDIs.xaml
    /// </summary>
    public partial class VentanaPDIs : Window
    {
        private List<Pdi> pdis;
        Pdi pdi_seleccionado;
        public VentanaPDIs(List<Pdi> pdis)
        {
            InitializeComponent();
            this.pdis = pdis;
            prepararVisualziacion();
        }

        public void prepararVisualziacion()
        {
            foreach (Pdi pdi in pdis)
            {
                lstBoxPdis.Items.Add(pdi.Nombre);
            }
            imgPdi.Source = new BitmapImage(new Uri("/Imagenes/persona_estandar.png", UriKind.Relative));
        }

        private void lstBoxPdis_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (lstBoxPdis.SelectedItem is null)
            {
                return;
            }
            
        }
    }
}
