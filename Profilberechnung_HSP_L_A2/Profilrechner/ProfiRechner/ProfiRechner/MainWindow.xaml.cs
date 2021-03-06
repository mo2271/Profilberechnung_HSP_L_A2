﻿using System;
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
            Double.TryParse(tbx_Input_RechteckPreisProMeter.Text, out double RechteckPreisProMeter);

            // Klasse Rechteckprofil
            Rechteck R = new Rechteck();
            R.setGeometrie(RechteckBreite, RechteckHoehe, RechteckLaenge);

            // neue Variablen
            double Rechteck_Flaeche, Rechteck_Volumen, Rechteck_FTM_X, Rechteck_FTM_Y, Rechteck_SWP_X, Rechteck_SWP_Y, Rechteck_Masse, Rechteck_Preis;

            // Einheitenumrechnung
            if (RechteckEinheit.Equals("mm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 9);
                RechteckPreisProMeter = RechteckPreisProMeter / Math.Pow(10, 3);
            }
            else if (RechteckEinheit.Equals("cm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 6);
                RechteckPreisProMeter = RechteckPreisProMeter / Math.Pow(10, 2);
            }

            // Berechnungen
            Rechteck_Flaeche = R.KlasseRechteckBreite * R.KlasseRechteckHoehe;
            Rechteck_Volumen = Rechteck_Flaeche * R.KlasseRechteckLaenge;
            Rechteck_Masse = Rechteck_Volumen * RechteckWSDichte;
            Rechteck_FTM_X = R.KlasseRechteckBreite * Math.Pow(R.KlasseRechteckHoehe, 3) / 12;
            Rechteck_FTM_Y = R.KlasseRechteckHoehe * Math.Pow(R.KlasseRechteckBreite, 3) / 12;
            Rechteck_SWP_X = R.KlasseRechteckBreite / 2;
            Rechteck_SWP_Y = R.KlasseRechteckHoehe / 2;
            Rechteck_Preis = RechteckPreisProMeter * R.KlasseRechteckLaenge;

            // Runden      
            Rechteck_Flaeche = Math.Round(Rechteck_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Flaeche))));
            Rechteck_Volumen = Math.Round(Rechteck_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Volumen))));
            Rechteck_Masse = Math.Round(Rechteck_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Masse))));
            Rechteck_FTM_X = Math.Round(Rechteck_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_FTM_X))));
            Rechteck_FTM_Y = Math.Round(Rechteck_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_FTM_Y))));
            Rechteck_Preis = Math.Round(Rechteck_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Preis, 2))))), 2) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Preis))));


            // Umwandlung in String
            string Rechteck_Flaeche_String = Convert.ToString(Rechteck_Flaeche) + " " + RechteckEinheit + "²";
            string Rechteck_Volumen_String = Convert.ToString(Rechteck_Volumen) + " " + RechteckEinheit + "³";
            string Rechteck_Masse_String = Convert.ToString(Rechteck_Masse) + " kg";
            string Rechteck_FTM_X_String = Convert.ToString(Rechteck_FTM_X) + " " + RechteckEinheit + "^4";
            string Rechteck_FTM_Y_String = Convert.ToString(Rechteck_FTM_Y) + " " + RechteckEinheit + "^4";
            string Rechteck_SWP_String = "(x=" + Rechteck_SWP_X + " " + RechteckEinheit + "/y=" + Rechteck_SWP_Y + " " + RechteckEinheit + ")";
            string Rechteck_Preis_String = Convert.ToString(Rechteck_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Rechteck_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Rechteck_Volumen_String;
            lbl_Rechteck_Masse_Ergebnis.Content = Rechteck_Masse_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Rechteck_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Rechteck_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Rechteck_SWP_String;
            lbl_Rechteck_Preis_Ergebnis.Content = Rechteck_Preis_String;

            Rechteck CatR = new Rechteck();

            if (CatR.CATIA_Rechteck_Run())
            {
                CatR.PartRechteck();
                CatR.Rechteck_CreateSketch();
                CatR.Rechteck_DrawSketch(RechteckBreite, RechteckHoehe);
                CatR.RechteckExtrusion(RechteckLaenge);

                // Ersetzen von Umlauten in den Dateinamen
                string Rechteck_CATPart_Filename_Input = tbx_Rechteck_DateinameCATPart.Text;
                string Rechteck_CATPart_Filename = Rechteck_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string Rechteck_STEP_Filename_Input = tbx_Rechteck_DateinameSTEP.Text;
                string Rechteck_STEP_Filename = Rechteck_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatR.SaveRechteckPart(Rechteck_CATPart_Filename, Rechteck_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Rechteck.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Rechteck.IsChecked));
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
            Double.TryParse(tbx_Input_RechteckPreisProMeter.Text, out double RechteckHohlPreisProMeter);

            // Klasse Rechteckhohlprofil
            Rechteck_Hohl RH = new Rechteck_Hohl();
            RH.setGeometrie(RechteckHohlBreite, RechteckHohlHoehe, RechteckHohlLaenge, RechteckHohlWandstaerke);

            // neue Variablen
            double Rechteck_Hohl_Flaeche, Rechteck_Hohl_Volumen, Rechteck_Hohl_FTM_X, Rechteck_Hohl_FTM_Y, Rechteck_Hohl_SWP_X, Rechteck_Hohl_SWP_Y, Rechteck_Hohl_Masse, Rechteck_Hohl_Preis;

            // Einheitenumrechnung
            if (RechteckEinheit.Equals("mm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 9);
                RechteckHohlPreisProMeter = RechteckHohlPreisProMeter / Math.Pow(10, 3);
            }
            else if (RechteckEinheit.Equals("cm"))
            {
                RechteckWSDichte = RechteckWSDichte / Math.Pow(10, 6);
                RechteckHohlPreisProMeter = RechteckHohlPreisProMeter / Math.Pow(10, 2);
            }
            // Berechnungen
            Rechteck_Hohl_Flaeche = RH.KlasseRechteckHohlBreite * RH.KlasseRechteckHohlHoehe - (RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke) * (RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke);
            Rechteck_Hohl_Volumen = Rechteck_Hohl_Flaeche * RH.KlasseRechteckHohlLaenge;
            Rechteck_Hohl_Masse = Rechteck_Hohl_Volumen * RechteckWSDichte;
            Rechteck_Hohl_FTM_X = RH.KlasseRechteckHohlBreite * Math.Pow(RH.KlasseRechteckHohlHoehe, 3) / 12 - (RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke) * Math.Pow((RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke), 3) / 12;
            Rechteck_Hohl_FTM_Y = RH.KlasseRechteckHohlHoehe * Math.Pow(RH.KlasseRechteckHohlBreite, 3) / 12 - (RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke) * Math.Pow((RH.KlasseRechteckHohlBreite - 2 * RH.KlasseRechteckHohlWandstaerke), 3) / 12;
            Rechteck_Hohl_SWP_X = RH.KlasseRechteckHohlBreite / 2;
            Rechteck_Hohl_SWP_Y = RH.KlasseRechteckHohlHoehe / 2;
            Rechteck_Hohl_Preis = RechteckHohlPreisProMeter * RH.KlasseRechteckHohlLaenge;

            // Runden
            Rechteck_Hohl_Flaeche = Math.Round(Rechteck_Hohl_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Hohl_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Hohl_Flaeche))));
            Rechteck_Hohl_Volumen = Math.Round(Rechteck_Hohl_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Hohl_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Hohl_Volumen))));
            Rechteck_Hohl_Masse = Math.Round(Rechteck_Hohl_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Hohl_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Hohl_Masse))));
            Rechteck_Hohl_FTM_X = Math.Round(Rechteck_Hohl_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Hohl_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Hohl_FTM_X))));
            Rechteck_Hohl_FTM_Y = Math.Round(Rechteck_Hohl_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Hohl_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Hohl_FTM_Y))));
            Rechteck_Hohl_Preis = Math.Round(Rechteck_Hohl_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Rechteck_Hohl_Preis, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Rechteck_Hohl_Preis))));

            // Umwandlung in String
            string Rechteck_Hohl_Flaeche_String = Convert.ToString(Rechteck_Hohl_Flaeche) + " " + RechteckEinheit + "²";
            string Rechteck_Hohl_Volumen_String = Convert.ToString(Rechteck_Hohl_Volumen) + " " + RechteckEinheit + "³";
            string Rechteck_Hohl_Masse_String = Convert.ToString(Rechteck_Hohl_Masse) + " kg";
            string Rechteck_Hohl_FTM_X_String = Convert.ToString(Rechteck_Hohl_FTM_X) + " " + RechteckEinheit + "^4";
            string Rechteck_Hohl_FTM_Y_String = Convert.ToString(Rechteck_Hohl_FTM_Y) + " " + RechteckEinheit + "^4";
            string Rechteck_Hohl_SWP_String = "(x=" + Rechteck_Hohl_SWP_X + " " + RechteckEinheit + "/y=" + Rechteck_Hohl_SWP_Y + " " + RechteckEinheit + ")";
            string Rechteck_Hohl_Preis_String = Convert.ToString(Rechteck_Hohl_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Rechteck_Fläche_Ergebnis.Content = Rechteck_Hohl_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Rechteck_Hohl_Volumen_String;
            lbl_Rechteck_Masse_Ergebnis.Content = Rechteck_Hohl_Masse_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Rechteck_Hohl_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Rechteck_Hohl_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Rechteck_Hohl_SWP_String;
            lbl_Rechteck_Preis_Ergebnis.Content = Rechteck_Hohl_Preis_String;

            Rechteck_Hohl CatR_hohl = new Rechteck_Hohl();

            if (CatR_hohl.CATIA_Rechteck_hohl_Run())
            {
                CatR_hohl.PartRechteck_hohl();
                CatR_hohl.Rechteck_hohl_CreateSketch();
                CatR_hohl.Rechteck_hohl_DrawSketch(RechteckHohlBreite, RechteckHohlHoehe, RechteckHohlWandstaerke);
                CatR_hohl.Rechteck_hohl_Extrusion(RechteckHohlLaenge);

                // Ersetzen von Umlauten in den Dateinamen
                string Rechteck_hohl_CATPart_Filename_Input = tbx_Rechteck_DateinameCATPart.Text;
                string Rechteck_hohl_CATPart_Filename = Rechteck_hohl_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string Rechteck_hohl_STEP_Filename_Input = tbx_Rechteck_DateinameSTEP.Text;
                string Rechteck_hohl_STEP_Filename = Rechteck_hohl_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatR_hohl.SaveRechteck_hohl_Part(Rechteck_hohl_CATPart_Filename, Rechteck_hohl_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Rechteck.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Rechteck.IsChecked));
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
            Double.TryParse(tbx_Input_KreisPreisProMeter.Text, out double KreisPreisProMeter);

            // Klasse Kreisprofil
            Kreis K = new Kreis();
            K.setGeometrie(KreisDurchmesser, KreisLaenge);

            // neue Variablen
            double Kreis_Flaeche, Kreis_Volumen, Kreis_FTM_X, Kreis_FTM_Y, Kreis_SWP_X, Kreis_SWP_Y, Kreis_Masse, Kreis_Preis;

            // Einheitenumrechnung
            if (KreisEinheit.Equals("mm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 9);
                KreisPreisProMeter = KreisPreisProMeter / Math.Pow(10, 3);
            }
            else if (KreisEinheit.Equals("cm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 6);
                KreisPreisProMeter = KreisPreisProMeter / Math.Pow(10, 2);
            }

            // Berechnungen
            Kreis_Flaeche = Math.Pow(K.KlasseKreisDurchmesser, 2) * Math.PI / 4;
            Kreis_Volumen = Kreis_Flaeche * K.KlasseKreisLaenge;
            Kreis_Masse = Kreis_Volumen * KreisWSDichte;
            Kreis_FTM_X = Math.Pow(K.KlasseKreisDurchmesser, 4) * Math.PI / 64;
            Kreis_FTM_Y = Kreis_FTM_X;
            Kreis_SWP_X = K.KlasseKreisDurchmesser / 2;
            Kreis_SWP_Y = K.KlasseKreisDurchmesser / 2;
            Kreis_Preis = KreisPreisProMeter * K.KlasseKreisLaenge;

            // Runden
            Kreis_Flaeche = Math.Round(Kreis_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Flaeche))));
            Kreis_Volumen = Math.Round(Kreis_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Volumen))));
            Kreis_Masse = Math.Round(Kreis_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Masse))));
            Kreis_FTM_X = Math.Round(Kreis_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_FTM_X))));
            Kreis_FTM_Y = Math.Round(Kreis_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_FTM_Y))));
            Kreis_Preis = Math.Round(Kreis_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Preis, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Preis))));


            // Umwandlung in String
            string Kreis_Flaeche_String = Convert.ToString(Kreis_Flaeche) + " " + KreisEinheit + "²";
            string Kreis_Volumen_String = Convert.ToString(Kreis_Volumen) + " " + KreisEinheit + "³";
            string Kreis_Masse_String = Convert.ToString(Kreis_Masse) + " kg";
            string Kreis_FTM_X_String = Convert.ToString(Kreis_FTM_X) + " " + KreisEinheit + "^4";
            string Kreis_FTM_Y_String = Convert.ToString(Kreis_FTM_Y) + " " + KreisEinheit + "^4";
            string Kreis_SWP_String = "(x=" + Kreis_SWP_X + " " + KreisEinheit + "/y=" + Kreis_SWP_Y + " " + KreisEinheit + ")";
            string Kreis_Preis_String = Convert.ToString(Kreis_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Kreis_Fläche_Ergebnis.Content = Kreis_Flaeche_String;
            lbl_Kreis_Volumen_Ergebnis.Content = Kreis_Volumen_String;
            lbl_Kreis_Masse_Ergebnis.Content = Kreis_Masse_String;
            lbl_Kreis_FTM_X_Ergebnis.Content = Kreis_FTM_X_String;
            lbl_Kreis_FTM_Y_Ergebnis.Content = Kreis_FTM_Y_String;
            lbl_Kreis_Schwerpunkt_Ergebnis.Content = Kreis_SWP_String;
            lbl_Kreis_Preis_Ergebnis.Content = Kreis_Preis_String;

            Kreis CatKr = new Kreis();

            if (CatKr.CATIA_Kreis_Run())
            {
                CatKr.PartKreis();
                CatKr.Kreis_CreateSketch();
                CatKr.Kreis_DrawSketch(KreisDurchmesser);
                CatKr.KreisExtrusion(KreisLaenge);

                // Ersetzen von Umlauten in den Dateinamen
                string Kreis_CATPart_Filename_Input = tbx_Kreis_DateinameCATPart.Text;
                string Kreis_CATPart_Filename = Kreis_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string Kreis_STEP_Filename_Input = tbx_Kreis_DateinameSTEP.Text;
                string Kreis_STEP_Filename = Kreis_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatKr.SaveKreis_Part(Kreis_CATPart_Filename, Kreis_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Kreis.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Kreis.IsChecked));
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
            Double.TryParse(tbx_Input_KreisPreisProMeter.Text, out double KreisHohlPreisProMeter);

            // Klasse Kreishohlprofil
            Kreis_Hohl KH = new Kreis_Hohl();
            KH.setGeometrie(KreisHohlDurchmesser, KreisHohlLaenge, KreisHohlWandstaerke);

            // neue Variablen
            double Kreis_Hohl_Flaeche, Kreis_Hohl_Volumen, Kreis_Hohl_FTM_X, Kreis_Hohl_FTM_Y, Kreis_Hohl_SWP_X, Kreis_Hohl_SWP_Y, Kreis_Hohl_Masse, Kreis_Hohl_Preis;

            // Einheitenumrechnung
            if (KreisEinheit.Equals("mm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 9);
                KreisHohlPreisProMeter = KreisHohlPreisProMeter / Math.Pow(10, 3);
            }
            else if (KreisEinheit.Equals("cm"))
            {
                KreisWSDichte = KreisWSDichte / Math.Pow(10, 6);
                KreisHohlPreisProMeter = KreisHohlPreisProMeter / Math.Pow(10, 2);
            }

            // Berechnungen
            Kreis_Hohl_Flaeche = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 2) - Math.Pow((KH.KlasseKreisHohlDurchmesser - 2 * KH.KlasseKreisHohlWandstaerke), 2)) * Math.PI / 4;
            Kreis_Hohl_Volumen = Kreis_Hohl_Flaeche * KH.KlasseKreisHohlLaenge;
            Kreis_Hohl_Masse = Kreis_Hohl_Volumen * KreisWSDichte;
            Kreis_Hohl_FTM_X = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 4) - Math.Pow((KH.KlasseKreisHohlDurchmesser - 2 * KH.KlasseKreisHohlWandstaerke), 4)) * Math.PI / 64;
            Kreis_Hohl_FTM_Y = Kreis_Hohl_FTM_X;
            Kreis_Hohl_SWP_X = KH.KlasseKreisHohlDurchmesser / 2;
            Kreis_Hohl_SWP_Y = KH.KlasseKreisHohlDurchmesser / 2;
            Kreis_Hohl_Preis = KreisHohlPreisProMeter * KH.KlasseKreisHohlLaenge;

            // Runden
            Kreis_Hohl_Flaeche = Math.Round(Kreis_Hohl_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Hohl_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Hohl_Flaeche))));
            Kreis_Hohl_Volumen = Math.Round(Kreis_Hohl_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Hohl_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Hohl_Volumen))));
            Kreis_Hohl_Masse = Math.Round(Kreis_Hohl_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Hohl_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Hohl_Masse))));
            Kreis_Hohl_FTM_X = Math.Round(Kreis_Hohl_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Hohl_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Hohl_FTM_X))));
            Kreis_Hohl_FTM_Y = Math.Round(Kreis_Hohl_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Hohl_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Hohl_FTM_Y))));
            Kreis_Hohl_Preis = Math.Round(Kreis_Hohl_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Kreis_Hohl_Preis, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Kreis_Hohl_Preis))));

            // Umwandlung in String
            string Kreis_Hohl_Flaeche_String = Convert.ToString(Kreis_Hohl_Flaeche) + " " + KreisEinheit + "²";
            string Kreis_Hohl_Volumen_String = Convert.ToString(Kreis_Hohl_Volumen) + " " + KreisEinheit + "³";
            string Kreis_Hohl_Masse_String = Convert.ToString(Kreis_Hohl_Masse) + " kg";
            string Kreis_Hohl_FTM_X_String = Convert.ToString(Kreis_Hohl_FTM_X) + " " + KreisEinheit + "^4";
            string Kreis_Hohl_FTM_Y_String = Convert.ToString(Kreis_Hohl_FTM_Y) + " " + KreisEinheit + "^4";
            string Kreis_Hohl_SWP_String = "(x=" + Kreis_Hohl_SWP_X + " " + KreisEinheit + "/y=" + Kreis_Hohl_SWP_Y + " " + KreisEinheit + ")";
            string Kreis_Hohl_Preis_String = Convert.ToString(Kreis_Hohl_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Kreis_Fläche_Ergebnis.Content = Kreis_Hohl_Flaeche_String;
            lbl_Kreis_Volumen_Ergebnis.Content = Kreis_Hohl_Volumen_String;
            lbl_Kreis_Masse_Ergebnis.Content = Kreis_Hohl_Masse_String;
            lbl_Kreis_FTM_X_Ergebnis.Content = Kreis_Hohl_FTM_X_String;
            lbl_Kreis_FTM_Y_Ergebnis.Content = Kreis_Hohl_FTM_Y_String;
            lbl_Kreis_Schwerpunkt_Ergebnis.Content = Kreis_Hohl_SWP_String;
            lbl_Kreis_Preis_Ergebnis.Content = Kreis_Hohl_Preis_String;

            Kreis_Hohl CatKr_hohl = new Kreis_Hohl();

            if (CatKr_hohl.CATIA_Kreis_hohl_Run())
            {
                CatKr_hohl.PartKreis_hohl();
                CatKr_hohl.Kreis_hohl_CreateSketch();
                CatKr_hohl.Kreis_hohl_DrawSketch(KreisHohlDurchmesser, KreisHohlWandstaerke);
                CatKr_hohl.Kreis_hohl_Extrusion(KreisHohlLaenge);

                // Ersetzen von Umlauten in den Dateinamen
                string Kreis_hohl_CATPart_Filename_Input = tbx_Kreis_DateinameCATPart.Text;
                string Kreis_hohl_CATPart_Filename = Kreis_hohl_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string Kreis_hohl_STEP_Filename_Input = tbx_Kreis_DateinameSTEP.Text;
                string Kreis_hohl_STEP_Filename = Kreis_hohl_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatKr_hohl.SaveKreis_hohl_Part(Kreis_hohl_CATPart_Filename, Kreis_hohl_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Kreis.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Kreis.IsChecked));
            }
        }

        #endregion

        #region BerechnungSonderIPEProfil

        public void I_Profil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_SonderHoehe.Text, out double SonderIPEHoehe);
            Double.TryParse(tbx_Input_SonderBreite.Text, out double SonderIPEBreite);
            Double.TryParse(tbx_Input_SonderFlanschbreite.Text, out double SonderIPEFlanschbreite);
            Double.TryParse(tbx_Input_SonderStegbreite.Text, out double SonderIPEStegbreite);
            Double.TryParse(tbx_Input_SonderLaenge.Text, out double SonderIPELaenge);
            String SonderEinheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);
            Double SonderWSDichte = CoB_Sonder_WS.Text.Equals("Stahl") ? 7850 : 2700;
            Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double SonderIPEPreisProMeter);

            // Klasse SonderIPEProfil
            Sonder_IPE SIPE = new Sonder_IPE();
            SIPE.setGeometrie(SonderIPEHoehe, SonderIPEBreite, SonderIPEFlanschbreite, SonderIPEStegbreite, SonderIPELaenge);

            // neue Variablen
            double Sonder_IPE_Flaeche, Sonder_IPE_Volumen, Sonder_IPE_FTM_X, Sonder_IPE_FTM_Y, Sonder_IPE_SWP_X, Sonder_IPE_SWP_Y, Sonder_IPE_Masse, Sonder_IPE_Preis;

            // Einheitenumrechnung
            if (SonderEinheit.Equals("mm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 9);
                SonderIPEPreisProMeter = SonderIPEPreisProMeter / Math.Pow(10, 3);
            }
            else if (SonderEinheit.Equals("cm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 6);
                SonderIPEPreisProMeter = SonderIPEPreisProMeter / Math.Pow(10, 2);
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
            FTM_Y_kleinesRechteckumSWPA = ((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * Math.Pow(((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) / 2), 3)) / 12;
            FTM_Y_kleinesRechteckSteiner = (SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * ((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) / 2) * Math.Pow(((SIPE.KlasseSonderIPEBreite + SonderIPEStegbreite) / 4), 2); // nur Flaeche * Abstand²
            Sonder_IPE_FTM_Y = FTM_Y_grossesRechteck - 2 * (FTM_Y_kleinesRechteckumSWPA + FTM_Y_kleinesRechteckSteiner);
            Sonder_IPE_SWP_X = SIPE.KlasseSonderIPEHoehe / 2;
            Sonder_IPE_SWP_Y = SIPE.KlasseSonderIPEBreite / 2;
            Sonder_IPE_Preis = SonderIPEPreisProMeter * SIPE.KlasseSonderIPELaenge;

            // Runden
            Sonder_IPE_Flaeche = Math.Round(Sonder_IPE_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_IPE_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_IPE_Flaeche))));
            Sonder_IPE_Volumen = Math.Round(Sonder_IPE_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_IPE_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_IPE_Volumen))));
            Sonder_IPE_Masse = Math.Round(Sonder_IPE_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_IPE_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_IPE_Masse))));
            Sonder_IPE_FTM_X = Math.Round(Sonder_IPE_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_IPE_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_IPE_FTM_X))));
            Sonder_IPE_FTM_Y = Math.Round(Sonder_IPE_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_IPE_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_IPE_FTM_Y))));
            Sonder_IPE_Preis = Math.Round(Sonder_IPE_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_IPE_Preis, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_IPE_Preis))));

            // Umwandlung in String
            string Sonder_IPE_Flaeche_String = Convert.ToString(Sonder_IPE_Flaeche) + " " + SonderEinheit + "²";
            string Sonder_IPE_Volumen_String = Convert.ToString(Sonder_IPE_Volumen) + " " + SonderEinheit + "³";
            string Sonder_IPE_Masse_String = Convert.ToString(Sonder_IPE_Masse) + " kg";
            string Sonder_IPE_FTM_X_String = Convert.ToString(Sonder_IPE_FTM_X) + " " + SonderEinheit + "^4";
            string Sonder_IPE_FTM_Y_String = Convert.ToString(Sonder_IPE_FTM_Y) + " " + SonderEinheit + "^4";
            string Sonder_IPE_SWP_String = "(x=" + Sonder_IPE_SWP_X + " " + SonderEinheit + "/y=" + Sonder_IPE_SWP_Y + " " + SonderEinheit + ")";
            string Sonder_IPE_Preis_String = Convert.ToString(Sonder_IPE_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Sonder_Fläche_Ergebnis.Content = Sonder_IPE_Flaeche_String;
            lbl_Sonder_Volumen_Ergebnis.Content = Sonder_IPE_Volumen_String;
            lbl_Sonder_Masse_Ergebnis.Content = Sonder_IPE_Masse_String;
            lbl_Sonder_FTM_X_Ergebnis.Content = Sonder_IPE_FTM_X_String;
            lbl_Sonder_FTM_Y_Ergebnis.Content = Sonder_IPE_FTM_Y_String;
            lbl_Sonder_Schwerpunkt_Ergebnis.Content = Sonder_IPE_SWP_String;
            lbl_Sonder_Preis_Ergebnis.Content = Sonder_IPE_Preis_String;

            Sonder_IPE CatIPE = new Sonder_IPE();

            if (CatIPE.CATIA_SonderIPE_Run())
            {
                CatIPE.PartSonderIPE();
                CatIPE.SonderIPE_CreateSketch();
                CatIPE.SonderIPE_DrawSketch(SonderIPEBreite, SonderIPEHoehe, SonderIPEFlanschbreite, SonderIPEStegbreite);
                CatIPE.SonderIPE_Extrusion(SonderIPELaenge);

                // Ersetzen von Umlauten in den Dateinamen
                string IPE_CATPart_Filename_Input = tbx_Sonderprofil_DateinameCATPart.Text;
                string IPE_CATPart_Filename = IPE_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string IPE_STEP_Filename_Input = tbx_Sonderprofil_DateinameSTEP.Text;
                string IPE_STEP_Filename = IPE_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatIPE.SaveIPEPart(IPE_CATPart_Filename, IPE_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Sonderprofil.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Sonderprofil.IsChecked));
            }
        }

        #endregion

        #region BerechnungSonderTProfil

        public void T_Profil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_SonderHoehe.Text, out double SonderTHoehe);
            Double.TryParse(tbx_Input_SonderBreite.Text, out double SonderTBreite);
            Double.TryParse(tbx_Input_SonderFlanschbreite.Text, out double SonderTFlanschbreite);
            Double.TryParse(tbx_Input_SonderStegbreite.Text, out double SonderTStegbreite);
            Double.TryParse(tbx_Input_SonderLaenge.Text, out double SonderTLaenge);
            String SonderEinheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);
            Double SonderWSDichte = CoB_Sonder_WS.Text.Equals("Stahl") ? 7850 : 2700;
            Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double SonderTPreisProMeter);

            // Klasse SonderTProfil
            Sonder_T ST = new Sonder_T();
            ST.setGeometrie(SonderTHoehe, SonderTBreite, SonderTFlanschbreite, SonderTStegbreite, SonderTLaenge);

            // neue Variablen
            double Sonder_T_Flaeche, Sonder_T_Volumen, Sonder_T_FTM_X, Sonder_T_FTM_Y, Sonder_T_SWP_X, Sonder_T_SWP_Y, Sonder_T_Masse, Sonder_T_Preis;

            // Einheitenumrechnung
            if (SonderEinheit.Equals("mm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 9);
                SonderTPreisProMeter = SonderTPreisProMeter / Math.Pow(10, 3);
            }
            else if (SonderEinheit.Equals("cm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 6);
                SonderTPreisProMeter = SonderTPreisProMeter / Math.Pow(10, 2);
            }

            // Hilfsgrößen, da Formeln für FTMX und FTMY sehr lang
            double FTM_X_grossesRechteckumSWPA, FTM_X_grossesRechteckSteiner, FTM_X_kleinesRechteckumSWPA, FTM_X_kleinesRechteckSteiner, FTM_Y_grossesRechteckumSWPA, FTM_Y_grossesRechteckSteiner, FTM_Y_kleinesRechteckumSWPA, FTM_Y_kleinesRechteckSteiner;

            // Berechnungen
            Sonder_T_Flaeche = ST.KlasseSonderTHoehe * ST.KlasseSonderTBreite - (ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite) * (ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite);
            Sonder_T_Volumen = Sonder_T_Flaeche * ST.KlasseSonderTLaenge;
            Sonder_T_Masse = Sonder_T_Volumen * SonderWSDichte;

            Sonder_T_SWP_X = ST.KlasseSonderTBreite / 2;
            Sonder_T_SWP_Y = 0.5 * ((ST.KlasseSonderTBreite * Math.Pow(ST.KlasseSonderTHoehe, 2) - Math.Pow(ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite, 2) * (ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite))) / (ST.KlasseSonderTBreite * ST.KlasseSonderTHoehe - (ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite) * (ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite));

            FTM_X_grossesRechteckumSWPA = ST.KlasseSonderTBreite * Math.Pow(ST.KlasseSonderTHoehe, 3) / 12;
            FTM_X_grossesRechteckSteiner = ST.KlasseSonderTHoehe * ST.KlasseSonderTBreite * Math.Pow((ST.KlasseSonderTHoehe / 2) - Sonder_T_SWP_Y, 2);
            FTM_X_kleinesRechteckumSWPA = ((ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite) / 2) * Math.Pow((ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite), 3) / 12;
            FTM_X_kleinesRechteckSteiner = (ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite) * (ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite) / 2 * Math.Pow(Sonder_T_SWP_Y - (ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite) / 2, 2);
            Sonder_T_FTM_X = FTM_X_grossesRechteckumSWPA + FTM_X_grossesRechteckSteiner - 2 * (FTM_X_kleinesRechteckumSWPA + FTM_X_kleinesRechteckSteiner);

            FTM_Y_grossesRechteckumSWPA = ST.KlasseSonderTHoehe * Math.Pow(ST.KlasseSonderTBreite, 3) / 12;
            FTM_Y_grossesRechteckSteiner = 0;
            FTM_Y_kleinesRechteckumSWPA = Math.Pow(((ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite) / 2), 3) * (ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite) / 12;
            FTM_Y_kleinesRechteckSteiner = ((ST.KlasseSonderTHoehe - ST.KlasseSonderTFlanschbreite) * (ST.KlasseSonderTBreite - ST.KlasseSonderTStegbreite) / 2) * Math.Pow((ST.KlasseSonderTBreite + ST.KlasseSonderTStegbreite) / 4, 2);
            Sonder_T_FTM_Y = FTM_Y_grossesRechteckumSWPA + FTM_Y_grossesRechteckSteiner - 2 * (FTM_Y_kleinesRechteckumSWPA + FTM_Y_kleinesRechteckSteiner);
            Sonder_T_Preis = SonderTPreisProMeter * ST.KlasseSonderTLaenge;

            // Runden
            Sonder_T_Flaeche = Math.Round(Sonder_T_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_Flaeche))));
            Sonder_T_Volumen = Math.Round(Sonder_T_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_Volumen))));
            Sonder_T_Masse = Math.Round(Sonder_T_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_Masse))));
            Sonder_T_SWP_X = Math.Round(Sonder_T_SWP_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_SWP_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_SWP_X))));
            Sonder_T_SWP_Y = Math.Round(Sonder_T_SWP_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_SWP_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_SWP_Y))));
            Sonder_T_FTM_X = Math.Round(Sonder_T_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_FTM_X))));
            Sonder_T_FTM_Y = Math.Round(Sonder_T_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_FTM_Y))));
            Sonder_T_Preis = Math.Round(Sonder_T_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_T_Preis, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_T_Preis))));

            // Umwandlung in String
            string Sonder_T_Flaeche_String = Convert.ToString(Sonder_T_Flaeche) + " " + SonderEinheit + "²";
            string Sonder_T_Volumen_String = Convert.ToString(Sonder_T_Volumen) + " " + SonderEinheit + "³";
            string Sonder_T_Masse_String = Convert.ToString(Sonder_T_Masse) + " kg";
            string Sonder_T_FTM_X_String = Convert.ToString(Sonder_T_FTM_X) + " " + SonderEinheit + "^4";
            string Sonder_T_FTM_Y_String = Convert.ToString(Sonder_T_FTM_Y) + " " + SonderEinheit + "^4";
            string Sonder_T_SWP_String = "(x=" + Sonder_T_SWP_X + " " + SonderEinheit + "/y=" + Sonder_T_SWP_Y + " " + SonderEinheit + ")";
            string Sonder_T_Preis_String = Convert.ToString(Sonder_T_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Sonder_Fläche_Ergebnis.Content = Sonder_T_Flaeche_String;
            lbl_Sonder_Volumen_Ergebnis.Content = Sonder_T_Volumen_String;
            lbl_Sonder_Masse_Ergebnis.Content = Sonder_T_Masse_String;
            lbl_Sonder_FTM_X_Ergebnis.Content = Sonder_T_FTM_X_String;
            lbl_Sonder_FTM_Y_Ergebnis.Content = Sonder_T_FTM_Y_String;
            lbl_Sonder_Schwerpunkt_Ergebnis.Content = Sonder_T_SWP_String;
            lbl_Sonder_Preis_Ergebnis.Content = Sonder_T_Preis_String;

            Sonder_T CatT = new Sonder_T();

            if (CatT.CATIA_SonderT_Run())
            {
                CatT.PartSonderT();
                CatT.SonderT_CreateSketch();
                CatT.SonderT_DrawSketch(SonderTBreite, SonderTHoehe, SonderTFlanschbreite, SonderTStegbreite);
                CatT.SonderT_Extrusion(SonderTLaenge, SonderTFlanschbreite, SonderTStegbreite);

                // Ersetzen von Umlauten in den Dateinamen
                string T_CATPart_Filename_Input = tbx_Sonderprofil_DateinameCATPart.Text;
                string T_CATPart_Filename = T_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string T_STEP_Filename_Input = tbx_Sonderprofil_DateinameSTEP.Text;
                string T_STEP_Filename = T_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatT.SaveTPart(T_CATPart_Filename, T_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Sonderprofil.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Sonderprofil.IsChecked));
            }
        }

        #endregion

        #region BerechnungSonderUProfil

        public void U_Profil_Berechnung()
        {
            // Übergabe von Eingabewerten
            Double.TryParse(tbx_Input_SonderHoehe.Text, out double SonderUHoehe);
            Double.TryParse(tbx_Input_SonderBreite.Text, out double SonderUBreite);
            Double.TryParse(tbx_Input_SonderFlanschbreite.Text, out double SonderUFlanschbreite);
            Double.TryParse(tbx_Input_SonderStegbreite.Text, out double SonderUStegbreite);
            Double.TryParse(tbx_Input_SonderLaenge.Text, out double SonderULaenge);
            String SonderEinheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);
            Double SonderWSDichte = CoB_Sonder_WS.Text.Equals("Stahl") ? 7850 : 2700;
            Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double SonderUPreisProMeter);

            // Klasse SonderUProfil
            Sonder_U SU = new Sonder_U();
            SU.setGeometrie(SonderUHoehe, SonderUBreite, SonderUFlanschbreite, SonderUStegbreite, SonderULaenge);

            // neue Variablen
            double Sonder_U_Flaeche, Sonder_U_Volumen, Sonder_U_FTM_X, Sonder_U_FTM_Y, Sonder_U_SWP_X, Sonder_U_SWP_Y, Sonder_U_Masse, Sonder_U_Preis;

            // Einheitenumrechnung
            if (SonderEinheit.Equals("mm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 9);
                SonderUPreisProMeter = SonderUPreisProMeter / Math.Pow(10, 3);
            }
            else if (SonderEinheit.Equals("cm"))
            {
                SonderWSDichte = SonderWSDichte / Math.Pow(10, 6);
                SonderUPreisProMeter = SonderUPreisProMeter / Math.Pow(10, 2);
            }

            // Hilfsgrößen, da Formeln für FTMX und FTMY sehr lang
            double FTM_X_grossesRechteckumSWPA, FTM_X_grossesRechteckSteiner, FTM_X_kleinesRechteckumSWPA, FTM_X_kleinesRechteckSteiner, FTM_Y_grossesRechteckumSWPA, FTM_Y_grossesRechteckSteiner, FTM_Y_kleinesRechteckumSWPA, FTM_Y_kleinesRechteckSteiner;

            // Berechnungen
            Sonder_U_Flaeche = SU.KlasseSonderUHoehe * SU.KlasseSonderUBreite - (SU.KlasseSonderUHoehe - SU.KlasseSonderUFlanschbreite) * (SU.KlasseSonderUBreite - SU.KlasseSonderUStegbreite);
            Sonder_U_Volumen = Sonder_U_Flaeche * SU.KlasseSonderULaenge;
            Sonder_U_Masse = Sonder_U_Volumen * SonderWSDichte;

            Sonder_U_SWP_X = ((Math.Pow(SU.KlasseSonderUBreite, 2) * SU.KlasseSonderUHoehe / 2) - (SU.KlasseSonderUBreite - SU.KlasseSonderUStegbreite) * (SU.KlasseSonderUHoehe - 2 * SU.KlasseSonderUFlanschbreite) * ((SU.KlasseSonderUStegbreite + SU.KlasseSonderUBreite) / 2)) / ((SU.KlasseSonderUBreite * SU.KlasseSonderUHoehe) - (SU.KlasseSonderUBreite - SU.KlasseSonderUStegbreite) * (SU.KlasseSonderUHoehe - 2 * SU.KlasseSonderUFlanschbreite));
            Sonder_U_SWP_Y = SU.KlasseSonderUHoehe / 2;

            FTM_X_grossesRechteckumSWPA = SU.KlasseSonderUBreite * Math.Pow(SU.KlasseSonderUHoehe, 3) / 12;
            FTM_X_grossesRechteckSteiner = 0;
            FTM_X_kleinesRechteckumSWPA = (SU.KlasseSonderUBreite - 2 * SU.KlasseSonderUStegbreite) * Math.Pow((SU.KlasseSonderUHoehe - 2 * SU.KlasseSonderUFlanschbreite), 3) / 12;
            FTM_X_kleinesRechteckSteiner = 0;
            Sonder_U_FTM_X = FTM_X_grossesRechteckumSWPA + FTM_X_grossesRechteckSteiner - (FTM_X_kleinesRechteckumSWPA + FTM_X_kleinesRechteckSteiner);

            FTM_Y_grossesRechteckumSWPA = SU.KlasseSonderUHoehe * Math.Pow(SU.KlasseSonderUBreite, 3) / 12;
            FTM_Y_grossesRechteckSteiner = SU.KlasseSonderUHoehe * SU.KlasseSonderUBreite * Math.Pow(((SU.KlasseSonderUBreite / 2) - Sonder_U_SWP_X), 2);
            FTM_Y_kleinesRechteckumSWPA = (SU.KlasseSonderUHoehe - 2 * SU.KlasseSonderUFlanschbreite) * Math.Pow((SU.KlasseSonderUBreite - SU.KlasseSonderUStegbreite), 3) / 12;
            FTM_Y_kleinesRechteckSteiner = (SU.KlasseSonderUHoehe - 2 * SU.KlasseSonderUFlanschbreite) * (SU.KlasseSonderUBreite - SU.KlasseSonderUStegbreite) * Math.Pow((((SU.KlasseSonderUBreite + SU.KlasseSonderUStegbreite) / 2) - Sonder_U_SWP_X), 2);
            Sonder_U_FTM_Y = FTM_Y_grossesRechteckumSWPA + FTM_Y_grossesRechteckSteiner - (FTM_Y_kleinesRechteckumSWPA + FTM_Y_kleinesRechteckSteiner);
            Sonder_U_Preis = SonderUPreisProMeter * SU.KlasseSonderULaenge;

            // Runden
            Sonder_U_Flaeche = Math.Round(Sonder_U_Flaeche / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_Flaeche, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_Flaeche))));
            Sonder_U_Volumen = Math.Round(Sonder_U_Volumen / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_Volumen, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_Volumen))));
            Sonder_U_Masse = Math.Round(Sonder_U_Masse / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_Masse, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_Masse))));
            Sonder_U_SWP_X = Math.Round(Sonder_U_SWP_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_SWP_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_SWP_X))));
            Sonder_U_SWP_Y = Math.Round(Sonder_U_SWP_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_SWP_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_SWP_Y))));
            Sonder_U_FTM_X = Math.Round(Sonder_U_FTM_X / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_FTM_X, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_FTM_X))));
            Sonder_U_FTM_Y = Math.Round(Sonder_U_FTM_Y / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_FTM_Y, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_FTM_Y))));
            Sonder_U_Preis = Math.Round(Sonder_U_Preis / Math.Pow(10, Math.Floor(Math.Log10(Math.Sqrt(Math.Pow(Sonder_U_Preis, 2))))), 3) * Math.Pow(10, Math.Floor(Math.Log10(Math.Abs(Sonder_U_Preis))));

            // Umwandlung in String
            string Sonder_U_Flaeche_String = Convert.ToString(Sonder_U_Flaeche) + " " + SonderEinheit + "²";
            string Sonder_U_Volumen_String = Convert.ToString(Sonder_U_Volumen) + " " + SonderEinheit + "³";
            string Sonder_U_Masse_String = Convert.ToString(Sonder_U_Masse) + " kg";
            string Sonder_U_FTM_X_String = Convert.ToString(Sonder_U_FTM_X) + " " + SonderEinheit + "^4";
            string Sonder_U_FTM_Y_String = Convert.ToString(Sonder_U_FTM_Y) + " " + SonderEinheit + "^4";
            string Sonder_U_SWP_String = "(x=" + Sonder_U_SWP_X + " " + SonderEinheit + "/y=" + Sonder_U_SWP_Y + " " + SonderEinheit + ")";
            string Sonder_U_Preis_String = Convert.ToString(Sonder_U_Preis) + " €";

            // Übergabe Ergebnisse
            lbl_Sonder_Fläche_Ergebnis.Content = Sonder_U_Flaeche_String;
            lbl_Sonder_Volumen_Ergebnis.Content = Sonder_U_Volumen_String;
            lbl_Sonder_Masse_Ergebnis.Content = Sonder_U_Masse_String;
            lbl_Sonder_FTM_X_Ergebnis.Content = Sonder_U_FTM_X_String;
            lbl_Sonder_FTM_Y_Ergebnis.Content = Sonder_U_FTM_Y_String;
            lbl_Sonder_Schwerpunkt_Ergebnis.Content = Sonder_U_SWP_String;
            lbl_Sonder_Preis_Ergebnis.Content = Sonder_U_Preis_String;

            Sonder_U CatU = new Sonder_U();

            if (CatU.CATIA_SonderU_Run())
            {
                CatU.PartSonderU();
                CatU.SonderU_CreateSketch();
                CatU.SonderU_DrawSketch(SonderUBreite, SonderUHoehe, SonderUFlanschbreite, SonderUStegbreite);
                CatU.SonderU_Extrusion(SonderULaenge, SonderUFlanschbreite);

                // Ersetzen von Umlauten in den Dateinamen
                string U_CATPart_Filename_Input = tbx_Sonderprofil_DateinameCATPart.Text;
                string U_CATPart_Filename = U_CATPart_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                string U_STEP_Filename_Input = tbx_Sonderprofil_DateinameSTEP.Text;
                string U_STEP_Filename = U_STEP_Filename_Input.Replace("ä", "ae").Replace("ö", "oe").Replace("ü", "ue").Replace("Ä", "Ae").Replace("Ö", "Oe").Replace("Ü", "Ue");

                CatU.SaveUPart(U_CATPart_Filename, U_STEP_Filename, Convert.ToBoolean(ChB_ExportCATPart_Sonderprofil.IsChecked), Convert.ToBoolean(ChB_ExportSTEP_Sonderprofil.IsChecked));
            }

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
                    if (tbx_Input_RechteckPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_RechteckPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_RechteckPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_RechteckPreisProMeter.Text, out double P))
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
                                                    if (Double.TryParse(tbx_Input_RechteckPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                                    {
                                                        string Zeichenlaenge_pr;
                                                        Zeichenlaenge_pr = Convert.ToString(pr);


                                                        if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                        {

                                                            MessageBoxResult result;
                                                            result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error
                                                            );
                                                            if (result == MessageBoxResult.OK)
                                                            {
                                                                tbx_Input_RechteckPreisProMeter.Text = "";

                                                                tbx_Input_RechteckPreisProMeter.Focus();
                                                            }

                                                        }
                                                        if (Zeichenlaenge_pr.Length <= 4)
                                                        {
                                                            Rechteckprofil_Berechnung(); // Aufruf der Berechnung
                                                        }
                                                    }
                                                    else
                                                    {
                                                        MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                        MessageBoxButton.OK,
                                                        MessageBoxImage.Error
                                                        );

                                                        tbx_Input_RechteckPreisProMeter.Clear();
                                                        tbx_Input_RechteckPreisProMeter.Focus();
                                                    }



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
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Preis: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_RechteckPreisProMeter.Text = "";

                                tbx_Input_RechteckPreisProMeter.Focus();
                            }
                        }
                    }

                    

                }




                lbl_Rechteck_Fläche_Ergebnis.Visibility = Visibility.Visible;   //Schaltet Sichtbarkeit der Ergebnisse um
                lbl_Rechteck_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Preis_Ergebnis.Visibility = Visibility.Visible;
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
                    if (tbx_Input_RechteckPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben. Bitte geben Sie einen Preis ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_RechteckPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_RechteckPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_RechteckPreisProMeter.Text, out double P))
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
                                                    if (Zeichenlaenge_b.Length <= 4)
                                                    {
                                                        if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlänge der Höhe
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
                                                        if (Zeichenlaenge_h.Length <= 4)
                                                        {
                                                            if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
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
                                                            if (Zeichenlaenge_l.Length <= 4)
                                                            {
                                                                if (Zeichenlaenge_w.Length > 2)        //Kontrolle der Zeichenlänge der Wandstärke
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
                                                                    if (Double.TryParse(tbx_Input_RechteckPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                                                    {
                                                                        string Zeichenlaenge_pr;
                                                                        Zeichenlaenge_pr = Convert.ToString(pr);


                                                                        if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                                        {

                                                                            MessageBoxResult result;
                                                                            result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                                            MessageBoxButton.OK,
                                                                            MessageBoxImage.Error
                                                                            );
                                                                            if (result == MessageBoxResult.OK)
                                                                            {
                                                                                tbx_Input_RechteckPreisProMeter.Text = "";

                                                                                tbx_Input_RechteckPreisProMeter.Focus();
                                                                            }

                                                                        }
                                                                        if (Zeichenlaenge_pr.Length <= 4)
                                                                        {
                                                                            Rechteckprofil_hohl_Berechnung(); // Aufruf der Berechnung
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                                        MessageBoxButton.OK,
                                                                        MessageBoxImage.Error
                                                                        );

                                                                        tbx_Input_RechteckPreisProMeter.Clear();
                                                                        tbx_Input_RechteckPreisProMeter.Focus();
                                                                    }


                                                                }
                                                            }
                                                        }
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
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Preis: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_RechteckPreisProMeter.Text = "";

                                tbx_Input_RechteckPreisProMeter.Focus();
                            }
                        }
                    }

                    


                }



                lbl_Rechteck_Fläche_Ergebnis.Visibility = Visibility.Visible;   //Schaltet Sichtbarkeit der Ergebnisse um
                lbl_Rechteck_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Rechteck_Preis_Ergebnis.Visibility = Visibility.Visible;


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
                    if (tbx_Input_KreisPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben. Bitte geben Sie einen Preis ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_KreisPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_KreisPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_KreisPreisProMeter.Text, out double P))
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
                                    if (Zeichenlaenge_Durchmesser.Length <= 4)
                                    {
                                        if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
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
                                        if (Zeichenlaenge_l.Length <= 4)
                                        {

                                            if (Double.TryParse(tbx_Input_KreisPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                            {
                                                string Zeichenlaenge_pr;
                                                Zeichenlaenge_pr = Convert.ToString(pr);


                                                if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                {

                                                    MessageBoxResult result;
                                                    result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                    MessageBoxButton.OK,
                                                    MessageBoxImage.Error
                                                    );
                                                    if (result == MessageBoxResult.OK)
                                                    {
                                                        tbx_Input_KreisPreisProMeter.Text = "";

                                                        tbx_Input_KreisPreisProMeter.Focus();
                                                    }

                                                }
                                                if (Zeichenlaenge_pr.Length <= 4)
                                                {
                                                    Kreisprofil_Berechnung(); // Aufruf der Berechnung
                                                }
                                            }
                                            else
                                            {
                                                MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                MessageBoxButton.OK,
                                                MessageBoxImage.Error
                                                );

                                                tbx_Input_RechteckPreisProMeter.Clear();
                                                tbx_Input_RechteckPreisProMeter.Focus();
                                            }




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
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Preis: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_KreisPreisProMeter.Text = "";

                                tbx_Input_KreisPreisProMeter.Focus();
                            }
                        }
                    }
                    



                }


                lbl_Kreis_Fläche_Ergebnis.Visibility = Visibility.Visible;  //Schaltet Ergebnis Sichtbarkeit um
                lbl_Kreis_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Preis_Ergebnis.Visibility = Visibility.Visible;
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
                    if (tbx_Input_KreisPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben. Bitte geben Sie einen Preis ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_KreisPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_KreisPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_KreisPreisProMeter.Text, out double P))
                        {
                            if (Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double D))     //Kontrolle auf Buchstaben des Durchmessers
                            {
                                if (Double.TryParse(tbx_Input_Kreis_hohlWandstaerke.Text, out double w))     //Kontrolle auf Buchstaben der Wandstärke
                                {
                                    if (Double.TryParse(tbx_Input_KreisLaenge.Text, out double l))     //Kontrolle auf Buchstaben der Länge
                                    {

                                        string Zeichenlaenge_D;
                                        string Zeichenlaenge_w;
                                        string Zeichenlaenge_l;

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
                                            if (Zeichenlaenge_D.Length <= 4)
                                            {
                                                if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
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
                                                if (Zeichenlaenge_l.Length <= 4)
                                                {
                                                    if (Zeichenlaenge_w.Length > 3)        //Kontrolle der Zeichenlänge der Wandstärke
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
                                                    if (Zeichenlaenge_w.Length <= 3)
                                                    {
                                                        if (Double.TryParse(tbx_Input_KreisPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                                        {
                                                            string Zeichenlaenge_pr;
                                                            Zeichenlaenge_pr = Convert.ToString(pr);


                                                            if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                            {

                                                                MessageBoxResult result;
                                                                result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                                MessageBoxButton.OK,
                                                                MessageBoxImage.Error
                                                                );
                                                                if (result == MessageBoxResult.OK)
                                                                {
                                                                    tbx_Input_KreisPreisProMeter.Text = "";

                                                                    tbx_Input_KreisPreisProMeter.Focus();
                                                                }

                                                            }
                                                            if (Zeichenlaenge_pr.Length <= 4)
                                                            {
                                                                Kreisprofil_hohl_Berechnung(); // Aufruf der Berechnung
                                                            }
                                                        }
                                                        else
                                                        {
                                                            MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                            MessageBoxButton.OK,
                                                            MessageBoxImage.Error
                                                            );

                                                            tbx_Input_RechteckPreisProMeter.Clear();
                                                            tbx_Input_RechteckPreisProMeter.Focus();
                                                        }



                                                    }
                                                }
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
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Preis: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_KreisPreisProMeter.Text = "";

                                tbx_Input_KreisPreisProMeter.Focus();
                            }
                        }
                    }
                   



                }


                lbl_Kreis_Fläche_Ergebnis.Visibility = Visibility.Visible;  //Schaltet Ergebnis Sichtbarkeit um
                lbl_Kreis_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Kreis_Preis_Ergebnis.Visibility = Visibility.Visible;
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
                    if (tbx_Input_SonderPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben. Bitte geben Sie einen Preis ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_SonderPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_SonderPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double P))
                        {
                            if (Double.TryParse(tbx_Input_SonderBreite.Text, out double B))     //Kontrolle auf Buchstaben der Breite
                            {
                                if (Double.TryParse(tbx_Input_SonderHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                                {
                                    if (Double.TryParse(tbx_Input_SonderFlanschbreite.Text, out double b))     //Kontrolle auf Buchstaben der Flanschbreite
                                    {
                                        if (Double.TryParse(tbx_Input_SonderStegbreite.Text, out double sb))        //Kontrolle auf Buchstaben der Stegbreite
                                        {
                                            if (Double.TryParse(tbx_Input_SonderLaenge.Text, out double l))     //Kontrolle auf Buchstaben der Länge
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

                                                if (Stegbreite < (Breite / 4))        //Kontrolle der Verhältnismäßigkeit der Stegbreite
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
                                                                tbx_Input_SonderBreite.Text = "";

                                                                tbx_Input_SonderBreite.Focus();
                                                            }
                                                        }
                                                        if (Zeichenlaenge_B.Length <= 4)
                                                        {

                                                            if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlaenge der Höhe
                                                            {
                                                                MessageBoxResult result;
                                                                result = MessageBox.Show("Höhe: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                                                MessageBoxButton.OK,
                                                                MessageBoxImage.Error
                                                                );
                                                                if (result == MessageBoxResult.OK)
                                                                {
                                                                    tbx_Input_SonderHoehe.Text = "";

                                                                    tbx_Input_SonderHoehe.Focus();
                                                                }
                                                            }
                                                            if (Zeichenlaenge_h.Length <= 4)
                                                            {
                                                                if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlaenge der Länge
                                                                {
                                                                    MessageBoxResult result;
                                                                    result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Error
                                                                    );
                                                                    if (result == MessageBoxResult.OK)
                                                                    {
                                                                        tbx_Input_SonderLaenge.Text = "";

                                                                        tbx_Input_SonderLaenge.Focus();
                                                                    }
                                                                }
                                                                if (Zeichenlaenge_l.Length <= 4)

                                                                {
                                                                    if (Zeichenlaenge_b.Length > 2)        //Kontrolle der Zeichenlaenge der Flanschbreite
                                                                    {
                                                                        MessageBoxResult result;
                                                                        result = MessageBox.Show("Flanschbreite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 2 Stellen.", "Eingabekontrolle",
                                                                        MessageBoxButton.OK,
                                                                        MessageBoxImage.Error
                                                                        );
                                                                        if (result == MessageBoxResult.OK)
                                                                        {
                                                                            tbx_Input_SonderFlanschbreite.Text = "";

                                                                            tbx_Input_SonderFlanschbreite.Focus();
                                                                        }
                                                                    }
                                                                    if (Zeichenlaenge_b.Length <= 4)
                                                                    {
                                                                        if (Zeichenlaenge_sb.Length > 2)       //Kontrolle der Zeichenlaenge der Stegbreite
                                                                        {
                                                                            MessageBoxResult result;
                                                                            result = MessageBox.Show("Stegbreite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 2 Stellen.", "Eingabekontrolle",
                                                                            MessageBoxButton.OK,
                                                                            MessageBoxImage.Error
                                                                            );
                                                                            if (result == MessageBoxResult.OK)
                                                                            {
                                                                                tbx_Input_SonderStegbreite.Text = "";

                                                                                tbx_Input_SonderStegbreite.Focus();
                                                                            }
                                                                        }


                                                                        if (Zeichenlaenge_sb.Length <= 2)
                                                                        {
                                                                            //Aufruf der Berechnung

                                                                            if (Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                                                            {
                                                                                string Zeichenlaenge_pr;
                                                                                Zeichenlaenge_pr = Convert.ToString(pr);


                                                                                if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                                                {

                                                                                    MessageBoxResult result;
                                                                                    result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                                                    MessageBoxButton.OK,
                                                                                    MessageBoxImage.Error
                                                                                    );
                                                                                    if (result == MessageBoxResult.OK)
                                                                                    {
                                                                                        tbx_Input_SonderPreisProMeter.Text = "";

                                                                                        tbx_Input_SonderPreisProMeter.Focus();
                                                                                    }

                                                                                }
                                                                                if (Zeichenlaenge_pr.Length <= 4)
                                                                                {
                                                                                    I_Profil_Berechnung(); // Aufruf der Berechnung
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                                                MessageBoxButton.OK,
                                                                                MessageBoxImage.Error
                                                                                );

                                                                                tbx_Input_RechteckPreisProMeter.Clear();
                                                                                tbx_Input_RechteckPreisProMeter.Focus();
                                                                            }




                                                                        }
                                                                    }
                                                                }
                                                            }
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
                                                            tbx_Input_SonderFlanschbreite.Text = "";

                                                            tbx_Input_SonderFlanschbreite.Focus();
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
                                                        tbx_Input_SonderStegbreite.Text = "";

                                                        tbx_Input_SonderStegbreite.Focus();
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
                                                    tbx_Input_SonderLaenge.Text = "";

                                                    tbx_Input_SonderLaenge.Focus();
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
                                                tbx_Input_SonderStegbreite.Text = "";

                                                tbx_Input_SonderStegbreite.Focus();
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
                                            tbx_Input_SonderFlanschbreite.Text = "";

                                            tbx_Input_SonderFlanschbreite.Focus();
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
                                        tbx_Input_SonderHoehe.Text = "";

                                        tbx_Input_SonderHoehe.Focus();
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
                                    tbx_Input_SonderBreite.Text = "";

                                    tbx_Input_SonderBreite.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Preis: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_SonderPreisProMeter.Text = "";

                                tbx_Input_SonderPreisProMeter.Focus();
                            }
                        }
                    }
                    





                }

                lbl_Sonder_Fläche_Ergebnis.Visibility = Visibility.Visible;     //Schaltet Ergebnis Sichtbarkeit um
                lbl_Sonder_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Preis_Ergebnis.Visibility = Visibility.Visible;
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
                    if (tbx_Input_SonderPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben. Bitte geben Sie einen Preis ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_SonderPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_SonderPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double P))
                        {
                            if (Double.TryParse(tbx_Input_SonderBreite.Text, out double b))        //Kontrolle auf Buchstaben der Breite
                            {
                                //Konvertierung erfolgreich

                                if (Double.TryParse(tbx_Input_SonderHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                                {
                                    //Konvertierung erfolgreich

                                    if (Double.TryParse(tbx_Input_SonderLaenge.Text, out double l))        //Kontrolle auf Buchstaben der Länge
                                    {
                                        //Konvertierung erfolgreich

                                        if (Double.TryParse(tbx_Input_SonderFlanschbreite.Text, out double fb))        //Kontrolle auf Buchstaben der Flanschbreite
                                        {
                                            //Konvertierung erfolgreich

                                            if (Double.TryParse(tbx_Input_SonderStegbreite.Text, out double sb))       //Kontrolle auf Buchstaben der Stegbreite
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
                                                                tbx_Input_SonderBreite.Text = "";

                                                                tbx_Input_SonderBreite.Focus();
                                                            }
                                                        }
                                                        if (Zeichenlaenge_b.Length <= 4)
                                                        {
                                                            if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlänge der Höhe
                                                            {
                                                                MessageBoxResult result;
                                                                result = MessageBox.Show("Höhe: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                                MessageBoxButton.OK,
                                                                MessageBoxImage.Error
                                                                );
                                                                if (result == MessageBoxResult.OK)
                                                                {
                                                                    tbx_Input_SonderHoehe.Text = "";

                                                                    tbx_Input_SonderHoehe.Focus();
                                                                }
                                                            }
                                                            if (Zeichenlaenge_h.Length <= 4)
                                                            {
                                                                if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                                                                {
                                                                    MessageBoxResult result;
                                                                    result = MessageBox.Show("Länge: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Error
                                                                    );
                                                                    if (result == MessageBoxResult.OK)
                                                                    {
                                                                        tbx_Input_SonderLaenge.Text = "";

                                                                        tbx_Input_SonderLaenge.Focus();
                                                                    }
                                                                }
                                                                if (Zeichenlaenge_l.Length <= 4)
                                                                {
                                                                    if (Zeichenlaenge_fb.Length > 2)       //Kontrolle der Zeichenlänge der Flanschbreite
                                                                    {
                                                                        MessageBoxResult result;
                                                                        result = MessageBox.Show("Flanschbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                                        MessageBoxButton.OK,
                                                                        MessageBoxImage.Error
                                                                        );
                                                                        if (result == MessageBoxResult.OK)
                                                                        {
                                                                            tbx_Input_SonderFlanschbreite.Text = "";

                                                                            tbx_Input_SonderFlanschbreite.Focus();
                                                                        }
                                                                    }
                                                                    if (Zeichenlaenge_fb.Length <= 2)
                                                                    {
                                                                        if (Zeichenlaenge_sb.Length > 2)       //Kontrolle der Zeichenlänge der Stegbreite
                                                                        {
                                                                            MessageBoxResult result;
                                                                            result = MessageBox.Show("Stegbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                                            MessageBoxButton.OK,
                                                                            MessageBoxImage.Error
                                                                            );
                                                                            if (result == MessageBoxResult.OK)
                                                                            {
                                                                                tbx_Input_SonderStegbreite.Text = "";

                                                                                tbx_Input_SonderStegbreite.Focus();
                                                                            }
                                                                        }
                                                                        if (Zeichenlaenge_sb.Length <= 2)
                                                                        {
                                                                            if (Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                                                            {
                                                                                string Zeichenlaenge_pr;
                                                                                Zeichenlaenge_pr = Convert.ToString(pr);


                                                                                if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                                                {

                                                                                    MessageBoxResult result;
                                                                                    result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                                                    MessageBoxButton.OK,
                                                                                    MessageBoxImage.Error
                                                                                    );
                                                                                    if (result == MessageBoxResult.OK)
                                                                                    {
                                                                                        tbx_Input_SonderPreisProMeter.Text = "";

                                                                                        tbx_Input_SonderPreisProMeter.Focus();
                                                                                    }

                                                                                }
                                                                                if (Zeichenlaenge_pr.Length <= 4)
                                                                                {
                                                                                    U_Profil_Berechnung(); // Aufruf der Berechnung
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                                                MessageBoxButton.OK,
                                                                                MessageBoxImage.Error
                                                                                );

                                                                                tbx_Input_RechteckPreisProMeter.Clear();
                                                                                tbx_Input_RechteckPreisProMeter.Focus();
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                            }
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
                                                            tbx_Input_SonderFlanschbreite.Text = "";

                                                            tbx_Input_SonderFlanschbreite.Focus();
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
                                                        tbx_Input_SonderBreite.Text = "";

                                                        tbx_Input_SonderBreite.Focus();
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
                                                    tbx_Input_SonderStegbreite.Text = "";

                                                    tbx_Input_SonderStegbreite.Focus();
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
                                                tbx_Input_SonderFlanschbreite.Text = "";

                                                tbx_Input_SonderFlanschbreite.Focus();
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
                                            tbx_Input_SonderLaenge.Text = "";

                                            tbx_Input_SonderLaenge.Focus();
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
                                        tbx_Input_SonderHoehe.Text = "";

                                        tbx_Input_SonderHoehe.Focus();
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
                                    tbx_Input_SonderBreite.Text = "";

                                    tbx_Input_SonderBreite.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Preis: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_SonderPreisProMeter.Text = "";

                                tbx_Input_SonderPreisProMeter.Focus();
                            }
                        }
                    }
                    
                }
                lbl_Sonder_Fläche_Ergebnis.Visibility = Visibility.Visible;     //Schaltet Ergebnis Sichtbarkeit um
                lbl_Sonder_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Preis_Ergebnis.Visibility = Visibility.Visible;
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
                    if (tbx_Input_SonderPreisProMeter.Text == "")
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Fehler: Sie haben keinen Preis eingegeben. Bitte geben Sie einen Preis ein.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_SonderPreisProMeter.Focus();
                        }
                    }
                    if (tbx_Input_SonderPreisProMeter.Text != "")
                    {
                        if (Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double P))
                        {
                            if (Double.TryParse(tbx_Input_SonderBreite.Text, out double b))        //Kontrolle auf Buchstaben der Breite
                            {
                                if (Double.TryParse(tbx_Input_SonderHoehe.Text, out double h))     //Kontrolle auf Buchstaben der Höhe
                                {
                                    if (Double.TryParse(tbx_Input_SonderLaenge.Text, out double l))        //Kontrolle auf Buchstaben der Länge
                                    {
                                        if (Double.TryParse(tbx_Input_SonderStegbreite.Text, out double sb))       //Kontrolle auf Buchstaben der Stegbreite
                                        {
                                            if (Double.TryParse(tbx_Input_SonderFlanschbreite.Text, out double fb))     //Kontrolle auf Buchstaben der Flanschbreite
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
                                                                tbx_Input_SonderBreite.Text = "";

                                                                tbx_Input_SonderBreite.Focus();
                                                            }
                                                        }
                                                        if (Zeichenlaenge_b.Length <= 4)
                                                        {
                                                            if (Zeichenlaenge_h.Length > 4)        //Kontrolle der Zeichenlänge der Höhe
                                                            {
                                                                MessageBoxResult result;
                                                                result = MessageBox.Show("Höhe: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                                MessageBoxButton.OK,
                                                                MessageBoxImage.Error
                                                                );
                                                                if (result == MessageBoxResult.OK)
                                                                {
                                                                    tbx_Input_SonderHoehe.Text = "";

                                                                    tbx_Input_SonderHoehe.Focus();
                                                                }
                                                            }
                                                            if (Zeichenlaenge_h.Length <= 4)
                                                            {
                                                                if (Zeichenlaenge_l.Length > 4)        //Kontrolle der Zeichenlänge der Länge
                                                                {
                                                                    MessageBoxResult result;
                                                                    result = MessageBox.Show("Länge: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind vier Stellen.", "Eingabekontrolle",
                                                                    MessageBoxButton.OK,
                                                                    MessageBoxImage.Error
                                                                    );
                                                                    if (result == MessageBoxResult.OK)
                                                                    {
                                                                        tbx_Input_SonderLaenge.Text = "";

                                                                        tbx_Input_SonderLaenge.Focus();
                                                                    }
                                                                }
                                                                if (Zeichenlaenge_l.Length <= 4)
                                                                {
                                                                    if (Zeichenlaenge_fb.Length > 2)       //Kontrolle der Zeichenlänge der Flanschbreite
                                                                    {
                                                                        MessageBoxResult result;
                                                                        result = MessageBox.Show("Flanschbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                                        MessageBoxButton.OK,
                                                                        MessageBoxImage.Error
                                                                        );
                                                                        if (result == MessageBoxResult.OK)
                                                                        {
                                                                            tbx_Input_SonderFlanschbreite.Text = "";

                                                                            tbx_Input_SonderFlanschbreite.Focus();
                                                                        }
                                                                    }
                                                                    if (Zeichenlaenge_fb.Length <= 2)
                                                                    {
                                                                        if (Zeichenlaenge_sb.Length > 2)       //Kontrolle der Zeichenlänge der Stegbreite
                                                                        {
                                                                            MessageBoxResult result;
                                                                            result = MessageBox.Show("Stegbreite: Ihre Eingabe enthält zu viele Stellen. Maximal erlaubt sind zwei Stellen.", "Eingabekontrolle",
                                                                            MessageBoxButton.OK,
                                                                            MessageBoxImage.Error
                                                                            );
                                                                            if (result == MessageBoxResult.OK)
                                                                            {
                                                                                tbx_Input_SonderStegbreite.Text = "";

                                                                                tbx_Input_SonderStegbreite.Focus();
                                                                            }
                                                                        }
                                                                        if (Zeichenlaenge_sb.Length <= 2)
                                                                        {
                                                                            if (Double.TryParse(tbx_Input_SonderPreisProMeter.Text, out double pr)) // Erfolgreiche Konvertierung
                                                                            {
                                                                                string Zeichenlaenge_pr;
                                                                                Zeichenlaenge_pr = Convert.ToString(pr);


                                                                                if (Zeichenlaenge_pr.Length > 4)     //Kontrolle der Zahlenlänge der Breite
                                                                                {

                                                                                    MessageBoxResult result;
                                                                                    result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt vier Stellen.", "Eingabekontrolle",
                                                                                    MessageBoxButton.OK,
                                                                                    MessageBoxImage.Error
                                                                                    );
                                                                                    if (result == MessageBoxResult.OK)
                                                                                    {
                                                                                        tbx_Input_SonderPreisProMeter.Text = "";

                                                                                        tbx_Input_SonderPreisProMeter.Focus();
                                                                                    }

                                                                                }
                                                                                if (Zeichenlaenge_pr.Length <= 4)
                                                                                {
                                                                                    T_Profil_Berechnung(); // Aufruf der Berechnung
                                                                                }
                                                                            }
                                                                            else
                                                                            {
                                                                                MessageBox.Show("Fehler: Eingabe von Buchstaben nicht zulässig!", "Eingabekontrolle",
                                                                                MessageBoxButton.OK,
                                                                                MessageBoxImage.Error
                                                                                );

                                                                                tbx_Input_RechteckPreisProMeter.Clear();
                                                                                tbx_Input_RechteckPreisProMeter.Focus();
                                                                            }
                                                                        }
                                                                    }
                                                                }
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
                                                            tbx_Input_SonderFlanschbreite.Text = "";

                                                            tbx_Input_SonderFlanschbreite.Focus();
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
                                                        tbx_Input_SonderStegbreite.Text = "";

                                                        tbx_Input_SonderStegbreite.Focus();
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
                                                    tbx_Input_SonderFlanschbreite.Text = "";

                                                    tbx_Input_SonderFlanschbreite.Focus();
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
                                                tbx_Input_SonderStegbreite.Text = "";

                                                tbx_Input_SonderStegbreite.Focus();
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
                                            tbx_Input_SonderLaenge.Text = "";

                                            tbx_Input_SonderLaenge.Focus();
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
                                        tbx_Input_SonderHoehe.Text = "";

                                        tbx_Input_SonderHoehe.Focus();
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
                                    tbx_Input_SonderBreite.Text = "";

                                    tbx_Input_SonderBreite.Focus();
                                }
                            }
                        }
                        else
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Fehler: Sie haben einen oder mehrere Buchstaben eingegeben.", "Eingabekontrolle",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_SonderPreisProMeter.Text = "";

                                tbx_Input_SonderPreisProMeter.Focus();
                            }
                        }
                    }
                   
                }
                lbl_Sonder_Fläche_Ergebnis.Visibility = Visibility.Visible;     //Schaltet Ergebnis Sichtbarkeit um
                lbl_Sonder_Masse_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Volumen_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Schwerpunkt_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_X_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_FTM_Y_Ergebnis.Visibility = Visibility.Visible;
                lbl_Sonder_Preis_Ergebnis.Visibility = Visibility.Visible;

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

            if (CoB_Rechteck_WS.SelectedItem == Rechteck_WS_Stahl)
            {
                if (ChB_Bestellnummer_Rechteck.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: Flachstab EN 10058 - " + tbx_Input_RechteckBreite.Text + " x " + tbx_Input_RechteckHoehe.Text + " x " + tbx_Input_RechteckLaenge.Text);
                }

            }

            if (CoB_Rechteck_WS.SelectedItem == Rechteck_WS_Aluminium)
            {
                if (ChB_Bestellnummer_Rechteck.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: Flachstab - " + tbx_Input_RechteckBreite.Text + " x " + tbx_Input_RechteckHoehe.Text + " x " + tbx_Input_RechteckLaenge.Text);
                }

            }


        }

        private void btn_StartRechteckprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kontrolle_Rechteckprofil_hohl();

            if (ChB_Bestellnummer_Rechteck.IsChecked == true)
            {
                MessageBox.Show("Bestellnummer: Stahlrohr EN 10219 - " + tbx_Input_RechteckBreite.Text + " x " + tbx_Input_RechteckHoehe.Text + " x " + tbx_Input_Rechteck_hohl_Wall.Text + " x " + tbx_Input_RechteckLaenge.Text);
            }


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

            if (CoB_Kreis_WS.SelectedItem == Kreis_WS_Stahl)
            {
                if (ChB_Bestellnummer_Kreis.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: Rundstab DIN EN 10060 - " + tbx_Input_KreisDurchmesser.Text + " x " + tbx_Input_KreisLaenge.Text);
                }

            }

            if (CoB_Kreis_WS.SelectedItem == Kreis_WS_Aluminium)
            {
                if (ChB_Bestellnummer_Kreis.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: Rundstab - " + tbx_Input_KreisDurchmesser.Text + " x " + tbx_Input_KreisLaenge.Text);
                }
            }



        }

        private void btn_StartKreisprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kontrolle_Kreisprofil_hohl();   // Kontrolle der Eingabe

            if (ChB_Bestellnummer_Kreis.IsChecked == true)
            {
                MessageBox.Show("Bestellnummer: Rohrprofil Rund DIN EN 10220 - " + tbx_Input_KreisDurchmesser.Text + " x " + tbx_Input_Kreis_hohlWandstaerke.Text + " x " + tbx_Input_KreisLaenge.Text);
            }



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

            if (CoB_Sonder_WS.SelectedItem == Sonder_WS_Stahl)
            {
                if (ChB_Bestellnummer_Sonder.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: I-Profil DIN 1025 - IPE " + tbx_Input_SonderBreite.Text + " x " + tbx_Input_SonderLaenge.Text);
                }

            }

            if (CoB_Sonder_WS.SelectedItem == Sonder_WS_Aluminium)
            {
                if (ChB_Bestellnummer_Sonder.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: I-Profil - IPE " + tbx_Input_SonderBreite.Text + " x " + tbx_Input_SonderLaenge.Text);
                }
            }
        }

        private void btn_StartU_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kontrolle_U_Profil();

            if (CoB_Sonder_WS.SelectedItem == Sonder_WS_Stahl)
            {
                if (ChB_Bestellnummer_Sonder.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: U-Profil DIN 1026 - U" + tbx_Input_SonderHoehe.Text + " x " + tbx_Input_SonderLaenge.Text);
                }

            }

            if (CoB_Sonder_WS.SelectedItem == Sonder_WS_Aluminium)
            {
                if (ChB_Bestellnummer_Sonder.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: U-Profil - U" + tbx_Input_SonderHoehe.Text + " x " + tbx_Input_SonderLaenge.Text);
                }
            }
        }

        private void btn_StartT_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            Kontrolle_T_Profil();

            if (CoB_Sonder_WS.SelectedItem == Sonder_WS_Stahl)
            {
                if (ChB_Bestellnummer_Sonder.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: T-Profil EN 10055 - T" + tbx_Input_SonderHoehe.Text + " x " + tbx_Input_SonderLaenge.Text);
                }

            }

            if (CoB_Sonder_WS.SelectedItem == Sonder_WS_Aluminium)
            {
                if (ChB_Bestellnummer_Sonder.IsChecked == true)
                {
                    MessageBox.Show("Bestellnummer: T-Profil - T" + tbx_Input_SonderHoehe.Text + " x " + tbx_Input_SonderLaenge.Text);
                }
            }





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

        private void tbx_Input_SonderBreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Breite.Content = tbx_Input_SonderBreite.Text;
            lbl_U_Breite.Content = tbx_Input_SonderBreite.Text;
            lbl_T_Breite.Content = tbx_Input_SonderBreite.Text;

        }

        private void tbx_Input_SonderHoehe_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Hoehe.Content = tbx_Input_SonderHoehe.Text;
            lbl_U_Hoehe.Content = tbx_Input_SonderHoehe.Text;
            lbl_T_Hoehe.Content = tbx_Input_SonderHoehe.Text;
        }

        private void tbx_Input_SonderStegbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Stegbreite.Content = tbx_Input_SonderStegbreite.Text;
            lbl_U_Stegbreite.Content = tbx_Input_SonderStegbreite.Text;
            lbl_T_Stegbreite.Content = tbx_Input_SonderStegbreite.Text;
        }

        private void tbx_Input_SonderFlanschbreite_TextChanged(object sender, TextChangedEventArgs e)
        {
            lbl_IPEGrafik_Flanschbreite.Content = tbx_Input_SonderFlanschbreite.Text;
            lbl_U_Flanschbreite.Content = tbx_Input_SonderFlanschbreite.Text;
            lbl_T_Flanschbreite.Content = tbx_Input_SonderFlanschbreite.Text;
        }












        #endregion

        #endregion


    }
}
