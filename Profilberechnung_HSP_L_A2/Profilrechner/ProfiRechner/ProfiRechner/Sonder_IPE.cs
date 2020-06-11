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
using System.Net;

namespace ProfiRechner
{
    class Sonder_IPE
    {
        INFITF.Application CATIA_SonderIPE;
        MECMOD.PartDocument CATIA_SonderIPE_Part;
        MECMOD.Sketch CATIA_SonderIPE_2D;

        public bool CATIA_SonderIPE_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_SonderIPE = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }

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

        public void PartSonderIPE()
        {
            INFITF.Documents SonderIPE_Part = CATIA_SonderIPE.Documents;
            CATIA_SonderIPE_Part = SonderIPE_Part.Add("Part") as MECMOD.PartDocument;
        }

        public void SonderIPE_CreateSketch()
        {
            HybridBodies SonderIPE_HybridBodies = CATIA_SonderIPE_Part.Part.HybridBodies;
            HybridBody SonderIPE_HybridBody;

            try
            {
                SonderIPE_HybridBody = SonderIPE_HybridBodies.Item("Geometrisches Set.1");
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
            SonderIPE_HybridBody.set_Name("Querschnitt-Skizze");
            Sketches SonderIPE_Sketch = SonderIPE_HybridBody.HybridSketches;
            OriginElements SonderIPE_OriginElements = CATIA_SonderIPE_Part.Part.OriginElements;
            Reference SonderIPE_Reference = (Reference)SonderIPE_OriginElements.PlaneYZ;
            CATIA_SonderIPE_2D = SonderIPE_Sketch.Add(SonderIPE_Reference);

            ReferenzAchsensystem();

            CATIA_SonderIPE_Part.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_SonderIPE_2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void SonderIPE_DrawSketch(double SonderIPE_Breite, double SonderIPE_Hoehe, double Flanschbreite, double Stegbreite)
        {
            double b = SonderIPE_Breite;
            double h = SonderIPE_Hoehe;
            double f = Flanschbreite;
            double s = Stegbreite;
            double R = 2 * s;   // Definition des Eckenradius nach DIN 1025-2 und DIN 1025-5
            double bs = (b - s) / 2;

            CATIA_SonderIPE_2D.set_Name("I-Profil");

            Factory2D SonderIPE_Factory = CATIA_SonderIPE_2D.OpenEdition();

            // Definition der Geometrie

            // Definition der Radienmittelpunkte
            Point2D Mittelpunkt1 = SonderIPE_Factory.CreatePoint((bs - R), (f + R));
            Point2D Mittelpunkt2 = SonderIPE_Factory.CreatePoint((bs + s + R), (f + R));
            Point2D Mittelpunkt3 = SonderIPE_Factory.CreatePoint((bs + s + R), (h - f - R));
            Point2D Mittelpunkt4 = SonderIPE_Factory.CreatePoint((bs - R), (h - f - R));

            // Definition der Konturpunkte
            Point2D Konturpunkt1 = SonderIPE_Factory.CreatePoint(0, 0);
            Point2D Konturpunkt2 = SonderIPE_Factory.CreatePoint(b, 0);
            Point2D Konturpunkt3 = SonderIPE_Factory.CreatePoint(b, f);
            Point2D Konturpunkt4 = SonderIPE_Factory.CreatePoint((bs + s + R), f);
            Point2D Konturpunkt5 = SonderIPE_Factory.CreatePoint((bs + s), (f + R));
            Point2D Konturpunkt6 = SonderIPE_Factory.CreatePoint((bs + s), (h - f - R));
            Point2D Konturpunkt7 = SonderIPE_Factory.CreatePoint((bs + s + R), (h - f));
            Point2D Konturpunkt8 = SonderIPE_Factory.CreatePoint(b, (h - f));
            Point2D Konturpunkt9 = SonderIPE_Factory.CreatePoint(b, h);
            Point2D Konturpunkt10 = SonderIPE_Factory.CreatePoint(0, h);
            Point2D Konturpunkt11 = SonderIPE_Factory.CreatePoint(0, (h - f));
            Point2D Konturpunkt12 = SonderIPE_Factory.CreatePoint((bs - R), (h - f));
            Point2D Konturpunkt13 = SonderIPE_Factory.CreatePoint(bs, (h - f - R));
            Point2D Konturpunkt14 = SonderIPE_Factory.CreatePoint(bs, (f + R));
            Point2D Konturpunkt15 = SonderIPE_Factory.CreatePoint((bs - R), f);
            Point2D Konturpunkt16 = SonderIPE_Factory.CreatePoint(0, f);

            // Defintion der Linien
            Line2D Linie12 = SonderIPE_Factory.CreateLine(0, 0, b, 0);
            Linie12.StartPoint = Konturpunkt1;
            Linie12.EndPoint = Konturpunkt2;

            Line2D Linie23 = SonderIPE_Factory.CreateLine(b, 0, b, f);
            Linie23.StartPoint = Konturpunkt2;
            Linie23.EndPoint = Konturpunkt3;

            Line2D Linie34 = SonderIPE_Factory.CreateLine(b, f, (bs + s + R), f);
            Linie34.StartPoint = Konturpunkt3;
            Linie34.EndPoint = Konturpunkt4;

            Line2D Linie56 = SonderIPE_Factory.CreateLine((bs + s), (f + R), (bs + s), (h - f - R));
            Linie56.StartPoint = Konturpunkt5;
            Linie56.EndPoint = Konturpunkt6;

            Line2D Linie78 = SonderIPE_Factory.CreateLine((bs + s + R), (h - f), b, (h - f));
            Linie78.StartPoint = Konturpunkt7;
            Linie78.EndPoint = Konturpunkt8;

            Line2D Linie89 = SonderIPE_Factory.CreateLine(b, (h - f), b, h);
            Linie89.StartPoint = Konturpunkt8;
            Linie89.EndPoint = Konturpunkt9;

            Line2D Linie910 = SonderIPE_Factory.CreateLine(b, h, 0, h);
            Linie910.StartPoint = Konturpunkt9;
            Linie910.EndPoint = Konturpunkt10;

            Line2D Linie1011 = SonderIPE_Factory.CreateLine(0, h, 0, (h - f));
            Linie1011.StartPoint = Konturpunkt10;
            Linie1011.EndPoint = Konturpunkt11;

            Line2D Linie1112 = SonderIPE_Factory.CreateLine(0, (h - f), (bs - R), (h - f));
            Linie1112.StartPoint = Konturpunkt11;
            Linie1112.EndPoint = Konturpunkt12;

            Line2D Linie1314 = SonderIPE_Factory.CreateLine(bs, (h - f - R), bs, (f + R));
            Linie1314.StartPoint = Konturpunkt13;
            Linie1314.EndPoint = Konturpunkt14;

            Line2D Linie1516 = SonderIPE_Factory.CreateLine((bs - R), f, 0, f);
            Linie1516.StartPoint = Konturpunkt15;
            Linie1516.EndPoint = Konturpunkt16;

            Line2D Linie161 = SonderIPE_Factory.CreateLine(0, f, 0, 0);
            Linie161.StartPoint = Konturpunkt16;
            Linie161.EndPoint = Konturpunkt1;

            // Definition der Eckenverrundungen
            Circle2D Eckenverrundung1 = SonderIPE_Factory.CreateCircle((bs - R), (f + R), R, 0, 0);
            Eckenverrundung1.CenterPoint = Mittelpunkt1;
            Eckenverrundung1.StartPoint = Konturpunkt15;
            Eckenverrundung1.EndPoint = Konturpunkt14;

            Circle2D Eckenverrundung2 = SonderIPE_Factory.CreateCircle((bs + s + R), (f + R), R, 0, 0);
            Eckenverrundung2.CenterPoint = Mittelpunkt2;
            Eckenverrundung2.StartPoint = Konturpunkt5;
            Eckenverrundung2.EndPoint = Konturpunkt4;

            Circle2D Eckenverrundung3 = SonderIPE_Factory.CreateCircle((bs + s + R), (h - f - R), R, 0, 0);
            Eckenverrundung3.CenterPoint = Mittelpunkt3;
            Eckenverrundung3.StartPoint = Konturpunkt7;
            Eckenverrundung3.EndPoint = Konturpunkt6;

            Circle2D Eckenverrundung4 = SonderIPE_Factory.CreateCircle((bs - R), (h - f - R), R, 0, 0);
            Eckenverrundung4.CenterPoint = Mittelpunkt4;
            Eckenverrundung4.StartPoint = Konturpunkt13;
            Eckenverrundung4.EndPoint = Konturpunkt12;

            CATIA_SonderIPE_2D.CloseEdition();

            CATIA_SonderIPE_Part.Part.Update();

        }
        public void SonderIPE_Extrusion(double SonderIPE_Laenge)
        {
            double l = SonderIPE_Laenge;

            CATIA_SonderIPE_Part.Part.InWorkObject = CATIA_SonderIPE_Part.Part.MainBody;

            ShapeFactory SonderIPE_3D = (ShapeFactory)CATIA_SonderIPE_Part.Part.ShapeFactory;
            Pad SonderIPE_Pad = SonderIPE_3D.AddNewPad(CATIA_SonderIPE_2D, l);

            SonderIPE_Pad.set_Name("IPE-Traeger");

            CATIA_SonderIPE_Part.Part.Update();
        }
    }
}
