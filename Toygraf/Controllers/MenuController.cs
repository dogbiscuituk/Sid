namespace ToyGraf.Controllers
{
    using System;
    using System.Windows.Forms;

    internal static class MenuController
    {
        #region Internal Interface

        /// <summary>
        /// Copy the Items from one ToolStrip to another.
        /// ToolStrip may be either a MenuStrip or a ContextMenuStrip.
        /// </summary>
        /// <param name="s">The source menu, contributing the Items to be copied.</param>
        /// <param name="t">The target menu, receiving the copies.</param>
        internal static void CloneTo(this ToolStrip s, ToolStrip t) => s.Items.CloneTo(t.Items);

        /// <summary>
        /// Copy the DropDownItems from one ToolStripDropDownItem to another.
        /// ToolStripDropDownItem may be a ToolStripMenuItem in a main or context menu,
        /// or a ToolStripSplitButton in a Toolbar.
        /// </summary>
        /// <param name="s">The source parent item, contributing the DropDownItems to be copied.</param>
        /// <param name="t">The target parent item, receiving the copies.</param>
        internal static void CloneTo(this ToolStripDropDownItem s, ToolStripDropDownItem t) =>
            s.DropDownItems.CloneTo(t.DropDownItems);

        #endregion

        #region Private Methods

        private static ToolStripItem Clone(this ToolStripItem s)
        {
            switch (s)
            {
                case ToolStripSeparator p:
                    return new ToolStripSeparator();
                case ToolStripMenuItem m:
                    var t = new ToolStripMenuItem(m.Text, m.Image,
                        (object sender, EventArgs e) => m.PerformClick(),
                        m.ShortcutKeys)
                    {
                        Checked = m.Checked,
                        Enabled = m.Enabled,
                        Font = m.Font,
                        ShortcutKeyDisplayString = m.ShortcutKeyDisplayString,
                        Tag = m.Tag,
                        ToolTipText = m.ToolTipText
                    };
                    if (m.HasDropDownItems)
                        m.DropDownItems.CloneTo(t.DropDownItems);
                    return t;
            }
            return null;
        }

        private static void CloneTo(this ToolStripItemCollection s, ToolStripItemCollection t)
        {
            t.Clear();
            foreach (ToolStripItem i in s)
                t.Add(i.Clone());
        }

        #endregion
    }
}
