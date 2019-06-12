namespace ToyGraf.Commands
{
    using System;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Controllers;
    using ToyGraf.Controls;
    using ToyGraf.Views;

    partial class CommandProcessor
    {
        internal class GraphProxyProxy
        {
            internal GraphProxyProxy()
            {
                TgCollectionEditor.CollectionEdited += TgCollectionEditor_CollectionEdited;
                TgCollectionEditor.CollectionFormShown += TgCollectionEditor_CollectionFormShown;
                //TgCollectionEditor.CollectionFormClosed += TgCollectionEditor_CollectionFormClosed;
                TgFileNameEditor.InitDialog += TgFileNameEditor_InitDialog;
            }

            private void TgCollectionEditor_CollectionEdited(object sender, CollectionEditedEventArgs e)
            {
                return;
            }

            //private void TgCollectionEditor_CollectionFormClosed(object sender, FormClosedEventArgs e) =>
            //    CommandProcessor = null;

            private void TgCollectionEditor_CollectionFormShown(object sender, EventArgs e)
            {
                if (sender is Form form)
                {
                    form.Size = new Size(512, 512);
                    form.Text = "Properties";
                    if (form.Owner is GraphForm graphForm)
                    {
                        form.Font = graphForm.Font;
                        CommandProcessor = AppController.GraphControllers
                            .FirstOrDefault(p => p.View == graphForm)
                            .CommandProcessor;
                    }
                    var propertyGrid = form.Controls.Find("propertyBrowser", true)?[0] as PropertyGrid;
                    PropertyTableController.HidePropertyPagesButton(propertyGrid);
                    propertyGrid.HelpVisible = true;
                }
            }

            private void TgFileNameEditor_InitDialog(object sender, InitDialogEventArgs e) =>
                GraphController.InitTextureDialog(e.OpenFileDialog);

            private CommandProcessor CommandProcessor;
        }

        private static GraphProxyProxy TheGraphProxyProxy = new GraphProxyProxy();
    }
}
