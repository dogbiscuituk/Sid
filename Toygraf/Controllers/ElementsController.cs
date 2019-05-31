namespace ToyGraf.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows.Forms;
    using ToyGraf.Models;
    using ToyGraf.Models.Commands;
    using ToyGraf.Models.Enumerations;

    internal class ElementsController
    {
        #region Internal Interface

        internal ElementsController(GraphPropertiesController graphPropertiesController)
        {
            GraphPropertiesController = graphPropertiesController;
            View = graphPropertiesController.View.ElementCheckboxes;
            for (var index = 0; index < 12; index++)
                States[index] = View.GetItemCheckState(index);
        }

        internal void ElementsRead()
        {
            var values = GetElementValues();
            var graphElements = Graph.Elements;
            Updating = true;
            for (var index = 0; index < 12; index++)
            {
                var value = values[ControlToEnum(index)];
                var graphValue = graphElements & value;
                var state = graphValue == value ? CheckState.Checked
                    : graphValue == 0 ? CheckState.Unchecked
                    : CheckState.Indeterminate;
                SetState(index, state);
            }
            Updating = false;
        }

        #endregion

        #region Private Properties

        private CheckedListBox _view;
        private CheckedListBox View
        {
            get => _view;
            set
            {
                _view = value;
                View.ItemCheck += View_ItemCheck;
            }
        }

        private GraphPropertiesController GraphPropertiesController;
        private Graph Graph => GraphPropertiesController.Graph;
        private readonly CheckState[] States = new CheckState[12];
        private bool Updating;

        #endregion

        #region Private Event Handlers

        private void View_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Updating)
                return;
            var index = e.Index;
            CheckState
                oldState = e.CurrentValue,
                newState = e.NewValue;
            if (newState == States[index])
                return;
            int x = index % 4, y = x + 4, z = y + 4;
            Updating = true;
            if (index == x)
            {
                SetState(x, newState);
                SetState(z, newState == States[y] ? newState : CheckState.Indeterminate);
            }
            else if (index == y)
            {
                SetState(y, newState);
                SetState(z, newState == States[x] ? newState : CheckState.Indeterminate);
            }
            else
            {
                SetState(x, newState);
                SetState(y, newState);
                SetState(z, newState);
            }
            Updating = false;
            ElementsWrite();
        }

        #endregion

        #region Private Methods

        private void ElementsWrite()
        {
            var values = GetElementValues();
            Elements elements = 0;
            for (var index = 0; index < 8; index++)
                if (States[index] == CheckState.Checked)
                    elements |= values[ControlToEnum(index)];
            if (GraphPropertiesController.Graph.Elements != elements)
                GraphPropertiesController.GraphController.CommandProcessor.Run(new GraphElementsCommand(elements));
        }

        private void SetState(int index, CheckState state) => View.SetItemCheckState(index, States[index] = state);

        private static int ControlToEnum(int index) => index < 11 ? 3 * index % 11 : 11;
        private static List<Elements> GetElementValues() =>
            ((Elements[])Enum.GetValues(typeof(Elements))).Skip(1).Take(12).ToList();

        #endregion
    }
}
