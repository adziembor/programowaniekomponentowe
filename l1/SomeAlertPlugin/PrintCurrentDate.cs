using PluginBase;
using System;
using System.Globalization;
using System.Windows.Forms;

namespace SomeAlertPlugin
{
    class PrintCurrentDate : IInvokable
    {
        readonly RichTextBox _rtb;
        public PrintCurrentDate(RichTextBox rtb)
        {
            _rtb = rtb;
        }

        public void DoWork()
        {
            _rtb.Text += DateTime.Now.ToString(CultureInfo.InvariantCulture);
        }
    }
}