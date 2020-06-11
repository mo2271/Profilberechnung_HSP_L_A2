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
using System.IO;

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

            CATIA_SonderT_2D.set_Name("T-Profil");

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

            Line2D Linie12 = SonderT_Factory.CreateLine(0, h, b, h);
            Linie12.StartPoint = Konturpunkt1;
            Linie12.EndPoint = Konturpunkt2;

            Line2D Linie23 = SonderT_Factory.CreateLine(b, h, b, (h - f + upperDelta));
            Linie23.StartPoint = Konturpunkt2;
            Linie23.EndPoint = Konturpunkt3;

            Line2D Linie34 = SonderT_Factory.CreateLine(b, (h - f + upperDelta), (b - x), y);
            Linie34.StartPoint = Konturpunkt3;
            Linie34.EndPoint = Konturpunkt4;

            Line2D Linie45 = SonderT_Factory.CreateLine((b - x), y, ((b / 2) + (s / 2) - (lowerDelta)), 0);
            Linie45.StartPoint = Konturpunkt4;
            Linie45.EndPoint = Konturpunkt5;

            Line2D Linie56 = SonderT_Factory.CreateLine(((b / 2) + (s / 2) - (lowerDelta)), 0, ((b / 2) - (s / 2) + (lowerDelta)), 0);
            Linie56.StartPoint = Konturpunkt5;
            Linie56.EndPoint = Konturpunkt6;

            Line2D Linie67 = SonderT_Factory.CreateLine(((b / 2) - (s / 2) + (lowerDelta)), 0, x, y);
            Linie67.StartPoint = Konturpunkt6;
            Linie67.EndPoint = Konturpunkt7;

            Line2D Linie78 = SonderT_Factory.CreateLine(x, y, 0, (h - f + upperDelta));
            Linie78.StartPoint = Konturpunkt7;
            Linie78.EndPoint = Konturpunkt8;

            Line2D Linie81 = SonderT_Factory.CreateLine(0, (h - f + upperDelta), 0, h);
            Linie81.StartPoint = Konturpunkt8;
            Linie81.EndPoint = Konturpunkt1;

            CATIA_SonderT_2D.CloseEdition();

            CATIA_SonderT_Part.Part.Update();

        }
        public void SonderT_Extrusion(double SonderT_Laenge, double SonderT_Flanschbreite, double SonderT_Stegbreite)
        {
            double l = SonderT_Laenge;
            double f = SonderT_Flanschbreite;
            double s = SonderT_Stegbreite;

            double R1 = f;
            double R2 = s / 2;
            double R3 = s / 4;

            CATIA_SonderT_Part.Part.InWorkObject = CATIA_SonderT_Part.Part.MainBody;

            ShapeFactory SonderT_3D = (ShapeFactory)CATIA_SonderT_Part.Part.ShapeFactory;
            Pad SonderT_Pad = SonderT_3D.AddNewPad(CATIA_SonderT_2D, l);

            // Kantenverrundung #1 für R1
            Reference Referenz11_R1 = CATIA_SonderT_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung1_R1 = SonderT_3D.AddNewEdgeFilletWithConstantRadius(Referenz11_R1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R1);
            Reference Referenz21_R1 = CATIA_SonderT_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;6)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;5)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderT_Pad);
            Verrundung1_R1.AddObjectToFillet(Referenz21_R1);
            Verrundung1_R1.set_Name("Verrundung1_R1 = " + R1);

            // Kantenverrundung #2 für R1
            Reference Referenz12_R1 = CATIA_SonderT_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung2_R1 = SonderT_3D.AddNewEdgeFilletWithConstantRadius(Referenz12_R1, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R1);
            Reference Referenz22_R1 = CATIA_SonderT_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;3)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;2)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderT_Pad);
            Verrundung2_R1.AddObjectToFillet(Referenz22_R1);
            Verrundung2_R1.set_Name("Verrundung2_R1 = " + R1);

            // Kantenverrundung #1 für R2
            Reference Referenz11_R2 = CATIA_SonderT_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung1_R2 = SonderT_3D.AddNewEdgeFilletWithConstantRadius(Referenz11_R2, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R2);
            Reference Referenz21_R2 = CATIA_SonderT_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;7)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;6)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderT_Pad);
            Verrundung1_R2.AddObjectToFillet(Referenz21_R2);
            Verrundung1_R2.set_Name("Verrundung1_R2 = " + R2);

            // Kantenverrundung #2 für R2
            Reference Referenz12_R2 = CATIA_SonderT_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung2_R2 = SonderT_3D.AddNewEdgeFilletWithConstantRadius(Referenz12_R2, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R2);
            Reference Referenz22_R2 = CATIA_SonderT_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;2)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;1)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderT_Pad);
            Verrundung2_R2.AddObjectToFillet(Referenz22_R2);
            Verrundung2_R2.set_Name("Verrundung2_R2 = " + R2);

            // Kantenverrundung #1 für R3
            Reference Referenz11_R3 = CATIA_SonderT_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung1_R3 = SonderT_3D.AddNewEdgeFilletWithConstantRadius(Referenz11_R3, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R3);
            Reference Referenz21_R3 = CATIA_SonderT_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;5)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;4)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderT_Pad);
            Verrundung1_R3.AddObjectToFillet(Referenz21_R3);
            Verrundung1_R3.set_Name("Verrundung1_R3 = " + R3);

            // Kantenverrundung #2 für R3
            Reference Referenz12_R3 = CATIA_SonderT_Part.Part.CreateReferenceFromName("");
            ConstRadEdgeFillet Verrundung2_R3 = SonderT_3D.AddNewEdgeFilletWithConstantRadius(Referenz12_R3, CatFilletEdgePropagation.catTangencyFilletEdgePropagation, R3);
            Reference Referenz22_R3 = CATIA_SonderT_Part.Part.CreateReferenceFromBRepName("REdge:(Edge:(Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;4)));None:();Cf11:());Face:(Brp:(Pad.1;0:(Brp:(Sketch.1;3)));None:();Cf11:());None:(Limits1:();Limits2:());Cf11:());WithTemporaryBody;WithoutBuildError;WithSelectingFeatureSupport;MFBRepVersion_CXR15)", SonderT_Pad);
            Verrundung2_R3.AddObjectToFillet(Referenz22_R3);
            Verrundung2_R3.set_Name("Verrundung2_R3 = " + R3);

            SonderT_Pad.set_Name("T-Traeger");

            CATIA_SonderT_Part.Part.Update();
        }

        public void SaveTPart(string CATPart_Name, string STEP_Name, bool CATPart_Checked, bool STEP_Checked)
        {
            string filename_CATPart = CATPart_Name;
            string filename_STEP = STEP_Name;

            // Lege die Dateien auf dem Desktop ab
            string PARTName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), filename_CATPart + ".CATPart");
            string STEPName = Path.Combine(Environment.GetFolderPath(System.Environment.SpecialFolder.Desktop), filename_STEP);

            if (CATPart_Checked)
            {
                // Speichern als .CATPart
                try
                {
                    CATIA_SonderT.ActiveDocument.SaveAs(PARTName);

                    MessageBox.Show("Datei " + filename_CATPart + ".CATPart wurde auf dem Desktop abgelegt.", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Export der .CATPart-Datei fehlgeschlagen!", "Fehler",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }

            }

            if (STEP_Checked)
            {
                try
                {
                    // Speichern als .stp (STEP)
                    CATIA_SonderT.ActiveDocument.ExportData(STEPName, "stp");

                    MessageBox.Show("Datei " + filename_STEP + ".stp wurde auf dem Desktop abgelegt.", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception)
                {
                    MessageBox.Show("Export der .stp-Datei fehlgeschlagen!", "Fehler",
                        MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}