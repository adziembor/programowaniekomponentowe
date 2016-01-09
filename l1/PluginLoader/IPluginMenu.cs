using System.Collections.Generic;

namespace PluginBase
{
    public interface IPluginMenu
    {
        string Name { get; }
        IEnumerable<IPluginMenuItem> Items { get; }
    }
}
