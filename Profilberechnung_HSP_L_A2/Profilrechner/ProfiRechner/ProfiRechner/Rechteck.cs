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
using System.IO;


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

            ReferenzAchsensystem();

            CATIA_RechteckPart.Part.Update();
        }

        private void ReferenzAchsensystem()
        {
            object[] ReferenceArray = new object[] {0.0, 0.0, 0.0,
                                                    0.0, 1.0, 0.0,
                                                    0.0, 0.0, 1.0};
            CATIA_Rechteck2D.SetAbsoluteAxisData(ReferenceArray);
        }

        public void Rechteck_DrawSketch(double RechteckBreite, double RechteckHoehe)
        {
            double b = RechteckBreite;
            double h = RechteckHoehe;
            
            CATIA_Rechteck2D.set_Name("Rechteckprofil");

            Factory2D RechteckFactory = CATIA_Rechteck2D.OpenEdition();

            // Definition der Eckpunkte
            Point2D Rechteck_Eckpunkt1 = RechteckFactory.CreatePoint(0, 0);
            Point2D Rechteck_Eckpunkt2 = RechteckFactory.CreatePoint(b, 0);
            Point2D Rechteck_Eckpunkt3 = RechteckFactory.CreatePoint(b, h);
            Point2D Rechteck_Eckpunkt4 = RechteckFactory.CreatePoint(0, h);

            // Definition der Linien
            Line2D RechteckLinie1 = RechteckFactory.CreateLine(0, 0, b, 0);
            RechteckLinie1.StartPoint = Rechteck_Eckpunkt1;
            RechteckLinie1.EndPoint = Rechteck_Eckpunkt2;

            Line2D RechteckLinie2 = RechteckFactory.CreateLine(b, 0, b, h);
            RechteckLinie2.StartPoint = Rechteck_Eckpunkt2;
            RechteckLinie2.EndPoint = Rechteck_Eckpunkt3;

            Line2D RechteckLinie3 = RechteckFactory.CreateLine(b, h, 0, h);
            RechteckLinie3.StartPoint = Rechteck_Eckpunkt3;
            RechteckLinie3.EndPoint = Rechteck_Eckpunkt4;

            Line2D RechteckLinie4 = RechteckFactory.CreateLine(0, 0, 0, h);
            RechteckLinie4.StartPoint = Rechteck_Eckpunkt1;
            RechteckLinie4.EndPoint = Rechteck_Eckpunkt4;

            CATIA_Rechteck2D.CloseEdition();

            CATIA_RechteckPart.Part.Update();
            
        }
        public void RechteckExtrusion(double RechteckLaenge)
        {
            double l = RechteckLaenge;

            CATIA_RechteckPart.Part.InWorkObject = CATIA_RechteckPart.Part.MainBody;

            ShapeFactory Rechteck3D = (ShapeFactory)CATIA_RechteckPart.Part.ShapeFactory;
            Pad RechteckPad = Rechteck3D.AddNewPad(CATIA_Rechteck2D, l);
         
            RechteckPad.set_Name("Rechteckbalken");

            CATIA_RechteckPart.Part.Update();
        }

        public void SaveRechteckPart(string CATPart_Name, string STEP_Name, bool CATPart_Checked, bool STEP_Checked)
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
                    CATIA_Rechteck.ActiveDocument.SaveAs(PARTName);

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
                    CATIA_Rechteck.ActiveDocument.ExportData(STEPName, "stp");

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
