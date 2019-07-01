namespace ToyGraf.Controllers
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Controls;
    using ToyGraf.Models;
    using Win32 = Microsoft.Win32;

    /// <summary>
    /// "Most Recently Used" Controller.
    /// 
    /// Use the Windows Register to keep track of an application's recently used files.
    /// Display recently used file paths on a dedicated submenu in the application.
    /// Allow these recently used files to be invoked via a virtual "Reopen" method.
    /// Provide a "Clear" subitem to reset the content of this submenu to (empty).
    /// Note: unsafe code is used to abbreviate long paths (see CompactMenuText method).
    /// </summary>
    internal class MruController
    {
        #region Protected Constructor

        protected MruController(Model model, string subKeyName, ToolStripDropDownItem recentMenu)
        {
            if (string.IsNullOrWhiteSpace(subKeyName))
                throw new ArgumentNullException("subKeyName");
            Model = model;
            SubKeyName = string.Format(
                @"Software\{0}\{1}\{2}",
                Application.CompanyName,
                Application.ProductName,
                subKeyName);
            RecentMenu = recentMenu;
            RefreshRecentMenu();
        }

        #endregion

        #region Internal Interface

        internal virtual void Reopen(ToolStripItem menuItem) { }

        #endregion

        #region Protected Properties

        protected readonly Model Model;

        #endregion

        #region Private Properties

        private readonly string SubKeyName;
        private ToolStripDropDownItem RecentMenu;

        #endregion

        #region Private Event Handlers

        private void OnItemClick(object sender, EventArgs e) => Reopen((ToolStripItem)sender);

        private void OnRecentClear_Click(object sender, EventArgs e)
        {
            try
            {
                Win32.RegistryKey key = OpenSubKey(true);
                if (key == null)
                    return;
                foreach (string name in key.GetValueNames())
                    key.DeleteValue(name, true);
                key.Close();
                if (RecentMenu != null)
                {
                    RecentMenu.DropDownItems.Clear();
                    RecentMenu.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        #endregion

        #region Protected Methods

        protected void AddItem(string item)
        {
            try
            {
                var key = CreateSubKey();
                if (key == null)
                    return;
                try
                {
                    DeleteItem(key, item);
                    key.SetValue(string.Format("{0:yyyyMMddHHmmssFF}", DateTime.Now), item);
                }
                finally
                {
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            RefreshRecentMenu();
        }

        protected void RemoveItem(string item)
        {
            try
            {
                var key = OpenSubKey(true);
                if (key == null)
                    return;
                try
                {
                    DeleteItem(key, item);
                }
                finally
                {
                    key.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            RefreshRecentMenu();
        }

        #endregion

        #region Private Methods

        private Win32.RegistryKey CreateSubKey()
        {
            return Win32.Registry.CurrentUser.CreateSubKey(
                SubKeyName, Win32.RegistryKeyPermissionCheck.ReadWriteSubTree);
        }

        private void DeleteItem(Win32.RegistryKey key, string item)
        {
            var name = key.GetValueNames()
                .Where(n => key.GetValue(n, null) as string == item)
                .FirstOrDefault();
            if (name != null)
                key.DeleteValue(name);
        }

        private Win32.RegistryKey OpenSubKey(bool writable) =>
            Win32.Registry.CurrentUser.OpenSubKey(SubKeyName, writable);

        private void RefreshRecentMenu()
        {
            if (RecentMenu == null)
                return;
            var items = RecentMenu.DropDownItems;
            items.Clear();
            Win32.RegistryKey key = null;
            try
            {
                key = OpenSubKey(false);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            bool ok = key != null;
            if (ok)
            {
                foreach (var name in key.GetValueNames().OrderByDescending(n => n))
                {
                    var value = key.GetValue(name, null) as string;
                    if (value == null)
                        continue;
                    try
                    {
                        var text = value.Split('|')[0].CompactMenuText();
                        var item = items.Add(text, null, OnItemClick);
                        item.Tag = value;
                        item.ToolTipText = value.Replace('|', '\n');
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
                ok = items.Count > 0;
                if (ok)
                {
                    items.Add("-");
                    items.Add("Clear this list").Click += OnRecentClear_Click;
                }
            }
            RecentMenu.Enabled = ok;
        }

        #endregion
    }
}
