﻿namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Text.RegularExpressions;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class SeriesPropertiesController : IDisposable
    {
        #region Internal Interface

        internal SeriesPropertiesController(GraphController graphController)
        {
            GraphController = graphController;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            View = new SeriesPropertiesDialog();
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

        internal Series Series
        {
            get => _series;
            set
            {
                _series = value;
                LoadSeries();
            }
        }

        internal void Clear()
        {
            Series = null;
            Close();
        }

        internal void Show(IWin32Window owner, Point location, int index)
        {
            Series = Graph.Series[index];
            View.Location = location;
            View.Show(owner);
        }

        internal readonly GraphController GraphController;
        internal AppController AppController => GraphController.AppController;
        internal KeyboardController KeyboardController;
        internal SeriesPropertiesDialog View;
        internal Graph Graph => GraphController.Graph;
        internal int Index => Graph.Series.IndexOf(Series);

        #endregion

        #region Private Properties

        private ColourController ColourController;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private List<SeriesController> SeriesControllers => GraphController.LegendController.Children;
        private SeriesView SeriesView => SeriesControllers[Index].View;
        private Series _series;
        private int Count => Graph.Series.Count;
        private bool Loading, Updating;

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

        private void BtnClose_Click(object sender, EventArgs e) => Close();

        private void BtnFillColour1_Click(object sender, EventArgs e)
        {
            View.ColourDialog.Color = Series.FillColour1;
            if (View.ColourDialog.ShowDialog(View) == DialogResult.OK)
                CommandProcessor.Run(new SeriesFillColour1Command(Index, View.ColourDialog.Color));
        }

        private void BtnFillColour2_Click(object sender, EventArgs e)
        {
            View.ColourDialog.Color = Series.FillColour2;
            if (View.ColourDialog.ShowDialog(View) == DialogResult.OK)
                CommandProcessor.Run(new SeriesFillColour2Command(Index, View.ColourDialog.Color));
        }

        private void BtnPenColour_Click(object sender, EventArgs e)
        {
            View.ColourDialog.Color = Series.PenColour;
            if (View.ColourDialog.ShowDialog(View) == DialogResult.OK)
                CommandProcessor.Run(new SeriesPenColourCommand(Index, View.ColourDialog.Color));
        }

        private void FillColour1Changed(object sender, EventArgs e)
        {
            if (Loading) return;
            var fillColour1 = ColourController.GetColour(View.cbFillColour1);
            if (Series.FillColour1 != fillColour1)
                CommandProcessor.Run(new SeriesFillColour1Command(Index, fillColour1));
        }

        private void FillColour2Changed(object sender, EventArgs e)
        {
            if (Loading) return;
            var fillColour2 = ColourController.GetColour(View.cbFillColour2);
            if (Series.FillColour2 != fillColour2)
                CommandProcessor.Run(new SeriesFillColour2Command(Index, fillColour2));
        }

        private void FillTransparencyChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var fillTransparencyPercent = View.seTransparency.Value;
            if (Series.FillTransparencyPercent != fillTransparencyPercent)
                CommandProcessor.Run(new SeriesFillTransparencyPercentCommand(Index, (int)fillTransparencyPercent));
        }

        private void GradientModeChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var gradientMode = (LinearGradientMode)View.cbGradientMode.SelectedIndex;
            if (Series.GradientMode != gradientMode)
                CommandProcessor.Run(new SeriesGradientModeCommand(Index, gradientMode));
        }

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "Model.Graph.Series":
                    if (Count == 0)
                        Clear();
                    else
                    {
                        var index = Math.Max(Graph.Series.IndexOf(Series), 0);
                        Series = Graph.Series[index];
                    }
                    return;
            }
            if (!Updating)
            {
                var match = Regex.Match(e.PropertyName, $@"Model.Graph.Series\[{Index}\]\.(\w+)");
                if (match.Success)
                {
                    Updating = true;
                    switch (match.Groups[1].Value)
                    {
                        case "BrushType":
                            View.cbBrushType.SelectedIndex = (int)Series.BrushType;
                            break;
                        case "FillColour1":
                            ColourController.SetColour(View.cbFillColour1, Series.FillColour1);
                            break;
                        case "FillColour2":
                            ColourController.SetColour(View.cbFillColour2, Series.FillColour2);
                            break;
                        case "FillTransparencyPercent":
                            View.seTransparency.Value = Series.FillTransparencyPercent;
                            break;
                        case "Formula":
                            View.FunctionBox.Text = Series.Formula;
                            break;
                        case "GradientMode":
                            View.cbGradientMode.SelectedIndex = (int)Series.GradientMode;
                            break;
                        case "HatchStyle":
                            View.cbHatchStyle.SelectedIndex = (int)Series.HatchStyle;
                            break;
                        case "PenColour":
                            ColourController.SetColour(View.cbPenColour, Series.PenColour);
                            break;
                        case "PenStyle":
                            View.cbPenStyle.SelectedIndex = (int)Series.PenStyle;
                            break;
                        case "PenWidth":
                            View.sePenSize.Value = (decimal)Series.PenWidth;
                            break;
                        case "Texture":
                        case "TexturePath":
                            View.lblTexturePath.Text = Series.TexturePath;
                            break;
                        case "Visible":
                            View.cbVisible.Checked = Series.Visible;
                            break;
                        case "WrapMode":
                            View.cbWrapMode.SelectedIndex = (int)Series.WrapMode;
                            break;
                    }
                    Updating = false;
                }
            }
        }

        private void HatchStyleChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var hatchStyle = (HatchStyle)View.cbHatchStyle.SelectedIndex;
            if (Series.HatchStyle != hatchStyle)
                CommandProcessor.Run(new SeriesHatchStyleCommand(Index, hatchStyle));
        }

        private void PenColourChanged(object sender, EventArgs e)
        {
            if (Loading) return;
            var penColour = ColourController.GetColour(View.cbPenColour);
            if (Series.PenColour != penColour)
                CommandProcessor.Run(new SeriesPenColourCommand(Index, penColour));
        }

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
            var taylorPolynomialController = new TaylorPolynomialController(this);
            if (taylorPolynomialController.Execute())
            {
                Close();
                taylorPolynomialController.CreateGraph();
            }
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
                Close();
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

        private void Close() => View.Hide();

        private void IndexValueChanged(object Sender, EventArgs e)
        {
            if (!Loading)
            {
                Loading = true;
                Series = Graph.Series[Count - (int)View.seIndex.Value - 1];
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

        private void LoadSeries()
        {
            if (Series != null)
            {
                Loading = true;
                View.IndexLabel.Text = $"f{Index}";
                View.seIndex.Maximum = Count - 1;
                View.seIndex.Value = Count - Index - 1;
                ColourController.SetColour(View.cbPenColour, Series.PenColour);
                ColourController.SetColour(View.cbFillColour1, Series.FillColour1);
                ColourController.SetColour(View.cbFillColour2, Series.FillColour2);
                View.cbPenStyle.SelectedIndex = (int)Series.PenStyle;
                View.cbBrushType.SelectedIndex = (int)Series.BrushType;
                View.cbHatchStyle.SelectedIndex = (int)Series.HatchStyle;
                View.cbGradientMode.SelectedIndex = (int)Series.GradientMode;
                View.cbWrapMode.SelectedIndex = (int)Series.WrapMode;
                View.lblTexturePath.Text = Series.TexturePath;
                View.sePenSize.Value = (decimal)Series.PenWidth;
                View.seTransparency.Value = Series.FillTransparencyPercent;
                Loading = false;
            }
            KeyboardController.LoadSeries();
        }

        private void UpdateIndexLabel() => View.IndexLabel.Text = $"f{Index}";

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
                View.btnFillColour1.Visible =
                View.lblTransparency.Visible =
                View.seTransparency.Visible = !texture;
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
