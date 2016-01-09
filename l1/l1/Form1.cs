using System;
using System.Windows.Forms;
using LightInject;

namespace l1
{
    public class LoadedFile
    {
        private readonly string _initial;
        private readonly string _fileName;
        private readonly RichTextBox _textBox;
        public LoadedFile(RichTextBox box, string name)
        {
            _textBox = box;
            _textBox.LoadFile(name);
            _fileName = name;
            _initial = box.Text;
        }

        public LoadedFile(RichTextBox box, string fileName, string text)
        {
            _textBox = box;
            _fileName = fileName;
            _initial = text;
        }


        public bool WasModified
        {
            get
            {
                return _initial != _textBox.Text;
            }
        }

        public void Save()
        {
            _textBox.SaveFile(_fileName);
        }
    }
    public class Form1 : Form
    {
        public RichTextBox TextBox;
        public LoadedFile File;
        public ServiceContainer Sc = new ServiceContainer();
        public Form1()
        {
            Text = "rtf edit";


            TextBox = new RichTextBox();
            Action updateSize = () =>
            {
                //textBox.Top = 40;
                TextBox.Width = ClientSize.Width;
                TextBox.Height = ClientSize.Height;
            };
            Controls.Add(TextBox);
            Resize += (sender, e) =>
            {
                updateSize();
            };
            updateSize();

            var menu = new MainMenu();
            Menu = menu;
            var file = new MenuItem("File");
            menu.MenuItems.Add(file);
            file.MenuItems.Add(new MenuItem("New", FileNew, Shortcut.CtrlN));
            file.MenuItems.Add(new MenuItem("Open...", FileOpen, Shortcut.CtrlL));
            file.MenuItems.Add(new MenuItem("Save...", FileSave, Shortcut.CtrlS));
            file.MenuItems.Add(new MenuItem("Exit", FileExit, Shortcut.CtrlQ));
            FormClosing += (sender, ev) =>
            {
                try
                {
                    EnsureFileEmptyOrSave();
                }
                catch (CancelException)
                {
                    if (ev.CloseReason == CloseReason.UserClosing)
                        ev.Cancel = true;
                }
            };
            Sc.RegisterInstance(TextBox);
            Sc.RegisterInstance<Form>(this);
            FormPluginManager.Create(this);
        }

        public sealed override string Text
        {
            get { return base.Text; }
            set { base.Text = value; }
        }

        class CancelException : Exception { }
        void EnsureFileEmptyOrSave()
        {
            if (File != null && File.WasModified || File == null && TextBox.Text != "")
            {
                var res = MessageBox.Show("File was modified, do you want to save?", "?", MessageBoxButtons.YesNoCancel);
                if (res == DialogResult.Yes)
                {
                    if (File != null)
                        File.Save();
                    else
                        FileSave(this, EventArgs.Empty);
                }
                if (res == DialogResult.Cancel)
                {
                    throw new CancelException();
                }
            }
        }
        void FileNew(object sender, EventArgs e)
        {
            try
            {
                EnsureFileEmptyOrSave();
                TextBox.Text = "";
                File = null;
            }
            catch (CancelException)
            {
            }
        }

        void FileOpen(object sender, EventArgs e)
        {
            try
            {
                EnsureFileEmptyOrSave();
                OpenFileDialog openFile1 = new OpenFileDialog
                {
                    DefaultExt = "*.rtf",
                    Filter = "RTF Files|*.rtf"
                };
                if (openFile1.ShowDialog() == DialogResult.OK &&
                openFile1.FileName.Length > 0)
                {
                    File = new LoadedFile(TextBox, openFile1.FileName);
                }
            }
            catch (CancelException) { }
        }

        void FileSave(object sender, EventArgs e)
        {
            if (File == null)
            {
                SaveFileDialog saveFile1 = new SaveFileDialog
                {
                    DefaultExt = "*.rtf",
                    Filter = "RTF Files|*.rtf"
                };
                if (saveFile1.ShowDialog() == DialogResult.OK &&
                saveFile1.FileName.Length > 0)
                {
                    TextBox.SaveFile(saveFile1.FileName);
                }
            }
            else
            {
                File.Save();
            }
        }

        void FileExit(object sender, EventArgs e)
        {
            try
            {
                EnsureFileEmptyOrSave();
                Application.Exit();
            }
            catch (CancelException) { }
        }
    }
}