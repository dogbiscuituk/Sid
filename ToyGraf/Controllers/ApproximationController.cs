namespace ToyGraf.Controllers
{
    using System.Linq.Expressions;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models;

    internal abstract class ApproximationController
    {
        #region Internal Interface

        protected ApproximationController(TracePropertiesController tracePropertiesController)
        {
            SourceGraph = tracePropertiesController.Graph;
            SourceOwner = tracePropertiesController.TracePropertiesDialog;
            SourceTrace = tracePropertiesController.Trace;
        }

        internal void CreateGraph()
        {
            var graphController = AppController.AddNewGraphController();
            var targetGraph = graphController.Graph;
            targetGraph.OnBeginUpdate();
            targetGraph.Clear();
            var proxy = SourceTrace.Proxy;
            PopulateTraces(targetGraph, proxy);
            var trace = targetGraph.AddTrace();
            trace.Formula = proxy.AsString();
            trace.PenColour = targetGraph.PaperColour.Contrast();
            targetGraph.DomainInfo = SourceGraph.DomainInfo;
            targetGraph.OnEndUpdate();
            graphController.Model.Modified = false;
        }

        #endregion

        protected Graph SourceGraph;
        protected IWin32Window SourceOwner;
        protected Trace SourceTrace;

        protected abstract void PopulateTraces(Graph targetGraph, Expression proxy);
    }
}
