using PluginBase;
using System;
namespace l1
{
    class PluginInstance : IDisposable
    {
        private AppDomain _domain;
        private readonly PluginProxy _proxy;
        public PluginInstance(string assemblyPath, bool loadAppDomain)
        {
            if (loadAppDomain)
            {
                var appDomainSetup = new AppDomainSetup { ApplicationBase = Environment.CurrentDirectory };
                var evidence = AppDomain.CurrentDomain.Evidence;
                _domain = AppDomain.CreateDomain("Container: " + assemblyPath, evidence, appDomainSetup);

                Type type = typeof(PluginProxy);
                _proxy = (PluginProxy)_domain.CreateInstanceAndUnwrap(type.Assembly.FullName, type.FullName);
            }
            else
            {
                _proxy = new PluginProxy();
            }
            _proxy.Load(assemblyPath);
        }

        public IPlugin Plugin { get { return _proxy.Plugin; } }

        public void Dispose()
        {
            var domain = _domain;
            _domain = null;
            if (domain != null)
            {
                AppDomain.Unload(domain);
            }
        }
    }
}