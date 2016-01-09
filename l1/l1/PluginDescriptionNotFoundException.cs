using PluginBase;
using System;

namespace l1
{
    [Serializable]
    public class PluginDescriptionNotFoundException : Exception
    {
        public PluginDescriptionNotFoundException()
            : base("Assembly is not implementing " + typeof(IPlugin).Name)
        {
        }
    }
}