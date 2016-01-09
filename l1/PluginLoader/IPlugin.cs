
using System.Collections.Generic;
namespace PluginBase
{
    public interface IPlugin
    {
        string Name { get; }
        IEnumerable<IPluginMenuItem> Menus { get; }
    }
}