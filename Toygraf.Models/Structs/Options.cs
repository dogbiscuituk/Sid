namespace ToyGraf.Models.Structs
{
    public struct Options
    {
        public string FilesFolderPath { get; set; }
        public bool GroupUndo { get; set; }
        public bool OpenInNewWindow { get; set; }
        public string TemplatesFolderPath { get; set; }
        public bool UseMaxima { get; set; }
    }
}
