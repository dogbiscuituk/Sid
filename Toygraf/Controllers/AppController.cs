namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Timers;
    using System.Windows.Forms;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;
    using Timer = System.Timers.Timer;

    internal static class AppController
    {
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

        internal static Options Options
        {
            get
            {
                var options = new Options
                {
                    OpenInNewWindow = Settings.Options_OpenInNewWindow,
                    GroupUndo = Settings.Options_GroupUndo,
                    FilesFolderPath = Settings.FilesFolderPath,
                    TemplatesFolderPath = Settings.TemplatesFolderPath
                };
                if (string.IsNullOrWhiteSpace(options.FilesFolderPath))
                    options.FilesFolderPath = DefaultFilesFolderPath;
                if (string.IsNullOrWhiteSpace(options.TemplatesFolderPath))
                    options.TemplatesFolderPath = $"{DefaultFilesFolderPath}\\Templates";
                return options;
            }
            set
            {
                Settings.Options_OpenInNewWindow = value.OpenInNewWindow;
                Settings.Options_GroupUndo = value.GroupUndo;
                Settings.FilesFolderPath = value.FilesFolderPath;
                Settings.TemplatesFolderPath = value.TemplatesFolderPath;
                Settings.Save();
                if (!Directory.Exists(value.FilesFolderPath))
                    Directory.CreateDirectory(value.FilesFolderPath);
                if (!Directory.Exists(value.TemplatesFolderPath))
                    Directory.CreateDirectory(value.TemplatesFolderPath);
            }
        }

        #region Private Properties

        private static readonly string DefaultFilesFolderPath =
            $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{Application.ProductName}";

        private static Properties.Settings Settings => Properties.Settings.Default;

        #endregion

        internal static GraphController AddNewGraphController()
        {
            var graphController = new GraphController();
            GraphControllers.Add(graphController);
            graphController.Show();
            return graphController;
        }

        internal static void Close() => Application.Exit();

        internal static string GetDefaultFolder(FilterIndex filterIndex)
        {
            switch (filterIndex)
            {
                case FilterIndex.File:
                    return Options.FilesFolderPath;
                case FilterIndex.Template:
                    return Options.TemplatesFolderPath;
                default:
                    return string.Empty;
            }
        }

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
