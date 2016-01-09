using PluginBase;

namespace SomeAlertPlugin
{
    class SimpleMessageBox : IInvokable
    {
        public void DoWork()
        {
            System.Windows.Forms.MessageBox.Show("Message box!");
        }
    }
}