namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Views;

    internal class DataGridController
    {
        #region Internal Interface

        internal DataGridController(GraphController graphController)
        {
            GraphController = graphController;
            Grid = Form.DataGridView;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            Grid.AutoGenerateColumns = false;
            Form.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            Form.ViewDataGrid.Click += ToggleDataGrid;
            Form.PopupDataGridMenu.Opening += PopupDataGridMenu_Opening;
            Form.PopupDataGridDock.Click += PopupDataGridDock_Click;
            Form.PopupDataGridHide.Click += PopupDataGridHide_Click;

        }

        internal bool DataGridVisible
        {
            get => !Form.SplitContainer2.Panel2Collapsed;
            set => Form.SplitContainer2.Panel2Collapsed = !value;
        }

        #endregion

        #region Private Properties

        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private readonly GraphController GraphController;
        private GraphForm Form => GraphController.View;
        private readonly DataGridView Grid;
        private Form HostForm;

        private bool DataGridDocked
        {
            get => Grid.FindForm() == Form;
            set
            {
                if (DataGridDocked != value)
                    if (value)
                    {
                        HostForm.FormClosing -= HostForm_FormClosing;
                        HostForm.Controls.Remove(Grid);
                        HostForm.Close();
                        HostForm = null;
                        Form.SplitContainer2.Panel2.Controls.Add(Grid);
                        ResizeRows();
                        DataGridVisible = true;
                    }
                    else
                    {
                        DataGridVisible = false;
                        HostForm = new HostForm
                        {
                            ClientSize = Grid.Size,
                            Text = "Traces Data Grid"
                        };
                        HostForm.FormClosing += HostForm_FormClosing;
                        Grid.Parent.Controls.Remove(Grid);
                        HostForm.Controls.Add(Grid);
                        HostForm.Show(Form);
                        ResizeRows();
                    }
            }
        }

        #endregion

        #region Private Event Handlers

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Grid.DataSource = CommandProcessor.Traces;
            ResizeRows();
        }

        private void HostForm_FormClosing(object sender, FormClosingEventArgs e) =>
            DataGridDocked = true;

        private void PopupDataGridDock_Click(object sender, System.EventArgs e) =>
            DataGridDocked = !DataGridDocked;

        private void PopupDataGridHide_Click(object sender, EventArgs e)
        {
            DataGridDocked = true;
            DataGridVisible = false;
        }

        private void PopupDataGridMenu_Opening(object sender, CancelEventArgs e) =>
            Form.PopupDataGridDock.Checked = DataGridDocked;

        private void ToggleDataGrid(object sender, EventArgs e)
        {
            DataGridDocked = true;
            DataGridVisible = !DataGridVisible;
        }

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            Form.ViewDataGrid.Checked = DataGridVisible;

        #endregion

        #region Private Methods

        private void ResizeRows()
        {
            foreach (DataGridViewRow row in Grid.Rows)
                row.Height = 18;
        }

        #endregion
    }
}
