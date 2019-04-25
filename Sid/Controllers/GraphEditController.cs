﻿namespace Sid.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using FormulaBuilder;
    using Sid.Models;
    using Sid.Views;

    public class GraphEditController
    {
        public GraphEditController(MainFormController parent)
        {
            Parent = parent;
            View = parent.View;
        }

        #region Properties

        private bool CanCancel, Loading = true;
        private Panel FlowLayoutPanel { get => View.FlowLayoutPanel; }
        private Graph Graph { get => Parent.Graph; }
        private MainFormController Parent;
        public List<TraceEditController> Children = new List<TraceEditController>();

        private MainForm _view;

        public MainForm View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    View.seXmin.ValueChanged -= LiveUpdate;
                    View.seYmin.ValueChanged -= LiveUpdate;
                    View.seXmax.ValueChanged -= LiveUpdate;
                    View.seYmax.ValueChanged -= LiveUpdate;
                    View.btnAddNewFunction.Click -= BtnAddNewFunction_Click;
                }
                _view = value;
                if (View != null)
                {
                    View.seXmin.ValueChanged += LiveUpdate;
                    View.seYmin.ValueChanged += LiveUpdate;
                    View.seXmax.ValueChanged += LiveUpdate;
                    View.seYmax.ValueChanged += LiveUpdate;
                    View.btnAddNewFunction.Click += BtnAddNewFunction_Click;
                }
            }
        }

        #endregion

        #region Series Management

        private void BtnAddNewFunction_Click(object sender, EventArgs e) =>
            AddNewEdit(null);

        private void BtnRemoveFunction_Click(object sender, EventArgs e) =>
            RemoveEdit((TraceEdit)((Control)sender).Parent);

        private void AddNewEdit(Series series)
        {
            Loading = true;
            var child = new TraceEditController(this);
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

        private void RemoveEdit(TraceEdit edit)
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
            View.seXmin.Value = (decimal)Graph.Limits.Left;
            View.seYmin.Value = (decimal)Graph.Limits.Top;
            View.seXmax.Value = (decimal)Graph.Limits.Right;
            View.seYmax.Value = (decimal)Graph.Limits.Bottom;
            FlowLayoutPanel.Controls.Clear();
            foreach (Series series in Graph.Series)
                AddNewEdit(series);
            Validate();
            Loading = false;
        }

        private void GraphWrite()
        {
            if (!Validate())
                return;
            float
                xMin = (float)View.seXmin.Value, yMin = (float)View.seYmin.Value,
                xMax = (float)View.seXmax.Value, yMax = (float)View.seYmax.Value;
            Graph.Location = new PointF(xMin, yMin);
            Graph.Size = new SizeF(xMax - xMin, yMax - yMin);
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
