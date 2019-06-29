namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class HostController
    {
        #region Internal Interface

        internal HostController(string text, Control control)
        {
            Control = control;
            OriginalParent = control.Parent;
            HostForm = new HostForm();
            HostForm.FormClosing += HostForm_FormClosing;
            HostForm.ClientSize = new Size(
                control.Width,
                control.Height + HostForm.StatusBar.Height);
            HostForm.Text = text;
            OriginalParent.Controls.Remove(control);
            HostForm.Controls.Add(control);
            control.BringToFront();
        }

        internal void Close()
        {
            HostForm.Controls.Remove(Control);
            OriginalParent.Controls.Add(Control);
            HostForm.Close();
        }

        internal void Show(IWin32Window owner) => HostForm.Show(owner);

        internal event EventHandler<FormClosingEventArgs> HostFormClosing;

        #endregion

        #region Private Implementation

        private readonly Control Control, OriginalParent;
        private readonly HostForm HostForm;

        private void HostForm_FormClosing(object sender, FormClosingEventArgs e) =>
            HostFormClosing?.Invoke(sender, e);

        #endregion
    }
}
