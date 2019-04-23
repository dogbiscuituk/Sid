namespace Sid.Views
{
    partial class TraceEditor
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
            this.cbFunction = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.cbPenColour = new System.Windows.Forms.ComboBox();
            this.cbFillColour = new System.Windows.Forms.ComboBox();
            this.cbFillOpacity = new System.Windows.Forms.ComboBox();
            this.cbVisible = new System.Windows.Forms.CheckBox();
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
            this.cbFunction.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.FunctionCombo_DrawItem);
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.Location = new System.Drawing.Point(357, 0);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(22, 21);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "X";
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
            this.cbPenColour.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColourCombo_DrawItem);
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
            this.cbFillColour.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColourCombo_DrawItem);
            // 
            // cbFillOpacity
            // 
            this.cbFillOpacity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillOpacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillOpacity.FormattingEnabled = true;
            this.cbFillOpacity.ItemHeight = 16;
            this.cbFillOpacity.Items.AddRange(new object[] {
            "0",
            "10",
            "20",
            "30",
            "40",
            "50",
            "60",
            "70",
            "80",
            "90",
            "100"});
            this.cbFillOpacity.Location = new System.Drawing.Point(321, 0);
            this.cbFillOpacity.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillOpacity.Name = "cbFillOpacity";
            this.cbFillOpacity.Size = new System.Drawing.Size(36, 22);
            this.cbFillOpacity.TabIndex = 4;
            // 
            // cbVisible
            // 
            this.cbVisible.Location = new System.Drawing.Point(0, 2);
            this.cbVisible.Margin = new System.Windows.Forms.Padding(0);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(64, 17);
            this.cbVisible.TabIndex = 0;
            this.cbVisible.Text = "y";
            this.cbVisible.UseVisualStyleBackColor = true;
            // 
            // TraceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFunction);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.cbPenColour);
            this.Controls.Add(this.cbFillColour);
            this.Controls.Add(this.cbFillOpacity);
            this.Controls.Add(this.cbVisible);
            this.Name = "TraceEditor";
            this.Size = new System.Drawing.Size(387, 22);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbFunction;
        public System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cbPenColour;
        private System.Windows.Forms.ComboBox cbFillColour;
        private System.Windows.Forms.ComboBox cbFillOpacity;
        public System.Windows.Forms.CheckBox cbVisible;
    }
}
