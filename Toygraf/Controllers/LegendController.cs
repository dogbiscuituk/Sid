namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class LegendController
    {
        #region Internal Interface

        internal LegendController(GraphController graphController)
        {
            GraphController = graphController;
            GraphController.Model.Cleared += Model_Cleared;
            View = graphController.View;
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

        internal GraphController GraphController;
        internal List<TraceController> Children = new List<TraceController>();
        internal bool Loading = true;

        internal void AdjustLegend()
        {
            Legend.Visible = true;
            const int margin = 0, rowHeight = 23, maxRows = 17;
            var scroll = TraceViews.Count > maxRows;
            int w = 448 + (scroll ? SystemInformation.VerticalScrollBarWidth : 0),
                h = TraceViews.Count > 0 ? Math.Min(TraceViews.Count, maxRows) * rowHeight + 2 : 0,
                x = Client.Width - w, y = Client.Height - h;
            Legend.AutoScrollPosition = new Point(0, 0);
            int index = 0, top = 0;
            foreach (TraceView traceView in TraceViews)
            {
                traceView.Location = new Point(0, top);
                traceView.Label.Text = $"f{index++}";
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
            TraceViews.Clear();
            Children.Clear();
        }

        internal void GraphRead()
        {
            if (Updating)
                return;
            Loading = true;
            Legend.SuspendLayout();
            // First, remove any child without a corresponding Trace in the Graph.
            for (var index = 0; index < Children.Count; index++)
                if (!Graph.Traces.Contains(Children[index].Trace))
                    RemoveTraceViewAt(index);
            // Next, add any Graph Trace without a corresponding child.
            for (var index = 0; index < Graph.Traces.Count; index++)
            {
                var trace = Graph.Traces[index];
                if (!Children.Any(p => p.Trace == trace))
                    InsertNewTraceView(index, trace);
            }
            // Done.
            Legend.ResumeLayout();
            Validate();
            Loading = false;
            AdjustLegend();
        }

        internal int IndexOf(TraceController child) => Children.IndexOf(child);
        internal void RemoveTrace(int index) => CommandProcessor.GraphDeleteTrace(index);

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
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private bool CanCancel, Updating;
        private Graph Graph { get => GraphController.Graph; }
        private Panel Client { get => View.ClientPanel; }
        private Panel Legend { get => View.LegendPanel; }
        private Control.ControlCollection TraceViews { get => Legend.Controls; }
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

        private void CbFunction_Validating(object sender, CancelEventArgs e)
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

        private void GraphAddNewFunction_Click(object sender, EventArgs e) => AddNewTrace();
        private void ViewLegendBottomLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomLeft;
        private void ViewLegendBottomRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomRight;
        private void ViewLegendHide_Click(object sender, EventArgs e) => Legend.Visible = !Legend.Visible;
        private void ViewLegendTopLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopLeft;
        private void ViewLegendTopRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopRight;

        #endregion

        #region Private Methods

        private void AddNewTrace() => CommandProcessor.GraphInsertTrace(Children.Count);

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

        private void InsertNewTraceView(int index, Trace trace)
        {
            Loading = true;
            var child = new TraceController(this, trace);
            Children.Insert(index, child);
            child.TraceVisible = trace.Visible;
            child.Formula = trace.Formula;
            child.PenColour = trace.PenColour;
            child.FillColour = trace.FillColour1;
            child.FillTransparencyPercent = trace.FillTransparencyPercent;
            child.View.cbFunction.Validating += CbFunction_Validating;
            TraceViews.Add(child.View);
            TraceViews.SetChildIndex(child.View, index);
            Loading = false;
            child.View.cbFunction.Focus();
        }

        private void RemoveTraceViewAt(int index)
        {
            View.StatusBar.Focus();
            TraceViews.RemoveAt(index);
            Children[index].BeforeRemove();
            Children.RemoveAt(index);
        }

        #endregion
    }
}
