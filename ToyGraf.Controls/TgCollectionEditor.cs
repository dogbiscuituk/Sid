namespace ToyGraf.Controls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Windows.Forms;

    public class TgCollectionEditor : CollectionEditor
    {
        public TgCollectionEditor(Type type) : base(type) { }

        public ITypeDescriptorContext GetContext() => Context;

        /// <summary>
        /// Provide static hooks into events on the CollectionForm.
        /// </summary>
        /// <returns></returns>
        protected override CollectionForm CreateCollectionForm()
        {
            var form = base.CreateCollectionForm();
            form.Activated += CollectionFormActivated;
            form.Deactivate += CollectionFormDeactivate;
            form.Enter += CollectionFormEnter;
            form.FormClosed += CollectionFormClosed;
            form.FormClosing += CollectionFormClosing;
            form.Layout += CollectionFormLayout;
            form.Enter += CollectionFormLeave;
            form.Load += CollectionFormLoad;
            form.Shown += CollectionFormShown;
            if (form.Controls[0] is TableLayoutPanel panel)
            {
                if (panel.Controls[4] is ListBox listBox)
                {
                    listBox.DrawMode = DrawMode.Normal;
                }
                if (panel.Controls[5] is PropertyGrid propertyGrid)
                {
                    CollectionFormGridInit?.Invoke(this, new PropertyGridInitEventArgs(propertyGrid));
                    propertyGrid.PropertyValueChanged += CollectionItemPropertyValueChanged;
                }
            }
            return form;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var result = base.EditValue(context, provider, value);

            return result;
        }

        public static event EventHandler
            CollectionFormActivated,
            CollectionFormDeactivate,
            CollectionFormEnter,
            CollectionFormLeave,
            CollectionFormLoad,
            CollectionFormShown;

        public static event FormClosedEventHandler CollectionFormClosed;
        public static event FormClosingEventHandler CollectionFormClosing;
        public static event EventHandler<PropertyGridInitEventArgs> CollectionFormGridInit;
        public static event LayoutEventHandler CollectionFormLayout;
        public static event PropertyValueChangedEventHandler CollectionItemPropertyValueChanged;
    }

    public class PropertyGridInitEventArgs : EventArgs
    {
        public PropertyGridInitEventArgs(PropertyGrid propertyGrid) : base()
            => PropertyGrid = propertyGrid;

        public PropertyGrid PropertyGrid { get; set; }
    }
}
