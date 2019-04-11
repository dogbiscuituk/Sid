namespace Sid
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MenuSinX = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuSinhXover2 = new System.Windows.Forms.ToolStripMenuItem();
            this.Menu1overX = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuLnXsquaredMinus1 = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PopupMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PopupMenu
            // 
            this.PopupMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MenuSinX,
            this.MenuSinhXover2,
            this.Menu1overX,
            this.MenuLnXsquaredMinus1});
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(135, 92);
            // 
            // MenuSinX
            // 
            this.MenuSinX.Name = "MenuSinX";
            this.MenuSinX.Size = new System.Drawing.Size(134, 22);
            this.MenuSinX.Text = "y=sin(x)";
            this.MenuSinX.Click += new System.EventHandler(this.MenuSinX_Click);
            // 
            // MenuSinhXover2
            // 
            this.MenuSinhXover2.Name = "MenuSinhXover2";
            this.MenuSinhXover2.Size = new System.Drawing.Size(134, 22);
            this.MenuSinhXover2.Text = "y=sinh(x/2)";
            this.MenuSinhXover2.Click += new System.EventHandler(this.MenuSinhXover2_Click);
            // 
            // Menu1overX
            // 
            this.Menu1overX.Name = "Menu1overX";
            this.Menu1overX.Size = new System.Drawing.Size(134, 22);
            this.Menu1overX.Text = "y=1/x";
            this.Menu1overX.Click += new System.EventHandler(this.Menu1overX_Click);
            // 
            // MenuLnXsquaredMinus1
            // 
            this.MenuLnXsquaredMinus1.Name = "MenuLnXsquaredMinus1";
            this.MenuLnXsquaredMinus1.Size = new System.Drawing.Size(134, 22);
            this.MenuLnXsquaredMinus1.Text = "y=ln(x²-1)";
            this.MenuLnXsquaredMinus1.Click += new System.EventHandler(this.MenuLnXsquaredMinus1_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(496, 361);
            this.ContextMenuStrip = this.PopupMenu;
            this.Name = "MainForm";
            this.Text = "Differentiator Demo";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.MainForm_Paint);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.MainForm_MouseMove);
            this.Resize += new System.EventHandler(this.MainForm_Resize);
            this.PopupMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip PopupMenu;
        private System.Windows.Forms.ToolStripMenuItem MenuSinX;
        private System.Windows.Forms.ToolStripMenuItem MenuSinhXover2;
        private System.Windows.Forms.ToolStripMenuItem Menu1overX;
        private System.Windows.Forms.ToolStripMenuItem MenuLnXsquaredMinus1;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}