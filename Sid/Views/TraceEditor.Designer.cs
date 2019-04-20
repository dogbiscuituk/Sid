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
            this.tbLabel = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // cbFunction
            // 
            this.cbFunction.FormattingEnabled = true;
            this.cbFunction.Location = new System.Drawing.Point(32, 0);
            this.cbFunction.Margin = new System.Windows.Forms.Padding(0);
            this.cbFunction.Name = "cbFunction";
            this.cbFunction.Size = new System.Drawing.Size(149, 21);
            this.cbFunction.TabIndex = 1;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(289, -1);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(22, 23);
            this.btnRemove.TabIndex = 5;
            this.btnRemove.Text = "X";
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // cbPenColour
            // 
            this.cbPenColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPenColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPenColour.FormattingEnabled = true;
            this.cbPenColour.Items.AddRange(new object[] {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""});
            this.cbPenColour.Location = new System.Drawing.Point(181, 0);
            this.cbPenColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbPenColour.Name = "cbPenColour";
            this.cbPenColour.Size = new System.Drawing.Size(36, 21);
            this.cbPenColour.TabIndex = 2;
            this.cbPenColour.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColourCombo_DrawItem);
            // 
            // cbFillColour
            // 
            this.cbFillColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillColour.FormattingEnabled = true;
            this.cbFillColour.Items.AddRange(new object[] {
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""});
            this.cbFillColour.Location = new System.Drawing.Point(217, 0);
            this.cbFillColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillColour.Name = "cbFillColour";
            this.cbFillColour.Size = new System.Drawing.Size(36, 21);
            this.cbFillColour.TabIndex = 3;
            this.cbFillColour.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColourCombo_DrawItem);
            // 
            // cbFillOpacity
            // 
            this.cbFillOpacity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillOpacity.FormattingEnabled = true;
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
            this.cbFillOpacity.Location = new System.Drawing.Point(253, 0);
            this.cbFillOpacity.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillOpacity.Name = "cbFillOpacity";
            this.cbFillOpacity.Size = new System.Drawing.Size(36, 21);
            this.cbFillOpacity.TabIndex = 4;
            // 
            // tbLabel
            // 
            this.tbLabel.Location = new System.Drawing.Point(0, 0);
            this.tbLabel.Margin = new System.Windows.Forms.Padding(0);
            this.tbLabel.Name = "tbLabel";
            this.tbLabel.Size = new System.Drawing.Size(32, 20);
            this.tbLabel.TabIndex = 0;
            this.tbLabel.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // TraceEditor
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tbLabel);
            this.Controls.Add(this.cbFunction);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.cbPenColour);
            this.Controls.Add(this.cbFillColour);
            this.Controls.Add(this.cbFillOpacity);
            this.Name = "TraceEditor";
            this.Size = new System.Drawing.Size(336, 21);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public System.Windows.Forms.ComboBox cbFunction;
        public System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.ComboBox cbPenColour;
        private System.Windows.Forms.ComboBox cbFillColour;
        private System.Windows.Forms.ComboBox cbFillOpacity;
        public System.Windows.Forms.TextBox tbLabel;
    }
}
