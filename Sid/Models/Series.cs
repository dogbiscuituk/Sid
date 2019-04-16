namespace Sid.Models
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq.Expressions;
    using System.Xml.Serialization;
    using FormulaBuilder;

    [Serializable]
    public class Series
    {
        public Series() : this(Expressions.Constant(0)) { }

        public Series(Expression formula) : this(formula, 8000) { }

        public Series(Expression formula, int stepCount) : this(formula, stepCount, Color.Black) { }

        public Series(Expression formula, int stepCount, Color penColour)
            : this(formula, stepCount, penColour, Color.Yellow) { }
        
        public Series(Expression formula, int stepCount, Color penColour, Color fillColour)
            : this(formula, stepCount, penColour, fillColour, Color.DarkGray) { }

        public Series(Expression formula, int stepCount, Color penColour, Color fillColour, Color limitColour)
        {
            Formula = formula.AsString();
            StepCount = stepCount;
            PenColour = penColour;
            FillColour = fillColour;
            LimitColour = limitColour;
        }

        [XmlIgnore] public Color PenColour { get; set; }
        [XmlIgnore] public Color FillColour { get; set; }
        [XmlIgnore] public Color LimitColour { get; set; }

        /// <summary>
        /// XML proxy colour names are spelt incorrectly (without the "u").
        /// </summary>
        public string PenColor { get => ColorTranslator.ToHtml(PenColour); set => PenColour = ColorTranslator.FromHtml(value); }
        public string FillColor { get => ColorTranslator.ToHtml(FillColour); set => FillColour = ColorTranslator.FromHtml(value); }
        public string LimitColor { get => ColorTranslator.ToHtml(LimitColour); set => LimitColour = ColorTranslator.FromHtml(value); }

        private string _formula;
        public string Formula
        {
            get => _formula;
            set
            {
                _formula = value;
                Func = new Parser().Parse(value).AsFunction();
            }
        }

        private Func<double, double> Func { get; set; }

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
                float x = x1 + step * w / StepCount, y = (float)Func(x);
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
    }
}
