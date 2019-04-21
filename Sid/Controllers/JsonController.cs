namespace Sid.Controllers
{
    using System.IO;
    using System.Windows.Forms;
    using Newtonsoft.Json;
    using Sid.Models;

    public class JsonController : SdiController
    {
        public JsonController(Model model, Control view, ToolStripDropDownItem recentMenu)
            : base(model, Properties.Settings.Default.GraphFilter, "LibraryMRU", recentMenu)
        {
            View = view;
        }

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
            foreach (var series in Model.Graph.Series)
                series.PropertyChanged += Model.Graph.Series_PropertyChanged;
            return result;
        }

        private void Series_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            throw new System.NotImplementedException();
        }

        protected override bool SaveToStream(Stream stream, string format)
        {
            using (var streamer = new StreamWriter(stream))
            using (var writer = new JsonTextWriter(streamer))
                return UseStream(() => GetSerializer().Serialize(writer, Model.Graph));
        }

        private static JsonSerializer GetSerializer() =>
            new JsonSerializer{ Formatting = Formatting.Indented };
    }
}
