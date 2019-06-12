﻿namespace ToyGraf.Controls
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
            var collectionForm = base.CreateCollectionForm();
            collectionForm.Activated += CollectionFormActivated;
            collectionForm.Deactivate += CollectionFormDeactivate;
            collectionForm.Enter += CollectionFormEnter;
            collectionForm.FormClosed += CollectionForm_FormClosed;
            collectionForm.FormClosed += CollectionFormClosed;
            collectionForm.FormClosing += CollectionFormClosing;
            collectionForm.Layout += CollectionFormLayout;
            collectionForm.Enter += CollectionFormLeave;
            collectionForm.Load += CollectionFormLoad;
            collectionForm.Shown += CollectionFormShown;
            if (collectionForm.Controls[0] is TableLayoutPanel panel)
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
            return collectionForm;
        }

        private void CollectionForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            DialogResult = ((Form)sender).DialogResult;
        }

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            var result = base.EditValue(context, provider, value);
            if (DialogResult == DialogResult.OK)
                CollectionEdited?.Invoke(this, new CollectionEditedEventArgs(context, provider, value));
            return result;
        }

        private DialogResult DialogResult;

        public static event EventHandler
            CollectionFormActivated,
            CollectionFormDeactivate,
            CollectionFormEnter,
            CollectionFormLeave,
            CollectionFormLoad,
            CollectionFormShown;

        public static event EventHandler<CollectionEditedEventArgs> CollectionEdited;
        public static event FormClosedEventHandler CollectionFormClosed;
        public static event FormClosingEventHandler CollectionFormClosing;
        public static event EventHandler<PropertyGridInitEventArgs> CollectionFormGridInit;
        public static event LayoutEventHandler CollectionFormLayout;
        public static event PropertyValueChangedEventHandler CollectionItemPropertyValueChanged;
    }

    public class CollectionEditedEventArgs : EventArgs
    {
        public CollectionEditedEventArgs(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            Context = context;
            Provider = provider;
            Value = value;
        }

        ITypeDescriptorContext Context { get; set; }
        IServiceProvider Provider { get; set; }
        object Value { get; set; }
    }

    public class PropertyGridInitEventArgs : EventArgs
    {
        public PropertyGridInitEventArgs(PropertyGrid propertyGrid) : base()
            => PropertyGrid = propertyGrid;

        public PropertyGrid PropertyGrid { get; set; }
    }
}
