namespace ToyGraf.Controllers
{
    using System.Drawing;
    using System.Linq.Expressions;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;

    internal class TaylorPolynomialController
    {
        internal TaylorPolynomialController(SeriesPropertiesController seriesPropertiesController) =>
            SeriesPropertiesController = seriesPropertiesController;

        internal void CreateGraph()
        {
            GraphController = AppController.AddNewGraphController();
            PopulateSeries(SeriesPropertiesController.Series.Proxy);
        }

        internal bool Execute()
        {
            var view = new TaylorPolynomialParamsDialog();
            view.edCentreX.Text = "0";
            var graph = SeriesPropertiesController.Graph;
            DomainInfo = graph.DomainInfo;
            var ok = view.ShowDialog(SeriesPropertiesController.View) == DialogResult.OK;
            if (ok)
            {
                Degree = (int)view.seDegree.Value;
                var parser = new Parser();
                Centre = view.edCentreX.Text;
                ok &= parser.TryParseConstant(Centre, out CentreX);
            }
            return ok;
        }

        internal void PopulateSeries(Expression proxy)
        {
            Graph.OnBeginUpdate();
            Graph.Clear();
            var targetFormula = proxy.AsString();
            double denominator = 1;
            var linearFactor = Expressions.x.Minus(CentreX);
            Expression powerFactor, runningTotal = 0.0.Constant();
            var oldFormula = string.Empty;
            Series series;
            var degree = 0;
            for (int index = 0, penIndex = 0; index <= Degree; index++)
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
                var coefficient = proxy.AsFunction()(CentreX, 0);
                var termTaylor = coefficient.Times(powerFactor).Over(denominator).Simplify();
                runningTotal = runningTotal.Plus(termTaylor).Simplify();
                var newFormula = runningTotal.AsString();
                if (newFormula != oldFormula)
                {
                    series = Graph.AddSeries();
                    series.Formula = newFormula;
                    Color penColour;
                    do penColour = Defaults.GetGraphPenColour(penIndex++);
                    while (penColour == Color.Black || penColour == Color.White);
                    series.PenColour = penColour;
                    degree = index;
                }
                if (index < Degree)
                {
                    oldFormula = newFormula;
                    proxy = proxy.Differentiate();
                }
            }
            series = Graph.AddSeries();
            series.Formula = targetFormula;
            series.PenColour = Graph.PaperColour.Contrast();
            Graph.DomainInfo = DomainInfo;
            Graph.Title = $"Taylor Polynomial of degree {degree} for {targetFormula} at x={Centre}";
            Graph.OnEndUpdate();
            GraphController.Model.Modified = false;
        }

        private readonly SeriesPropertiesController SeriesPropertiesController;
        private AppController AppController => SeriesPropertiesController.AppController;
        private GraphController GraphController;
        private Graph Graph => GraphController.Graph;
        private Series Series => SeriesPropertiesController.Series;
        private DomainInfo DomainInfo;
        private string Centre;
        private double CentreX;
        private int Degree;
    }
}
