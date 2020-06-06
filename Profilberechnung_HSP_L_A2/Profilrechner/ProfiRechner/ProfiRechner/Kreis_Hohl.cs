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
    class Kreis_Hohl
    {
        INFITF.Application CATIA_Kreis_hohl;
        MECMOD.PartDocument CATIA_Kreis_hohl_Part;
        MECMOD.Sketch CATIA_Kreis_hohl_2D;

        public bool CATIA_Kreis_hohl_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_Kreis_hohl = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

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

        public void PartKreis_hohl()
        {
            INFITF.Documents Kreis_hohl_Part = CATIA_Kreis_hohl.Documents;
            CATIA_Kreis_hohl_Part = Kreis_hohl_Part.Add("Part") as MECMOD.PartDocument;
        }

        public void Kreis_hohl_CreateSketch()
        {
            HybridBodies Kreis_hohl_HybridBodies = CATIA_Kreis_hohl_Part.Part.HybridBodies;
            HybridBody Kreis_hohl_HybridBody;

            try
            {
                Kreis_hohl_HybridBody = Kreis_hohl_HybridBodies.Item("Geometrisches Set.1");
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
            Kreis_hohl_HybridBody.set_Name("Querschnitt-Skizze");
            Sketches KreisSketch = Kreis_hohl_HybridBody.HybridSketches;
            OriginElements Kreis_hohl_OriginElements = CATIA_Kreis_hohl_Part.Part.OriginElements;
            Reference Kreis_hohl_Reference = (Reference)Kreis_hohl_OriginElements.PlaneYZ;
            CATIA_Kreis_hohl_2D = KreisSketch.Add(Kreis_hohl_Reference);

            ReferenzAchsensystem();

            CATIA_Kreis_hohl_Part.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_Kreis_hohl_2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void Kreis_hohl_DrawSketch(double KreisDurchmesser, double Wandstaerke)
        {
            double d = KreisDurchmesser;
            double t = Wandstaerke;
            double di = (d / 2) - t;

            CATIA_Kreis_hohl_2D.set_Name("Kreis-Hohlprofil");

            Factory2D Kreis_hohl_Factory = CATIA_Kreis_hohl_2D.OpenEdition();

            // Definition der Kreise
            Circle2D OuterCircle = Kreis_hohl_Factory.CreateCircle(0, 0, (d / 2), 0, 0);
            Circle2D InnerCircle = Kreis_hohl_Factory.CreateCircle(0, 0, di, 0, 0);

            CATIA_Kreis_hohl_2D.CloseEdition();

            CATIA_Kreis_hohl_Part.Part.Update();

        }
        public void Kreis_hohl_Extrusion(double Kreis_hohl_Laenge)
        {
            double l = Kreis_hohl_Laenge;

            CATIA_Kreis_hohl_Part.Part.InWorkObject = CATIA_Kreis_hohl_Part.Part.MainBody;

            ShapeFactory Kreis_hohl_3D = (ShapeFactory)CATIA_Kreis_hohl_Part.Part.ShapeFactory;
            Pad Kreis_hohl_Pad = Kreis_hohl_3D.AddNewPad(CATIA_Kreis_hohl_2D, l);

            Kreis_hohl_Pad.set_Name("Rohr");

            CATIA_Kreis_hohl_Part.Part.Update();
        }
    }
}
