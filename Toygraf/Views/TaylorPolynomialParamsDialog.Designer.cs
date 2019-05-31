﻿namespace ToyGraf.Views
{
    partial class TaylorPolynomialParamsDialog
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
            this.label1 = new System.Windows.Forms.Label();
            this.seDegree = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.edCentreX = new System.Windows.Forms.TextBox();
            this.btnCancel = new System.Windows.Forms.Button();
            this.btnOK = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.btnCentreX = new System.Windows.Forms.Button();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.seDegree)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Degree";
            // 
            // seDegree
            // 
            this.seDegree.BackColor = System.Drawing.SystemColors.Control;
            this.seDegree.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seDegree.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.seDegree.Location = new System.Drawing.Point(123, 9);
            this.seDegree.Margin = new System.Windows.Forms.Padding(0);
            this.seDegree.Maximum = new decimal(new int[] {
            999,
            0,
            0,
            0});
            this.seDegree.Name = "seDegree";
            this.seDegree.ReadOnly = true;
            this.seDegree.Size = new System.Drawing.Size(44, 21);
            this.seDegree.TabIndex = 4;
            this.seDegree.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.seDegree.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 36);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Centre";
            // 
            // edCentreX
            // 
            this.edCentreX.BackColor = System.Drawing.SystemColors.Window;
            this.edCentreX.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.edCentreX.Location = new System.Drawing.Point(123, 37);
            this.edCentreX.Name = "edCentreX";
            this.edCentreX.Size = new System.Drawing.Size(100, 13);
            this.edCentreX.TabIndex = 6;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(169, 66);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOK.Location = new System.Drawing.Point(88, 66);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 9;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(96, 36);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(21, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "x =";
            // 
            // btnCentreX
            // 
            this.btnCentreX.FlatAppearance.BorderSize = 0;
            this.btnCentreX.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnCentreX.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCentreX.Image = global::ToyGraf.Properties.Resources.keybd;
            this.btnCentreX.Location = new System.Drawing.Point(226, 33);
            this.btnCentreX.Margin = new System.Windows.Forms.Padding(0);
            this.btnCentreX.Name = "btnCentreX";
            this.btnCentreX.Size = new System.Drawing.Size(21, 19);
            this.btnCentreX.TabIndex = 7;
            this.btnCentreX.Text = "...";
            this.ToolTip.SetToolTip(this.btnCentreX, "Onscreen Keyboard");
            this.btnCentreX.UseVisualStyleBackColor = true;
            this.btnCentreX.Visible = false;
            // 
            // TaylorPolynomialParamsDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(256, 100);
            this.ControlBox = false;
            this.Controls.Add(this.label4);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnCentreX);
            this.Controls.Add(this.edCentreX);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.seDegree);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TaylorPolynomialParamsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Taylor Polynomial Parameters";
            ((System.ComponentModel.ISupportInitialize)(this.seDegree)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        public System.Windows.Forms.NumericUpDown seDegree;
        private System.Windows.Forms.Label label2;
        public System.Windows.Forms.Button btnCentreX;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label label4;
        public System.Windows.Forms.TextBox edCentreX;
        private System.Windows.Forms.ToolTip ToolTip;
    }
}