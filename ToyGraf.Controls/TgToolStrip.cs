namespace ToyGraf.Controls
{
    using System.Windows.Forms;

    public class TgToolStrip : ToolStrip
    {
        protected override void WndProc(ref Message m)
        {
            this.FirstFocus(ref m);
            base.WndProc(ref m);
        }
    }
}
