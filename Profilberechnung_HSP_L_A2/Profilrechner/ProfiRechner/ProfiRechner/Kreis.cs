using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFITF;
using MECMOD;
using PARTITF;
using System.Data.SqlTypes;

namespace ProfiRechner
{
    class Kreis
    {

        INFITF.Application CATIA_Kreis;
        MECMOD.PartDocument CATIA_KreisPart;
        MECMOD.Sketch CATIA_Kreis2D;

        public bool CATIA_Kreis_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_Kreis = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        public double KlasseKreisDurchmesser;
        public double KlasseKreisLaenge;

        public double setGeometrie(double local_KreisDurchmesser, double local_KreisLaenge)
        {
            KlasseKreisDurchmesser = local_KreisDurchmesser;
            KlasseKreisLaenge = local_KreisLaenge;
            return KlasseKreisDurchmesser + KlasseKreisLaenge;
        }

        public void PartKreis()
        {
            INFITF.Documents KreisPart = CATIA_Kreis.Documents;
            CATIA_KreisPart = KreisPart.Add("Part") as MECMOD.PartDocument;
        }

        public void Kreis_CreateSketch()
        {
            HybridBodies KreisHybridBodies = CATIA_KreisPart.Part.HybridBodies;
            HybridBody KreisHybridBody;

            try
            {
                KreisHybridBody = KreisHybridBodies.Item("Geometrisches Set.1");
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
            KreisHybridBody.set_Name("Querschnitt-Skizze");
            Sketches KreisSketch = KreisHybridBody.HybridSketches;
            OriginElements KreisOriginElements = CATIA_KreisPart.Part.OriginElements;
            Reference KreisReference = (Reference)KreisOriginElements.PlaneYZ;
            CATIA_Kreis2D = KreisSketch.Add(KreisReference);

            ReferenzAchsensystem();

            CATIA_KreisPart.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_Kreis2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void Kreis_DrawSketch(double KreisDurchmesser)
        {
            double d = KreisDurchmesser;

            CATIA_Kreis2D.set_Name("Kreisprofil");

            Factory2D KreisFactory = CATIA_Kreis2D.OpenEdition();

            // Definition des Kreises
            Circle2D KreisSketch = KreisFactory.CreateCircle(0, 0, (d / 2), 0, 0);

            CATIA_Kreis2D.CloseEdition();

            CATIA_KreisPart.Part.Update();

        }
        public void KreisExtrusion(double KreisLaenge)
        {
            double l = KreisLaenge;

            CATIA_KreisPart.Part.InWorkObject = CATIA_KreisPart.Part.MainBody;

            ShapeFactory Kreis3D = (ShapeFactory)CATIA_KreisPart.Part.ShapeFactory;
            Pad KreisPad = Kreis3D.AddNewPad(CATIA_Kreis2D, l);

            KreisPad.set_Name("Rundstab");

            CATIA_KreisPart.Part.Update();
        }
    }
}
