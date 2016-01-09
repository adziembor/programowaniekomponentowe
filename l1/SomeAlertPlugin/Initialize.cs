using PluginBase;
using System;
using System.Collections.Generic;

namespace SomeAlertPlugin
{
    [Serializable]
    public class Initialize : IPlugin
    {
        public IEnumerable<IPluginMenuItem> Menus
        {
            get
            {
                yield return new InvokeMenuItem<SimpleMessageBox>("Message box");
                yield return new InvokeMenuItem<PrintCurrentDate>("Print current date");
                yield return new Separator();
                yield return new SubMenuItem("Text functions", new IPluginMenuItem[] {
                    new InvokeMenuItem<Uppercase>("Uppercase"),
                    new InvokeMenuItem<Lowercase>("Lowercase"),
                });
                yield return new InvokeMenuItem<GenerateColors>("Generate some colors");
                yield return new SubMenuItem("Programmistic functions", new IPluginMenuItem[] {
                    new InvokeMenuItem<FormatBrackets>("Format brackets"),
                });
            }
        }

        public string Name
        {
            get { return "SomeAlertPlugin"; }
        }
    }
}