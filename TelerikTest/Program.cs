using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using FoxLearn.License;

namespace TelerikTest
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // LICENSE CHECK
            var id = ComputerInfo.GetComputerId();
            KeyManager km = new KeyManager(id);
            LicenseInfo lic = new LicenseInfo();
            //Get license information from license file
            int value = km.LoadSuretyFile(string.Format(@"{0}\Key.lic", Application.StartupPath), ref lic);
            string productKey = lic.ProductKey;
            //Check valid
            if (km.ValidKey(ref productKey))
            {
                Application.Run(new RadForm1());
            }
            else
            {
                Application.Run(new Registration());
            }
            //END LICENSE CHECK
        }
    }
}
