namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class CommandProcessor
    {
        #region Internal Interface

        internal CommandProcessor(AppController parent)
        {
            Parent = parent;
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
            //
            UpdateUI();
        }

        internal void Run(params GraphCommand[] commands)
        {
            foreach (var command in commands)
                Redo(command);
            RedoStack.Clear();
        }

        internal void ScrollBy(float xDelta, float yDelta) => Run(new GraphCentreCommand(
            Graph.Centre.X + xDelta,
            Graph.Centre.Y + yDelta));

        internal void Zoom(float factor) => Run(new GraphWidthCommand(Graph.Width * factor));

        internal void ZoomReset() => Run(
            new GraphCentreCommand(Graph.OriginalCentre),
            new GraphWidthCommand(Graph.OriginalWidth));

        #endregion

        #region Private Properties

        private AppController Parent;
        private AppForm View => Parent.View;
        private Graph Graph => Parent.Graph;
        private readonly Stack<GraphCommand> UndoStack = new Stack<GraphCommand>();
        private readonly Stack<GraphCommand> RedoStack = new Stack<GraphCommand>();

        private bool CanUndo { get => UndoStack.Count > 0; }
        private bool CanRedo { get => RedoStack.Count > 0; }

        private bool GroupUndo
        {
            get => View.EditGroupUndo.Checked;
            set => View.EditGroupUndo.Checked = value;
        }

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

        #endregion

        #region Private Methods

        private void Undo() { if (CanUndo) Undo(UndoStack.Pop()); }
        private void Redo() { if (CanRedo) Redo(RedoStack.Pop()); }

        private void Copy(Stack<GraphCommand> stack, ToolStripDropDownItem item, EventHandler handler)
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

        private void Undo(GraphCommand command)
        {
            command.Undo(Graph);
            command = command.Invert();
            RedoStack.Push(command);
            UpdateUI();
        }

        private void Redo(GraphCommand command)
        {
            command.Redo(Graph);
            command = command.Invert();
            var group = GroupUndo && CanUndo;
            if (group)
            {
                var prevCmd = UndoStack.Peek();
                group = !(command is GraphSeriesCommand) && command.GetType() == prevCmd.GetType();
                if (group && command is SeriesCommand s)
                    group = s.Index == ((SeriesCommand)prevCmd).Index;
            };
            if (!group)
                UndoStack.Push(command);
            UpdateUI();
        }

        private void UndoMultiple(object sender, EventArgs e)
        {
            var peek = ((ToolStripItem)sender).Tag;
            do Undo(); while (RedoStack.Peek() != peek);
        }

        private void RedoMultiple(object sender, EventArgs e)
        {
            var peek = ((ToolStripItem)sender).Tag;
            do Redo(); while (UndoStack.Peek() != peek);
        }

        private void Scroll(float xFactor, float yFactor) => Run(new GraphCentreCommand(
            Graph.Centre.X + Graph.Width * xFactor,
            Graph.Centre.Y + Graph.Width * yFactor));

        private void ScrollTo(float x, float y) => Run(new GraphCentreCommand(x, y));

        private string UndoAction => UndoStack.Peek().UndoAction;
        private string RedoAction => RedoStack.Peek().RedoAction;

        private void UpdateUI()
        {
            View.EditUndo.Enabled = View.tbUndo.Enabled = CanUndo;
            View.EditUndo.Text = CanUndo ? $"&Undo {UndoAction}" : "&Undo";
            View.tbUndo.ToolTipText = CanUndo ? $"Undo {UndoAction} (^Z)" : "Undo (^Z)";
            View.EditRedo.Enabled = View.tbRedo.Enabled = CanRedo;
            View.EditRedo.Text = CanRedo ? $"&Redo {RedoAction}" : "&Redo";
            View.tbRedo.ToolTipText = CanRedo ? $"Redo {RedoAction} (^Y)" : "Redo (^Z)";
            View.EditCut.Enabled = View.tbCut.Enabled = false;
            View.EditCopy.Enabled = View.tbCopy.Enabled = false;
            View.EditPaste.Enabled = View.tbPaste.Enabled = false;
            View.EditDelete.Enabled = View.tbDelete.Enabled = false;
        }

        #endregion
    }
}
