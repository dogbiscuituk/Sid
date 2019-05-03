namespace Sid.Controllers
{
    using System;
    using System.ComponentModel;
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
            View = new AppForm();
            Model = new Model();
            Model.Cleared += Model_Cleared;
            Model.ModifiedChanged += Model_ModifiedChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            PropertiesController = new PropertiesController(Model);
            MathboardController = new MathboardController();
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
        public readonly PropertiesController PropertiesController;
        public readonly MathboardController MathboardController;
        public readonly JsonController JsonController;
        public readonly LegendController LegendController;

        public Panel ClientPanel { get => View.ClientPanel; }
        public Graph Graph { get => Model.Graph; }
        public PictureBox PictureBox { get => View.PictureBox; }

        private Clock Clock = new Clock();
        private Point DragFrom;
        private bool Dragging;
        private Point MouseDownAt;
        private FormWindowState PriorWindowState;

        private bool FullScreen
        {
            get => View.ZoomFullScreen.Checked;
            set
            {
                View.ZoomFullScreen.Checked = value;
                AdjustFullScreen();
            }
        }

        private bool ShowCoordinatesTooltip
        {
            get => View.ViewCoordinatesTooltip.Checked;
            set
            {
                View.ViewCoordinatesTooltip.Checked = value;
                if (!value)
                    InitCoordinatesToolTip(string.Empty);
            }
        }

        private AppForm _view;
        public AppForm View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    Clock.Stop();
                    Clock.Tick -= Clock_Tick;
                    View.FormClosing -= View_FormClosing;
                    View.Resize -= View_Resize;
                    View.FileMenu.DropDownOpening -= FileMenu_DropDownOpening;
                    View.FileNew.Click -= FileNew_Click;
                    View.FileOpen.Click -= FileOpen_Click;
                    View.FileSave.Click -= FileSave_Click;
                    View.FileSaveAs.Click -= FileSaveAs_Click;
                    View.FileExit.Click -= FileExit_Click;
                    View.GraphProperties.Click -= GraphProperties_Click;
                    View.ViewCoordinatesTooltip.Click -= ViewCoordinatesTooltip_Click;
                    View.ViewMathboard.Click -= ViewMathboard_Click;
                    View.ZoomMenu.DropDownOpening -= ZoomMenu_DropDownOpening;
                    View.ZoomIn.Click -= ZoomIn_Click;
                    View.ZoomOut.Click -= ZoomOut_Click;
                    View.ZoomReset.Click -= ZoomReset_Click;
                    View.ZoomFullScreen.Click -= ZoomFullScreen_Click;
                    View.ZoomIsotropic.Click -= ZoomIsotropic_Click;
                    View.ScrollLeft.Click -= ScrollLeft_Click;
                    View.ScrollRight.Click -= ScrollRight_Click;
                    View.ScrollUp.Click -= ScrollUp_Click;
                    View.ScrollDown.Click -= ScrollDown_Click;
                    View.ScrollCentre.Click -= ScrollCentre_Click;
                    View.TimerMenu.DropDownOpening -= TimerMenu_DropDownOpening;
                    View.TimerRunPause.Click -= TimerRunPause_Click;
                    View.TimerReset.Click -= TimerReset_Click;
                    View.TimerInterval.SelectedIndexChanged -= TimerInterval_SelectedIndexChanged;
                    View.HelpAbout.Click -= HelpAbout_Click;
                    PictureBox.MouseDown -= PictureBox_MouseDown;
                    PictureBox.MouseLeave -= PictureBox_MouseLeave;
                    PictureBox.MouseMove -= PictureBox_MouseMove;
                    PictureBox.MouseUp -= PictureBox_MouseUp;
                    PictureBox.MouseWheel -= PictureBox_MouseWheel;
                    PictureBox.Paint -= PictureBox_Paint;
                    PictureBox.Resize -= PictureBox_Resize;
                }
                _view = value;
                if (View != null)
                {
                    View.FormClosing += View_FormClosing;
                    View.Resize += View_Resize;
                    View.FileMenu.DropDownOpening += FileMenu_DropDownOpening;
                    View.FileNew.Click += FileNew_Click;
                    View.FileOpen.Click += FileOpen_Click;
                    View.FileSave.Click += FileSave_Click;
                    View.FileSaveAs.Click += FileSaveAs_Click;
                    View.FileExit.Click += FileExit_Click;
                    View.GraphProperties.Click += GraphProperties_Click;
                    View.ViewCoordinatesTooltip.Click += ViewCoordinatesTooltip_Click;
                    View.ViewMathboard.Click += ViewMathboard_Click;
                    View.ZoomMenu.DropDownOpening += ZoomMenu_DropDownOpening;
                    View.ZoomIn.Click += ZoomIn_Click;
                    View.ZoomOut.Click += ZoomOut_Click;
                    View.ZoomReset.Click += ZoomReset_Click;
                    View.ZoomIsotropic.Click += ZoomIsotropic_Click;
                    View.ZoomFullScreen.Click += ZoomFullScreen_Click;
                    View.ScrollLeft.Click += ScrollLeft_Click;
                    View.ScrollRight.Click += ScrollRight_Click;
                    View.ScrollUp.Click += ScrollUp_Click;
                    View.ScrollDown.Click += ScrollDown_Click;
                    View.ScrollCentre.Click += ScrollCentre_Click;
                    View.TimerMenu.DropDownOpening += TimerMenu_DropDownOpening;
                    View.TimerRunPause.Click += TimerRunPause_Click;
                    View.TimerReset.Click += TimerReset_Click;
                    View.TimerInterval.SelectedIndexChanged += TimerInterval_SelectedIndexChanged;
                    View.HelpAbout.Click += HelpAbout_Click;
                    PictureBox.MouseDown += PictureBox_MouseDown;
                    PictureBox.MouseLeave += PictureBox_MouseLeave;
                    PictureBox.MouseMove += PictureBox_MouseMove;
                    PictureBox.MouseUp += PictureBox_MouseUp;
                    PictureBox.MouseWheel += PictureBox_MouseWheel;
                    PictureBox.Paint += PictureBox_Paint;
                    PictureBox.Resize += PictureBox_Resize;
                    Clock = new Clock { Sync = View };
                    Clock.Tick += Clock_Tick;
                }
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
        private void GraphProperties_Click(object sender, EventArgs e) => PropertiesController.Show(View);
        private void ViewMathboard_Click(object sender, EventArgs e) => MathboardController.Show(View, null);
        private void ZoomMenu_DropDownOpening(object sender, EventArgs e) => View.ZoomIsotropic.Checked = Graph.Isotropic;
        private void ZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ZoomReset_Click(object sender, EventArgs e) => ZoomReset();
        private void ZoomIsotropic_Click(object sender, EventArgs e) => Graph.Isotropic = !Graph.Isotropic;
        private void ZoomFullScreen_Click(object sender, EventArgs e) => ToggleFullScreen();
        private void ScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1, 0);
        private void ScrollRight_Click(object sender, EventArgs e) => Scroll(0.1, 0);
        private void ScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1);
        private void ScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1);
        private void ScrollCentre_Click(object sender, EventArgs e) => ScrollTo(0, 0);

        public void HideMathboard(Control sender) => MathboardController.Hide();
        public void ShowMathboard(Control sender) => MathboardController.Show(View, sender);

        private void TimerMenu_DropDownOpening(object sender, EventArgs e)
        {
            View.TimerRunPause.Checked = Clock.Running;
            View.TimerInterval.SelectedIndex = View.TimerInterval.Items.IndexOf(Clock.Tick_ms.ToString());
        }

        private void TimerRunPause_Click(object sender, EventArgs e) => Clock.Running = !Clock.Running;
        private void TimerReset_Click(object sender, EventArgs e)
        {
            Clock.Reset();
            UpdateTlabel();
        }

        private void TimerInterval_SelectedIndexChanged(object sender, EventArgs e) => Clock.Tick_ms = Get_ms();

        private void ViewCoordinatesTooltip_Click(object sender, EventArgs e) => ToggleCoordinatesTooltip();
        private void HelpAbout_Click(object sender, EventArgs e) => ShowVersionInfo();

        private void ShowVersionInfo()
        {
            MessageBox.Show(
                $@"Company Name: {Application.CompanyName}
Product Name: {Application.ProductName}
Version: {Application.ProductVersion}",
                $"About {Application.ProductName}");
        }

        private int Get_ms() => Get_ms(View.TimerInterval.SelectedItem);
        private int Get_ms(object item) => int.Parse(item.ToString());

        #endregion

        #region Model

        private void Model_Cleared(object sender, EventArgs e) => ModelCleared();

        private void Model_ClockTick(object sender, EventArgs e)
        {
            InvalidatePictureBox();
        }

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
            if (propertyName == "Model.Graph.FillColour")
                InitPaper();
            if (propertyName == "Model.Graph.Isotropic")
                AdjustPictureBox();
            InvalidatePictureBox();
        }

        #endregion

        #region Clock

        private void Clock_Tick(object sender, EventArgs e)
        {
            System.Diagnostics.Debug.WriteLine(Clock.TimeElapsed);

            UpdateTlabel();
            InvalidatePictureBox();

        }

        private void UpdateTlabel()
        {
            View.Tlabel.Text = $"T={Clock.SecondsElapsed}";
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

        private void PictureBox_MouseLeave(object sender, EventArgs e) =>
            UpdateMouseCoordinates(string.Empty);

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
            Graph.Draw(e.Graphics, PictureBox.ClientRectangle, Clock.SecondsElapsed);

        private void PictureBox_Resize(object sender, EventArgs e) => InvalidatePictureBox();

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
            if (Graph.Isotropic)
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
        private void InvalidatePictureBox() => PictureBox.Invalidate();

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
        private void ToggleCoordinatesTooltip() => ShowCoordinatesTooltip = !ShowCoordinatesTooltip;
        private void ToggleFullScreen() => FullScreen = !FullScreen;

        private void UpdateMouseCoordinates(MouseEventArgs e)
        {
            UpdateMouseCoordinates(ScreenToGraph(e.Location).ToString());
        }

        private void UpdateMouseCoordinates(string coords)
        {
            View.XYlabel.Text = coords;
            if (ShowCoordinatesTooltip)
                InitCoordinatesToolTip(coords);
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

        private void FileLoaded()
        {
            Graph.ZoomSet();
            InitPaper();
            LegendController.GraphRead();
        }

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
