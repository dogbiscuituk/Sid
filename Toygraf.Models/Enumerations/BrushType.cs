namespace ToyGraf.Models.Enumerations
{
    using System.ComponentModel;

    public enum BrushType
    {
        [Description("Solid")]
        Solid,
        [Description("Hatch")]
        Hatch,
        [Description("Texture")]
        Texture,
        [Description("Path Gradient")]
        PathGradient,
        [Description("Linear Gradient")]
        LinearGradient
    };
}
