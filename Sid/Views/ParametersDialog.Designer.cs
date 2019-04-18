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
            this.lblXmax = new System.Windows.Forms.Label();
            this.seXmin = new System.Windows.Forms.NumericUpDown();
            this.lblXmin = new System.Windows.Forms.Label();
            this.gbRange = new System.Windows.Forms.GroupBox();
            this.lblYmax = new System.Windows.Forms.Label();
            this.lblYmin = new System.Windows.Forms.Label();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.cbFunction = new System.Windows.Forms.ComboBox();
            this.lblFunction = new System.Windows.Forms.Label();
            this.btnApply = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.seXmax = new System.Windows.Forms.NumericUpDown();
            this.seYmin = new System.Windows.Forms.NumericUpDown();
            this.seYmax = new System.Windows.Forms.NumericUpDown();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.gbDomain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).BeginInit();
            this.gbRange.SuspendLayout();
            this.gbFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).BeginInit();
            this.SuspendLayout();
            // 
            // gbDomain
            // 
            this.gbDomain.Controls.Add(this.seXmax);
            this.gbDomain.Controls.Add(this.lblXmax);
            this.gbDomain.Controls.Add(this.seXmin);
            this.gbDomain.Controls.Add(this.lblXmin);
            this.gbDomain.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbDomain.Location = new System.Drawing.Point(16, 15);
            this.gbDomain.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDomain.Name = "gbDomain";
            this.gbDomain.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbDomain.Size = new System.Drawing.Size(360, 54);
            this.gbDomain.TabIndex = 0;
            this.gbDomain.TabStop = false;
            this.gbDomain.Text = "Domain";
            // 
            // lblXmax
            // 
            this.lblXmax.AutoSize = true;
            this.lblXmax.Location = new System.Drawing.Point(179, 20);
            this.lblXmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXmax.Name = "lblXmax";
            this.lblXmax.Size = new System.Drawing.Size(47, 16);
            this.lblXmax.TabIndex = 2;
            this.lblXmax.Text = "X max:";
            // 
            // seXmin
            // 
            this.seXmin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seXmin.DecimalPlaces = 4;
            this.seXmin.Location = new System.Drawing.Point(64, 21);
            this.seXmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.seXmin.Size = new System.Drawing.Size(107, 18);
            this.seXmin.TabIndex = 0;
            this.seXmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seXmin.Value = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            // 
            // lblXmin
            // 
            this.lblXmin.AutoSize = true;
            this.lblXmin.Location = new System.Drawing.Point(8, 20);
            this.lblXmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXmin.Name = "lblXmin";
            this.lblXmin.Size = new System.Drawing.Size(43, 16);
            this.lblXmin.TabIndex = 1;
            this.lblXmin.Text = "X min:";
            // 
            // gbRange
            // 
            this.gbRange.Controls.Add(this.seYmax);
            this.gbRange.Controls.Add(this.seYmin);
            this.gbRange.Controls.Add(this.lblYmax);
            this.gbRange.Controls.Add(this.lblYmin);
            this.gbRange.Location = new System.Drawing.Point(384, 15);
            this.gbRange.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbRange.Name = "gbRange";
            this.gbRange.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbRange.Size = new System.Drawing.Size(360, 54);
            this.gbRange.TabIndex = 1;
            this.gbRange.TabStop = false;
            this.gbRange.Text = "Range";
            // 
            // lblYmax
            // 
            this.lblYmax.AutoSize = true;
            this.lblYmax.Location = new System.Drawing.Point(179, 20);
            this.lblYmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYmax.Name = "lblYmax";
            this.lblYmax.Size = new System.Drawing.Size(48, 16);
            this.lblYmax.TabIndex = 2;
            this.lblYmax.Text = "Y max:";
            // 
            // lblYmin
            // 
            this.lblYmin.AutoSize = true;
            this.lblYmin.Location = new System.Drawing.Point(8, 20);
            this.lblYmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYmin.Name = "lblYmin";
            this.lblYmin.Size = new System.Drawing.Size(44, 16);
            this.lblYmin.TabIndex = 1;
            this.lblYmin.Text = "Y min:";
            // 
            // gbFunction
            // 
            this.gbFunction.Controls.Add(this.comboBox2);
            this.gbFunction.Controls.Add(this.comboBox1);
            this.gbFunction.Controls.Add(this.cbFunction);
            this.gbFunction.Controls.Add(this.lblFunction);
            this.gbFunction.Location = new System.Drawing.Point(16, 77);
            this.gbFunction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Padding = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.gbFunction.Size = new System.Drawing.Size(728, 54);
            this.gbFunction.TabIndex = 2;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Function";
            // 
            // cbFunction
            // 
            this.cbFunction.FormattingEnabled = true;
            this.cbFunction.Items.AddRange(new object[] {
            "sin x",
            "sinh (x/2)",
            "1/x",
            "ln(x^2-1)",
            "exp -(x^2)"});
            this.cbFunction.Location = new System.Drawing.Point(64, 17);
            this.cbFunction.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.cbFunction.Name = "cbFunction";
            this.cbFunction.Size = new System.Drawing.Size(387, 24);
            this.cbFunction.TabIndex = 0;
            // 
            // lblFunction
            // 
            this.lblFunction.AutoSize = true;
            this.lblFunction.Location = new System.Drawing.Point(8, 20);
            this.lblFunction.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblFunction.Name = "lblFunction";
            this.lblFunction.Size = new System.Drawing.Size(27, 16);
            this.lblFunction.TabIndex = 1;
            this.lblFunction.Text = "Y =";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(431, 407);
            this.btnApply.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(100, 28);
            this.btnApply.TabIndex = 3;
            this.btnApply.Text = "Apply";
            this.btnApply.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(539, 407);
            this.btnOK.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 4;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(647, 407);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 5;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // seXmax
            // 
            this.seXmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seXmax.DecimalPlaces = 4;
            this.seXmax.Location = new System.Drawing.Point(239, 21);
            this.seXmax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.seXmax.Size = new System.Drawing.Size(107, 18);
            this.seXmax.TabIndex = 3;
            this.seXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seXmax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // seYmin
            // 
            this.seYmin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seYmin.DecimalPlaces = 4;
            this.seYmin.Location = new System.Drawing.Point(64, 21);
            this.seYmin.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.seYmin.Size = new System.Drawing.Size(107, 18);
            this.seYmin.TabIndex = 3;
            this.seYmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seYmin.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            // 
            // seYmax
            // 
            this.seYmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seYmax.DecimalPlaces = 4;
            this.seYmax.Location = new System.Drawing.Point(239, 21);
            this.seYmax.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
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
            this.seYmax.Size = new System.Drawing.Size(107, 18);
            this.seYmax.TabIndex = 4;
            this.seYmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seYmax.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(16, 407);
            this.button1.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(100, 28);
            this.button1.TabIndex = 6;
            this.button1.Text = "Test";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
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
            this.comboBox1.Location = new System.Drawing.Point(458, 17);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(40, 23);
            this.comboBox1.TabIndex = 19;
            this.comboBox1.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // comboBox2
            // 
            this.comboBox2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBox2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Items.AddRange(new object[] {
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
            this.comboBox2.Location = new System.Drawing.Point(504, 17);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(40, 23);
            this.comboBox2.TabIndex = 20;
            this.comboBox2.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.comboBox1_DrawItem);
            // 
            // ParametersDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(759, 476);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.gbRange);
            this.Controls.Add(this.gbDomain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Margin = new System.Windows.Forms.Padding(4, 4, 4, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ParametersDialog";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Parameters";
            this.gbDomain.ResumeLayout(false);
            this.gbDomain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).EndInit();
            this.gbRange.ResumeLayout(false);
            this.gbRange.PerformLayout();
            this.gbFunction.ResumeLayout(false);
            this.gbFunction.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).EndInit();
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
        public System.Windows.Forms.NumericUpDown seXmin;
        public System.Windows.Forms.Button btnApply;
        public System.Windows.Forms.Button btnOK;
        public System.Windows.Forms.Button btnCancel;
        public System.Windows.Forms.ComboBox cbFunction;
        public System.Windows.Forms.NumericUpDown seXmax;
        public System.Windows.Forms.NumericUpDown seYmax;
        public System.Windows.Forms.NumericUpDown seYmin;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.ComboBox comboBox2;
    }
}