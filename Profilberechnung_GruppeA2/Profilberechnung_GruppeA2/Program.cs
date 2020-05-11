using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics.Eventing.Reader;
using System.Threading;

namespace Profilberechnung_GruppeA2
{
    class Program
    {
        static void Rechteckprofil(ref double Dichte)
        {
            double Breite;
            double Hoehe;
            double Laenge;
            double Flaeche, Volumen;

            double FTM_X;
            double FTM_Y;
            string Masseinheit;

            Console.WriteLine("Berechnung eines Rechteckprofils gestartet.");
            Console.WriteLine("Bitte geben Sie die Breite an:");
            Breite = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie die Hoehe an:");
            Hoehe = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie die Länge des Profilstrangs an:");
            Laenge = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie die verwendete Masseinheit an [mm]; [cm]; [m]:");
            Masseinheit = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Profil-Kennwerte werden berechnet...");

            Rechteck R = new Rechteck();
            R.setGeometrie(Breite, Hoehe, Laenge);


            Flaeche = R.Breite_Rechteck * R.Hoehe_Rechteck;
            Volumen = R.Breite_Rechteck * R.Hoehe_Rechteck * R.Laenge_Rechteck;

            FTM_X = (R.Breite_Rechteck * Math.Pow(R.Hoehe_Rechteck, 3)) / 12;   // Berechnung FTM um die X-Achse
            FTM_Y = (R.Hoehe_Rechteck * Math.Pow(R.Breite_Rechteck, 3)) / 12;   // Berechnung FTM um die Y-Achse



            Console.WriteLine("Flächeninhalt: " + Flaeche + " " + Masseinheit + "^2");
            Console.WriteLine("Volumen: " + Volumen + " " + Masseinheit + "^3");
            Console.WriteLine("Dichte: " + Dichte + " " + "kg/m^3");
            Gewichtsberechnung(Masseinheit, Dichte, Volumen);   // Ausgabe Gewicht in Unterprogramm 


            Console.WriteLine("Flächenträgheitsmoment um die x-Achse:" + FTM_X + " " + Masseinheit + "^4");
            Console.WriteLine("Flächenträgheitsmoment um die y-Achse:" + FTM_Y + " " + Masseinheit + "^4");

        }

        static double Werkstoff_Auswahl()
        {
        Werkstoffabfrage:
            Console.WriteLine("Bitte geben Sie zunächst den verwendeten Werkstoff an (St, Al):");
            string Werkstoff;
            Werkstoff = Convert.ToString(Console.ReadLine());
            string lower_Werkstoff = Werkstoff.ToLower();

            double Dichte;

            // Startet die Überprüfung der Werkstoffeingabe.
            if (lower_Werkstoff.Contains("st"))
            {
                Console.WriteLine("Werkstoff STAHL gewählt.");
                Dichte = 7850d;
            }
            else if (lower_Werkstoff.Contains("al"))
            {
                Console.WriteLine("Werkstoff ALUMINIUM gewählt.");

                Dichte = 2700d;
            }
            else
            {
                Console.WriteLine("Kein gültiger Werkstoff ausgewählt, bitte geben Sie einen gültigen Werkstoff ein!");

                goto Werkstoffabfrage;  // Werkstoffabfrage wird erneut durchgeführt. 
            }

            return Dichte;
        }

        static void Gewichtsberechnung(string Masseinheit, double Dichte, double Volumen)
        {
            double Gewicht;

            if (Masseinheit == "mm")
            {
                Gewicht = (Dichte / 1000 / 1000 / 1000) * Volumen;

                Console.WriteLine("Gewicht: " + Gewicht + " " + "kg");
            }
            else if (Masseinheit == "cm")
            {
                Gewicht = (Dichte / 1000 / 1000) * Volumen;

                Console.WriteLine("Gewicht: " + Gewicht + " " + "kg");
            }
            else    // Masseinheit == "m"
            {
                Gewicht = Dichte * Volumen;

                Console.WriteLine("Gewicht: " + Gewicht + " " + "kg");
            }

        }

        static void Main(string[] args)
        {
            string neustart;        // Abfrage nach weiterer Berechnung über MessageBox
            neustart = "Yes";

            while (neustart == "Yes")
            {
                Console.Clear();
                Console.WriteLine("Willkommen im Profilrechner!");

                double Dichte = Werkstoff_Auswahl();    // Startet die Werkstoffabfrage.

                // Startet die Auswahl des zu berechnenden Profils.
                Console.WriteLine("Bitte geben Sie nun den zu berechnenden Profiltyp an: [1] Rechteck-Profil; [2]...");
                string Eingabe;
                Eingabe = Convert.ToString(Console.ReadLine());
                if (Eingabe.Equals("1"))
                {
                    Rechteckprofil(ref Dichte);
                }
                else
                {
                    Console.WriteLine("... noch nicht implementiert ...");
                }

                MessageBoxResult result;
                result = MessageBox.Show("Soll eine weitere Berechnung durchgeführt werden?", "Berechnung wurde durchgeführt",
               MessageBoxButton.YesNo,
               MessageBoxImage.Question
               );

                neustart = Convert.ToString(result);
            }

            Console.WriteLine("Beliebige Taste zum Beenden drücken...");
            Console.ReadKey();
            // .gitignore Test
            // .suo ignore Test
            // .suo delete
            // .gitignore edit
            // Erstmal die Brille entgraten
        }
    }
}
