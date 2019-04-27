namespace Sid.Views
{
    partial class KeyView
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
            this.cbFunction.BackColor = System.Drawing.SystemColors.Control;
            this.cbFunction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFunction.FormattingEnabled = true;
            this.cbFunction.ItemHeight = 16;
            this.cbFunction.Location = new System.Drawing.Point(34, 0);
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
            this.btnRemove.Location = new System.Drawing.Point(368, 0);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(18, 18);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "X";
            this.ToolTip.SetToolTip(this.btnRemove, "Delete this trace from the graph");
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // cbPenColour
            // 
            this.cbPenColour.BackColor = System.Drawing.SystemColors.Control;
            this.cbPenColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPenColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPenColour.DropDownWidth = 144;
            this.cbPenColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPenColour.FormattingEnabled = true;
            this.cbPenColour.ItemHeight = 16;
            this.cbPenColour.Location = new System.Drawing.Point(199, 0);
            this.cbPenColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbPenColour.Name = "cbPenColour";
            this.cbPenColour.Size = new System.Drawing.Size(64, 22);
            this.cbPenColour.TabIndex = 2;
            this.ToolTip.SetToolTip(this.cbPenColour, "Pen colour used to draw this trace");
            // 
            // cbFillColour
            // 
            this.cbFillColour.BackColor = System.Drawing.SystemColors.Control;
            this.cbFillColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillColour.DropDownWidth = 144;
            this.cbFillColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFillColour.FormattingEnabled = true;
            this.cbFillColour.ItemHeight = 16;
            this.cbFillColour.Location = new System.Drawing.Point(262, 0);
            this.cbFillColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillColour.Name = "cbFillColour";
            this.cbFillColour.Size = new System.Drawing.Size(64, 22);
            this.cbFillColour.TabIndex = 3;
            this.ToolTip.SetToolTip(this.cbFillColour, "Fill colour (area under this trace)");
            // 
            // cbVisible
            // 
            this.cbVisible.BackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.BorderSize = 0;
            this.cbVisible.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVisible.Location = new System.Drawing.Point(4, 1);
            this.cbVisible.Margin = new System.Windows.Forms.Padding(0);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(64, 17);
            this.cbVisible.TabIndex = 0;
            this.cbVisible.Text = "f";
            this.ToolTip.SetToolTip(this.cbVisible, "Show or hide this trace");
            this.cbVisible.UseVisualStyleBackColor = false;
            // 
            // seTransparency
            // 
            this.seTransparency.BackColor = System.Drawing.SystemColors.Control;
            this.seTransparency.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seTransparency.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seTransparency.Location = new System.Drawing.Point(326, 3);
            this.seTransparency.Margin = new System.Windows.Forms.Padding(0);
            this.seTransparency.Name = "seTransparency";
            this.seTransparency.ReadOnly = true;
            this.seTransparency.Size = new System.Drawing.Size(38, 16);
            this.seTransparency.TabIndex = 6;
            this.seTransparency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.seTransparency, "Fill transparency (%)");
            // 
            // KeyView
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
            this.Name = "KeyView";
            this.Padding = new System.Windows.Forms.Padding(1);
            this.Size = new System.Drawing.Size(386, 22);
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
