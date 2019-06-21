namespace ToyGraf.Views
{
    partial class TracePropertiesDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(TracePropertiesDialog));
            this.FunctionBox = new System.Windows.Forms.ComboBox();
            this.btnClose = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.tbProxy = new System.Windows.Forms.TextBox();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.seTransparency = new System.Windows.Forms.NumericUpDown();
            this.cbPenColour = new System.Windows.Forms.ComboBox();
            this.cbFillColour1 = new System.Windows.Forms.ComboBox();
            this.sePenSize = new System.Windows.Forms.NumericUpDown();
            this.cbPenStyle = new System.Windows.Forms.ComboBox();
            this.cbFillColour2 = new System.Windows.Forms.ComboBox();
            this.cbBrushType = new System.Windows.Forms.ComboBox();
            this.cbHatchStyle = new System.Windows.Forms.ComboBox();
            this.cbGradientMode = new System.Windows.Forms.ComboBox();
            this.cbWrapMode = new System.Windows.Forms.ComboBox();
            this.btnTaylorPolynomial = new System.Windows.Forms.Button();
            this.btnFourierSeries = new System.Windows.Forms.Button();
            this.seIndex = new System.Windows.Forms.NumericUpDown();
            this.IndexLabel = new System.Windows.Forms.Label();
            this.TabControl = new System.Windows.Forms.TabControl();
            this.tpTraceProperties = new System.Windows.Forms.TabPage();
            this.lblType = new System.Windows.Forms.Label();
            this.lblFillColour2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblTransparency = new System.Windows.Forms.Label();
            this.lblFillColour = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTexturePath = new System.Windows.Forms.Label();
            this.btnTexture = new System.Windows.Forms.Button();
            this.tpKeyboard = new System.Windows.Forms.TabPage();
            this.panel1 = new System.Windows.Forms.Panel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.ColourDialog = new System.Windows.Forms.ColorDialog();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.btnFillColour2 = new System.Windows.Forms.Button();
            this.btnFillColour1 = new System.Windows.Forms.Button();
            this.btnPenColour = new System.Windows.Forms.Button();
            this.Keyboard = new ToyGraf.Controls.TgKeyboard();
            ((System.ComponentModel.ISupportInitialize)(this.seTransparency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePenSize)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seIndex)).BeginInit();
            this.TabControl.SuspendLayout();
            this.tpTraceProperties.SuspendLayout();
            this.tpKeyboard.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // FunctionBox
            // 
            this.FunctionBox.Location = new System.Drawing.Point(84, 9);
            this.FunctionBox.Margin = new System.Windows.Forms.Padding(0);
            this.FunctionBox.MaxDropDownItems = 36;
            this.FunctionBox.Name = "FunctionBox";
            this.FunctionBox.Size = new System.Drawing.Size(360, 23);
            this.FunctionBox.TabIndex = 0;
            this.ToolTip.SetToolTip(this.FunctionBox, "Formula for points on this trace");
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnClose.Location = new System.Drawing.Point(379, 6);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 25);
            this.btnClose.TabIndex = 3;
            this.btnClose.Tag = "Fixed";
            this.btnClose.Text = "Close";
            this.btnClose.UseVisualStyleBackColor = true;
            // 
            // tbProxy
            // 
            this.tbProxy.BackColor = System.Drawing.SystemColors.Control;
            this.tbProxy.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.tbProxy.Dock = System.Windows.Forms.DockStyle.Left;
            this.tbProxy.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbProxy.Location = new System.Drawing.Point(3, 3);
            this.tbProxy.Multiline = true;
            this.tbProxy.Name = "tbProxy";
            this.tbProxy.ReadOnly = true;
            this.tbProxy.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.tbProxy.Size = new System.Drawing.Size(360, 31);
            this.tbProxy.TabIndex = 81;
            this.ToolTip.SetToolTip(this.tbProxy, "Preview of the final algebraic expression");
            // 
            // cbVisible
            // 
            this.cbVisible.AutoSize = true;
            this.cbVisible.Location = new System.Drawing.Point(9, 12);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(15, 14);
            this.cbVisible.TabIndex = 84;
            this.ToolTip.SetToolTip(this.cbVisible, "Show or hide this trace");
            this.cbVisible.UseVisualStyleBackColor = true;
            // 
            // seTransparency
            // 
            this.seTransparency.BackColor = System.Drawing.SystemColors.Control;
            this.seTransparency.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seTransparency.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seTransparency.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seTransparency.Location = new System.Drawing.Point(411, 33);
            this.seTransparency.Margin = new System.Windows.Forms.Padding(0);
            this.seTransparency.Name = "seTransparency";
            this.seTransparency.ReadOnly = true;
            this.seTransparency.Size = new System.Drawing.Size(44, 21);
            this.seTransparency.TabIndex = 6;
            this.seTransparency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.seTransparency, "Fill transparency (%)");
            // 
            // cbPenColour
            // 
            this.cbPenColour.BackColor = System.Drawing.SystemColors.Control;
            this.cbPenColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbPenColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPenColour.DropDownWidth = 144;
            this.cbPenColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPenColour.FormattingEnabled = true;
            this.cbPenColour.ItemHeight = 18;
            this.cbPenColour.Location = new System.Drawing.Point(209, 9);
            this.cbPenColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbPenColour.Name = "cbPenColour";
            this.cbPenColour.Size = new System.Drawing.Size(92, 24);
            this.cbPenColour.TabIndex = 4;
            this.ToolTip.SetToolTip(this.cbPenColour, "Pen colour used to draw this trace");
            // 
            // cbFillColour1
            // 
            this.cbFillColour1.BackColor = System.Drawing.SystemColors.Control;
            this.cbFillColour1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillColour1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillColour1.DropDownWidth = 144;
            this.cbFillColour1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFillColour1.FormattingEnabled = true;
            this.cbFillColour1.ItemHeight = 18;
            this.cbFillColour1.Location = new System.Drawing.Point(209, 31);
            this.cbFillColour1.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillColour1.Name = "cbFillColour1";
            this.cbFillColour1.Size = new System.Drawing.Size(92, 24);
            this.cbFillColour1.TabIndex = 5;
            this.ToolTip.SetToolTip(this.cbFillColour1, "Fill colour (area under this trace)");
            // 
            // sePenSize
            // 
            this.sePenSize.BackColor = System.Drawing.SystemColors.Control;
            this.sePenSize.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.sePenSize.DecimalPlaces = 1;
            this.sePenSize.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.sePenSize.Increment = new decimal(new int[] {
            5,
            0,
            0,
            65536});
            this.sePenSize.Location = new System.Drawing.Point(411, 11);
            this.sePenSize.Margin = new System.Windows.Forms.Padding(0);
            this.sePenSize.Maximum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.sePenSize.Name = "sePenSize";
            this.sePenSize.ReadOnly = true;
            this.sePenSize.Size = new System.Drawing.Size(44, 21);
            this.sePenSize.TabIndex = 12;
            this.sePenSize.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.sePenSize, "Fill transparency (%)");
            this.sePenSize.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // cbPenStyle
            // 
            this.cbPenStyle.BackColor = System.Drawing.SystemColors.Control;
            this.cbPenStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPenStyle.DropDownWidth = 144;
            this.cbPenStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbPenStyle.FormattingEnabled = true;
            this.cbPenStyle.ItemHeight = 15;
            this.cbPenStyle.Location = new System.Drawing.Point(64, 9);
            this.cbPenStyle.Margin = new System.Windows.Forms.Padding(0);
            this.cbPenStyle.Name = "cbPenStyle";
            this.cbPenStyle.Size = new System.Drawing.Size(93, 23);
            this.cbPenStyle.TabIndex = 14;
            this.ToolTip.SetToolTip(this.cbPenStyle, "Fill colour (area under this trace)");
            // 
            // cbFillColour2
            // 
            this.cbFillColour2.BackColor = System.Drawing.SystemColors.Control;
            this.cbFillColour2.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillColour2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillColour2.DropDownWidth = 144;
            this.cbFillColour2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFillColour2.FormattingEnabled = true;
            this.cbFillColour2.ItemHeight = 18;
            this.cbFillColour2.Location = new System.Drawing.Point(209, 53);
            this.cbFillColour2.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillColour2.Name = "cbFillColour2";
            this.cbFillColour2.Size = new System.Drawing.Size(92, 24);
            this.cbFillColour2.TabIndex = 16;
            this.ToolTip.SetToolTip(this.cbFillColour2, "Fill colour (area under this trace)");
            // 
            // cbBrushType
            // 
            this.cbBrushType.BackColor = System.Drawing.SystemColors.Control;
            this.cbBrushType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbBrushType.DropDownWidth = 144;
            this.cbBrushType.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbBrushType.FormattingEnabled = true;
            this.cbBrushType.ItemHeight = 15;
            this.cbBrushType.Location = new System.Drawing.Point(64, 31);
            this.cbBrushType.Margin = new System.Windows.Forms.Padding(0);
            this.cbBrushType.Name = "cbBrushType";
            this.cbBrushType.Size = new System.Drawing.Size(93, 23);
            this.cbBrushType.TabIndex = 17;
            this.ToolTip.SetToolTip(this.cbBrushType, "Fill colour (area under this trace)");
            // 
            // cbHatchStyle
            // 
            this.cbHatchStyle.BackColor = System.Drawing.SystemColors.Control;
            this.cbHatchStyle.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbHatchStyle.DropDownWidth = 144;
            this.cbHatchStyle.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbHatchStyle.FormattingEnabled = true;
            this.cbHatchStyle.ItemHeight = 15;
            this.cbHatchStyle.Location = new System.Drawing.Point(64, 53);
            this.cbHatchStyle.Margin = new System.Windows.Forms.Padding(0);
            this.cbHatchStyle.Name = "cbHatchStyle";
            this.cbHatchStyle.Size = new System.Drawing.Size(93, 23);
            this.cbHatchStyle.TabIndex = 19;
            this.ToolTip.SetToolTip(this.cbHatchStyle, "Fill colour (area under this trace)");
            // 
            // cbGradientMode
            // 
            this.cbGradientMode.BackColor = System.Drawing.SystemColors.Control;
            this.cbGradientMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbGradientMode.DropDownWidth = 144;
            this.cbGradientMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbGradientMode.FormattingEnabled = true;
            this.cbGradientMode.ItemHeight = 15;
            this.cbGradientMode.Location = new System.Drawing.Point(64, 53);
            this.cbGradientMode.Margin = new System.Windows.Forms.Padding(0);
            this.cbGradientMode.Name = "cbGradientMode";
            this.cbGradientMode.Size = new System.Drawing.Size(93, 23);
            this.cbGradientMode.TabIndex = 20;
            this.ToolTip.SetToolTip(this.cbGradientMode, "Fill colour (area under this trace)");
            // 
            // cbWrapMode
            // 
            this.cbWrapMode.BackColor = System.Drawing.SystemColors.Control;
            this.cbWrapMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbWrapMode.DropDownWidth = 144;
            this.cbWrapMode.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbWrapMode.FormattingEnabled = true;
            this.cbWrapMode.ItemHeight = 15;
            this.cbWrapMode.Location = new System.Drawing.Point(64, 53);
            this.cbWrapMode.Margin = new System.Windows.Forms.Padding(0);
            this.cbWrapMode.Name = "cbWrapMode";
            this.cbWrapMode.Size = new System.Drawing.Size(93, 23);
            this.cbWrapMode.TabIndex = 23;
            this.ToolTip.SetToolTip(this.cbWrapMode, "Fill colour (area under this trace)");
            // 
            // btnTaylorPolynomial
            // 
            this.btnTaylorPolynomial.Location = new System.Drawing.Point(8, 92);
            this.btnTaylorPolynomial.Name = "btnTaylorPolynomial";
            this.btnTaylorPolynomial.Size = new System.Drawing.Size(72, 25);
            this.btnTaylorPolynomial.TabIndex = 24;
            this.btnTaylorPolynomial.Tag = "Fixed";
            this.btnTaylorPolynomial.Text = "&Taylor...";
            this.ToolTip.SetToolTip(this.btnTaylorPolynomial, "Create a Taylor Polynomial for this trace");
            this.btnTaylorPolynomial.UseVisualStyleBackColor = true;
            // 
            // btnFourierSeries
            // 
            this.btnFourierSeries.Location = new System.Drawing.Point(86, 92);
            this.btnFourierSeries.Name = "btnFourierSeries";
            this.btnFourierSeries.Size = new System.Drawing.Size(72, 25);
            this.btnFourierSeries.TabIndex = 28;
            this.btnFourierSeries.Tag = "Fixed";
            this.btnFourierSeries.Text = "&Fourier...";
            this.ToolTip.SetToolTip(this.btnFourierSeries, "Create a Fourier Series for this trace");
            this.btnFourierSeries.UseVisualStyleBackColor = true;
            // 
            // seIndex
            // 
            this.seIndex.BackColor = System.Drawing.SystemColors.Control;
            this.seIndex.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seIndex.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seIndex.Location = new System.Drawing.Point(68, 6);
            this.seIndex.Margin = new System.Windows.Forms.Padding(0);
            this.seIndex.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.seIndex.Name = "seIndex";
            this.seIndex.Size = new System.Drawing.Size(16, 28);
            this.seIndex.TabIndex = 82;
            // 
            // IndexLabel
            // 
            this.IndexLabel.AutoSize = true;
            this.IndexLabel.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.IndexLabel.Location = new System.Drawing.Point(27, 8);
            this.IndexLabel.Margin = new System.Windows.Forms.Padding(0);
            this.IndexLabel.Name = "IndexLabel";
            this.IndexLabel.Size = new System.Drawing.Size(15, 21);
            this.IndexLabel.TabIndex = 83;
            this.IndexLabel.Text = "f";
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(this.tpTraceProperties);
            this.TabControl.Controls.Add(this.tpKeyboard);
            this.TabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TabControl.Location = new System.Drawing.Point(0, 40);
            this.TabControl.Multiline = true;
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 0;
            this.TabControl.Size = new System.Drawing.Size(463, 201);
            this.TabControl.TabIndex = 85;
            // 
            // tpTraceProperties
            // 
            this.tpTraceProperties.BackColor = System.Drawing.SystemColors.Control;
            this.tpTraceProperties.Controls.Add(this.btnFourierSeries);
            this.tpTraceProperties.Controls.Add(this.btnFillColour2);
            this.tpTraceProperties.Controls.Add(this.btnFillColour1);
            this.tpTraceProperties.Controls.Add(this.btnPenColour);
            this.tpTraceProperties.Controls.Add(this.btnTaylorPolynomial);
            this.tpTraceProperties.Controls.Add(this.cbWrapMode);
            this.tpTraceProperties.Controls.Add(this.cbGradientMode);
            this.tpTraceProperties.Controls.Add(this.cbHatchStyle);
            this.tpTraceProperties.Controls.Add(this.lblType);
            this.tpTraceProperties.Controls.Add(this.cbBrushType);
            this.tpTraceProperties.Controls.Add(this.cbFillColour2);
            this.tpTraceProperties.Controls.Add(this.lblFillColour2);
            this.tpTraceProperties.Controls.Add(this.cbPenStyle);
            this.tpTraceProperties.Controls.Add(this.label6);
            this.tpTraceProperties.Controls.Add(this.sePenSize);
            this.tpTraceProperties.Controls.Add(this.label5);
            this.tpTraceProperties.Controls.Add(this.label4);
            this.tpTraceProperties.Controls.Add(this.lblTransparency);
            this.tpTraceProperties.Controls.Add(this.lblFillColour);
            this.tpTraceProperties.Controls.Add(this.label1);
            this.tpTraceProperties.Controls.Add(this.seTransparency);
            this.tpTraceProperties.Controls.Add(this.cbPenColour);
            this.tpTraceProperties.Controls.Add(this.cbFillColour1);
            this.tpTraceProperties.Controls.Add(this.lblTexturePath);
            this.tpTraceProperties.Controls.Add(this.btnTexture);
            this.tpTraceProperties.Location = new System.Drawing.Point(4, 24);
            this.tpTraceProperties.Name = "tpTraceProperties";
            this.tpTraceProperties.Padding = new System.Windows.Forms.Padding(3);
            this.tpTraceProperties.Size = new System.Drawing.Size(455, 173);
            this.tpTraceProperties.TabIndex = 0;
            this.tpTraceProperties.Text = "Trace Properties";
            // 
            // lblType
            // 
            this.lblType.AutoSize = true;
            this.lblType.Location = new System.Drawing.Point(4, 56);
            this.lblType.Name = "lblType";
            this.lblType.Size = new System.Drawing.Size(35, 15);
            this.lblType.TabIndex = 18;
            this.lblType.Text = "Type:";
            // 
            // lblFillColour2
            // 
            this.lblFillColour2.AutoSize = true;
            this.lblFillColour2.Location = new System.Drawing.Point(160, 56);
            this.lblFillColour2.Name = "lblFillColour2";
            this.lblFillColour2.Size = new System.Drawing.Size(46, 15);
            this.lblFillColour2.TabIndex = 15;
            this.lblFillColour2.Text = "2nd fill:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 12);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(57, 15);
            this.label6.TabIndex = 13;
            this.label6.Text = "Pen style:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(328, 12);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 15);
            this.label5.TabIndex = 11;
            this.label5.Text = "Pen size:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(40, 15);
            this.label4.TabIndex = 10;
            this.label4.Text = "Brush:";
            // 
            // lblTransparency
            // 
            this.lblTransparency.AutoSize = true;
            this.lblTransparency.Location = new System.Drawing.Point(328, 34);
            this.lblTransparency.Name = "lblTransparency";
            this.lblTransparency.Size = new System.Drawing.Size(80, 15);
            this.lblTransparency.TabIndex = 9;
            this.lblTransparency.Text = "Transparency:";
            // 
            // lblFillColour
            // 
            this.lblFillColour.AutoSize = true;
            this.lblFillColour.Location = new System.Drawing.Point(160, 34);
            this.lblFillColour.Name = "lblFillColour";
            this.lblFillColour.Size = new System.Drawing.Size(25, 15);
            this.lblFillColour.TabIndex = 8;
            this.lblFillColour.Text = "Fill:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(160, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(46, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Colour:";
            // 
            // lblTexturePath
            // 
            this.lblTexturePath.AutoEllipsis = true;
            this.lblTexturePath.Location = new System.Drawing.Point(163, 56);
            this.lblTexturePath.Name = "lblTexturePath";
            this.lblTexturePath.Size = new System.Drawing.Size(295, 61);
            this.lblTexturePath.TabIndex = 21;
            // 
            // btnTexture
            // 
            this.btnTexture.Location = new System.Drawing.Point(160, 30);
            this.btnTexture.Name = "btnTexture";
            this.btnTexture.Size = new System.Drawing.Size(25, 22);
            this.btnTexture.TabIndex = 22;
            this.btnTexture.Text = "...";
            this.btnTexture.UseVisualStyleBackColor = true;
            // 
            // tpKeyboard
            // 
            this.tpKeyboard.BackColor = System.Drawing.SystemColors.Control;
            this.tpKeyboard.Controls.Add(this.Keyboard);
            this.tpKeyboard.Location = new System.Drawing.Point(4, 24);
            this.tpKeyboard.Name = "tpKeyboard";
            this.tpKeyboard.Padding = new System.Windows.Forms.Padding(3);
            this.tpKeyboard.Size = new System.Drawing.Size(455, 173);
            this.tpKeyboard.TabIndex = 1;
            this.tpKeyboard.Text = "Onscreen Keyboard";
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.FunctionBox);
            this.panel1.Controls.Add(this.IndexLabel);
            this.panel1.Controls.Add(this.cbVisible);
            this.panel1.Controls.Add(this.seIndex);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(463, 40);
            this.panel1.TabIndex = 86;
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.btnClose);
            this.panel2.Controls.Add(this.tbProxy);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel2.Location = new System.Drawing.Point(0, 241);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(3);
            this.panel2.Size = new System.Drawing.Size(463, 37);
            this.panel2.TabIndex = 87;
            // 
            // ColourDialog
            // 
            this.ColourDialog.AnyColor = true;
            this.ColourDialog.FullOpen = true;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // btnFillColour2
            // 
            this.btnFillColour2.FlatAppearance.BorderSize = 0;
            this.btnFillColour2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillColour2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillColour2.Image = global::ToyGraf.Properties.Resources.Color_linecolor;
            this.btnFillColour2.Location = new System.Drawing.Point(303, 55);
            this.btnFillColour2.Margin = new System.Windows.Forms.Padding(0);
            this.btnFillColour2.Name = "btnFillColour2";
            this.btnFillColour2.Size = new System.Drawing.Size(22, 22);
            this.btnFillColour2.TabIndex = 27;
            this.btnFillColour2.Text = "...";
            this.ToolTip.SetToolTip(this.btnFillColour2, "2nd fill colour dialog");
            this.btnFillColour2.UseVisualStyleBackColor = true;
            // 
            // btnFillColour1
            // 
            this.btnFillColour1.FlatAppearance.BorderSize = 0;
            this.btnFillColour1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnFillColour1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFillColour1.Image = global::ToyGraf.Properties.Resources.Color_linecolor;
            this.btnFillColour1.Location = new System.Drawing.Point(303, 33);
            this.btnFillColour1.Margin = new System.Windows.Forms.Padding(0);
            this.btnFillColour1.Name = "btnFillColour1";
            this.btnFillColour1.Size = new System.Drawing.Size(22, 22);
            this.btnFillColour1.TabIndex = 26;
            this.btnFillColour1.Text = "...";
            this.ToolTip.SetToolTip(this.btnFillColour1, "Fill colour dialog");
            this.btnFillColour1.UseVisualStyleBackColor = true;
            // 
            // btnPenColour
            // 
            this.btnPenColour.FlatAppearance.BorderSize = 0;
            this.btnPenColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnPenColour.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPenColour.Image = global::ToyGraf.Properties.Resources.Color_linecolor;
            this.btnPenColour.Location = new System.Drawing.Point(303, 11);
            this.btnPenColour.Margin = new System.Windows.Forms.Padding(0);
            this.btnPenColour.Name = "btnPenColour";
            this.btnPenColour.Size = new System.Drawing.Size(22, 22);
            this.btnPenColour.TabIndex = 25;
            this.btnPenColour.Text = "...";
            this.ToolTip.SetToolTip(this.btnPenColour, "Pen colour dialog");
            this.btnPenColour.UseVisualStyleBackColor = true;
            // 
            // Keyboard
            // 
            this.Keyboard.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Keyboard.Location = new System.Drawing.Point(0, 0);
            this.Keyboard.Name = "Keyboard";
            this.Keyboard.Size = new System.Drawing.Size(454, 172);
            this.Keyboard.TabIndex = 0;
            // 
            // TracePropertiesDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(463, 278);
            this.Controls.Add(this.TabControl);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "TracePropertiesDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.Manual;
            ((System.ComponentModel.ISupportInitialize)(this.seTransparency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.sePenSize)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seIndex)).EndInit();
            this.TabControl.ResumeLayout(false);
            this.tpTraceProperties.ResumeLayout(false);
            this.tpTraceProperties.PerformLayout();
            this.tpKeyboard.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.Button btnClose;
        internal System.Windows.Forms.ComboBox FunctionBox;
        internal System.Windows.Forms.ToolTip ToolTip;
        internal System.Windows.Forms.TextBox tbProxy;
        internal System.Windows.Forms.NumericUpDown seIndex;
        internal System.Windows.Forms.Label IndexLabel;
        internal System.Windows.Forms.CheckBox cbVisible;
        internal System.Windows.Forms.Panel panel1;
        internal System.Windows.Forms.Panel panel2;
        internal System.Windows.Forms.NumericUpDown seTransparency;
        internal System.Windows.Forms.ComboBox cbPenColour;
        internal System.Windows.Forms.ComboBox cbFillColour1;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.NumericUpDown sePenSize;
        internal System.Windows.Forms.Label label5;
        internal System.Windows.Forms.ComboBox cbPenStyle;
        internal System.Windows.Forms.Label label6;
        internal System.Windows.Forms.TabPage tpKeyboard;
        internal System.Windows.Forms.TabPage tpTraceProperties;
        internal System.Windows.Forms.TabControl TabControl;
        internal System.Windows.Forms.ComboBox cbBrushType;
        internal System.Windows.Forms.ComboBox cbFillColour2;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.ComboBox cbGradientMode;
        internal System.Windows.Forms.ComboBox cbHatchStyle;
        internal System.Windows.Forms.Label lblFillColour;
        internal System.Windows.Forms.Label lblTransparency;
        internal System.Windows.Forms.Label lblFillColour2;
        internal System.Windows.Forms.Label lblType;
        internal System.Windows.Forms.Label lblTexturePath;
        internal System.Windows.Forms.Button btnTexture;
        internal System.Windows.Forms.ComboBox cbWrapMode;
        internal System.Windows.Forms.Button btnTaylorPolynomial;
        internal System.Windows.Forms.Button btnPenColour;
        internal System.Windows.Forms.Button btnFillColour2;
        internal System.Windows.Forms.Button btnFillColour1;
        internal System.Windows.Forms.ColorDialog ColourDialog;
        internal System.Windows.Forms.ErrorProvider ErrorProvider;
        internal System.Windows.Forms.Button btnFourierSeries;
        internal Controls.TgKeyboard Keyboard;
    }
}