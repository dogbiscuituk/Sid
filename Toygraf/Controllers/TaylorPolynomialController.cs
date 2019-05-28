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
            Graph.Clear();
            var series = AddSeries();
            series.Formula = proxy.AsString();

            var linearTerm = Expressions.x.Minus(a);
            var power = 1.0.Constant();
            int denom = 1;

            for (var index = 0; index < count; index++)
            {
                if (index > 1)
                    denom *= index;
                var term = proxy.AsFunction()(a, 0);
                series = AddSeries();
                series.Formula = power.AsString();

                proxy = proxy.Differentiate();
            }
        }

        private readonly GraphController GraphController;
        private Graph Graph => GraphController.Graph;

        private Series AddSeries() => Graph.AddSeries();
    }
}
