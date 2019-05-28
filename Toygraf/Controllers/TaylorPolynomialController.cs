namespace ToyGraf.Controllers
{
    using System.Linq.Expressions;
    using ToyGraf.Expressions;
    using ToyGraf.Models;

    internal class TaylorPolynomialController
    {
        internal TaylorPolynomialController(GraphController graphController)
        {
            GraphController = graphController;
        }

        internal void PopulateSeries(Expression proxy, double a, int count)
        {
            Graph.OnBeginUpdate();
            Graph.Clear();
            var series = AddSeries();
            series.Formula = proxy.AsString();
            double denominator = 1;
            var linearFactor = Expressions.x.Minus(a);
            Expression powerFactor, runningTotal = 0.0.Constant();
            var oldFormula = string.Empty;
            for (var index = 0; index < count; index++)
            {
                switch (index)
                {
                    case 0:
                        powerFactor = 1.0.Constant();
                        break;
                    case 1:
                        powerFactor = linearFactor;
                        break;
                    default:
                        powerFactor = linearFactor.Power(index);
                        break;
                }
                if (index > 1)
                    denominator *= index;
                var coefficient = proxy.AsFunction()(a, 0);
                var termTaylor = coefficient.Times(powerFactor).Over(denominator).Simplify();
                runningTotal = runningTotal.Plus(termTaylor).Simplify();
                System.Diagnostics.Debug.WriteLine(runningTotal.AsString());
                series = AddSeries();
                var newFormula = runningTotal.AsString();
                series.Formula = newFormula;
                series.Visible = newFormula != oldFormula;
                if (index < count - 1)
                {
                    oldFormula = newFormula;
                    proxy = proxy.Differentiate();
                }
            }
            Graph.OnEndUpdate();
        }

        private readonly GraphController GraphController;
        private Graph Graph => GraphController.Graph;

        private Series AddSeries() => Graph.AddSeries();
    }
}
