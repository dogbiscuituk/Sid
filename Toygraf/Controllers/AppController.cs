namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;

    internal static class AppController
    {
        #region Internal Interface

        static AppController()
        {
            Timer = new Timer
            {
                Interval = 5000,
                Enabled = true
            };
            Timer.Tick += Timer_Tick;
            AddNewGraphController();
            ApplyOptions();
        }

        #region Properties

        internal static AboutDialog AboutDialog
        {
            get
            {
                if (_AboutDialog == null)
                    _AboutDialog = new AboutController().View;
                return _AboutDialog;
            }
        }

        internal static bool EpilepsyWarningAcknowledged;

        internal static List<GraphController> GraphControllers = new List<GraphController>();

        internal static Options Options
        {
            get
            {
                var options = new Options
                {
                    OpenInNewWindow = Settings.Options_OpenInNewWindow,
                    GroupUndo = Settings.Options_GroupUndo,
                    FilesFolderPath = Settings.FilesFolderPath,
                    TemplatesFolderPath = Settings.TemplatesFolderPath,
                    UseMaxima = Settings.UseMaxima
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
                Settings.UseMaxima = value.UseMaxima;
                Settings.Save();
                ApplyOptions();
            }
        }

        #endregion

        #region Methods

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

        #endregion

        #endregion

        #region Private Event Handlers

        private static void Timer_Tick(object sender, EventArgs e)
        {
            Timer.Dispose();
            Timer = null;
            AboutDialog.Hide();
        }

        #endregion

        #region Private Properties

        private static AboutDialog _AboutDialog;
        private static readonly string DefaultFilesFolderPath = $"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{Application.ProductName}";
        private static Properties.Settings Settings => Properties.Settings.Default;
        private static Timer Timer;

        #endregion

        #region Private Methods

        private static void ApplyOptions()
        {
            if (!Directory.Exists(Options.FilesFolderPath))
                Directory.CreateDirectory(Options.FilesFolderPath);
            if (!Directory.Exists(Options.TemplatesFolderPath))
                Directory.CreateDirectory(Options.TemplatesFolderPath);
            Expressions.UseMaxima = Options.UseMaxima;
        }

        #endregion
    }
}
