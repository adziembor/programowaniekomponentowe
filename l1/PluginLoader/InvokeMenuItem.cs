using System;

namespace PluginBase
{
    [Serializable]
    public class InvokeMenuItem<T> : IPluginMenuInvokableItem
        where T : IInvokable
    {
        public InvokeMenuItem(string name)
        {
            Name = name;
            Invokable = typeof(T);
        }

        public string Name { get; private set; }
        public Type Invokable { get; private set; }
    }
}