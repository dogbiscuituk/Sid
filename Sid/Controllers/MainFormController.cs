namespace Sid.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using Sid.Models;
    using Sid.Views;

    public class MainFormController
    {
        public MainFormController()
        {
            View = new MainForm();
            Model = new Model();
            Model.IsotropicChanged += Model_IsotropicChanged;
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
                View.Resize += View_Resize;

                View.FileMenu.DropDownOpening += FileMenu_DropDownOpening;
                View.FileNew.Click += FileNew_Click;
                View.FileOpen.Click += FileOpen_Click;
                View.FileSave.Click += FileSave_Click;
                View.FileSaveAs.Click += FileSaveAs_Click;
                View.FileExit.Click += FileExit_Click;
                View.EditUndo.Click += EditUndo_Click;
                View.EditRedo.Click += EditRedo_Click;
                View.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
                View.ViewIsotropic.Click += ViewIsotropic_Click;
                View.ViewZoomIn.Click += ViewZoomIn_Click;
                View.ViewZoomOut.Click += ViewZoomOut_Click;
                View.ViewScrollLeft.Click += ViewScrollLeft_Click;
                View.ViewScrollRight.Click += ViewScrollRight_Click;
                View.ViewScrollUp.Click += ViewScrollUp_Click;
                View.ViewScrollDown.Click += ViewScrollDown_Click;
                View.HelpAbout.Click += HelpAbout_Click;

                PictureBox.MouseDown += PictureBox_MouseDown;
                PictureBox.MouseMove += PictureBox_MouseMove;
                PictureBox.MouseUp += PictureBox_MouseUp;
                PictureBox.MouseWheel += PictureBox_MouseWheel;
                PictureBox.Paint += PictureBox_Paint;
                PictureBox.Resize += PictureBox_Resize;
            }
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = !PersistenceController.SaveIfModified();
        }

        private void View_Resize(object sender, EventArgs e)
        {
            if (Model.Isotropic)
                AdjustPictureBox();
        }

        public Panel ClientPanel { get => View.ClientPanel; }
        public Graph Graph { get => Model.Graph; }
        public PictureBox PictureBox { get => View.PictureBox; }

        private bool Dragging;
        private PointF DragOrigin;

        private void PersistenceController_FileSaving(object sender, CancelEventArgs e)
        {
            e.Cancel = !ContinueSaving();
        }

        private void PersistenceController_FilePathChanged(object sender, EventArgs e)
        {
            View.Text = PersistenceController.WindowCaption;
        }


        private void Model_IsotropicChanged(object sender, EventArgs e) { IsotropicChanged(); }
        private void Model_ModifiedChanged(object sender, EventArgs e) { ModifiedChanged(); }

        public void FileMenu_DropDownOpening(object sender, EventArgs e) => View.FileSave.Enabled = Model.Modified;
        private void FileNew_Click(object sender, EventArgs e) => PersistenceController.Clear();
        private void FileOpen_Click(object sender, EventArgs e) => PersistenceController.Open();
        private void FileSave_Click(object sender, EventArgs e) => PersistenceController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => PersistenceController.SaveAs();
        private void FileExit_Click(object sender, EventArgs e) => View.Close();
        private void EditUndo_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
        private void EditRedo_Click(object sender, EventArgs e) { throw new NotImplementedException(); }
        private void ViewMenu_DropDownOpening(object sender, EventArgs e) { View.ViewIsotropic.Checked = Model.Isotropic; }
        private void ViewIsotropic_Click(object sender, EventArgs e) { Model.Isotropic = !Model.Isotropic; }
        private void ViewZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ViewZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ViewScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1, 0);
        private void ViewScrollRight_Click(object sender, EventArgs e) => Scroll(0.1, 0);
        private void ViewScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1);
        private void ViewScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1);

        private void HelpAbout_Click(object sender, EventArgs e) =>
            MessageBox.Show(
                $"{Application.CompanyName}\n{Application.ProductName}\nVersion {Application.ProductVersion}",
                $"About {Application.ProductName}");

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            Dragging = true;
            DragOrigin = GetMousePosition(e);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            var p = GetMousePosition(e);
            View.ToolTip.SetToolTip(PictureBox, $"({p.X}, {p.Y})");
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            var p = GetMousePosition(e);
            ScrollBy(DragOrigin.X - p.X, DragOrigin.Y - p.Y);
            Dragging = false;
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            Zoom((float)Math.Pow(e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0,
                Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta)));
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e) =>
            Graph.Draw(e.Graphics, PictureBox.ClientRectangle);

        private void PictureBox_Resize(object sender, EventArgs e) => PictureBox.Invalidate();

        private void AdjustPictureBox()
        {
            float gW = Graph.Size.Width, gH = Graph.Size.Height;
            int cW = ClientPanel.ClientSize.Width, cH = ClientPanel.ClientSize.Height;
            if (gW > gH * cW / cH)
            {
                var h = gH * cW / gW;
                PictureBox.SetBounds(0, (int)Math.Round((cH - h) / 2), cW, (int)(Math.Round(h)));
            }
            else
            {
                var w = gW * cH / gH;
                PictureBox.SetBounds((int)Math.Round((cW - w) / 2), 0, (int)(Math.Round(w)), cH);
            }
        }

        private void IsotropicChanged()
        {
            if (Model.Isotropic)
            {
                PictureBox.Dock = DockStyle.None;
                AdjustPictureBox();
            }
            else
                PictureBox.Dock = DockStyle.Fill;
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

        private PointF GetMousePosition(MouseEventArgs e)
        {
            return Graph.ScreenToGraph(e.Location, PictureBox.ClientRectangle);
        }

        private void Scroll(double xFactor, double yFactor)
        {
            Graph.Scroll(xFactor, yFactor);
            PictureBox.Invalidate();
        }

        private void ScrollBy(float xDelta, float yDelta)
        {
            Graph.ScrollBy(xDelta, yDelta);
            PictureBox.Invalidate();
        }

        private void Zoom(float factor)
        {
            Graph.Zoom(factor);
            PictureBox.Invalidate();
        }
    }
}
