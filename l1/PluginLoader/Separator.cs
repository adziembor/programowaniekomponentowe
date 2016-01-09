using System;

namespace PluginBase
{
    [Serializable]
    public class Separator : IPluginMenuItem
    {
        public string Name
        {
            get { return "----"; }
        }
    }
}