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
            //Branch-Test
            InitializeComponent();
        }


        #region Rechteckprofil-Steuerung


        private void lb_item_Rechteckprofil_MouseEnter(object sender, MouseEventArgs e)
        {
            lb_item_Rechteckprofil.FontWeight = FontWeights.Bold;
        }

        private void lb_item_Rechteckprofil_MouseLeave(object sender, MouseEventArgs e)
        {
            lb_item_Rechteckprofil.FontWeight = FontWeights.Normal;
        }

        private void lb_item_Rechteckprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Rechteckprofil.Visibility = Visibility.Visible;
            tab_Rechteckprofil.Visibility = Visibility.Visible;
        }

        private void img_CloseButton_Rechteck_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Rechteckprofil.Visibility = Visibility.Hidden;
            tab_Rechteckprofil.Visibility = Visibility.Hidden;
        }

        #region Checkboxen_Profilwahl
        private void ChB_Vollprofil_Checked(object sender, RoutedEventArgs e)
        {
            ChB_Hohlprofil.IsChecked = false;
        }
        private void ChB_Hohlprofil_Checked(object sender, RoutedEventArgs e)
        {
            ChB_Vollprofil.IsChecked = false;
        }

        private void lb_item_Rechteck_Hohlprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChB_Hohlprofil.IsChecked = true;

        }
        private void lb_item_Rechteck_Vollprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {           
                ChB_Vollprofil.IsChecked = true;           
        }



        #endregion
        #endregion

        #region Kreisprofil-Steuerung

        private void lb_item_Kreisprofil_MouseEnter(object sender, MouseEventArgs e)
        {
            lb_item_Kreisprofil.FontWeight = FontWeights.Bold;
        }

        private void lb_item_Kreisprofil_MouseLeave(object sender, MouseEventArgs e)
        {
            lb_item_Kreisprofil.FontWeight = FontWeights.Normal;
        }

        private void lb_item_Kreisprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Kreisprofil.Visibility = Visibility.Visible;
            tab_Kreisprofil.Visibility = Visibility.Visible;
        }

        private void img_CloseButton_Kreis_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Kreisprofil.Visibility = Visibility.Hidden;
            tab_Kreisprofil.Visibility = Visibility.Hidden;
        }

        #endregion

        #region Sonderprofil-Steuerung

        private void lb_item_Sonderprofil_MouseEnter(object sender, MouseEventArgs e)
        {
            lb_item_Sonderprofil.FontWeight = FontWeights.Bold;
        }

        private void lb_item_Sonderprofil_MouseLeave(object sender, MouseEventArgs e)
        {
            lb_item_Sonderprofil.FontWeight = FontWeights.Normal;
        }

        private void lb_item_Sonderprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Visible;
            tab_Sonderprofile.Visibility = Visibility.Visible;
        }

        private void img_CloseButton_Sonder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Hidden;
            tab_Sonderprofile.Visibility = Visibility.Hidden;
        }





        #endregion


    }
}
