using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfiRechner
{
    class Rechteck
    {
        public double KlasseRechteckBreite;
        public double KlasseRechteckHoehe;
        public double KlasseRechteckLaenge;

        public double setGeometrie(double local_RechteckBreite, double local_RechteckHoehe, double local_RechteckLaenge)
        {
            KlasseRechteckBreite = local_RechteckBreite;
            KlasseRechteckHoehe = local_RechteckHoehe;
            KlasseRechteckLaenge = local_RechteckLaenge;
            return KlasseRechteckBreite + KlasseRechteckHoehe + KlasseRechteckLaenge;
        }
    }
}
