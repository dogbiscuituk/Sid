namespace ToyGraf.Views
{
    partial class SeriesView
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SeriesView));
            this.cbFunction = new System.Windows.Forms.ComboBox();
            this.btnRemove = new System.Windows.Forms.Button();
            this.ImageList = new System.Windows.Forms.ImageList(this.components);
            this.cbPenColour = new System.Windows.Forms.ComboBox();
            this.cbFillColour = new System.Windows.Forms.ComboBox();
            this.cbVisible = new System.Windows.Forms.CheckBox();
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.seTransparency = new System.Windows.Forms.NumericUpDown();
            this.btnDetails = new System.Windows.Forms.Button();
            this.Label = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.seTransparency)).BeginInit();
            this.SuspendLayout();
            // 
            // cbFunction
            // 
            this.cbFunction.BackColor = System.Drawing.SystemColors.Control;
            this.cbFunction.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFunction.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFunction.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cbFunction.FormattingEnabled = true;
            this.cbFunction.ItemHeight = 18;
            this.cbFunction.Location = new System.Drawing.Point(54, 0);
            this.cbFunction.Margin = new System.Windows.Forms.Padding(0);
            this.cbFunction.Name = "cbFunction";
            this.cbFunction.Size = new System.Drawing.Size(173, 24);
            this.cbFunction.TabIndex = 0;
            this.ToolTip.SetToolTip(this.cbFunction, "Formula for points on this trace");
            // 
            // btnRemove
            // 
            this.btnRemove.FlatAppearance.BorderSize = 0;
            this.btnRemove.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnRemove.ImageIndex = 1;
            this.btnRemove.ImageList = this.ImageList;
            this.btnRemove.Location = new System.Drawing.Point(467, 1);
            this.btnRemove.Margin = new System.Windows.Forms.Padding(0);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(21, 19);
            this.btnRemove.TabIndex = 4;
            this.ToolTip.SetToolTip(this.btnRemove, "Delete this trace from the graph");
            this.btnRemove.UseVisualStyleBackColor = true;
            // 
            // ImageList
            // 
            this.ImageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("ImageList.ImageStream")));
            this.ImageList.TransparentColor = System.Drawing.Color.Transparent;
            this.ImageList.Images.SetKeyName(0, "close.png");
            this.ImageList.Images.SetKeyName(1, "delete.png");
            this.ImageList.Images.SetKeyName(2, "info.png");
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
            this.cbPenColour.Location = new System.Drawing.Point(246, 0);
            this.cbPenColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbPenColour.Name = "cbPenColour";
            this.cbPenColour.Size = new System.Drawing.Size(74, 24);
            this.cbPenColour.TabIndex = 1;
            this.ToolTip.SetToolTip(this.cbPenColour, "Pen colour used to draw this trace");
            // 
            // cbFillColour
            // 
            this.cbFillColour.BackColor = System.Drawing.SystemColors.Control;
            this.cbFillColour.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFillColour.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFillColour.DropDownWidth = 144;
            this.cbFillColour.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbFillColour.FormattingEnabled = true;
            this.cbFillColour.ItemHeight = 18;
            this.cbFillColour.Location = new System.Drawing.Point(320, 0);
            this.cbFillColour.Margin = new System.Windows.Forms.Padding(0);
            this.cbFillColour.Name = "cbFillColour";
            this.cbFillColour.Size = new System.Drawing.Size(74, 24);
            this.cbFillColour.TabIndex = 2;
            this.ToolTip.SetToolTip(this.cbFillColour, "Fill colour (area under this trace)");
            // 
            // cbVisible
            // 
            this.cbVisible.BackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.BorderColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.BorderSize = 0;
            this.cbVisible.FlatAppearance.CheckedBackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.MouseDownBackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatAppearance.MouseOverBackColor = System.Drawing.SystemColors.Control;
            this.cbVisible.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.cbVisible.Location = new System.Drawing.Point(5, 2);
            this.cbVisible.Margin = new System.Windows.Forms.Padding(0);
            this.cbVisible.Name = "cbVisible";
            this.cbVisible.Size = new System.Drawing.Size(12, 18);
            this.cbVisible.TabIndex = 5;
            this.ToolTip.SetToolTip(this.cbVisible, "Show or hide this trace");
            this.cbVisible.UseVisualStyleBackColor = false;
            // 
            // seTransparency
            // 
            this.seTransparency.BackColor = System.Drawing.SystemColors.Control;
            this.seTransparency.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seTransparency.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.seTransparency.Location = new System.Drawing.Point(394, 3);
            this.seTransparency.Margin = new System.Windows.Forms.Padding(0);
            this.seTransparency.Name = "seTransparency";
            this.seTransparency.ReadOnly = true;
            this.seTransparency.Size = new System.Drawing.Size(44, 19);
            this.seTransparency.TabIndex = 3;
            this.seTransparency.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.ToolTip.SetToolTip(this.seTransparency, "Fill transparency (%)");
            // 
            // btnDetails
            // 
            this.btnDetails.FlatAppearance.BorderSize = 0;
            this.btnDetails.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDetails.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDetails.Image = global::ToyGraf.Properties.Resources._126_Edit_16x16_72;
            this.btnDetails.Location = new System.Drawing.Point(443, 1);
            this.btnDetails.Margin = new System.Windows.Forms.Padding(0);
            this.btnDetails.Name = "btnDetails";
            this.btnDetails.Size = new System.Drawing.Size(21, 19);
            this.btnDetails.TabIndex = 6;
            this.btnDetails.Text = "...";
            this.ToolTip.SetToolTip(this.btnDetails, "Show more details");
            this.btnDetails.UseVisualStyleBackColor = true;
            // 
            // Label
            // 
            this.Label.Location = new System.Drawing.Point(20, 2);
            this.Label.Name = "Label";
            this.Label.Size = new System.Drawing.Size(31, 18);
            this.Label.TabIndex = 7;
            this.Label.Text = "f";
            // 
            // SeriesView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.Transparent;
            this.Controls.Add(this.Label);
            this.Controls.Add(this.btnDetails);
            this.Controls.Add(this.seTransparency);
            this.Controls.Add(this.cbFunction);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.cbPenColour);
            this.Controls.Add(this.cbFillColour);
            this.Controls.Add(this.cbVisible);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "SeriesView";
            this.Size = new System.Drawing.Size(489, 24);
            ((System.ComponentModel.ISupportInitialize)(this.seTransparency)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        public System.Windows.Forms.ComboBox cbFunction;
        public System.Windows.Forms.Button btnRemove;
        public System.Windows.Forms.CheckBox cbVisible;
        private System.Windows.Forms.ToolTip ToolTip;
        public System.Windows.Forms.ComboBox cbPenColour;
        public System.Windows.Forms.ComboBox cbFillColour;
        public System.Windows.Forms.NumericUpDown seTransparency;
        public System.Windows.Forms.Button btnDetails;
        private System.Windows.Forms.ImageList ImageList;
        public System.Windows.Forms.Label Label;
    }
}
