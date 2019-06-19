namespace ToyGraf.Views
{
    partial class FourierParamsDialog
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
            this.btnOK = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.label4 = new System.Windows.Forms.Label();
            this.btnCentreX = new System.Windows.Forms.Button();
            this.edMinimumX = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.seHighestHarmonic = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.edMaximumX = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.seHighestHarmonic)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(103, 92);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 27);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(197, 92);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(87, 27);
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(86, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(47, 15);
            this.label4.TabIndex = 1;
            this.label4.Text = "x min =";
            // 
            // btnCentreX
            // 
            this.btnCentreX.FlatAppearance.BorderSize = 0;
            this.btnCentreX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCentreX.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCentreX.Image = global::ToyGraf.Properties.Resources.keybd;
            this.btnCentreX.Location = new System.Drawing.Point(260, 7);
            this.btnCentreX.Margin = new System.Windows.Forms.Padding(0);
            this.btnCentreX.Name = "btnCentreX";
            this.btnCentreX.Size = new System.Drawing.Size(24, 22);
            this.btnCentreX.TabIndex = 3;
            this.btnCentreX.Text = "...";
            this.ToolTip.SetToolTip(this.btnCentreX, "Onscreen Keyboard");
            this.btnCentreX.UseVisualStyleBackColor = true;
            this.btnCentreX.Visible = false;
            // 
            // edMinimumX
            // 
            this.edMinimumX.BackColor = System.Drawing.SystemColors.Window;
            this.edMinimumX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edMinimumX.Location = new System.Drawing.Point(139, 12);
            this.edMinimumX.Name = "edMinimumX";
            this.edMinimumX.Size = new System.Drawing.Size(117, 16);
            this.edMinimumX.TabIndex = 2;
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(10, 7);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(70, 44);
            this.label2.TabIndex = 0;
            this.label2.Text = "Periodic interval";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // seHighestHarmonic
            // 
            this.seHighestHarmonic.BackColor = System.Drawing.SystemColors.Window;
            this.seHighestHarmonic.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seHighestHarmonic.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seHighestHarmonic.Location = new System.Drawing.Point(139, 58);
            this.seHighestHarmonic.Margin = new System.Windows.Forms.Padding(0);
            this.seHighestHarmonic.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.seHighestHarmonic.Name = "seHighestHarmonic";
            this.seHighestHarmonic.ReadOnly = true;
            this.seHighestHarmonic.Size = new System.Drawing.Size(51, 21);
            this.seHighestHarmonic.TabIndex = 8;
            this.seHighestHarmonic.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seHighestHarmonic.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(102, 15);
            this.label1.TabIndex = 7;
            this.label1.Text = "Highest harmonic";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(86, 33);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 15);
            this.label3.TabIndex = 4;
            this.label3.Text = "x max =";
            // 
            // button1
            // 
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Image = global::ToyGraf.Properties.Resources.keybd;
            this.button1.Location = new System.Drawing.Point(260, 29);
            this.button1.Margin = new System.Windows.Forms.Padding(0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(24, 22);
            this.button1.TabIndex = 6;
            this.button1.Text = "...";
            this.ToolTip.SetToolTip(this.button1, "Onscreen Keyboard");
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // edMaximumX
            // 
            this.edMaximumX.BackColor = System.Drawing.SystemColors.Window;
            this.edMaximumX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edMaximumX.Location = new System.Drawing.Point(139, 34);
            this.edMaximumX.Name = "edMaximumX";
            this.edMaximumX.Size = new System.Drawing.Size(117, 16);
            this.edMaximumX.TabIndex = 5;
            // 
            // FourierSeriesParamsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(299, 131);
            this.ControlBox = false;
            this.Controls.Add(this.label3);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.edMaximumX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnCentreX);
            this.Controls.Add(this.edMinimumX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.seHighestHarmonic);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FourierSeriesParamsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Fourier Series Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.seHighestHarmonic)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.Button btnOK;
        internal System.Windows.Forms.Button btnCancel;
        internal System.Windows.Forms.ToolTip ToolTip;
        internal System.Windows.Forms.Label label4;
        internal System.Windows.Forms.Button btnCentreX;
        internal System.Windows.Forms.TextBox edMinimumX;
        internal System.Windows.Forms.Label label2;
        internal System.Windows.Forms.NumericUpDown seHighestHarmonic;
        internal System.Windows.Forms.Label label1;
        internal System.Windows.Forms.Label label3;
        internal System.Windows.Forms.Button button1;
        internal System.Windows.Forms.TextBox edMaximumX;
    }
}