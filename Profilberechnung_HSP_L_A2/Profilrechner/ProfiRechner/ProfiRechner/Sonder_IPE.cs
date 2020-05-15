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
        public double KlasseSonderIPEStegbreite;
        public double KlasseSonderIPEFlanschbreite;
        public double KlasseSonderIPELaenge;

        public double setGeometrie(double local_SonderIPEHoehe, double local_SonderIPEBreite, double local_SonderIPEStegbreite, double local_SonderIPEFlanschhoehe, double local_SonderIPELaenge)
        {
            KlasseSonderIPEHoehe = local_SonderIPEHoehe;
            KlasseSonderIPEBreite = local_SonderIPEBreite;
            KlasseSonderIPEStegbreite = local_SonderIPEStegbreite;
            KlasseSonderIPEFlanschbreite = local_SonderIPEFlanschhoehe;
            KlasseSonderIPELaenge = local_SonderIPELaenge;
            return KlasseSonderIPEHoehe + KlasseSonderIPEBreite + KlasseSonderIPEStegbreite + KlasseSonderIPEFlanschbreite + KlasseSonderIPELaenge;
        }
    }
}
