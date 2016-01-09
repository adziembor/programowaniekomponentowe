using PluginBase;
using System.Windows.Forms;

namespace SomeAlertPlugin
{
    class Lowercase : IInvokable
    {
        readonly RichTextBox _rtb;
        public Lowercase(RichTextBox rtb)
        {
            _rtb = rtb;
        }

        public void DoWork()
        {
            _rtb.Text = _rtb.Text.ToLower();
        }
    }
}