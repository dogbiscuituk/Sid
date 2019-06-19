﻿namespace ToyGraf.Controllers
{
    using System.Linq.Expressions;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class TaylorPolynomialController : ApproximationController
    {
        #region Internal Interface

        internal TaylorPolynomialController(TracePropertiesController tracePropertiesController) :
            base(tracePropertiesController)
        { }

        internal bool Execute()
        {
            var view = new TaylorPolynomialParamsDialog();
            view.edCentreX.Text = "0";
            var ok = view.ShowDialog(SourceOwner) == DialogResult.OK;
            if (ok)
            {
                Degree = (int)view.seDegree.Value;
                var parser = new Parser();
                Centre = view.edCentreX.Text;
                ok &= parser.TryParseConstant(Centre, out CentreX);
            }
            return ok;
        }

        #endregion

        #region Private Properties

        private string Centre;
        private double CentreX;
        private int Degree;

        #endregion

        #region Protected Override Methods

        protected override void PopulateTraces(Graph targetGraph, Expression proxy)
        {
            double denominator = 1;
            var linearFactor = Expressions.x.Minus(CentreX);
            Expression powerFactor, runningTotal = 0.0.Constant();
            var oldFormula = string.Empty;
            Trace trace;
            var degree = 0;
            for (int index = 0; index <= Degree; index++)
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
                    trace = targetGraph.AddTrace();
                    trace.Formula = newFormula;
                    degree = index;
                }
                if (index < Degree)
                {
                    oldFormula = newFormula;
                    proxy = proxy.Differentiate();
                }
            }
            targetGraph.Title = $"Taylor Polynomial of degree {degree} for {proxy.AsString()} at x={Centre}";
        }

        #endregion
    }
}
