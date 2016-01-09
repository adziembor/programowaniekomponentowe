using PluginBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using l1.DiscoverPlugin;

namespace l1
{
    public class PluginProxy : MarshalByRefObject
    {
        private static bool IsAssignableFrom(Type requestedType, Type assignedType)
        {
            return (requestedType.IsAssignableFrom(assignedType) || assignedType.IsSubclassOf(requestedType)) && !assignedType.IsAbstract;
        }

        public void Load(string path)
        {
            var assembly = Assembly.LoadFile(path);
            var types = assembly.GetTypes();
            var pluginType = types.Where(type => IsAssignableFrom(typeof(IPlugin), type));
            var enumerable = pluginType as IList<Type> ?? pluginType.ToList();
            switch (enumerable.Count)
            {
                case 0:
                    Plugin = new DiscoveredPlugin(assembly);
                    break;
                case 1:
                    Plugin = (IPlugin)Activator.CreateInstance(enumerable.First());
                    break;
                default:
                    throw new MultiplePluginDescriptionsFoundException();
            }
        }

        public IPlugin Plugin { get; private set; }
    }
}