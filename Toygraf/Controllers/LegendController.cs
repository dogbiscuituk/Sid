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
            GraphForm = graphController.GraphForm;
            Graph.BeginUpdate += GraphBeginUpdate;
            Graph.EndUpdate += GraphEndUpdate;
        }

        internal GraphForm GraphForm
        {
            get => _GraphForm;
            set
            {
                _GraphForm = value;
                GraphForm.GraphAddNewFunction.Click += GraphAddNewFunction_Click;
                GraphForm.tbAdd.Click += GraphAddNewFunction_Click;

                GraphForm.ViewLegend.DropDownOpening += ViewLegend_DropDownOpening;
                GraphForm.ViewLegendFloat.Click += ViewLegendFloat_Click;
                GraphForm.ViewLegendHide.Click += ViewLegendHide_Click;
                GraphForm.ViewLegendTopLeft.Click += ViewLegendTopLeft_Click;
                GraphForm.ViewLegendTopRight.Click += ViewLegendTopRight_Click;
                GraphForm.ViewLegendBottomLeft.Click += ViewLegendBottomLeft_Click;
                GraphForm.ViewLegendBottomRight.Click += ViewLegendBottomRight_Click;

                GraphForm.PopupLegendMenu.Opening += ViewLegend_DropDownOpening;
                GraphForm.PopupLegendFloat.Click += ViewLegendFloat_Click;
                GraphForm.PopupLegendHide.Click += ViewLegendHide_Click;
                GraphForm.PopupLegendTopLeft.Click += ViewLegendTopLeft_Click;
                GraphForm.PopupLegendTopRight.Click += ViewLegendTopRight_Click;
                GraphForm.PopupLegendBottomLeft.Click += ViewLegendBottomLeft_Click;
                GraphForm.PopupLegendBottomRight.Click += ViewLegendBottomRight_Click;

                GraphForm.tbLegend.DropDownOpening += TbLegend_DropDownOpening;
                GraphForm.tbLegend.ButtonClick += ViewLegendHide_Click;
            }
        }

        private void ViewLegendFloat_Click(object sender, EventArgs e) =>
            LegendDocked = !LegendDocked;

        internal GraphController GraphController;
        internal List<TraceController> TraceControllers = new List<TraceController>();
        internal bool Updating = true;

        internal bool LegendVisible
        {
            get => Legend.Visible;
            set => Legend.Visible = value;
        }

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
                traceView.Label.Text = $"f{index++.ToSubscript()}";
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
            GraphForm.StatusBar.Focus();
            TraceViews.Clear();
            TraceControllers.Clear();
        }

        internal void GraphRead()
        {
            //if (Updating)
            //    return;
            Updating = true;
            Legend.SuspendLayout();
            // First, remove any TraceController without a corresponding Trace in the Graph.
            for (var index = 0; index < TraceControllers.Count; index++)
                if (!Graph.Traces.Contains(TraceControllers[index].Trace))
                    RemoveTraceViewAt(index);
            // Next, add any Graph Trace without a corresponding TraceController.
            for (var index = 0; index < Graph.Traces.Count; index++)
            {
                var trace = Graph.Traces[index];
                if (!TraceControllers.Any(p => p.Trace == trace))
                    InsertNewTraceView(index, trace);
            }
            // Done.
            Legend.ResumeLayout();
            Validate();
            Updating = false;
            AdjustLegend();
        }

        internal int IndexOf(TraceController traceController) => TraceControllers.IndexOf(traceController);
        internal void RemoveTrace(int index) => CommandProcessor.GraphDeleteTrace(index);

        internal bool Validate()
        {
            Graph.ValidateProxies();
            CanCancel = true;
            var ok = GraphForm.ValidateChildren();
            CanCancel = false;
            return ok;
        }

        #endregion

        #region Private Properties

        private HostController HostController;
        private GraphForm _GraphForm;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private bool CanCancel;
        private Graph Graph { get => GraphController.Graph; }
        private Panel Client { get => GraphForm.ClientPanel; }
        private Panel Legend { get => GraphForm.LegendPanel; }
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

        private bool LegendDocked
        {
            get => Legend.FindForm() == GraphForm;
            set
            {
                if (LegendDocked != value)
                    if (value)
                    {
                        HostController.HostFormClosing -= HostFormClosing;
                        HostController.Close();
                        HostController = null;
                        //LegendVisible = true;
                    }
                    else
                    {
                        //LegendVisible = false;
                        HostController = new HostController("Legend", Legend);
                        HostController.HostFormClosing += HostFormClosing;
                        HostController.Show(GraphForm);
                    }
            }
        }

        #endregion

        #region Private Event Handlers

        private void CbFunction_Validating(object sender, CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out _, out string error);
            GraphForm.ErrorProvider.SetError(comboBox, ok ? string.Empty : error);
            e.Cancel = CanCancel && !ok;
        }

        private void GraphBeginUpdate(object sender, EventArgs e)
        {
            Updating = true;
        }

        private void GraphEndUpdate(object sender, EventArgs e)
        {
            Updating = false;
            GraphRead();
        }

        private void HostFormClosing(object sender, FormClosingEventArgs e) =>
            LegendDocked = true;

        private void Model_Cleared(object sender, EventArgs e) => Clear();

        private void TbLegend_DropDownOpening(object sender, EventArgs e)
        {
            ViewLegend_DropDownOpening(sender, e);
            GraphForm.ViewLegend.CloneTo(GraphForm.tbLegend);
        }

        private void ViewLegend_DropDownOpening(object sender, EventArgs e)
        {
            GraphForm.ViewLegendTopLeft.Checked = LegendAlignment == ContentAlignment.TopLeft;
            GraphForm.ViewLegendTopRight.Checked = LegendAlignment == ContentAlignment.TopRight;
            GraphForm.ViewLegendBottomLeft.Checked = LegendAlignment == ContentAlignment.BottomLeft;
            GraphForm.ViewLegendBottomRight.Checked = LegendAlignment == ContentAlignment.BottomRight;
            GraphForm.ViewLegendHide.Checked = !Legend.Visible;
        }

        private void GraphAddNewFunction_Click(object sender, EventArgs e) => AddNewTrace();

        private void PopupLegendDock_Click(object sender, EventArgs e) =>
            LegendDocked = !LegendDocked;

        private void PopupLegendHide_Click(object sender, EventArgs e)
        {
            LegendDocked = true;
            LegendVisible = false;
        }

        private void PopupLegendMenu_Opening(object sender, CancelEventArgs e) =>
            GraphForm.PopupLegendFloat.Checked = !LegendDocked;

        private void ToggleLegend(object sender, EventArgs e)
        {
            LegendDocked = true;
            LegendVisible = !LegendVisible;
        }


        private void ViewLegendBottomLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomLeft;
        private void ViewLegendBottomRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.BottomRight;
        private void ViewLegendHide_Click(object sender, EventArgs e) => Legend.Visible = !Legend.Visible;
        private void ViewLegendTopLeft_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopLeft;
        private void ViewLegendTopRight_Click(object sender, EventArgs e) => LegendAlignment = ContentAlignment.TopRight;

        #endregion

        #region Private Methods

        private void AddNewTrace() => CommandProcessor.GraphInsertTrace(TraceControllers.Count);

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
            Updating = true;
            var traceController = new TraceController(this, trace);
            TraceControllers.Insert(index, traceController);
            traceController.TraceVisible = trace.Visible;
            traceController.Formula = trace.Formula;
            traceController.PenColour = trace.PenColour;
            traceController.FillColour = trace.FillColour1;
            traceController.FillTransparencyPercent = trace.FillTransparencyPercent;
            traceController.View.cbFunction.Validating += CbFunction_Validating;
            TraceViews.Add(traceController.View);
            TraceViews.SetChildIndex(traceController.View, index);
            Updating = false;
            traceController.View.cbFunction.Focus();
        }

        private void RemoveTraceViewAt(int index)
        {
            GraphForm.StatusBar.Focus();
            TraceViews.RemoveAt(index);
            TraceControllers[index].BeforeRemove();
            TraceControllers.RemoveAt(index);
        }

        #endregion
    }
}
