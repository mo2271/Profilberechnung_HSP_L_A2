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
using System.Windows.Media.Animation;

namespace ProfiRechner
{
    class Sonder_T
    {
        
        INFITF.Application CATIA_SonderT;
        MECMOD.PartDocument CATIA_SonderT_Part;
        MECMOD.Sketch CATIA_SonderT_2D;

        public bool CATIA_SonderT_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_SonderT = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        
        public double KlasseSonderTHoehe;
        public double KlasseSonderTBreite;
        public double KlasseSonderTFlanschbreite;
        public double KlasseSonderTStegbreite;
        public double KlasseSonderTLaenge;

        public double setGeometrie(double local_SonderTHoehe, double local_SonderTBreite, double local_SonderTFlanschhoehe, double local_SonderTStegbreite, double local_SonderTLaenge)
        {
            KlasseSonderTHoehe = local_SonderTHoehe;
            KlasseSonderTBreite = local_SonderTBreite;
            KlasseSonderTFlanschbreite = local_SonderTFlanschhoehe;
            KlasseSonderTStegbreite = local_SonderTStegbreite;
            KlasseSonderTLaenge = local_SonderTLaenge;
            return KlasseSonderTHoehe + KlasseSonderTBreite + KlasseSonderTStegbreite + KlasseSonderTFlanschbreite + KlasseSonderTLaenge;
        }
        
        public void PartSonderT()
        {
            INFITF.Documents SonderT_Part = CATIA_SonderT.Documents;
            CATIA_SonderT_Part = SonderT_Part.Add("Part") as MECMOD.PartDocument;
        }

        public void SonderT_CreateSketch()
        {
            HybridBodies SonderT_HybridBodies = CATIA_SonderT_Part.Part.HybridBodies;
            HybridBody SonderT_HybridBody;

            try
            {
                SonderT_HybridBody = SonderT_HybridBodies.Item("Geometrisches Set.1");
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
            SonderT_HybridBody.set_Name("Querschnitt-Skizze");
            Sketches SonderT_Sketch = SonderT_HybridBody.HybridSketches;
            OriginElements SonderT_OriginElements = CATIA_SonderT_Part.Part.OriginElements;
            Reference SonderT_Reference = (Reference)SonderT_OriginElements.PlaneYZ;
            CATIA_SonderT_2D = SonderT_Sketch.Add(SonderT_Reference);

            ReferenzAchsensystem();

            CATIA_SonderT_Part.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_SonderT_2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void SonderT_DrawSketch(double SonderT_Breite, double SonderT_Hoehe, double Flanschbreite, double Stegbreite)
        {
            double b = SonderT_Breite;
            double h = SonderT_Hoehe;
            double f = Flanschbreite;
            double s = Stegbreite;

            double alpha = Math.Atan(0.02);

            double upperDelta = Math.Tan(alpha) * (b / 4);
            double lowerDelta = Math.Tan(alpha) * (h / 2);

            double delta = h - f + Math.Tan(alpha) * (b / 4);
            double phi = (b / 2) - (s / 2) + (Math.Tan(alpha) * (h / 2));

            double x = (delta - Math.Tan((Math.PI/2) - alpha) * phi) / (Math.Tan(alpha) - Math.Tan((Math.PI / 2) - alpha));
            double y = ((-1) * Math.Tan(alpha)) * x + delta;

            CATIA_SonderT_2D.set_Name("I-Profil");

            Factory2D SonderT_Factory = CATIA_SonderT_2D.OpenEdition();

            // Definition der Geometrie
            Point2D Konturpunkt1 = SonderT_Factory.CreatePoint(0, h);
            Point2D Konturpunkt2 = SonderT_Factory.CreatePoint(b, h);
            Point2D Konturpunkt3 = SonderT_Factory.CreatePoint(b, (h - f + upperDelta));
            Point2D Konturpunkt4 = SonderT_Factory.CreatePoint((b - x), y);
            Point2D Konturpunkt5 = SonderT_Factory.CreatePoint(((b / 2) + (s / 2) - (lowerDelta)), 0);
            Point2D Konturpunkt6 = SonderT_Factory.CreatePoint(((b / 2) - (s / 2) + (lowerDelta)), 0);
            Point2D Konturpunkt7 = SonderT_Factory.CreatePoint(x, y);
            Point2D Konturpunkt8 = SonderT_Factory.CreatePoint(0, (h - f + upperDelta));

            CATIA_SonderT_2D.CloseEdition();

            CATIA_SonderT_Part.Part.Update();

        }
        public void SonderT_Extrusion(double SonderT_Laenge, double SonderT_Flanschbreite)
        {
            double l = SonderT_Laenge;
            double f = SonderT_Flanschbreite;

            CATIA_SonderT_Part.Part.InWorkObject = CATIA_SonderT_Part.Part.MainBody;

            ShapeFactory SonderT_3D = (ShapeFactory)CATIA_SonderT_Part.Part.ShapeFactory;
            Pad SonderT_Pad = SonderT_3D.AddNewPad(CATIA_SonderT_2D, l);

            SonderT_Pad.set_Name("IPE-Traeger");

            CATIA_SonderT_Part.Part.Update();
        } 
    }
}