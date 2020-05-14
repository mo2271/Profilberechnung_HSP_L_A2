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
        #region Berechnungsmethoden
        public void Rechteckprofil_Berechnung()     // exemplarische Berechnung in übergeordneter Methode
        {
            Double.TryParse(tbx_Input_RechteckBreite.Text, out double b);   // Eingaben in Double umwandeln
            Double.TryParse(tbx_Input_RechteckHoehe.Text, out double h);

            double Flaeche;     // Definition des Ergebnis-Doubles

            Flaeche = b * h;    // Durchführung der Berechnung

            string resFlaeche = Convert.ToString(Flaeche);  // Ergebnis-Double in string zurückwandeln

            lbl_Rechteck_Fläche_Ergebnis.Content = resFlaeche;  // Rückgabe des Ergebnisstrings an das Ergebnis-Label
        }

        public void Rechteckprofil_hohl_Berechnung()
        {

        }

        public void Kreisprofil_Berechnung()
        {

        }

        public void Kreisprofil_hohl_Berechnung()
        {

        }

        public void I_Profil_Berechnung()
        {

        }

        #endregion

        public MainWindow()
        {
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

        private void btn_StartRechteckprofil_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Rechteckprofil_Berechnung();    // Aufruf der Rechteckprofil-Berechnung
        }

        private void btn_StartRechteckprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Rechteckprofil_hohl_Berechnung();   // Aufruf der Rechteck-Hohlprofil-Berechnung
        }

        #region Checkboxen_Profilwahl
        private void ChB_Rechteckprofil_Hohlprofil_Checked(object sender, RoutedEventArgs e)
        {
            ChB_Rechteckprofil_Vollprofil.IsChecked = false;
            img_Rechteckprofil.Visibility = Visibility.Hidden;
            img_Rechteckprofil_hohl.Visibility = Visibility.Visible;
            lbl_RechteckGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_RechteckGrafik_Hoehe.Visibility = Visibility.Hidden;
            lbl_Rechteck_hohlGrafik_Breite.Visibility = Visibility.Visible;
            lbl_Rechteck_hohlGrafik_Hoehe.Visibility = Visibility.Visible;

            lbl_Rechteck_hohlGrafik_Wall.Visibility = Visibility.Visible;
            tbx_Input_Rechteck_hohl_Wall.Visibility = Visibility.Visible;
            lbl_Input_Rechteck_hohl_Wall.Visibility = Visibility.Visible;

            btn_StartRechteckprofil_Berechnung.Visibility = Visibility.Hidden;
            btn_StartRechteckprofil_hohl_Berechnung.Visibility = Visibility.Visible;
        }

        private void ChB_Rechteckprofil_Vollprofil_Checked(object sender, RoutedEventArgs e)
        {
            ChB_Rechteckprofil_Hohlprofil.IsChecked = false;
            img_Rechteckprofil.Visibility = Visibility.Visible;
            img_Rechteckprofil_hohl.Visibility = Visibility.Hidden;
            lbl_RechteckGrafik_Breite.Visibility = Visibility.Visible;
            lbl_RechteckGrafik_Hoehe.Visibility = Visibility.Visible;
            lbl_Rechteck_hohlGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_Rechteck_hohlGrafik_Hoehe.Visibility = Visibility.Hidden;

            lbl_Rechteck_hohlGrafik_Wall.Visibility = Visibility.Hidden;
            tbx_Input_Rechteck_hohl_Wall.Visibility = Visibility.Hidden;
            lbl_Input_Rechteck_hohl_Wall.Visibility = Visibility.Hidden;

            btn_StartRechteckprofil_Berechnung.Visibility = Visibility.Visible;
            btn_StartRechteckprofil_hohl_Berechnung.Visibility = Visibility.Hidden;
        }

        private void lb_item_Rechteck_Hohlprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChB_Rechteckprofil_Hohlprofil.IsChecked = true;
            tabH_Rechteckprofil.Visibility = Visibility.Visible;
            tab_Rechteckprofil.Visibility = Visibility.Visible;
            img_Rechteckprofil_hohl.Visibility = Visibility.Visible;
            lbl_RechteckGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_RechteckGrafik_Hoehe.Visibility = Visibility.Hidden;
            lbl_Rechteck_hohlGrafik_Breite.Visibility = Visibility.Visible;
            lbl_Rechteck_hohlGrafik_Hoehe.Visibility = Visibility.Visible;

            lbl_Rechteck_hohlGrafik_Wall.Visibility = Visibility.Visible;
            tbx_Input_Rechteck_hohl_Wall.Visibility = Visibility.Visible;
            lbl_Input_Rechteck_hohl_Wall.Visibility = Visibility.Visible;

            btn_StartRechteckprofil_Berechnung.Visibility = Visibility.Hidden;
            btn_StartRechteckprofil_hohl_Berechnung.Visibility = Visibility.Visible;
        }

        private void lb_item_Rechteck_Vollprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChB_Rechteckprofil_Vollprofil.IsChecked = true;
            tabH_Rechteckprofil.Visibility = Visibility.Visible;
            tab_Rechteckprofil.Visibility = Visibility.Visible;
            img_Rechteckprofil.Visibility = Visibility.Visible;
            img_Rechteckprofil_hohl.Visibility = Visibility.Hidden;
            lbl_RechteckGrafik_Breite.Visibility = Visibility.Visible;
            lbl_RechteckGrafik_Hoehe.Visibility = Visibility.Visible;
            lbl_Rechteck_hohlGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_Rechteck_hohlGrafik_Hoehe.Visibility = Visibility.Hidden;

            lbl_Rechteck_hohlGrafik_Wall.Visibility = Visibility.Hidden;
            tbx_Input_Rechteck_hohl_Wall.Visibility = Visibility.Hidden;
            lbl_Input_Rechteck_hohl_Wall.Visibility = Visibility.Hidden;

            btn_StartRechteckprofil_Berechnung.Visibility = Visibility.Visible;
            btn_StartRechteckprofil_hohl_Berechnung.Visibility = Visibility.Hidden;
        }


        #endregion

        #region Grafische Darstellung

        private void tbx_Input_RechteckBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_RechteckGrafik_Breite.Content = tbx_Input_RechteckBreite.Text;
            lbl_Rechteck_hohlGrafik_Breite.Content = tbx_Input_RechteckBreite.Text;
        }

        private void tbx_Input_RechteckHoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_RechteckGrafik_Hoehe.Content = tbx_Input_RechteckHoehe.Text;
            lbl_Rechteck_hohlGrafik_Hoehe.Content = tbx_Input_RechteckHoehe.Text;
        }

        private void tbx_Input_Rechteck_hohl_Wall_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_Rechteck_hohlGrafik_Wall.Content = tbx_Input_Rechteck_hohl_Wall.Text;
        }
        #endregion

        #region Textboxen_Ergebnisse

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

        private void btn_StartKreisprofil_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kreisprofil_Berechnung();   // Startet die Kreisprofil-Berechnung
        }

        private void btn_StartKreisprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kreisprofil_hohl_Berechnung();  // Startet die Kreis-Hohlprofil-Berechnung
        }

        #region Checkboxen_Profilwahl
        private void ChB_Kreisprofil_Hohlprofil_Checked(object sender, RoutedEventArgs e)
        {
            ChB_Kreisprofil_Vollprofil.IsChecked = false;
            img_Kreisprofil_hohl.Visibility = Visibility.Visible;
            img_Kreisprofil.Visibility = Visibility.Hidden;
            lbl_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Visible;
            tbx_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Visible;
            lbl_KreisGrafik_Durchmesser.Visibility = Visibility.Visible;
            lbl_Kreis_hohlGrafik_Wandstaerke.Visibility = Visibility.Visible;

            btn_StartKreisprofil_Berechnung.Visibility = Visibility.Hidden;
            btn_StartKreisprofil_hohl_Berechnung.Visibility = Visibility.Visible;
        }

        private void ChB_Kreisprofil_Vollprofil_Checked(object sender, RoutedEventArgs e)
        {
            ChB_Kreisprofil_Hohlprofil.IsChecked = false;
            img_Kreisprofil_hohl.Visibility = Visibility.Hidden;
            img_Kreisprofil.Visibility = Visibility.Visible;
            lbl_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Hidden;
            tbx_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Hidden;
            lbl_KreisGrafik_Durchmesser.Visibility = Visibility.Visible;
            lbl_Kreis_hohlGrafik_Wandstaerke.Visibility = Visibility.Hidden;

            btn_StartKreisprofil_Berechnung.Visibility = Visibility.Visible;
            btn_StartKreisprofil_hohl_Berechnung.Visibility = Visibility.Hidden;
        }

        private void lb_item_Kreis_Hohlprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChB_Kreisprofil_Hohlprofil.IsChecked = true;
            tabH_Kreisprofil.Visibility = Visibility.Visible;
            tab_Kreisprofil.Visibility = Visibility.Visible;
            img_Kreisprofil_hohl.Visibility = Visibility.Visible;
            img_Kreisprofil.Visibility = Visibility.Hidden;
            lbl_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Visible;
            tbx_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Visible;
            lbl_KreisGrafik_Durchmesser.Visibility = Visibility.Visible;
            lbl_Kreis_hohlGrafik_Wandstaerke.Visibility = Visibility.Visible;

            btn_StartKreisprofil_Berechnung.Visibility = Visibility.Hidden;
            btn_StartKreisprofil_hohl_Berechnung.Visibility = Visibility.Visible;
        }

        private void lb_item_Kreis_Vollprofil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            ChB_Kreisprofil_Vollprofil.IsChecked = true;
            tabH_Kreisprofil.Visibility = Visibility.Visible;
            tab_Kreisprofil.Visibility = Visibility.Visible;
            img_Kreisprofil_hohl.Visibility = Visibility.Hidden;
            img_Kreisprofil.Visibility = Visibility.Visible;
            lbl_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Hidden;
            tbx_Input_Kreis_hohlWandstaerke.Visibility = Visibility.Hidden;
            lbl_KreisGrafik_Durchmesser.Visibility = Visibility.Visible;
            lbl_Kreis_hohlGrafik_Wandstaerke.Visibility = Visibility.Hidden;

            btn_StartKreisprofil_Berechnung.Visibility = Visibility.Visible;
            btn_StartKreisprofil_hohl_Berechnung.Visibility = Visibility.Hidden;
        }
        #endregion

        #region Grafische Darstellung

        private void tbx_Input_KreisDurchmesser_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_KreisGrafik_Durchmesser.Content = tbx_Input_KreisDurchmesser.Text;
        }

        private void tbx_Input_Kreis_hohlWandstaerke_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_Kreis_hohlGrafik_Wandstaerke.Content = tbx_Input_Kreis_hohlWandstaerke.Text;
        }

        #endregion

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

        private void lb_item_Sonder_I_Profil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Visible;
            tab_Sonderprofile.Visibility = Visibility.Visible;
        }

        private void img_CloseButton_Sonder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Hidden;
            tab_Sonderprofile.Visibility = Visibility.Hidden;
        }

        private void btn_StartIPE_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            I_Profil_Berechnung();
        }

        #region Grafische Darstellung

        private void tbx_Input_IPEBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Breite.Content = tbx_Input_IPEBreite.Text;
        }

        private void tbx_Input_IPEHoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Hoehe.Content = tbx_Input_IPEHoehe.Text;
        }

        private void tbx_Input_IPEStegbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Stegbreite.Content = tbx_Input_IPEStegbreite.Text;
        }

        private void tbx_Input_IPEFlanschbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Flanschbreite.Content = tbx_Input_IPEFlanschbreite.Text;
        }





        #endregion

        #endregion

        
    }
}
