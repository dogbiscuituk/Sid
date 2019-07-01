namespace ToyGraf.Controls
{
    using System.Drawing;
    using System.IO;
    using System.Windows.Forms;
    using ToyGraf.Expressions;

    public static class TgControls
    {
        public static string CompactMenuText(this string text)
        {
            var result = Path.ChangeExtension(text, string.Empty).TrimEnd('.');
            /*
                https://stackoverflow.com/questions/1764204/how-to-display-abbreviated-path-names-in-net
                "Important: Passing in a formatting option of TextFormatFlags.ModifyString actually causes
                the MeasureText method to alter the string argument [...] to be a compacted string. This
                seems very weird since no explicit ref or out method parameter keyword is specified and
                strings are immutable. However, its definitely the case. I assume the string's pointer was
                updated via unsafe code to the new compacted string." -- Sam.
            */
            TextRenderer.MeasureText(
                result, SystemFonts.MenuFont, new Size(320, 0),
                TextFormatFlags.PathEllipsis | TextFormatFlags.ModifyString);
            var length = result.IndexOf((char)0);
            if (length >= 0)
                result = result.Substring(0, length);
            return result.AmpersandEscape();
        }

        public static void FirstFocus(this Control control, ref Message m)
        {
            const int WM_MOUSEACTIVATE = 0x21;
            if (m.Msg == WM_MOUSEACTIVATE && control.CanFocus && !control.Focused)
                control.Focus();
        }
    }
}
