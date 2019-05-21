namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    public class CommandController
    {
        public CommandController(AppController parent)
        {
            Parent = parent;
            View.EditUndo.Click += EditUndo_Click;
            View.EditRedo.Click += EditRedo_Click;
            View.EditGroupUndo.Click += EditGroupUndo_Click;
            View.GraphTypeCartesian.Click += GraphTypeCartesian_Click;
            View.tbCartesian.Click += GraphTypeCartesian_Click;
            View.GraphTypePolar.Click += GraphTypePolar_Click;
            View.tbPolar.Click += GraphTypePolar_Click;
            View.tbUndo.ButtonClick += EditUndo_Click;
            View.tbUndo.DropDownOpening += TbUndo_DropDownOpening;
            View.tbRedo.ButtonClick += EditRedo_Click;
            View.tbRedo.DropDownOpening += TbRedo_DropDownOpening;
            UpdateUI();
        }

        public void Execute(params GraphCommand[] commands)
        {
            foreach (var command in commands)
                Redo(command);
            RedoStack.Clear();
        }

        private void EditGroupUndo_Click(object sender, EventArgs e) => GroupUndo = !GroupUndo;

        private void GraphTypePolar_Click(object sender, EventArgs e) =>
            Execute(new GraphPlotTypeCommand(PlotType.Polar));

        private void GraphTypeCartesian_Click(object sender, EventArgs e) =>
            Execute(new GraphPlotTypeCommand(PlotType.Cartesian));

        private void TbUndo_DropDownOpening(object sender, EventArgs e) =>
            CopyCommands(UndoStack, View.tbUndo.DropDownItems, UndoMultiple);

        private void TbRedo_DropDownOpening(object sender, EventArgs e) =>
            CopyCommands(RedoStack, View.tbRedo.DropDownItems, RedoMultiple);

        private AppController Parent;
        private AppForm View => Parent.View;
        private Graph Graph => Parent.Graph;
        private readonly Stack<GraphCommand> UndoStack = new Stack<GraphCommand>();
        private readonly Stack<GraphCommand> RedoStack = new Stack<GraphCommand>();

        private bool GroupUndo
        {
            get => View.EditGroupUndo.Checked;
            set => View.EditGroupUndo.Checked = value;
        }

        private void EditUndo_Click(object sender, EventArgs e) => Undo();
        private void EditRedo_Click(object sender, EventArgs e) => Redo();

        private bool CanUndo { get => UndoStack.Count > 0; }
        private bool CanRedo { get => RedoStack.Count > 0; }
        private void Undo() { if (CanUndo) Undo(UndoStack.Pop()); }
        private void Redo() { if (CanRedo) Redo(RedoStack.Pop()); }

        private void CopyCommands(Stack<GraphCommand> stack,
            ToolStripItemCollection items, EventHandler handler)
        {
            const int MaxItems = 20;
            var commands = stack.ToArray();
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
            RedoStack.Push(command);
            UpdateUI();
        }

        private void Redo(GraphCommand command)
        {
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
            command.Redo(Graph);
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

        private void UpdateUI()
        {
            View.EditUndo.Enabled = View.tbUndo.Enabled = CanUndo;
            View.EditUndo.Text = CanUndo ? $"&Undo {UndoStack.Peek().Action}" : "&Undo";
            View.tbUndo.ToolTipText = CanUndo ? $"Undo {UndoStack.Peek().Action} (^Z)" : "Undo (^Z)";
            View.EditRedo.Enabled = View.tbRedo.Enabled = CanRedo;
            View.EditRedo.Text = CanRedo ? $"&Redo {RedoStack.Peek().Action}" : "&Redo";
            View.tbRedo.ToolTipText = CanRedo ? $"Redo {RedoStack.Peek().Action} (^Y)" : "Redo (^Z)";
            View.EditCut.Enabled = View.tbCut.Enabled = false;
            View.EditCopy.Enabled = View.tbCopy.Enabled = false;
            View.EditPaste.Enabled = View.tbPaste.Enabled = false;
            View.EditDelete.Enabled = View.tbDelete.Enabled = false;
        }
    }
}
