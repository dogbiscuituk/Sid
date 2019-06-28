namespace ToyGraf.Controllers
{
    using System.ComponentModel;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class DataGridController
    {
        internal DataGridController(GraphController graphController)
        {
            GraphController = graphController;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            Grid.AutoGenerateColumns = false;
        }

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Grid.DataSource = CommandProcessor.Traces;
            foreach (DataGridViewRow row in Grid.Rows)
                row.Height = 18;
        }

        private readonly GraphController GraphController;
        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private GraphForm Form => GraphController.View;
        private Graph Graph => GraphController.Graph;
        private DataGridView Grid => Form.DataGridView;
    }
}
