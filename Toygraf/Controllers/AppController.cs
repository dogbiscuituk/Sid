namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.Timers;
    using ToyGraf.Views;

    internal class AppController
    {
        internal AppController(): base()
        {
            View = new AboutController().View;
            Timer timer = new Timer(5000)
            {
                AutoReset = false,
                Enabled = true,
                SynchronizingObject = View
            };
            timer.Elapsed += Timer_Elapsed;
            AddNewGraphController();
        }

        internal GraphController AddNewGraphController()
        {
            var graphController = new GraphController(this);
            GraphControllers.Add(graphController);
            graphController.Show();
            return graphController;
        }

        internal void Remove(GraphController graphController)
        {
            GraphControllers.Remove(graphController);
            if (GraphControllers.Count == 0)
                System.Windows.Forms.Application.Exit();
        }

        internal AboutDialog View;
        internal bool EpilepsyWarningAcknowledged;

        private List<GraphController> GraphControllers = new List<GraphController>();
        private void Timer_Elapsed(object sender, ElapsedEventArgs e) => View.Hide();
    }
}
