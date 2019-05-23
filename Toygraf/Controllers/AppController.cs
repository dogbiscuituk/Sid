namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;

    internal class AppController
    {
        #region Internal Interface

        internal AppController()
        {
            View = new AppForm();
            View.MinimumSize = Properties.Settings.Default.MinimumWindowSize;
            Model = new Model();
            Model.Cleared += Model_Cleared;
            Model.ModifiedChanged += Model_ModifiedChanged;
            Model.PropertyChanged += Model_PropertyChanged;
            GraphicsController = new GraphicsController(this);
            JsonController = new JsonController(Model, View, View.FileReopen);
            JsonController.FileLoaded += JsonController_FileLoaded;
            JsonController.FilePathChanged += JsonController_FilePathChanged;
            JsonController.FileSaving += JsonController_FileSaving;
            JsonController.FileSaved += JsonController_FileSaved;
            KeyboardController = new KeyboardController(this);
            LegendController = new LegendController(this);
            PropertiesController = new PropertiesController(this);
            ToolbarController = new ToolbarController(this);
            ModifiedChanged();
            LegendController.AdjustLegend();
            UpdateUI();
            PopupMenu_Opening(View, new CancelEventArgs());
            CommandProcessor = new CommandProcessor(this);
        }

        internal AppForm View
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
                View.tbSave.Click += FileSaveAs_Click;
                View.FileExit.Click += FileExit_Click;
                View.GraphProperties.Click += GraphProperties_Click;
                View.tbProperties.Click += GraphProperties_Click;
                View.ZoomFullScreen.Click += ZoomFullScreen_Click;
                View.tbFullScreen.Click += ZoomFullScreen_Click;
                View.ViewCoordinatesTooltip.Click += ViewCoordinatesTooltip_Click;
                View.HelpAbout.Click += HelpAbout_Click;
                View.PopupMenu.Opening += PopupMenu_Opening;
                View.FormClosing += View_FormClosing;
            }
        }

        internal readonly Model Model;
        internal Graph Graph { get => Model.Graph; }

        internal readonly CommandProcessor CommandProcessor;
        internal readonly KeyboardController KeyboardController;
        internal readonly LegendController LegendController;

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

        private AppForm _view;
        private Panel ClientPanel { get => View.ClientPanel; }
        private PictureBox PictureBox { get => View.PictureBox; }

        private readonly GraphicsController GraphicsController;
        private readonly JsonController JsonController;
        private readonly PropertiesController PropertiesController;
        private readonly ToolbarController ToolbarController;

        private Clock Clock => GraphicsController.Clock;
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

        #endregion

        #region Private Event Handlers

        private void FileNew_Click(object sender, EventArgs e) => NewFile();
        private void FileOpen_Click(object sender, EventArgs e) => OpenFile();
        private void FileSave_Click(object sender, EventArgs e) => JsonController.Save();
        private void FileSaveAs_Click(object sender, EventArgs e) => JsonController.SaveAs();
        private void FileExit_Click(object sender, EventArgs e) => View.Close();
        private void GraphProperties_Click(object sender, EventArgs e) => PropertiesController.Show(View);
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
        private void JsonController_FileSaved(object sender, EventArgs e) => FileSaved();
        private void JsonController_FileSaving(object sender, CancelEventArgs e) => e.Cancel = !ContinueSaving();
        private void View_FormClosing(object sender, FormClosingEventArgs e) => e.Cancel = !JsonController.SaveIfModified();

        #endregion

        #region Private Methods

        private void AdjustFullScreen()
        {
            var normal = !FullScreen;
            View.MainMenuStrip.Visible =
                View.Toolbar.Visible =
                View.TimeTrackBar.Visible =
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
            Graph.ZoomSet();
            InitPaper();
            UpdateUI();
        }

        private void FileSaved() => Graph.ZoomSet();

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
            JsonController.Clear();
            Graph.InvalidateReticle();
            GraphicsController.InvalidateView();
            UpdateUI();
        }

        private void OnPropertyChanged(string propertyName)
        {
            //System.Diagnostics.Debug.WriteLine($"AppController.OnPropertyChanged(\"{ propertyName})\"");

            switch (propertyName)
            {
                case "Model.Graph.PaperColour":
                    InitPaper();
                    break;
                case "Model.Graph.PlotType":
                    GraphicsController.AdjustPictureBox();
                    UpdatePlotType();
                    break;
                case "Model.Graph.Series":
                    LegendController.AfterAddRemove();
                    break;
            }
            GraphicsController.InvalidateView();
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void OpenFile() { JsonController.Open(); }

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

        private void ToggleCoordinatesTooltip() => ShowCoordinatesTooltip = !ShowCoordinatesTooltip;
        private void ToggleFullScreen() => FullScreen = !FullScreen;
        private void UpdateCaption() { View.Text = JsonController.WindowCaption; }

        #endregion
    }
}
