namespace ToyGraf.Controllers
{
    using System;
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
            try
            {
                targetGraph.OnBeginUpdate();
                targetGraph.Clear();
                var proxy = SourceTrace.Proxy;
                try
                {
                    PopulateTraces(targetGraph, proxy);
                    var trace = targetGraph.AddTrace();
                    trace.Formula = proxy.AsString();
                    trace.PenColour = targetGraph.PaperColour.Contrast();
                    targetGraph.DomainInfo = SourceGraph.DomainInfo;
                }
                catch (FormatException ex)
                {
                    MessageBox.Show(
                        graphController.GraphForm,
                        $@"An error occurred during graph construction:

{ex.Message}

This may be caused by a discontinuity in the target function.",
                        "FormatException",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            finally
            {
                targetGraph.OnEndUpdate();
                graphController.Model.Modified = false;
            }
        }

        #endregion

        protected Graph SourceGraph;
        protected IWin32Window SourceOwner;
        protected Trace SourceTrace;

        protected abstract void PopulateTraces(Graph targetGraph, Expression proxy);
    }
}
