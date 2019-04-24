namespace Sid.Controllers
{
    using System.Collections.Generic;
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using Sid.Models;

    public class JsonController : SdiController
    {
        public JsonController(Model model, Control view, ToolStripDropDownItem recentMenu)
            : base(model, Properties.Settings.Default.GraphFilter, "LibraryMRU", recentMenu)
        {
            Model.PropertyChanged += Model_PropertyChanged;
            View = view;
        }

        private Graph Graph { get => Model.Graph; }
        private List<Series> Series { get => Graph.Series; }
        private readonly Control View;

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

        private void Model_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
        }

        protected override bool SaveToStream(Stream stream, string format)
        {
            using (var streamer = new StreamWriter(stream))
            using (var writer = new JsonTextWriter(streamer))
                return UseStream(() => GetSerializer().Serialize(writer, Model.Graph));
        }

        private static JsonSerializer GetSerializer() => new JsonSerializer{
            Formatting = Formatting.Indented,
            DefaultValueHandling = DefaultValueHandling.Ignore };
    }
}
