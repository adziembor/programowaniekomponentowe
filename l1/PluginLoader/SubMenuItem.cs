using System;
using System.Collections.Generic;

namespace PluginBase
{
    [Serializable]
    public class SubMenuItem : IPluginSubMenuItem
    {
        public SubMenuItem(string name, IEnumerable<IPluginMenuItem> submenu)
        {
            Name = name;
            Submenu = submenu;
        }

        public string Name { get; private set; }
        public IEnumerable<IPluginMenuItem> Submenu { get; private set; }
    }
}