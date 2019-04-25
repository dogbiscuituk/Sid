namespace Sid
{
    partial class GraphEdit
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
            this.gbDomain = new System.Windows.Forms.GroupBox();
            this.seXmax = new System.Windows.Forms.NumericUpDown();
            this.lblXmax = new System.Windows.Forms.Label();
            this.seXmin = new System.Windows.Forms.NumericUpDown();
            this.lblXmin = new System.Windows.Forms.Label();
            this.gbRange = new System.Windows.Forms.GroupBox();
            this.seYmax = new System.Windows.Forms.NumericUpDown();
            this.seYmin = new System.Windows.Forms.NumericUpDown();
            this.lblYmax = new System.Windows.Forms.Label();
            this.lblYmin = new System.Windows.Forms.Label();
            this.gbFunction = new System.Windows.Forms.GroupBox();
            this.FlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddNewFunction = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.gbDomain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).BeginInit();
            this.gbRange.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).BeginInit();
            this.gbFunction.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
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
            this.gbDomain.Margin = new System.Windows.Forms.Padding(4);
            this.gbDomain.Name = "gbDomain";
            this.gbDomain.Padding = new System.Windows.Forms.Padding(4);
            this.gbDomain.Size = new System.Drawing.Size(294, 54);
            this.gbDomain.TabIndex = 0;
            this.gbDomain.TabStop = false;
            this.gbDomain.Text = "Domain";
            // 
            // seXmax
            // 
            this.seXmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seXmax.DecimalPlaces = 4;
            this.seXmax.Location = new System.Drawing.Point(201, 21);
            this.seXmax.Margin = new System.Windows.Forms.Padding(4);
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
            this.seXmax.Size = new System.Drawing.Size(85, 18);
            this.seXmax.TabIndex = 3;
            this.seXmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seXmax.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // lblXmax
            // 
            this.lblXmax.AutoSize = true;
            this.lblXmax.Location = new System.Drawing.Point(149, 20);
            this.lblXmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXmax.Name = "lblXmax";
            this.lblXmax.Size = new System.Drawing.Size(44, 16);
            this.lblXmax.TabIndex = 2;
            this.lblXmax.Text = "X max";
            // 
            // seXmin
            // 
            this.seXmin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seXmin.DecimalPlaces = 4;
            this.seXmin.Location = new System.Drawing.Point(56, 21);
            this.seXmin.Margin = new System.Windows.Forms.Padding(4);
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
            this.seXmin.Size = new System.Drawing.Size(85, 18);
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
            this.lblXmin.Size = new System.Drawing.Size(40, 16);
            this.lblXmin.TabIndex = 1;
            this.lblXmin.Text = "X min";
            // 
            // gbRange
            // 
            this.gbRange.Controls.Add(this.seYmax);
            this.gbRange.Controls.Add(this.seYmin);
            this.gbRange.Controls.Add(this.lblYmax);
            this.gbRange.Controls.Add(this.lblYmin);
            this.gbRange.Location = new System.Drawing.Point(318, 15);
            this.gbRange.Margin = new System.Windows.Forms.Padding(4);
            this.gbRange.Name = "gbRange";
            this.gbRange.Padding = new System.Windows.Forms.Padding(4);
            this.gbRange.Size = new System.Drawing.Size(294, 54);
            this.gbRange.TabIndex = 1;
            this.gbRange.TabStop = false;
            this.gbRange.Text = "Range";
            // 
            // seYmax
            // 
            this.seYmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seYmax.DecimalPlaces = 4;
            this.seYmax.Location = new System.Drawing.Point(203, 21);
            this.seYmax.Margin = new System.Windows.Forms.Padding(4);
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
            this.seYmax.Size = new System.Drawing.Size(85, 18);
            this.seYmax.TabIndex = 4;
            this.seYmax.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seYmax.Value = new decimal(new int[] {
            5,
            0,
            0,
            0});
            // 
            // seYmin
            // 
            this.seYmin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seYmin.DecimalPlaces = 4;
            this.seYmin.Location = new System.Drawing.Point(57, 21);
            this.seYmin.Margin = new System.Windows.Forms.Padding(4);
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
            this.seYmin.Size = new System.Drawing.Size(85, 18);
            this.seYmin.TabIndex = 3;
            this.seYmin.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seYmin.Value = new decimal(new int[] {
            5,
            0,
            0,
            -2147483648});
            // 
            // lblYmax
            // 
            this.lblYmax.AutoSize = true;
            this.lblYmax.Location = new System.Drawing.Point(150, 20);
            this.lblYmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYmax.Name = "lblYmax";
            this.lblYmax.Size = new System.Drawing.Size(45, 16);
            this.lblYmax.TabIndex = 2;
            this.lblYmax.Text = "Y max";
            // 
            // lblYmin
            // 
            this.lblYmin.AutoSize = true;
            this.lblYmin.Location = new System.Drawing.Point(8, 20);
            this.lblYmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYmin.Name = "lblYmin";
            this.lblYmin.Size = new System.Drawing.Size(41, 16);
            this.lblYmin.TabIndex = 1;
            this.lblYmin.Text = "Y min";
            // 
            // gbFunction
            // 
            this.gbFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gbFunction.Controls.Add(this.FlowLayoutPanel);
            this.gbFunction.Location = new System.Drawing.Point(16, 77);
            this.gbFunction.Margin = new System.Windows.Forms.Padding(4);
            this.gbFunction.Name = "gbFunction";
            this.gbFunction.Padding = new System.Windows.Forms.Padding(4);
            this.gbFunction.Size = new System.Drawing.Size(596, 315);
            this.gbFunction.TabIndex = 2;
            this.gbFunction.TabStop = false;
            this.gbFunction.Text = "Functions";
            // 
            // FlowLayoutPanel
            // 
            this.FlowLayoutPanel.AutoScroll = true;
            this.FlowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.FlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel.Location = new System.Drawing.Point(4, 19);
            this.FlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FlowLayoutPanel.Name = "FlowLayoutPanel";
            this.FlowLayoutPanel.Size = new System.Drawing.Size(588, 292);
            this.FlowLayoutPanel.TabIndex = 0;
            // 
            // btnAddNewFunction
            // 
            this.btnAddNewFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNewFunction.Location = new System.Drawing.Point(20, 395);
            this.btnAddNewFunction.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewFunction.Name = "btnAddNewFunction";
            this.btnAddNewFunction.Size = new System.Drawing.Size(137, 28);
            this.btnAddNewFunction.TabIndex = 6;
            this.btnAddNewFunction.Text = "&Add a new function";
            this.btnAddNewFunction.UseVisualStyleBackColor = true;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
            this.ErrorProvider.ContainerControl = this;
            // 
            // GraphEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoValidate = System.Windows.Forms.AutoValidate.EnableAllowFocusChange;
            this.ClientSize = new System.Drawing.Size(620, 437);
            this.ControlBox = false;
            this.Controls.Add(this.btnAddNewFunction);
            this.Controls.Add(this.gbFunction);
            this.Controls.Add(this.gbRange);
            this.Controls.Add(this.gbDomain);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Location = new System.Drawing.Point(64, 64);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "GraphEdit";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Graph Properties";
            this.gbDomain.ResumeLayout(false);
            this.gbDomain.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).EndInit();
            this.gbRange.ResumeLayout(false);
            this.gbRange.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).EndInit();
            this.gbFunction.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
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
        public System.Windows.Forms.NumericUpDown seXmin;
        public System.Windows.Forms.NumericUpDown seXmax;
        public System.Windows.Forms.NumericUpDown seYmax;
        public System.Windows.Forms.NumericUpDown seYmin;
        public System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel;
        public System.Windows.Forms.Button btnAddNewFunction;
        public System.Windows.Forms.ErrorProvider ErrorProvider;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}