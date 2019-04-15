namespace Sid
{
    partial class ParametersDialog
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
            this.gbDomain = new System.Windows.Forms.GroupBox();
            this.seXmax = new System.Windows.Forms.NumericUpDown();
            this.lblXmax = new System.Windows.Forms.Label();
            this.seXmin = new System.Windows.Forms.NumericUpDown();
            this.lblXmin = new System.Windows.Forms.Label();
            this.gbRange = new System.Windows.Forms.GroupBox();
            this.seYmax = new System.Windows.Forms.NumericUpDown();
            this.lblYmax = new System.Windows.Forms.Label();
            this.seYmin = new System.Windows.Forms.NumericUpDown();
            this.lblYmin = new System.Windows.Forms.Label();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.edFunction = new System.Windows.Forms.TextBox();
            this.lblFunction = new System.Windows.Forms.Label();
            this.ApplyButton = new System.Windows.Forms.Button();
            this.gbDomain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).BeginInit();
            this.gbRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).BeginInit();
            this.gbFunction.SuspendLayout();
            this.SuspendLayout();
            // 
            // gbDomain
            // 
            this.gbDomain.Controls.Add(this.seXmax);
            this.gbDomain.Controls.Add(this.lblXmax);
            this.gbDomain.Controls.Add(this.seXmin);
            this.gbDomain.Controls.Add(this.lblXmin);
            this.gbDomain.Location = new System.Drawing.Point(12, 12);
            this.gbDomain.Name = "gbDomain";
            this.gbDomain.Size = new System.Drawing.Size(350, 44);
            this.gbDomain.TabIndex = 0;
            this.gbDomain.TabStop = false;
            this.gbDomain.Text = "Domain";
            // 
            // seXmax
            // 
            this.seXmax.Location = new System.Drawing.Point(219, 14);
            this.seXmax.Maximum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            0});
            this.seXmax.Minimum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            -2147483648});
            this.seXmax.Name = "seXmax";
            this.seXmax.Size = new System.Drawing.Size(120, 20);
            this.seXmax.TabIndex = 3;
            // 
            // lblXmax
            // 
            this.lblXmax.AutoSize = true;
            this.lblXmax.Location = new System.Drawing.Point(174, 16);
            this.lblXmax.Name = "lblXmax";
            this.lblXmax.Size = new System.Drawing.Size(39, 13);
            this.lblXmax.TabIndex = 2;
            this.lblXmax.Text = "X max:";
            // 
            // seXmin
            // 
            this.seXmin.Location = new System.Drawing.Point(48, 14);
            this.seXmin.Maximum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            0});
            this.seXmin.Minimum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            -2147483648});
            this.seXmin.Name = "seXmin";
            this.seXmin.Size = new System.Drawing.Size(120, 20);
            this.seXmin.TabIndex = 1;
            // 
            // lblXmin
            // 
            this.lblXmin.AutoSize = true;
            this.lblXmin.Location = new System.Drawing.Point(6, 16);
            this.lblXmin.Name = "lblXmin";
            this.lblXmin.Size = new System.Drawing.Size(36, 13);
            this.lblXmin.TabIndex = 1;
            this.lblXmin.Text = "X min:";
            // 
            // gbRange
            // 
            this.gbRange.Controls.Add(this.seYmax);
            this.gbRange.Controls.Add(this.lblYmax);
            this.gbRange.Controls.Add(this.seYmin);
            this.gbRange.Controls.Add(this.lblYmin);
            this.gbRange.Location = new System.Drawing.Point(12, 62);
            this.gbRange.Name = "gbRange";
            this.gbRange.Size = new System.Drawing.Size(350, 44);
            this.gbRange.TabIndex = 1;
            this.gbRange.TabStop = false;
            this.gbRange.Text = "Range";
            // 
            // seYmax
            // 
            this.seYmax.Location = new System.Drawing.Point(219, 14);
            this.seYmax.Maximum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            0});
            this.seYmax.Minimum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            -2147483648});
            this.seYmax.Name = "seYmax";
            this.seYmax.Size = new System.Drawing.Size(120, 20);
            this.seYmax.TabIndex = 3;
            // 
            // lblYmax
            // 
            this.lblYmax.AutoSize = true;
            this.lblYmax.Location = new System.Drawing.Point(174, 16);
            this.lblYmax.Name = "lblYmax";
            this.lblYmax.Size = new System.Drawing.Size(39, 13);
            this.lblYmax.TabIndex = 2;
            this.lblYmax.Text = "Y max:";
            // 
            // seYmin
            // 
            this.seYmin.Location = new System.Drawing.Point(48, 14);
            this.seYmin.Maximum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            0});
            this.seYmin.Minimum = new decimal(new int[] {
            268435456,
            1042612833,
            542101086,
            -2147483648});
            this.seYmin.Name = "seYmin";
            this.seYmin.Size = new System.Drawing.Size(120, 20);
            this.seYmin.TabIndex = 1;
            // 
            // lblYmin
            // 
            this.lblYmin.AutoSize = true;
            this.lblYmin.Location = new System.Drawing.Point(6, 16);
            this.lblYmin.Name = "lblYmin";
            this.lblYmin.Size = new System.Drawing.Size(36, 13);
            this.lblYmin.TabIndex = 1;
            this.lblYmin.Text = "Y min:";
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.edFunction);
            this.gbFunction.Controls.Add(this.lblFunction);
            this.gbFunction.Location = new System.Drawing.Point(12, 112);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Size = new System.Drawing.Size(350, 44);
            this.gbFunction.TabIndex = 2;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Function";
            // 
            // edFunction
            // 
            this.edFunction.Location = new System.Drawing.Point(48, 13);
            this.edFunction.Name = "edFunction";
            this.edFunction.Size = new System.Drawing.Size(291, 20);
            this.edFunction.TabIndex = 3;
            // 
            // lblFunction
            // 
            this.lblFunction.AutoSize = true;
            this.lblFunction.Location = new System.Drawing.Point(6, 16);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new System.Drawing.Size(23, 13);
            this.lblFunction.TabIndex = 1;
            this.lblFunction.Text = "Y =";
            // 
            // ApplyButton
            // 
            this.ApplyButton.Location = new System.Drawing.Point(276, 162);
            this.ApplyButton.Name = "ApplyButton";
            this.ApplyButton.Size = new System.Drawing.Size(75, 23);
            this.ApplyButton.TabIndex = 3;
            this.ApplyButton.Text = "Apply";
            this.ApplyButton.UseVisualStyleBackColor = true;
            // 
            // ParametersDialog
            // 
            this.AcceptButton = this.ApplyButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(375, 206);
            this.Controls.Add(this.ApplyButton);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.gbRange);
            this.Controls.Add(this.gbDomain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParametersDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Parameters";
            this.TopMost = true;
            this.gbDomain.ResumeLayout(false);
            this.gbDomain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).EndInit();
            this.gbRange.ResumeLayout(false);
            this.gbRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).EndInit();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox gbDomain;
        private System.Windows.Forms.Label lblXmax;
        private System.Windows.Forms.Label lblXmin;
        private System.Windows.Forms.GroupBox gbRange;
        private System.Windows.Forms.Label lblYmax;
        private System.Windows.Forms.Label lblYmin;
        private System.Windows.Forms.GroupBox gbFunction;
        private System.Windows.Forms.Label lblFunction;
        public System.Windows.Forms.NumericUpDown seXmax;
        public System.Windows.Forms.NumericUpDown seXmin;
        public System.Windows.Forms.NumericUpDown seYmax;
        public System.Windows.Forms.NumericUpDown seYmin;
        public System.Windows.Forms.TextBox edFunction;
        public System.Windows.Forms.Button ApplyButton;
    }
}