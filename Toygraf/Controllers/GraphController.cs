namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;

    internal class GraphController : IDisposable
    {
        #region Internal Interface

        internal GraphController()
        {
            View = new GraphForm { MinimumSize = Properties.Settings.Default.GraphForm_MinimumSize };
            Model = new Model();
            Model.Cleared += Model_Cleared;
            Model.ModifiedChanged += Model_ModifiedChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            GraphicsController = new GraphicsController(this, doubleBuffered: false);
            JsonController = new JsonController(Model, View, View.FileReopen);
            JsonController.FileLoaded += JsonController_FileLoaded;
            JsonController.FilePathChanged += JsonController_FilePathChanged;
            JsonController.FilePathRequest += JsonController_FilePathRequest;
            JsonController.FileSaving += JsonController_FileSaving;
            JsonController.FileSaved += JsonController_FileSaved;
            LegendController = new LegendController(this);
            GraphPropertiesController = new GraphPropertiesController(this);
            TracePropertiesController = new TracePropertiesController(this);
            ToolbarController = new ToolbarController(this);
            PropertyGridController = new PropertyGridController(this);
            ModifiedChanged();
            LegendController.AdjustLegend();
            UpdateUI();
            PopupMenu_Opening(View, new CancelEventArgs());
            GraphProxy = new GraphProxy(this);
            View.PropertyTable.SelectedObject = GraphProxy;
        }

        internal GraphForm View
        {
            get => _view;
            set
            {
                _view = value;
                View.FileNew.Click += FileNew_Click;
                View.tbNew.Click += FileNew_Click;
                View.FileOpen.Click += FileOpen_Click;
                View.tbOpen.ButtonClick += FileOpen_Click;
                View.tbOpen.DropDownOpening += TbOpen_DropDownOpening;
                View.FileSave.Click += FileSave_Click;
                View.FileSaveAs.Click += FileSaveAs_Click;
                View.tbSave.Click += TbSave_Click;
                View.FileExit.Click += FileExit_Click;
                View.GraphProperties.Click += GraphProperties_Click;
                View.tbProperties.Click += GraphProperties_Click;
                View.ZoomFullScreen.Click += ZoomFullScreen_Click;
                View.tbFullScreen.Click += ZoomFullScreen_Click;
                View.ViewCoordinatesTooltip.Click += ViewCoordinatesTooltip_Click;
                View.HelpAbout.Click += HelpAbout_Click;
                View.PopupMenu.Opening += PopupMenu_Opening;
                View.FormClosing += View_FormClosing;
                View.SizeChanged += View_SizeChanged;
            }
        }

        internal readonly Model Model;
        internal Graph Graph { get => Model.Graph; }

        internal ClockController ClockController => GraphicsController.ClockController;
        internal readonly GraphProxy GraphProxy;
        internal readonly LegendController LegendController;
        internal TracePropertiesController TracePropertiesController;

        internal bool ExecuteTextureDialog(Trace trace) => SelectTexture(trace);

        internal void Show() => View.Show();

        internal void UpdateMouseCoordinates(PointF p)
        {
            string
                xy = $"{{x={p.X}, y={p.Y}}}",
                rθ = new PolarPointF(p).ToString(Graph.DomainPolarDegrees);
            View.XYlabel.Text = xy;
            View.Rϴlabel.Text = rθ;
            if (ShowCoordinatesTooltip)
                InitCoordinatesToolTip($"{xy}\n{rθ}");
        }

        internal event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Properties

        private GraphForm _view;
        private Panel ClientPanel { get => View.ClientPanel; }
        private PictureBox PictureBox { get => View.PictureBox; }

        private readonly GraphicsController GraphicsController;
        private readonly JsonController JsonController;
        private readonly GraphPropertiesController GraphPropertiesController;
        private readonly ToolbarController ToolbarController;
        private readonly PropertyGridController PropertyGridController;

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

        #endregion

        #region Private Event Handlers

        private void FileNew_Click(object sender, EventArgs e) => NewFile();
        private void FileOpen_Click(object sender, EventArgs e) => OpenFile();
        private void FileSave_Click(object sender, EventArgs e) => JsonController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => JsonController.SaveAs();
        private void FileExit_Click(object sender, EventArgs e) => View.Close();
        private void GraphProperties_Click(object sender, EventArgs e) => GraphPropertiesController.Show(View);
        private void ZoomFullScreen_Click(object sender, EventArgs e) => ToggleFullScreen();
        private void ViewCoordinatesTooltip_Click(object sender, EventArgs e) => ToggleCoordinatesTooltip();
        private void HelpAbout_Click(object sender, EventArgs e) => new AboutController().ShowDialog(View);
        private void PopupMenu_Opening(object sender, CancelEventArgs e) => View.MainMenu.CloneTo(View.PopupMenu);
        private void TbOpen_DropDownOpening(object sender, EventArgs e) => View.FileReopen.CloneTo(View.tbOpen);
        private void Model_Cleared(object sender, EventArgs e) => ModelCleared();
        private void Model_ModifiedChanged(object sender, EventArgs e) => ModifiedChanged();
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged($"Model.{e.PropertyName}");
        private void JsonController_FileLoaded(object sender, EventArgs e) => FileLoaded();
        private void JsonController_FilePathChanged(object sender, EventArgs e) => UpdateCaption();
        private void JsonController_FilePathRequest(object sender, SdiController.FilePathRequestEventArgs e) => FilePathRequest(e);
        private void JsonController_FileSaved(object sender, EventArgs e) => FileSaved();
        private void JsonController_FileSaving(object sender, CancelEventArgs e) => e.Cancel = !ContinueSaving();
        private void View_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = !FormClosing(e.CloseReason);

        private bool FormClosing(CloseReason closeReason)
        {
            var cancel = !JsonController.SaveIfModified();
            if (!cancel)
            {
                Model.Modified = false;
                AppController.TheAppController.Remove(this);
            }
            return !cancel;
        }

        private void TbSave_Click(object sender, EventArgs e)
        {
            if (View.FileSave.Enabled)
                FileSave_Click(sender, e);
            else
                FileSaveAs_Click(sender, e);
        }

        private void View_SizeChanged(object sender, EventArgs e) =>
            View.StatusBar.ShowItemToolTips = View.WindowState != FormWindowState.Maximized;

        #endregion

        #region Private Methods

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
            }
            else
            {
                View.LegendPanel.Visible = PriorLegendVisible;
                View.FormBorderStyle = FormBorderStyle.Sizable;
                View.WindowState = PriorWindowState;
            }
        }

        private bool ContinueSaving() => true;

        private void FileLoaded()
        {
            TracePropertiesController.Clear();
            LegendController.Clear();
            GraphProxy.Clear();
            Graph.ZoomSet();
            InitPaper();
            UpdateUI();
        }

        private void FilePathRequest(SdiController.FilePathRequestEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.FilePath))
                e.FilePath = Graph.Title.ToFilename();
            if (string.IsNullOrWhiteSpace(e.FilePath) && Graph.Traces.Count > 0)
                e.FilePath = Graph.Traces[0].Formula.ToFilename();
        }

        private void FileSaved() => Graph.ZoomSet();

        private static string ImageToBase64String(string filePath)
        {
            using (var image = Image.FromFile(filePath))
            using (var stream = new MemoryStream())
            {
                image.Save(stream, ImageFormat.Bmp);
                return Convert.ToBase64String(stream.GetBuffer());
            }
        }

        private void InitCoordinatesToolTip(string toolTip)
        {
            if (View.ToolTip.GetToolTip(PictureBox) != toolTip)
                View.ToolTip.SetToolTip(PictureBox, toolTip);
        }

        private void InitPaper() => ClientPanel.BackColor = Graph.PaperColour;
        private void ModelCleared() => InitPaper();

        private void ModifiedChanged()
        {
            UpdateCaption();
            View.FileSave.Enabled = Model.Modified;
            View.ModifiedLabel.Visible = Model.Modified;
        }

        private void NewFile()
        {
            if (JsonController.Clear())
            {
                TracePropertiesController.Clear();
                Graph.InvalidateReticle();
                GraphicsController.InvalidateView();
                UpdateUI();
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            switch (propertyName)
            {
                case "Model.Graph.PaperColour":
                    InitPaper();
                    break;
                case "Model.Graph.PlotType":
                    GraphicsController.AdjustPictureBox();
                    UpdatePlotType();
                    break;
                case "Model.Graph.Traces":
                    ClockController.UpdateTimeControls();
                    LegendController.GraphRead();
                    break;
                case string s when Regex.IsMatch(s, @"^Model\.Graph\.Traces\[\d+\].(Formula|Visible)$"):
                    ClockController.UpdateTimeControls();
                    break;
                case "Model.Graph.Title":
                    if (string.IsNullOrWhiteSpace(JsonController.FilePath))
                        JsonController.FilePath = Graph.Title.ToFilename();
                    break;
            }
            GraphicsController.InvalidateView();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenFile() => JsonController.Open();

        private bool SelectTexture(Trace trace)
        {
            var dialog = View.ImageOpenDialog;
            dialog.FileName = trace.TexturePath;
            bool ok = dialog.ShowDialog(View) == DialogResult.OK;
            if (ok)
            {
                var index = Graph.Traces.IndexOf(trace);
                var filePath = dialog.FileName;
                GraphProxy[index].TexturePath = filePath;
                GraphProxy[index].Texture = ImageToBase64String(filePath);
            }
            return ok;
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
            ClockController.UpdateTimeControls();
            LegendController.GraphRead();
        }

        private void ToggleCoordinatesTooltip() => ShowCoordinatesTooltip = !ShowCoordinatesTooltip;
        private void ToggleFullScreen() => FullScreen = !FullScreen;
        private void UpdateCaption() { View.Text = JsonController.WindowCaption; }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => DisposeTracePropertiesController();

        private void DisposeTracePropertiesController()
        {
            if (TracePropertiesController != null)
            {
                TracePropertiesController.Dispose();
                TracePropertiesController = null;
            }
        }

        #endregion
    }
}
