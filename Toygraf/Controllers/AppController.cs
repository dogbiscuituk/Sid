namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.Timers;
    using System.Windows.Forms;
    using ToyGraf.Views;
    using Timer = System.Timers.Timer;

    internal class AppController
    {
        internal static AppController TheAppController = new AppController();

        private AppController(): base()
        {
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
            var graphController = new GraphController();
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

        private AboutDialog _view;
        internal AboutDialog View
        {
            get
            {
                if (_view == null)
                {
                    Application.SetCompatibleTextRenderingDefault(false);
                    _view = new AboutController().View;
                }
                return _view;
            }
        }

        internal bool EpilepsyWarningAcknowledged;

        internal List<GraphController> GraphControllers = new List<GraphController>();

        private void Timer_Elapsed(object sender, ElapsedEventArgs e) => View.Hide();
    }
}
