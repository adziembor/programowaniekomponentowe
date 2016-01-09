using PluginBase;
using System.Drawing;
using System.Windows.Forms;

namespace SomeAlertPlugin
{
    class GenerateColors : IInvokable
    {
        readonly RichTextBox _rtb;
        public GenerateColors(RichTextBox rtb)
        {
            _rtb = rtb;
        }

        private void AppendText(RichTextBox box, string text, Color color)
        {
            box.SelectionStart = box.TextLength;
            box.SelectionLength = 0;

            box.SelectionColor = color;
            box.AppendText(text);
            box.SelectionColor = box.ForeColor;
        }

        private void Generate(RichTextBox rtb, string name)
        {
            for (var i = 0; i < 10000; i++)
            {
                AppendText(rtb, name, Color.FromArgb((name + i).GetHashCode()));
            }
        }

        public void DoWork()
        {
            Generate(_rtb, Prompt.ShowDialog("Tell me a string to use as hash", "name"));
        }
    }
}