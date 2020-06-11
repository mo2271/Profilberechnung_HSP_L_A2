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

            Line2D Linie12 = SonderU_Factory.CreateLine(0, 0, b, 0);
            Linie12.StartPoint = Konturpunkt1;
            Linie12.EndPoint = Konturpunkt2;

            Line2D Linie23 = SonderU_Factory.CreateLine(b, 0, b, (f - outerDelta));
            Linie23.StartPoint = Konturpunkt2;
            Linie23.EndPoint = Konturpunkt3;

            Line2D Linie34 = SonderU_Factory.CreateLine(b, (f - outerDelta), s, (f + innerDelta));
            Linie34.StartPoint = Konturpunkt3;
            Linie34.EndPoint = Konturpunkt4;

            Line2D Linie45 = SonderU_Factory.CreateLine(s, (f + innerDelta), s, (h - f - innerDelta));
            Linie45.StartPoint = Konturpunkt4;
            Linie45.EndPoint = Konturpunkt5;

            Line2D Linie56 = SonderU_Factory.CreateLine(s, (h - f - innerDelta), b, (h - f + outerDelta));
            Linie56.StartPoint = Konturpunkt5;
            Linie56.EndPoint = Konturpunkt6;

            Line2D Linie67 = SonderU_Factory.CreateLine(b, (h - f + outerDelta), b, h);
            Linie67.StartPoint = Konturpunkt6;
            Linie67.EndPoint = Konturpunkt7;

            Line2D Linie78 = SonderU_Factory.CreateLine(b, h, 0, h);
            Linie78.StartPoint = Konturpunkt7;
            Linie78.EndPoint = Konturpunkt8;

            Line2D Linie81 = SonderU_Factory.CreateLine(0, h, 0, 0);
            Linie81.StartPoint = Konturpunkt8;
            Linie81.EndPoint = Konturpunkt1;

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

            // Kantenverrundung #1 für R1
            Reference Referenz11_R1 = CATIA_SonderU_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung1_R1 = SonderU_3D.AddNewEdgeFilletWithConstantRadius(Referenz11_R1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R1);
            Reference Referenz21_R1 = CATIA_SonderU_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;4)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;3)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderU_Pad);
            Verrundung1_R1.AddObjectToFillet(Referenz21_R1);
            Verrundung1_R1.set_Name("Verrundung1_R1 = " + R1);
            
            // Kantenverrundung #2 für R1
            Reference Referenz12_R1 = CATIA_SonderU_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung2_R1 = SonderU_3D.AddNewEdgeFilletWithConstantRadius(Referenz12_R1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R1);
            Reference Referenz22_R1 = CATIA_SonderU_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;5)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;4)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderU_Pad);
            Verrundung2_R1.AddObjectToFillet(Referenz22_R1);
            Verrundung2_R1.set_Name("Verrundung2_R1 = " + R1);

            // Kantenverrundung #1 für R2
            Reference Referenz11_R2 = CATIA_SonderU_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung1_R2 = SonderU_3D.AddNewEdgeFilletWithConstantRadius(Referenz11_R2, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R2);
            Reference Referenz21_R2 = CATIA_SonderU_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;3)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;2)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderU_Pad);
            Verrundung1_R2.AddObjectToFillet(Referenz21_R2);
            Verrundung1_R2.set_Name("Verrundung1_R2 = " + R2);

            // Kantenverrundung #2 für R2
            Reference Referenz12_R2 = CATIA_SonderU_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung2_R2 = SonderU_3D.AddNewEdgeFilletWithConstantRadius(Referenz12_R2, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R2);
            Reference Referenz22_R2 = CATIA_SonderU_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;6)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;5)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderU_Pad);
            Verrundung2_R2.AddObjectToFillet(Referenz22_R2);
            Verrundung2_R2.set_Name("Verrundung2_R2 = " + R2);

            SonderU_Pad.set_Name("U-Profiltraeger");

            CATIA_SonderU_Part.Part.Update();
        } 
    }
}