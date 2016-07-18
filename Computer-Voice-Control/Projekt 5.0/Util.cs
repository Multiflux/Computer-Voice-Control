using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace Projekt_5._0
{
    /// <summary>
    /// Diese Klasse ermöglicht das automatische Starten der Spracherkennung beim Start von Windows. 
    /// </summary>
    public class Util
    {
        private const string RUN_LOCATION = @"Software\Microsoft\Windows\CurrentVersion\Run";

        /// <summary>
        /// Sets the autostart value for the assembly.
        /// </summary>
        /// <param name="keyName">Registry Key Name</param>
        /// <param name="assemblyLocation">Assembly location (e.g. Assembly.GetExecutingAssembly().Location)</param>
        public static void SetAutoStart(string keyName, string assemblyLocation)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.SetValue(keyName, assemblyLocation);
        }

        /// <summary>
        /// Prüft ob der Autostart gesetzt ist oder nicht 
        /// </summary>
        /// <param name="keyName">Registry Key Name</param>
        /// <param name="assemblyLocation">Assembly Pfad</param>
        public static bool IsAutoStartEnabled(string keyName, string assemblyLocation)
        {
            RegistryKey key = Registry.CurrentUser.OpenSubKey(RUN_LOCATION);
            if (key == null)
            {
                return false;

            }
                string value = (string)key.GetValue(keyName);
            if (value == null)
            {
                return false;
            }

            return (value == assemblyLocation);
                
        }

        /// <summary>
        /// Entfernt den Autostart-Wert des Programms
        /// </summary>
        /// <param name="keyName">Registry Key Name</param>
        public static void UnSetAutoStart(string keyName)
        {
            RegistryKey key = Registry.CurrentUser.CreateSubKey(RUN_LOCATION);
            key.DeleteValue(keyName);
        }
    }
}
