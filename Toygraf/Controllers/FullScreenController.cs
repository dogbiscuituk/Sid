namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class FullScreenController
    {
        #region Internal Interface

        internal FullScreenController(GraphController graphController)
        {
            GraphController = graphController;
            Form.ZoomFullScreen.Click += ZoomFullScreen_Click;
            Form.tbFullScreen.Click += ZoomFullScreen_Click;
        }

        #endregion

        #region Private Properties

        private GraphController GraphController;
        private GraphForm Form => GraphController.GraphForm;
        private PropertyGridController PropertyGridController => GraphController.PropertyGridController;
        private TraceTableController TraceTableController => GraphController.TraceTableController;

        private FormState SavedFormState;

        private bool FullScreen
        {
            get => Form.ZoomFullScreen.Checked;
            set
            {
                Form.ZoomFullScreen.Checked = value;
                AdjustFullScreen();
            }
        }

        private FormState State
        {
            get => new FormState
            {
                BorderStyle = Form.FormBorderStyle,
                Elements = FormElements.None
                | (Form.MainMenuStrip.Visible ? FormElements.MainMenu : 0)
                | (Form.Toolbar.Visible ? FormElements.Toolbar : 0)
                | (Form.StatusBar.Visible ? FormElements.StatusBar : 0)
                | (PropertyGridController.PropertyGridVisible ? FormElements.PropertyGrid : 0)
                | (TraceTableController.TraceTableVisible ? FormElements.TraceTable : 0)
                | (Form.LegendPanel.Visible ? FormElements.Legend : 0),
                WindowState = Form.WindowState
            };
            set
            {
                Form.FormBorderStyle = value.BorderStyle;
                var elements = value.Elements;
                Form.MainMenuStrip.Visible = (elements & FormElements.MainMenu) != 0;
                Form.Toolbar.Visible = (elements & FormElements.Toolbar) != 0;
                Form.StatusBar.Visible = (elements & FormElements.StatusBar) != 0;
                PropertyGridController.PropertyGridVisible = (elements & FormElements.PropertyGrid) != 0;
                TraceTableController.TraceTableVisible = (elements & FormElements.TraceTable) != 0;
                Form.LegendPanel.Visible = (elements & FormElements.Legend) != 0;
                Form.WindowState = value.WindowState;
            }
        }

        private static readonly FormState FullScreenState = new FormState
        {
            BorderStyle = FormBorderStyle.None,
            Elements = FormElements.None,
            WindowState = FormWindowState.Maximized
        };

        #endregion

        #region Private Event Handlers

        private void ZoomFullScreen_Click(object sender, EventArgs e) => ToggleFullScreen();

        #endregion

        #region Private Methods

        private void AdjustFullScreen()
        {
            if (FullScreen)
            {
                SavedFormState = State;
                State = FullScreenState;
            }
            else
                State = SavedFormState;
        }

        private void ToggleFullScreen() => FullScreen = !FullScreen;

        #endregion

        #region Private Types

        [Flags]
        internal enum FormElements
        {
            None = 0x00,
            MainMenu = 0x01,
            Toolbar = 0x02,
            StatusBar = 0x04,
            PropertyGrid = 0x08,
            TraceTable = 0x10,
            Legend = 0x20
        }

        private struct FormState
        {
            internal FormBorderStyle BorderStyle;
            internal FormElements Elements;
            internal FormWindowState WindowState;
        }

        #endregion
    }
}
