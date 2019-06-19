namespace ToyGraf.Controllers
{
    using System.Linq.Expressions;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class FourierSeriesController : ApproximationController
    {
        #region Internal Interface

        internal FourierSeriesController(TracePropertiesController tracePropertiesController) :
            base(tracePropertiesController)
        { }

        internal bool Execute()
        {
            var view = new FourierSeriesParamsDialog();
            var ok = view.ShowDialog(SourceOwner) == DialogResult.OK;
            if (ok)
            {

            }
            return ok;
        }

        #endregion

        #region Private Properties

        #endregion

        #region Protected Override Methods

        protected override void PopulateTraces(Graph targetGraph, Expression proxy)
        {

            targetGraph.Title = $"Fourier Series for {proxy.AsString()}";
        }

        #endregion
    }
}
