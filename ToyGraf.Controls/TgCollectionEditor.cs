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

        protected override CollectionForm CreateCollectionForm()
        {
            var form = base.CreateCollectionForm();
            form.FormClosed += CollectionFormClosed;
            form.Shown += CollectionFormShown;
            if (form.Controls[0] is TableLayoutPanel panel)
                if (panel.Controls[5] is PropertyGrid grid)
                    grid.PropertyValueChanged += OnCollectionItemPropertyValueChanged;
            return form;
        }

        protected virtual void OnCollectionFormClosed(object sender, FormClosedEventArgs e)
        {
            CollectionFormClosed?.Invoke(this, e);
        }

        protected virtual void OnCollectionItemPropertyValueChanged(object sender, PropertyValueChangedEventArgs e) =>
            CollectionItemPropertyValueChanged?.Invoke(this, e);

        public static event FormClosedEventHandler CollectionFormClosed;
        public static event EventHandler CollectionFormShown;
        public static event PropertyValueChangedEventHandler CollectionItemPropertyValueChanged;
    }
}
