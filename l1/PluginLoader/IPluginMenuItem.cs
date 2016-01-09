using System;
using System.Collections.Generic;

namespace PluginBase
{
    /// <summary>
    /// a menu item that does nothing, use it for separators
    /// </summary>
    public interface IPluginMenuItem
    {
        string Name { get; }
    }

    public interface IPluginSubMenuItem : IPluginMenuItem
    {
        IEnumerable<IPluginMenuItem> Submenu { get; }
    }

    public interface IPluginMenuInvokableItem : IPluginMenuItem
    {
        Type Invokable { get; }
    }
}
