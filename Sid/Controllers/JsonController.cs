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
            using (var streamer = new StreamReader(stream))
            using (var reader = new JsonTextReader(streamer))
                return UseStream(() => Model.Graph = GetSerializer().Deserialize<Graph>(reader));
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
