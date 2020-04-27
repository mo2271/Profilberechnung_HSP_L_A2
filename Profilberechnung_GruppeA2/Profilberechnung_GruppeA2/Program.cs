using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profilberechnung_GruppeA2
{
    class Program
    {
        static void Rechteckprofil()
        {
            double Breite;
            double Hoehe;
            double Laenge;
            double Flaeche, Volumen;
            Console.WriteLine("Bitte geben Sie die Breite an:");
            Breite = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie die Hoehe an:");
            Hoehe = Convert.ToDouble(Console.ReadLine());

            Console.WriteLine("Bitte geben Sie die Länge des Profilstrangs an:");
            Laenge = Convert.ToDouble(Console.ReadLine());
            
            Rechteck R = new Rechteck();
            R.setGeometrie(Breite, Hoehe, Laenge);
            

            Flaeche = R.Breite_Rechteck * R.Hoehe_Rechteck;
            Volumen = R.Breite_Rechteck * R.Hoehe_Rechteck * R.Laenge_Rechteck;

            Console.WriteLine("Flächeninhalt: " + Flaeche);
            Console.WriteLine("Volumen: " + Volumen);
            Console.ReadKey();
        }
        static void Main(string[] args)
        {
            double Breite;
            double Hoehe;
            double Laenge;
            string Werkstoff;
            double Dichte;

            double Flaeche;
            double Volumen;
            double Gewicht;
            double FTM_X;
            double FTM_Y;

            string Eingabe;

            Console.WriteLine("Bitte geben Sie den zu berechnenden Profiltyp an: [1] Rechteck-Profil; [2]...");

            Eingabe = Convert.ToString(Console.ReadLine());
            if (Eingabe.Equals("1"))
            {
                Rechteckprofil();
            }
            else
            {
                Console.WriteLine("... noch nicht implementiert...");
            }

            Console.ReadKey();
        }
    }
}
