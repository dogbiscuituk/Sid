namespace Sid.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using Sid.Models;
    using Sid.Views;

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

        public readonly Model Model;
        public readonly PersistenceController PersistenceController;

        private MainForm _view;
        public MainForm View
        {
            get => _view;
            set
            {
                _view = value;

                View.FormClosing += View_FormClosing;

                View.FileMenu.DropDownOpening += FileMenu_DropDownOpening;
                View.FileNew.Click += FileNew_Click;
                View.FileOpen.Click += FileOpen_Click;
                View.FileSave.Click += FileSave_Click;
                View.FileSaveAs.Click += FileSaveAs_Click;
                View.FileExit.Click += FileExit_Click;
                View.EditUndo.Click += EditUndo_Click;
                View.EditRedo.Click += EditRedo_Click;
                View.ViewZoomIn.Click += ViewZoomIn_Click;
                View.ViewZoomOut.Click += ViewZoomOut_Click;
                View.ViewScrollLeft.Click += ViewScrollLeft_Click;
                View.ViewScrollRight.Click += ViewScrollRight_Click;
                View.ViewScrollUp.Click += ViewScrollUp_Click;
                View.ViewScrollDown.Click += ViewScrollDown_Click;
                View.HelpAbout.Click += HelpAbout_Click;

                PictureBox.MouseMove += PictureBox_MouseMove;
                PictureBox.Paint += PictureBox_Paint;
                PictureBox.Resize += PictureBox_Resize;
            }
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !PersistenceController.SaveIfModified();
        }

        public PictureBox PictureBox { get => View.PictureBox; }
        public Graph Graph { get => Model.Graph; }

        private void PersistenceController_FileSaving(object sender, CancelEventArgs e)
        {
            e.Cancel = !ContinueSaving();
        }

        private void PersistenceController_FilePathChanged(object sender, EventArgs e)
        {
            View.Text = PersistenceController.WindowCaption;
        }

        private void Model_ModifiedChanged(object sender, EventArgs e) { ModifiedChanged(); }

        public void FileMenu_DropDownOpening(object sender, EventArgs e) => View.FileSave.Enabled = Model.Modified;
        private void FileNew_Click(object sender, EventArgs e) => PersistenceController.Clear();
        private void FileOpen_Click(object sender, EventArgs e) => PersistenceController.Open();
        private void FileSave_Click(object sender, EventArgs e) => PersistenceController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => PersistenceController.SaveAs();
        private void FileExit_Click(object sender, EventArgs e) => View.Close();
        private void EditUndo_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
        private void EditRedo_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
        private void ViewZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ViewZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ViewScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1f, 0);
        private void ViewScrollRight_Click(object sender, EventArgs e) => Scroll(0.1f, 0);
        private void ViewScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1f);
        private void ViewScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1f);
        private void PictureBox_Resize(object sender, EventArgs e) => PictureBox.Invalidate();

        private void HelpAbout_Click(object sender, EventArgs e) =>
            MessageBox.Show(
                $"{Application.CompanyName}\n{Application.ProductName}\nVersion {Application.ProductVersion}",
                $"About {Application.ProductName}");

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var p = Graph.ScreenToGraph(e.Location, PictureBox.ClientRectangle);
            View.ToolTip.SetToolTip(PictureBox, $"({p.X}, {p.Y})");
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e) => Graph.Draw(e.Graphics, PictureBox.ClientRectangle);

        private void ModifiedChanged()
        {
            View.Text = PersistenceController.WindowCaption;
            View.ModifiedLabel.Visible = Model.Modified;
        }

        private bool ContinueSaving()
        {
            return true;
        }

        private void Zoom(float factor)
        {
            Graph.Zoom(factor);
            PictureBox.Invalidate();
        }

        private void Scroll(float xFactor, float yFactor)
        {
            Graph.Scroll(xFactor, yFactor);
            PictureBox.Invalidate();
        }
    }
}
