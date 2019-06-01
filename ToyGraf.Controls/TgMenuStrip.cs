namespace ToyGraf.Controls
{
    using System.Windows.Forms;

    public class TgMenuStrip : MenuStrip
    {
        protected override void WndProc(ref Message m)
        {
            this.FirstFocus(ref m);
            base.WndProc(ref m);
        }
    }
}
