namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;
    using ToyGraf.Views;

    internal class CommandProcessor
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

        internal bool Run(IGraphCommand command, bool run = true)
        {
            var result = run ? Redo(command) : Note(command);
            RedoStack.Clear();
            UpdateUI();
            return result;
        }

        internal void ScrollBy(float xDelta, float yDelta) =>
            SetGraphCentre(new PointF(Graph.Centre.X + xDelta, Graph.Centre.Y + yDelta));

        internal void Zoom(float factor) =>
            SetGraphWidth(Graph.Width * factor);

        internal void ZoomReset()
        {
            SetGraphCentre(Graph.OriginalCentre);
            SetGraphWidth(Graph.OriginalWidth);
        }

        #endregion

        #region Command Runners

        internal bool GraphDeleteSeries(int index) => Run(new GraphDeleteSeriesCommand(index));
        internal bool GraphInsertSeries(int index) => Run(new GraphInsertSeriesCommand(index));

        internal bool SetGraphAxisColour(Color value, bool run = true) => Run(new GraphAxisColourCommand(value), run);
        internal bool SetGraphCentre(PointF value, bool run = true) => Run(new GraphCentreCommand(value), run);
        internal bool SetGraphDomainGraphWidth(bool value, bool run = true) => Run(new GraphDomainGraphWidthCommand(value), run);
        internal bool SetGraphDomainMaxCartesian(float value, bool run = true) => Run(new GraphDomainMaxCartesianCommand(value), run);
        internal bool SetGraphDomainMaxPolar(float value, bool run = true) => Run(new GraphDomainMaxPolarCommand(value), run);
        internal bool SetGraphDomainMinCartesian(float value, bool run = true) => Run(new GraphDomainMinCartesianCommand(value), run);
        internal bool SetGraphDomainMinPolar(float value, bool run = true) => Run(new GraphDomainMinPolarCommand(value), run);
        internal bool SetGraphDomainPolarDegrees(bool value, bool run = true) => Run(new GraphDomainPolarDegreesCommand(value), run);
        internal bool SetGraphElements(Elements value, bool run = true) => Run(new GraphElementsCommand(value), run);
        internal bool SetGraphFillColour1(Color value, bool run = true) => Run(new GraphFillColour1Command(value), run);
        internal bool SetGraphFillColour2(Color value, bool run = true) => Run(new GraphFillColour2Command(value), run);
        internal bool SetGraphFillTransparencyPercent(int value, bool run = true) => Run(new GraphFillTransparencyPercentCommand(value), run);
        internal bool SetGraphInterpolation(Interpolation value, bool run = true) => Run(new GraphInterpolationCommand(value), run);
        internal bool SetGraphLimitColour(Color value, bool run = true) => Run(new GraphLimitColourCommand(value), run);
        internal bool SetGraphOptimization(Optimization value, bool run = true) => Run(new GraphOptimizationCommand(value), run);
        internal bool SetGraphPaperColour(Color value, bool run = true) => Run(new GraphPaperColourCommand(value), run);
        internal bool SetGraphPaperTransparencyPercent(int value, bool run = true) => Run(new GraphPaperTransparencyPercentCommand(value), run);
        internal bool SetGraphPenColour(Color value, bool run = true) => Run(new GraphPenColourCommand(value), run);
        internal bool SetGraphPlotType(PlotType value, bool run = true) => Run(new GraphPlotTypeCommand(value), run);
        internal bool SetGraphReticleColour(Color colour, bool run = true) => Run(new GraphReticleColourCommand(colour), run);
        internal bool SetGraphStepCount(int value, bool run = true) => Run(new GraphStepCountCommand(value), run);
        internal bool SetGraphTickStyles(TickStyles value, bool run = true) => Run(new GraphTickStylesCommand(value), run);
        internal bool SetGraphTitle(string value, bool run = true) => Run(new GraphTitleCommand(value), run);
        internal bool SetGraphWidth(float value, bool run = true) => Run(new GraphWidthCommand(value), run);

        internal bool SetSeriesBrushType(int index, BrushType value, bool run = true) => Run(new SeriesBrushTypeCommand(index, value), run);
        internal bool SetSeriesFillColour1(int index, Color value, bool run = true) => Run(new SeriesFillColour1Command(index, value), run);
        internal bool SetSeriesFillColour2(int index, Color value, bool run = true) => Run(new SeriesFillColour2Command(index, value), run);
        internal bool SetSeriesFillTransparencyPercent(int index, int value, bool run = true) => Run(new SeriesFillTransparencyPercentCommand(index, value), run);
        internal bool SetSeriesFormula(int index, string value, bool run = true) => Run(new SeriesFormulaCommand(index, value), run);
        internal bool SetSeriesGradientMode(int index, LinearGradientMode value, bool run = true) => Run(new SeriesGradientModeCommand(index, value), run);
        internal bool SetSeriesHatchStyle(int index, HatchStyle value, bool run = true) => Run(new SeriesHatchStyleCommand(index, value), run);
        internal bool SetSeriesLimitColour(int index, Color value, bool run = true) => Run(new SeriesLimitColourCommand(index, value), run);
        internal bool SetSeriesPenColour(int index, Color value, bool run = true) => Run(new SeriesPenColourCommand(index, value), run);
        internal bool SetSeriesPenStyle(int index, DashStyle value, bool run = true) => Run(new SeriesPenStyleCommand(index, value), run);
        internal bool SetSeriesPenWidth(int index, float value, bool run = true) => Run(new SeriesPenWidthCommand(index, value), run);
        internal bool SetSeriesStepCount(int index, int value, bool run = true) => Run(new SeriesStepCountCommand(index, value), run);
        internal bool SetSeriesTexture(int index, string value, bool run = true) => Run(new SeriesTextureCommand(index, value), run);
        internal bool SetSeriesTexturePath(int index, string value, bool run = true) => Run(new SeriesTexturePathCommand(index, value), run);
        internal bool SetSeriesVisible(int index, bool value, bool run = true) => Run(new SeriesVisibleCommand(index, value), run);
        internal bool SetSeriesWrapMode(int index, WrapMode value, bool run = true) => Run(new SeriesWrapModeCommand(index, value), run);

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

        private void Undo() { if (CanUndo) Undo(UndoStack.Pop()); }
        private void Redo() { if (CanRedo) Redo(RedoStack.Pop()); }

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

        private void Undo(IGraphCommand command)
        {
            command.Do(Graph);
            RedoStack.Push(command);
            UpdateUI();
        }

        private bool Redo(IGraphCommand command)
        {
            var result = command.Do(Graph);
            Note(command);
            return result;
        }

        private bool Note(IGraphCommand command)
        {
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
                    if (canGroup && gs.Value == null)
                        gs.Value = Graph.Series[sf.Index];
                }
            };
            if (!canGroup)
                UndoStack.Push(command);
            UpdateUI();
            return true;
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
