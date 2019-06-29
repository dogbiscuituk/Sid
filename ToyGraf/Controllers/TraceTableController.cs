namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Views;

    internal class TraceTableController
    {
        #region Internal Interface

        internal TraceTableController(GraphController graphController)
        {
            GraphController = graphController;
            TraceTable = Form.TraceTable;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            TraceTable.AutoGenerateColumns = false;
            Form.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            Form.ViewTraceTable.Click += ToggleTraceTable;
            Form.PopupTraceTableMenu.Opening += PopupTraceTableMenu_Opening;
            Form.PopupTraceTableFloat.Click += PopupTraceTableDock_Click;
            Form.PopupTraceTableHide.Click += PopupTraceTableHide_Click;

        }

        internal bool TraceTableVisible
        {
            get => !Form.SplitContainer2.Panel2Collapsed;
            set => Form.SplitContainer2.Panel2Collapsed = !value;
        }

        #endregion

        #region Private Properties

        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private readonly GraphController GraphController;
        private HostController HostController;
        private GraphForm Form => GraphController.GraphForm;
        private readonly DataGridView TraceTable;

        private bool TraceTableDocked
        {
            get => TraceTable.FindForm() == Form;
            set
            {
                if (TraceTableDocked != value)
                    if (value)
                    {
                        HostController.HostFormClosing -= HostFormClosing;
                        HostController.Close();
                        HostController = null;
                        ResizeRows();
                        TraceTableVisible = true;
                    }
                    else
                    {
                        TraceTableVisible = false;
                        HostController = new HostController("Trace Table", TraceTable);
                        HostController.HostFormClosing += HostFormClosing;
                        HostController.Show(Form);
                        ResizeRows();
                    }
            }
        }

        #endregion

        #region Private Event Handlers

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            TraceTable.DataSource = CommandProcessor.Traces;
            ResizeRows();
        }

        private void HostFormClosing(object sender, FormClosingEventArgs e) =>
            TraceTableDocked = true;

        private void PopupTraceTableDock_Click(object sender, System.EventArgs e) =>
            TraceTableDocked = !TraceTableDocked;

        private void PopupTraceTableHide_Click(object sender, EventArgs e)
        {
            TraceTableDocked = true;
            TraceTableVisible = false;
        }

        private void PopupTraceTableMenu_Opening(object sender, CancelEventArgs e) =>
            Form.PopupTraceTableFloat.Checked = !TraceTableDocked;

        private void ToggleTraceTable(object sender, EventArgs e)
        {
            TraceTableDocked = true;
            TraceTableVisible = !TraceTableVisible;
        }

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            Form.ViewTraceTable.Checked = TraceTableVisible;

        #endregion

        #region Private Methods

        private void ResizeRows()
        {
            foreach (DataGridViewRow row in TraceTable.Rows)
                row.Height = 18;
        }

        #endregion
    }
}
