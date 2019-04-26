namespace Sid.Views
{
    partial class Key
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.cbFunction = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbPenColour = new System.Windows.Forms.ComboBox();
            this.cbFillColour = new System.Windows.Forms.ComboBox();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.seTransparency = new System.Windows.Forms.NumericUpDown();
            ((System.ComponentModel.ISupportInitialize)(this.seTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // cbFunction
            // 
            this.cbFunction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFunction.FormattingEnabled = true;
            this.cbFunction.ItemHeight = 16;
            this.cbFunction.Location = new System.Drawing.Point(31, 0);
            this.cbFunction.Margin = new System.Windows.Forms.Padding(0);
            this.cbFunction.Name = "cbFunction";
            this.cbFunction.Size = new System.Drawing.Size(149, 22);
            this.cbFunction.TabIndex = 1;
            this.ToolTip.SetToolTip(this.cbFunction, "Formula for points on this trace");
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(362, -1);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(22, 21);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "X";
            this.ToolTip.SetToolTip(this.btnRemove, "Delete this trace from the graph");
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // cbPenColour
            // 
            this.cbPenColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPenColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPenColour.DropDownWidth = 144;
            this.cbPenColour.FormattingEnabled = true;
            this.cbPenColour.ItemHeight = 16;
            this.cbPenColour.Location = new System.Drawing.Point(193, 0);
            this.cbPenColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbPenColour.Name = "cbPenColour";
            this.cbPenColour.Size = new System.Drawing.Size(64, 22);
            this.cbPenColour.TabIndex = 2;
            this.ToolTip.SetToolTip(this.cbPenColour, "Pen colour used to draw this trace");
            // 
            // cbFillColour
            // 
            this.cbFillColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillColour.DropDownWidth = 144;
            this.cbFillColour.FormattingEnabled = true;
            this.cbFillColour.ItemHeight = 16;
            this.cbFillColour.Location = new System.Drawing.Point(257, 0);
            this.cbFillColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillColour.Name = "cbFillColour";
            this.cbFillColour.Size = new System.Drawing.Size(64, 22);
            this.cbFillColour.TabIndex = 3;
            this.ToolTip.SetToolTip(this.cbFillColour, "Fill colour (area under this trace)");
            // 
            // cbVisible
            // 
            this.cbVisible.Location = new System.Drawing.Point(0, 2);
            this.cbVisible.Margin = new System.Windows.Forms.Padding(0);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(64, 17);
            this.cbVisible.TabIndex = 0;
            this.cbVisible.Text = "f";
            this.ToolTip.SetToolTip(this.cbVisible, "Show or hide this trace");
            this.cbVisible.UseVisualStyleBackColor = true;
            // 
            // seTransparency
            // 
            this.seTransparency.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seTransparency.Location = new System.Drawing.Point(324, 0);
            this.seTransparency.Margin = new System.Windows.Forms.Padding(0);
            this.seTransparency.Name = "seTransparency";
            this.seTransparency.Size = new System.Drawing.Size(38, 20);
            this.seTransparency.TabIndex = 6;
            this.ToolTip.SetToolTip(this.seTransparency, "Fill transparency (%)");
            // 
            // LegendLine
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.seTransparency);
            this.Controls.Add(this.cbFunction);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.cbPenColour);
            this.Controls.Add(this.cbFillColour);
            this.Controls.Add(this.cbVisible);
            this.Name = "LegendLine";
            this.Size = new System.Drawing.Size(400, 22);
            ((System.ComponentModel.ISupportInitialize)(this.seTransparency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbFunction;
        public System.Windows.Forms.Button btnRemove;
        public System.Windows.Forms.CheckBox cbVisible;
        private System.Windows.Forms.ToolTip ToolTip;
        public System.Windows.Forms.ComboBox cbPenColour;
        public System.Windows.Forms.ComboBox cbFillColour;
        public System.Windows.Forms.NumericUpDown seTransparency;
    }
}
