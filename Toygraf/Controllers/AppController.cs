namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.Timers;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class AppController
    {
        internal AppController(): base()
        {
            View = new AboutController().View;
            Hide = new Work(View.Hide);
            System.Timers.Timer timer = new System.Timers.Timer(2000);
            timer.Elapsed += Timer_Elapsed;
            timer.Enabled = true;
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
                Application.Exit();
        }

        internal AboutDialog View;
        private List<GraphController> GraphControllers = new List<GraphController>();
        private delegate void Work();
        private readonly Work Hide;

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) =>
            View.Invoke(Hide);
    }
}
