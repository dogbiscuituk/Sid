﻿namespace ToyGraf.Commands
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Windows.Forms;
    using ToyGraf.Controllers;
    using ToyGraf.Models;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal partial class CommandProcessor
    {
        #region Internal Interface

        internal CommandProcessor(GraphController graphController)
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

        internal void GraphDeleteTrace(int index) => Run(new TraceDeleteCommand(index));
        internal void GraphInsertTrace(int index) => Run(new TraceInsertCommand(index));

        #endregion

        #region Private Properties

        private GraphController GraphController;
        private GraphForm View => GraphController.View;
        private Graph Graph => GraphController.Graph;
        private readonly Stack<ICommand> UndoStack = new Stack<ICommand>();
        private readonly Stack<ICommand> RedoStack = new Stack<ICommand>();

        private bool CanUndo { get => UndoStack.Count > 0; }
        private bool CanRedo { get => RedoStack.Count > 0; }

        private bool GroupUndo => AppController.Options.GroupUndo;
        private string UndoAction => UndoStack.Peek().UndoAction;
        private string RedoAction => RedoStack.Peek().RedoAction;

        #endregion

        #region Private Event Handlers

        // Edit
        private static void UndoRedoItem_MouseEnter(object sender, EventArgs e) => HighlightUndoRedoItems((ToolStripItem)sender);
        private void EditUndo_Click(object sender, EventArgs e) => Undo();
        private void TbUndo_DropDownOpening(object sender, EventArgs e) => Copy(UndoStack, View.tbUndo, UndoMultiple);
        private void EditRedo_Click(object sender, EventArgs e) => Redo();
        private void TbRedo_DropDownOpening(object sender, EventArgs e) => Copy(RedoStack, View.tbRedo, RedoMultiple);
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

        private void Copy(Stack<ICommand> source, ToolStripDropDownItem target, EventHandler handler)
        {
            const int MaxItems = 20;
            var commands = source.ToArray();
            var items = target.DropDownItems;
            items.Clear();
            for (int n = 0; n < Math.Min(commands.Length, MaxItems); n++)
            {
                var command = commands[n];
                var item = items.Add(command.ToString(), null, handler);
                item.Tag = command;
                item.MouseEnter += UndoRedoItem_MouseEnter;
            }
        }

        private static void HighlightUndoRedoItems(ToolStripItem activeItem)
        {
            var parent = activeItem.GetCurrentParent();
            var index = parent.Items.IndexOf(activeItem);
            foreach (ToolStripItem item in parent.Items)
                item.BackColor = Color.FromKnownColor(parent.Items.IndexOf(item) <= index
                    ? KnownColor.GradientActiveCaption
                    : KnownColor.Control);
        }

        private void Redo() { if (CanRedo) Redo(RedoStack.Pop()); }

        private void Redo(ICommand command)
        {
            if (!command.Do(Graph))
                return;
            var canGroup = false;
            if (GroupUndo && CanUndo)
            {
                var prevCmd = UndoStack.Peek();
                canGroup = !(command is ICollectionCommand) && command.GetType() == prevCmd.GetType();
                if (canGroup && command is IStylePropertyCommand sp1)
                    canGroup = sp1.Index == ((IStylePropertyCommand)prevCmd).Index;
                else if (canGroup && command is ITracePropertyCommand tp1)
                    canGroup = tp1.Index == ((ITracePropertyCommand)prevCmd).Index;
                else if (command is IStylePropertyCommand sp2 && prevCmd is IStylesCommand sc2)
                {
                    canGroup = !sc2.Adding && sp2.Index == sc2.Index;
                    if (canGroup && sc2.Value == null)
                        sc2.Value = Graph.Styles[sp2.Index];
                }
                else if (command is ITracePropertyCommand tp2 && prevCmd is ITracesCommand tc2)
                {
                    canGroup = !tc2.Adding && tp2.Index == tc2.Index;
                    if (canGroup && tc2.Value == null)
                        tc2.Value = Graph.Traces[tp2.Index];
                }
                else
                    canGroup = false;
            };
            if (!canGroup)
                UndoStack.Push(command);
            UpdateUI();
        }

        private void RedoMultiple(object sender, EventArgs e)
        {
            var peek = ((ToolStripItem)sender).Tag;
            do Redo(); while (UndoStack.Peek() != peek);
        }

        private void Run(ICommand command)
        {
            Redo(command);
            RedoStack.Clear();
            UpdateUI();
        }

        private void Scroll(float xFactor, float yFactor) => Run(new GraphCentreCommand(
            Graph.Centre.X + Graph.Width * xFactor,
            Graph.Centre.Y + Graph.Width * yFactor));

        private void ScrollTo(float x, float y) => Run(new GraphCentreCommand(x, y));

        private void Undo() { if (CanUndo) Undo(UndoStack.Pop()); }

        private void Undo(ICommand command)
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
