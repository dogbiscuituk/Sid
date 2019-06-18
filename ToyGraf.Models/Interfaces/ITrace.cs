namespace ToyGraf.Models.Interfaces
{
    public interface ITrace : IStyle
    {
        string Formula { get; set; }
        bool Visible { get; set; }
    }
}
