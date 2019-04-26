namespace Sid.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Windows.Forms;
    using Sid.Expressions;
    using Sid.Models;
    using Sid.Views;

    public class AppController
    {
        public AppController()
        {
            Expressions.TestAll();
            View = new MainForm();
            Model = new Model();
            Model.Cleared += Model_Cleared;
            Model.ModifiedChanged += Model_ModifiedChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            JsonController = new JsonController(Model, View, View.FileReopen);
            JsonController.FileLoaded += JsonController_FileLoaded;
            JsonController.FilePathChanged += JsonController_FilePathChanged;
            JsonController.FileSaving += JsonController_FileSaving;
            JsonController.FileSaved += JsonController_FileSaved;
            LegendController = new LegendController(this);
            AdjustPictureBox();
            UpdateCaption();
        }

        #region Properties

        public readonly Model Model;
        public readonly JsonController JsonController;
        public readonly LegendController LegendController;

        public Panel ClientPanel { get => View.ClientPanel; }
        public Graph Graph { get => Model.Graph; }
        public PictureBox PictureBox { get => View.PictureBox; }

        private Point DragFrom;
        private bool Dragging;
        private Point MouseDownAt;
        private FormWindowState PriorWindowState;

        private bool FullScreen
        {
            get => View.ViewFullScreen.Checked;
            set
            {
                View.ViewFullScreen.Checked = value;
                AdjustFullScreen();
            }
        }

        private bool Isotropic
        {
            get => View.ViewIsotropic.Checked;
            set
            {
                View.ViewIsotropic.Checked = value;
                AdjustPictureBox();
            }
        }

        private bool ShowEditor
        {
            get => View.ViewLegend.Checked;
            set
            {
                View.ViewLegend.Checked = value;
                AdjustPictureBox();
            }
        }

        private bool ShowMouseCoordinates
        {
            get => View.ViewMouseCoordinates.Checked;
            set
            {
                View.ViewMouseCoordinates.Checked = value;
                if (!value)
                    InitCoordinatesToolTip(string.Empty);
            }
        }

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
                View.ViewZoomIn.Click += ViewZoomIn_Click;
                View.ViewZoomOut.Click += ViewZoomOut_Click;
                View.ViewZoomReset.Click += ViewZoomReset_Click;
                View.ViewScrollLeft.Click += ViewScrollLeft_Click;
                View.ViewScrollRight.Click += ViewScrollRight_Click;
                View.ViewScrollUp.Click += ViewScrollUp_Click;
                View.ViewScrollDown.Click += ViewScrollDown_Click;
                View.ViewScrollCentre.Click += ViewScrollCentre_Click;
                View.ViewLegend.Click += ViewEditor_Click;
                View.ViewFullScreen.Click += ViewFullScreen_Click;
                View.ViewIsotropic.Click += ViewIsotropic_Click;
                View.ViewMouseCoordinates.Click += ViewMouseCoordinates_Click;
                View.HelpAbout.Click += HelpAbout_Click;
                PictureBox.MouseDown += PictureBox_MouseDown;
                PictureBox.MouseMove += PictureBox_MouseMove;
                PictureBox.MouseUp += PictureBox_MouseUp;
                PictureBox.MouseWheel += PictureBox_MouseWheel;
                PictureBox.Paint += PictureBox_Paint;
                PictureBox.Resize += PictureBox_Resize;
            }
        }

        #endregion

        #region Menus

        public void FileMenu_DropDownOpening(object sender, EventArgs e) => View.FileSave.Enabled = Model.Modified;
        private void FileNew_Click(object sender, EventArgs e) => NewFile();
        private void FileOpen_Click(object sender, EventArgs e) => OpenFile();
        private void FileSave_Click(object sender, EventArgs e) => JsonController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => JsonController.SaveAs();
        private void FileExit_Click(object sender, EventArgs e) => View.Close();
        private void ViewZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ViewZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ViewZoomReset_Click(object sender, EventArgs e) => ZoomReset();
        private void ViewScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1, 0);
        private void ViewScrollRight_Click(object sender, EventArgs e) => Scroll(0.1, 0);
        private void ViewScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1);
        private void ViewScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1);
        private void ViewScrollCentre_Click(object sender, EventArgs e) => ScrollTo(0, 0);
        private void ViewEditor_Click(object sender, EventArgs e) => ToggleEditor();
        private void ViewFullScreen_Click(object sender, EventArgs e) => ToggleFullScreen();
        private void ViewIsotropic_Click(object sender, EventArgs e) => Isotropic = !Isotropic;
        private void ViewMouseCoordinates_Click(object sender, EventArgs e) => ToggleMouseCoordinates();
        private void HelpAbout_Click(object sender, EventArgs e) => ShowVersionInfo();

        private void ShowVersionInfo()
        {
            /*
            MessageBox.Show(
                $@"Company Name: {Application.CompanyName}
Product Name: {Application.ProductName}
Version: {Application.ProductVersion}",
                $"About {Application.ProductName}");
                */
            Debug.WriteLine("GRAPH INFO:");
            for (int i = 0; i < Graph.Series.Count; i++)
            {
                var s = Graph.Series[i];
                Debug.WriteLine($"f{i}: '{s.Formula}'");
            }
            Debug.WriteLine("EDIT CONTROL INFO:");
            for (int j = 0; j < LegendController.Children.Count; j++)
            {
                var t = LegendController.Children[j];
                Debug.WriteLine($"f{j}: '{t.Formula}'");
            }
        }

        #endregion

        #region Model

        private void Model_Cleared(object sender, EventArgs e) => ModelCleared();
        private void Model_ModifiedChanged(object sender, EventArgs e) => ModifiedChanged();
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Model.{e.PropertyName}");

        private void ModelCleared() => InitPaper();

        private void ModifiedChanged()
        {
            UpdateCaption();
            View.ModifiedLabel.Visible = Model.Modified;
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            System.Diagnostics.Debug.WriteLine($"Controller.OnPropertyChanged(\"{propertyName}\")");
            if (propertyName == "Model.Graph.FillColour")
                InitPaper();
            PictureBox.Invalidate();
        }

        #endregion

        #region PictureBox

        private void View_Resize(object sender, EventArgs e) => AdjustPictureBox();

        private void PictureBox_MouseDown(object sender, MouseEventArgs e)
        {
            switch (e.Button)
            {
                case MouseButtons.Left:
                    Dragging = true;
                    PictureBox.Cursor = Cursors.Hand;
                    MouseDownAt = e.Location;
                    DragFrom = PictureBox.Location;
                    break;
                case MouseButtons.Middle: // Click wheel
                    ZoomReset();
                    break;
                case MouseButtons.Right:
                    break;
            }
        }

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
                PictureBox.Location = new Point(
                    PictureBox.Left - MouseDownAt.X + e.X,
                    PictureBox.Top - MouseDownAt.Y + e.Y);
            else
                UpdateMouseCoordinates(e);
        }

        private void PictureBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (Dragging)
            {
                PointF p = ScreenToGraph(DragFrom), q = ScreenToGraph(PictureBox.Location);
                ScrollBy(p.X - q.X, p.Y - q.Y);
                AdjustPictureBox();
                PictureBox.Cursor = Cursors.Default;
                Dragging = false;
            }
        }

        private void PictureBox_MouseWheel(object sender, MouseEventArgs e) =>
            Zoom((float)Math.Pow(e.Delta > 0 ? 10.0 / 11.0 : 11.0 / 10.0,
                Math.Abs(e.Delta / SystemInformation.MouseWheelScrollDelta)));

        private void PictureBox_Paint(object sender, PaintEventArgs e) =>
            Graph.Draw(e.Graphics, PictureBox.ClientRectangle);

        private void PictureBox_Resize(object sender, EventArgs e) => PictureBox.Invalidate();

        private void AdjustFullScreen()
        {
            var normal = !FullScreen;
            View.MainMenuStrip.Visible = normal;
            View.StatusBar.Visible = normal;
            if (FullScreen)
            {
                View.FormBorderStyle = FormBorderStyle.None;
                PriorWindowState = View.WindowState;
                View.WindowState = FormWindowState.Maximized;
                MoveMenuItems(View.MainMenu, View.PopupMenu);
            }
            else
            {
                View.FormBorderStyle = FormBorderStyle.Sizable;
                View.WindowState = PriorWindowState;
                MoveMenuItems(View.PopupMenu, View.MainMenu);
            }
        }

        private void AdjustPictureBox()
        {
            int cW = ClientPanel.ClientSize.Width, cH = ClientPanel.ClientSize.Height;
            var r = new Rectangle(0, 0, cW, cH);
            if (Isotropic)
            {
                float gW = Graph.Size.Width, gH = Graph.Size.Height;
                if (gW > gH * cW / cH)
                {
                    var h = gH * cW / gW;
                    r.Y = (int)Math.Round((cH - h) / 2);
                    r.Height = (int)(Math.Round(h));
                }
                else
                {
                    var w = gW * cH / gH;
                    r.X = (int)Math.Round((cW - w) / 2);
                    r.Width = (int)(Math.Round(w));
                }
            }
            PictureBox.SetBounds(r.X, r.Y, r.Width, r.Height);
        }

        private void InitCoordinatesToolTip(string text) => View.ToolTip.SetToolTip(PictureBox, text);
        private void InitPaper() => ClientPanel.BackColor = Graph.FillColour;

        private static void MoveMenuItems(ToolStrip source, ToolStrip target)
        {
            while (source.Items.Count > 0)
            {
                var item = source.Items[0];
                source.Items.RemoveAt(0);
                target.Items.Add(item);
            }
        }

        private PointF ScreenToGraph(Point p) => Graph.ScreenToGraph(p, PictureBox.ClientRectangle);
        private void ToggleEditor() => ShowEditor = !ShowEditor;
        private void ToggleFullScreen() => FullScreen = !FullScreen;
        private void ToggleMouseCoordinates() => ShowMouseCoordinates = !ShowMouseCoordinates;

        private void UpdateMouseCoordinates(MouseEventArgs e)
        {
            if (ShowMouseCoordinates)
                InitCoordinatesToolTip(ScreenToGraph(e.Location).ToString());
        }

        #endregion

        #region Scroll & Zoom

        private void Scroll(double xFactor, double yFactor) => Graph.Scroll(xFactor, yFactor);
        private void ScrollBy(float xDelta, float yDelta) => Graph.ScrollBy(xDelta, yDelta);
        private void ScrollTo(float x, float y) => Graph.ScrollTo(x, y);
        private void Zoom(float factor) => Graph.Zoom(factor);
        private void ZoomReset() => Graph.ZoomReset();

        #endregion

        #region JsonController

        private void JsonController_FileLoaded(object sender, EventArgs e) => FileLoaded();

        private void JsonController_FilePathChanged(object sender, EventArgs e) =>
            UpdateCaption();

        private void JsonController_FileSaved(object sender, EventArgs e) => FileSaved();

        private void JsonController_FileSaving(object sender, CancelEventArgs e) =>
            e.Cancel = !ContinueSaving();

        private void View_FormClosing(object sender, FormClosingEventArgs e) =>
            e.Cancel = !JsonController.SaveIfModified();

        private bool ContinueSaving() => true;
        private void FileLoaded() { Graph.ZoomSet(); InitPaper(); }
        private void FileSaved() => Graph.ZoomSet();

        private void NewFile()
        {
            JsonController.Clear();
            LegendController.GraphRead();
        }

        private void OpenFile()
        {
            JsonController.Open();
            LegendController.GraphRead();
        }

        private void UpdateCaption() => View.Text = JsonController.WindowCaption;

        #endregion
    }
}
