using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFITF;
using MECMOD;
using PARTITF;

namespace ProfiRechner
{
    class Rechteck
    {
        INFITF.Application CATIA_Rechteck;
        MECMOD.PartDocument CATIA_RechteckPart;
        MECMOD.Sketch CATIA_Rechteck2D;

        public bool CATIA_Rechteck_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_Rechteck = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        

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

        public void PartRechteck()
        {
            INFITF.Documents RechteckPart = CATIA_Rechteck.Documents;
            CATIA_RechteckPart = RechteckPart.Add("Part") as MECMOD.PartDocument;
        }

        public void Rechteck_CreateSketch()
        {
            HybridBodies RechteckHybridBodies = CATIA_RechteckPart.Part.HybridBodies;
            HybridBody RechteckHybridBody;

            try
            {
                RechteckHybridBody = RechteckHybridBodies.Item("Geometrisches Set.1");
            }
            catch (Exception)
            {
                MessageBoxResult result;
                result = MessageBox.Show("CATIA wird nicht ausgeführt!", "Fehler",
                MessageBoxButton.OK,
                MessageBoxImage.Error
                );
                return;
            }
            RechteckHybridBody.set_Name("Querschnitt-Skizze");
            Sketches RechteckSketch = RechteckHybridBody.HybridSketches;
            OriginElements RechteckOriginElements = CATIA_RechteckPart.Part.OriginElements;
            Reference RechteckReference = (Reference)RechteckOriginElements.PlaneYZ;
            CATIA_Rechteck2D = RechteckSketch.Add(RechteckReference);
        }

    }
}
