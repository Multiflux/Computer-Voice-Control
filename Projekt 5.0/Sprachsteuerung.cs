using System.Text;
using System.Windows.Forms;
using System.Speech.Synthesis;
using System.Speech.Recognition;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Xml;
using System;
using System.Globalization;
using System.Collections.Generic;
using Microsoft.ProjectOxford.SpeechRecognition;

namespace Projekt_5._0
{
    /// <summary>
    /// Hier wird die Sprachsteuerung gestartet.
    /// </summary>
    public class Sprachsteuerung
    {


        public Sprachsteuerung(Form form, MicrophoneRecognitionClient micClient)
        {
            _form = form;
            bingClient = micClient;
        }

        private Form _form = null;
        private MicrophoneRecognitionClient bingClient = null;

        //static CultureInfo language = new CultureInfo("en-EN");
        public SpeechRecognitionEngine spracherkennung = new SpeechRecognitionEngine(new System.Globalization.CultureInfo("en-US"));

        private Dictionary<String, PluginInformations> loadedPlugins = new Dictionary<String, PluginInformations>();

        /// <summary>
        /// Methode um die Sprachsteuereung zu initialisieren
        /// </summary>
        public void spracherkennung_init()
        {

            if (Settings.Instance.spracherkennung_zum_ersten_mal_gestartet == false && Settings.Instance.spracherkennung_ein == false) //Falls das programm nicht zum ersten mal gestartet wird 
            {                                                                                      //und die spracherkennung nicht schon an ist, diese If schleife 
                spracherkennung.RecognizeAsync(RecognizeMode.Multiple);                            //dient der Sicherheit und stabilität 
                Settings.Instance.spracherkennung_ein = true;

            }

            if (Settings.Instance.spracherkennung_zum_ersten_mal_gestartet == true) // Falls das Programm zum ersten mal gestartet wird
            {


                try
                {
                    Create_Choices();
                    spracherkennung.SetInputToDefaultAudioDevice(); //Benutzt den standart mikrofon
                    spracherkennung.LoadGrammar(new Grammar(new GrammarBuilder(Settings.Instance.choices)));//Lädt eine neue Grammatik
                    spracherkennung.SpeechRecognized += new EventHandler<SpeechRecognizedEventArgs>(spracherkennung_SpeechRecognized); //Falls die Sprache Erkannt wurde
                    spracherkennung.SpeechRecognitionRejected += new EventHandler<SpeechRecognitionRejectedEventArgs>(spracherkennung_SpeechRecognizedRejected); //Falls die Sprache nicht erkannt wurde
                    spracherkennung.RecognizeAsync(RecognizeMode.Multiple); //Starte Spracherkennung von mehrere Wörter
                    Settings.Instance.spracherkennung_zum_ersten_mal_gestartet = false; //Setzte die Variable zum ersten Mal gestartet auf falsch 
                    Settings.Instance.spracherkennung_ein = true; // Setzte die Variable auf ein (Spracherkennung ist an)
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.ToString()); //Falls Fehler, anzeigen

                }
            }
        }

        

        private void spracherkennung_SpeechRecognized(object sender, SpeechRecognizedEventArgs e) //Methode Sprache wurde erkannt
        {
            Settings.Instance.speech = e.Result.Text; //Erkannten Text speichern
            darf_ich_zuhören(Settings.Instance.speech); //Setzt von Passivem auf Aktivem zuhören um und umgekehrt

            if (Settings.Instance.spracherkennung_ein == true)
            {
                if (Settings.Instance.spracherkennung_darf_ich_zuhoeren == true) 
                {
                    Console.WriteLine("Recognized: " + Settings.Instance.speech);
                    if (Settings.Instance.speech == "Start Dictation")
                    {
                        _form.Show();
                        _form.WindowState = FormWindowState.Normal;
                        Settings.Instance.spracherkennung_ein = false;
                        Thread.Sleep(1000);
                        bingClient.StartMicAndRecognition();
                    }
                    else
                    {
                        Check_Befehle(Settings.Instance.speech); //Führt die Dazugehörige Methode aus
                    }
                    
                }
            }
           
        }


        private void spracherkennung_SpeechRecognizedRejected(object sender, SpeechRecognitionRejectedEventArgs e) //Methode Sprache wurde NICHT erkannt
        {
            Settings.Instance.speech = e.Result.Text; //Erkannten Text speichern
            darf_ich_zuhören(Settings.Instance.speech); //Setzt von Passivem auf Aktivem zuhören um und umgekehrt
            if (Settings.Instance.spracherkennung_ein == true)
            {
                if (Settings.Instance.spracherkennung_darf_ich_zuhoeren == true) // Prüft ob Aktiv oder Passiv zugehört werden soll
                {
                    Console.WriteLine("Rejected: " + Settings.Instance.speech);
                    if (Settings.Instance.speech == "Start Dictation")
                    {
                        _form.Show();
                        _form.WindowState = FormWindowState.Normal;
                        Settings.Instance.spracherkennung_ein = false;
                        Thread.Sleep(1000);
                        bingClient.StartMicAndRecognition();
                    }
                    else
                    {
                        Check_Befehle(Settings.Instance.speech); //Führt die Dazugehörige Methode aus
                    }
                    
                }
            }
        }


        /// <summary>
        /// Prüft ob der erkannte Sprachbefehl existiert und führt die dazugehörige Methode aus
        /// </summary>
        /// <param name="wort">Sprachbefehl (bzw. erkanntes wort)</param>
        private void Check_Befehle(string wort)
        {
            foreach (var spb1 in Settings.Instance.tupl)
            {
                if (spb1.Item2 == wort)
                {
                    execute_void(spb1.Item1, spb1.Item3);
                }
            }

        }

        /// <summary>
        /// Methode um ein Plugin zu laden und eine Methode auszuführen
        /// </summary>
        /// <param name="PluginName">Pluginname (zB: Plugin1)</param>
        /// <param name="MethodenName">Methodenname (zB: öffne_google)</param>
        public void execute_void(string PluginName, string MethodenName)
        {
            try
            {
                //string[] path = PluginName.Split('/');
                //string pluginFileName = path[path.Length];
                PluginInformations plugin = null;
                Type typ = null;
                object obj = null;

                if (isPluginLoaded(PluginName))
                {
                    plugin = getLoadedPlugin(PluginName);
                }
                else
                {
                    plugin = new PluginInformations(PluginName);
                    loadedPlugins.Add(plugin.obj.ToString(), plugin);
                }

                typ = plugin.typ;
                obj = plugin.obj;

                // Besorge den zu ausführenden Methodenname
                MethodInfo myMethod = typ.GetMethod(MethodenName);


                // Führe die Methode aus
                myMethod.Invoke(obj, null);

                ////Console.WriteLine("Plugin: " + PluginName + "; MethodCalled: " + MethodenName);
                //Assembly assembly = Assembly.LoadFrom(PluginName);

                //// Die erste (bzw. nullte) klasse soll geladen werden
                //Type typ = assembly.GetTypes()[0];

                //// Erstelle Objekt
                //object obj = Activator.CreateInstance(typ);

                //// Besorge den zu ausführenden Methodenname
                //MethodInfo myMethod = typ.GetMethod(MethodenName);


                //// Führe die Methode aus
                //myMethod.Invoke(obj, null);

                //assembly = null;
                //typ = null;
                //obj = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private bool isPluginLoaded(string name)
        {
            foreach (KeyValuePair<String, PluginInformations> kvp in loadedPlugins)
            {
                if (name.EndsWith(kvp.Key))
                {
                    return true;
                }
            }
            return false;
        }

        private PluginInformations getLoadedPlugin(string name)
        {
            foreach (KeyValuePair<String, PluginInformations> kvp in loadedPlugins)
            {
                if (name.EndsWith(kvp.Key))
                {
                    return kvp.Value;
                }
            }
            return null;
        }

        private bool loadPlugin(String PluginPath)
        {
            bool success = false;
            return success;
        }

        /// <summary>
        /// Erstellt die Choices für die Grammatik der Spracherkennung. (Werden aus dem Array geladen)
        /// </summary>
        private void Create_Choices()
        {
            Settings.Instance.choices.Add(new string[] { "Start" });
            Settings.Instance.choices.Add(new string[] { "Stop" });
            Settings.Instance.choices.Add(new string[] { "Start Dictation" });
            foreach (var spb in Settings.Instance.tupl)
            {
                Settings.Instance.choices.Add(new string[] { spb.Item2 });
            }
        }

        /// <summary>
        /// Methode um das Aktive bzw. Passive zuhören einzuschalten
        /// </summary>
        /// <param name="sb2"></param>
        private void darf_ich_zuhören(string sb2)
        {
            if (Settings.Instance.speech == "Start")
            {
                Settings.Instance.spracherkennung_darf_ich_zuhoeren = true;
                Console.WriteLine("Now Listening");
            }
            if (Settings.Instance.speech == "Stop")
            {
                Settings.Instance.spracherkennung_darf_ich_zuhoeren = false;
                Console.WriteLine("No longer Listening");
            }
        }

        private class PluginInformations
        {

            public Assembly assembly;
            public Type typ;
            public object obj;

            public PluginInformations(String PluginName)
            {
                ////Console.WriteLine("Plugin: " + PluginName + "; MethodCalled: " + MethodenName);
                assembly = Assembly.LoadFrom(PluginName);

                // Die erste (bzw. nullte) klasse soll geladen werden
                typ = assembly.GetTypes()[0];
                // Erstelle Objekt
                 obj = Activator.CreateInstance(typ);
            }

        }
    }      
}


