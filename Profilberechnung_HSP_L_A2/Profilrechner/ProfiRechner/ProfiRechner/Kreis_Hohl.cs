using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfiRechner
{
    class Kreis_Hohl
    {
        public double KlasseKreisHohlDurchmesser;
        public double KlasseKreisHohlLaenge;
        public double KlasseKreisHohlWandstaerke;

        public double setGeometrie(double local_KreisHohlDurchmesser, double local_KreisHohlLaenge, double local_KreisHohlWandstaerke)
        {
            KlasseKreisHohlDurchmesser = local_KreisHohlDurchmesser;
            KlasseKreisHohlLaenge = local_KreisHohlLaenge;
            KlasseKreisHohlWandstaerke = local_KreisHohlWandstaerke;
            return KlasseKreisHohlDurchmesser + KlasseKreisHohlLaenge + KlasseKreisHohlWandstaerke;
        }
    }
}
