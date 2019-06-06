namespace ToyGraf.Models.Structs
{
    public struct Options
    {
        public bool GroupUndo { get; set; }
        public bool OpenInNewWindow { get; set; }

        public string FilesFolderPath { get; set; }
        public string TemplatesFolderPath { get; set; }
    }
}
