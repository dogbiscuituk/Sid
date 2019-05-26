namespace ToyGraf.Controllers
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Views;

    internal class SeriesPropertiesController
    {
        #region Internal Interface

        internal SeriesPropertiesController(AppController parent)
        {
            Parent = parent;
            View = new SeriesPropertiesDialog();
            KeyboardController = new KeyboardController(this);
        }

        internal SeriesPropertiesDialog View
        {
            get => _view;
            set
            {
                _view = value;
                View.FormClosing += View_FormClosing;
                InitEnumControls();
            }
        }

        internal readonly AppController Parent;
        internal Graph Graph;

        internal void ShowDialog(IWin32Window owner, Point location, Graph graph, int index)
        {
            Graph = graph;
            View.seIndex.Maximum = Parent.LegendController.Children.Count - 1;
            Index = index;
            View.Location = location;
            View.ShowDialog(owner);
        }

        #endregion

        #region Private Properties

        private SeriesPropertiesDialog _view;
        private readonly ColourController ColourController = new ColourController();
        private KeyboardController KeyboardController;
        private ComboBox.ObjectCollection PenStyles { get => View.cbPenStyle.Items; }

        private int Index
        {
            get => KeyboardController.Index;
            set => KeyboardController.Index = value;
        }

        #endregion

        #region Private Event Handlers

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                e.Cancel = true;
                View.Hide();
            }
        }

        #endregion

        #region Private Methods

        private void InitEnumControls()
        {
            PenStyles.Clear();
            PenStyles.AddRange(Enum.GetNames(typeof(DashStyle)));
        }

        #endregion
    }
}
