namespace Sid.Controllers
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Sid.Models;
    using Sid.Views;

    public class ParametersDialogController
    {
        public ParametersDialogController(MainFormController parent)
        {
            Parent = parent;
            View = new ParametersDialog();
            View.btnAddNew.Click += btnAddNew_Click;
            View.btnApply.Click += btnApply_Click;
        }

        private MainFormController Parent;
        private Graph Graph { get => Parent.Graph; }

        private ParametersDialog _view;
        public ParametersDialog View
        {
            get => _view;
            set
            {
                _view = value;
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

            foreach (Series series in Graph.Series)
                AddNewEditor(series);

            //View.btnAdd.Click += BtnAdd_Click;

            if (View.ShowDialog() == DialogResult.OK)
                Apply();
        }

        private Panel FlowLayoutPanel { get => View.FlowLayoutPanel; }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            AddNewEditor(null);
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            RemoveEditor(((Control)sender).Parent);
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
            editor.btnRemove.Click += btnRemove_Click;

            FlowLayoutPanel.Controls.Add(editor);
        }

        private void Apply()
        {
            float
                xMin = (float)View.seXmin.Value,
                yMin = (float)View.seYmin.Value,
                xMax = (float)View.seXmax.Value,
                yMax = (float)View.seYmax.Value;
            Graph.Location = new PointF(xMin, yMin);
            Graph.Size = new SizeF(xMax - xMin, yMax - yMin);
            var index = 0;
            foreach (TraceEditor editor in FlowLayoutPanel.Controls)
            {
                var series =
                    index < Graph.Series.Count
                    ? Graph.Series[index]
                    : Graph.AddSeries();
                series.Formula = editor.Formula;
                series.PenColour = editor.PenColour;
                series.FillColour = editor.FillColour;
                index++;
            }
        }

        private void RemoveEditor(Control control)
        {
            var bounds = control.Bounds;
            var controls = FlowLayoutPanel.Controls;
            var index = controls.IndexOf(control);
            controls.RemoveAt(index);
        }
    }
}
