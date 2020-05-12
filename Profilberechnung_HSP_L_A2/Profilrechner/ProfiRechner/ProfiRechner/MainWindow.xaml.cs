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

namespace ProfiRechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //Besser Rein in die Algen
            InitializeComponent();
        }

        private void lb_item_Rechteckprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Rechteckprofil.Visibility = Visibility.Visible;
            tab_Rechteckprofil.Visibility = Visibility.Visible;
        }

        private void btn_Rechteck_Close_Click(object sender, RoutedEventArgs e)
        {
            tabH_Rechteckprofil.Visibility = Visibility.Hidden;
            tab_Rechteckprofil.Visibility = Visibility.Hidden;
        }

        private void img_CloseButton_Rechteck_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Rechteckprofil.Visibility = Visibility.Hidden;
            tab_Rechteckprofil.Visibility = Visibility.Hidden;
        }

        private void img_CloseButton_Kreis_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Kreisprofil.Visibility = Visibility.Hidden;
            tab_Kreisprofil.Visibility = Visibility.Hidden;
        }

        private void img_CloseButton_Sonder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Hidden;
            tab_Sonderprofile.Visibility = Visibility.Hidden;
        }

        private void lb_item_Kreisprofil_MouseEnter(object sender, MouseEventArgs e)
        {
            lb_item_Kreisprofil.FontWeight = FontWeights.Bold;
        }
    }
}
