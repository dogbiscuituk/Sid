namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Views;

    internal class LegendController
    {
        #region Internal Interface

        internal LegendController(GraphController parent)
        {
            Parent = parent;
            Parent.Model.Cleared += Model_Cleared;
            View = parent.View;
            Graph.BeginUpdate += GraphBeginUpdate;
            Graph.EndUpdate += GraphEndUpdate;
        }

        internal GraphForm View
        {
            get => _view;
            set
            {
                _view = value;
                View.GraphAddNewFunction.Click += GraphAddNewFunction_Click;
                View.tbAdd.Click += GraphAddNewFunction_Click;
                View.ViewLegend.DropDownOpening += ViewLegend_DropDownOpening;
                View.tbLegend.DropDownOpening += TbLegend_DropDownOpening;
                View.ViewLegendTopLeft.Click += ViewLegendTopLeft_Click;
                View.ViewLegendTopRight.Click += ViewLegendTopRight_Click;
                View.ViewLegendBottomLeft.Click += ViewLegendBottomLeft_Click;
                View.ViewLegendBottomRight.Click += ViewLegendBottomRight_Click;
                View.ViewLegendHide.Click += ViewLegendHide_Click;
                View.tbLegend.ButtonClick += ViewLegendHide_Click;
            }
        }

        internal GraphController Parent;
        internal List<SeriesController> Children = new List<SeriesController>();
        internal bool Loading = true;

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

        internal void Clear()
        {
            View.StatusBar.Focus();
            SeriesViews.Clear();
            Children.Clear();
        }

        internal void GraphRead()
        {
            if (Updating)
                return;
            Loading = true;
            Legend.SuspendLayout();
            // First, remove any child without a corresponding Series in the Graph.
            for (var index = 0; index < Children.Count; index++)
                if (!Graph.Series.Contains(Children[index].Series))
                    RemoveSeriesViewAt(index);
            // Next, add any Graph Series without a corresponding child.
            for (var index = 0; index < Graph.Series.Count; index++)
            {
                var series = Graph.Series[index];
                if (!Children.Any(p => p.Series == series))
                    InsertNewSeriesView(index, series);
            }
            // Done.
            Legend.ResumeLayout();
            Validate();
            Loading = false;
            AdjustLegend();
        }

        internal int IndexOf(SeriesController child) => Children.IndexOf(child);
        internal void RemoveSeries(int index) => CommandController.Run(new GraphDeleteSeriesCommand(index));

        internal bool Validate()
        {
            Graph.ValidateProxies();
            CanCancel = true;
            var ok = View.ValidateChildren();
            CanCancel = false;
            return ok;
        }

        #endregion

        #region Private Properties

        private GraphForm _view;
        private CommandProcessor CommandController { get => Parent.CommandProcessor; }
        private bool CanCancel, Updating;
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

        private void GraphBeginUpdate(object sender, EventArgs e)
        {
            Updating = true;
        }

        private void GraphEndUpdate(object sender, EventArgs e)
        {
            Updating = false;
            GraphRead();
        }

        private void CbFunction_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out object result);
            View.ErrorProvider.SetError(comboBox, ok ? string.Empty : result.ToString());
            e.Cancel = CanCancel && !ok;
        }

        private void Model_Cleared(object sender, EventArgs e) => Clear();

        private void ViewLegend_DropDownOpening(object sender, EventArgs e)
        {
            View.ViewLegendTopLeft.Checked = LegendAlignment == ContentAlignment.TopLeft;
            View.ViewLegendTopRight.Checked = LegendAlignment == ContentAlignment.TopRight;
            View.ViewLegendBottomLeft.Checked = LegendAlignment == ContentAlignment.BottomLeft;
            View.ViewLegendBottomRight.Checked = LegendAlignment == ContentAlignment.BottomRight;
            View.ViewLegendHide.Checked = !Legend.Visible;
        }

        private void TbLegend_DropDownOpening(object sender, EventArgs e)
        {
            ViewLegend_DropDownOpening(sender, e);
            View.ViewLegend.CloneTo(View.tbLegend);
        }

        private void GraphAddNewFunction_Click(object sender, EventArgs e) => AddNewSeries();
        private void ViewLegendBottomLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomLeft;
        private void ViewLegendBottomRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomRight;
        private void ViewLegendHide_Click(object sender, EventArgs e) => Legend.Visible = !Legend.Visible;
        private void ViewLegendTopLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopLeft;
        private void ViewLegendTopRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopRight;

        #endregion

        #region Private Methods

        private void AddNewSeries() => CommandController.Run(new GraphInsertSeriesCommand(Children.Count));

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

        private void InsertNewSeriesView(int index, Series series)
        {
            Loading = true;
            var child = new SeriesController(this, series);
            Children.Insert(index, child);
            child.TraceVisible = series.Visible;
            child.Formula = series.Formula;
            child.PenColour = series.PenColour;
            child.FillColour = series.FillColour1;
            child.FillTransparencyPercent = series.FillTransparencyPercent;
            child.View.cbFunction.Validating += CbFunction_Validating;
            SeriesViews.Add(child.View);
            SeriesViews.SetChildIndex(child.View, index);
            Loading = false;
            child.View.cbFunction.Focus();
        }

        private void RemoveSeriesViewAt(int index)
        {
            View.StatusBar.Focus();
            SeriesViews.RemoveAt(index);
            Children.RemoveAt(index);
        }

        #endregion
    }
}
