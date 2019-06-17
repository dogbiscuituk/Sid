namespace ToyGraf
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Controllers;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            if (AppController.Options.UseMaxima)
                Expressions.Expressions.TestAllMaxima();
            else
                Expressions.Expressions.TestAllIce();
            Application.Run(AppController.AboutDialog);
        }
    }
}
