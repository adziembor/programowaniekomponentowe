using PluginBase;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
namespace l1
{
    class FormPluginManager : IDisposable
    {
        static readonly Dictionary<Form, FormPluginManager> Manager = new Dictionary<Form, FormPluginManager>();
        public static FormPluginManager Create(Form1 form)
        {
            FormPluginManager pm;
            lock (Manager)
            {
                if (!Manager.TryGetValue(form, out pm))
                {
                    pm = Manager[form] = new FormPluginManager(form);
                    form.Disposed += (sender, ev) => { pm.Dispose(); };
                }
            }
            return pm;
        }

        private readonly Form1 _form;
        private readonly Menu _pluginsMenu;
        private event EventHandler Disposed;
        private FormPluginManager(Form1 form)
        {
            _form = form;
            var menuItem = new MenuItem { Text = "Plugins" };
            _form.Menu.MenuItems.Add(menuItem);
            _pluginsMenu = menuItem;
            menuItem.MenuItems.Add(new MenuItem("Load...", (sender, ev) =>
            {
                var dlg = new OpenFileDialog { DefaultExt = "*.dll", Filter = ".net images|*.dll" };
                if (dlg.ShowDialog() == DialogResult.OK && dlg.FileName.Length > 0)
                {
                    Load(dlg.FileName, false);
                }
            }));
        }

        public void Load(string assembly, bool proxy)
        {
            Action dispose = null;
            try
            {
                var p = new PluginInstance(assembly, proxy);
                var plugin = p.Plugin;
                var item = _pluginsMenu.MenuItems.Add(plugin.Name);
                dispose = () => { _pluginsMenu.MenuItems.Remove(item); dispose = () => { }; p.Dispose(); };
                item.MenuItems.Add(new MenuItem("Unload...", (s, o) => dispose()));
                LoadMenus(item.MenuItems, plugin.Menus);
                Disposed += (sender, ev) => { if (dispose != null) dispose(); };
            }
            catch (Exception ex)
            {
                if (dispose != null)
                    dispose();
                var res = MessageBox.Show("An error has occured:\n" + ex, "Error", proxy ? MessageBoxButtons.AbortRetryIgnore : MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                //possible stack overflow exception
                if (res == DialogResult.Retry)
                    Load(assembly, proxy);
                if (res == DialogResult.Ignore)
                    Load(assembly, false);
            }
        }

        private void LoadMenus(Menu.MenuItemCollection items, IEnumerable<IPluginMenuItem> pluginMenu)
        {
            foreach (var pluginItem in pluginMenu)
            {
                var invokable = pluginItem as IPluginMenuInvokableItem;
                if (invokable != null)
                {
                    var invokableHash = Guid.NewGuid().ToString();
                    _form.Sc.Register(typeof(IInvokable), invokable.Invokable, invokableHash);
                    items.Add(new MenuItem(invokable.Name, (sender, ev) => _form.Sc.GetInstance<IInvokable>(invokableHash).DoWork()));
                    continue;
                }
                var submenu = pluginItem as IPluginSubMenuItem;
                if (submenu != null)
                {
                    var item = new MenuItem(submenu.Name);
                    items.Add(item);
                    LoadMenus(item.MenuItems, submenu.Submenu);
                    continue;
                }
                items.Add(new MenuItem(pluginItem.Name) { Enabled = false });
            }
        }

        public void Dispose()
        {
            var disposed = Disposed;
            if (disposed != null)
                disposed(this, EventArgs.Empty);
        }
    }
}