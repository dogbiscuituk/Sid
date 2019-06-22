namespace ToyGraf.Controllers
{
    using System;
    using System.Linq.Expressions;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class FourierController : ApproximationController
    {
        #region Internal Interface

        internal FourierController(TracePropertiesController tracePropertiesController) :
            base(tracePropertiesController)
        { }

        internal bool Execute()
        {
            var view = new FourierParamsDialog();
            view.edMinimumX.Text = "-π";
            view.edMaximumX.Text = "+π";
            var ok = view.ShowDialog(SourceOwner) == DialogResult.OK;
            if (ok)
            {
                HighestHarmonic = (int)view.seHighestHarmonic.Value;
                var parser = new Parser();
                var minimumX = view.edMinimumX.Text;
                var maximumX = view.edMaximumX.Text;
                Interval = $"[{minimumX}, {maximumX}]";
                ok = parser.TryParseConstant(minimumX, out MinimumX)
                    & parser.TryParseConstant(maximumX, out MaximumX);
            }
            return ok;
        }

        #endregion

        #region Private Properties

        private string Interval;
        private double MinimumX, MaximumX;
        private int HighestHarmonic;

        #endregion

        #region Protected Override Methods

        protected override void PopulateTraces(Graph targetGraph, Expression proxy)
        {
            Expression
                angle = 0.0.Constant(),
                sum = 0.0.Constant();
            var N = HighestHarmonic;
            var k = 2 / (MaximumX - MinimumX);
            double a, b;
            for (var n = 0; n <= N; n++)
            {
                if (n == 0)
                {
                    a = k * proxy.Integrate(MinimumX, MaximumX);
                    b = 0;
                }
                else
                {
                    angle = Expressions.x.Times(k * n * Math.PI);
                    a = k * proxy.Times(angle.cos()).Integrate(MinimumX, MaximumX);
                    b = k * proxy.Times(angle.sin()).Integrate(MinimumX, MaximumX);
                }
                if (a != 0 || b != 0)
                {
                    var A = Math.Sqrt(a * a + b * b);
                    var φ = Math.Atan2(b, a);
                    Expression term = n == 0
                        ? (Expression)(A / 2).Constant()
                        : A.Constant().Times(angle.Minus(φ).cos()).Simplify();
                    sum = sum.Plus(term).Simplify();
                    var newFormula = sum.AsString();
                    var trace = targetGraph.AddTrace();
                    trace.Formula = newFormula;
                }
            }
            targetGraph.Title = $"{N.ToOrdinal()} harmonic Fourier Series for {proxy.AsString()} on {Interval}";
        }

        #endregion
    }
}
