using System;
using System.Collections.Generic;
using System.Linq;
using System.Speech.Recognition;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_5._0
{
    /// <summary>
    /// Diverse Setting eigenschaften.
    /// </summary>
      class Settings
        {
            public bool spracherkennung_ein = false;
            public bool spracherkennung_darf_ich_zuhoeren = false;
            public bool spracherkennung_zum_ersten_mal_gestartet = true;

            public Choices choices = new Choices();
            public string speech;

            public List<string> pluginListe = new List<string>();
            public List<Tuple<string, string, string>> tupl = new List<Tuple<string, string, string>>();

            #region singleton
            
            private Settings()
            {
            }

            
            private static Settings _instance;

            
            public static Settings Instance
            {
                get
                {
                    if (_instance == null)
                    {
                        _instance = new Settings();
                    }
                    return _instance;
                }
            }


            #endregion
        }
    
}
