namespace Sid.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using Sid.Expressions;
    using Sid.Models;
    using Sid.Views;

    public class LegendController
    {
        public LegendController(AppController parent)
        {
            Parent = parent;
            View = parent.View;
        }

        #region Properties

        private bool CanCancel, Loading = true;
        private Panel FlowLayoutPanel { get => View.LegendPanel; }
        private Graph Graph { get => Parent.Graph; }
        private AppController Parent;
        public List<KeyController> Children = new List<KeyController>();

        private MainForm _view;

        public MainForm View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    View.btnAddNewFunction.Click -= BtnAddNewFunction_Click;
                }
                _view = value;
                if (View != null)
                {
                    View.ViewLegendTopLeft.Tag = ContentAlignment.TopLeft;
                    View.ViewLegendTopRight.Tag = ContentAlignment.TopRight;
                    View.ViewLegendBottomLeft.Tag = ContentAlignment.BottomLeft;
                    View.ViewLegendBottomRight.Tag = ContentAlignment.BottomRight;
                    View.ViewLegendTopLeft.Click += ViewLegendAlignment_Click;
                    View.ViewLegendTopRight.Click += ViewLegendAlignment_Click;
                    View.ViewLegendBottomLeft.Click += ViewLegendAlignment_Click;
                    View.ViewLegendBottomRight.Click += ViewLegendAlignment_Click;
                    View.btnAddNewFunction.Click += BtnAddNewFunction_Click;
                }
            }
        }

        private void ViewLegendAlignment_Click(object sender, EventArgs e)
        {
            var align = (ContentAlignment)((ToolStripMenuItem)sender).Tag;
            var panel = View.LegendPanel;
            switch (align)
            {
                case ContentAlignment.TopLeft:
                    InitLegendPanel(0, 0,
                        AnchorStyles.Top | AnchorStyles.Left, FlowDirection.TopDown);
                    break;
                case ContentAlignment.TopRight:
                    InitLegendPanel(View.Width - panel.Width, 0,
                        AnchorStyles.Top | AnchorStyles.Right, FlowDirection.TopDown);
                    break;
                case ContentAlignment.BottomLeft:
                    InitLegendPanel(0, View.ClientPanel.Height - panel.Height,
                        AnchorStyles.Bottom | AnchorStyles.Left, FlowDirection.BottomUp);
                    break;
                case ContentAlignment.BottomRight:
                    InitLegendPanel(View.Width - panel.Width, View.ClientPanel.Height - panel.Height,
                        AnchorStyles.Bottom | AnchorStyles.Right, FlowDirection.BottomUp);
                    break;
            }
        }

        private void InitLegendPanel(int x, int y, AnchorStyles anchor, FlowDirection flow)
        {
            View.LegendPanel.Anchor = anchor;
            View.LegendPanel.FlowDirection = flow;
            View.LegendPanel.Location = new Point(x, y);
        }

        #endregion

        #region Series Management

        private void BtnAddNewFunction_Click(object sender, EventArgs e) =>
            AddNewKey(null);

        private void BtnRemoveFunction_Click(object sender, EventArgs e) =>
            RemoveKey((Key)((Control)sender).Parent);

        private void AddNewKey(Series series)
        {
            Loading = true;
            var child = new KeyController(this);
            Children.Add(child);
            if (series != null)
            {
                child.TraceVisible = series.Visible;
                child.Formula = series.Formula;
                child.PenColour = series.PenColour;
                child.FillColour = series.FillColour;
                child.FillTransparencyPercent = series.FillTransparencyPercent;
            }
            else
            {
                child.TraceVisible = true;
                child.Formula = string.Empty;
                child.PenColour = Color.Black;
                child.FillColour = Color.Yellow;
                child.FillTransparencyPercent = 0;
            }
            var controls = FlowLayoutPanel.Controls;
            var index = controls.Count;
            child.TraceLabel = $"f{controls.Count.ToString().ToSubscript()}";
            child.View.cbFunction.Validating += CbFunction_Validating;
            child.View.btnRemove.Click += BtnRemoveFunction_Click;
            FlowLayoutPanel.Controls.Add(child.View);
            Loading = false;
            GraphWrite();
        }

        private void RemoveKey(Key edit)
        {
            var controls = FlowLayoutPanel.Controls;
            var index = controls.IndexOf(edit);
            controls.RemoveAt(index);
            Children.RemoveAt(index);
            GraphWrite();
        }

        #endregion

        public void LiveUpdate(object sender, EventArgs e)
        {
            if (!Loading)
                GraphWrite();
        }

        public void Show(IWin32Window owner)
        {
            GraphRead();
            View.Show(owner);
            GraphWrite();
        }

        private void CbFunction_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out object result);
            //comboBox.BackColor = ok ? Color.FromKnownColor(KnownColor.Window) : Color.Red;
            //comboBox.ForeColor = ok ? Color.FromKnownColor(KnownColor.ControlText) : Color.White;
            View.ErrorProvider.SetError(comboBox, ok ? string.Empty : result.ToString());
            e.Cancel = CanCancel && !ok;
        }

        public void GraphRead()
        {
            Loading = true;
            FlowLayoutPanel.Controls.Clear();
            foreach (Series series in Graph.Series)
                AddNewKey(series);
            Validate();
            Loading = false;
        }

        private void GraphWrite()
        {
            if (!Validate())
                return;
            int index = 0, count = Graph.Series.Count;
            foreach (var child in Children)
            {
                var series = index < count ? Graph.Series[index] : Graph.AddSeries();
                series.Visible = child.TraceVisible;
                series.Formula = child.Formula;
                series.PenColour = child.PenColour;
                series.FillColour = child.FillColour;
                series.FillTransparencyPercent = child.FillTransparencyPercent;
                index++;
            }
            count -= index;
            if (count > 0)
                Graph.RemoveSeriesRange(index, count);
        }

        private bool Validate()
        {
            CanCancel = true;
            var ok = View.ValidateChildren();
            CanCancel = false;
            return ok;
        }
    }
}
