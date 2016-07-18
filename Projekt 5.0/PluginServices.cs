using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using iPlugin;
using System.Xml;
using System.Xml.Schema;

namespace Projekt_5._0
{
    public class PluginServices
    {

        /// <summary> 
        /// Konstruktor, beim erstellen dieser Klasse soll die Funktion array füllen ausgeführt werden.
        /// Dies dient dazu, im zielordner die XML dateien auszulesen und die Kommandos (Pluginname, Sprachbefehle und Funktionname)
        /// in ein Array zu packen um sie dann dort weiterzuverwenden
        /// </summary>

        public PluginServices()
        {
            fill_array(get_Xml_List());
        }

        /// <summary>
        /// Dieser bool Prüft ob das eingegebene Plugin korrekt aufgebaut ist (Ob es von dem iPlugin erbt).
        /// </summary>
        /// <param name="PluginType">Plugin-Typ</param>
        /// <param name="pPath">Pfad des Plugins</param>
        bool IsPluginGood(string pPath, Type PluginType)
        {
            try
            {
                Assembly assembly = Assembly.LoadFrom(pPath); //Lädt die Assembly aus dem Angegebenen Pfad.

                foreach (Type type in assembly.GetTypes()) //Foreach Schleife die jeden typ aus der Assembly folgendes prüft.
                {
                    if (type.IsPublic) // Ruft einen Wert ab, der angibt, ob der Typ als öffentlich deklariert ist. 
                    {
                        if (!type.IsAbstract)  //nur Assemblys verwenden die nicht Abstrakt sind
                        {
                            return true;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Plugin konnte nicht geladen werde. Fehler :\n" + ex.ToString());
            }
            return false;
        }

        /// <summary>
        /// Füllt das Array mit den Kommandos (Pluginname, Sprachbefehle, Funktionsname).
        /// </summary>
        /// <param name="XmlListe">Liste der XMLs aus der die Befehle entnommen werden.</param>
        public void fill_array(String[] XmlListe)
        {
            Settings.Instance.tupl.Clear(); //Leert das Aktuelle Array.

            foreach (string aktuelleXml in XmlListe) //Foreach-Schleife die Prüft ob die XMLs aus der XML-list passen und dann 
                                                     // die Kommandos speichert.
            {
                if (isXmlGood(aktuelleXml) == true) //Prüft ob die XML passt
                {
                    string mm_sp;
                    string mm_mb;
                    string mm_pl = "";
                    bool check = false;
                    XmlTextReader reader = new XmlTextReader(aktuelleXml);
                    reader.Read();

                    while (reader.Read()) //Liest die XML Datei aus
                    {

                        switch (reader.NodeType)
                        {
                            // Wenn es ein Element gibt : Prüfen ob es ein Attribut der form eines Plugin pfades
                            // Sprachbefehl bzw. ein Methodenbefehl und fügt diese dann in das Array ein, wenn das
                            // plugin gültig ist.
                            case XmlNodeType.Element:
                                if (reader.HasAttributes == true)
                                {
                                    string file = reader.GetAttribute(0);
                                    check = IsPluginGood(file, typeof(iPlugin.iPlugin));
                                    if (check == true)
                                    {
                                        mm_pl = file.Replace('\\', '/');
                                    }

                                }
                                if (reader.Name == "Sprachbefehl" && check == true)
                                {
                                    try
                                    {
                                        reader.MoveToContent();
                                        mm_sp = reader.ReadString();
                                        reader.ReadToFollowing("Methodebefehl");
                                        reader.MoveToContent();
                                        mm_mb = reader.ReadString();
                                        Settings.Instance.tupl.Add(Tuple.Create(mm_pl, mm_sp, mm_mb));
                                    }
                                    catch (Exception ex)
                                    {
                                        MessageBox.Show(ex.ToString());
                                    }
                                }


                                break;



                        }

                    }
                    reader.Close();


                }
            }
        }


        /// <summary>
        /// Erstellt ein Array von den XML dateien die sich in dem Ordner "C:/Windows/Temp/Spracherkennung" befinden
        /// </summary
        /// <returns>gibt das Array zurück</returns>
        public String[] get_Xml_List()
        {
            String[] Xmls = Directory.GetFiles("C:/Users/Raphael/Desktop/Projekt_5.0/Plugin1", "*.xml");
            return Xmls;
        }

        /// <summary>
        /// Dieser Bool sollte prüfen ob die XML datei dem XMLSchema entspricht, was uns aber nicht gelungen ist.
        /// </summary>
        /// <param name="infilename"></param>
        /// <returns></returns>
        private bool isXmlGood(String infilename)
        {
            ////this function will validate the schema file (xsd)
            //XmlSchema myschema;
            //m_Success = true; //make sure to reset the success var
            //XmlReader sr = new XmlReader(infilename);
            //try
            //{
            //    myschema = XmlSchema.Read(sr,
            //        new ValidationEventHandler(ValidationCallBack));
            //    //This compile statement is what ususally catches the errors
            //    myschema.Compile(new ValidationEventHandler(ValidationCallBack));
            //}
            //catch
            //{
            //}
            //finally
            //{
            //    sr.Close();
            //}
            return m_Success;
        }

        bool m_Success = true;
       
        private void ValidationCallBack(Object sender, ValidationEventArgs args)
        {
           m_Success = false; //Validation failed
           MessageBox.Show("Validation error: " + args.Message);
        }
        

    }
}