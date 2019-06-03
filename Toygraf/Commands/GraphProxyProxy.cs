using System;
using System.Linq;
using System.Windows.Forms;
using ToyGraf.Controllers;
using ToyGraf.Controls;
using ToyGraf.Views;

namespace ToyGraf.Commands
{
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
                    if (form.Owner is GraphForm graphForm)
                        GraphProxy = AppController.TheAppController.GraphControllers
                            .FirstOrDefault(p => p.View == graphForm)
                            .GraphProxy;
            }

            private void TgCollectionEditor_CollectionFormClosed(object sender, FormClosedEventArgs e) =>
                GraphProxy = null;

            private void TgFileNameEditor_InitDialog(object sender, InitDialogEventArgs e)
            {
                var target = e.OpenFileDialog;
                target.Filter = "Images (*.bmp;*.gif;*.jpeg;*.jpg;*.png)|*.bmp;*.gif;*.jpeg;*.jpg;*.png|All files " +
    "(*.*)|*.*";
                target.Title = "Select Texture";
            }

            internal static GraphProxy GraphProxy;
        }

        private static readonly GraphProxyProxy TheGraphProxyProxy = new GraphProxyProxy();
    }
}
