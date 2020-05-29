using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFITF;
using MECMOD;
using PARTITF;
using System.Security.Cryptography.X509Certificates;

namespace ProfiRechner
{
    class CATIAConnection
    {
        INFITF.Application CATIA_ProfilProfi;
        
        public bool CATIA_Run()
        {
            try
            {
                object catiaObject = System.Runtime.InteropServices.Marshal.GetActiveObject("CATIA.Application");
                CATIA_ProfilProfi = (INFITF.Application) catiaObject;

                

                return true;
            }
            catch (Exception)
            {
                

                return false;
            }
        }
    }
}
