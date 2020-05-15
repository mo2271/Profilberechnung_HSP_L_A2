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

namespace ProfiRechner
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
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
            Rechteck_Hohl_Flaeche = RH.KlasseRechteckHohlBreite * RH.KlasseRechteckHohlHoehe - (RH.KlasseRechteckHohlBreite - 2* RH.KlasseRechteckHohlWandstaerke) *(RH.KlasseRechteckHohlHoehe - 2 * RH.KlasseRechteckHohlWandstaerke);
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
            K.setGeometrie(KreisDurchmesser,KreisLaenge);

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
            Kreis_Hohl_Flaeche = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 2)-Math.Pow((KH.KlasseKreisHohlDurchmesser-2* KH.KlasseKreisHohlWandstaerke), 2)) * Math.PI / 4;
            Kreis_Hohl_Volumen = Kreis_Hohl_Flaeche * KH.KlasseKreisHohlLaenge;
            Kreis_Hohl_Masse = Kreis_Hohl_Volumen * KreisWSDichte;
            Kreis_Hohl_FTM_X = (Math.Pow(KH.KlasseKreisHohlDurchmesser, 4)-Math.Pow((KH.KlasseKreisHohlDurchmesser-2*KH.KlasseKreisHohlWandstaerke),4)) * Math.PI / 64;
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

            // Berechnungen
            Sonder_IPE_Flaeche = SIPE.KlasseSonderIPEBreite * SIPE.KlasseSonderIPEHoehe - ((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite)*(SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite));
            Sonder_IPE_Volumen = Sonder_IPE_Flaeche * SIPE.KlasseSonderIPELaenge;
            Sonder_IPE_Masse = Sonder_IPE_Volumen * SonderWSDichte;
            Sonder_IPE_FTM_X = SIPE.KlasseSonderIPEBreite * Math.Pow(SIPE.KlasseSonderIPEHoehe, 3) / 12 - (SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) * Math.Pow((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite), 3) / 12;
            Sonder_IPE_FTM_Y = SIPE.KlasseSonderIPEHoehe * Math.Pow(SIPE.KlasseSonderIPEBreite, 3) / 12 - 2 * ((SIPE.KlasseSonderIPEHoehe - 2 * SIPE.KlasseSonderIPEFlanschbreite) * Math.Pow((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite), 3) / 12 + Math.Pow((SIPE.KlasseSonderIPEStegbreite + SIPE.KlasseSonderIPEBreite / 4), 2) + (SIPE.KlasseSonderIPEHoehe - SIPE.KlasseSonderIPEFlanschbreite) * ((SIPE.KlasseSonderIPEBreite - SIPE.KlasseSonderIPEStegbreite) / 2));
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
            lbl_Rechteck_Fläche_Ergebnis.Content = Sonder_IPE_Flaeche_String;
            lbl_Rechteck_Volumen_Ergebnis.Content = Sonder_IPE_Volumen_String;
            lbl_Rechteck_Masse_Ergebnis.Content = Sonder_IPE_Masse_String;
            lbl_Rechteck_FTM_X_Ergebnis.Content = Sonder_IPE_FTM_X_String;
            lbl_Rechteck_FTM_Y_Ergebnis.Content = Sonder_IPE_FTM_Y_String;
            lbl_Rechteck_Schwerpunkt_Ergebnis.Content = Sonder_IPE_SWP_String;
        }

        #endregion

        public void Kontrolle_Rechteckprofil()     //Kontrolle der Eingaben 
        {
            
            
            String Einheit = Convert.ToString(CoB_Rechteck_Auswahl_Einheit.SelectionBoxItem);


            if (Einheit == "")
            {
                MessageBoxResult result;
                result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

            }
            else
            {
                Double.TryParse(tbx_Input_RechteckBreite.Text, out double b);   // Eingaben in Double umwandeln
                Double.TryParse(tbx_Input_RechteckHoehe.Text, out double h);
                Double.TryParse(tbx_Input_RechteckLaenge.Text, out double l);

                string Zeichenlaenge_b;
                string Zeichenlaenge_h;
                string Zeichenlaenge_l;

                Zeichenlaenge_b = Convert.ToString(b);      //Umformung der Eingabe in einen String
                Zeichenlaenge_h = Convert.ToString(h);
                Zeichenlaenge_l = Convert.ToString(l);

                if (Zeichenlaenge_b.Length > 4)
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
                else if (Zeichenlaenge_h.Length > 4)
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
                else if (Zeichenlaenge_l.Length > 4)
                {
                    MessageBoxResult result;
                    result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "WichtigeFrage",
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

            
        }
        public void Kontrolle_Rechteckprofil_hohl()     //Kontrolle der Eingaben
        {
            String Einheit = Convert.ToString(CoB_Rechteck_Auswahl_Einheit.SelectionBoxItem);

            if (Einheit == "")
            {
                MessageBoxResult result;
                result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

            }
            else
            {
                Double.TryParse(tbx_Input_RechteckBreite.Text, out double b);   // Eingaben in Double umwandeln
                Double.TryParse(tbx_Input_RechteckHoehe.Text, out double h);
                Double.TryParse(tbx_Input_RechteckLaenge.Text, out double l);
                Double.TryParse(tbx_Input_Rechteck_hohl_Wall.Text, out double w);

                string Zeichenlaenge_b;
                string Zeichenlaenge_h;
                string Zeichenlaenge_l;
                string Zeichenlaenge_w;



                Zeichenlaenge_b = Convert.ToString(b);      //Umformung der Eingabe in einen String
                Zeichenlaenge_h = Convert.ToString(h);
                Zeichenlaenge_l = Convert.ToString(l);
                Zeichenlaenge_w = Convert.ToString(w);

                if (w < (b / 2))
                {
                    if (w < (h / 2))
                    {
                        if (Zeichenlaenge_b.Length > 4)
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
                        else if (Zeichenlaenge_h.Length > 4)
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
                        else if (Zeichenlaenge_l.Length > 4)
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Länge: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "WichtigeFrage",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_RechteckLaenge.Text = "";

                                tbx_Input_RechteckLaenge.Focus();
                            }
                        }
                        else if (Zeichenlaenge_w.Length > 2)
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Wandstärke: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "WichtigeFrage",
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
        }
        public void Kontrolle_Kreisprofil()
        {
            
            
            String Einheit = Convert.ToString(CoB_Kreis_Auswahl_Einheit.SelectionBoxItem);

            if (Einheit == "")
            {
                MessageBoxResult result;
                result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
            }
            else
            {
                Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double D);
                Double.TryParse(tbx_Input_KreisLaenge.Text, out double l);
                string Zeichenlaenge_Durchmesser;
                string Zeichenlaenge_l;

                Zeichenlaenge_Durchmesser = Convert.ToString(D);
                Zeichenlaenge_l = Convert.ToString(l);


                if (Zeichenlaenge_Durchmesser.Length > 4)
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
                else if (Zeichenlaenge_l.Length > 4)
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
            }
        }
        public void Kontrolle_Kreisprofil_hohl()
        {
            String Einheit = Convert.ToString(CoB_Kreis_Auswahl_Einheit.SelectionBoxItem);

            if (Einheit == "")
            {
                MessageBoxResult result;
                result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
            }
            else
            {
                Double.TryParse(tbx_Input_KreisDurchmesser.Text, out double D);
                Double.TryParse(tbx_Input_Kreis_hohlWandstaerke.Text, out double w);
                Double.TryParse(tbx_Input_KreisLaenge.Text, out double l);

                double Durchmesser;
                double Wandstaerke;
                string Zeichenlaenge_D;
                string Zeichenlaenge_w;
                string Zeichenlaenge_l;


                Durchmesser = D;
                Wandstaerke = w;

                if ((D / 2) > w)
                {
                    Zeichenlaenge_D = Convert.ToString(D);
                    Zeichenlaenge_w = Convert.ToString(w);
                    Zeichenlaenge_l = Convert.ToString(l);

                    if (Zeichenlaenge_D.Length > 4)
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
                    else if (Zeichenlaenge_w.Length > 4)
                    {
                        MessageBoxResult result;
                        result = MessageBox.Show("Wandstärke: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "Eingabekontrolle",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error
                        );
                        if (result == MessageBoxResult.OK)
                        {
                            tbx_Input_Kreis_hohlWandstaerke.Text = "";

                            tbx_Input_Kreis_hohlWandstaerke.Focus();
                        }
                    }
                    else if (Zeichenlaenge_l.Length > 4)
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


        }
        public void Kontrolle_I_Profil()
        {
            String Einheit = Convert.ToString(CoB_Sonder_Auswahl_Einheit.SelectionBoxItem);

            if (Einheit == "")          //Beginn Einheitenkontrolle        
            {
                MessageBoxResult result;
                result = MessageBox.Show("Einheit: Sie haben keine Einheit ausgewählt. Bitte wählen Sie eine Einheit aus.", "Eingabekontrolle",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );

            }
            else
            {
                // Kontrolle der eingegebenen Zahlen
                Double.TryParse(tbx_Input_IPEBreite.Text, out double B);
                Double.TryParse(tbx_Input_IPEHoehe.Text, out double h);
                Double.TryParse(tbx_Input_IPELaenge.Text, out double l);
                Double.TryParse(tbx_Input_IPEFlanschbreite.Text, out double b);
                Double.TryParse(tbx_Input_IPEStegbreite.Text, out double sb);

                double Breite;
                double Hoehe;
                double Flanschbreite;
                double Stegbreite;

                Breite = B;
                Hoehe = h;
                Flanschbreite = b;
                Stegbreite = sb;

                if (Stegbreite < Breite)
                {
                    if (Flanschbreite < (Hoehe / 2))
                    {
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

                        if (Zeichenlaenge_B.Length > 4)
                        {
                            MessageBoxResult result;
                            result = MessageBox.Show("Breite: Sie haben eine zu lange Zahl eingetragen, die maximale Länge beträgt 4 Stellen.", "WichtigeFrage",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error
                            );
                            if (result == MessageBoxResult.OK)
                            {
                                tbx_Input_IPEBreite.Text = "";

                                tbx_Input_IPEBreite.Focus();
                            }
                        }
                        else if (Zeichenlaenge_h.Length > 4)
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
                        else if (Zeichenlaenge_l.Length > 4)
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
                        else if (Zeichenlaenge_b.Length > 2)
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
                        else if (Zeichenlaenge_sb.Length > 2)
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
            
            Kontrolle_Rechteckprofil();
            Rechteckprofil_Berechnung();    // Aufruf der Rechteckprofil-Berechnung
        }

        private void btn_StartRechteckprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            
            Kontrolle_Rechteckprofil_hohl();
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
            
            Kontrolle_Kreisprofil();    //Kontrolle der Eingaben
            Kreisprofil_Berechnung();   // Startet die Kreisprofil-Berechnung
        }

        private void btn_StartKreisprofil_hohl_Berechnung_Click(object sender, RoutedEventArgs e)
        {
            
            Kontrolle_Kreisprofil_hohl();   // Kontrolle der Eingabe
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
            
            Kontrolle_I_Profil();
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
