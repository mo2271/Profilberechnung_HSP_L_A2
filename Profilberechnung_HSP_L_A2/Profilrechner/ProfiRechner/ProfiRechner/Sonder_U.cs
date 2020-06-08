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
    class Sonder_U
    {
        
        INFITF.Application CATIA_SonderU;
        MECMOD.PartDocument CATIA_SonderU_Part;
        MECMOD.Sketch CATIA_SonderU_2D;

        public bool CATIA_SonderU_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_SonderU = (INFITF.Application)catiaObject;

                return true;
            }
            catch (Exception)
            {
                return false;

            }
        }
        
        public double KlasseSonderUHoehe;
        public double KlasseSonderUBreite;
        public double KlasseSonderUFlanschbreite;
        public double KlasseSonderUStegbreite;
        public double KlasseSonderULaenge;

        public double setGeometrie(double local_SonderUHoehe, double local_SonderUBreite, double local_SonderUFlanschhoehe, double local_SonderUStegbreite, double local_SonderULaenge)
        {
            KlasseSonderUHoehe = local_SonderUHoehe;
            KlasseSonderUBreite = local_SonderUBreite;
            KlasseSonderUFlanschbreite = local_SonderUFlanschhoehe;
            KlasseSonderUStegbreite = local_SonderUStegbreite;
            KlasseSonderULaenge = local_SonderULaenge;
            return KlasseSonderUHoehe + KlasseSonderUBreite + KlasseSonderUStegbreite + KlasseSonderUFlanschbreite + KlasseSonderULaenge;
        }
        
        public void PartSonderU()
        {
            INFITF.Documents SonderU_Part = CATIA_SonderU.Documents;
            CATIA_SonderU_Part = SonderU_Part.Add("Part") as MECMOD.PartDocument;
        }

        public void SonderU_CreateSketch()
        {
            HybridBodies SonderU_HybridBodies = CATIA_SonderU_Part.Part.HybridBodies;
            HybridBody SonderU_HybridBody;

            try
            {
                SonderU_HybridBody = SonderU_HybridBodies.Item("Geometrisches Set.1");
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
            SonderU_HybridBody.set_Name("Querschnitt-Skizze");
            Sketches SonderU_Sketch = SonderU_HybridBody.HybridSketches;
            OriginElements SonderU_OriginElements = CATIA_SonderU_Part.Part.OriginElements;
            Reference SonderU_Reference = (Reference)SonderU_OriginElements.PlaneYZ;
            CATIA_SonderU_2D = SonderU_Sketch.Add(SonderU_Reference);

            ReferenzAchsensystem();

            CATIA_SonderU_Part.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_SonderU_2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void SonderU_DrawSketch(double SonderU_Breite, double SonderU_Hoehe, double Flanschbreite, double Stegbreite)
        {
            double b = SonderU_Breite;
            double h = SonderU_Hoehe;
            double f = Flanschbreite;
            double s = Stegbreite;
            
            double c;   // Hilfsmaß zur Bemaßung am abgeschrägten Flansch
            double alpha;

            if (h <= 300)
            {
                c = 0.5 * b;
            }
            else
            {
                c = 0.5 * (b - s);
            }

            if (h > 300)
            {
                alpha = Math.Atan(0.05);
            }
            else
            {
                alpha = Math.Atan(0.08);
            }

            double outerDelta = Math.Tan(alpha) * c;    // Berechnung des Versatzmaßes der Schrägenkonturpunkte
            double innerDelta = Math.Tan(alpha) * (b - c - s);

            CATIA_SonderU_2D.set_Name("U-Profil");

            Factory2D SonderU_Factory = CATIA_SonderU_2D.OpenEdition();

            // Definition der Geometrie
            Point2D Konturpunkt1 = SonderU_Factory.CreatePoint(0, 0);
            Point2D Konturpunkt2 = SonderU_Factory.CreatePoint(b, 0);
            Point2D Konturpunkt3 = SonderU_Factory.CreatePoint(b, (f - outerDelta));
            Point2D Konturpunkt4 = SonderU_Factory.CreatePoint(s, (f + innerDelta));
            Point2D Konturpunkt5 = SonderU_Factory.CreatePoint(s, (h - f - innerDelta));
            Point2D Konturpunkt6 = SonderU_Factory.CreatePoint(b, (h - f + outerDelta));
            Point2D Konturpunkt7 = SonderU_Factory.CreatePoint(b, h);
            Point2D Konturpunkt8 = SonderU_Factory.CreatePoint(0, h);

            CATIA_SonderU_2D.CloseEdition();

            CATIA_SonderU_Part.Part.Update();

        }
        public void SonderU_Extrusion(double SonderU_Laenge, double Flanschbreite)
        {
            double l = SonderU_Laenge;
            double f = Flanschbreite;
            double R1 = f;   // Definition der Eckenradien nach DIN 1026-1
            double R2 = R1 / 2;

            CATIA_SonderU_Part.Part.InWorkObject = CATIA_SonderU_Part.Part.MainBody;

            ShapeFactory SonderU_3D = (ShapeFactory)CATIA_SonderU_Part.Part.ShapeFactory;
            Pad SonderU_Pad = SonderU_3D.AddNewPad(CATIA_SonderU_2D, l);

             

            SonderU_Pad.set_Name("U-Profiltraeger");

            CATIA_SonderU_Part.Part.Update();
        } 
    }
}