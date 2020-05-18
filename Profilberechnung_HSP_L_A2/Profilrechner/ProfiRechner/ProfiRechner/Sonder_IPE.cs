using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProfiRechner
{
    class Sonder_IPE
    {
        public double KlasseSonderIPEHoehe;
        public double KlasseSonderIPEBreite;
        public double KlasseSonderIPEFlanschbreite;
        public double KlasseSonderIPEStegbreite;
        public double KlasseSonderIPELaenge;

        public double setGeometrie(double local_SonderIPEHoehe, double local_SonderIPEBreite, double local_SonderIPEFlanschhoehe, double local_SonderIPEStegbreite, double local_SonderIPELaenge)
        {
            KlasseSonderIPEHoehe = local_SonderIPEHoehe;
            KlasseSonderIPEBreite = local_SonderIPEBreite;
            KlasseSonderIPEFlanschbreite = local_SonderIPEFlanschhoehe;
            KlasseSonderIPEStegbreite = local_SonderIPEStegbreite;
            KlasseSonderIPELaenge = local_SonderIPELaenge;
            return KlasseSonderIPEHoehe + KlasseSonderIPEBreite + KlasseSonderIPEStegbreite + KlasseSonderIPEFlanschbreite + KlasseSonderIPELaenge;
        }
    }
}
