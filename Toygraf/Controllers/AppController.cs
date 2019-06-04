namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.Timers;
    using System.Windows.Forms;
    using ToyGraf.Views;
    using Timer = System.Timers.Timer;

    internal static class AppController
    {
        //internal static AppController TheAppController = new AppController();

        static AppController()
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

        internal static GraphController AddNewGraphController()
        {
            var graphController = new GraphController();
            GraphControllers.Add(graphController);
            graphController.Show();
            return graphController;
        }

        internal static void Close() => Application.Exit();

        internal static void Remove(GraphController graphController)
        {
            GraphControllers.Remove(graphController);
            if (GraphControllers.Count == 0)
                Application.Exit();
        }

        private static AboutDialog _view;
        internal static AboutDialog View
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

        internal static bool EpilepsyWarningAcknowledged;

        internal static List<GraphController> GraphControllers = new List<GraphController>();

        private static void Timer_Elapsed(object sender, ElapsedEventArgs e) => View.Hide();
    }
}
