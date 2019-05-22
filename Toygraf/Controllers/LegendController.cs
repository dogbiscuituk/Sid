namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Views;

    internal class LegendController
    {
        #region Internal Interface

        internal LegendController(AppController parent)
        {
            Parent = parent;
            View = parent.View;
        }

        internal AppForm View
        {
            get => _view;
            set
            {
                _view = value;
                View.GraphAddNewFunction.Click += GraphAddNewFunction_Click;
                View.tbAdd.Click += GraphAddNewFunction_Click;
                View.ViewLegend.DropDownOpening += ViewLegend_DropDownOpening;
                View.ViewLegendTopLeft.Click += ViewLegendTopLeft_Click;
                View.ViewLegendTopRight.Click += ViewLegendTopRight_Click;
                View.ViewLegendBottomLeft.Click += ViewLegendBottomLeft_Click;
                View.ViewLegendBottomRight.Click += ViewLegendBottomRight_Click;
                View.ViewLegendHide.Click += ViewLegendHide_Click;
            }
        }

        internal AppController Parent;
        internal List<SeriesController> Children = new List<SeriesController>();

        internal void AdjustLegend()
        {
            Legend.Visible = true;
            const int margin = 0, rowHeight = 23, maxRows = 20;
            var scroll = SeriesViews.Count > maxRows;
            int w = 489 + (scroll ? SystemInformation.VerticalScrollBarWidth : 0),
                h = SeriesViews.Count > 0 ? Math.Min(SeriesViews.Count, maxRows) * rowHeight + 2 : 0,
                x = Client.Width - w, y = Client.Height - h;
            Legend.AutoScrollPosition = new Point(0, 0);
            int index = 0, top = 0;
            foreach (SeriesView seriesView in SeriesViews)
            {
                seriesView.Location = new Point(0, top);
                seriesView.Label.Text = $"f{index++}";
                top += rowHeight;
            }
            var anchor = AlignToAnchor(LegendAlignment);
            switch (LegendAlignment)
            {
                case ContentAlignment.TopLeft: x = margin; y = margin; break;
                case ContentAlignment.TopRight: x -= margin; y = margin; break;
                case ContentAlignment.BottomLeft: x = margin; y -= margin; break;
                case ContentAlignment.BottomRight: x -= margin; y -= margin; break;
            }
            Legend.Anchor = anchor;
            Legend.AutoScroll = scroll;
            Legend.Size = new Size(w, h);
            Legend.SetBounds(x, y, w, h);
        }

        internal void GraphRead()
        {
            Loading = true;
            RemoveAllSeriesViews();
            foreach (Series series in Graph.Series)
                AddNewSeriesView(series);
            Validate();
            Loading = false;
        }

        internal int IndexOf(SeriesController child) => Children.IndexOf(child);

        internal void LiveUpdate(object sender, EventArgs e)
        {
            if (!Loading) GraphWrite();
        }

        internal void RemoveSeries(int index)
        {
            CommandController.Run(new GraphDeleteSeriesCommand(index));
            GraphRead();
            AfterChange();
        }

        #endregion

        #region Private Properties

        private AppForm _view;
        private CommandProcessor CommandController { get => Parent.CommandProcessor; }
        private bool CanCancel, Loading = true;
        private Graph Graph { get => Parent.Graph; }
        private Panel Client { get => View.ClientPanel; }
        private Panel Legend { get => View.LegendPanel; }
        private Control.ControlCollection SeriesViews { get => Legend.Controls; }
        private ContentAlignment _legendAlignment = ContentAlignment.TopLeft;
        private ContentAlignment LegendAlignment
        {
            get => _legendAlignment;
            set
            {
                _legendAlignment = value;
                AdjustLegend();
            }
        }

        #endregion

        #region Private Event Handlers

        private void CbFunction_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out object result);
            View.ErrorProvider.SetError(comboBox, ok ? string.Empty : result.ToString());
            e.Cancel = CanCancel && !ok;
        }

        private void ViewLegend_DropDownOpening(object sender, EventArgs e)
        {
            View.ViewLegendTopLeft.Checked = LegendAlignment == ContentAlignment.TopLeft;
            View.ViewLegendTopRight.Checked = LegendAlignment == ContentAlignment.TopRight;
            View.ViewLegendBottomLeft.Checked = LegendAlignment == ContentAlignment.BottomLeft;
            View.ViewLegendBottomRight.Checked = LegendAlignment == ContentAlignment.BottomRight;
            View.ViewLegendHide.Checked = !Legend.Visible;
        }

        private void GraphAddNewFunction_Click(object sender, EventArgs e) => AddNewSeries();
        private void ViewLegendBottomLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomLeft;
        private void ViewLegendBottomRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomRight;
        private void ViewLegendHide_Click(object sender, EventArgs e) => Legend.Visible = !Legend.Visible;
        private void ViewLegendTopLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopLeft;
        private void ViewLegendTopRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopRight;

        #endregion

        #region Private Methods

        private void AddNewSeries()
        {
            CommandController.Run(new GraphInsertSeriesCommand(Children.Count));
            GraphRead();
            AfterChange();
        }

        private void AddNewSeriesView(Series series)
        {
            Loading = true;
            var child = new SeriesController(this);
            Children.Add(child);
            if (series == null)
            {
                child.TraceVisible = true;
                child.Formula = string.Empty;
                child.PenColour = Graph.PenColour;
                child.FillColour = Graph.FillColour;
                child.FillTransparencyPercent = Graph.FillTransparencyPercent;
            }
            else
            {
                child.TraceVisible = series.Visible;
                child.Formula = series.Formula;
                child.PenColour = series.PenColour;
                child.FillColour = series.FillColour;
                child.FillTransparencyPercent = series.FillTransparencyPercent;
            }
            var index = SeriesViews.Count;
            child.View.cbFunction.Validating += CbFunction_Validating;
            SeriesViews.Add(child.View);
            AfterChange();
            Loading = false;
            child.View.cbFunction.Focus();
        }

        private static AnchorStyles AlignToAnchor(ContentAlignment align)
        {
            switch (align)
            {
                case ContentAlignment.BottomLeft:
                    return AnchorStyles.Bottom | AnchorStyles.Left;
                case ContentAlignment.BottomRight:
                    return AnchorStyles.Bottom | AnchorStyles.Right;
                case ContentAlignment.TopLeft:
                    return AnchorStyles.Top | AnchorStyles.Left;
                case ContentAlignment.TopRight:
                    return AnchorStyles.Top | AnchorStyles.Right;
            }
            return 0;
        }

        private void AfterChange()
        {
            AdjustLegend();
            if (!Loading)
                GraphWrite();
        }

        private void GraphWrite()
        {
            if (!Validate())
                return;
            int index = 0, count = Graph.Series.Count;
            foreach (var child in Children)
            {
                var series = index < count ? Graph.Series[index] : Graph.AddSeries();
                var visible = child.TraceVisible;
                if (series.Visible != visible)
                    CommandController.Run(new SeriesVisibleCommand(index, visible));
                var formula = child.Formula;
                if (series.Formula != formula)
                    CommandController.Run(new SeriesFormulaCommand(index, formula));
                var penColour = child.PenColour;
                if (series.PenColour.ToArgb() != penColour.ToArgb())
                    CommandController.Run(new SeriesPenColourCommand(index, penColour));
                var fillColour = child.FillColour;
                if (series.FillColour.ToArgb() != fillColour.ToArgb())
                    CommandController.Run(new SeriesFillColourCommand(index, fillColour));
                var fillTransparencyPercent = child.FillTransparencyPercent;
                if (series.FillTransparencyPercent != fillTransparencyPercent)
                    CommandController.Run(new SeriesFillTransparencyPercentCommand(index, fillTransparencyPercent));
                index++;
            }
            count -= index;
            if (count > 0)
                Graph.RemoveSeriesRange(index, count);
        }

        private void RemoveAllSeriesViews()
        {
            View.StatusBar.Focus();
            SeriesViews.Clear();
            Children.Clear();
            AfterChange();
        }

        private void RemoveSeriesViewAt(int index)
        {
            View.StatusBar.Focus();
            SeriesViews.RemoveAt(index);
            Children.RemoveAt(index);
            AfterChange();
        }

        private bool Validate()
        {
            Graph.ValidateProxies();
            CanCancel = true;
            var ok = View.ValidateChildren();
            CanCancel = false;
            return ok;
        }

        #endregion
    }
}
