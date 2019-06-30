﻿namespace ToyGraf.Controllers
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
            TraceTable = GraphForm.TraceTable;
            GraphController.PropertyChanged += GraphController_PropertyChanged;
            TraceTable.AutoGenerateColumns = false;
            GraphForm.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            GraphForm.ViewTraceTable.Click += ToggleTraceTable;
            GraphForm.PopupTraceTableMenu.Opening += PopupTraceTableMenu_Opening;
            GraphForm.PopupTraceTableFloat.Click += PopupTraceTableDock_Click;
            GraphForm.PopupTraceTableHide.Click += PopupTraceTableHide_Click;

        }

        internal bool TraceTableVisible
        {
            get => !GraphForm.SplitContainer2.Panel2Collapsed;
            set => GraphForm.SplitContainer2.Panel2Collapsed = !value;
        }

        #endregion

        #region Private Properties

        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private readonly GraphController GraphController;


        private HostController _HostController;
        private HostController HostController
        {
            get
            {
                if (_HostController == null)
                    _HostController = new HostController("Trace Table", TraceTable);
                return _HostController;
            }
        }

        private GraphForm GraphForm => GraphController.GraphForm;
        private readonly DataGridView TraceTable;

        private bool TraceTableDocked
        {
            get => TraceTable.FindForm() == GraphForm;
            set
            {
                if (TraceTableDocked != value)
                    if (TraceTableDocked)
                    {
                        TraceTableVisible = false;
                        HostController.HostFormClosing += HostFormClosing;
                        HostController.Show(GraphForm);
                        ResizeRows();
                    }
                    else
                    {
                        HostController.HostFormClosing -= HostFormClosing;
                        HostController.Close();
                        ResizeRows();
                        TraceTableVisible = true;
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
            GraphForm.PopupTraceTableFloat.Checked = !TraceTableDocked;

        private void ToggleTraceTable(object sender, EventArgs e)
        {
            TraceTableDocked = true;
            TraceTableVisible = !TraceTableVisible;
        }

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            GraphForm.ViewTraceTable.Checked = TraceTableVisible;

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
