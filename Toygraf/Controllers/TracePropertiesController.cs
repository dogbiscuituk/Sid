namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class TracePropertiesController : IDisposable
    {
        #region Internal Interface

        internal TracePropertiesController(GraphController graphController)
        {
            GraphController = graphController;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            TracePropertiesDialog = new TracePropertiesDialog();
            TracePropertiesDialog.FormClosing += View_FormClosing;
            ColourController = new ColourController();
            ColourController.AddControls(TracePropertiesDialog.cbPenColour, TracePropertiesDialog.cbFillColour1, TracePropertiesDialog.cbFillColour2);
            KeyboardController = new KeyboardController(this);
            InitEnumControls();
            TracePropertiesDialog.btnClose.Click += BtnClose_Click;
            TracePropertiesDialog.btnFillColour1.Click += BtnFillColour1_Click;
            TracePropertiesDialog.btnFillColour2.Click += BtnFillColour2_Click;
            TracePropertiesDialog.btnPenColour.Click += BtnPenColour_Click;
            TracePropertiesDialog.btnTaylorPolynomial.Click += TaylorPolynomialClick;
            TracePropertiesDialog.btnFourierSeries.Click += FourierSeriesClick;
            TracePropertiesDialog.btnTexture.Click += TextureClick;
            TracePropertiesDialog.cbBrushType.SelectedIndexChanged += BrushTypeChanged;
            TracePropertiesDialog.cbFillColour1.SelectedIndexChanged += FillColour1Changed;
            TracePropertiesDialog.cbFillColour2.SelectedIndexChanged += FillColour2Changed;
            TracePropertiesDialog.cbGradientMode.SelectedIndexChanged += GradientModeChanged;
            TracePropertiesDialog.cbHatchStyle.SelectedIndexChanged += HatchStyleChanged;
            TracePropertiesDialog.cbPenColour.SelectedIndexChanged += PenColourChanged;
            TracePropertiesDialog.cbPenStyle.SelectedIndexChanged += PenStyleChanged;
            TracePropertiesDialog.cbWrapMode.SelectedIndexChanged += WrapModeChanged;
            TracePropertiesDialog.seIndex.ValueChanged += IndexValueChanged;
            TracePropertiesDialog.seTransparency.ValueChanged += FillTransparencyChanged;
            TracePropertiesDialog.sePenSize.ValueChanged += PenSizeChanged;
            UpdateUI();
        }

        internal Trace Trace
        {
            get => _trace;
            set
            {
                _trace = value;
                LoadTrace();
            }
        }

        internal void Clear()
        {
            Trace = null;
            Close();
        }

        internal void Show(IWin32Window owner, Point location, int index)
        {
            Trace = Graph.Traces[index];
            TracePropertiesDialog.Location = location;
            TracePropertiesDialog.Show(owner);
        }

        internal readonly GraphController GraphController;
        internal KeyboardController KeyboardController;
        internal TracePropertiesDialog TracePropertiesDialog;
        internal Graph Graph => GraphController.Graph;
        internal int Index => Graph.Traces.IndexOf(Trace);
        internal bool Updating;

        #endregion

        #region Private Properties

        private ColourController ColourController;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private List<TraceController> TraceControllers => GraphController.LegendController.TraceControllers;
        private TraceView TraceView => TraceControllers[Index].View;
        private Trace _trace;
        private int Count => Graph.Traces.Count;

        #endregion

        #region Private Event Handlers

        private void BrushTypeChanged(object sender, EventArgs e)
        {
            UpdateUI();
            if (!Updating)
                CommandProcessor[Index].BrushType = (BrushType)TracePropertiesDialog.cbBrushType.SelectedIndex;
        }

        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private void BtnFillColour1_Click(object sender, EventArgs e)
        {
            TracePropertiesDialog.ColourDialog.Color = Trace.FillColour1;
            if (TracePropertiesDialog.ColourDialog.ShowDialog(TracePropertiesDialog) == DialogResult.OK)
                CommandProcessor[Index].FillColour1 = TracePropertiesDialog.ColourDialog.Color;
        }

        private void BtnFillColour2_Click(object sender, EventArgs e)
        {
            TracePropertiesDialog.ColourDialog.Color = Trace.FillColour2;
            if (TracePropertiesDialog.ColourDialog.ShowDialog(TracePropertiesDialog) == DialogResult.OK)
                CommandProcessor[Index].FillColour2 = TracePropertiesDialog.ColourDialog.Color;
        }

        private void BtnPenColour_Click(object sender, EventArgs e)
        {
            TracePropertiesDialog.ColourDialog.Color = Trace.PenColour;
            if (TracePropertiesDialog.ColourDialog.ShowDialog(TracePropertiesDialog) == DialogResult.OK)
                CommandProcessor[Index].PenColour = TracePropertiesDialog.ColourDialog.Color;
        }

        private void FillColour1Changed(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].FillColour1 = ColourController.GetColour(TracePropertiesDialog.cbFillColour1);
        }

        private void FillColour2Changed(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].FillColour2 = ColourController.GetColour(TracePropertiesDialog.cbFillColour2);
        }

        private void FillTransparencyChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].FillTransparencyPercent = (int)TracePropertiesDialog.seTransparency.Value;
        }

        private void FourierSeriesClick(object sender, EventArgs e)
        {
            var fourierSeriesController = new FourierSeriesController(this);
            if (fourierSeriesController.Execute())
            {
                Close();
                fourierSeriesController.CreateGraph();
            }
        }

        private void GradientModeChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].GradientMode = (LinearGradientMode)TracePropertiesDialog.cbGradientMode.SelectedIndex;
        }

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Model.Graph.Traces":
                    if (Count == 0)
                        Clear();
                    else
                    {
                        var index = Math.Max(Graph.Traces.IndexOf(Trace), 0);
                        Trace = Graph.Traces[index];
                    }
                    return;
            }
            if (!Updating)
            {
                var match = Regex.Match(e.PropertyName, $@"Model.Graph.Traces\[{Index}\]\.(\w+)");
                if (match.Success)
                {
                    Updating = true;
                    switch (match.Groups[1].Value)
                    {
                        case "BrushType":
                            TracePropertiesDialog.cbBrushType.SelectedIndex = (int)Trace.BrushType;
                            break;
                        case "FillColour1":
                            ColourController.SetColour(TracePropertiesDialog.cbFillColour1, Trace.FillColour1);
                            break;
                        case "FillColour2":
                            ColourController.SetColour(TracePropertiesDialog.cbFillColour2, Trace.FillColour2);
                            break;
                        case "FillTransparencyPercent":
                            TracePropertiesDialog.seTransparency.Value = Trace.FillTransparencyPercent;
                            break;
                        case "GradientMode":
                            TracePropertiesDialog.cbGradientMode.SelectedIndex = (int)Trace.GradientMode;
                            break;
                        case "HatchStyle":
                            TracePropertiesDialog.cbHatchStyle.SelectedIndex = (int)Trace.HatchStyle;
                            break;
                        case "PenColour":
                            ColourController.SetColour(TracePropertiesDialog.cbPenColour, Trace.PenColour);
                            break;
                        case "PenStyle":
                            TracePropertiesDialog.cbPenStyle.SelectedIndex = (int)Trace.PenStyle;
                            break;
                        case "PenWidth":
                            TracePropertiesDialog.sePenSize.Value = (decimal)Trace.PenWidth;
                            break;
                        case "Texture":
                        case "TexturePath":
                            TracePropertiesDialog.lblTexturePath.Text = Trace.TexturePath;
                            break;
                        case "Visible":
                            TracePropertiesDialog.cbVisible.Checked = Trace.Visible;
                            break;
                        case "WrapMode":
                            TracePropertiesDialog.cbWrapMode.SelectedIndex = (int)Trace.WrapMode;
                            break;
                    }
                    Updating = false;
                }
            }
        }

        private void HatchStyleChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].HatchStyle = (HatchStyle)TracePropertiesDialog.cbHatchStyle.SelectedIndex;
        }

        private void PenColourChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].PenColour = ColourController.GetColour(TracePropertiesDialog.cbPenColour);
        }

        private void PenSizeChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].PenWidth = (float)TracePropertiesDialog.sePenSize.Value;
        }

        private void PenStyleChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].PenStyle = (DashStyle)TracePropertiesDialog.cbPenStyle.SelectedIndex;
        }

        private void TaylorPolynomialClick(object sender, EventArgs e)
        {
            var taylorPolynomialController = new TaylorPolynomialController(this);
            if (taylorPolynomialController.Execute())
            {
                Close();
                taylorPolynomialController.CreateGraph();
            }
        }

        private void TextureClick(object sender, EventArgs e)
        {
            if (GraphController.ExecuteTextureDialog(Trace))
                UpdateTexturePathLabel();
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                Close();
            }
        }

        private void WrapModeChanged(object sender, EventArgs e)
        {
            if (!Updating)
                CommandProcessor[Index].WrapMode = (WrapMode)TracePropertiesDialog.cbWrapMode.SelectedIndex;
        }

        #endregion

        #region Private Methods

        private void Close() => TracePropertiesDialog.Hide();

        private void IndexValueChanged(object Sender, EventArgs e)
        {
            if (!Updating)
            {
                Updating = true;
                Trace = Graph.Traces[Count - (int)TracePropertiesDialog.seIndex.Value - 1];
                Updating = false;
            }
            KeyboardController.IndexValueChanged();
        }

        private void InitEnumControls()
        {
            TracePropertiesDialog.cbPenStyle.Items.PopulateWithNames(typeof(DashStyle));
            TracePropertiesDialog.cbBrushType.Items.PopulateWithDescriptions(typeof(BrushType));
            TracePropertiesDialog.cbHatchStyle.Items.PopulateWithNames(typeof(HatchStyle));
            TracePropertiesDialog.cbGradientMode.Items.PopulateWithNames(typeof(LinearGradientMode));
            TracePropertiesDialog.cbWrapMode.Items.PopulateWithNames(typeof(WrapMode));
        }

        private void LoadTrace()
        {
            if (Trace != null)
            {
                Updating = true;
                TracePropertiesDialog.IndexLabel.Text = $"f{Index}";
                TracePropertiesDialog.seIndex.Maximum = Count - 1;
                TracePropertiesDialog.seIndex.Value = Count - Index - 1;
                ColourController.SetColour(TracePropertiesDialog.cbPenColour, Trace.PenColour);
                ColourController.SetColour(TracePropertiesDialog.cbFillColour1, Trace.FillColour1);
                ColourController.SetColour(TracePropertiesDialog.cbFillColour2, Trace.FillColour2);
                TracePropertiesDialog.cbPenStyle.SelectedIndex = (int)Trace.PenStyle;
                TracePropertiesDialog.cbBrushType.SelectedIndex = (int)Trace.BrushType;
                TracePropertiesDialog.cbHatchStyle.SelectedIndex = (int)Trace.HatchStyle;
                TracePropertiesDialog.cbGradientMode.SelectedIndex = (int)Trace.GradientMode;
                TracePropertiesDialog.cbWrapMode.SelectedIndex = (int)Trace.WrapMode;
                UpdateTexturePathLabel();
                TracePropertiesDialog.sePenSize.Value = (decimal)Trace.PenWidth;
                TracePropertiesDialog.seTransparency.Value = Trace.FillTransparencyPercent;
                Updating = false;
            }
            KeyboardController.LoadTrace();
        }

        private void UpdateIndexLabel() => TracePropertiesDialog.IndexLabel.Text = $"f{Index}";
        private void UpdateTexturePathLabel() => TracePropertiesDialog.lblTexturePath.Text = Trace.TexturePath.AmpersandEscape();

        private void UpdateUI()
        {
            var brushType = (BrushType)TracePropertiesDialog.cbBrushType.SelectedIndex;
            bool
                solid = brushType == BrushType.Solid,
                hatch = brushType == BrushType.Hatch,
                texture = brushType == BrushType.Texture,
                path = brushType == BrushType.PathGradient,
                linear = brushType == BrushType.LinearGradient;
            TracePropertiesDialog.lblFillColour.Visible =
                TracePropertiesDialog.cbFillColour1.Visible =
                TracePropertiesDialog.btnFillColour1.Visible = !texture;
            TracePropertiesDialog.lblTransparency.Visible =
                TracePropertiesDialog.seTransparency.Visible = true;
            TracePropertiesDialog.lblFillColour2.Visible =
                TracePropertiesDialog.cbFillColour2.Visible =
                TracePropertiesDialog.btnFillColour2.Visible = !(solid || texture);
            TracePropertiesDialog.cbHatchStyle.Visible = hatch;
            TracePropertiesDialog.cbGradientMode.Visible = linear;
            TracePropertiesDialog.lblType.Visible = hatch || texture || linear;
            TracePropertiesDialog.cbWrapMode.Visible =
                TracePropertiesDialog.lblTexturePath.Visible =
                TracePropertiesDialog.btnTexture.Visible = texture;
        }

        #endregion

        #region IDisposable

        public void Dispose() => Dispose(true);

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
                DisposeTracePropertiesDialog();
        }

        private void DisposeTracePropertiesDialog()
        {
            if (TracePropertiesDialog != null)
            {
                TracePropertiesDialog.Dispose();
                TracePropertiesDialog = null;
            }
        }

        #endregion
    }
}
