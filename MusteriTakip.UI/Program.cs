﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MusteriTakip.UI
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            try
            {
                // Create a new object, representing the German culture.  
                CultureInfo culture = CultureInfo.CreateSpecificCulture("tr");

                // The following line provides localization for the application's user interface.  
                Thread.CurrentThread.CurrentUICulture = culture;

                // The following line provides localization for data formats.  
                Thread.CurrentThread.CurrentCulture = culture;

                // Set this culture as the default culture for all threads in this application.  
                // Note: The following properties are supported in the .NET Framework 4.5+ 
                CultureInfo.DefaultThreadCurrentCulture = culture;
                CultureInfo.DefaultThreadCurrentUICulture = culture;

                System.Threading.Thread.CurrentThread.CurrentCulture = new System.Globalization.CultureInfo("tr-TR");
                System.Threading.Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo("tr-TR");

                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                DevExpress.Skins.SkinManager.EnableFormSkins();

                DevExpress.XtraEditors.WindowsFormsSettings.DefaultFont = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular);
                CheckFiles();
                Application.Run(new Musteriler());
            }
            catch (Exception)
            {
                
            }
        }

        private static void CheckFiles()
        {
            string[] Dirs = new string[]
            {
               Properties.Settings.Default.DBDir
            };
            foreach (string dir in Dirs)
            {
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
            }
            string[] Files = new string[]
            {
                Dirs[0] + "\\Customers.json"
            };
            foreach (string fname in Files)
            {
                if (!File.Exists(fname))
                {
                    File.Create(fname).Close();
                    File.WriteAllText(fname, "[]");
                }
            }

        }
    }
}
