namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;

    internal class OptionsController : IDisposable
    {
        #region Internal Interface

        internal OptionsController(GraphController graphController)
        {
            GraphController = graphController;
            OptionsDialog = new OptionsDialog();
            OptionsDialog.btnFilesFolder.Click += BtnFilesFolder_Click;
            OptionsDialog.btnTemplatesFolder.Click += BtnTemplatesFolder_Click;
        }

        internal DialogResult ShowModal(IWin32Window owner)
        {
            Options = AppController.Options;
            var result = OptionsDialog.ShowDialog(owner);
            if (result == DialogResult.OK)
                AppController.Options = Options;
            return result;
        }

        #endregion

        #region Private Properties

        private GraphController GraphController;
        private OptionsDialog OptionsDialog;
        private Options Options
        {
            get => GetOptions();
            set => SetOptions(value);
        }

        #endregion

        #region Private Event Handlers

        private void BtnFilesFolder_Click(object sender, System.EventArgs e) =>
            BrowseFolder("files", OptionsDialog.edFilesFolder);

        private void BtnTemplatesFolder_Click(object sender, System.EventArgs e) =>
            BrowseFolder("templates", OptionsDialog.edTemplatesFolder);

        #endregion

        #region Private Methods

        private void BrowseFolder(string detail, TextBox textBox)
        {
            using (var dialog = new FolderBrowserDialog
            {
                Description = $"Select the default folder for storing graph {detail}:",
                SelectedPath = textBox.Text,
                ShowNewFolderButton = true
            })
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                    textBox.Text = dialog.SelectedPath;
            }
        }

        private Options GetOptions() => new Options {
            OpenInNewWindow = OptionsDialog.rbWindowNew.Checked,
            GroupUndo = OptionsDialog.rbGroupUndo.Checked,
            FilesFolderPath = OptionsDialog.edFilesFolder.Text,
            TemplatesFolderPath = OptionsDialog.edTemplatesFolder.Text
        };

        private void SetOptions(Options options)
        {
            OptionsDialog.rbWindowNew.Checked = options.OpenInNewWindow;
            OptionsDialog.rbWindowReuse.Checked = !options.OpenInNewWindow;
            OptionsDialog.rbGroupUndo.Checked = options.GroupUndo;
            OptionsDialog.rbNoGroupUndo.Checked = !options.GroupUndo;
            OptionsDialog.edFilesFolder.Text = options.FilesFolderPath;
            OptionsDialog.edTemplatesFolder.Text = options.TemplatesFolderPath;
        }

        #endregion

        #region IDisposable

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposeOptionsDialog();
        }

        private void DisposeOptionsDialog()
        {
            if (OptionsDialog != null)
            {
                OptionsDialog.Dispose();
                OptionsDialog = null;
            }
        }

        #endregion
    }
}
