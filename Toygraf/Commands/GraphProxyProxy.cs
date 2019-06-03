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

            internal static GraphProxy GraphProxy;
        }

        private static readonly GraphProxyProxy TheGraphProxyProxy = new GraphProxyProxy();
    }
}
