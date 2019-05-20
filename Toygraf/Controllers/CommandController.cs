namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Views;

    public class CommandController
    {
        public CommandController(AppController parent)
        {
            Parent = parent;
            View.EditMenu.DropDownOpening += EditMenu_DropDownOpening;
            View.EditUndo.Click += EditUndo_Click;
            View.EditRedo.Click += EditRedo_Click;
            View.tbUndo.DropDownOpening += TbUndo_DropDownOpening;
            View.tbRedo.DropDownOpening += TbRedo_DropDownOpening;

            /*
            Execute(new GraphDomainGraphWidthCommand(true));
            Execute(new GraphDomainPolarDegreesCommand(false));

            Execute(new GraphAxisColourCommand(System.Drawing.Color.Black));
            Execute(new GraphFillColourCommand(System.Drawing.Color.Brown));
            Execute(new GraphLimitColourCommand(System.Drawing.Color.Red));
            Execute(new GraphPaperColourCommand(System.Drawing.Color.Orange));
            Execute(new GraphPenColourCommand(System.Drawing.Color.Yellow));
            Execute(new GraphReticleColourCommand(System.Drawing.Color.Green));

            Execute(new GraphElementsCommand(
                Models.Enumerations.Elements.Axes | Models.Enumerations.Elements.HorizontalWires | Models.Enumerations.Elements.Calibration
                ));

            Execute(new GraphInterpolationCommand(Models.Enumerations.Interpolation.CardinalSpline));
            Execute(new GraphOptimizationCommand(Models.Enumerations.Optimization.HighQuality));
            Execute(new GraphPlotTypeCommand(Models.Enumerations.PlotType.Polar));
            Execute(new GraphTickStylesCommand(Models.Enumerations.TickStyles.Both));

            Execute(new GraphDomainMaxCartesianCommand(11));
            Execute(new GraphDomainMaxPolarCommand(180));
            Execute(new GraphDomainMinCartesianCommand(-11));
            Execute(new GraphDomainMinPolarCommand(-180));
            Execute(new GraphWidthCommand(30));

            Execute(new GraphFillTransparencyPercentCommand(50));
            Execute(new GraphPaperTransparencyPercentCommand(90));
            Execute(new GraphStepCountCommand(1000));

            Execute(new GraphCentreCommand(new System.Drawing.PointF(3, 4)));

            Execute(new GraphInsertSeriesCommand(0));
            Execute(new GraphRemoveSeriesCommand(0));
            */
        }

        private void TbUndo_DropDownOpening(object sender, EventArgs e) =>
            CopyCommands(UndoStack, View.tbUndo.DropDownItems, UndoMultiple);

        private void TbRedo_DropDownOpening(object sender, EventArgs e) =>
            CopyCommands(RedoStack, View.tbRedo.DropDownItems, RedoMultiple);

        private void EditMenu_DropDownOpening(object sender, EventArgs e)
        {
            View.EditUndo.Enabled = CanUndo;
            View.EditRedo.Enabled = CanRedo;
        }

        public void Execute(GraphCommand command)
        {
            Redo(command);
            RedoStack.Clear();
        }

        private AppController Parent;
        private AppForm View => Parent.View;
        private Graph Graph => Parent.Graph;
        private readonly Stack<GraphCommand> UndoStack = new Stack<GraphCommand>();
        private readonly Stack<GraphCommand> RedoStack = new Stack<GraphCommand>();

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
        }

        private void Redo(GraphCommand command)
        {
            command.Redo(Graph);
            UndoStack.Push(command);
        }

        private void UndoMultiple(object sender, EventArgs e)
        {
            do Undo(); while (RedoStack.Peek() != ((ToolStripItem)sender).Tag);
        }

        private void RedoMultiple(object sender, EventArgs e)
        {
            do Redo(); while (UndoStack.Peek() != ((ToolStripItem)sender).Tag);
        }
    }
}
