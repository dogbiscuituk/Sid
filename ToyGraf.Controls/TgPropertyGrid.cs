namespace ToyGraf.Controls
{
    using System.Linq;
    using System.Windows.Forms;

    public class TgPropertyGrid : PropertyGrid
    {
        public ToolStrip GetToolStrip() => Controls.OfType<ToolStrip>().FirstOrDefault();
    }
}
