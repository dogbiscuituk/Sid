namespace ToyGraf.Models.Interfaces
{
    using ToyGraf.Models.Enumerations;

    public interface ITrace : IStyle
    {
        string Formula { get; set; }
        Interpolation Interpolation { get; set; }
        bool Visible { get; set; }
    }
}
