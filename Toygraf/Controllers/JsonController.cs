namespace ToyGraf.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using ToyGraf.Models;

    /// <summary>
    /// Extend SdiController to provide concrete I/O methods using Json data format.
    /// Maintain a "WindowCaption" property for the app, including the product name,
    /// current filename (if any, otherwise "(untitled)", and "Modified" flag.
    /// </summary>
    public class JsonController : SdiController
    {
        public JsonController(Model model, Control view, ToolStripDropDownItem recentMenu)
            : base(model, Properties.Settings.Default.GraphFilter, "LibraryMRU", recentMenu)
        {
            Model.PropertyChanged += Model_PropertyChanged;
            View = view;
        }

        #region Public Properties

        public string WindowCaption
        {
            get
            {
                var text = Path.GetFileNameWithoutExtension(FilePath);
                if (string.IsNullOrWhiteSpace(text))
                    text = "(untitled)";
                var modified = Model.Modified;
                if (modified)
                    text = string.Concat("* ", text);
                text = string.Concat(text, " - ", Application.ProductName);
                return text;
            }
        }

        #endregion

        #region Protected I/O Overrides

        protected override void ClearDocument() => Model.Clear();

        protected override bool LoadFromStream(Stream stream, string format)
        {
            bool result;
            using (var streamer = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamer))
                result = UseStream(() => Model.Graph = GetSerializer().Deserialize<Graph>(reader));
            Graph.PropertyChanged += Model.Graph_PropertyChanged;
            foreach (var series in Series)
                series.PropertyChanged += Graph.Series_PropertyChanged;

            return result;
        }

        protected override bool SaveToStream(Stream stream, string format)
        {
            using (var streamer = new StreamWriter(stream))
            using (var writer = new JsonTextWriter(streamer))
                return UseStream(() => GetSerializer().Serialize(writer, Model.Graph));
        }

        #endregion

        #region Private Implementation

        private Graph Graph { get => Model.Graph; }
        private List<Series> Series { get => Graph.Series; }
        private readonly Control View;

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e) { }

        private static JsonSerializer GetSerializer() => new JsonSerializer{
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Ignore };

        #endregion
    }
}
