namespace ToyGraf.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Controllers;
    using ToyGraf.Controls;
    using ToyGraf.Models;
    using ToyGraf.Views;

    partial class CommandProcessor
    {
        internal class CollectionController
        {
            internal CollectionController()
            {
                TgCollectionEditor.CollectionEdited += TgCollectionEditor_CollectionEdited;
                TgCollectionEditor.CollectionFormShown += TgCollectionEditor_CollectionFormShown;
                TgFileNameEditor.InitDialog += TgFileNameEditor_InitDialog;
            }

            private void ProcessStyles(List<StyleProxy> sources)
            {
                // First step: remove any deleted Styles.
                for (int index = Graph.Styles.Count - 1; index >= 0; index--)
                    if (sources.FirstOrDefault(p => p.Index == index) == null)
                        CommandProcessor.GraphDeleteStyle(index);
                // Second step: insert/append any new Styles.
                foreach (var source in sources)
                    if (source.Index < 0)
                    {
                        CommandProcessor.GraphAppendStyle();
                        source.Index = Graph.Styles.Count - 1;
                    }
                // Third & final step: update all Style properties.
                var targets = CommandProcessor.StylesLive;
                for (int index = 0; index < sources.Count; index++)
                    sources[index].CopyTo(targets[index]);
            }

            private void ProcessTraces(List<TraceProxy> sources)
            {
                // First step: remove any deleted Traces.
                for (int index = Graph.Traces.Count - 1; index >= 0; index--)
                    if (sources.FirstOrDefault(p => p.Index == index) == null)
                        CommandProcessor.GraphDeleteTrace(index);
                // Second step: insert/append any new Traces.
                foreach (var source in sources.Where(p => p.Index < 0))
                {
                    CommandProcessor.GraphAppendTrace();
                    source.Index = Graph.Traces.Count - 1;
                }
                // Third & final step: update all Trace properties.
                var targets = CommandProcessor.TracesLive;
                for (int index = 0; index < sources.Count; index++)
                    sources[index].CopyTo(targets[index]);
            }

            private void TgCollectionEditor_CollectionEdited(object sender, CollectionEditedEventArgs e)
            {
                if (e.DialogResult == DialogResult.OK)
                {
                    if (e.Value is List<StyleProxy> styleProxies)
                        ProcessStyles(styleProxies);
                    else if (e.Value is List<TraceProxy> traceProxies)
                        ProcessTraces(traceProxies);
                }
                CommandProcessor = null;
            }

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
                            .FirstOrDefault(p => p.GraphForm == graphForm)
                            .CommandProcessor;
                    }
                    var propertyGrid = form.Controls.Find("propertyBrowser", true)?[0] as PropertyGrid;
                    PropertyGridController.HidePropertyPagesButton(propertyGrid);
                    propertyGrid.HelpVisible = true;
                    if (Graph != null)
                        TraceCounter = Graph.Traces.Count;
                }
            }

            public int TraceCounter;

            public void AssignStyle(Trace trace)
            {
                Graph.InitializeStyle(trace, TraceCounter++);
            }

            private void TgFileNameEditor_InitDialog(object sender, InitDialogEventArgs e) =>
                GraphController.InitTextureDialog(e.OpenFileDialog);

            private CommandProcessor CommandProcessor;
            private Graph Graph => CommandProcessor?.Graph;
        }

        private static CollectionController TheCollectionController = new CollectionController();
    }
}
