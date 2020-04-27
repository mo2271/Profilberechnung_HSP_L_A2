using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profilberechnung_GruppeA2
{
    class Rechteck
    {
        public double Breite_Rechteck;
        public double Hoehe_Rechteck;
        public double Laenge_Rechteck;
        
        public double setGeometrie(double local_Breite, double local_Hoehe, double local_Laenge)
        {
            Breite_Rechteck = local_Breite;
            Hoehe_Rechteck = local_Hoehe;
            Laenge_Rechteck = local_Laenge;
            return Breite_Rechteck + Hoehe_Rechteck + Laenge_Rechteck;    
        }
    }
}
