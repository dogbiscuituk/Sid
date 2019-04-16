namespace Sid.Controllers
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using FormulaBuilder;
    using Sid.Models;

    public class ParametersDialogController
    {
        public ParametersDialogController(MainFormController parent)
        {
            Parent = parent;
            View = new ParametersDialog();
            View.btnApply.Click += ApplyButton_Click;
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

        private void Apply()
        {
            float
                xMin = (float)View.seXmin.Value,
                yMin = (float)View.seYmin.Value,
                xMax = (float)View.seXmax.Value,
                yMax = (float)View.seYmax.Value;
            var series = new Parser().Parse(View.cbFunction.Text);
            Graph.Clear();
            Graph.Location = new PointF(xMin, yMin);
            Graph.Size = new SizeF(xMax - xMin, yMax - yMin);
            Graph.AddSeries(series, Color.Black, Color.Yellow);
            series = series.Differentiate();
            Graph.AddSeries(series, Color.Red, Color.Orange);
            series = series.Differentiate();
            Graph.AddSeries(series, Color.Green, Color.Lime);
            series = series.Differentiate();
            Graph.AddSeries(series, Color.Blue, Color.AliceBlue);
            Parent.PictureBox.Invalidate();
        }

        private void ApplyButton_Click(object sender, EventArgs e)
        {
            Apply();
        }

        private void Init()
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
            View.cbFunction.Text = Graph.Series.First().Formula;
        }

        public void ShowDialog()
        {
            Init();
            if (View.ShowDialog() == DialogResult.OK)
                Apply();
        }
    }
}
