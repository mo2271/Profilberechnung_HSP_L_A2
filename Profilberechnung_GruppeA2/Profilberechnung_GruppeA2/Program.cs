using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace Profilberechnung_GruppeA2
{
    class Program
    {
        static void Rechteckprofil(ref double Dichte_spezifisch)
        {
            double Breite;
            double Hoehe;
            double Laenge;
            double Flaeche, Volumen;
            double Gewicht;
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

            Console.WriteLine("Flächeninhalt: " + Flaeche + " " + Masseinheit + "^2");
            Console.WriteLine("Volumen: " + Volumen + " " + Masseinheit + "^3");
            Console.WriteLine("Dichte: " + Dichte_spezifisch);
        }

        static double Werkstoff_Auswahl(ref string lower_Werkstoff)
        {
            double Dichte;

            if (lower_Werkstoff.Contains("st"))
            {
                Dichte = 7850d;
            }
            else if (lower_Werkstoff.Contains("al"))
            {
                Dichte = 2700d;
            }
            else
            {
                Console.WriteLine("Kein gültiger Werkstoff ausgewählt!");
                Dichte = 0d;
            }
            
            return Dichte;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Willkommen im Profilrechner!");
            
            Console.WriteLine("Bitte geben Sie zunächst den verwendeten Werkstoff an (St, Al):");
            string Werkstoff;
            Werkstoff = Convert.ToString(Console.ReadLine());
            string lower_Werkstoff = Werkstoff.ToLower();
            double Dichte_spezifisch = Werkstoff_Auswahl(ref lower_Werkstoff);

            string Eingabe;

            Console.WriteLine("Bitte geben Sie nun den zu berechnenden Profiltyp an: [1] Rechteck-Profil; [2]...");

            Eingabe = Convert.ToString(Console.ReadLine());
            if (Eingabe.Equals("1"))
            {  
                Rechteckprofil(ref Dichte_spezifisch);
            }
            else
            {
                Console.WriteLine("... noch nicht implementiert ...");
            }

            
            Console.WriteLine("Beliebige Taste zum Beenden drücken...");
            Console.ReadKey();
        }
    }
}
