﻿namespace ToyGraf.Controllers
{
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Windows.Forms;
    using ToyGraf.Models;

    /// <summary>
    /// "Single Document Interface" Controller.
    /// 
    /// Extend MruController to provide file Open and Save dialogs.
    /// Keep track of the document/model's "Modified" state, prompting for "Save" as necessary
    /// (for example, prior to "File|New" or "File|Open", or application closing).
    /// </summary>
    public abstract class SdiController : MruController
	{
		protected SdiController(Model model, string filter, string subKeyName, ToolStripDropDownItem recentMenu)
			: base(model, subKeyName, recentMenu)
		{
			OpenFileDialog = new OpenFileDialog { Filter = filter, Title = "Select the file to open" };
			SaveFileDialog = new SaveFileDialog { Filter = filter, Title = "Save file" };
		}

        #region Public Interface

        public bool Clear()
		{
			var result = SaveIfModified();
			if (result)
			{
				FilePath = string.Empty;
                ClearDocument();
                Model.Modified = false;
            }
            return result;
		}

		public bool Open() => SaveIfModified()
            && OpenFileDialog.ShowDialog() == DialogResult.OK
            && LoadFromFile(OpenFileDialog.FileName);

		public bool Save() => string.IsNullOrEmpty(FilePath) ? SaveAs() : SaveToFile(FilePath);

		public bool SaveAs() => SaveFileDialog.ShowDialog() == DialogResult.OK
            && SaveToFile(SaveFileDialog.FileName);

		public bool SaveIfModified()
		{
			if (Model.Modified)
				switch (MessageBox.Show(
					"The contents of this file have changed. Do you want to save the changes?",
					"File modified",
					MessageBoxButtons.YesNoCancel,
					MessageBoxIcon.Warning))
				{
					case DialogResult.Yes:
						return Save();
					case DialogResult.No:
						return true;
					case DialogResult.Cancel:
						return false;
				}
			return true;
		}

        public event EventHandler<CancelEventArgs> FileLoading, FileSaving;
        public event EventHandler FileLoaded, FilePathChanged, FileSaved;

        #endregion

        #region Properties

        private string _filePath = string.Empty;
		protected string FilePath
        {
            get => _filePath;
            set
            {
                if (FilePath != value)
                {
                    _filePath = value;
                    OnFilePathChanged();
                }
            }
        }

        private readonly OpenFileDialog OpenFileDialog;
        private readonly SaveFileDialog SaveFileDialog;

        #endregion

        #region Event Handling

        protected virtual void OnFilePathChanged()
        {
            FilePathChanged?.Invoke(this, EventArgs.Empty);
        }

        protected virtual void OnFileLoaded() => FileLoaded?.Invoke(this, EventArgs.Empty);

        protected virtual bool OnFileLoading()
        {
            var result = true;
            var fileLoading = FileLoading;
            if (fileLoading != null)
            {
                var e = new CancelEventArgs();
                fileLoading(this, e);
                result = !e.Cancel;
            }
            return result;
        }

        protected virtual void OnFileSaved() => FileSaved?.Invoke(this, EventArgs.Empty);

        protected virtual bool OnFileSaving()
        {
            var result = true;
            var fileSaving = FileSaving;
            if (fileSaving != null)
            {
                var e = new CancelEventArgs();
                fileSaving(this, e);
                result = !e.Cancel;
            }
            return result;
        }

        #endregion

        #region Non-Public Methods

        protected abstract void ClearDocument();

		protected abstract bool LoadFromStream(Stream stream, string format);

		protected override void Reopen(ToolStripItem menuItem)
		{
			var filePath = menuItem.ToolTipText;
			if (File.Exists(filePath))
			{
				if (SaveIfModified())
					LoadFromFile(filePath);
			}
			else if (MessageBox.Show(
				string.Format("File \"{0}\" no longer exists. Remove from menu?", filePath),
				"Reopen file", MessageBoxButtons.YesNo) == DialogResult.Yes)
				RemoveItem(filePath);
		}

		protected abstract bool SaveToStream(Stream stream, string format);

		protected bool UseStream(Action action)
		{
			var result = true;
			try
			{
				action();
				Model.Modified = false;
			}
			catch (Exception x)
			{
				MessageBox.Show(
					x.Message,
					x.GetType().Name,
					MessageBoxButtons.OK,
					MessageBoxIcon.Error);
				result = false;
			}
			return result;
		}

		private bool LoadFromFile(string filePath)
		{
			var result = false;
			if (OnFileLoading())
			{
                using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
					result = LoadFromStream(stream, Path.GetExtension(filePath));
				if (result)
				{
					FilePath = filePath;
					AddItem(filePath);
                    OnFileLoaded();
				}
			}
			return result;
		}

		private bool SaveToFile(string filePath)
		{
			var result = false;
			if (OnFileSaving())
				using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
				{
					result = SaveToStream(stream, Path.GetExtension(filePath));
					if (result)
					{
						FilePath = filePath;
						AddItem(filePath);
					}
				}
			return result;
		}

        #endregion
    }
}