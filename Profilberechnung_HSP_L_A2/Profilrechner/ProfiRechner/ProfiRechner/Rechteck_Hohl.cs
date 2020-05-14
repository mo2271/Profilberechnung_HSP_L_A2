using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfiRechner
{
    class Rechteck_Hohl
    {
        public double KlasseRechteckHohlBreite;
        public double KlasseRechteckHohlHoehe;
        public double KlasseRechteckHohlLaenge;
        public double KlasseRechteckHohlWandstaerke;

        public double setGeometrie(double local_RechteckHohlBreite, double local_RechteckHohlHoehe, double local_RechteckHohlLaenge, double local_RechteckHohlWandstaerke)
        {
            KlasseRechteckHohlBreite = local_RechteckHohlBreite;
            KlasseRechteckHohlHoehe = local_RechteckHohlHoehe;
            KlasseRechteckHohlLaenge = local_RechteckHohlLaenge;
            KlasseRechteckHohlWandstaerke = local_RechteckHohlWandstaerke;
            return KlasseRechteckHohlBreite + KlasseRechteckHohlHoehe + KlasseRechteckHohlLaenge + KlasseRechteckHohlWandstaerke;
        }
    }
}
