using System;
using System.Linq;
using System.Windows.Forms;

namespace ToyGraf.Controls
{
    public class TgPropertyGrid : PropertyGrid
    {
        public ToolStrip GetToolStrip() => Controls.OfType<ToolStrip>().FirstOrDefault();
    }
}
