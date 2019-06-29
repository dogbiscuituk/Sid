namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Commands;
    using ToyGraf.Views;

    internal class PropertyTableController
    {
        #region Internal Interface

        internal PropertyTableController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyTable = Form.PropertyTable;
            Form.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            Form.ViewPropertyTable.Click += TogglePropertyTable;
            Form.PopupPropertyTableMenu.Opening += PopupPropertyTableMenu_Opening;
            Form.PopupPropertyTableDock.Click += PopupPropertyTableDock_Click;
            Form.PopupPropertyTableHide.Click += PopupPropertyTableHide_Click;
            var toolStrip = FindToolStrip(PropertyTable);
            HidePropertyPagesButton(toolStrip);
            AddCloseButton(toolStrip);
            GraphController.PropertyChanged += GraphController_PropertyChanged;
        }

        internal bool PropertyTableVisible
        {
            get => !Form.SplitContainer1.Panel2Collapsed;
            set => Form.SplitContainer1.Panel2Collapsed = !value;
        }

        internal static void HidePropertyPagesButton(PropertyGrid propertyGrid) =>
            HidePropertyPagesButton(FindToolStrip(propertyGrid));

        internal void Refresh() => PropertyTable.Refresh();

        #endregion

        #region Private Properties

        private CommandProcessor CommandProcessor => GraphController.CommandProcessor;
        private readonly GraphController GraphController;
        private GraphForm Form => GraphController.View;
        private readonly PropertyGrid PropertyTable;
        private Form HostForm;

        private bool PropertyTableDocked
        {
            get => PropertyTable.FindForm() == Form;
            set
            {
                if (PropertyTableDocked != value)
                    if (value)
                    {
                        HostForm.FormClosing -= HostForm_FormClosing;
                        HostForm.Controls.Remove(PropertyTable);
                        HostForm.Close();
                        HostForm = null;
                        Form.SplitContainer1.Panel2.Controls.Add(PropertyTable);
                        PropertyTableVisible = true;
                    }
                    else
                    {
                        PropertyTableVisible = false;
                        HostForm = new HostForm
                        {
                            ClientSize = PropertyTable.Size,
                            Text = "Property Table"
                        };
                        HostForm.FormClosing += HostForm_FormClosing;
                        PropertyTable.Parent.Controls.Remove(PropertyTable);
                        HostForm.Controls.Add(PropertyTable);
                        HostForm.Show(Form);
                    }
            }
        }

        #endregion

        #region Private Event Handlers

        private void CloseButton_Click(object sender, EventArgs e) =>
            PropertyTableVisible = false;

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            PropertyChanged();

        private void HostForm_FormClosing(object sender, FormClosingEventArgs e) =>
            PropertyTableDocked = true;

        private void PopupPropertyTableDock_Click(object sender, EventArgs e) =>
            PropertyTableDocked = !PropertyTableDocked;

        private void PopupPropertyTableHide_Click(object sender, EventArgs e)
        {
            PropertyTableDocked = true;
            PropertyTableVisible = false;
        }

        private void PopupPropertyTableMenu_Opening(object sender, CancelEventArgs e) =>
            Form.PopupPropertyTableDock.Checked = PropertyTableDocked;

        private void TogglePropertyTable(object sender, EventArgs e)
        {
            PropertyTableDocked = true;
            PropertyTableVisible = !PropertyTableVisible;
        }

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            Form.ViewPropertyTable.Checked = PropertyTableVisible;

        #endregion

        #region Private Methods

        private void AddCloseButton(ToolStrip toolStrip)
        {
            var closeButton = new ToolStripButton("Close", Properties.Resources.Close)
            {
                Alignment = ToolStripItemAlignment.Right,
                DisplayStyle = ToolStripItemDisplayStyle.Image,
                ImageTransparentColor = Color.White,
                ToolTipText = "Hide the Property Table"
            };
            closeButton.Click += CloseButton_Click;
            toolStrip.Items.Add(closeButton);
        }

        private static ToolStrip FindToolStrip(PropertyGrid propertyGrid) =>
            propertyGrid.Controls.OfType<ToolStrip>().FirstOrDefault();

        private static void HidePropertyPagesButton(ToolStrip toolStrip)
        {
            toolStrip.Items[4].Visible = false; // Property Pages
            toolStrip.Items[3].Visible = false; // Separator
        }

        private void PropertyChanged() => PropertyTable.Refresh();

        #endregion
    }
}
