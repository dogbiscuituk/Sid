namespace ToyGraf.Controls
{
    using System.Windows.Forms;

    public static class TgControls
    {
        public static void FirstFocus(this Control control, ref Message m)
        {
            const int WM_MOUSEACTIVATE = 0x21;
            if (m.Msg == WM_MOUSEACTIVATE && control.CanFocus && !control.Focused)
                control.Focus();
        }
    }
}
