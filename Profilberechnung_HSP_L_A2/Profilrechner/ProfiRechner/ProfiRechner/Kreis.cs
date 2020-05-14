using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfiRechner
{
    class Kreis
    {
        public double KlasseKreisDurchmesser;
        public double KlasseKreisLaenge;

        public double setGeometrie(double local_KreisDurchmesser, double local_KreisLaenge)
        {
            KlasseKreisDurchmesser = local_KreisDurchmesser;
            KlasseKreisDurchmesser = local_KreisLaenge;
            return KlasseKreisDurchmesser + KlasseKreisLaenge;
        }
    }
}
