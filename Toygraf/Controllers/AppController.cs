namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
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
            PictureBoxController = new PictureBoxController(this);
            PropertiesController = new PropertiesController(this);
            MathController = new KeyboardController(this);
            JsonController = new JsonController(Model, View, View.FileReopen);
            JsonController.FileLoaded += JsonController_FileLoaded;
            JsonController.FilePathChanged += JsonController_FilePathChanged;
            JsonController.FileSaving += JsonController_FileSaving;
            JsonController.FileSaved += JsonController_FileSaved;
            LegendController = new LegendController(this);
            ModifiedChanged();
            LegendController.AdjustLegend();
            UpdateUI();
            PopupMenu_Opening(View, new CancelEventArgs());
        }

        #region Properties

        public readonly Model Model;
        public readonly JsonController JsonController;
        public readonly KeyboardController MathController;
        public readonly LegendController LegendController;
        public readonly PictureBoxController PictureBoxController;
        public readonly PropertiesController PropertiesController;

        public Panel ClientPanel { get => View.ClientPanel; }
        public Graph Graph { get => Model.Graph; }
        public PictureBox PictureBox { get => View.PictureBox; }

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
                    // Form
                    View.FormClosing -= View_FormClosing;
                    // Main Menu
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
                    View.TimerReverse.Click -= TimerReverse_Click;
                    View.TimerReset.Click -= TimerReset_Click;
                    View.HelpAbout.Click -= HelpAbout_Click;
                    // PopupMenu
                    View.PopupMenu.Opening -= PopupMenu_Opening;
                    // Toolbar
                    View.tbNew.Click -= FileNew_Click;
                    View.tbOpen.ButtonClick -= FileOpen_Click;
                    View.tbOpen.DropDownOpening -= TbOpen_DropDownOpening;
                    View.tbSave.Click -= FileSaveAs_Click;
                    View.tbCartesian.Click -= GraphTypeCartesian_Click;
                    View.tbPolar.Click -= GraphTypePolar_Click;
                    View.tbProperties.Click -= GraphProperties_Click;
                    View.tbFullScreen.Click -= ZoomFullScreen_Click;
                    View.tbTimer.ButtonClick -= TimerRunPause_Click;
                    View.tbTimer.DropDownOpening -= TbTimer_DropDownOpening;
                    View.TimeTrackBar.ValueChanged -= TimeTrackBar_ValueChanged;
                }
                _view = value;
                if (View != null)
                {
                    // Form
                    View.FormClosing += View_FormClosing;
                    // Main Menu
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
                    View.TimerReverse.Click += TimerReverse_Click;
                    View.TimerReset.Click += TimerReset_Click;
                    View.HelpAbout.Click += HelpAbout_Click;
                    // PopupMenu
                    View.PopupMenu.Opening += PopupMenu_Opening;
                    // Toolbar
                    View.tbNew.Click += FileNew_Click;
                    View.tbOpen.ButtonClick += FileOpen_Click;
                    View.tbOpen.DropDownOpening += TbOpen_DropDownOpening;
                    View.tbSave.Click += FileSaveAs_Click;
                    View.tbCartesian.Click += GraphTypeCartesian_Click;
                    View.tbPolar.Click += GraphTypePolar_Click;
                    View.tbProperties.Click += GraphProperties_Click;
                    View.tbFullScreen.Click += ZoomFullScreen_Click;
                    View.tbTimer.ButtonClick += TimerRunPause_Click;
                    View.tbTimer.DropDownOpening += TbTimer_DropDownOpening;
                    View.TimeTrackBar.ValueChanged += TimeTrackBar_ValueChanged;
                }
            }
        }

        #endregion

        #region Menu events

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
        private void TimerMenu_DropDownOpening(object sender, EventArgs e) => View.TimerRunPause.Checked = PictureBoxController.Clock.Running;
        private void TimerRunPause_Click(object sender, EventArgs e) => PictureBoxController.Clock.Running = !PictureBoxController.Clock.Running;
        private void TimerReverse_Click(object sender, EventArgs e) => TimerReverse = !TimerReverse;
        private void TimerReset_Click(object sender, EventArgs e) => PictureBoxController.ClockReset();
        private void ViewCoordinatesTooltip_Click(object sender, EventArgs e) => ToggleCoordinatesTooltip();
        private void HelpAbout_Click(object sender, EventArgs e) => new AboutController().ShowDialog(View);

        private void PopupMenu_Opening(object sender, CancelEventArgs e) => View.MainMenu.CloneTo(View.PopupMenu);
        private void TbOpen_DropDownOpening(object sender, EventArgs e) => View.FileReopen.CloneTo(View.tbOpen);
        private void TbTimer_DropDownOpening(object sender, EventArgs e) => View.TimerMenu.CloneTo(View.tbTimer);
        private void TimeTrackBar_ValueChanged(object sender, EventArgs e) => PictureBoxController.UpdateVirtualTimeFactor();

        #endregion

        #region Model

        private void Model_Cleared(object sender, EventArgs e) => ModelCleared();
        private void Model_ClockTick(object sender, EventArgs e) => PictureBoxController.InvalidateView();
        private void Model_ModifiedChanged(object sender, EventArgs e) => ModifiedChanged();
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            OnPropertyChanged($"Model.{e.PropertyName}");

        private void ModelCleared() => InitPaper();

        private void ModifiedChanged()
        {
            UpdateCaption();
            View.FileSave.Enabled = Model.Modified;
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

        public bool TimerReverse
        {
            get => View.TimerReverse.Checked;
            set
            {
                View.TimerReverse.Checked = value;
                PictureBoxController.UpdateVirtualTimeFactor();
            }
        }

        public int TimerSign { get => TimerReverse ? -1 : +1; }

        public void UpdateLabels(double virtualSecondsElapsed, double fps)
        {
            View.Tlabel.Text = string.Format("t={0:f1}", virtualSecondsElapsed);
            View.FPSlabel.Text = string.Format("fps={0:f1}", fps);
        }

        #endregion

        #region PictureBox

        private void AdjustFullScreen()
        {
            var normal = !FullScreen;
            View.MainMenuStrip.Visible =
                View.Toolbar.Visible =
                View.TimeTrackBar.Visible =
                View.StatusBar.Visible =normal;
            if (FullScreen)
            {
                PriorLegendVisible = View.LegendPanel.Visible;
                View.LegendPanel.Visible = false;
                View.FormBorderStyle = FormBorderStyle.None;
                PriorWindowState = View.WindowState;
                View.WindowState = FormWindowState.Maximized;
            }
            else
            {
                View.LegendPanel.Visible = PriorLegendVisible;
                View.FormBorderStyle = FormBorderStyle.Sizable;
                View.WindowState = PriorWindowState;
            }
        }

        private void InitCoordinatesToolTip(string toolTip)
        {
            if (View.ToolTip.GetToolTip(PictureBox) != toolTip)
                View.ToolTip.SetToolTip(PictureBox, toolTip);
        }

        private void InitPaper() => ClientPanel.BackColor = Graph.PaperColour;

        private void ToggleCoordinatesTooltip() => ShowCoordinatesTooltip = !ShowCoordinatesTooltip;
        private void ToggleFullScreen() => FullScreen = !FullScreen;

        public void UpdateMouseCoordinates(PointF p)
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

        public void Scroll(float xFactor, float yFactor) => Graph.Scroll(xFactor, yFactor);
        public void ScrollBy(float xDelta, float yDelta) => Graph.ScrollBy(xDelta, yDelta);
        public void ScrollTo(float x, float y) => Graph.ScrollTo(x, y);
        public void Zoom(float factor) => Graph.Zoom(factor);
        public void ZoomReset() => Graph.ZoomReset();

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
            Graph.InvalidateReticle();
            PictureBoxController.InvalidateView();
            UpdateUI();
        }

        private void OpenFile() { JsonController.Open(); }
        private void UpdateCaption() { View.Text = JsonController.WindowCaption; }

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
                    PictureBoxController.AdjustPictureBox();
                    UpdatePlotType();
                    break;
            }
            PictureBoxController.InvalidateView();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
