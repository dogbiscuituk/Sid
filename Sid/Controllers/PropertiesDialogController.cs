namespace Sid.Controllers
{
    using System;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;
    using FormulaBuilder;
    using Sid.Models;
    using Sid.Views;

    public class PropertiesDialogController
    {
        public PropertiesDialogController(MainFormController parent)
        {
            Parent = parent;
            View = new PropertiesDialog();
        }

        private bool CanCancel;
        private Panel FlowLayoutPanel { get => View.FlowLayoutPanel; }
        private Graph Graph { get => Parent.Graph; }
        private MainFormController Parent;

        private PropertiesDialog _view;
        public PropertiesDialog View
        {
            get => _view;
            set
            {
                _view = value;
                View.FormClosing += View_FormClosing;
                View.btnAddNewFunction.Click += BtnAddNewFunction_Click;
                View.btnApply.Click += BtnApply_Click;
            }
        }

        public void ShowDialog()
        {
            float
                xMin = Graph.Location.X,
                yMin = Graph.Location.Y,
                xMax = xMin + Graph.Size.Width,
                yMax = yMin + Graph.Size.Height;
            View.seXmin.Value = (decimal)xMin;
            View.seYmin.Value = (decimal)yMin;
            View.seXmax.Value = (decimal)xMax;
            View.seYmax.Value = (decimal)yMax;
            FlowLayoutPanel.Controls.Clear();
            foreach (Series series in Graph.Series)
                AddNewEditor(series);
            Validate();
            if (View.ShowDialog() == DialogResult.OK)
                Apply();
        }

        private void BtnAddNewFunction_Click(object sender, EventArgs e) =>
            AddNewEditor(null);

        private void BtnApply_Click(object sender, EventArgs e) =>
            Apply();

        private void BtnRemove_Click(object sender, EventArgs e) =>
            RemoveEditor((TraceEditor)((Control)sender).Parent);

        private void CbFunction_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var comboBox = (ComboBox)sender;
            var ok = new Parser().TryParse(comboBox.Text, out object result);
            //comboBox.BackColor = ok ? Color.FromKnownColor(KnownColor.Window) : Color.Red;
            //comboBox.ForeColor = ok ? Color.FromKnownColor(KnownColor.ControlText) : Color.White;
            View.ErrorProvider.SetError(comboBox, ok ? string.Empty : result.ToString());
            e.Cancel = CanCancel && !ok;
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (View.DialogResult != DialogResult.Cancel)
                e.Cancel = !Validate();
        }

        private void AddNewEditor(Series series)
        {
            var controls = FlowLayoutPanel.Controls;
            var editor = new TraceEditor();
            if (series != null)
            {
                editor.Formula = series.Formula;
                editor.PenColour = series.PenColour;
                editor.FillColour = series.FillColour;
            }
            else
            {
                editor.Formula = string.Empty;
                editor.PenColour = Color.Black;
                editor.FillColour = Color.Yellow;
            }
            var index = controls.Count;
            editor.tbLabel.Text = index > 0 ? $"y{controls.Count}" : "y";
            editor.cbFunction.Validating += CbFunction_Validating;
            editor.btnRemove.Click += BtnRemove_Click;
            FlowLayoutPanel.Controls.Add(editor);
        }

        private void Apply()
        {
            if (!Validate())
                return;
            float
                xMin = (float)View.seXmin.Value,
                yMin = (float)View.seYmin.Value,
                xMax = (float)View.seXmax.Value,
                yMax = (float)View.seYmax.Value;
            Graph.Location = new PointF(xMin, yMin);
            Graph.Size = new SizeF(xMax - xMin, yMax - yMin);
            int
                index = 0,
                count = Graph.Series.Count;
            foreach (TraceEditor editor in FlowLayoutPanel.Controls)
            {
                var series =
                    index < count
                    ? Graph.Series[index]
                    : Graph.AddSeries();
                series.Formula = editor.Formula;
                series.PenColour = editor.PenColour;
                series.FillColour = editor.FillColour;
                index++;
            }
            count -= index;
            if (count > 0)
                Graph.RemoveSeriesRange(index, count);
        }

        private void RemoveEditor(TraceEditor editor)
        {
            var bounds = editor.Bounds;
            var controls = FlowLayoutPanel.Controls;
            var index = controls.IndexOf(editor);
            controls.RemoveAt(index);
        }

        private bool Validate()
        {
            CanCancel = true;
            var ok = View.ValidateChildren();
            CanCancel = false;
            if (!ok)
                MessageBox.Show(View,
                    "Property values can't be applied while errors exist.",
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return ok;
        }
    }
}
