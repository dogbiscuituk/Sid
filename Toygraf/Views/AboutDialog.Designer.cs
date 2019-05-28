﻿namespace ToyGraf.Views
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.btnOK = new System.Windows.Forms.Button();
            this.lblDescription = new System.Windows.Forms.Label();
            this.lblAuthor = new System.Windows.Forms.Label();
            this.lblCopyright = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.NewtonsoftLinkLabel = new System.Windows.Forms.LinkLabel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnOK.Location = new System.Drawing.Point(165, 146);
            this.btnOK.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(87, 26);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Visible = false;
            // 
            // lblDescription
            // 
            this.lblDescription.AutoSize = true;
            this.lblDescription.Location = new System.Drawing.Point(91, 11);
            this.lblDescription.Margin = new System.Windows.Forms.Padding(3);
            this.lblDescription.Name = "lblDescription";
            this.lblDescription.Size = new System.Drawing.Size(67, 15);
            this.lblDescription.TabIndex = 1;
            this.lblDescription.Text = "Description";
            // 
            // lblAuthor
            // 
            this.lblAuthor.AutoSize = true;
            this.lblAuthor.Location = new System.Drawing.Point(175, 53);
            this.lblAuthor.Margin = new System.Windows.Forms.Padding(3);
            this.lblAuthor.Name = "lblAuthor";
            this.lblAuthor.Size = new System.Drawing.Size(44, 15);
            this.lblAuthor.TabIndex = 2;
            this.lblAuthor.Text = "Author";
            // 
            // lblCopyright
            // 
            this.lblCopyright.AutoSize = true;
            this.lblCopyright.Location = new System.Drawing.Point(175, 74);
            this.lblCopyright.Margin = new System.Windows.Forms.Padding(3);
            this.lblCopyright.Name = "lblCopyright";
            this.lblCopyright.Size = new System.Drawing.Size(60, 15);
            this.lblCopyright.TabIndex = 3;
            this.lblCopyright.Text = "Copyright";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(91, 32);
            this.label2.Margin = new System.Windows.Forms.Padding(3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 15);
            this.label2.TabIndex = 5;
            this.label2.Text = "Version:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(91, 53);
            this.label3.Margin = new System.Windows.Forms.Padding(3);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 15);
            this.label3.TabIndex = 6;
            this.label3.Text = "Author:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(91, 74);
            this.label4.Margin = new System.Windows.Forms.Padding(3);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(63, 15);
            this.label4.TabIndex = 7;
            this.label4.Text = "Copyright:";
            // 
            // lblVersion
            // 
            this.lblVersion.AutoSize = true;
            this.lblVersion.Location = new System.Drawing.Point(175, 32);
            this.lblVersion.Margin = new System.Windows.Forms.Padding(3);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(45, 15);
            this.lblVersion.TabIndex = 9;
            this.lblVersion.Text = "Version";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(122, 103);
            this.label5.Margin = new System.Windows.Forms.Padding(3);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(163, 15);
            this.label5.TabIndex = 10;
            this.label5.Text = "Newtonsoft Json.NET licence:";
            // 
            // NewtonsoftLinkLabel
            // 
            this.NewtonsoftLinkLabel.AutoSize = true;
            this.NewtonsoftLinkLabel.Location = new System.Drawing.Point(12, 124);
            this.NewtonsoftLinkLabel.Margin = new System.Windows.Forms.Padding(3);
            this.NewtonsoftLinkLabel.Name = "NewtonsoftLinkLabel";
            this.NewtonsoftLinkLabel.Size = new System.Drawing.Size(395, 15);
            this.NewtonsoftLinkLabel.TabIndex = 11;
            this.NewtonsoftLinkLabel.TabStop = true;
            this.NewtonsoftLinkLabel.Text = "https://github.com/JamesNK/Newtonsoft.Json/blob/master/LICENSE.md";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::ToyGraf.Properties.Resources._3phase64;
            this.pictureBox1.Location = new System.Drawing.Point(12, 12);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(64, 64);
            this.pictureBox1.TabIndex = 12;
            this.pictureBox1.TabStop = false;
            // 
            // AboutDialog
            // 
            this.AcceptButton = this.btnOK;
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.CancelButton = this.btnOK;
            this.ClientSize = new System.Drawing.Size(417, 178);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.NewtonsoftLinkLabel);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblCopyright);
            this.Controls.Add(this.lblAuthor);
            this.Controls.Add(this.lblDescription);
            this.Controls.Add(this.btnOK);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "About";
            this.TopMost = true;
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        public System.Windows.Forms.Label lblDescription;
        public System.Windows.Forms.Label lblAuthor;
        public System.Windows.Forms.Label lblCopyright;
        public System.Windows.Forms.Label lblVersion;
        public System.Windows.Forms.LinkLabel NewtonsoftLinkLabel;
        private System.Windows.Forms.PictureBox pictureBox1;
        public System.Windows.Forms.Button btnOK;
    }
}