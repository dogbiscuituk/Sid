namespace ToyGraf.Controllers
{
    using System.ComponentModel;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ToyGraf.Expressions;
    using ToyGraf.Views;

    internal class SeriesPropertiesController
    {
        #region Internal Interface

        internal SeriesPropertiesController(AppController parent)
        {
            Parent = parent;
            View = new SeriesPropertiesDialog();
            View.FormClosing += View_FormClosing;
            KeyboardController = new KeyboardController(this);
            ColourController.AddControls(View.cbPenColour, View.cbFillColour, View.cb2ndFillColour);
            InitEnumControls();
        }

        internal readonly AppController Parent;
        internal KeyboardController KeyboardController;
        internal SeriesPropertiesDialog View;

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

        #region Private Properties

        private ColourController ColourController = new ColourController();

        #endregion

        #region Private Methods

        private void InitEnumControls()
        {
            View.cbPenStyle.Items.PopulateWith(typeof(DashStyle));
            View.cbBrushType.Items.PopulateWith(typeof(BrushType));
            View.cbHatchPattern.Items.PopulateWith(typeof(HatchStyle));
            View.cbLinearGraphicMode.Items.PopulateWith(typeof(LinearGradientMode));
        }

        private void UpdateUI()
        {
            var brushType = (BrushType)View.cbBrushType.SelectedIndex;
            bool solid = brushType == BrushType.Solid,
                hatch = brushType == BrushType.Hatch,
                texture = brushType == BrushType.Texture,
                path = brushType == BrushType.PathGradient,
                linear = brushType == BrushType.LinearGradient;
            View.cbFillColour.Visible = !texture;
            View.cb2ndFillColour.Visible = !(solid || texture);
            View.cbHatchPattern.Visible = hatch;
            View.cbLinearGraphicMode.Visible = linear;
        }

        #endregion

        #region Private Types

        enum BrushType
        {
            [Description("Solid")]
            Solid,
            [Description("Hatch")]
            Hatch,
            [Description("Texture")]
            Texture,
            [Description("Path Gradient")]
            PathGradient,
            [Description("Linear Gradient")]
            LinearGradient
        };

        #endregion
    }
}
