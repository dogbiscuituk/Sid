namespace ToyGraf.Controllers
{
    using System.Windows.Forms;
    using ToyGraf.Models.Structs;
    using ToyGraf.Views;

    internal class OptionsController
    {
        internal OptionsController(GraphController graphController)
        {
            GraphController = graphController;
            OptionsDialog = new OptionsDialog();
        }

        private GraphController GraphController;
        private OptionsDialog OptionsDialog;
        private Options Options
        {
            get => GetOptions();
            set => SetOptions(value);
        }

        internal DialogResult ShowModal(IWin32Window owner)

        {
            Options = AppController.Options;
            var result = OptionsDialog.ShowDialog(owner);
            if (result == DialogResult.OK)
                AppController.Options = Options;
            return result;
        }

        private Options GetOptions() => new Options {
            OpenInNewWindow = OptionsDialog.rbWindowNew.Checked,
            GroupUndo = OptionsDialog.rbGroupUndo.Checked };

        private void SetOptions(Options options)
        {
            OptionsDialog.rbWindowNew.Checked = options.OpenInNewWindow;
            OptionsDialog.rbWindowReuse.Checked = !options.OpenInNewWindow;
            OptionsDialog.rbGroupUndo.Checked = options.GroupUndo;
            OptionsDialog.rbNoGroupUndo.Checked = !options.GroupUndo;
        }
    }
}
