using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
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
        public void Rechteckprofil_Berechnung()     
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_RechteckBreite.Text, out double RechteckBreite);  
            Double.TryParse(tbx_Input_RechteckHoehe.Text, out double RechteckHoehe);
            Double.TryParse(tbx_Input_RechteckLaenge.Text, out double RechteckLaenge);
           
            // Klasse Rechteckprofil
            Rechteck R = new Rechteck();
            R.setGeometrie(RechteckBreite, RechteckHoehe, RechteckLaenge);

            // neue Variablen
            double Rechteck_Flaeche, Rechteck_Volumen, Rechteck_FTM_X, Rechteck_FTM_Y, Rechteck_SWP_X, Rechteck_SWP_Y, Rechteck_Masse;

            // Berechnungen
            Rechteck_Flaeche = R.KlasseRechteckBreite * R.KlasseRechteckHoehe;
            Rechteck_Volumen = Rechteck_Flaeche * R.KlasseRechteckLaenge;
            Rechteck_FTM_X = R.KlasseRechteckBreite * Math.Pow(R.KlasseRechteckHoehe, 3) / 12;
            Rechteck_FTM_Y = R.KlasseRechteckHoehe * Math.Pow(R.KlasseRechteckBreite, 3) / 12;
            Rechteck_SWP_X = 0; // Ursprung = Schwerpunkt
            Rechteck_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Umwandlung in String
            string Rechteck_Flaeche_String = Convert.ToString(Rechteck_Flaeche);
            string Rechteck_Volumen_String = Convert.ToString(Rechteck_Volumen);
            string Rechteck_FTM_X_String = Convert.ToString(Rechteck_FTM_X);
            string Rechteck_FTM_Y_String = Convert.ToString(Rechteck_FTM_Y);
            string Rechteck_SWP_String = "(x=" + Rechteck_SWP_X + "/y=" + Rechteck_SWP_Y + ")";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Rechteck_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Rechteck_Volumen_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Rechteck_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Rechteck_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Rechteck_SWP_String;
        }

        public void Rechteckprofil_hohl_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_RechteckBreite.Text, out double RechteckHohlBreite);
            Double.TryParse(tbx_Input_RechteckHoehe.Text, out double RechteckHohlHoehe);
            Double.TryParse(tbx_Input_RechteckLaenge.Text, out double RechteckHohlLaenge);
            Double.TryParse(tbx_Input_Rechteck_hohl_Wall.Text, out double RechteckHohlWandstaerke);

            // Klasse Rechteckhohlprofil
            Rechteck_Hohl RH = new Rechteck_Hohl();
            RH.setGeometrie(RechteckHohlBreite, RechteckHohlHoehe, RechteckHohlLaenge, RechteckHohlWandstaerke);

            // neue Variablen
            double Rechteck_Hohl_Flaeche, Rechteck_Hohl_Volumen, Rechteck_Hohl_FTM_X, Rechteck_Hohl_FTM_Y, Rechteck_Hohl_SWP_X, Rechteck_Hohl_SWP_Y, Rechteck_Hohl_Masse;

            // Berechnungen
            Rechteck_Hohl_Flaeche = RH.KlasseRechteckHohlBreite * RH.KlasseRechteckHohlHoehe - (RH.KlasseRechteckHohlBreite - 2* RH.KlasseRechteckHohlWandstaerke) *(RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke);
            Rechteck_Hohl_Volumen = Rechteck_Hohl_Flaeche * RH.KlasseRechteckHohlLaenge;
            Rechteck_Hohl_FTM_X = RH.KlasseRechteckHohlBreite * Math.Pow(RH.KlasseRechteckHohlHoehe, 3) / 12 - (RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke) * Math.Pow((RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke), 3) / 12;
            Rechteck_Hohl_FTM_Y = RH.KlasseRechteckHohlHoehe * Math.Pow(RH.KlasseRechteckHohlBreite, 3) / 12 - (RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke) * Math.Pow((RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke), 3) / 12;
            Rechteck_Hohl_SWP_X = 0; // Ursprung = Schwerpunkt
            Rechteck_Hohl_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Umwandlung in String
            string Rechteck_Hohl_Flaeche_String = Convert.ToString(Rechteck_Hohl_Flaeche);
            string Rechteck_Hohl_Volumen_String = Convert.ToString(Rechteck_Hohl_Volumen);
            string Rechteck_Hohl_FTM_X_String = Convert.ToString(Rechteck_Hohl_FTM_X);
            string Rechteck_Hohl_FTM_Y_String = Convert.ToString(Rechteck_Hohl_FTM_Y);
            string Rechteck_Hohl_SWP_String = "(x=" + Rechteck_Hohl_SWP_X + "/y=" + Rechteck_Hohl_SWP_Y + ")";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Rechteck_Hohl_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Rechteck_Hohl_Volumen_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Rechteck_Hohl_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Rechteck_Hohl_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Rechteck_Hohl_SWP_String;
        }

        public void Kreisprofil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double KreisDurchmesser);
            // Double.TryParse(tbx_Input_KreisLaenge.Text, out double KreisLaenge);

            double KreisLaenge = 10;

            // Klasse Kreisprofil
            Kreis K = new Kreis();
            K.setGeometrie(KreisDurchmesser,KreisLaenge);

            // neue Variablen
            double Kreis_Flaeche, Kreis_Volumen, Kreis_FTM_X, Kreis_FTM_Y, Kreis_SWP_X, Kreis_SWP_Y, Kreis_Masse;

            // Berechnungen
            Kreis_Flaeche = Math.Pow(K.KlasseKreisDurchmesser, 2) * Math.PI / 4;
            Kreis_Volumen = Kreis_Flaeche * K.KlasseKreisLaenge;
            Kreis_FTM_X = Math.Pow(K.KlasseKreisDurchmesser, 4) * Math.PI / 64;
            Kreis_FTM_Y = Kreis_FTM_X;
            Kreis_SWP_X = 0; // Ursprung = Schwerpunkt
            Kreis_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Umwandlung in String
            string Kreis_Flaeche_String = Convert.ToString(Kreis_Flaeche);
            string Kreis_Volumen_String = Convert.ToString(Kreis_Volumen);
            string Kreis_FTM_X_String = Convert.ToString(Kreis_FTM_X);
            string Kreis_FTM_Y_String = Convert.ToString(Kreis_FTM_Y);
            string Kreis_SWP_String = "(x=" + Kreis_SWP_X + "/y=" + Kreis_SWP_Y + ")";

            // Übergabe Ergebnisse
            lbl_Kreis_Fläche_Ergebnis.Content = Kreis_Flaeche_String;
            lbl_Kreis_Volumen_Ergebnis.Content = Kreis_Volumen_String;
            lbl_Kreis_FTM_X_Ergebnis.Content = Kreis_FTM_X_String;
            lbl_Kreis_FTM_Y_Ergebnis.Content = Kreis_FTM_Y_String;
            lbl_Kreis_Schwerpunkt_Ergebnis.Content = Kreis_SWP_String;
        }

        public void Kreisprofil_hohl_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double KreisHohlDurchmesser);
            // Double.TryParse(tbx_Input_Kreis_HohlLaenge.Text, out double KreisHohlLaenge);
            Double.TryParse(tbx_Input_Kreis_hohlWandstaerke.Text, out double KreisHohlWandstaerke);

            double KreisHohlLaenge = 10;

            // Klasse Kreishohlprofil
            Kreis_Hohl KH = new Kreis_Hohl();
            KH.setGeometrie(KreisHohlDurchmesser, KreisHohlLaenge, KreisHohlWandstaerke);

            // neue Variablen
            double Kreis_Hohl_Flaeche, Kreis_Hohl_Volumen, Kreis_Hohl_FTM_X, Kreis_Hohl_FTM_Y, Kreis_Hohl_SWP_X, Kreis_Hohl_SWP_Y, Kreis_Hohl_Masse;

            // Berechnungen
            Kreis_Hohl_Flaeche = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 2)-Math.Pow((KH.KlasseKreisHohlDurchmesser-KH.KlasseKreisHohlWandstaerke), 2)) * Math.PI / 4;
            Kreis_Hohl_Volumen = Kreis_Hohl_Flaeche * KH.KlasseKreisHohlLaenge;
            Kreis_Hohl_FTM_X = Math.Pow(KH.KlasseKreisHohlDurchmesser, 4) * Math.PI / 64;
            Kreis_Hohl_FTM_Y = Kreis_Hohl_FTM_X;
            Kreis_Hohl_SWP_X = 0; // Ursprung = Schwerpunkt
            Kreis_Hohl_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Umwandlung in String
            string Kreis_Hohl_Flaeche_String = Convert.ToString(Kreis_Hohl_Flaeche);
            string Kreis_Hohl_Volumen_String = Convert.ToString(Kreis_Hohl_Volumen);
            string Kreis_Hohl_FTM_X_String = Convert.ToString(Kreis_Hohl_FTM_X);
            string Kreis_Hohl_FTM_Y_String = Convert.ToString(Kreis_Hohl_FTM_Y);
            string Kreis_Hohl_SWP_String = "(x=" + Kreis_Hohl_SWP_X + "/y=" + Kreis_Hohl_SWP_Y + ")";

            // Übergabe Ergebnisse
            lbl_Kreis_Fläche_Ergebnis.Content = Kreis_Hohl_Flaeche_String;
            lbl_Kreis_Volumen_Ergebnis.Content = Kreis_Hohl_Volumen_String;
            lbl_Kreis_FTM_X_Ergebnis.Content = Kreis_Hohl_FTM_X_String;
            lbl_Kreis_FTM_Y_Ergebnis.Content = Kreis_Hohl_FTM_Y_String;
            lbl_Kreis_Schwerpunkt_Ergebnis.Content = Kreis_Hohl_SWP_String;
        }

        public void I_Profil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_IPEHoehe.Text, out double SonderIPEHoehe);
            Double.TryParse(tbx_Input_IPEBreite.Text, out double SonderIPEBreite);
            Double.TryParse(tbx_Input_IPEFlanschbreite.Text, out double SonderIPEFlanschbreite);
            Double.TryParse(tbx_Input_IPEStegbreite.Text, out double SonderIPEStegbreite);
            // Double.TryParse(tbx_Input_IPELaenge.Text, out double SonderIPELaenge);

            double SonderIPELaenge = 10;

            // Klasse SonderIPEProfil
            Sonder_IPE SIPE = new Sonder_IPE();
            SIPE.setGeometrie(SonderIPEHoehe, SonderIPEBreite, SonderIPEFlanschbreite, SonderIPEStegbreite, SonderIPELaenge);

            // neue Variablen
            double Sonder_IPE_Flaeche, Sonder_IPE_Volumen, Sonder_IPE_FTM_X, Sonder_IPE_FTM_Y, Sonder_IPE_SWP_X, Sonder_IPE_SWP_Y, Sonder_IPE_Masse;

            // Berechnungen
            Sonder_IPE_Flaeche = SIPE.KlasseSonderIPEBreite * SIPE.KlasseSonderIPEHoehe - ((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite)*(SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite));
            Sonder_IPE_Volumen = Sonder_IPE_Flaeche * SIPE.KlasseSonderIPELaenge;
            Sonder_IPE_FTM_X = SIPE.KlasseSonderIPEBreite * Math.Pow(SIPE.KlasseSonderIPEHoehe, 3) / 12 - (SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) * Math.Pow((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite), 3) / 12;
            Sonder_IPE_FTM_Y = SIPE.KlasseSonderIPEHoehe * Math.Pow(SIPE.KlasseSonderIPEBreite, 3) / 12 - 2 * ((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * Math.Pow((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite), 3) / 12 + Math.Pow((SIPE.KlasseSonderIPEStegbreite + SIPE.KlasseSonderIPEBreite / 4), 2) + (SIPE.KlasseSonderIPEHoehe - SIPE.KlasseSonderIPEFlanschbreite) * ((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) / 2));
            Sonder_IPE_SWP_X = 0; // Ursprung = Schwerpunkt
            Sonder_IPE_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Umwandlung in String
            string Sonder_IPE_Flaeche_String = Convert.ToString(Sonder_IPE_Flaeche);
            string Sonder_IPE_Volumen_String = Convert.ToString(Sonder_IPE_Volumen);
            string Sonder_IPE_FTM_X_String = Convert.ToString(Sonder_IPE_FTM_X);
            string Sonder_IPE_FTM_Y_String = Convert.ToString(Sonder_IPE_FTM_Y);
            string Sonder_IPE_SWP_String = "(x=" + Sonder_IPE_SWP_X + "/y=" + Sonder_IPE_SWP_Y + ")";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Sonder_IPE_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Sonder_IPE_Volumen_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Sonder_IPE_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Sonder_IPE_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Sonder_IPE_SWP_String;
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

            TabItem tab_Rechteck = (TabItem)tabctrl_Profilauswahl.Items[0];     // Index Rechtecktab: 0
            tab_Rechteck.Focus();       // Fokussiere Rechtecktab
        }

        private void img_CloseButton_Rechteck_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Rechteckprofil.Visibility = Visibility.Hidden;
            tab_Rechteckprofil.Visibility = Visibility.Hidden;

            // Fokusführung beim Schließen des Tabs: Fokussiere bevorzugt auf den Tab links vom aktuellen Tab, sonst einen anderen offenen Tab.
            if (tab_Kreisprofil.Visibility != Visibility.Hidden)
            {
                TabItem tabSwitch1_Rechteck = (TabItem)tabctrl_Profilauswahl.Items[1];
                tabSwitch1_Rechteck.Focus();        // Fokussiere Kreisprofiltab
            }
            else
            {
                TabItem tabSwitch2_Rechteck = (TabItem)tabctrl_Profilauswahl.Items[2];
                tabSwitch2_Rechteck.Focus();        // Alternativ: Fokussiere Sonderprofiltab
            }
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

            TabItem tab_Rechteck = (TabItem)tabctrl_Profilauswahl.Items[0];
            tab_Rechteck.Focus();
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

            TabItem tab_Rechteck = (TabItem)tabctrl_Profilauswahl.Items[0];
            tab_Rechteck.Focus();
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

            TabItem tab_Kreis = (TabItem)tabctrl_Profilauswahl.Items[1];    // Index Kreistab: 1
            tab_Kreis.Focus();      // Fokussiere Kreistab
        }

        private void img_CloseButton_Kreis_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Kreisprofil.Visibility = Visibility.Hidden;
            tab_Kreisprofil.Visibility = Visibility.Hidden;

            // Fokusführung beim Schließen des Tabs: Fokussiere bevorzugt auf den Tab links vom aktuellen Tab, sonst einen anderen offenen Tab.
            if (tab_Rechteckprofil.Visibility != Visibility.Hidden)
            {
                TabItem tabSwitch1_Kreis = (TabItem)tabctrl_Profilauswahl.Items[0];
                tabSwitch1_Kreis.Focus();       // Fokussiere Rechteckprofiltab
            }
            else
            {
                TabItem tabSwitch2_Kreis = (TabItem)tabctrl_Profilauswahl.Items[2];
                tabSwitch2_Kreis.Focus();       // Alternativ: Fokussiere Sonderprofiltab
            }
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

            TabItem tab_Kreis = (TabItem)tabctrl_Profilauswahl.Items[1];
            tab_Kreis.Focus();
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

            TabItem tab_Kreis = (TabItem)tabctrl_Profilauswahl.Items[1];
            tab_Kreis.Focus();
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

            TabItem tab_I_Profil = (TabItem)tabctrl_Profilauswahl.Items[2];     // Index Sonderprofiltab: 2
            tab_I_Profil.Focus();       // Fokussiere Sonderprofiltab
        }

        private void lb_item_Sonder_I_Profil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Visible;
            tab_Sonderprofile.Visibility = Visibility.Visible;

            TabItem tab_I_Profil = (TabItem)tabctrl_Profilauswahl.Items[2];
            tab_I_Profil.Focus();    
        }

        private void img_CloseButton_Sonder_MouseDown(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Hidden;
            tab_Sonderprofile.Visibility = Visibility.Hidden;

            // Fokusführung beim Schließen des Tabs: Fokussiere bevorzugt auf den Tab links vom aktuellen Tab, sonst einen anderen offenen Tab.
            if (tab_Kreisprofil.Visibility != Visibility.Hidden)
            {
                TabItem tabSwitch1_Sonderprofil = (TabItem)tabctrl_Profilauswahl.Items[1];
                tabSwitch1_Sonderprofil.Focus();    // Fokussiere Kreisprofil-Tab
            }
            else
            {
                TabItem tabSwitch2_Sonderprofil = (TabItem)tabctrl_Profilauswahl.Items[0];
                tabSwitch2_Sonderprofil.Focus();    // Alternativ: Fokussiere Rechteckprofil-Tab
            }
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
