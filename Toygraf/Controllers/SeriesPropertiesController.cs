namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class SeriesPropertiesController : IDisposable
    {
        #region Internal Interface

        internal SeriesPropertiesController(GraphController parent)
        {
            GraphController = parent;
            View = new SeriesPropertiesDialog();
            View.FormClosing += View_FormClosing;
            ColourController = new ColourController();
            ColourController.AddControls(View.cbPenColour, View.cbFillColour1, View.cbFillColour2);
            KeyboardController = new KeyboardController(this);
            InitEnumControls();
            View.btnTaylorPolynomial.Click += TaylorPolynomialClick;
            View.btnTexture.Click += TextureClick;
            View.cbBrushType.SelectedIndexChanged += BrushTypeChanged;
            View.cbFillColour1.SelectedIndexChanged += FillColourChanged;
            View.cbFillColour2.SelectedIndexChanged += FillColour2Changed;
            View.cbGradientMode.SelectedIndexChanged += GradientModeChanged;
            View.cbHatchStyle.SelectedIndexChanged += HatchStyleChanged;
            View.cbPenColour.SelectedIndexChanged += PenColourChanged;
            View.cbPenStyle.SelectedIndexChanged += PenStyleChanged;
            View.cbWrapMode.SelectedIndexChanged += WrapModeChanged;
            View.seIndex.ValueChanged += IndexValueChanged;
            View.seTransparency.ValueChanged += FillTransparencyChanged;
            View.sePenSize.ValueChanged += PenSizeChanged;
            UpdateUI();
        }

        internal void ShowDialog(IWin32Window owner, Point location, Graph graph, int index)
        {
            Graph = graph;
            View.seIndex.Maximum = SeriesControllers.Count - 1;
            Index = index;
            View.Location = location;
            View.ShowDialog(owner);
        }

        internal readonly GraphController GraphController;
        internal AppController AppController => GraphController.AppController;
        internal KeyboardController KeyboardController;
        internal SeriesPropertiesDialog View;
        internal Graph Graph;
        internal int Index
        {
            get => (int)(View.seIndex.Maximum - View.seIndex.Value);
            set
            {
                View.seIndex.Value = View.seIndex.Maximum - value;
                IndexValueChanged(this, EventArgs.Empty);
            }
        }

        private void IndexValueChanged(object Sender, EventArgs e)
        {
            View.IndexLabel.Text = $"f{Index}";
            KeyboardController.IndexValueChanged();
            LoadSeries();
        }

        #endregion

        #region Private Properties

        private ColourController ColourController;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private List<SeriesController> SeriesControllers => GraphController.LegendController.Children;
        private SeriesView SeriesView => SeriesControllers[Index].View;
        private Series Series => Graph.Series[Index];
        private bool Loading;

        #endregion

        #region Private Event Handlers

        private void BrushTypeChanged(object sender, EventArgs e)
        {
            UpdateUI();
            if (Loading) return;
            var brushType = (BrushType)View.cbBrushType.SelectedIndex;
            if (Series.BrushType != brushType)
                CommandProcessor.Run(new SeriesBrushTypeCommand(Index, brushType));
        }

        private void FillColourChanged(object sender, EventArgs e) =>
            SeriesView.cbFillColour.SelectedIndex = View.cbFillColour1.SelectedIndex;

        private void FillColour2Changed(object sender, EventArgs e)
        {
            if (Loading) return;
            var fillColour2 = ColourController.GetColour(View.cbFillColour2);
            if (Series.FillColour2 != fillColour2)
                CommandProcessor.Run(new SeriesFillColour2Command(Index, fillColour2));
        }

        private void FillTransparencyChanged(object sender, EventArgs e) =>
            SeriesView.seTransparency.Value = View.seTransparency.Value;

        private void GradientModeChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var gradientMode = (LinearGradientMode)View.cbGradientMode.SelectedIndex;
            if (Series.GradientMode != gradientMode)
                CommandProcessor.Run(new SeriesGradientModeCommand(Index, gradientMode));
        }

        private void HatchStyleChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var hatchStyle = (HatchStyle)View.cbHatchStyle.SelectedIndex;
            if (Series.HatchStyle != hatchStyle)
                CommandProcessor.Run(new SeriesHatchStyleCommand(Index, hatchStyle));
        }

        private void PenColourChanged(object sender, EventArgs e) =>
            SeriesView.cbPenColour.SelectedIndex = View.cbPenColour.SelectedIndex;

        private void PenSizeChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var penWidth = (float)View.sePenSize.Value;
            if (Series.PenWidth != penWidth)
                CommandProcessor.Run(new SeriesPenWidthCommand(Index, penWidth));
        }

        private void PenStyleChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var penStyle = (DashStyle)View.cbPenStyle.SelectedIndex;
            if (Series.PenStyle != penStyle)
                CommandProcessor.Run(new SeriesPenStyleCommand(Index, penStyle));
        }

        private void TaylorPolynomialClick(object sender, EventArgs e)
        {
            View.DialogResult = DialogResult.OK;
            var graphController = AppController.AddNewGraphController();
            var taylorPolynomialController = new TaylorPolynomialController(graphController);
            taylorPolynomialController.PopulateSeries(Series.Proxy, 0, 16);
            var myDelegate = new MyDelegate(Foo);
            View.Invoke(myDelegate, graphController.View);
        }

        private delegate void MyDelegate(Form form);
        private void Foo(Form form)
        {
            form.BringToFront();
            form.Focus();
        }

        private void TextureClick(object sender, EventArgs e)
        {
            if (GraphController.ExecuteTextureDialog(Series))
                View.lblTexturePath.Text = Series.TexturePath.AmpersandEscape();
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                View.Hide();
            }
        }

        private void WrapModeChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var wrapMode = (WrapMode)View.cbWrapMode.SelectedIndex;
            if (Series.WrapMode != wrapMode)
                CommandProcessor.Run(new SeriesWrapModeCommand(Index, wrapMode));
        }

        #endregion

        #region Private Methods

        private void InitEnumControls()
        {
            View.cbPenStyle.Items.PopulateWithNames(typeof(DashStyle));
            View.cbBrushType.Items.PopulateWithDescriptions(typeof(BrushType));
            View.cbHatchStyle.Items.PopulateWithNames(typeof(HatchStyle));
            View.cbGradientMode.Items.PopulateWithNames(typeof(LinearGradientMode));
            View.cbWrapMode.Items.PopulateWithNames(typeof(WrapMode));
        }

        private void LoadSeries()
        {
            Loading = true;
            var series = Graph.Series[Index];
            ColourController.SetColour(View.cbPenColour, series.PenColour);
            ColourController.SetColour(View.cbFillColour1, series.FillColour1);
            ColourController.SetColour(View.cbFillColour2, series.FillColour2);
            View.cbPenStyle.SelectedIndex = (int)series.PenStyle;
            View.cbBrushType.SelectedIndex = (int)series.BrushType;
            View.cbHatchStyle.SelectedIndex = (int)series.HatchStyle;
            View.cbGradientMode.SelectedIndex = (int)series.GradientMode;
            View.cbWrapMode.SelectedIndex = (int)series.WrapMode;
            View.lblTexturePath.Text = series.TexturePath;
            View.sePenSize.Value = (decimal)series.PenWidth;
            View.seTransparency.Value = series.FillTransparencyPercent;
            Loading = false;
            KeyboardController.LoadSeries();
        }

        private void UpdateUI()
        {
            var brushType = (BrushType)View.cbBrushType.SelectedIndex;
            bool
                solid = brushType == BrushType.Solid,
                hatch = brushType == BrushType.Hatch,
                texture = brushType == BrushType.Texture,
                path = brushType == BrushType.PathGradient,
                linear = brushType == BrushType.LinearGradient;
            View.lblFillColour.Visible =
                View.cbFillColour1.Visible =
                View.lblTransparency.Visible =
                View.seTransparency.Visible = !texture;
            View.cbFillColour2.Visible =
                View.lblFillColour2.Visible = !(solid || texture);
            View.cbHatchStyle.Visible = hatch;
            View.cbGradientMode.Visible = linear;
            View.lblType.Visible = hatch || texture || linear;
            View.cbWrapMode.Visible =
                View.lblTexturePath.Visible =
                View.btnTexture.Visible = texture;
        }

        #endregion

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing) => DisposeView();

        private void DisposeView()
        {
            if (View != null)
            {
                View.Dispose();
                View = null;
            }
        }

        #endregion
    }
}
