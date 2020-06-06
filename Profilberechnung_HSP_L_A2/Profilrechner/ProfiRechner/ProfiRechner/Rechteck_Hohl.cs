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
    class Rechteck_Hohl
    {
        INFITF.Application CATIA_Rechteck_hohl;
        MECMOD.PartDocument CATIA_Rechteck_hohl_Part;
        MECMOD.Sketch CATIA_Rechteck_hohl_2D;

        public bool CATIA_Rechteck_hohl_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_Rechteck_hohl = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

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

        public void PartRechteck_hohl()
        {
            INFITF.Documents Rechteck_hohl_Part = CATIA_Rechteck_hohl.Documents;
            CATIA_Rechteck_hohl_Part = Rechteck_hohl_Part.Add("Part") as MECMOD.PartDocument;
        }

        public void Rechteck_hohl_CreateSketch()
        {
            HybridBodies Rechteck_hohl_HybridBodies = CATIA_Rechteck_hohl_Part.Part.HybridBodies;
            HybridBody Rechteck_hohl_HybridBody;

            try
            {
                Rechteck_hohl_HybridBody = Rechteck_hohl_HybridBodies.Item("Geometrisches Set.1");
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
            Rechteck_hohl_HybridBody.set_Name("Querschnitt-Skizze");
            Sketches Rechteck_hohl_Sketch = Rechteck_hohl_HybridBody.HybridSketches;
            OriginElements Rechteck_hohl_OriginElements = CATIA_Rechteck_hohl_Part.Part.OriginElements;
            Reference Rechteck_hohl_Reference = (Reference)Rechteck_hohl_OriginElements.PlaneYZ;
            CATIA_Rechteck_hohl_2D = Rechteck_hohl_Sketch.Add(Rechteck_hohl_Reference);

            ReferenzAchsensystem();

            CATIA_Rechteck_hohl_Part.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_Rechteck_hohl_2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void Rechteck_hohl_DrawSketch(double Rechteck_hohl_Breite, double Rechteck_hohl_Hoehe, double Wandstaerke)
        {
            double b = Rechteck_hohl_Breite;
            double h = Rechteck_hohl_Hoehe;
            double t = Wandstaerke;
            double bi = b - 2 * t;
            double hi = h - 2 * t;
            double ra, ri;

            // Berechnung der Eckenradien nach DIN EN 10219-2:2006 für Hohlprofile
            if (t <= 6)
            {
                ra = 1.6 * t;
            }
            else if (t <= 10)
            {
                ra = 2 * t;
            }
            else if (t > 10)
            {
                ra = 2.4 * t;
            }
            else
            {
                ra = 2.4 * t;
            }

            ri = ra - t;

            CATIA_Rechteck_hohl_2D.set_Name("Kreis-Hohlprofil");

            Factory2D Rechteck_hohl_Factory = CATIA_Rechteck_hohl_2D.OpenEdition();

            // Definition der Radienmittelpunkte
            Point2D Mittelpunkt1 = Rechteck_hohl_Factory.CreatePoint(ra, ra);
            Point2D Mittelpunkt2 = Rechteck_hohl_Factory.CreatePoint((b - ra), ra);
            Point2D Mittelpunkt3 = Rechteck_hohl_Factory.CreatePoint((b - ra), (h - ra));
            Point2D Mittelpunkt4 = Rechteck_hohl_Factory.CreatePoint(ra, (h - ra));

            // Definition des äußeren Rechtecks
            Point2D Konturpunkt1 = Rechteck_hohl_Factory.CreatePoint(ra, 0);
            Point2D Konturpunkt2 = Rechteck_hohl_Factory.CreatePoint((b - ra), 0);
            Point2D Konturpunkt3 = Rechteck_hohl_Factory.CreatePoint(b, ra);
            Point2D Konturpunkt4 = Rechteck_hohl_Factory.CreatePoint(b, (h - ra));
            Point2D Konturpunkt5 = Rechteck_hohl_Factory.CreatePoint((b - ra), h);
            Point2D Konturpunkt6 = Rechteck_hohl_Factory.CreatePoint(ra, h);
            Point2D Konturpunkt7 = Rechteck_hohl_Factory.CreatePoint(0, (h - ra));
            Point2D Konturpunkt8 = Rechteck_hohl_Factory.CreatePoint(0, ra);

            Line2D Linie12 = Rechteck_hohl_Factory.CreateLine(ra, 0, (b - ra), 0);
            Linie12.StartPoint = Konturpunkt1;
            Linie12.EndPoint = Konturpunkt2;

            Line2D Linie34 = Rechteck_hohl_Factory.CreateLine(b, ra, b, (h - ra));
            Linie34.StartPoint = Konturpunkt3;
            Linie34.EndPoint = Konturpunkt4;

            Line2D Linie56 = Rechteck_hohl_Factory.CreateLine((b - ra), h, ra, h);
            Linie56.StartPoint = Konturpunkt5;
            Linie56.EndPoint = Konturpunkt6;

            Line2D Linie78 = Rechteck_hohl_Factory.CreateLine(0, (h - ra), 0, ra);
            Linie78.StartPoint = Konturpunkt7;
            Linie78.EndPoint = Konturpunkt8;

            Circle2D Eckenverrundung81 = Rechteck_hohl_Factory.CreateCircle(ra, ra, ra, 0, 0);
            Eckenverrundung81.CenterPoint = Mittelpunkt1;
            Eckenverrundung81.StartPoint = Konturpunkt8;
            Eckenverrundung81.EndPoint = Konturpunkt1;

            Circle2D Eckenverrundung23 = Rechteck_hohl_Factory.CreateCircle(0, 0, ra, 0, 0);
            Eckenverrundung23.CenterPoint = Mittelpunkt2;
            Eckenverrundung23.StartPoint = Konturpunkt2;
            Eckenverrundung23.EndPoint = Konturpunkt3;

            Circle2D Eckenverrundung45 = Rechteck_hohl_Factory.CreateCircle(0, 0, ra, 0, 0);
            Eckenverrundung45.CenterPoint = Mittelpunkt3;
            Eckenverrundung45.StartPoint = Konturpunkt4;
            Eckenverrundung45.EndPoint = Konturpunkt5;

            Circle2D Eckenverrundung67 = Rechteck_hohl_Factory.CreateCircle(0, 0, ra, 0, 0);
            Eckenverrundung67.CenterPoint = Mittelpunkt4;
            Eckenverrundung67.StartPoint = Konturpunkt6;
            Eckenverrundung67.EndPoint = Konturpunkt7;
            

            CATIA_Rechteck_hohl_2D.CloseEdition();

            CATIA_Rechteck_hohl_Part.Part.Update();

        }
        public void Rechteck_hohl_Extrusion(double Rechteck_hohl_Laenge)
        {
            double l = Rechteck_hohl_Laenge;

            CATIA_Rechteck_hohl_Part.Part.InWorkObject = CATIA_Rechteck_hohl_Part.Part.MainBody;

            ShapeFactory Rechteck_hohl_3D = (ShapeFactory)CATIA_Rechteck_hohl_Part.Part.ShapeFactory;
            Pad Rechteck_hohl_Pad = Rechteck_hohl_3D.AddNewPad(CATIA_Rechteck_hohl_2D, l);

            Rechteck_hohl_Pad.set_Name("Rohr");

            CATIA_Rechteck_hohl_Part.Part.Update();
        }
    }
}
