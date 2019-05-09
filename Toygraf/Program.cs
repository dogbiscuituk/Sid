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
            Expressions.Expressions.TestAll();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new AppController().View);
        }
    }
}
