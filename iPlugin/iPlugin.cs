using System;
using System.Collections.Generic;

namespace iPlugin
{

    /// <summary>
    /// Dieses Interface iPlugin dient als Basis für die alle folgenden Plugins. Alle Plugins der Spracherkennung erben
    /// von diesem Interface die Eigenschaften Name und Beschreibung, und müssen darauffolgend mindestens einen Namen und
    /// eine Beschreibung besitzen. Ansonsten ist der Entwickler frei jede funktion in seinem Plugin zu implementieren.
    /// Jedes Plugin muss eine XML datei besitzen, die dem hier vorhandenen XMLSchema entspricht, wo PluginName, Sprachbefehle 
    /// und die dazugehörigen Funktionsnamen.
    /// </summary>

    public interface iPlugin
    {

        /// <summary>
        /// Name des Plugins
        /// </summary>
        String Name { get; set; }

        /// <summary>
        /// Beschreibung des Plugins
        /// </summary>
        String Description { get; set; }

    }
}
