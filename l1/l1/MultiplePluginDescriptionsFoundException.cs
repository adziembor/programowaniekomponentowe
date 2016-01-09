
using PluginBase;
using System;

namespace l1
{
    [Serializable]
    public class MultiplePluginDescriptionsFoundException : Exception
    {
        public MultiplePluginDescriptionsFoundException()
            : base("Assembly is implementing multiple " + typeof(IPlugin).Name)
        {
        }
    }
}