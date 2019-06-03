namespace ToyGraf.Controllers
{
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Views;

    internal class PropertyGridController
    {
        internal PropertyGridController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyTable = graphController.View.PropertyTable;
        }

        private GraphProxy GraphProxy => GraphController.GraphProxy;
        private readonly GraphController GraphController;
        private GraphForm GraphForm => GraphController.View;
        private readonly PropertyGrid PropertyTable;
    }
}
