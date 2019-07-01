namespace ToyGraf.Models.Enumerations
{
    using System.ComponentModel;

    /// <summary>
    /// Values chosen to match those used in the C++ GDI+ header file, gdiplusenims.h:
    /// https://docs.microsoft.com/en-us/windows/desktop/api/gdiplusenums/ne-gdiplusenums-brushtype
    /// </summary>
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
