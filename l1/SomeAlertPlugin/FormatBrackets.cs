using PluginBase;
using System.Linq;
using System;
using System.Text;
using System.Windows.Forms;

namespace SomeAlertPlugin
{
    class FormatBrackets : IInvokable
    {
        readonly RichTextBox _rtb;
        public FormatBrackets(RichTextBox rtb)
        {
            _rtb = rtb;
        }

        public void DoWork()
        {
            var t = _rtb.Text;
            StringBuilder sb = new StringBuilder();
            var lvl = 0;
            var n = "\t\n\r ".ToCharArray();
            bool insideIdentifier = false;
            bool appendAfter = true;
            foreach (char t1 in t)
            {
                if (t1 == '\'' || t1 == '"')
                {
                    insideIdentifier = !insideIdentifier;
                }
                if (!insideIdentifier)
                {
                    if (t1 == '}')
                    {
                        lvl--;
                        if (appendAfter)
                        {
                            sb.Append("\n");
                            sb.Append(new String(' ', lvl * 4));
                            appendAfter = false;
                        }
                    }
                    if (appendAfter)
                    {
                        sb.Append("\n");
                        sb.Append(new String(' ', lvl * 4));
                        appendAfter = false;
                    }
                    if (t1 == '}')
                    {
                        appendAfter = true;
                    }
                    if (t1 == ';')
                        appendAfter = true;
                    if (t1 == '{')
                    {
                        sb.Append("\n");
                        sb.Append(new String(' ', lvl * 4));
                        lvl++;
                        appendAfter = true;
                    }
                }
                if (!n.Contains(t1))
                    sb.Append(t1);
            }

            _rtb.Text = sb.ToString();
        }
    }
}