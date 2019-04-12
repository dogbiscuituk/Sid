namespace Sid.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sid.Models;

    public class MainFormController
    {
        public MainFormController()
        {
            View = new MainForm();
            Model = new Model();
            Model.ModifiedChanged += Model_ModifiedChanged;

            PersistenceController = new PersistenceController(Model, View, View.FileReopen);
            PersistenceController.FilePathChanged += PersistenceController_FilePathChanged;
            PersistenceController.FileSaving += PersistenceController_FileSaving;
        }

        private void PersistenceController_FileSaving(object sender, CancelEventArgs e)
        {
            e.Cancel = !ContinueSaving();
        }

        private void PersistenceController_FilePathChanged(object sender, EventArgs e)
        {
            View.Text = PersistenceController.WindowCaption;
        }

        private void Model_ModifiedChanged(object sender, EventArgs e)
        {
            ModifiedChanged();
        }

        public readonly Model Model;
        public readonly PersistenceController PersistenceController;

        private MainForm _view;
        public MainForm View
        {
            get => _view;
            set
            {
                _view = value;
                View.FileMenu.DropDownOpening += FileMenu_DropDownOpening;
                View.FileNew.Click += FileNew_Click;
                View.FileOpen.Click += FileOpen_Click;
                View.FileSave.Click += FileSave_Click;
                View.FileSaveAs.Click += FileSaveAs_Click;
                View.FileExit.Click += FileExit_Click;

                PictureBox.MouseMove += PictureBox_MouseMove;
                PictureBox.Paint += PictureBox_Paint;
                PictureBox.Resize += PictureBox_Resize;
            }
        }

        public PictureBox PictureBox { get => View.PictureBox; }

        private void PictureBox_Resize(object sender, EventArgs e)
        {
            PictureBox.Invalidate();
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var p = Model.Graph.ScreenToGraph(e.Location, PictureBox.ClientRectangle);
            View.ToolTip.SetToolTip(PictureBox, $"({p.X}, {p.Y})");
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            Model.Graph.Draw(e.Graphics, PictureBox.ClientRectangle);
        }

        public void FileMenu_DropDownOpening(object sender, EventArgs e)
        {
            View.FileSave.Enabled = Model.Modified;
        }

        private void FileNew_Click(object sender, EventArgs e)
        {
            PersistenceController.Clear();
        }

        private void FileOpen_Click(object sender, EventArgs e)
        {
            PersistenceController.Open();
        }

        private void FileSave_Click(object sender, EventArgs e)
        {
            PersistenceController.Save();
        }

        private void FileSaveAs_Click(object sender, EventArgs e)
        {
            PersistenceController.SaveAs();
        }

        private void FileExit_Click(object sender, EventArgs e)
        {
            View.Close();
        }

        private void ModifiedChanged()
        {
            View.Text = PersistenceController.WindowCaption;
            View.ModifiedLabel.Visible = Model.Modified;
        }

        private bool ContinueSaving()
        {
            return true;
        }
    }
}
