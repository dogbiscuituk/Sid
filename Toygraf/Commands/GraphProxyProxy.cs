namespace ToyGraf.Commands
{
    using System;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Controllers;
    using ToyGraf.Controls;
    using ToyGraf.Views;

    partial class GraphProxy
    {
        internal class GraphProxyProxy
        {
            internal GraphProxyProxy()
            {
                TgCollectionEditor.CollectionFormShown += TgCollectionEditor_CollectionFormShown;
                TgCollectionEditor.CollectionFormClosed += TgCollectionEditor_CollectionFormClosed;
                TgFileNameEditor.InitDialog += TgFileNameEditor_InitDialog;
            }

            private void TgCollectionEditor_CollectionFormShown(object sender, EventArgs e)
            {
                if (sender is Form form)
                {
                    form.Size = new System.Drawing.Size(512, 512);
                    form.Text = "Trace properties";
                    if (form.Owner is GraphForm graphForm)
                        GraphProxy = AppController.TheAppController.GraphControllers
                            .FirstOrDefault(p => p.View == graphForm)
                            .GraphProxy;
                    var propertyGrid = form.Controls.Find("propertyBrowser", true)?[0] as PropertyGrid;
                    PropertyTableController.HidePropertyPagesButton(propertyGrid);
                    propertyGrid.HelpVisible = true;
                }
            }

            private void TgCollectionEditor_CollectionFormClosed(object sender, FormClosedEventArgs e) =>
                GraphProxy = null;

            private void TgFileNameEditor_InitDialog(object sender, InitDialogEventArgs e) =>
                GraphController.InitTextureDialog(e.OpenFileDialog);

            internal static GraphProxy GraphProxy;
        }

        internal static readonly GraphProxyProxy TheGraphProxyProxy = new GraphProxyProxy();
    }
}
