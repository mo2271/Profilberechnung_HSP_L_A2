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
using INFITF;
using MECMOD;
using PARTITF;

namespace ProfiRechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : System.Windows.Window
    {
        #region Berechnungsmethoden

        #region BerechnungRechteckvollprofil

        public void Rechteckprofil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_RechteckBreite.Text, out double RechteckBreite);
            Double.TryParse(tbx_Input_RechteckHoehe.Text, out double RechteckHoehe);
            Double.TryParse(tbx_Input_RechteckLaenge.Text, out double RechteckLaenge);
            String RechteckEinheit = Convert.ToString(CoB_Rechteck_Auswahl_Einheit.SelectionBoxItem);
            Double RechteckWSDichte = CoB_Rechteck_WS.Text.Equals("Stahl") ? 7850 : 2700;

            // Klasse Rechteckprofil
            Rechteck R = new Rechteck();
            R.setGeometrie(RechteckBreite, RechteckHoehe, RechteckLaenge);

            // neue Variablen
            double Rechteck_Flaeche, Rechteck_Volumen, Rechteck_FTM_X, Rechteck_FTM_Y, Rechteck_SWP_X, Rechteck_SWP_Y, Rechteck_Masse;

            // Einheitenumrechnung
            if (RechteckEinheit.Equals("mm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 9);
            }
            else if (RechteckEinheit.Equals("cm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 6);
            }

            // Berechnungen
            Rechteck_Flaeche = R.KlasseRechteckBreite * R.KlasseRechteckHoehe;
            Rechteck_Volumen = Rechteck_Flaeche * R.KlasseRechteckLaenge;
            Rechteck_Masse = Rechteck_Volumen * RechteckWSDichte;
            Rechteck_FTM_X = R.KlasseRechteckBreite * Math.Pow(R.KlasseRechteckHoehe, 3) / 12;
            Rechteck_FTM_Y = R.KlasseRechteckHoehe * Math.Pow(R.KlasseRechteckBreite, 3) / 12;
            Rechteck_SWP_X = 0; // Ursprung = Schwerpunkt
            Rechteck_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Runden
            Rechteck_Flaeche = Math.Round(Rechteck_Flaeche, 2);
            Rechteck_Volumen = Math.Round(Rechteck_Volumen, 2);
            Rechteck_Masse = Math.Round(Rechteck_Masse, 3);
            Rechteck_FTM_X = Math.Round(Rechteck_FTM_X, 2);
            Rechteck_FTM_Y = Math.Round(Rechteck_FTM_Y, 2);

            // Umwandlung in String
            string Rechteck_Flaeche_String = Convert.ToString(Rechteck_Flaeche) + " " + RechteckEinheit + "²";
            string Rechteck_Volumen_String = Convert.ToString(Rechteck_Volumen) + " " + RechteckEinheit + "³";
            string Rechteck_Masse_String = Convert.ToString(Rechteck_Masse) + " kg";
            string Rechteck_FTM_X_String = Convert.ToString(Rechteck_FTM_X) + " " + RechteckEinheit + "^4";
            string Rechteck_FTM_Y_String = Convert.ToString(Rechteck_FTM_Y) + " " + RechteckEinheit + "^4";
            string Rechteck_SWP_String = "(x=" + Rechteck_SWP_X + " " + RechteckEinheit + "/y=" + Rechteck_SWP_Y + " " + RechteckEinheit + ")";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Rechteck_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Rechteck_Volumen_String;
            lbl_Rechteck_Masse_Ergebnis.Content = Rechteck_Masse_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Rechteck_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Rechteck_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Rechteck_SWP_String;

            Rechteck CatR = new Rechteck();

            if (CatR.CATIA_Rechteck_Run())
            {
                CatR.PartRechteck();
                CatR.Rechteck_CreateSketch();
                CatR.Rechteck_DrawSketch(RechteckBreite, RechteckHoehe);
                CatR.RechteckExtrusion(RechteckLaenge);
            }
        }

        #endregion

        #region BerechnungRechteckhohlprofil

        public void Rechteckprofil_hohl_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_RechteckBreite.Text, out double RechteckHohlBreite);
            Double.TryParse(tbx_Input_RechteckHoehe.Text, out double RechteckHohlHoehe);
            Double.TryParse(tbx_Input_RechteckLaenge.Text, out double RechteckHohlLaenge);
            Double.TryParse(tbx_Input_Rechteck_hohl_Wall.Text, out double RechteckHohlWandstaerke);
            String RechteckEinheit = Convert.ToString(CoB_Rechteck_Auswahl_Einheit.SelectionBoxItem);
            Double RechteckWSDichte = CoB_Rechteck_WS.Text.Equals("Stahl") ? 7850 : 2700;

            // Klasse Rechteckhohlprofil
            Rechteck_Hohl RH = new Rechteck_Hohl();
            RH.setGeometrie(RechteckHohlBreite, RechteckHohlHoehe, RechteckHohlLaenge, RechteckHohlWandstaerke);

            // neue Variablen
            double Rechteck_Hohl_Flaeche, Rechteck_Hohl_Volumen, Rechteck_Hohl_FTM_X, Rechteck_Hohl_FTM_Y, Rechteck_Hohl_SWP_X, Rechteck_Hohl_SWP_Y, Rechteck_Hohl_Masse;

            // Einheitenumrechnung
            if (RechteckEinheit.Equals("mm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 9);
            }
            else if (RechteckEinheit.Equals("cm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 6);
            }
            // Berechnungen
            Rechteck_Hohl_Flaeche = RH.KlasseRechteckHohlBreite * RH.KlasseRechteckHohlHoehe - (RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke) * (RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke);
            Rechteck_Hohl_Volumen = Rechteck_Hohl_Flaeche * RH.KlasseRechteckHohlLaenge;
            Rechteck_Hohl_Masse = Rechteck_Hohl_Volumen * RechteckWSDichte;
            Rechteck_Hohl_FTM_X = RH.KlasseRechteckHohlBreite * Math.Pow(RH.KlasseRechteckHohlHoehe, 3) / 12 - (RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke) * Math.Pow((RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke), 3) / 12;
            Rechteck_Hohl_FTM_Y = RH.KlasseRechteckHohlHoehe * Math.Pow(RH.KlasseRechteckHohlBreite, 3) / 12 - (RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke) * Math.Pow((RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke), 3) / 12;
            Rechteck_Hohl_SWP_X = 0; // Ursprung = Schwerpunkt
            Rechteck_Hohl_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Runden
            Rechteck_Hohl_Flaeche = Math.Round(Rechteck_Hohl_Flaeche, 2);
            Rechteck_Hohl_Volumen = Math.Round(Rechteck_Hohl_Volumen, 2);
            Rechteck_Hohl_Masse = Math.Round(Rechteck_Hohl_Masse, 3);
            Rechteck_Hohl_FTM_X = Math.Round(Rechteck_Hohl_FTM_X, 2);
            Rechteck_Hohl_FTM_Y = Math.Round(Rechteck_Hohl_FTM_Y, 2);

            // Umwandlung in String
            string Rechteck_Hohl_Flaeche_String = Convert.ToString(Rechteck_Hohl_Flaeche) + " " + RechteckEinheit + "²";
            string Rechteck_Hohl_Volumen_String = Convert.ToString(Rechteck_Hohl_Volumen) + " " + RechteckEinheit + "³";
            string Rechteck_Hohl_Masse_String = Convert.ToString(Rechteck_Hohl_Masse) + " kg";
            string Rechteck_Hohl_FTM_X_String = Convert.ToString(Rechteck_Hohl_FTM_X) + " " + RechteckEinheit + "^4";
            string Rechteck_Hohl_FTM_Y_String = Convert.ToString(Rechteck_Hohl_FTM_Y) + " " + RechteckEinheit + "^4";
            string Rechteck_Hohl_SWP_String = "(x=" + Rechteck_Hohl_SWP_X + " " + RechteckEinheit + "/y=" + Rechteck_Hohl_SWP_Y + " " + RechteckEinheit + ")";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Rechteck_Hohl_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Rechteck_Hohl_Volumen_String;
            lbl_Rechteck_Masse_Ergebnis.Content = Rechteck_Hohl_Masse_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Rechteck_Hohl_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Rechteck_Hohl_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Rechteck_Hohl_SWP_String;

            Rechteck_Hohl CatR_hohl = new Rechteck_Hohl();

            if (CatR_hohl.CATIA_Rechteck_hohl_Run())
            {
                CatR_hohl.PartRechteck_hohl();
                CatR_hohl.Rechteck_hohl_CreateSketch();
                CatR_hohl.Rechteck_hohl_DrawSketch(RechteckHohlBreite, RechteckHohlHoehe, RechteckHohlWandstaerke);
                CatR_hohl.Rechteck_hohl_Extrusion(RechteckHohlLaenge);
            }
        }

        #endregion

        #region BerechnungKreisvollprofil

        public void Kreisprofil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double KreisDurchmesser);
            Double.TryParse(tbx_Input_KreisLaenge.Text, out double KreisLaenge);
            String KreisEinheit = Convert.ToString(CoB_Kreis_Auswahl_Einheit.SelectionBoxItem);
            Double KreisWSDichte = CoB_Kreis_WS.Text.Equals("Stahl") ? 7850 : 2700;

            // Klasse Kreisprofil
            Kreis K = new Kreis();
            K.setGeometrie(KreisDurchmesser, KreisLaenge);

            // neue Variablen
            double Kreis_Flaeche, Kreis_Volumen, Kreis_FTM_X, Kreis_FTM_Y, Kreis_SWP_X, Kreis_SWP_Y, Kreis_Masse;

            // Einheitenumrechnung
            if (KreisEinheit.Equals("mm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 9);
            }
            else if (KreisEinheit.Equals("cm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 6);
            }

            // Berechnungen
            Kreis_Flaeche = Math.Pow(K.KlasseKreisDurchmesser, 2) * Math.PI / 4;
            Kreis_Volumen = Kreis_Flaeche * K.KlasseKreisLaenge;
            Kreis_Masse = Kreis_Volumen * KreisWSDichte;
            Kreis_FTM_X = Math.Pow(K.KlasseKreisDurchmesser, 4) * Math.PI / 64;
            Kreis_FTM_Y = Kreis_FTM_X;
            Kreis_SWP_X = 0; // Ursprung = Schwerpunkt
            Kreis_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Runden
            Kreis_Flaeche = Math.Round(Kreis_Flaeche, 2);
            Kreis_Volumen = Math.Round(Kreis_Volumen, 2);
            Kreis_Masse = Math.Round(Kreis_Masse, 3);
            Kreis_FTM_X = Math.Round(Kreis_FTM_X, 2);
            Kreis_FTM_Y = Math.Round(Kreis_FTM_Y, 2);

            // Umwandlung in String
            string Kreis_Flaeche_String = Convert.ToString(Kreis_Flaeche) + " " + KreisEinheit + "²";
            string Kreis_Volumen_String = Convert.ToString(Kreis_Volumen) + " " + KreisEinheit + "³";
            string Kreis_Masse_String = Convert.ToString(Kreis_Masse) + " kg";
            string Kreis_FTM_X_String = Convert.ToString(Kreis_FTM_X) + " " + KreisEinheit + "^4";
            string Kreis_FTM_Y_String = Convert.ToString(Kreis_FTM_Y) + " " + KreisEinheit + "^4";
            string Kreis_SWP_String = "(x=" + Kreis_SWP_X + " " + KreisEinheit + "/y=" + Kreis_SWP_Y + " " + KreisEinheit + ")";

            // Übergabe Ergebnisse
            lbl_Kreis_Fläche_Ergebnis.Content = Kreis_Flaeche_String;
            lbl_Kreis_Volumen_Ergebnis.Content = Kreis_Volumen_String;
            lbl_Kreis_Masse_Ergebnis.Content = Kreis_Masse_String;
            lbl_Kreis_FTM_X_Ergebnis.Content = Kreis_FTM_X_String;
            lbl_Kreis_FTM_Y_Ergebnis.Content = Kreis_FTM_Y_String;
            lbl_Kreis_Schwerpunkt_Ergebnis.Content = Kreis_SWP_String;

            Kreis CatKr = new Kreis();

            if (CatKr.CATIA_Kreis_Run())
            {
                CatKr.PartKreis();
                CatKr.Kreis_CreateSketch();
                CatKr.Kreis_DrawSketch(KreisDurchmesser);
                CatKr.KreisExtrusion(KreisLaenge);
            }
        }

        #endregion

        #region BerechnungKreishohlprofil

        public void Kreisprofil_hohl_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double KreisHohlDurchmesser);
            Double.TryParse(tbx_Input_KreisLaenge.Text, out double KreisHohlLaenge);
            Double.TryParse(tbx_Input_Kreis_hohlWandstaerke.Text, out double KreisHohlWandstaerke);
            String KreisEinheit = Convert.ToString(CoB_Kreis_Auswahl_Einheit.SelectionBoxItem);
            Double KreisWSDichte = CoB_Kreis_WS.Text.Equals("Stahl") ? 7850 : 2700;

            // Klasse Kreishohlprofil
            Kreis_Hohl KH = new Kreis_Hohl();
            KH.setGeometrie(KreisHohlDurchmesser, KreisHohlLaenge, KreisHohlWandstaerke);

            // neue Variablen
            double Kreis_Hohl_Flaeche, Kreis_Hohl_Volumen, Kreis_Hohl_FTM_X, Kreis_Hohl_FTM_Y, Kreis_Hohl_SWP_X, Kreis_Hohl_SWP_Y, Kreis_Hohl_Masse;

            // Einheitenumrechnung
            if (KreisEinheit.Equals("mm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 9);
            }
            else if (KreisEinheit.Equals("cm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 6);
            }

            // Berechnungen
            Kreis_Hohl_Flaeche = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 2) - Math.Pow((KH.KlasseKreisHohlDurchmesser - 2 * KH.KlasseKreisHohlWandstaerke), 2)) * Math.PI / 4;
            Kreis_Hohl_Volumen = Kreis_Hohl_Flaeche * KH.KlasseKreisHohlLaenge;
            Kreis_Hohl_Masse = Kreis_Hohl_Volumen * KreisWSDichte;
            Kreis_Hohl_FTM_X = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 4) - Math.Pow((KH.KlasseKreisHohlDurchmesser - 2 * KH.KlasseKreisHohlWandstaerke), 4)) * Math.PI / 64;
            Kreis_Hohl_FTM_Y = Kreis_Hohl_FTM_X;
            Kreis_Hohl_SWP_X = 0; // Ursprung = Schwerpunkt
            Kreis_Hohl_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Runden
            Kreis_Hohl_Flaeche = Math.Round(Kreis_Hohl_Flaeche, 2);
            Kreis_Hohl_Volumen = Math.Round(Kreis_Hohl_Volumen, 2);
            Kreis_Hohl_Masse = Math.Round(Kreis_Hohl_Masse, 3);
            Kreis_Hohl_FTM_X = Math.Round(Kreis_Hohl_FTM_X, 2);
            Kreis_Hohl_FTM_Y = Math.Round(Kreis_Hohl_FTM_Y, 2);

            // Umwandlung in String
            string Kreis_Hohl_Flaeche_String = Convert.ToString(Kreis_Hohl_Flaeche) + " " + KreisEinheit + "²";
            string Kreis_Hohl_Volumen_String = Convert.ToString(Kreis_Hohl_Volumen) + " " + KreisEinheit + "³";
            string Kreis_Hohl_Masse_String = Convert.ToString(Kreis_Hohl_Masse) + " kg";
            string Kreis_Hohl_FTM_X_String = Convert.ToString(Kreis_Hohl_FTM_X) + " " + KreisEinheit + "^4";
            string Kreis_Hohl_FTM_Y_String = Convert.ToString(Kreis_Hohl_FTM_Y) + " " + KreisEinheit + "^4";
            string Kreis_Hohl_SWP_String = "(x=" + Kreis_Hohl_SWP_X + " " + KreisEinheit + "/y=" + Kreis_Hohl_SWP_Y + " " + KreisEinheit + ")";

            // Übergabe Ergebnisse
            lbl_Kreis_Fläche_Ergebnis.Content = Kreis_Hohl_Flaeche_String;
            lbl_Kreis_Volumen_Ergebnis.Content = Kreis_Hohl_Volumen_String;
            lbl_Kreis_Masse_Ergebnis.Content = Kreis_Hohl_Masse_String;
            lbl_Kreis_FTM_X_Ergebnis.Content = Kreis_Hohl_FTM_X_String;
            lbl_Kreis_FTM_Y_Ergebnis.Content = Kreis_Hohl_FTM_Y_String;
            lbl_Kreis_Schwerpunkt_Ergebnis.Content = Kreis_Hohl_SWP_String;

            Kreis_Hohl CatKr_hohl = new Kreis_Hohl();

            if (CatKr_hohl.CATIA_Kreis_hohl_Run())
            {
                CatKr_hohl.PartKreis_hohl();
                CatKr_hohl.Kreis_hohl_CreateSketch();
                CatKr_hohl.Kreis_hohl_DrawSketch(KreisHohlDurchmesser, KreisHohlWandstaerke);
                CatKr_hohl.Kreis_hohl_Extrusion(KreisHohlLaenge);
            }
        }

        #endregion

        #region BerechnungSonderIPEProfil

        public void I_Profil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_IPEHoehe.Text, out double SonderIPEHoehe);
            Double.TryParse(tbx_Input_IPEBreite.Text, out double SonderIPEBreite);
            Double.TryParse(tbx_Input_IPEFlanschbreite.Text, out double SonderIPEFlanschbreite);
            Double.TryParse(tbx_Input_IPEStegbreite.Text, out double SonderIPEStegbreite);
            Double.TryParse(tbx_Input_IPELaenge.Text, out double SonderIPELaenge);
            String SonderEinheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);
            Double SonderWSDichte = CoB_Sonder_WS.Text.Equals("Stahl") ? 7850 : 2700;

            // Klasse SonderIPEProfil
            Sonder_IPE SIPE = new Sonder_IPE();
            SIPE.setGeometrie(SonderIPEHoehe, SonderIPEBreite, SonderIPEFlanschbreite, SonderIPEStegbreite, SonderIPELaenge);

            // neue Variablen
            double Sonder_IPE_Flaeche, Sonder_IPE_Volumen, Sonder_IPE_FTM_X, Sonder_IPE_FTM_Y, Sonder_IPE_SWP_X, Sonder_IPE_SWP_Y, Sonder_IPE_Masse;

            // Einheitenumrechnung
            if (SonderEinheit.Equals("mm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 9);
            }
            else if (SonderEinheit.Equals("cm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 6);
            }

            // Hilfsgrößen, da Formeln für FTMX und FTMY sehr lang
            double FTM_X_grossesRechteck, FTM_X_kleinesRechteck, FTM_Y_grossesRechteck, FTM_Y_kleinesRechteckumSWPA, FTM_Y_kleinesRechteckSteiner;

            // Berechnungen
            Sonder_IPE_Flaeche = SIPE.KlasseSonderIPEHoehe * SIPE.KlasseSonderIPEBreite - (SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * (SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite);
            Sonder_IPE_Volumen = Sonder_IPE_Flaeche * SIPE.KlasseSonderIPELaenge;
            Sonder_IPE_Masse = Sonder_IPE_Volumen * SonderWSDichte;
            FTM_X_grossesRechteck = (SIPE.KlasseSonderIPEBreite * Math.Pow(SIPE.KlasseSonderIPEHoehe, 3)) / 12;
            FTM_X_kleinesRechteck = ((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) / 2) * Math.Pow((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite), 3) / 12;
            Sonder_IPE_FTM_X = FTM_X_grossesRechteck - 2 * FTM_X_kleinesRechteck;
            FTM_Y_grossesRechteck = (SIPE.KlasseSonderIPEHoehe * Math.Pow(SIPE.KlasseSonderIPEBreite, 3)) / 12;
            FTM_Y_kleinesRechteckumSWPA = ((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * Math.Pow(((SIPE.KlasseSonderIPEBreite - SonderIPEStegbreite) / 2), 3)) / 12;
            FTM_Y_kleinesRechteckSteiner = (SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * ((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) / 2) * Math.Pow(((SIPE.KlasseSonderIPEBreite + SonderIPEStegbreite) / 4), 2); // nur Flaeche * Abstand²
            Sonder_IPE_FTM_Y = FTM_Y_grossesRechteck - 2 * (FTM_Y_kleinesRechteckumSWPA + FTM_Y_kleinesRechteckSteiner);
            Sonder_IPE_SWP_X = 0; // Ursprung = Schwerpunkt
            Sonder_IPE_SWP_Y = 0; // Ursprung = Schwerpunkt

            // Runden
            Sonder_IPE_Flaeche = Math.Round(Sonder_IPE_Flaeche, 2);
            Sonder_IPE_Volumen = Math.Round(Sonder_IPE_Volumen, 2);
            Sonder_IPE_Masse = Math.Round(Sonder_IPE_Masse, 3);
            Sonder_IPE_FTM_X = Math.Round(Sonder_IPE_FTM_X, 2);
            Sonder_IPE_FTM_Y = Math.Round(Sonder_IPE_FTM_Y, 2);

            // Umwandlung in String
            string Sonder_IPE_Flaeche_String = Convert.ToString(Sonder_IPE_Flaeche) + " " + SonderEinheit + "²";
            string Sonder_IPE_Volumen_String = Convert.ToString(Sonder_IPE_Volumen) + " " + SonderEinheit + "³";
            string Sonder_IPE_Masse_String = Convert.ToString(Sonder_IPE_Masse) + " kg";
            string Sonder_IPE_FTM_X_String = Convert.ToString(Sonder_IPE_FTM_X) + " " + SonderEinheit + "^4";
            string Sonder_IPE_FTM_Y_String = Convert.ToString(Sonder_IPE_FTM_Y) + " " + SonderEinheit + "^4";
            string Sonder_IPE_SWP_String = "(x=" + Sonder_IPE_SWP_X + " " + SonderEinheit + "/y=" + Sonder_IPE_SWP_Y + " " + SonderEinheit + ")";

            // Übergabe Ergebnisse
            lbl_Sonder_Fläche_Ergebnis.Content = Sonder_IPE_Flaeche_String;
            lbl_Sonder_Volumen_Ergebnis.Content = Sonder_IPE_Volumen_String;
            lbl_Sonder_Masse_Ergebnis.Content = Sonder_IPE_Masse_String;
            lbl_Sonder_FTM_X_Ergebnis.Content = Sonder_IPE_FTM_X_String;
            lbl_Sonder_FTM_Y_Ergebnis.Content = Sonder_IPE_FTM_Y_String;
            lbl_Sonder_Schwerpunkt_Ergebnis.Content = Sonder_IPE_SWP_String;
        }

        #endregion

        #endregion

        #region Eingabekontrollen

        #region KontrolleRechteckprofile
        public void Kontrolle_Rechteckprofil()     //Kontrolle der Eingaben 
        {

            string Material = Convert.ToString(CoB_Rechteck_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialauswahl
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                if (result == MessageBoxResult.OK)
                {
                    CoB_Rechteck_WS.IsDropDownOpen = true;
                }

            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Rechteck_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")      //Kontrolle der Einheitenauswahl
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Rechteck_Auswahl_Einheit.IsDropDownOpen = true;
                    }

                }
                if (Einheit != "")
                {

                    if (Double.TryParse(tbx_Input_RechteckBreite.Text, out double b))   //Abfrage ob Konvertierung erfolgreich war
                    {
                        //Konvertierung erfolgreich

                        if (Double.TryParse(tbx_Input_RechteckHoehe.Text, out double h))
                        {
                            //Konvertierung erfolgreich

                            if (Double.TryParse(tbx_Input_RechteckLaenge.Text, out double l))
                            {
                                //Konvertierung erfolgreich

                                string Zeichenlaenge_b;
                                string Zeichenlaenge_h;
                                string Zeichenlaenge_l;

                                Zeichenlaenge_b = Convert.ToString(b);      //Umformung der Eingabe in einen String
                                Zeichenlaenge_h = Convert.ToString(h);
                                Zeichenlaenge_l = Convert.ToString(l);

                                if (Zeichenlaenge_b.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error
                                    );
                                    if (result == MessageBoxResult.OK)
                                    {
                                        tbx_Input_RechteckBreite.Text = "";

                                        tbx_Input_RechteckBreite.Focus();
                                    }
                                }
                                if (Zeichenlaenge_b.Length <= 4)
                                {
                                    if (Zeichenlaenge_h.Length > 4)
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Höhe: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_RechteckHoehe.Text = "";

                                            tbx_Input_RechteckHoehe.Focus();
                                        }
                                    }
                                    if (Zeichenlaenge_h.Length <= 4)
                                    {
                                        if (Zeichenlaenge_l.Length > 4)
                                        {
                                            MessageBoxResult result;
                                            result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error
                                            );
                                            if (result == MessageBoxResult.OK)
                                            {
                                                tbx_Input_RechteckLaenge.Text = "";

                                                tbx_Input_RechteckLaenge.Focus();
                                            }
                                        }
                                        if (Zeichenlaenge_l.Length <= 4)
                                        {
                                            Rechteckprofil_Berechnung(); // Aufruf der Berechnung


                                        }
                                    }
                                }
                            }
                            else
                            {
                                MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );

                                tbx_Input_RechteckLaenge.Clear();
                                tbx_Input_RechteckLaenge.Focus();
                            }
                        }
                        else
                        {
                            MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );

                            tbx_Input_RechteckHoehe.Clear();
                            tbx_Input_RechteckHoehe.Focus();
                        }

                    }
                    else
                    {
                        MessageBox.Show("Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );

                        tbx_Input_RechteckBreite.Clear();
                        tbx_Input_RechteckBreite.Focus();
                    }

                }




                lbl_Rechteck_Fläche_Ergebnis.Visibility = Visibility.Visible;   //Schaltet Sichtbarkeit der Ergebnisse um
                lbl_Rechteck_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
            }

        }
        public void Kontrolle_Rechteckprofil_hohl()     //Kontrolle der Eingaben
        {
            string Material = Convert.ToString(CoB_Rechteck_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialauswahl
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                if (result == MessageBoxResult.OK)
                {
                    CoB_Rechteck_WS.IsDropDownOpen = true;
                }
            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Rechteck_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")      //Kontrolle der Einheitenauswahl
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Rechteck_Auswahl_Einheit.IsDropDownOpen = true;
                    }

                }
                if (Einheit != "")
                {

                    if (Double.TryParse(tbx_Input_RechteckBreite.Text, out double b))     //Kontrolle auf Buchstaben der Breite
                    {
                        //Konvertierung erfolgreich

                        if (Double.TryParse(tbx_Input_RechteckHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                        {
                            //Konvertierung erfolgreich

                            if (Double.TryParse(tbx_Input_RechteckLaenge.Text, out double l))     //Kontrolle auf Buchstaben der Länge
                            {
                                // Konvertierung erfolgreich

                                if (Double.TryParse(tbx_Input_Rechteck_hohl_Wall.Text, out double w))       //Kontrolle auf Buchstaben der Länge
                                {
                                    string Zeichenlaenge_b;
                                    string Zeichenlaenge_h;
                                    string Zeichenlaenge_l;
                                    string Zeichenlaenge_w;



                                    Zeichenlaenge_b = Convert.ToString(b);      //Umformung der Eingabe in einen String
                                    Zeichenlaenge_h = Convert.ToString(h);
                                    Zeichenlaenge_l = Convert.ToString(l);
                                    Zeichenlaenge_w = Convert.ToString(w);



                                    if (w <= (b / 4))                            //Kontrolle der Verhältnismäßigkeit der Wandstärke
                                    {
                                        if (w <= (h / 4))                        //Kontrolle der Verhältnismäßigkeit der Höhe
                                        {
                                            if (Zeichenlaenge_b.Length > 4)     //Kontrolle der Zeichenlänge für die Breite
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_RechteckBreite.Text = "";

                                                    tbx_Input_RechteckBreite.Focus();
                                                }
                                            }          // Kontrolle der einzelnen Eingabefelder
                                            else if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlänge der Höhe
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Hoehe: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabenkontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_RechteckHoehe.Text = "";

                                                    tbx_Input_RechteckHoehe.Focus();
                                                }
                                            }
                                            else if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_RechteckLaenge.Text = "";

                                                    tbx_Input_RechteckLaenge.Focus();
                                                }
                                            }
                                            else if (Zeichenlaenge_w.Length > 2)        //Kontrolle der Zeichenlänge der Wandstärke
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Wandstärke: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 2 Stellen.", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_Rechteck_hohl_Wall.Text = "";

                                                    tbx_Input_Rechteck_hohl_Wall.Focus();
                                                }
                                            }
                                            if (Zeichenlaenge_w.Length < 4)
                                            {
                                                Rechteckprofil_hohl_Berechnung();       //Aufruf der Berechnung
                                            }
                                        }
                                        else
                                        {
                                            MessageBoxResult result;
                                            result = MessageBox.Show("Fehler: Ihre eingetragene Wandstärke ist im Verhältnis zur Höhe zu groß.", "Eingabekontrolle",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error
                                            );
                                            if (result == MessageBoxResult.OK)
                                            {
                                                tbx_Input_Rechteck_hohl_Wall.Text = "";

                                                tbx_Input_Rechteck_hohl_Wall.Focus();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Fehler: Ihre eingetragene Wandstärke ist im Verhältnis zur Breite zu groß.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_Rechteck_hohl_Wall.Text = "";

                                            tbx_Input_Rechteck_hohl_Wall.Focus();
                                        }
                                    }


                                }
                                else
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show("Fehler: Ihre Eingabe der Wandstärke enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error
                                    );
                                    if (result == MessageBoxResult.OK)
                                    {
                                        tbx_Input_Rechteck_hohl_Wall.Text = "";

                                        tbx_Input_Rechteck_hohl_Wall.Focus();
                                    }
                                }

                            }
                            else
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Fehler: Ihre Eingabe der Länge enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_RechteckLaenge.Text = "";

                                    tbx_Input_RechteckLaenge.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Ihre Eingabe der Höhe enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_RechteckHoehe.Text = "";

                                tbx_Input_RechteckHoehe.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Ihre Eingabe der Breite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_RechteckBreite.Text = "";

                            tbx_Input_RechteckBreite.Focus();
                        }
                    }



                }
                


                lbl_Rechteck_Fläche_Ergebnis.Visibility = Visibility.Visible;   //Schaltet Sichtbarkeit der Ergebnisse um
                lbl_Rechteck_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_Y_Ergebnis.Visibility = Visibility.Visible;


            }
        }
        #endregion

        #region KontrolleKreisprofile
        public void Kontrolle_Kreisprofil()
        {
            string Material = Convert.ToString(CoB_Kreis_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialauswahl
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

                if (result == MessageBoxResult.OK)
                {
                    CoB_Kreis_WS.IsDropDownOpen = true;
                }
            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Kreis_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")      //Kontrolle der Einheitenauswahl
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Kreis_Auswahl_Einheit.IsDropDownOpen = true;
                    }

                }
                if (Einheit != "")
                {

                    if (Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double D))       //Kontrolle auf Buchstaben des Durchmessers
                    {
                        if (Double.TryParse(tbx_Input_KreisLaenge.Text, out double l))     //Kontrolle auf Buchstaben der Länge
                        {
                            string Zeichenlaenge_Durchmesser;
                            string Zeichenlaenge_l;

                            Zeichenlaenge_Durchmesser = Convert.ToString(D);
                            Zeichenlaenge_l = Convert.ToString(l);

                            if (Zeichenlaenge_Durchmesser.Length > 4)       //Kontrolle der Zeichenlänge des Durchmessers
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Durchmeser: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_KreisDurchmesser.Text = "";

                                    tbx_Input_KreisDurchmesser.Focus();
                                }
                            }
                            else if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_KreisLaenge.Text = "";

                                    tbx_Input_KreisLaenge.Focus();
                                }

                            }
                            if (Zeichenlaenge_l.Length < 4)
                            {
                                Kreisprofil_Berechnung();   // Startet die Kreisprofil-Berechnung
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Ihre Eingabe der Länge enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_RechteckLaenge.Text = "";

                                tbx_Input_RechteckLaenge.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Ihre Eingabe des Durchmessers enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_KreisDurchmesser.Text = "";

                            tbx_Input_KreisDurchmesser.Focus();
                        }
                    }



                }


                lbl_Kreis_Fläche_Ergebnis.Visibility = Visibility.Visible;  //Schaltet Ergebnis Sichtbarkeit um
                lbl_Kreis_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
            }


        }
        public void Kontrolle_Kreisprofil_hohl()
        {
            string Material = Convert.ToString(CoB_Kreis_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialauswahl
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                if (result == MessageBoxResult.OK)
                {
                    CoB_Kreis_WS.IsDropDownOpen = true;
                }
            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Kreis_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")      //Kontrolle der Einheitenauswahl
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Kreis_Auswahl_Einheit.IsDropDownOpen = true;
                    }
                }
                if (Einheit != "")
                {
                    if (Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double D))     //Kontrolle auf Buchstaben des Durchmessers
                    {
                        if (Double.TryParse(tbx_Input_Kreis_hohlWandstaerke.Text, out double w))     //Kontrolle auf Buchstaben der Wandstärke
                        {
                            if (Double.TryParse(tbx_Input_KreisLaenge.Text, out double l))     //Kontrolle auf Buchstaben der Länge
                            {
                                double Durchmesser;
                                double Wandstaerke;
                                string Zeichenlaenge_D;
                                string Zeichenlaenge_w;
                                string Zeichenlaenge_l;


                                Durchmesser = D;
                                Wandstaerke = w;

                                Zeichenlaenge_D = Convert.ToString(D);
                                Zeichenlaenge_w = Convert.ToString(w);
                                Zeichenlaenge_l = Convert.ToString(l);

                                if ((D / 2) > w)        //Kontrolle auf Verhältnismäßigkeit der Wandstärke
                                {
                                    if (Zeichenlaenge_D.Length > 4)     //Kontrolle der Zeichenlänge des Durchmessers
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Durchmeser: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_KreisDurchmesser.Text = "";

                                            tbx_Input_KreisDurchmesser.Focus();
                                        }
                                    }
                                    else if (Zeichenlaenge_w.Length > 3)        //Kontrolle der Zeichenlänge der Wandstärke
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Wandstärke: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 3 Stellen.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_Kreis_hohlWandstaerke.Text = "";

                                            tbx_Input_Kreis_hohlWandstaerke.Focus();
                                        }
                                    }
                                    else if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_KreisLaenge.Text = "";

                                            tbx_Input_KreisLaenge.Focus();
                                        }
                                    }
                                    if (Zeichenlaenge_l.Length < 4)
                                    {
                                        Kreisprofil_hohl_Berechnung();      //Aufruf der Berechnung
                                    }
                                }
                                else
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show("Fehler: Sie haben die Wandstärke größer als den Durchmesser gewählt.", "Eingabekontrolle",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error
                                    );
                                    if (result == MessageBoxResult.OK)
                                    {
                                        tbx_Input_Kreis_hohlWandstaerke.Text = "";

                                        tbx_Input_Kreis_hohlWandstaerke.Focus();
                                    }
                                }
                            }
                            else
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Fehler: Ihre Eingabe der Länge enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_KreisLaenge.Text = "";

                                    tbx_Input_KreisLaenge.Focus();
                                }
                            }


                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Ihre Eingabe der Wandstärke enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_Kreis_hohlWandstaerke.Text = "";

                                tbx_Input_Kreis_hohlWandstaerke.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Ihre Eingabe des Durchmessers enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_KreisDurchmesser.Text = "";

                            tbx_Input_KreisDurchmesser.Focus();
                        }
                    }




                }


                lbl_Kreis_Fläche_Ergebnis.Visibility = Visibility.Visible;  //Schaltet Ergebnis Sichtbarkeit um
                lbl_Kreis_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
            }
        }
        #endregion

        #region KontrolleSonderprofil
        public void Kontrolle_I_Profil()
        {
            string Material = Convert.ToString(CoB_Sonder_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialauswahl
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                if (result == MessageBoxResult.OK)
                {
                    CoB_Sonder_WS.IsDropDownOpen = true;
                }
            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")      //Kontrolle der Einheitenauswahl     
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Sonder_Auswahl_Einheit.IsDropDownOpen = true;
                    }

                }
                if (Einheit != "")
                {
                    if (Double.TryParse(tbx_Input_IPEBreite.Text, out double B))     //Kontrolle auf Buchstaben der Breite
                    {
                        if (Double.TryParse(tbx_Input_IPEHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                        {
                            if (Double.TryParse(tbx_Input_IPEFlanschbreite.Text, out double b))     //Kontrolle auf Buchstaben der Flanschbreite
                            {
                                if (Double.TryParse(tbx_Input_IPEStegbreite.Text, out double sb))        //Kontrolle auf Buchstaben der Stegbreite
                                {
                                    if (Double.TryParse(tbx_Input_IPELaenge.Text, out double l))     //Kontrolle auf Buchstaben der Länge
                                    {
                                        double Breite;
                                        double Hoehe;
                                        double Flanschbreite;
                                        double Stegbreite;

                                        Breite = B;
                                        Hoehe = h;
                                        Flanschbreite = b;
                                        Stegbreite = sb;

                                        string Zeichenlaenge_B;
                                        string Zeichenlaenge_h;
                                        string Zeichenlaenge_b;
                                        string Zeichenlaenge_sb;
                                        string Zeichenlaenge_l;

                                        Zeichenlaenge_B = Convert.ToString(B);
                                        Zeichenlaenge_h = Convert.ToString(h);
                                        Zeichenlaenge_b = Convert.ToString(b);
                                        Zeichenlaenge_sb = Convert.ToString(sb);
                                        Zeichenlaenge_l = Convert.ToString(l);

                                        if (Stegbreite < (Breite/4))        //Kontrolle der Verhältnismäßigkeit der Stegbreite
                                        {
                                            if (Flanschbreite < (Hoehe / 4))        //Kontrolle der Verhältnismäßigkeit der Flanschbreite
                                            {
                                                if (Zeichenlaenge_B.Length > 4)     //Kontrolle der Zeichenlaenge der Breite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEBreite.Text = "";

                                                        tbx_Input_IPEBreite.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlaenge der Höhe
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Höhe: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEHoehe.Text = "";

                                                        tbx_Input_IPEHoehe.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlaenge der Länge
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPELaenge.Text = "";

                                                        tbx_Input_IPELaenge.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_b.Length > 2)        //Kontrolle der Zeichenlaenge der Flanschbreite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Flanschbreite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 2 Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEFlanschbreite.Text = "";

                                                        tbx_Input_IPEFlanschbreite.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_sb.Length > 2)       //Kontrolle der Zeichenlaenge der Stegbreite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Stegbreite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 2 Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEStegbreite.Text = "";

                                                        tbx_Input_IPEStegbreite.Focus();
                                                    }
                                                }
                                                if (Zeichenlaenge_sb.Length < 2)
                                                {
                                                    I_Profil_Berechnung();      //Aufruf der Berechnung
                                                }
                                            }
                                            else
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Fehler: Sie haben die Flanschbreite im Verhältnis zur Höhe zu groß gewählt.", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_IPEFlanschbreite.Text = "";

                                                    tbx_Input_IPEFlanschbreite.Focus();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBoxResult result;
                                            result = MessageBox.Show("Fehler: Sie haben die Stegbreite größer als die Breite gewählt.", "Eingabekontrolle",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error
                                            );
                                            if (result == MessageBoxResult.OK)
                                            {
                                                tbx_Input_IPEStegbreite.Text = "";

                                                tbx_Input_IPEStegbreite.Focus();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Fehler: Ihre Eingabe der Länge enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_IPELaenge.Text = "";

                                            tbx_Input_IPELaenge.Focus();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show("Fehler: Ihre Eingabe der Stegbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error
                                    );
                                    if (result == MessageBoxResult.OK)
                                    {
                                        tbx_Input_IPEStegbreite.Text = "";

                                        tbx_Input_IPEStegbreite.Focus();
                                    }
                                }
                            }
                            else
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Fehler: Ihre Eingabe der Flanschbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_IPEFlanschbreite.Text = "";

                                    tbx_Input_IPEFlanschbreite.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Ihre Eingabe der Höhe enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_IPEHoehe.Text = "";

                                tbx_Input_IPEHoehe.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Ihre Eingabe der Breite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_IPEBreite.Text = "";

                            tbx_Input_IPEBreite.Focus();
                        }
                    }





                }

                lbl_Sonder_Fläche_Ergebnis.Visibility = Visibility.Visible;     //Schaltet Ergebnis Sichtbarkeit um
                lbl_Sonder_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
            }



        }

        public void Kontrolle_U_Profil()
        {
            string Material = Convert.ToString(CoB_Sonder_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialeingabe
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                if (result == MessageBoxResult.OK)
                {
                    CoB_Sonder_WS.IsDropDownOpen = true;
                }
            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")          //Kontrolle der Einheiteneingabe
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Sonder_Auswahl_Einheit.IsDropDownOpen = true;
                    }

                }
                if (Einheit != "")
                {
                    if (Double.TryParse(tbx_Input_IPEBreite.Text, out double b))        //Kontrolle auf Buchstaben der Breite
                    {
                        //Konvertierung erfolgreich

                        if (Double.TryParse(tbx_Input_IPEHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                        {
                            //Konvertierung erfolgreich

                            if (Double.TryParse(tbx_Input_IPELaenge.Text, out double l))        //Kontrolle auf Buchstaben der Länge
                            {
                                //Konvertierung erfolgreich

                                if (Double.TryParse(tbx_Input_IPEFlanschbreite.Text, out double fb))        //Kontrolle auf Buchstaben der Flanschbreite
                                {
                                    //Konvertierung erfolgreich

                                    if (Double.TryParse(tbx_Input_IPEStegbreite.Text, out double sb))       //Kontrolle auf Buchstaben der Stegbreite
                                    {
                                        string Zeichenlaenge_b;
                                        string Zeichenlaenge_h;
                                        string Zeichenlaenge_l;
                                        string Zeichenlaenge_fb;
                                        string Zeichenlaenge_sb;

                                        Zeichenlaenge_b = Convert.ToString(b);
                                        Zeichenlaenge_h = Convert.ToString(h);
                                        Zeichenlaenge_l = Convert.ToString(l);
                                        Zeichenlaenge_fb = Convert.ToString(fb);
                                        Zeichenlaenge_sb = Convert.ToString(sb);

                                        if (sb < (b / 4))     //Kontrolle auf Verhältnismäßigkeit der Stegbreite
                                        {
                                            if (fb < (h / 4))     //Kontrolle auf Verhältnismäßigkeit der Flanschbreite
                                            {
                                                if (Zeichenlaenge_b.Length > 4)     //Kontrolle der Zeichenlänge der Breite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Breite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEBreite.Text = "";

                                                        tbx_Input_IPEBreite.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlänge der Höhe
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Höhe: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEHoehe.Text = "";

                                                        tbx_Input_IPEHoehe.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Länge: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPELaenge.Text = "";

                                                        tbx_Input_IPELaenge.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_fb.Length > 2)       //Kontrolle der Zeichenlänge der Flanschbreite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Flanschbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEFlanschbreite.Text = "";

                                                        tbx_Input_IPEFlanschbreite.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_sb.Length > 2)       //Kontrolle der Zeichenlänge der Stegbreite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Stegbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEStegbreite.Text = "";

                                                        tbx_Input_IPEStegbreite.Focus();
                                                    }
                                                }
                                                if (Zeichenlaenge_sb.Length < 2)
                                                {
                                                    //Aufruf der Berechnung
                                                }
                                            }
                                            else
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Fehler: Ihre Eingabe der Flanschbreite ist im Verhältnis zur Höhe zu groß.", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_IPEFlanschbreite.Text = "";

                                                    tbx_Input_IPEFlanschbreite.Focus();
                                                }
                                            }

                                        }
                                        else
                                        {
                                            MessageBoxResult result;
                                            result = MessageBox.Show("Fehler: Ihre Eingabe der Breite ist im Verhältnis zur Breite zu groß.", "Eingabekontrolle",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error
                                            );
                                            if (result == MessageBoxResult.OK)
                                            {
                                                tbx_Input_IPEBreite.Text = "";

                                                tbx_Input_IPEBreite.Focus();
                                            }
                                        }

                                    }
                                    else
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Fehler: Ihre Eingabe der Stegbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_IPEStegbreite.Text = "";

                                            tbx_Input_IPEStegbreite.Focus();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show("Fehler: Ihre Eingabe der Flanschbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error
                                    );
                                    if (result == MessageBoxResult.OK)
                                    {
                                        tbx_Input_IPEFlanschbreite.Text = "";

                                        tbx_Input_IPEFlanschbreite.Focus();
                                    }
                                }
                            }
                            else
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Fehler: Ihre Eingabe der Länge enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_IPELaenge.Text = "";

                                    tbx_Input_IPELaenge.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Ihre Eingabe der Höhe enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_IPEHoehe.Text = "";

                                tbx_Input_IPEHoehe.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Ihre Eingabe der Breite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_IPEBreite.Text = "";

                            tbx_Input_IPEBreite.Focus();
                        }
                    }
                }
            }
        }

        public void Kontrolle_T_Profil()
        {
            string Material = Convert.ToString(CoB_Sonder_WS.SelectionBoxItem);

            if (Material == "")     //Kontrolle der Materialeingabe
            {
                MessageBoxResult result;
                result = MessageBox.Show("Werkstoff: Sie haben keinen Werkstoff ausgewählt. Bitte wählen Sie einen Werkstoff aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                if (result == MessageBoxResult.OK)
                {
                    CoB_Sonder_WS.IsDropDownOpen = true;
                }
            }
            if (Material != "")
            {
                String Einheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);

                if (Einheit == "")          //Kontrolle der Einheiteneingabe
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error
                    );
                    if (result == MessageBoxResult.OK)
                    {
                        CoB_Sonder_Auswahl_Einheit.IsDropDownOpen = true;
                    }

                }
                if (Einheit != "")
                {
                    if (Double.TryParse(tbx_Input_IPEBreite.Text, out double b))        //Kontrolle auf Buchstaben der Breite
                    {
                        if (Double.TryParse(tbx_Input_IPEHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                        {
                            if (Double.TryParse(tbx_Input_IPELaenge.Text, out double l))        //Kontrolle auf Buchstaben der Länge
                            {
                                if (Double.TryParse(tbx_Input_IPEStegbreite.Text, out double sb))       //Kontrolle auf Buchstaben der Stegbreite
                                {
                                    if (Double.TryParse(tbx_Input_IPEFlanschbreite.Text, out double fb))     //Kontrolle auf Buchstaben der Flanschbreite
                                    {
                                        if (sb < (b / 4))     //Prüfung auf Verhältnismäßigkeit der Stegbreite
                                        {
                                            if (fb < (h / 4))     //Prüfung auf Verhältnismäßigkeit der Flanschbreite
                                            {
                                                string Zeichenlaenge_b;     //Einführung der Strings
                                                string Zeichenlaenge_h;
                                                string Zeichenlaenge_l;
                                                string Zeichenlaenge_sb;
                                                string Zeichenlaenge_fb;

                                                Zeichenlaenge_b = Convert.ToString(b);      //Konvertierung der Eingaben in strings
                                                Zeichenlaenge_h = Convert.ToString(h);
                                                Zeichenlaenge_l = Convert.ToString(l);
                                                Zeichenlaenge_sb = Convert.ToString(sb);
                                                Zeichenlaenge_fb = Convert.ToString(fb);

                                                if (Zeichenlaenge_b.Length > 4)     //Kontrolle der Zeichenlänge der Breite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Breite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEBreite.Text = "";

                                                        tbx_Input_IPEBreite.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlänge der Höhe
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Höhe: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEHoehe.Text = "";

                                                        tbx_Input_IPEHoehe.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Länge: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPELaenge.Text = "";

                                                        tbx_Input_IPELaenge.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_fb.Length > 2)       //Kontrolle der Zeichenlänge der Flanschbreite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Flanschbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEFlanschbreite.Text = "";

                                                        tbx_Input_IPEFlanschbreite.Focus();
                                                    }
                                                }
                                                else if (Zeichenlaenge_sb.Length > 2)       //Kontrolle der Zeichenlänge der Stegbreite
                                                {
                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Stegbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_IPEStegbreite.Text = "";

                                                        tbx_Input_IPEStegbreite.Focus();
                                                    }
                                                }
                                                if (Zeichenlaenge_sb.Length < 2)
                                                {
                                                    //Aufruf der Berechnung
                                                }
                                            }
                                            else
                                            {
                                                MessageBoxResult result;
                                                result = MessageBox.Show("Fehler: Ihre Eingabe der Flanschbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );
                                                if (result == MessageBoxResult.OK)
                                                {
                                                    tbx_Input_IPEFlanschbreite.Text = "";

                                                    tbx_Input_IPEFlanschbreite.Focus();
                                                }
                                            }
                                        }
                                        else
                                        {
                                            MessageBoxResult result;
                                            result = MessageBox.Show("Fehler: Ihre Eingabe der Stegbreite ist im Verhältnis zur Breite zu groß.", "Eingabekontrolle",
                                            MessageBoxButton.OK,
                                            MessageBoxImage.Error
                                            );
                                            if (result == MessageBoxResult.OK)
                                            {
                                                tbx_Input_IPEStegbreite.Text = "";

                                                tbx_Input_IPEStegbreite.Focus();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        MessageBoxResult result;
                                        result = MessageBox.Show("Fehler: Ihre Eingabe der Flanschbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                        MessageBoxButton.OK,
                                        MessageBoxImage.Error
                                        );
                                        if (result == MessageBoxResult.OK)
                                        {
                                            tbx_Input_IPEFlanschbreite.Text = "";

                                            tbx_Input_IPEFlanschbreite.Focus();
                                        }
                                    }
                                }
                                else
                                {
                                    MessageBoxResult result;
                                    result = MessageBox.Show("Fehler: Ihre Eingabe der Stegbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                    MessageBoxButton.OK,
                                    MessageBoxImage.Error
                                    );
                                    if (result == MessageBoxResult.OK)
                                    {
                                        tbx_Input_IPEStegbreite.Text = "";

                                        tbx_Input_IPEStegbreite.Focus();
                                    }
                                }
                            }
                            else
                            {
                                MessageBoxResult result;
                                result = MessageBox.Show("Fehler: Ihre Eingabe der Länge enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                                MessageBoxButton.OK,
                                MessageBoxImage.Error
                                );
                                if (result == MessageBoxResult.OK)
                                {
                                    tbx_Input_IPELaenge.Text = "";

                                    tbx_Input_IPELaenge.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Ihre Eingabe der Höhe enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_IPEHoehe.Text = "";

                                tbx_Input_IPEHoehe.Focus();
                            }
                        }
                    }
                    else
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Ihre Eingabe der Stegbreite enthält einen oder mehrere Buchstaben.Bitte geben Sie nur Zahlen ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_IPEBreite.Text = "";

                            tbx_Input_IPEBreite.Focus();
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        public MainWindow()
        {
            //InitializeComponent();

            CATIAConnection cc = new CATIAConnection();

            if (cc.CATIA_Run())
            {
                MessageBoxResult result;
                result = MessageBox.Show("CATIA wird ausgeführt.", "Hinweis",
                MessageBoxButton.OK,
                MessageBoxImage.Information
                );

                InitializeComponent();
            }
            else
            {
                MessageBoxResult result;
                result = MessageBox.Show("CATIA wird nicht ausgeführt!", "Fehler",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

                Environment.Exit(0);
            }
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
            Kontrolle_Rechteckprofil();


        }

        private void btn_StartRechteckprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kontrolle_Rechteckprofil_hohl();

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
            Kontrolle_Kreisprofil();    //Kontrolle der Eingaben

        }

        private void btn_StartKreisprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kontrolle_Kreisprofil_hohl();   // Kontrolle der Eingabe

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

            img_IPE_Profil.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Hoehe.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Stegbreite.Visibility = Visibility.Hidden;



            TabItem tab_I_Profil = (TabItem)tabctrl_Profilauswahl.Items[2];     // Index Sonderprofiltab: 2
            tab_I_Profil.Focus();       // Fokussiere Sonderprofiltab
        }

        private void lb_item_Sonder_I_Profil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Visible;
            tab_Sonderprofile.Visibility = Visibility.Visible;
            btn_StartIPE_Berechnung.Visibility = Visibility.Visible;

            ChB_Sonder_I_Profil.IsChecked = true;

            TabItem tab_I_Profil = (TabItem)tabctrl_Profilauswahl.Items[2];
            tab_I_Profil.Focus();
        }

        private void lb_item_Sonder_U_Profil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Visible;
            tab_Sonderprofile.Visibility = Visibility.Visible;
            btn_StartU_Berechnung.Visibility = Visibility.Visible;

            ChB_Sonder_U_Profil.IsChecked = true;

            TabItem tab_I_Profil = (TabItem)tabctrl_Profilauswahl.Items[2];
            tab_I_Profil.Focus();
        }

        private void lb_item_Sonder_T_Profil_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            tabH_Sonderprofile.Visibility = Visibility.Visible;
            tab_Sonderprofile.Visibility = Visibility.Visible;
            btn_StartT_Berechnung.Visibility = Visibility.Visible;

            ChB_Sonder_T_Profil.IsChecked = true;

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
            Kontrolle_I_Profil();

        }

        #region Checkboxen Profilwahl
        private void ChB_Sonder_I_Profil_Checked(object sender, RoutedEventArgs e)
        {
            btn_StartIPE_Berechnung.Visibility = Visibility.Visible;

            ChB_Sonder_T_Profil.IsChecked = false;
            ChB_Sonder_U_Profil.IsChecked = false;

            btn_StartT_Berechnung.Visibility = Visibility.Hidden;
            btn_StartU_Berechnung.Visibility = Visibility.Hidden;

            img_IPE_Profil.Visibility = Visibility.Visible;
            lbl_IPEGrafik_Breite.Visibility = Visibility.Visible;
            lbl_IPEGrafik_Flanschbreite.Visibility = Visibility.Visible;
            lbl_IPEGrafik_Hoehe.Visibility = Visibility.Visible;
            lbl_IPEGrafik_Stegbreite.Visibility = Visibility.Visible;

            lbl_U_Breite.Visibility = Visibility.Hidden;
            lbl_U_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_U_Hoehe.Visibility = Visibility.Hidden;
            lbl_U_Stegbreite.Visibility = Visibility.Hidden;
            img_U_Profil.Visibility = Visibility.Hidden;
            lbl_U_CMaß.Visibility = Visibility.Hidden;

            lbl_T_Breite.Visibility = Visibility.Hidden;
            lbl_T_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_T_Hoehe.Visibility = Visibility.Hidden;
            lbl_T_Stegbreite.Visibility = Visibility.Hidden;
            img_T_Profil.Visibility = Visibility.Hidden;
        }

        private void ChB_Sonder_U_Profil_Checked(object sender, RoutedEventArgs e)
        {
            btn_StartU_Berechnung.Visibility = Visibility.Visible;

            ChB_Sonder_T_Profil.IsChecked = false;
            ChB_Sonder_I_Profil.IsChecked = false;

            btn_StartIPE_Berechnung.Visibility = Visibility.Hidden;
            btn_StartT_Berechnung.Visibility = Visibility.Hidden;

            img_IPE_Profil.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Hoehe.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Stegbreite.Visibility = Visibility.Hidden;

            lbl_U_Breite.Visibility = Visibility.Visible;
            lbl_U_Flanschbreite.Visibility = Visibility.Visible;
            lbl_U_Hoehe.Visibility = Visibility.Visible;
            lbl_U_Stegbreite.Visibility = Visibility.Visible;
            img_U_Profil.Visibility = Visibility.Visible;
            lbl_U_CMaß.Visibility = Visibility.Visible;

            lbl_T_Breite.Visibility = Visibility.Hidden;
            lbl_T_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_T_Hoehe.Visibility = Visibility.Hidden;
            lbl_T_Stegbreite.Visibility = Visibility.Hidden;
            img_T_Profil.Visibility = Visibility.Hidden;

        }

        private void ChB_Sonder_T_Profil_Checked(object sender, RoutedEventArgs e)
        {
            btn_StartT_Berechnung.Visibility = Visibility.Visible;

            ChB_Sonder_I_Profil.IsChecked = false;
            ChB_Sonder_U_Profil.IsChecked = false;

            btn_StartU_Berechnung.Visibility = Visibility.Hidden;
            btn_StartIPE_Berechnung.Visibility = Visibility.Hidden;

            img_IPE_Profil.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Breite.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Hoehe.Visibility = Visibility.Hidden;
            lbl_IPEGrafik_Stegbreite.Visibility = Visibility.Hidden;

            lbl_U_Breite.Visibility = Visibility.Hidden;
            lbl_U_Flanschbreite.Visibility = Visibility.Hidden;
            lbl_U_Hoehe.Visibility = Visibility.Hidden;
            lbl_U_Stegbreite.Visibility = Visibility.Hidden;
            img_U_Profil.Visibility = Visibility.Hidden;
            lbl_U_CMaß.Visibility = Visibility.Hidden;


            lbl_T_Breite.Visibility = Visibility.Visible;
            lbl_T_Flanschbreite.Visibility = Visibility.Visible;
            lbl_T_Hoehe.Visibility = Visibility.Visible;
            lbl_T_Stegbreite.Visibility = Visibility.Visible;
            img_T_Profil.Visibility = Visibility.Visible;
        }



        #endregion 

        #region Grafische Darstellung

        private void tbx_Input_IPEBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Breite.Content = tbx_Input_IPEBreite.Text;
            lbl_U_Breite.Content = tbx_Input_IPEBreite.Text;
            lbl_T_Breite.Content = tbx_Input_IPEBreite.Text;

        }

        private void tbx_Input_IPEHoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Hoehe.Content = tbx_Input_IPEHoehe.Text;
            lbl_U_Hoehe.Content = tbx_Input_IPEHoehe.Text;
            lbl_T_Hoehe.Content = tbx_Input_IPEHoehe.Text;
        }

        private void tbx_Input_IPEStegbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Stegbreite.Content = tbx_Input_IPEStegbreite.Text;
            lbl_U_Stegbreite.Content = tbx_Input_IPEStegbreite.Text;
            lbl_T_Stegbreite.Content = tbx_Input_IPEStegbreite.Text;
        }         

        private void tbx_Input_IPEFlanschbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Flanschbreite.Content = tbx_Input_IPEFlanschbreite.Text;
            lbl_U_Flanschbreite.Content = tbx_Input_IPEFlanschbreite.Text;
            lbl_T_Flanschbreite.Content = tbx_Input_IPEFlanschbreite.Text;
        }









        #endregion

        #endregion


    }
}
