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

        private AppView _view;
        public AppView View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    View.EditAddaNewFunction.Click -= EditAddaNewFunction_Click;
                    View.ViewLegendTopLeft.Click -= ViewLegendTopLeft_Click;
                    View.ViewLegendTopRight.Click -= ViewLegendTopRight_Click;
                    View.ViewLegendBottomLeft.Click -= ViewLegendBottomLeft_Click;
                    View.ViewLegendBottomRight.Click -= ViewLegendBottomRight_Click;
                    View.ViewLegendNone.Click -= ViewLegendNone_Click;
                }
                _view = value;
                if (View != null)
                {
                    View.EditAddaNewFunction.Click += EditAddaNewFunction_Click;
                    View.ViewLegendTopLeft.Click += ViewLegendTopLeft_Click;
                    View.ViewLegendTopRight.Click += ViewLegendTopRight_Click;
                    View.ViewLegendBottomLeft.Click += ViewLegendBottomLeft_Click;
                    View.ViewLegendBottomRight.Click += ViewLegendBottomRight_Click;
                    View.ViewLegendNone.Click += ViewLegendNone_Click;
                }
            }
        }

        public List<KeyController> Children = new List<KeyController>();

        private bool CanCancel, Loading = true;
        private Graph Graph { get => Parent.Graph; }
        private AppController Parent;
        private Panel Client { get => View.ClientPanel; }
        private Panel Legend { get => View.LegendPanel; }
        private Control.ControlCollection Keys { get => Legend.Controls; }
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

        #region Alignment

        private void ViewLegendTopLeft_Click(object sender, EventArgs e) =>
            LegendAlignment = ContentAlignment.TopLeft;

        private void ViewLegendTopRight_Click(object sender, EventArgs e) =>
            LegendAlignment = ContentAlignment.TopRight;

        private void ViewLegendBottomLeft_Click(object sender, EventArgs e) =>
            LegendAlignment = ContentAlignment.BottomLeft;

        private void ViewLegendBottomRight_Click(object sender, EventArgs e) =>
            LegendAlignment = ContentAlignment.BottomRight;

        private void ViewLegendNone_Click(object sender, EventArgs e) => Legend.Hide();

        private void AdjustLegend()
        {
            Legend.Show();
            const int margin = 0, keyHeight = 21, maxKeys = 17;
            var scroll = Keys.Count > maxKeys;
            int w = 392 + (scroll ? SystemInformation.VerticalScrollBarWidth : 0),
                h = Math.Min(Keys.Count, maxKeys) * keyHeight,
                x = Client.Width - w, y = Client.Height - h;
            for (int index = 0, top = 0; index < Keys.Count; index++, top += keyHeight)
                Keys[index].Location = new Point(0, top);
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

        #endregion

        #region Series Management

        private void EditAddaNewFunction_Click(object sender, EventArgs e) =>
            AddNewKey(null);

        private void BtnRemoveFunction_Click(object sender, EventArgs e) =>
            RemoveKey((KeyView)((Control)sender).Parent);

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
            var index = Keys.Count;
            child.TraceLabel = $"f{Keys.Count.ToString().ToSubscript()}";
            child.View.cbFunction.Validating += CbFunction_Validating;
            child.View.btnRemove.Click += BtnRemoveFunction_Click;
            Keys.Add(child.View);
            Loading = false;
            AdjustLegend();
            GraphWrite();
        }

        private void RemoveKey(KeyView edit)
        {
            RemoveKeyAt(Keys.IndexOf(edit));
        }

        private void RemoveKeyAt(int index)
        {
            Keys.RemoveAt(index);
            Children.RemoveAt(index);
            AdjustLegend();
            GraphWrite();
        }

        #endregion

        public void LiveUpdate(object sender, EventArgs e)
        {
            if (!Loading)
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
            Keys.Clear();
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
    }
}
