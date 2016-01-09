using PluginBase;
using System.Windows.Forms;

namespace SomeAlertPlugin
{
    class Uppercase : IInvokable
    {
        readonly RichTextBox _rtb;
        public Uppercase(RichTextBox rtb)
        {
            _rtb = rtb;
        }

        public void DoWork()
        {
            _rtb.Text = _rtb.Text.ToUpper();
        }
    }
}