namespace Sid
{
    using System;
    using System.Windows.Forms;
    using FormulaBuilder;
    using Sid.Controllers;

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Expressions.TestAll();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainFormController().View);
        }
    }
}
