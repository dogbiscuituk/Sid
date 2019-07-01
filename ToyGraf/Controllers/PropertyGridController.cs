namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Views;

    internal class PropertyGridController
    {
        #region Internal Interface

        internal PropertyGridController(GraphController graphController)
        {
            GraphController = graphController;
            PropertyGrid = GraphForm.PropertyGrid;
            GraphForm.ViewMenu.DropDownOpening += ViewMenu_DropDownOpening;
            GraphForm.ViewPropertyGrid.Click += TogglePropertyGrid;
            GraphForm.PopupPropertyGridMenu.Opening += PopupPropertyGridMenu_Opening;
            GraphForm.PopupPropertyGridFloat.Click += PopupPropertyGridDock_Click;
            GraphForm.PopupPropertyGridHide.Click += PopupPropertyGridHide_Click;
            var toolStrip = FindToolStrip(PropertyGrid);
            HidePropertyPagesButton(toolStrip);
            AddCloseButton(toolStrip);
            GraphController.PropertyChanged += GraphController_PropertyChanged;
        }

        internal bool PropertyGridVisible
        {
            get => !GraphForm.SplitContainer1.Panel2Collapsed;
            set => GraphForm.SplitContainer1.Panel2Collapsed = !value;
        }

        internal object[] SelectedObjects
        {
            get => PropertyGrid.SelectedObjects;
            set => PropertyGrid.SelectedObjects = value;
        }

        internal static void HidePropertyPagesButton(PropertyGrid propertyGrid) =>
            HidePropertyPagesButton(FindToolStrip(propertyGrid));

        internal void Refresh() => PropertyGrid.Refresh();

        #endregion

        #region Private Properties

        private readonly GraphController GraphController;

        private HostController _HostController;
        private HostController HostController
        {
            get
            {
                if (_HostController == null)
                    _HostController = new HostController("Property Grid", PropertyGrid);
                return _HostController;
            }
        }

        private GraphForm GraphForm => GraphController.GraphForm;
        private readonly PropertyGrid PropertyGrid;

        private bool PropertyGridDocked
        {
            get => PropertyGrid.FindForm() == GraphForm;
            set
            {
                if (PropertyGridDocked != value)
                    if (PropertyGridDocked)
                    {
                        PropertyGridVisible = false;
                        HostController.HostFormClosing += HostFormClosing;
                        HostController.Show(GraphForm);
                    }
                    else
                    {
                        HostController.HostFormClosing -= HostFormClosing;
                        HostController.Close();
                        PropertyGridVisible = true;
                    }
            }
        }

        #endregion

        #region Private Event Handlers

        private void CloseButton_Click(object sender, EventArgs e) =>
            PropertyGridVisible = false;

        private void GraphController_PropertyChanged(object sender, PropertyChangedEventArgs e) =>
            PropertyChanged();

        private void HostFormClosing(object sender, FormClosingEventArgs e) =>
            PropertyGridDocked = true;

        private void PopupPropertyGridDock_Click(object sender, EventArgs e) =>
            PropertyGridDocked = !PropertyGridDocked;

        private void PopupPropertyGridHide_Click(object sender, EventArgs e)
        {
            PropertyGridDocked = true;
            PropertyGridVisible = false;
        }

        private void PopupPropertyGridMenu_Opening(object sender, CancelEventArgs e) =>
            GraphForm.PopupPropertyGridFloat.Checked = !PropertyGridDocked;

        private void TogglePropertyGrid(object sender, EventArgs e)
        {
            PropertyGridDocked = true;
            PropertyGridVisible = !PropertyGridVisible;
        }

        private void ViewMenu_DropDownOpening(object sender, EventArgs e) =>
            GraphForm.ViewPropertyGrid.Checked = PropertyGridVisible;

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

        private void PropertyChanged() => PropertyGrid.Refresh();

        #endregion
    }
}
