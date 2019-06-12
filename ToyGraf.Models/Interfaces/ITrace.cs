namespace ToyGraf.Models.Interfaces
{
    public interface ITrace : IStyle
    {
        string Formula { get; set; }
        int StepCount { get; set; }
        bool Visible { get; set; }
    }
}
