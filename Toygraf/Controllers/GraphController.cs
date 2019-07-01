namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Linq;
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
            GraphForm = new GraphForm { MinimumSize = Properties.Settings.Default.GraphForm_MinimumSize };
            Model = new Model();
            Model.Cleared += Model_Cleared;
            Model.ModifiedChanged += Model_ModifiedChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            GraphicsController = new GraphicsController(this, doubleBuffered: false);
            JsonController = new JsonController(Model, GraphForm, GraphForm.FileReopen);
            JsonController.FileLoaded += JsonController_FileLoaded;
            JsonController.FilePathChanged += JsonController_FilePathChanged;
            JsonController.FilePathRequest += JsonController_FilePathRequest;
            JsonController.FileReopen += JsonController_FileReopen;
            JsonController.FileSaving += JsonController_FileSaving;
            JsonController.FileSaved += JsonController_FileSaved;
            LegendController = new LegendController(this);
            GraphPropertiesController = new GraphPropertiesController(this);
            TracePropertiesController = new TracePropertiesController(this);
            ToolbarController = new ToolbarController(this);
            PropertyGridController = new PropertyGridController(this);
            FullScreenController = new FullScreenController(this);
            ModifiedChanged();
            LegendController.AdjustLegend();
            UpdateUI();
            PopupMenu_Opening(GraphForm, new CancelEventArgs());
            CommandProcessor = new CommandProcessor(this);
            TraceTableController = new TraceTableController(this);
            TraceTableController.SelectionChanged += TraceTableController_SelectionChanged;
        }

        private void TraceTableController_SelectionChanged(object sender, EventArgs e)
        {
            var selection = TraceTableController.Selection;
            PropertyGridController.SelectedObjects =
                !selection.Any()
                    ? new object[] { CommandProcessor }
                    : selection.ToArray();
        }

        internal GraphForm GraphForm
        {
            get => _GraphForm;
            set
            {
                _GraphForm = value;
                GraphForm.FileNewEmptyGraph.Click += FileNewEmptyGraph_Click;
                GraphForm.FileNewFromTemplate.Click += FileNewFromTemplate_Click;
                GraphForm.tbNew.ButtonClick += FileNewEmptyGraph_Click;
                GraphForm.tbNewEmptyGraph.Click += FileNewEmptyGraph_Click;
                GraphForm.tbNewFromTemplate.Click += FileNewFromTemplate_Click;
                GraphForm.FileOpen.Click += FileOpen_Click;
                GraphForm.tbOpen.ButtonClick += FileOpen_Click;
                GraphForm.tbOpen.DropDownOpening += TbOpen_DropDownOpening;
                GraphForm.FileSave.Click += FileSave_Click;
                GraphForm.FileSaveAs.Click += FileSaveAs_Click;
                GraphForm.tbSave.Click += TbSave_Click;
                GraphForm.FileClose.Click += FileClose_Click;
                GraphForm.FileExit.Click += FileExit_Click;
                GraphForm.EditOptions.Click += EditOptions_Click;
                GraphForm.GraphProperties.Click += GraphProperties_Click;
                GraphForm.tbProperties.Click += GraphProperties_Click;
                GraphForm.ViewCoordinatesTooltip.Click += ViewCoordinatesTooltip_Click;
                GraphForm.HelpAbout.Click += HelpAbout_Click;
                GraphForm.PopupMenu.Opening += PopupMenu_Opening;
                GraphForm.FormClosing += View_FormClosing;
                GraphForm.SizeChanged += View_SizeChanged;
                InitTextureDialog(GraphForm.TextureDialog);
            }
        }

        internal readonly Model Model;
        internal Graph Graph { get => Model.Graph; }

        internal ClockController ClockController => GraphicsController.ClockController;
        internal readonly CommandProcessor CommandProcessor;
        internal readonly TraceTableController TraceTableController;
        internal readonly LegendController LegendController;
        internal readonly PropertyGridController PropertyGridController;
        internal TracePropertiesController TracePropertiesController;

        internal bool ExecuteTextureDialog(Trace trace) => SelectTexture(trace);

        internal static void InitTextureDialog(OpenFileDialog dialog)
        {
            dialog.Filter = Properties.Settings.Default.ImageFilter;
            dialog.Title = "Select Texture";
        }

        internal void Show() => GraphForm.Show();

        internal void UpdateMouseCoordinates(PointF p)
        {
            string
                xy = $"{{x={p.X}, y={p.Y}}}",
                rθ = new PolarPointF(p).ToString(Graph.DomainPolarDegrees);
            GraphForm.XYlabel.Text = xy;
            GraphForm.Rϴlabel.Text = rθ;
            if (ShowCoordinatesTooltip)
                InitCoordinatesToolTip($"{xy}\n{rθ}");
        }

        internal event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Private Properties

        private GraphForm _GraphForm;
        private Panel ClientPanel { get => GraphForm.ClientPanel; }
        private PictureBox PictureBox { get => GraphForm.PictureBox; }

        private readonly FullScreenController FullScreenController;
        private readonly GraphicsController GraphicsController;
        private readonly GraphPropertiesController GraphPropertiesController;
        private readonly JsonController JsonController;
        private readonly ToolbarController ToolbarController;

        #endregion

        #region Private Event Handlers

        private void FileNewEmptyGraph_Click(object sender, EventArgs e) => NewEmptyGraph();
        private void FileNewFromTemplate_Click(object sender, EventArgs e) => NewFromTemplate();
        private void FileOpen_Click(object sender, EventArgs e) => OpenFile();
        private void FileSave_Click(object sender, EventArgs e) => JsonController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => JsonController.SaveAs();
        private void FileClose_Click(object sender, EventArgs e) => GraphForm.Close();
        private void FileExit_Click(object sender, EventArgs e) => AppController.Close();
        private void EditOptions_Click(object sender, EventArgs e) => EditOptions();
        private void GraphProperties_Click(object sender, EventArgs e) => GraphPropertiesController.Show(GraphForm);
        private void ViewCoordinatesTooltip_Click(object sender, EventArgs e) => ToggleCoordinatesTooltip();
        private void HelpAbout_Click(object sender, EventArgs e) => new AboutController().ShowDialog(GraphForm);
        private void PopupMenu_Opening(object sender, CancelEventArgs e) => GraphForm.MainMenu.CloneTo(GraphForm.PopupMenu);
        private void TbOpen_DropDownOpening(object sender, EventArgs e) => GraphForm.FileReopen.CloneTo(GraphForm.tbOpen);
        private void Model_Cleared(object sender, EventArgs e) => ModelCleared();
        private void Model_ModifiedChanged(object sender, EventArgs e) => ModifiedChanged();
        private void Model_PropertyChanged(object sender, PropertyChangedEventArgs e) => OnPropertyChanged($"Model.{e.PropertyName}");
        private void JsonController_FileLoaded(object sender, EventArgs e) => FileLoaded();
        private void JsonController_FilePathChanged(object sender, EventArgs e) => UpdateCaption();
        private void JsonController_FilePathRequest(object sender, SdiController.FilePathEventArgs e) => FilePathRequest(e);
        private void JsonController_FileReopen(object sender, SdiController.FilePathEventArgs e) => OpenFile(e.FilePath);
        private void JsonController_FileSaved(object sender, EventArgs e) => FileSaved();
        private void JsonController_FileSaving(object sender, CancelEventArgs e) => e.Cancel = !ContinueSaving();

        private void View_FormClosing(object sender, FormClosingEventArgs e) =>
            e.Cancel = !FormClosing(e.CloseReason);

        private bool FormClosing(CloseReason closeReason)
        {
            var cancel = !JsonController.SaveIfModified();
            if (!cancel)
            {
                Model.Modified = false;
                AppController.Remove(this);
            }
            return !cancel;
        }

        private void TbSave_Click(object sender, EventArgs e)
        {
            if (GraphForm.FileSave.Enabled)
                FileSave_Click(sender, e);
            else
                FileSaveAs_Click(sender, e);
        }

        private void View_SizeChanged(object sender, EventArgs e) =>
            GraphForm.StatusBar.ShowItemToolTips = GraphForm.WindowState != FormWindowState.Maximized;

        #endregion

        #region File Operations

        private void FileLoaded()
        {
            TracePropertiesController.Clear();
            LegendController.Clear();
            CommandProcessor.Clear();
            Graph.ZoomSet();
            InitPaper();
            UpdateUI();
        }

        private void FilePathRequest(SdiController.FilePathEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.FilePath))
                e.FilePath = Graph.Title.ToFilename();
            if (string.IsNullOrWhiteSpace(e.FilePath) && Graph.Traces.Count > 0)
                e.FilePath = Graph.Traces[0].Formula.ToFilename();
        }

        private void FileSaved() => Graph.ZoomSet();

        private GraphController GetNewGraphController()
        {
            if (AppController.Options.OpenInNewWindow)
                return AppController.AddNewGraphController();
            if (!JsonController.SaveIfModified())
                return null;
            JsonController.Clear();
            TracePropertiesController.Clear();
            Graph.InvalidateReticle();
            GraphicsController.InvalidateView();
            UpdateUI();
            return this;
        }

        private void NewEmptyGraph() => GetNewGraphController();

        private void NewFromTemplate()
        {
            var graphController = OpenFile(FilterIndex.Template);
            if (graphController != null)
                graphController.JsonController.FilePath = string.Empty;
        }

        private GraphController OpenFile(FilterIndex filterIndex = FilterIndex.File) =>
            OpenFile(JsonController.SelectFilePath(filterIndex));

        private GraphController OpenFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return null;
            var graphController = GetNewGraphController();
            if (graphController == null)
                return null;
            graphController.JsonController.LoadFromFile(filePath);
            return graphController;
        }

        #endregion

        #region Private Methods

        private bool ContinueSaving() => true;
        private void EditOptions() => new OptionsController(this).ShowModal(GraphForm);

        private void InitCoordinatesToolTip(string toolTip)
        {
            if (GraphForm.ToolTip.GetToolTip(PictureBox) != toolTip)
                GraphForm.ToolTip.SetToolTip(PictureBox, toolTip);
        }

        private void InitPaper() => ClientPanel.BackColor = Graph.PaperColour;
        private void ModelCleared() => InitPaper();

        private void ModifiedChanged()
        {
            UpdateCaption();
            GraphForm.FileSave.Enabled = Model.Modified;
            GraphForm.ModifiedLabel.Visible = Model.Modified;
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

        private static Image PlotTypeToImage(PlotType plotType)
        {
            switch (plotType)
            {
                case PlotType.Cartesian:
                    return Properties.Resources.Cartesian;
                case PlotType.Polar:
                    return Properties.Resources.Polar;
                default:
                    return null;
            }
        }

        private bool SelectTexture(Trace trace)
        {
            var dialog = GraphForm.TextureDialog;
            dialog.FileName = trace.TexturePath;
            bool ok = dialog.ShowDialog(GraphForm) == DialogResult.OK;
            if (ok)
            {
                var index = Graph.Traces.IndexOf(trace);
                var filePath = dialog.FileName;
                CommandProcessor[index].TexturePath = filePath;
            }
            return ok;
        }

        private bool ShowCoordinatesTooltip
        {
            get => GraphForm.ViewCoordinatesTooltip.Checked;
            set
            {
                GraphForm.ViewCoordinatesTooltip.Checked = value;
                if (!value)
                    InitCoordinatesToolTip(string.Empty);
            }
        }

        private void ToggleCoordinatesTooltip() => ShowCoordinatesTooltip = !ShowCoordinatesTooltip;

        private void UpdateCaption() { GraphForm.Text = JsonController.WindowCaption; }

        private void UpdatePlotType()
        {
            GraphForm.GraphTypeCartesian.Checked =
                GraphForm.tbCartesian.Checked = Graph.PlotType == PlotType.Cartesian;
            GraphForm.GraphTypePolar.Checked =
                GraphForm.tbPolar.Checked = Graph.PlotType == PlotType.Polar;
            GraphForm.tbPlotType.Image = PlotTypeToImage(Graph.PlotType);
        }

        private void UpdateUI()
        {
            UpdatePlotType();
            ClockController.UpdateTimeControls();
            LegendController.GraphRead();
            PropertyGridController.Refresh();
        }

        #endregion

        #region IDisposable

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposeTracePropertiesController();
        }

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
