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
            Model.PropertyChanged += Model_PropertyChanged;

            PersistenceController = new JsonController(Model, View, View.FileReopen);
            PersistenceController.FilePathChanged += PersistenceController_FilePathChanged;
            PersistenceController.FileSaving += PersistenceController_FileSaving;

            PropertiesDialogController = new PropertiesDialogController(this);
        }

        public readonly Model Model;
        public readonly JsonController PersistenceController;
        public readonly PropertiesDialogController PropertiesDialogController;

        public Panel ClientPanel { get => View.ClientPanel; }
        public Graph Graph { get => Model.Graph; }
        public PictureBox PictureBox { get => View.PictureBox; }

        private bool ShowMouseCoordinates = true;
        private bool Dragging;
        private PointF DragOrigin;

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
                View.EditProperties.Click += EditProperties_Click;
                View.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
                View.ViewZoomIn.Click += ViewZoomIn_Click;
                View.ViewZoomOut.Click += ViewZoomOut_Click;
                View.ViewIsotropic.Click += ViewIsotropic_Click;
                View.ViewMouseCoordinates.Click += ViewMouseCoordinates_Click;
                View.ViewScrollLeft.Click += ViewScrollLeft_Click;
                View.ViewScrollRight.Click += ViewScrollRight_Click;
                View.ViewScrollUp.Click += ViewScrollUp_Click;
                View.ViewScrollDown.Click += ViewScrollDown_Click;
                View.ViewScrollCentre.Click += ViewScrollCentre_Click;
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

        private void PersistenceController_FileSaving(object sender, CancelEventArgs e)
        {
            e.Cancel = !ContinueSaving();
        }

        private void PersistenceController_FilePathChanged(object sender, EventArgs e)
        {
            View.Text = PersistenceController.WindowCaption;
        }

        private void Model_IsotropicChanged(object sender, EventArgs e) => IsotropicChanged();
        private void Model_ModifiedChanged(object sender, EventArgs e) => ModifiedChanged();
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) => ModelPropertyChanged();

        public void FileMenu_DropDownOpening(object sender, EventArgs e) => View.FileSave.Enabled = Model.Modified;
        private void FileNew_Click(object sender, EventArgs e) => PersistenceController.Clear();
        private void FileOpen_Click(object sender, EventArgs e) => PersistenceController.Open();
        private void FileSave_Click(object sender, EventArgs e) => PersistenceController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => PersistenceController.SaveAs();
        private void FileExit_Click(object sender, EventArgs e) => View.Close();
        private void EditUndo_Click(object sender, EventArgs e) { }
        private void EditRedo_Click(object sender, EventArgs e) { }
        private void EditProperties_Click(object sender, EventArgs e) => ShowPropertiesDialog();
        private void ViewMenu_DropDownOpening(object sender, EventArgs e) => UpdateViewMenu();
        private void ViewZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ViewZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ViewIsotropic_Click(object sender, EventArgs e) => Model.Isotropic = !Model.Isotropic;
        private void ViewMouseCoordinates_Click(object sender, EventArgs e) => ToggleMouseCoordinates();
        private void ViewScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1, 0);
        private void ViewScrollRight_Click(object sender, EventArgs e) => Scroll(0.1, 0);
        private void ViewScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1);
        private void ViewScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1);
        private void ViewScrollCentre_Click(object sender, EventArgs e) => ScrollTo(0, 0);
        private void HelpAbout_Click(object sender, EventArgs e) => ShowVersionInfo();

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            PictureBox.Cursor = Cursors.Hand;
            Dragging = true;
            DragOrigin = GetMousePosition(e);
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            UpdateMouseCoordinates(e);
        }

        private void UpdateMouseCoordinates(MouseEventArgs e)
        {
            if (ShowMouseCoordinates)
            {
                PointF p = GetMousePosition(e), q = new PointF(DragOrigin.X - p.X, DragOrigin.Y - p.Y);
                InitCoordinatesToolTip(Dragging ? $"Scroll by ({q.X}, {q.Y})" : $"({p.X}, {p.Y})");
            }
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            var p = GetMousePosition(e);
            ScrollBy(DragOrigin.X - p.X, DragOrigin.Y - p.Y);
            Dragging = false;
            PictureBox.Cursor = Cursors.Default;
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e)
        {
            Zoom((float)Math.Pow(e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0,
                Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta)));
        }

        private void PictureBox_Paint(object sender, PaintEventArgs e) =>
            Graph.Draw(e.Graphics, PictureBox.ClientRectangle);

        private void PictureBox_Resize(object sender, EventArgs e) => PictureBox.Invalidate();

        private void ShowVersionInfo()
        {
            MessageBox.Show(
                $"{Application.CompanyName}\n{Application.ProductName}\nVersion {Application.ProductVersion}",
                $"About {Application.ProductName}");
        }

        private void UpdateViewMenu()
        {
            View.ViewIsotropic.Checked = Model.Isotropic;
            View.ViewMouseCoordinates.Checked = ShowMouseCoordinates;
        }

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

        private void ModelPropertyChanged()
        {
            PictureBox.Invalidate();
        }

        private bool ContinueSaving()
        {
            return true;
        }

        private PointF GetMousePosition(MouseEventArgs e)
        {
            return Graph.ScreenToGraph(e.Location, PictureBox.ClientRectangle);
        }

        private void ToggleMouseCoordinates()
        {
            ShowMouseCoordinates = !ShowMouseCoordinates;
            if (!ShowMouseCoordinates)
                InitCoordinatesToolTip(string.Empty);
        }

        private void InitCoordinatesToolTip(string text)
        {
            View.ToolTip.SetToolTip(PictureBox, text);
        }

        private void Scroll(double xFactor, double yFactor)
        {
            Graph.Scroll(xFactor, yFactor);
        }

        private void ScrollBy(float xDelta, float yDelta)
        {
            Graph.ScrollBy(xDelta, yDelta);
        }

        private void ScrollTo(float x, float y)
        {
            Graph.ScrollTo(x, y);
        }

        private void ShowPropertiesDialog()
        {
            PropertiesDialogController.ShowDialog();
        }

        private void Zoom(float factor)
        {
            Graph.Zoom(factor);
        }
    }
}
