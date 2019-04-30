﻿namespace Sid.Controllers
{
    using System;
    using System.Windows.Forms;
    using Sid.Models;
    using Sid.Views;

    public class PropertiesController
    {
        public PropertiesController(Model model)
        {
            Model = model;
            View = new PropertiesDialog();
        }

        #region Properties

        private CheckedListBox ClbElements { get => View.ElementCheckboxes; }
        private Model Model { get; set; }
        private Graph Graph { get => Model.Graph; }
        private bool Loading, Updating;

        private PropertiesDialog _view;
        private PropertiesDialog View
        {
            get => _view;
            set
            {
                if (View != null)
                {
                    ColourController.Clear();
                    View.FormClosing -= View_FormClosing;
                    ClbElements.ItemCheck -= ClbElements_ItemCheck;
                    View.btnClose.Click -= BtnClose_Click;
                }
                _view = value;
                if (View != null)
                {
                    AddControls(View.cbAxisColour, View.cbGridColour, View.cbPenColour,
                        View.cbLimitColour, View.cbPaperColour, View.cbFillColour);
                    View.FormClosing += View_FormClosing;
                    ClbElements.ItemCheck += ClbElements_ItemCheck;
                    View.btnClose.Click += BtnClose_Click;
                }
            }
        }

        private ColourController ColourController = new ColourController();

        #endregion

        #region Show/Hide

        public void Show(IWin32Window owner)
        {
            if (!View.Visible)
            {
                GraphRead();
                View.Show(owner);
            }
            else
                View.BringToFront();
        }

        private void BtnClose_Click(object sender, EventArgs e)
        {
            View.Hide();
        }

        private void View_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason != CloseReason.UserClosing)
                return;
            e.Cancel = true;
            View.Hide();
        }

        #endregion

        #region Colours

        private void AddControls(params ComboBox[] controls)
        {
            ColourController.AddControls(controls);
            foreach (var control in controls)
                control.SelectedValueChanged += Control_SelectedValueChanged;
        }

        private void Control_SelectedValueChanged(object sender, EventArgs e) => LiveUpdate(sender, e);

        private void Clear()
        {
            foreach (var control in ColourController.Controls)
                control.SelectedValueChanged -= Control_SelectedValueChanged;
            ColourController.Clear();
        }

        #endregion

        #region Elements

        private void ClbElements_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            if (Updating || e.NewValue == e.CurrentValue)
                return;
            Updating = true;
            int index = e.Index, x = index % 4, y = x + 4, both = y + 4;
            if (index == x)
                SetState(both, e.NewValue == ClbElements.GetItemCheckState(y) ?
                    e.NewValue : CheckState.Indeterminate);
            else if (index == y)
                SetState(both, e.NewValue == ClbElements.GetItemCheckState(x) ?
                    e.NewValue : CheckState.Indeterminate);
            else
            {
                SetState(x, e.NewValue);
                SetState(y, e.NewValue);
            }
            Updating = false;
            if (!Loading)
                View.BeginInvoke((MethodInvoker)(() => LiveUpdate(sender, e)));
        }

        private Elements[] GetElementValues() => (Elements[])Enum.GetValues(typeof(Elements));

        private int ControlToEnum(int index) => index < 11 ? 3 * index % 11 : 11;
        private CheckState GetState(int index) => ClbElements.GetItemCheckState(index);
        private void SetState(int index, CheckState state) => ClbElements.SetItemCheckState(index, state);

        private void ElementsRead()
        {
            var values = GetElementValues();
            var graphElements = Graph.Elements;
            for (var index = 0; index < ClbElements.Items.Count; index++)
            {
                var value = values[ControlToEnum(index)];
                var graphValue = graphElements & value;
                var state = graphValue == value ? CheckState.Checked
                    : graphValue == 0 ? CheckState.Unchecked
                    : CheckState.Indeterminate;
                SetState(index, state);
            }
        }

        private void ElementsWrite()
        {
            var values = GetElementValues();
            Elements graphElements = 0;
            for (var index = 0; index < ClbElements.Items.Count; index++)
                if (GetState(index) == CheckState.Checked)
                    graphElements |= values[ControlToEnum(index)];
            Graph.Elements = graphElements;
        }

        #endregion

        #region Graph Read/Write

        private void GraphRead()
        {
            Loading = true;
            ColourController.SetColour(View.cbAxisColour, Graph.AxisColour);
            ColourController.SetColour(View.cbGridColour, Graph.GridColour);
            ColourController.SetColour(View.cbPenColour, Graph.PenColour);
            ColourController.SetColour(View.cbLimitColour, Graph.LimitColour);
            ColourController.SetColour(View.cbPaperColour, Graph.PaperColour);
            ColourController.SetColour(View.cbFillColour, Graph.FillColour);
            View.sePaperTransparency.Value = Graph.PaperTransparencyPercent;
            View.seFillTransparency.Value = Graph.FillTransparencyPercent;
            ElementsRead();
            Loading = false;
        }

        private void GraphWrite()
        {
            Graph.AxisColour = ColourController.GetColour(View.cbAxisColour);
            Graph.GridColour = ColourController.GetColour(View.cbGridColour);
            Graph.PenColour = ColourController.GetColour(View.cbPenColour);
            Graph.LimitColour = ColourController.GetColour(View.cbLimitColour);
            Graph.PaperColour = ColourController.GetColour(View.cbPaperColour);
            Graph.FillColour = ColourController.GetColour(View.cbFillColour);
            Graph.PaperTransparencyPercent = (int)View.sePaperTransparency.Value;
            Graph.FillTransparencyPercent = (int)View.seFillTransparency.Value;
            ElementsWrite();
        }

        public void LiveUpdate(object sender, EventArgs e)
        {
            if (!Loading)
                GraphWrite();
        }

        #endregion
    }
}
