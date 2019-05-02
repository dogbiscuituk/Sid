namespace Sid.Views
{
    using System;

    [Flags]
    public enum KeyboardModes
    {
        Normal = 0x00,
        Shift = 0x01,
        Greek = 0x02,
        Subscript = 0x04,
        Superscript = 0x08
    }
}
