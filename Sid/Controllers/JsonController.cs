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
            using (StreamReader streamReader = new StreamReader(stream))
            using (JsonReader jsonReader = new JsonTextReader(streamReader))
                return UseStream(() => Model.Graph = (Graph)GetJsonSerializer().Deserialize(jsonReader));
        }

        protected override bool SaveToStream(Stream stream, string format)
        {
            using (StreamWriter streamWriter = new StreamWriter(stream))
            using (JsonWriter jsonWriter = new JsonTextWriter(streamWriter))
                return UseStream(() => GetJsonSerializer().Serialize(jsonWriter, Model.Graph));
        }

        private static JsonSerializer GetJsonSerializer() =>
            new JsonSerializer{ Formatting = Formatting.Indented };
    }
}
