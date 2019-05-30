namespace ToyGraf.Controllers
{
    using System.Drawing;
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
            var targetFormula = proxy.AsString();
            double denominator = 1;
            var linearFactor = Expressions.x.Minus(a);
            Expression powerFactor, runningTotal = 0.0.Constant();
            var oldFormula = string.Empty;
            Series series;
            for (int index = 0, penIndex = 0; index < count; index++)
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
                var newFormula = runningTotal.AsString();
                if (newFormula != oldFormula)
                {
                    series = AddSeries();
                    series.Formula = newFormula;
                    Color penColour;
                    do penColour = Defaults.GetGraphPenColour(penIndex++);
                    while (penColour == Color.Black || penColour == Color.White);
                    series.PenColour = penColour;
                }
                if (index < count - 1)
                {
                    oldFormula = newFormula;
                    proxy = proxy.Differentiate();
                }
            }
            series = AddSeries();
            series.Formula = targetFormula;
            series.PenColour = Graph.PaperColour.Contrast();
            Graph.Title = $"Taylor Polynomial for f{Graph.Series.Count - 1} = {targetFormula} around x=0";
            Graph.OnEndUpdate();
        }

        private readonly GraphController GraphController;
        private Graph Graph => GraphController.Graph;

        private Series AddSeries() => Graph.AddSeries();
    }
}
