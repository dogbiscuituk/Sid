namespace Sid.Views
{
    partial class SeriesProperties
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
            this.cbPenColour = new System.Windows.Forms.ComboBox();
            this.cbFillColour = new System.Windows.Forms.ComboBox();
            this.cbFillOpacity = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // cbFunction
            // 
            this.cbFunction.FormattingEnabled = true;
            this.cbFunction.Location = new System.Drawing.Point(0, 0);
            this.cbFunction.Margin = new System.Windows.Forms.Padding(4);
            this.cbFunction.Name = "cbFunction";
            this.cbFunction.Size = new System.Drawing.Size(387, 21);
            this.cbFunction.TabIndex = 1;
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
            this.cbPenColour.Location = new System.Drawing.Point(394, 0);
            this.cbPenColour.Name = "cbPenColour";
            this.cbPenColour.Size = new System.Drawing.Size(40, 21);
            this.cbPenColour.TabIndex = 20;
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
            this.cbFillColour.Location = new System.Drawing.Point(440, 0);
            this.cbFillColour.Name = "cbFillColour";
            this.cbFillColour.Size = new System.Drawing.Size(40, 21);
            this.cbFillColour.TabIndex = 21;
            this.cbFillColour.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.ColourCombo_DrawItem);
            // 
            // cbFillOpacity
            // 
            this.cbFillOpacity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
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
            "100",
            "",
            "",
            "",
            "",
            "",
            "",
            "",
            ""});
            this.cbFillOpacity.Location = new System.Drawing.Point(486, 0);
            this.cbFillOpacity.Name = "cbFillOpacity";
            this.cbFillOpacity.Size = new System.Drawing.Size(40, 21);
            this.cbFillOpacity.TabIndex = 22;
            // 
            // SeriesProperties
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.cbFillOpacity);
            this.Controls.Add(this.cbFillColour);
            this.Controls.Add(this.cbPenColour);
            this.Controls.Add(this.cbFunction);
            this.Name = "SeriesProperties";
            this.Size = new System.Drawing.Size(534, 27);
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbFunction;
        private System.Windows.Forms.ComboBox cbPenColour;
        private System.Windows.Forms.ComboBox cbFillColour;
        private System.Windows.Forms.ComboBox cbFillOpacity;
    }
}
