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
            Rechteck R = new Rechteck();


        }
        static void Main(string[] args)
        {
            float Breite;
            float Hoehe;
            float Laenge;
            string Werkstoff;
            double Dichte;

            float Flaeche;
            float Volumen;
            float Gewicht;
            float FTM_X;
            float FTM_Y;

            string Eingabe;

            Console.WriteLine("Bitte geben Sie den zu berechnenden Profiltyp an: [1] Rechteck-Profil; [2]...");

            Eingabe = Convert.ToString(Console.ReadLine());
            if (Eingabe.Equals(1))
            {
                Rechteckprofil();
            }
        }
    }
}
