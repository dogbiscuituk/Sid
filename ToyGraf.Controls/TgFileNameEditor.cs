namespace ToyGraf.Controls
{
    using System;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    public class TgFileNameEditor : FileNameEditor
    {
        protected override void InitializeDialog(OpenFileDialog openFileDialog)
        {
            base.InitializeDialog(openFileDialog);
            OnInitDialog(openFileDialog);
        }

        protected virtual void OnInitDialog(OpenFileDialog openFileDialog) =>
            InitDialog?.Invoke(this, new InitDialogEventArgs(openFileDialog));

        public static event EventHandler<InitDialogEventArgs> InitDialog;
    }

    public class InitDialogEventArgs : EventArgs
    {
        public InitDialogEventArgs(OpenFileDialog openFileDialog) : base() =>
            OpenFileDialog = openFileDialog;

        public OpenFileDialog OpenFileDialog { get; set; }
    }
}
