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
            View = new TracePropertiesDialog();
            View.FormClosing += View_FormClosing;
            ColourController = new ColourController();
            ColourController.AddControls(View.cbPenColour, View.cbFillColour1, View.cbFillColour2);
            KeyboardController = new KeyboardController(this);
            InitEnumControls();
            View.btnClose.Click += BtnClose_Click;
            View.btnFillColour1.Click += BtnFillColour1_Click;
            View.btnFillColour2.Click += BtnFillColour2_Click;
            View.btnPenColour.Click += BtnPenColour_Click;
            View.btnTaylorPolynomial.Click += TaylorPolynomialClick;
            View.btnTexture.Click += TextureClick;
            View.cbBrushType.SelectedIndexChanged += BrushTypeChanged;
            View.cbFillColour1.SelectedIndexChanged += FillColour1Changed;
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
            View.Location = location;
            View.Show(owner);
        }

        internal readonly GraphController GraphController;
        internal KeyboardController KeyboardController;
        internal TracePropertiesDialog View;
        internal Graph Graph => GraphController.Graph;
        internal int Index => Graph.Traces.IndexOf(Trace);
        internal bool Loading;

        #endregion

        #region Private Properties

        private ColourController ColourController;
        private GraphProxy GraphProxy => GraphController.GraphProxy;
        private List<TraceController> TraceControllers => GraphController.LegendController.Children;
        private TraceView TraceView => TraceControllers[Index].View;
        private Trace _trace;
        private int Count => Graph.Traces.Count;
        private bool Updating;

        #endregion

        #region Private Event Handlers

        private void BrushTypeChanged(object sender, EventArgs e)
        {
            UpdateUI();
            if (!Loading)
                GraphProxy[Index].BrushType = (BrushType)View.cbBrushType.SelectedIndex;
        }

        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private void BtnFillColour1_Click(object sender, EventArgs e)
        {
            View.ColourDialog.Color = Trace.FillColour1;
            if (View.ColourDialog.ShowDialog(View) == DialogResult.OK)
                GraphProxy[Index].FillColour1 = View.ColourDialog.Color;
        }

        private void BtnFillColour2_Click(object sender, EventArgs e)
        {
            View.ColourDialog.Color = Trace.FillColour2;
            if (View.ColourDialog.ShowDialog(View) == DialogResult.OK)
                GraphProxy[Index].FillColour2 = View.ColourDialog.Color;
        }

        private void BtnPenColour_Click(object sender, EventArgs e)
        {
            View.ColourDialog.Color = Trace.PenColour;
            if (View.ColourDialog.ShowDialog(View) == DialogResult.OK)
                GraphProxy[Index].PenColour = View.ColourDialog.Color;
        }

        private void FillColour1Changed(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].FillColour1 = ColourController.GetColour(View.cbFillColour1);
        }

        private void FillColour2Changed(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].FillColour2 = ColourController.GetColour(View.cbFillColour2);
        }

        private void FillTransparencyChanged(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].FillTransparencyPercent = (int)View.seTransparency.Value;
        }

        private void GradientModeChanged(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].GradientMode = (LinearGradientMode)View.cbGradientMode.SelectedIndex;
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
                            View.cbBrushType.SelectedIndex = (int)Trace.BrushType;
                            break;
                        case "FillColour1":
                            ColourController.SetColour(View.cbFillColour1, Trace.FillColour1);
                            break;
                        case "FillColour2":
                            ColourController.SetColour(View.cbFillColour2, Trace.FillColour2);
                            break;
                        case "FillTransparencyPercent":
                            View.seTransparency.Value = Trace.FillTransparencyPercent;
                            break;
                        case "GradientMode":
                            View.cbGradientMode.SelectedIndex = (int)Trace.GradientMode;
                            break;
                        case "HatchStyle":
                            View.cbHatchStyle.SelectedIndex = (int)Trace.HatchStyle;
                            break;
                        case "PenColour":
                            ColourController.SetColour(View.cbPenColour, Trace.PenColour);
                            break;
                        case "PenStyle":
                            View.cbPenStyle.SelectedIndex = (int)Trace.PenStyle;
                            break;
                        case "PenWidth":
                            View.sePenSize.Value = (decimal)Trace.PenWidth;
                            break;
                        case "Texture":
                        case "TexturePath":
                            View.lblTexturePath.Text = Trace.TexturePath;
                            break;
                        case "Visible":
                            View.cbVisible.Checked = Trace.Visible;
                            break;
                        case "WrapMode":
                            View.cbWrapMode.SelectedIndex = (int)Trace.WrapMode;
                            break;
                    }
                    Updating = false;
                }
            }
        }

        private void HatchStyleChanged(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].HatchStyle = (HatchStyle)View.cbHatchStyle.SelectedIndex;
        }

        private void PenColourChanged(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].PenColour = ColourController.GetColour(View.cbPenColour);
        }

        private void PenSizeChanged(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].PenWidth = (float)View.sePenSize.Value;
        }

        private void PenStyleChanged(object sender, EventArgs e)
        {
            if (!Loading)
                GraphProxy[Index].PenStyle = (DashStyle)View.cbPenStyle.SelectedIndex;
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
            if (!Loading)
                GraphProxy[Index].WrapMode = (WrapMode)View.cbWrapMode.SelectedIndex;
        }

        #endregion

        #region Private Methods

        private void Close() => View.Hide();

        private void IndexValueChanged(object Sender, EventArgs e)
        {
            if (!Loading)
            {
                Loading = true;
                Trace = Graph.Traces[Count - (int)View.seIndex.Value - 1];
                Loading = false;
            }
            KeyboardController.IndexValueChanged();
        }

        private void InitEnumControls()
        {
            View.cbPenStyle.Items.PopulateWithNames(typeof(DashStyle));
            View.cbBrushType.Items.PopulateWithDescriptions(typeof(BrushType));
            View.cbHatchStyle.Items.PopulateWithNames(typeof(HatchStyle));
            View.cbGradientMode.Items.PopulateWithNames(typeof(LinearGradientMode));
            View.cbWrapMode.Items.PopulateWithNames(typeof(WrapMode));
        }

        private void LoadTrace()
        {
            if (Trace != null)
            {
                Loading = true;
                View.IndexLabel.Text = $"f{Index}";
                View.seIndex.Maximum = Count - 1;
                View.seIndex.Value = Count - Index - 1;
                ColourController.SetColour(View.cbPenColour, Trace.PenColour);
                ColourController.SetColour(View.cbFillColour1, Trace.FillColour1);
                ColourController.SetColour(View.cbFillColour2, Trace.FillColour2);
                View.cbPenStyle.SelectedIndex = (int)Trace.PenStyle;
                View.cbBrushType.SelectedIndex = (int)Trace.BrushType;
                View.cbHatchStyle.SelectedIndex = (int)Trace.HatchStyle;
                View.cbGradientMode.SelectedIndex = (int)Trace.GradientMode;
                View.cbWrapMode.SelectedIndex = (int)Trace.WrapMode;
                UpdateTexturePathLabel();
                View.sePenSize.Value = (decimal)Trace.PenWidth;
                View.seTransparency.Value = Trace.FillTransparencyPercent;
                Loading = false;
            }
            KeyboardController.LoadTrace();
        }

        private void UpdateIndexLabel() => View.IndexLabel.Text = $"f{Index}";
        private void UpdateTexturePathLabel() => View.lblTexturePath.Text = Trace.TexturePath.AmpersandEscape();

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
                View.btnFillColour1.Visible = !texture;
            View.lblTransparency.Visible =
                View.seTransparency.Visible = true;
            View.lblFillColour2.Visible =
                View.cbFillColour2.Visible =
                View.btnFillColour2.Visible = !(solid || texture);
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
