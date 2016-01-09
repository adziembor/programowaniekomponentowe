using System;
using System.Globalization;
using System.Linq;
using System.Windows.Forms;

namespace AdHocPlugin
{
    public class Methods
    {
        public class Usable
        {
            public class Box
            {
                public static void BoxToForm(RichTextBox box, Form form)
                {
                    form.Text = box.Text;
                }
                public static void ClearBox(RichTextBox box)
                {
                    box.Clear();
                }
            }
            public static string Reverse(string input)
            {
                return String.Join("", input.Reverse());
            }
        }

        public static void Test2(Form ha)
        {
            ha.Text = DateTime.UtcNow.ToString(CultureInfo.InvariantCulture);
        }

        public static void Test()
        {
            MessageBox.Show("Discoverable worked!");
        }
    }
}
