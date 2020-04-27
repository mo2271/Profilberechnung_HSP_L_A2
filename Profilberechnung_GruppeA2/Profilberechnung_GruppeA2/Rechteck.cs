using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Profilberechnung_GruppeA2
{
    class Rechteck
    {
        private float Breite;
        private float Hoehe;

        public float setBreite(float local_Breite)
        {
            Breite = local_Breite;
            return Breite;
        }

        public float setHoehe(float local_Hoehe)
        {
            Hoehe = local_Hoehe;
            return Hoehe;
        }
    }
}
