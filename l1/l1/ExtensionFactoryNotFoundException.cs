using System;

namespace l1
{
    public class ExtensionFactoryNotFoundException : Exception
    {
        public ExtensionFactoryNotFoundException(Type t, string name)
            : base("Factory implementing type " + t.Name + " with name " + name + " was not found")
        {
        }
    }
}