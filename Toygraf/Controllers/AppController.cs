namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Drawing;
    using System.Linq;
    using System.Reflection;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    public class AppController: INotifyPropertyChanged
    {
        public AppController()
        {
            View = new AppForm();
            Model = new Model();
            Model.Cleared += Model_Cleared;
            Model.ModifiedChanged += Model_ModifiedChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            PropertiesController = new PropertiesController(this);
            MathController = new MathController(this);
            JsonController = new JsonController(Model, View, View.FileReopen);
            JsonController.FileLoaded += JsonController_FileLoaded;
            JsonController.FilePathChanged += JsonController_FilePathChanged;
            JsonController.FileSaving += JsonController_FileSaving;
            JsonController.FileSaved += JsonController_FileSaved;
            LegendController = new LegendController(this);
            UpdateCaption();
            AdjustPictureBox();
            LegendController.AdjustLegend();
            UpdateUI();
        }

        #region Properties

        public readonly Model Model;
        public readonly PropertiesController PropertiesController;
        public readonly MathController MathController;
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
        private bool PriorLegendVisible;

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
                    // Clock
                    Clock.Stop();
                    Clock.Tick -= Clock_Tick;
                    // Form
                    View.FormClosing -= View_FormClosing;
                    View.Resize -= View_Resize;
                    // Main Menu
                    View.FileMenu.DropDownOpening -= FileMenu_DropDownOpening;
                    View.FileNew.Click -= FileNew_Click;
                    View.FileOpen.Click -= FileOpen_Click;
                    View.FileSave.Click -= FileSave_Click;
                    View.FileSaveAs.Click -= FileSaveAs_Click;
                    View.FileExit.Click -= FileExit_Click;
                    View.GraphTypeCartesian.Click -= GraphTypeCartesian_Click;
                    View.GraphTypePolar.Click -= GraphTypePolar_Click;
                    View.GraphProperties.Click -= GraphProperties_Click;
                    View.ViewCoordinatesTooltip.Click -= ViewCoordinatesTooltip_Click;
                    View.ZoomIn.Click -= ZoomIn_Click;
                    View.ZoomOut.Click -= ZoomOut_Click;
                    View.ZoomReset.Click -= ZoomReset_Click;
                    View.ZoomFullScreen.Click -= ZoomFullScreen_Click;
                    View.ScrollLeft.Click -= ScrollLeft_Click;
                    View.ScrollRight.Click -= ScrollRight_Click;
                    View.ScrollUp.Click -= ScrollUp_Click;
                    View.ScrollDown.Click -= ScrollDown_Click;
                    View.ScrollCentre.Click -= ScrollCentre_Click;
                    View.TimerMenu.DropDownOpening -= TimerMenu_DropDownOpening;
                    View.TimerRunPause.Click -= TimerRunPause_Click;
                    View.TimerReset.Click -= TimerReset_Click;
                    View.HelpAbout.Click -= HelpAbout_Click;
                    // Toolbar
                    View.tbNew.Click -= FileNew_Click;
                    View.tbOpen.Click -= FileOpen_Click;
                    View.tbSave.Click -= FileSaveAs_Click;
                    View.tbCartesian.Click -= GraphTypeCartesian_Click;
                    View.tbPolar.Click -= GraphTypePolar_Click;
                    View.tbProperties.Click -= GraphProperties_Click;
                    View.tbFullScreen.Click -= ZoomFullScreen_Click;
                    View.tbTimer.Click -= TimerRunPause_Click;
                    // PictureBox
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
                    // Form
                    View.FormClosing += View_FormClosing;
                    View.Resize += View_Resize;
                    // Main Menu
                    View.FileMenu.DropDownOpening += FileMenu_DropDownOpening;
                    View.FileNew.Click += FileNew_Click;
                    View.FileOpen.Click += FileOpen_Click;
                    View.FileSave.Click += FileSave_Click;
                    View.FileSaveAs.Click += FileSaveAs_Click;
                    View.FileExit.Click += FileExit_Click;
                    View.GraphTypeCartesian.Click += GraphTypeCartesian_Click;
                    View.GraphTypePolar.Click += GraphTypePolar_Click;
                    View.GraphProperties.Click += GraphProperties_Click;
                    View.ViewCoordinatesTooltip.Click += ViewCoordinatesTooltip_Click;
                    View.ZoomIn.Click += ZoomIn_Click;
                    View.ZoomOut.Click += ZoomOut_Click;
                    View.ZoomReset.Click += ZoomReset_Click;
                    View.ZoomFullScreen.Click += ZoomFullScreen_Click;
                    View.ScrollLeft.Click += ScrollLeft_Click;
                    View.ScrollRight.Click += ScrollRight_Click;
                    View.ScrollUp.Click += ScrollUp_Click;
                    View.ScrollDown.Click += ScrollDown_Click;
                    View.ScrollCentre.Click += ScrollCentre_Click;
                    View.TimerMenu.DropDownOpening += TimerMenu_DropDownOpening;
                    View.TimerRunPause.Click += TimerRunPause_Click;
                    View.TimerReset.Click += TimerReset_Click;
                    View.HelpAbout.Click += HelpAbout_Click;
                    // Toolbar
                    View.tbNew.Click += FileNew_Click;
                    View.tbOpen.Click += FileOpen_Click;
                    View.tbSave.Click += FileSaveAs_Click;
                    View.tbCartesian.Click += GraphTypeCartesian_Click;
                    View.tbPolar.Click += GraphTypePolar_Click;
                    View.tbProperties.Click += GraphProperties_Click;
                    View.tbFullScreen.Click += ZoomFullScreen_Click;
                    View.tbTimer.Click += TimerRunPause_Click;
                    // PictureBox
                    PictureBox.MouseDown += PictureBox_MouseDown;
                    PictureBox.MouseLeave += PictureBox_MouseLeave;
                    PictureBox.MouseMove += PictureBox_MouseMove;
                    PictureBox.MouseUp += PictureBox_MouseUp;
                    PictureBox.MouseWheel += PictureBox_MouseWheel;
                    PictureBox.Paint += PictureBox_Paint;
                    PictureBox.Resize += PictureBox_Resize;
                    // Clock
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
        private void GraphTypeCartesian_Click(object sender, EventArgs e) => Graph.PlotType = PlotType.Cartesian;
        private void GraphTypePolar_Click(object sender, EventArgs e) => Graph.PlotType = PlotType.Polar;
        private void GraphProperties_Click(object sender, EventArgs e) => PropertiesController.Show(View);
        private void ZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ZoomReset_Click(object sender, EventArgs e) => ZoomReset();
        private void ZoomFullScreen_Click(object sender, EventArgs e) => ToggleFullScreen();
        private void ScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1f, 0);
        private void ScrollRight_Click(object sender, EventArgs e) => Scroll(0.1f, 0);
        private void ScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1f);
        private void ScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1f);
        private void ScrollCentre_Click(object sender, EventArgs e) => ScrollTo(0, 0);

        private void TimerMenu_DropDownOpening(object sender, EventArgs e) =>
            View.TimerRunPause.Checked = Clock.Running;

        private void TimerRunPause_Click(object sender, EventArgs e) => Clock.Running = !Clock.Running;
        private void TimerReset_Click(object sender, EventArgs e) => ClockReset();
        private void ViewCoordinatesTooltip_Click(object sender, EventArgs e) => ToggleCoordinatesTooltip();
        private void HelpAbout_Click(object sender, EventArgs e) => ShowVersionInfo();

        private void ShowVersionInfo()
        {
            Assembly self = Assembly.GetExecutingAssembly();
            MessageBox.Show(
                $@"{self.GetCustomAttribute<AssemblyDescriptionAttribute>().Description}
by {Application.CompanyName}
version {Application.ProductVersion}
{self.GetCustomAttribute<AssemblyCopyrightAttribute>().Copyright}",
                $"About {Application.ProductName}");
        }

        #endregion

        #region Model

        private void Model_Cleared(object sender, EventArgs e) => ModelCleared();
        private void Model_ClockTick(object sender, EventArgs e) => InvalidatePictureBox();
        private void Model_ModifiedChanged(object sender, EventArgs e) => ModifiedChanged();
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Model.{e.PropertyName}");

        private void ModelCleared() => InitPaper();

        private void ModifiedChanged()
        {
            UpdateCaption();
            View.ModifiedLabel.Visible = Model.Modified;
        }

        private void UpdatePlotType()
        {
            View.GraphTypeCartesian.Checked =
                View.tbCartesian.Checked = Graph.PlotType == PlotType.Cartesian;
            View.GraphTypePolar.Checked =
                View.tbPolar.Checked = Graph.PlotType == PlotType.Polar;
        }

        private void UpdateUI()
        {
            UpdatePlotType();
            LegendController.GraphRead();
        }

        #endregion

        #region Clock

        private double Tick_ms = 100;
        private double[] Ticks = new double[64];
        private int TickCount, TickIndex;

        private void Clock_Tick(object sender, EventArgs e)
        {
            Clock.Tick_ms = Tick_ms;
            UpdateLabels();
        }

        private void ClockReset()
        {
            Clock.Reset();
            TickCount = 0;
            TickIndex = 0;
            Array.ForEach(Ticks, p => p = 0);
            UpdateLabels();
        }

        private void UpdateLabels()
        {
            var tick = Clock.SecondsElapsed;
            View.Tlabel.Text = string.Format("t={0:f1}", tick);
            Ticks[TickIndex = (TickIndex + 1) % Ticks.Length] = tick;
            if (TickCount < Ticks.Length - 1) TickCount++;
            var fps = 0.0;
            if (TickCount > 1)
            {
                var ticks = Ticks.Take(TickCount);
                fps = TickCount / (ticks.Max() - ticks.Min());
            }
            View.FPSlabel.Text = string.Format("fps={0:f1}", fps);
            InvalidatePictureBox();
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
            UpdateMouseCoordinates(PointF.Empty);

        private void PictureBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (Dragging)
                PictureBox.Location = new Point(
                    PictureBox.Left - MouseDownAt.X + e.X,
                    PictureBox.Top - MouseDownAt.Y + e.Y);
            else
                UpdateMouseCoordinates(ScreenToGraph(e.Location));
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

        private void PictureBox_Paint(object sender, PaintEventArgs e)
        {
            var r = PictureBox.ClientRectangle;
            Stopwatch stopwatch = null;
            if (Clock.Running)
            {
                stopwatch = new Stopwatch();
                stopwatch.Start();
            }
            Graph.Draw(e.Graphics, r, Clock.SecondsElapsed);
            if (stopwatch != null)
            {
                stopwatch.Stop();
                // That Graph.Draw() operation took ms = stopwatch.ElapsedMilliseconds.
                // Add 10% & round up to multiple of 10ms to get the time to next Draw.
                double
                    t = 10 * Math.Ceiling(stopwatch.ElapsedMilliseconds * 0.11),
                    fps = 2000 / (t + Tick_ms);
                Tick_ms = t;
            }
        }

        private void PictureBox_Resize(object sender, EventArgs e)
        {
            var w = PictureBox.Width;
            if (w != 0)
                Graph.Viewport.SetRatio(PictureBox.Height / w);
            InvalidatePictureBox();
        }

        private void AdjustFullScreen()
        {
            var normal = !FullScreen;
            View.MainMenuStrip.Visible =
                View.Toolbar.Visible =
                View.StatusBar.Visible = normal;
            if (FullScreen)
            {
                PriorLegendVisible = View.LegendPanel.Visible;
                View.LegendPanel.Visible = false;
                View.FormBorderStyle = FormBorderStyle.None;
                PriorWindowState = View.WindowState;
                View.WindowState = FormWindowState.Maximized;
                MoveMenuItems(View.MainMenu, View.PopupMenu);
            }
            else
            {
                View.LegendPanel.Visible = PriorLegendVisible;
                View.FormBorderStyle = FormBorderStyle.Sizable;
                View.WindowState = PriorWindowState;
                MoveMenuItems(View.PopupMenu, View.MainMenu);
            }
        }

        private void AdjustPictureBox() => PictureBox.Bounds = ClientPanel.ClientRectangle;

        private void InitCoordinatesToolTip(string toolTip)
        {
            if (View.ToolTip.GetToolTip(PictureBox) != toolTip)
                View.ToolTip.SetToolTip(PictureBox, toolTip);
        }

        private void InitPaper() => ClientPanel.BackColor = Graph.PaperColour;
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

        private void UpdateMouseCoordinates(PointF p)
        {
            string
                xy = $"{{x={p.X}, y={p.Y}}}",
                rθ = new PolarPointF(p).ToString(Graph.DomainPolarDegrees);
            View.XYlabel.Text = xy;
            View.Rϴlabel.Text = rθ;
            if (ShowCoordinatesTooltip)
                InitCoordinatesToolTip($"{xy}\n{rθ}");
        }

        #endregion

        #region Scroll & Zoom

        private void Scroll(float xFactor, float yFactor) => Graph.Scroll(xFactor, yFactor);
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
            UpdateUI();
        }

        private void FileSaved() => Graph.ZoomSet();

        private void NewFile()
        {
            JsonController.Clear();
            Graph.InvalidateGrid();
            InvalidatePictureBox();
            UpdateUI();
        }

        private void OpenFile() { JsonController.Open(); }
        private void UpdateCaption() => View.Text = JsonController.WindowCaption;

        #endregion

        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case "Model.Graph.PaperColour":
                    InitPaper();
                    break;
                case "Model.Graph.PlotType":
                    AdjustPictureBox();
                    UpdatePlotType();
                    break;
            }
            InvalidatePictureBox();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
