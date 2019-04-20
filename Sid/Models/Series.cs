namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq.Expressions;
    using FormulaBuilder;

    [Serializable]
    public class Series: INotifyPropertyChanged
    {
        public Series()
        {
            InitDefaults();
        }

        private Color _penColour;
        public Color PenColour
        {
            get => _penColour;
            set
            {
                if (PenColour != value)
                {
                    _penColour = value;
                    OnPropertyChanged("PenColour");
                }
            }
        }

        private Color _fillColour;
        public Color FillColour
        {
            get => _fillColour;
            set
            {
                if (FillColour != value)
                {
                    _fillColour = value;
                    OnPropertyChanged("FillColour");
                }
            }
        }

        private Color _limitColour;
        public Color LimitColour
        {
            get => _limitColour;
            set
            {
                if (LimitColour != value)
                {
                    _limitColour = value;
                    OnPropertyChanged("LimitColour");
                }
            }
        }

        private Expression _expression;
        public Expression Expression { get => _expression; }

        private string _formula;
        public string Formula
        {
            get => _formula;
            set
            {
                if (Formula != value)
                {
                    _expression = new Parser().Parse(value);
                    _func = Expression.AsFunction();
                    _formula = value;
                    OnPropertyChanged("Formula");
                }
            }
        }

        private Func<double, double> _func;
        public Func<double, double> Func { get => _func; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public void Draw(Graphics g, RectangleF limits, float penWidth, bool fill)
        {
            if (fill && FillColour == Color.Transparent)
                return; // Not just an optimisation; omits vertical asymptotes too.
            ComputePoints(limits);
            if (fill)
                using (var pen = new Pen(LimitColour, penWidth))
                {
                    pen.DashStyle = DashStyle.Dash;
                    using (var brush = new SolidBrush(FillColour))
                        PointLists.ForEach(p => FillArea(g, pen, brush, p));
                }
            else
                using (var pen = new Pen(PenColour, penWidth))
                    PointLists.ForEach(p => g.DrawLines(pen, p.ToArray()));
        }

        private int StepCount;
        private RectangleF Limits;
        private List<List<PointF>> PointLists = new List<List<PointF>>();

        private void ComputePoints(RectangleF limits)
        {
            if (Limits == limits)
                return;
            Limits = limits;
            PointLists.Clear();
            List<PointF> points = null;
            float
                x1 = Limits.Left, y1 = Limits.Top, y2 = Limits.Bottom,
                w = Limits.Width, h = 8 * Limits.Height;
            var skip = true;
            for (var step = 0; step <= StepCount; step++)
            {
                float x = x1 + step * w / StepCount, y = (float)_func(x);
                if (float.IsInfinity(y) || float.IsNaN(y) || y < y1 - h || y > y2 + h)
                    skip = true;
                else
                {
                    if (skip)
                    {
                        skip = false;
                        points = new List<PointF>();
                        PointLists.Add(points);
                    }
                    points.Add(new PointF(x, y));
                }
            }
            // Every segment of the trace must include at least 2 points.
            PointLists.RemoveAll(p => p.Count < 2);
        }

        private void FillArea(Graphics g, Pen pen, Brush brush, List<PointF> p)
        {
            var n = p.Count;
            var points = new PointF[n + 2];
            p.CopyTo(points);
            points[n] = new PointF(points[n - 1].X, 0);
            points[n + 1] = new PointF(points[0].X, 0);
            g.FillPolygon(brush, points);
            // Draw vertical asymptotes iff X extremes are not Limits.
            if (points[n].X < Limits.Right)
                g.DrawLine(pen, points[n - 1], points[n]);
            if (points[0].X > Limits.Left)
                g.DrawLine(pen, points[n + 1], points[0]);
        }

        private void InitDefaults()
        {
            Formula = "0";
            StepCount = 16000;
            PenColour = Color.Black;
            FillColour = Color.Yellow;
            LimitColour = Color.DarkGray;
        }
    }
}
