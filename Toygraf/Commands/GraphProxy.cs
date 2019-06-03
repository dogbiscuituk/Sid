namespace ToyGraf.Commands
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Controllers;
    using ToyGraf.Controls;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal partial class GraphProxy
    {
        #region Internal Interface

        internal GraphProxy(GraphController graphController)
        {
            GraphController = graphController;
            // Model
            GraphController.Model.Cleared += Model_Cleared;
            // Edit
            View.EditUndo.Click += EditUndo_Click;
            View.tbUndo.ButtonClick += EditUndo_Click;
            View.tbUndo.DropDownOpening += TbUndo_DropDownOpening;
            View.EditRedo.Click += EditRedo_Click;
            View.tbRedo.ButtonClick += EditRedo_Click;
            View.tbRedo.DropDownOpening += TbRedo_DropDownOpening;
            View.EditGroupUndo.Click += EditGroupUndo_Click;
            // Graph
            View.GraphTypeCartesian.Click += GraphTypeCartesian_Click;
            View.tbCartesian.Click += GraphTypeCartesian_Click;
            View.GraphTypePolar.Click += GraphTypePolar_Click;
            View.tbPolar.Click += GraphTypePolar_Click;
            // Zoom
            View.ZoomIn.Click += ZoomIn_Click;
            View.ZoomOut.Click += ZoomOut_Click;
            View.ZoomReset.Click += ZoomReset_Click;
            // Scroll
            View.ScrollLeft.Click += ScrollLeft_Click;
            View.ScrollRight.Click += ScrollRight_Click;
            View.ScrollUp.Click += ScrollUp_Click;
            View.ScrollDown.Click += ScrollDown_Click;
            View.ScrollCentre.Click += ScrollCentre_Click;
            // Done
            UpdateUI();
        }

        internal void Clear()
        {
            UndoStack.Clear();
            RedoStack.Clear();
            UpdateUI();
        }

        internal void ScrollBy(float xDelta, float yDelta) =>
            Centre = new PointF(Graph.Centre.X + xDelta, Graph.Centre.Y + yDelta);

        internal void Zoom(float factor) => Width *= factor;

        internal void ZoomReset()
        {
            Centre = Graph.OriginalCentre;
            Width = Graph.OriginalWidth;
        }

        #endregion

        #region Command Runners

        internal GraphSeries this[int index] { get => new GraphSeries(this, index); }

        [Editor(typeof(TgCollectionEditor), typeof(UITypeEditor))]
        public List<GraphSeries> Series
        {
            get => Graph.Series.Select(s => new GraphSeries(this, Graph.Series.IndexOf(s))).ToList();
        }

        internal bool GraphDeleteSeries(int index) => Run(new GraphDeleteSeriesCommand(index));
        internal bool GraphInsertSeries(int index) => Run(new GraphInsertSeriesCommand(index));

        #endregion

        #region Private Properties

        private GraphController GraphController;
        private GraphForm View => GraphController.View;
        private Graph Graph => GraphController.Graph;
        private readonly Stack<IGraphCommand> UndoStack = new Stack<IGraphCommand>();
        private readonly Stack<IGraphCommand> RedoStack = new Stack<IGraphCommand>();

        private bool CanUndo { get => UndoStack.Count > 0; }
        private bool CanRedo { get => RedoStack.Count > 0; }

        private bool GroupUndo
        {
            get => View.EditGroupUndo.Checked;
            set => View.EditGroupUndo.Checked = value;
        }

        private string UndoAction => UndoStack.Peek().UndoAction;
        private string RedoAction => RedoStack.Peek().RedoAction;

        #endregion

        #region Private Event Handlers

        // Edit
        private void EditUndo_Click(object sender, EventArgs e) => Undo();
        private void TbUndo_DropDownOpening(object sender, EventArgs e) => Copy(UndoStack, View.tbUndo, UndoMultiple);
        private void EditRedo_Click(object sender, EventArgs e) => Redo();
        private void TbRedo_DropDownOpening(object sender, EventArgs e) => Copy(RedoStack, View.tbRedo, RedoMultiple);
        private void EditGroupUndo_Click(object sender, EventArgs e) => GroupUndo = !GroupUndo;
        // Graph
        private void GraphTypePolar_Click(object sender, EventArgs e) => Run(new GraphPlotTypeCommand(PlotType.Polar));
        private void GraphTypeCartesian_Click(object sender, EventArgs e) => Run(new GraphPlotTypeCommand(PlotType.Cartesian));
        // Zoom
        private void ZoomIn_Click(object sender, EventArgs e) => Zoom(10.0f / 11.0f);
        private void ZoomOut_Click(object sender, EventArgs e) => Zoom(11.0f / 10.0f);
        private void ZoomReset_Click(object sender, EventArgs e) => ZoomReset();
        // Scroll
        private void ScrollLeft_Click(object sender, EventArgs e) => Scroll(-0.1f, 0);
        private void ScrollRight_Click(object sender, EventArgs e) => Scroll(0.1f, 0);
        private void ScrollUp_Click(object sender, EventArgs e) => Scroll(0, 0.1f);
        private void ScrollDown_Click(object sender, EventArgs e) => Scroll(0, -0.1f);
        private void ScrollCentre_Click(object sender, EventArgs e) => ScrollTo(0, 0);
        // Model
        private void Model_Cleared(object sender, EventArgs e) => Clear();

        #endregion

        #region Private Methods

        private void Copy(Stack<IGraphCommand> stack, ToolStripDropDownItem item, EventHandler handler)
        {
            const int MaxItems = 20;
            var commands = stack.ToArray();
            var items = item.DropDownItems;
            items.Clear();
            for (int n = 0; n < Math.Min(commands.Length, MaxItems); n++)
            {
                var command = commands[n];
                items.Add(command.ToString(), null, handler).Tag = command;
            }
        }

        private void Redo() { if (CanRedo) Redo(RedoStack.Pop()); }

        private bool Redo(IGraphCommand command)
        {
            var result = command.Do(Graph);
            var canGroup = false;
            if (GroupUndo && CanUndo)
            {
                var prevCmd = UndoStack.Peek();
                canGroup = !(command is GraphSeriesCommand) && command.GetType() == prevCmd.GetType();
                if (canGroup && command is ISeriesPropertyCommand s)
                    canGroup = s.Index == ((ISeriesPropertyCommand)prevCmd).Index;
                else if (command is ISeriesPropertyCommand sf && prevCmd is IGraphSeriesCommand gs)
                {
                    canGroup = !gs.Add && sf.Index == gs.Index;
                    if (canGroup && gs.Series == null)
                        gs.Series = Graph.Series[sf.Index];
                }
            };
            if (!canGroup)
                UndoStack.Push(command);
            UpdateUI();
            return result;
        }

        private void RedoMultiple(object sender, EventArgs e)
        {
            var peek = ((ToolStripItem)sender).Tag;
            do Redo(); while (UndoStack.Peek() != peek);
        }

        private bool Run(IGraphCommand command)
        {
            var result = Redo(command);
            RedoStack.Clear();
            UpdateUI();
            return result;
        }

        private void Scroll(float xFactor, float yFactor) => Run(new GraphCentreCommand(
            Graph.Centre.X + Graph.Width * xFactor,
            Graph.Centre.Y + Graph.Width * yFactor));

        private void ScrollTo(float x, float y) => Run(new GraphCentreCommand(x, y));

        private void Undo() { if (CanUndo) Undo(UndoStack.Pop()); }

        private void Undo(IGraphCommand command)
        {
            command.Do(Graph);
            RedoStack.Push(command);
            UpdateUI();
        }

        private void UndoMultiple(object sender, EventArgs e)
        {
            var peek = ((ToolStripItem)sender).Tag;
            do Undo(); while (RedoStack.Peek() != peek);
        }

        private void UpdateUI()
        {
            string
                undo = CanUndo ? $"Undo {UndoAction}" : "Undo",
                redo = CanRedo ? $"Redo {RedoAction}" : "Redo";
            View.EditUndo.Enabled = View.tbUndo.Enabled = CanUndo;
            View.EditRedo.Enabled = View.tbRedo.Enabled = CanRedo;
            View.EditUndo.Text = $"&{undo}";
            View.EditRedo.Text = $"&{redo}";
            View.tbUndo.ToolTipText = $"{undo} (^Z)";
            View.tbRedo.ToolTipText = $"{redo} (^Y)";
            View.EditCut.Enabled = View.tbCut.Enabled = false;
            View.EditCopy.Enabled = View.tbCopy.Enabled = false;
            View.EditPaste.Enabled = View.tbPaste.Enabled = false;
            View.EditDelete.Enabled = View.tbDelete.Enabled = false;
        }

        #endregion
    }
}
