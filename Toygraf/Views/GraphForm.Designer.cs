namespace ToyGraf.Views
{
    partial class GraphForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GraphForm));
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.LegendPanel = new System.Windows.Forms.Panel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.TextureDialog = new System.Windows.Forms.OpenFileDialog();
            this.PropertyGrid = new System.Windows.Forms.PropertyGrid();
            this.PopupPropertyGridMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PopupPropertyGridFloat = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupPropertyGridHide = new System.Windows.Forms.ToolStripMenuItem();
            this.SplitContainer1 = new System.Windows.Forms.SplitContainer();
            this.SplitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TraceTable = new System.Windows.Forms.DataGridView();
            this.colFormula = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colDerivative = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.colProxy = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.PopupTraceTableMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PopupTraceTableFloat = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupTraceTableHide = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripContainer = new System.Windows.Forms.ToolStripContainer();
            this.StatusBar = new ToyGraf.Controls.TgStatusStrip();
            this.tbDecelerate = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbReverse = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbStop = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbPause = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbForward = new System.Windows.Forms.ToolStripDropDownButton();
            this.tbAccelerate = new System.Windows.Forms.ToolStripDropDownButton();
            this.SpeedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Tlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FPSlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.XYlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Rϴlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Toolbar = new ToyGraf.Controls.TgToolStrip();
            this.tbNew = new System.Windows.Forms.ToolStripSplitButton();
            this.tbNewEmptyGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.tbNewFromTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.tbOpen = new System.Windows.Forms.ToolStripSplitButton();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.tbUndo = new System.Windows.Forms.ToolStripSplitButton();
            this.tbRedo = new System.Windows.Forms.ToolStripSplitButton();
            this.tbCut = new System.Windows.Forms.ToolStripButton();
            this.tbCopy = new System.Windows.Forms.ToolStripButton();
            this.tbPaste = new System.Windows.Forms.ToolStripButton();
            this.tbDelete = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.tbAdd = new System.Windows.Forms.ToolStripButton();
            this.tbPlotType = new System.Windows.Forms.ToolStripSplitButton();
            this.tbCartesian = new System.Windows.Forms.ToolStripMenuItem();
            this.tbPolar = new System.Windows.Forms.ToolStripMenuItem();
            this.tbProperties = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.tbFullScreen = new System.Windows.Forms.ToolStripButton();
            this.tbLegend = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.tbTool = new System.Windows.Forms.ToolStripSplitButton();
            this.tbToolArrow = new System.Windows.Forms.ToolStripMenuItem();
            this.tbToolCross = new System.Windows.Forms.ToolStripMenuItem();
            this.tbToolHand = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new ToyGraf.Controls.TgMenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNewEmptyGraph = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNewFromTemplate = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.FileReopen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileClose = new System.Windows.Forms.ToolStripMenuItem();
            this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.EditCut = new System.Windows.Forms.ToolStripMenuItem();
            this.EditCopy = new System.Windows.Forms.ToolStripMenuItem();
            this.EditPaste = new System.Windows.Forms.ToolStripMenuItem();
            this.EditDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem9 = new System.Windows.Forms.ToolStripSeparator();
            this.EditSelectAll = new System.Windows.Forms.ToolStripMenuItem();
            this.EditInvertSelection = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem10 = new System.Windows.Forms.ToolStripSeparator();
            this.EditOptions = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphAddNewFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphType = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphTypeCartesian = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphTypePolar = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.ScrollCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewToolbar = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendFloat = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewLegendTopLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewPropertyGrid = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewTraceTable = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewCoordinatesTooltip = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeDecelerate = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeReverse = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeStop = new System.Windows.Forms.ToolStripMenuItem();
            this.TimePause = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeForward = new System.Windows.Forms.ToolStripMenuItem();
            this.TimeAccelerate = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.PopupLegendMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.PopupLegendFloat = new System.Windows.Forms.ToolStripMenuItem();
            this.hideToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem15 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripMenuItem11 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem12 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem13 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem14 = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.PopupPropertyGridMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).BeginInit();
            this.SplitContainer1.Panel1.SuspendLayout();
            this.SplitContainer1.Panel2.SuspendLayout();
            this.SplitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).BeginInit();
            this.SplitContainer2.Panel1.SuspendLayout();
            this.SplitContainer2.Panel2.SuspendLayout();
            this.SplitContainer2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TraceTable)).BeginInit();
            this.PopupTraceTableMenu.SuspendLayout();
            this.ToolStripContainer.BottomToolStripPanel.SuspendLayout();
            this.ToolStripContainer.ContentPanel.SuspendLayout();
            this.ToolStripContainer.LeftToolStripPanel.SuspendLayout();
            this.ToolStripContainer.TopToolStripPanel.SuspendLayout();
            this.ToolStripContainer.SuspendLayout();
            this.StatusBar.SuspendLayout();
            this.Toolbar.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.PopupLegendMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // PopupMenu
            // 
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(61, 4);
            this.PopupMenu.Text = "Popup Menu";
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // ClientPanel
            // 
            this.ClientPanel.BackColor = System.Drawing.Color.White;
            this.ClientPanel.Controls.Add(this.LegendPanel);
            this.ClientPanel.Controls.Add(this.PictureBox);
            this.ClientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientPanel.Location = new System.Drawing.Point(0, 0);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(519, 414);
            this.ClientPanel.TabIndex = 6;
            // 
            // LegendPanel
            // 
            this.LegendPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LegendPanel.ContextMenuStrip = this.PopupLegendMenu;
            this.LegendPanel.Location = new System.Drawing.Point(0, 0);
            this.LegendPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LegendPanel.Name = "LegendPanel";
            this.LegendPanel.Size = new System.Drawing.Size(19, 16);
            this.LegendPanel.TabIndex = 9;
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(22, 19);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(19, 16);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            // 
            // PropertyGrid
            // 
            this.PropertyGrid.ContextMenuStrip = this.PopupPropertyGridMenu;
            this.PropertyGrid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PropertyGrid.Location = new System.Drawing.Point(0, 0);
            this.PropertyGrid.Name = "PropertyGrid";
            this.PropertyGrid.Size = new System.Drawing.Size(229, 414);
            this.PropertyGrid.TabIndex = 14;
            // 
            // PopupPropertyGridMenu
            // 
            this.PopupPropertyGridMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupPropertyGridFloat,
            this.PopupPropertyGridHide});
            this.PopupPropertyGridMenu.Name = "PopupPropertyGridMenu";
            this.PopupPropertyGridMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // PopupPropertyGridFloat
            // 
            this.PopupPropertyGridFloat.Name = "PopupPropertyGridFloat";
            this.PopupPropertyGridFloat.Size = new System.Drawing.Size(100, 22);
            this.PopupPropertyGridFloat.Text = "&Float";
            // 
            // PopupPropertyGridHide
            // 
            this.PopupPropertyGridHide.Name = "PopupPropertyGridHide";
            this.PopupPropertyGridHide.Size = new System.Drawing.Size(100, 22);
            this.PopupPropertyGridHide.Text = "&Hide";
            // 
            // SplitContainer1
            // 
            this.SplitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer1.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer1.Name = "SplitContainer1";
            // 
            // SplitContainer1.Panel1
            // 
            this.SplitContainer1.Panel1.Controls.Add(this.ClientPanel);
            // 
            // SplitContainer1.Panel2
            // 
            this.SplitContainer1.Panel2.Controls.Add(this.PropertyGrid);
            this.SplitContainer1.Size = new System.Drawing.Size(752, 414);
            this.SplitContainer1.SplitterDistance = 519;
            this.SplitContainer1.TabIndex = 17;
            // 
            // SplitContainer2
            // 
            this.SplitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer2.Location = new System.Drawing.Point(0, 0);
            this.SplitContainer2.Name = "SplitContainer2";
            this.SplitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // SplitContainer2.Panel1
            // 
            this.SplitContainer2.Panel1.Controls.Add(this.SplitContainer1);
            // 
            // SplitContainer2.Panel2
            // 
            this.SplitContainer2.Panel2.Controls.Add(this.TraceTable);
            this.SplitContainer2.Size = new System.Drawing.Size(752, 512);
            this.SplitContainer2.SplitterDistance = 414;
            this.SplitContainer2.TabIndex = 7;
            // 
            // TraceTable
            // 
            this.TraceTable.AllowUserToAddRows = false;
            this.TraceTable.AllowUserToDeleteRows = false;
            this.TraceTable.AllowUserToOrderColumns = true;
            this.TraceTable.AllowUserToResizeRows = false;
            this.TraceTable.BackgroundColor = System.Drawing.SystemColors.Window;
            this.TraceTable.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.TraceTable.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.TraceTable.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TraceTable.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.TraceTable.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.colFormula,
            this.colDerivative,
            this.colProxy});
            this.TraceTable.ContextMenuStrip = this.PopupTraceTableMenu;
            this.TraceTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TraceTable.ImeMode = System.Windows.Forms.ImeMode.Katakana;
            this.TraceTable.Location = new System.Drawing.Point(0, 0);
            this.TraceTable.Name = "TraceTable";
            this.TraceTable.ReadOnly = true;
            this.TraceTable.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.TraceTable.RowHeadersVisible = false;
            this.TraceTable.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.TraceTable.Size = new System.Drawing.Size(752, 94);
            this.TraceTable.TabIndex = 0;
            // 
            // colFormula
            // 
            this.colFormula.DataPropertyName = "Formula";
            this.colFormula.HeaderText = "Formula";
            this.colFormula.Name = "colFormula";
            this.colFormula.ReadOnly = true;
            this.colFormula.Width = 250;
            // 
            // colDerivative
            // 
            this.colDerivative.DataPropertyName = "Derivative";
            this.colDerivative.HeaderText = "Derivative";
            this.colDerivative.Name = "colDerivative";
            this.colDerivative.ReadOnly = true;
            this.colDerivative.Width = 251;
            // 
            // colProxy
            // 
            this.colProxy.DataPropertyName = "Proxy";
            this.colProxy.HeaderText = "Proxy";
            this.colProxy.Name = "colProxy";
            this.colProxy.ReadOnly = true;
            this.colProxy.Width = 251;
            // 
            // PopupTraceTableMenu
            // 
            this.PopupTraceTableMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupTraceTableFloat,
            this.PopupTraceTableHide});
            this.PopupTraceTableMenu.Name = "PopupDataGridMenu";
            this.PopupTraceTableMenu.Size = new System.Drawing.Size(101, 48);
            // 
            // PopupTraceTableFloat
            // 
            this.PopupTraceTableFloat.Name = "PopupTraceTableFloat";
            this.PopupTraceTableFloat.Size = new System.Drawing.Size(100, 22);
            this.PopupTraceTableFloat.Text = "&Float";
            // 
            // PopupTraceTableHide
            // 
            this.PopupTraceTableHide.Name = "PopupTraceTableHide";
            this.PopupTraceTableHide.Size = new System.Drawing.Size(100, 22);
            this.PopupTraceTableHide.Text = "&Hide";
            // 
            // ToolStripContainer
            // 
            // 
            // ToolStripContainer.BottomToolStripPanel
            // 
            this.ToolStripContainer.BottomToolStripPanel.Controls.Add(this.StatusBar);
            // 
            // ToolStripContainer.ContentPanel
            // 
            this.ToolStripContainer.ContentPanel.Controls.Add(this.SplitContainer2);
            this.ToolStripContainer.ContentPanel.Size = new System.Drawing.Size(752, 512);
            this.ToolStripContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            // 
            // ToolStripContainer.LeftToolStripPanel
            // 
            this.ToolStripContainer.LeftToolStripPanel.Controls.Add(this.Toolbar);
            this.ToolStripContainer.Location = new System.Drawing.Point(0, 0);
            this.ToolStripContainer.Name = "ToolStripContainer";
            this.ToolStripContainer.Size = new System.Drawing.Size(784, 561);
            this.ToolStripContainer.TabIndex = 18;
            this.ToolStripContainer.Text = "toolStripContainer1";
            // 
            // ToolStripContainer.TopToolStripPanel
            // 
            this.ToolStripContainer.TopToolStripPanel.Controls.Add(this.MainMenu);
            // 
            // StatusBar
            // 
            this.StatusBar.Dock = System.Windows.Forms.DockStyle.None;
            this.StatusBar.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbDecelerate,
            this.tbReverse,
            this.tbStop,
            this.tbPause,
            this.tbForward,
            this.tbAccelerate,
            this.SpeedLabel,
            this.Tlabel,
            this.FPSlabel,
            this.XYlabel,
            this.Rϴlabel,
            this.ModifiedLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 0);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.StatusBar.ShowItemToolTips = true;
            this.StatusBar.Size = new System.Drawing.Size(784, 25);
            this.StatusBar.TabIndex = 3;
            this.StatusBar.Text = "statusStrip1";
            // 
            // tbDecelerate
            // 
            this.tbDecelerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbDecelerate.Image = global::ToyGraf.Properties.Resources.RewindHS;
            this.tbDecelerate.ImageTransparentColor = System.Drawing.Color.White;
            this.tbDecelerate.Name = "tbDecelerate";
            this.tbDecelerate.ShowDropDownArrow = false;
            this.tbDecelerate.Size = new System.Drawing.Size(20, 23);
            this.tbDecelerate.ToolTipText = "Decelerate";
            // 
            // tbReverse
            // 
            this.tbReverse.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbReverse.Image = global::ToyGraf.Properties.Resources.BackHS;
            this.tbReverse.ImageTransparentColor = System.Drawing.Color.White;
            this.tbReverse.Name = "tbReverse";
            this.tbReverse.ShowDropDownArrow = false;
            this.tbReverse.Size = new System.Drawing.Size(20, 23);
            this.tbReverse.ToolTipText = "Reverse";
            // 
            // tbStop
            // 
            this.tbStop.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbStop.Image = global::ToyGraf.Properties.Resources.StopHS;
            this.tbStop.ImageTransparentColor = System.Drawing.Color.White;
            this.tbStop.Name = "tbStop";
            this.tbStop.ShowDropDownArrow = false;
            this.tbStop.Size = new System.Drawing.Size(20, 23);
            this.tbStop.ToolTipText = "Stop";
            // 
            // tbPause
            // 
            this.tbPause.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPause.Image = global::ToyGraf.Properties.Resources.PauseHS;
            this.tbPause.ImageTransparentColor = System.Drawing.Color.White;
            this.tbPause.Name = "tbPause";
            this.tbPause.ShowDropDownArrow = false;
            this.tbPause.Size = new System.Drawing.Size(20, 23);
            this.tbPause.ToolTipText = "Pause";
            // 
            // tbForward
            // 
            this.tbForward.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbForward.Image = global::ToyGraf.Properties.Resources.PlayHS;
            this.tbForward.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbForward.Name = "tbForward";
            this.tbForward.ShowDropDownArrow = false;
            this.tbForward.Size = new System.Drawing.Size(20, 23);
            this.tbForward.ToolTipText = "Forward";
            // 
            // tbAccelerate
            // 
            this.tbAccelerate.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAccelerate.Image = global::ToyGraf.Properties.Resources.FFwdHS;
            this.tbAccelerate.ImageTransparentColor = System.Drawing.Color.White;
            this.tbAccelerate.Name = "tbAccelerate";
            this.tbAccelerate.ShowDropDownArrow = false;
            this.tbAccelerate.Size = new System.Drawing.Size(20, 23);
            this.tbAccelerate.ToolTipText = "Accelerate";
            // 
            // SpeedLabel
            // 
            this.SpeedLabel.AutoSize = false;
            this.SpeedLabel.Name = "SpeedLabel";
            this.SpeedLabel.Size = new System.Drawing.Size(64, 20);
            this.SpeedLabel.Text = "time × 1";
            // 
            // Tlabel
            // 
            this.Tlabel.AutoSize = false;
            this.Tlabel.Name = "Tlabel";
            this.Tlabel.Size = new System.Drawing.Size(64, 20);
            this.Tlabel.Text = "t=0.0";
            this.Tlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FPSlabel
            // 
            this.FPSlabel.AutoSize = false;
            this.FPSlabel.Name = "FPSlabel";
            this.FPSlabel.Size = new System.Drawing.Size(64, 20);
            this.FPSlabel.Text = "fps=0.0";
            this.FPSlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // XYlabel
            // 
            this.XYlabel.AutoSize = false;
            this.XYlabel.Name = "XYlabel";
            this.XYlabel.Size = new System.Drawing.Size(192, 20);
            this.XYlabel.Text = "{x=0, y=0}";
            this.XYlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Rϴlabel
            // 
            this.Rϴlabel.AutoSize = false;
            this.Rϴlabel.Name = "Rϴlabel";
            this.Rϴlabel.Size = new System.Drawing.Size(192, 20);
            this.Rϴlabel.Text = "{r=0, θ=0}";
            this.Rϴlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ModifiedLabel
            // 
            this.ModifiedLabel.AutoSize = false;
            this.ModifiedLabel.Name = "ModifiedLabel";
            this.ModifiedLabel.Size = new System.Drawing.Size(55, 20);
            this.ModifiedLabel.Text = "Modified";
            // 
            // Toolbar
            // 
            this.Toolbar.Dock = System.Windows.Forms.DockStyle.None;
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbNew,
            this.tbOpen,
            this.tbSave,
            this.toolStripSeparator2,
            this.tbUndo,
            this.tbRedo,
            this.tbCut,
            this.tbCopy,
            this.tbPaste,
            this.tbDelete,
            this.toolStripSeparator1,
            this.tbAdd,
            this.tbPlotType,
            this.tbProperties,
            this.toolStripSeparator3,
            this.tbFullScreen,
            this.tbLegend,
            this.toolStripSeparator5,
            this.tbTool});
            this.Toolbar.Location = new System.Drawing.Point(0, 3);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Padding = new System.Windows.Forms.Padding(0);
            this.Toolbar.Size = new System.Drawing.Size(32, 288);
            this.Toolbar.TabIndex = 12;
            // 
            // tbNew
            // 
            this.tbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbNewEmptyGraph,
            this.tbNewFromTemplate});
            this.tbNew.Image = global::ToyGraf.Properties.Resources.NewDocumentHS;
            this.tbNew.ImageTransparentColor = System.Drawing.Color.White;
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(31, 20);
            this.tbNew.ToolTipText = "Create a new file (^N)";
            // 
            // tbNewEmptyGraph
            // 
            this.tbNewEmptyGraph.Name = "tbNewEmptyGraph";
            this.tbNewEmptyGraph.ShortcutKeyDisplayString = "^N";
            this.tbNewEmptyGraph.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.tbNewEmptyGraph.Size = new System.Drawing.Size(194, 22);
            this.tbNewEmptyGraph.Text = "&New Empty Graph";
            // 
            // tbNewFromTemplate
            // 
            this.tbNewFromTemplate.Name = "tbNewFromTemplate";
            this.tbNewFromTemplate.Size = new System.Drawing.Size(194, 22);
            this.tbNewFromTemplate.Text = "New From &Template...";
            // 
            // tbOpen
            // 
            this.tbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbOpen.Image = global::ToyGraf.Properties.Resources.OpenFile;
            this.tbOpen.ImageTransparentColor = System.Drawing.Color.White;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(31, 20);
            this.tbOpen.ToolTipText = "Open an existing file (^O)";
            // 
            // tbSave
            // 
            this.tbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbSave.Image = global::ToyGraf.Properties.Resources.saveHS;
            this.tbSave.ImageTransparentColor = System.Drawing.Color.White;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(31, 20);
            this.tbSave.ToolTipText = "Save to file (^S)";
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(31, 6);
            // 
            // tbUndo
            // 
            this.tbUndo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbUndo.Image = global::ToyGraf.Properties.Resources.Edit_UndoHS;
            this.tbUndo.ImageTransparentColor = System.Drawing.Color.White;
            this.tbUndo.Name = "tbUndo";
            this.tbUndo.Size = new System.Drawing.Size(31, 20);
            this.tbUndo.ToolTipText = "Undo (^Z)";
            // 
            // tbRedo
            // 
            this.tbRedo.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbRedo.Image = global::ToyGraf.Properties.Resources.Edit_RedoHS;
            this.tbRedo.ImageTransparentColor = System.Drawing.Color.White;
            this.tbRedo.Name = "tbRedo";
            this.tbRedo.Size = new System.Drawing.Size(31, 20);
            this.tbRedo.Text = "toolStripSplitButton2";
            this.tbRedo.ToolTipText = "Redo (^Y)";
            // 
            // tbCut
            // 
            this.tbCut.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbCut.Image = global::ToyGraf.Properties.Resources.CutHS;
            this.tbCut.ImageTransparentColor = System.Drawing.Color.White;
            this.tbCut.Name = "tbCut";
            this.tbCut.Size = new System.Drawing.Size(31, 20);
            this.tbCut.Text = "toolStripButton1";
            this.tbCut.ToolTipText = "Cut";
            this.tbCut.Visible = false;
            // 
            // tbCopy
            // 
            this.tbCopy.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbCopy.Image = global::ToyGraf.Properties.Resources.CopyHS;
            this.tbCopy.ImageTransparentColor = System.Drawing.Color.White;
            this.tbCopy.Name = "tbCopy";
            this.tbCopy.Size = new System.Drawing.Size(31, 20);
            this.tbCopy.Text = "toolStripButton2";
            this.tbCopy.ToolTipText = "Copy";
            this.tbCopy.Visible = false;
            // 
            // tbPaste
            // 
            this.tbPaste.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPaste.Image = global::ToyGraf.Properties.Resources.PasteHS;
            this.tbPaste.ImageTransparentColor = System.Drawing.Color.White;
            this.tbPaste.Name = "tbPaste";
            this.tbPaste.Size = new System.Drawing.Size(31, 20);
            this.tbPaste.Text = "toolStripButton3";
            this.tbPaste.ToolTipText = "Paste";
            this.tbPaste.Visible = false;
            // 
            // tbDelete
            // 
            this.tbDelete.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbDelete.Image = global::ToyGraf.Properties.Resources.Delete;
            this.tbDelete.ImageTransparentColor = System.Drawing.Color.White;
            this.tbDelete.Name = "tbDelete";
            this.tbDelete.Size = new System.Drawing.Size(31, 20);
            this.tbDelete.Text = "toolStripButton4";
            this.tbDelete.ToolTipText = "Delete";
            this.tbDelete.Visible = false;
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(31, 6);
            // 
            // tbAdd
            // 
            this.tbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAdd.Image = global::ToyGraf.Properties.Resources.action_add_16xLG;
            this.tbAdd.ImageTransparentColor = System.Drawing.Color.White;
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(31, 20);
            this.tbAdd.ToolTipText = "Add a new function (F2)";
            // 
            // tbPlotType
            // 
            this.tbPlotType.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPlotType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbCartesian,
            this.tbPolar});
            this.tbPlotType.Image = global::ToyGraf.Properties.Resources.Cartesian;
            this.tbPlotType.ImageTransparentColor = System.Drawing.Color.White;
            this.tbPlotType.Name = "tbPlotType";
            this.tbPlotType.Size = new System.Drawing.Size(31, 20);
            this.tbPlotType.Text = "toolStripSplitButton2";
            this.tbPlotType.ToolTipText = "Type of Plot";
            // 
            // tbCartesian
            // 
            this.tbCartesian.Image = global::ToyGraf.Properties.Resources.Cartesian;
            this.tbCartesian.ImageTransparentColor = System.Drawing.Color.White;
            this.tbCartesian.Name = "tbCartesian";
            this.tbCartesian.Size = new System.Drawing.Size(123, 22);
            this.tbCartesian.Text = "Cartesian";
            // 
            // tbPolar
            // 
            this.tbPolar.Image = global::ToyGraf.Properties.Resources.Polar;
            this.tbPolar.ImageTransparentColor = System.Drawing.Color.White;
            this.tbPolar.Name = "tbPolar";
            this.tbPolar.Size = new System.Drawing.Size(123, 22);
            this.tbPolar.Text = "Polar";
            // 
            // tbProperties
            // 
            this.tbProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbProperties.Image = global::ToyGraf.Properties.Resources.gear_16xLG;
            this.tbProperties.ImageTransparentColor = System.Drawing.Color.White;
            this.tbProperties.Name = "tbProperties";
            this.tbProperties.Size = new System.Drawing.Size(31, 20);
            this.tbProperties.ToolTipText = "Graph properties (F3)";
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(31, 6);
            // 
            // tbFullScreen
            // 
            this.tbFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbFullScreen.Image = global::ToyGraf.Properties.Resources.FullScreenHS;
            this.tbFullScreen.ImageTransparentColor = System.Drawing.Color.White;
            this.tbFullScreen.Name = "tbFullScreen";
            this.tbFullScreen.Size = new System.Drawing.Size(31, 20);
            this.tbFullScreen.ToolTipText = "Full screen (F11)";
            // 
            // tbLegend
            // 
            this.tbLegend.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbLegend.Image = global::ToyGraf.Properties.Resources.LegendHS;
            this.tbLegend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbLegend.Name = "tbLegend";
            this.tbLegend.Size = new System.Drawing.Size(31, 20);
            this.tbLegend.ToolTipText = "Show/Hide Legend";
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(31, 6);
            // 
            // tbTool
            // 
            this.tbTool.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbTool.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbToolArrow,
            this.tbToolCross,
            this.tbToolHand});
            this.tbTool.Image = global::ToyGraf.Properties.Resources.PointerHS;
            this.tbTool.ImageTransparentColor = System.Drawing.Color.White;
            this.tbTool.Name = "tbTool";
            this.tbTool.Size = new System.Drawing.Size(31, 20);
            this.tbTool.ToolTipText = "Select Tool";
            // 
            // tbToolArrow
            // 
            this.tbToolArrow.Image = global::ToyGraf.Properties.Resources.PointerHS;
            this.tbToolArrow.Name = "tbToolArrow";
            this.tbToolArrow.Size = new System.Drawing.Size(112, 22);
            this.tbToolArrow.Text = "&Pointer";
            // 
            // tbToolCross
            // 
            this.tbToolCross.Image = global::ToyGraf.Properties.Resources.Cross;
            this.tbToolCross.ImageTransparentColor = System.Drawing.Color.White;
            this.tbToolCross.Name = "tbToolCross";
            this.tbToolCross.Size = new System.Drawing.Size(112, 22);
            this.tbToolCross.Text = "&Cross";
            // 
            // tbToolHand
            // 
            this.tbToolHand.Image = global::ToyGraf.Properties.Resources.Hand;
            this.tbToolHand.ImageTransparentColor = System.Drawing.Color.White;
            this.tbToolHand.Name = "tbToolHand";
            this.tbToolHand.Size = new System.Drawing.Size(112, 22);
            this.tbToolHand.Text = "&Grab";
            // 
            // MainMenu
            // 
            this.MainMenu.AutoSize = false;
            this.MainMenu.Dock = System.Windows.Forms.DockStyle.None;
            this.MainMenu.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.GraphMenu,
            this.ViewMenu,
            this.TimeMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Padding = new System.Windows.Forms.Padding(7, 2, 0, 2);
            this.MainMenu.Size = new System.Drawing.Size(784, 24);
            this.MainMenu.TabIndex = 2;
            this.MainMenu.Text = "Main Menu";
            // 
            // FileMenu
            // 
            this.FileMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNew,
            this.FileOpen,
            this.FileReopen,
            this.toolStripMenuItem2,
            this.FileSave,
            this.FileSaveAs,
            this.toolStripMenuItem1,
            this.FileClose,
            this.FileExit});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // FileNew
            // 
            this.FileNew.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileNewEmptyGraph,
            this.FileNewFromTemplate});
            this.FileNew.Image = global::ToyGraf.Properties.Resources.NewDocumentHS;
            this.FileNew.ImageTransparentColor = System.Drawing.Color.White;
            this.FileNew.Name = "FileNew";
            this.FileNew.Size = new System.Drawing.Size(154, 22);
            this.FileNew.Text = "&New";
            // 
            // FileNewEmptyGraph
            // 
            this.FileNewEmptyGraph.ImageTransparentColor = System.Drawing.Color.White;
            this.FileNewEmptyGraph.Name = "FileNewEmptyGraph";
            this.FileNewEmptyGraph.ShortcutKeyDisplayString = "^N";
            this.FileNewEmptyGraph.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNewEmptyGraph.Size = new System.Drawing.Size(167, 22);
            this.FileNewEmptyGraph.Text = "&Empty Graph";
            // 
            // FileNewFromTemplate
            // 
            this.FileNewFromTemplate.ImageTransparentColor = System.Drawing.Color.White;
            this.FileNewFromTemplate.Name = "FileNewFromTemplate";
            this.FileNewFromTemplate.Size = new System.Drawing.Size(167, 22);
            this.FileNewFromTemplate.Text = "From &Template...";
            // 
            // FileOpen
            // 
            this.FileOpen.Image = global::ToyGraf.Properties.Resources.OpenFile;
            this.FileOpen.ImageTransparentColor = System.Drawing.Color.White;
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.ShortcutKeyDisplayString = "^O";
            this.FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpen.Size = new System.Drawing.Size(154, 22);
            this.FileOpen.Text = "&Open...";
            // 
            // FileReopen
            // 
            this.FileReopen.Name = "FileReopen";
            this.FileReopen.Size = new System.Drawing.Size(154, 22);
            this.FileReopen.Text = "&Reopen";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(151, 6);
            // 
            // FileSave
            // 
            this.FileSave.Image = global::ToyGraf.Properties.Resources.saveHS;
            this.FileSave.ImageTransparentColor = System.Drawing.Color.White;
            this.FileSave.Name = "FileSave";
            this.FileSave.ShortcutKeyDisplayString = "^S";
            this.FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSave.Size = new System.Drawing.Size(154, 22);
            this.FileSave.Text = "&Save";
            // 
            // FileSaveAs
            // 
            this.FileSaveAs.Name = "FileSaveAs";
            this.FileSaveAs.Size = new System.Drawing.Size(154, 22);
            this.FileSaveAs.Text = "Save &As...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(151, 6);
            // 
            // FileClose
            // 
            this.FileClose.Name = "FileClose";
            this.FileClose.ShortcutKeyDisplayString = "^F4";
            this.FileClose.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.F4)));
            this.FileClose.Size = new System.Drawing.Size(154, 22);
            this.FileClose.Text = "&Close";
            // 
            // FileExit
            // 
            this.FileExit.Name = "FileExit";
            this.FileExit.Size = new System.Drawing.Size(154, 22);
            this.FileExit.Text = "Close All && E&xit";
            // 
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditUndo,
            this.EditRedo,
            this.toolStripMenuItem3,
            this.EditCut,
            this.EditCopy,
            this.EditPaste,
            this.EditDelete,
            this.toolStripMenuItem9,
            this.EditSelectAll,
            this.EditInvertSelection,
            this.toolStripMenuItem10,
            this.EditOptions});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(39, 20);
            this.EditMenu.Text = "&Edit";
            // 
            // EditUndo
            // 
            this.EditUndo.Enabled = false;
            this.EditUndo.Image = global::ToyGraf.Properties.Resources.Edit_UndoHS;
            this.EditUndo.ImageTransparentColor = System.Drawing.Color.White;
            this.EditUndo.Name = "EditUndo";
            this.EditUndo.ShortcutKeyDisplayString = "^Z";
            this.EditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.EditUndo.Size = new System.Drawing.Size(155, 22);
            this.EditUndo.Text = "&Undo";
            // 
            // EditRedo
            // 
            this.EditRedo.Enabled = false;
            this.EditRedo.Image = global::ToyGraf.Properties.Resources.Edit_RedoHS;
            this.EditRedo.ImageTransparentColor = System.Drawing.Color.White;
            this.EditRedo.Name = "EditRedo";
            this.EditRedo.ShortcutKeyDisplayString = "^Y";
            this.EditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.EditRedo.Size = new System.Drawing.Size(155, 22);
            this.EditRedo.Text = "&Redo";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 6);
            // 
            // EditCut
            // 
            this.EditCut.Enabled = false;
            this.EditCut.Image = global::ToyGraf.Properties.Resources.CutHS;
            this.EditCut.ImageTransparentColor = System.Drawing.Color.White;
            this.EditCut.Name = "EditCut";
            this.EditCut.ShortcutKeyDisplayString = "^X";
            this.EditCut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.X)));
            this.EditCut.Size = new System.Drawing.Size(155, 22);
            this.EditCut.Text = "Cu&t";
            // 
            // EditCopy
            // 
            this.EditCopy.Enabled = false;
            this.EditCopy.Image = global::ToyGraf.Properties.Resources.CopyHS;
            this.EditCopy.ImageTransparentColor = System.Drawing.Color.White;
            this.EditCopy.Name = "EditCopy";
            this.EditCopy.ShortcutKeyDisplayString = "^C";
            this.EditCopy.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.C)));
            this.EditCopy.Size = new System.Drawing.Size(155, 22);
            this.EditCopy.Text = "&Copy";
            // 
            // EditPaste
            // 
            this.EditPaste.Enabled = false;
            this.EditPaste.Image = global::ToyGraf.Properties.Resources.PasteHS;
            this.EditPaste.ImageTransparentColor = System.Drawing.Color.White;
            this.EditPaste.Name = "EditPaste";
            this.EditPaste.ShortcutKeyDisplayString = "^V";
            this.EditPaste.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.V)));
            this.EditPaste.Size = new System.Drawing.Size(155, 22);
            this.EditPaste.Text = "&Paste";
            // 
            // EditDelete
            // 
            this.EditDelete.Enabled = false;
            this.EditDelete.Image = global::ToyGraf.Properties.Resources.Delete;
            this.EditDelete.ImageTransparentColor = System.Drawing.Color.White;
            this.EditDelete.Name = "EditDelete";
            this.EditDelete.Size = new System.Drawing.Size(155, 22);
            this.EditDelete.Text = "&Delete";
            // 
            // toolStripMenuItem9
            // 
            this.toolStripMenuItem9.Name = "toolStripMenuItem9";
            this.toolStripMenuItem9.Size = new System.Drawing.Size(152, 6);
            // 
            // EditSelectAll
            // 
            this.EditSelectAll.Name = "EditSelectAll";
            this.EditSelectAll.ShortcutKeyDisplayString = "^A";
            this.EditSelectAll.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.EditSelectAll.Size = new System.Drawing.Size(155, 22);
            this.EditSelectAll.Text = "Select &All";
            // 
            // EditInvertSelection
            // 
            this.EditInvertSelection.Name = "EditInvertSelection";
            this.EditInvertSelection.Size = new System.Drawing.Size(155, 22);
            this.EditInvertSelection.Text = "&Invert Selection";
            // 
            // toolStripMenuItem10
            // 
            this.toolStripMenuItem10.Name = "toolStripMenuItem10";
            this.toolStripMenuItem10.Size = new System.Drawing.Size(152, 6);
            // 
            // EditOptions
            // 
            this.EditOptions.Name = "EditOptions";
            this.EditOptions.Size = new System.Drawing.Size(155, 22);
            this.EditOptions.Text = "&Options...";
            // 
            // GraphMenu
            // 
            this.GraphMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GraphAddNewFunction,
            this.GraphType,
            this.GraphProperties});
            this.GraphMenu.Name = "GraphMenu";
            this.GraphMenu.Size = new System.Drawing.Size(51, 20);
            this.GraphMenu.Text = "&Graph";
            // 
            // GraphAddNewFunction
            // 
            this.GraphAddNewFunction.Image = global::ToyGraf.Properties.Resources.action_add_16xLG;
            this.GraphAddNewFunction.ImageTransparentColor = System.Drawing.Color.White;
            this.GraphAddNewFunction.Name = "GraphAddNewFunction";
            this.GraphAddNewFunction.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.GraphAddNewFunction.Size = new System.Drawing.Size(201, 22);
            this.GraphAddNewFunction.Text = "&Add a New Function";
            // 
            // GraphType
            // 
            this.GraphType.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GraphTypeCartesian,
            this.GraphTypePolar});
            this.GraphType.Name = "GraphType";
            this.GraphType.Size = new System.Drawing.Size(201, 22);
            this.GraphType.Text = "&Type of Plot";
            // 
            // GraphTypeCartesian
            // 
            this.GraphTypeCartesian.Image = global::ToyGraf.Properties.Resources.Cartesian;
            this.GraphTypeCartesian.ImageTransparentColor = System.Drawing.Color.White;
            this.GraphTypeCartesian.Name = "GraphTypeCartesian";
            this.GraphTypeCartesian.ShortcutKeyDisplayString = "Shift+^C";
            this.GraphTypeCartesian.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.GraphTypeCartesian.Size = new System.Drawing.Size(178, 22);
            this.GraphTypeCartesian.Text = "&Cartesian";
            // 
            // GraphTypePolar
            // 
            this.GraphTypePolar.Image = global::ToyGraf.Properties.Resources.Polar;
            this.GraphTypePolar.ImageTransparentColor = System.Drawing.Color.White;
            this.GraphTypePolar.Name = "GraphTypePolar";
            this.GraphTypePolar.ShortcutKeyDisplayString = "Shift+^P";
            this.GraphTypePolar.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.GraphTypePolar.Size = new System.Drawing.Size(178, 22);
            this.GraphTypePolar.Text = "&Polar";
            // 
            // GraphProperties
            // 
            this.GraphProperties.Image = global::ToyGraf.Properties.Resources.gear_16xLG;
            this.GraphProperties.ImageTransparentColor = System.Drawing.Color.White;
            this.GraphProperties.Name = "GraphProperties";
            this.GraphProperties.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.GraphProperties.Size = new System.Drawing.Size(201, 22);
            this.GraphProperties.Text = "&Properties...";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomMenu,
            this.ScrollMenu,
            this.toolStripMenuItem5,
            this.ViewToolbar,
            this.ViewLegend,
            this.ViewPropertyGrid,
            this.ViewTraceTable,
            this.toolStripMenuItem7,
            this.ViewCoordinatesTooltip});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.ViewMenu.Text = "&View";
            // 
            // ZoomMenu
            // 
            this.ZoomMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomIn,
            this.ZoomOut,
            this.ZoomReset,
            this.toolStripMenuItem8,
            this.ZoomFullScreen});
            this.ZoomMenu.Name = "ZoomMenu";
            this.ZoomMenu.Size = new System.Drawing.Size(183, 22);
            this.ZoomMenu.Text = "&Zoom";
            // 
            // ZoomIn
            // 
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.ShortcutKeyDisplayString = "^PgUp";
            this.ZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.PageUp)));
            this.ZoomIn.Size = new System.Drawing.Size(156, 22);
            this.ZoomIn.Text = "&In";
            this.ZoomIn.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ZoomOut
            // 
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.ShortcutKeyDisplayString = "^PgDn";
            this.ZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Next)));
            this.ZoomOut.Size = new System.Drawing.Size(156, 22);
            this.ZoomOut.Text = "&Out";
            this.ZoomOut.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ZoomReset
            // 
            this.ZoomReset.Name = "ZoomReset";
            this.ZoomReset.ShortcutKeyDisplayString = "^End";
            this.ZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)));
            this.ZoomReset.Size = new System.Drawing.Size(156, 22);
            this.ZoomReset.Text = "&Reset";
            this.ZoomReset.ToolTipText = "To the viewport when the graph was created or last saved";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(153, 6);
            // 
            // ZoomFullScreen
            // 
            this.ZoomFullScreen.Image = global::ToyGraf.Properties.Resources.FullScreenHS;
            this.ZoomFullScreen.ImageTransparentColor = System.Drawing.Color.White;
            this.ZoomFullScreen.Name = "ZoomFullScreen";
            this.ZoomFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.ZoomFullScreen.Size = new System.Drawing.Size(156, 22);
            this.ZoomFullScreen.Text = "&Full Screen";
            // 
            // ScrollMenu
            // 
            this.ScrollMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ScrollLeft,
            this.ScrollRight,
            this.ScrollUp,
            this.ScrollDown,
            this.toolStripMenuItem6,
            this.ScrollCentre});
            this.ScrollMenu.Name = "ScrollMenu";
            this.ScrollMenu.Size = new System.Drawing.Size(183, 22);
            this.ScrollMenu.Text = "&Scroll";
            // 
            // ScrollLeft
            // 
            this.ScrollLeft.Name = "ScrollLeft";
            this.ScrollLeft.ShortcutKeyDisplayString = "^Left";
            this.ScrollLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.ScrollLeft.Size = new System.Drawing.Size(157, 22);
            this.ScrollLeft.Text = "&Left";
            this.ScrollLeft.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ScrollRight
            // 
            this.ScrollRight.Name = "ScrollRight";
            this.ScrollRight.ShortcutKeyDisplayString = "^Right";
            this.ScrollRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.ScrollRight.Size = new System.Drawing.Size(157, 22);
            this.ScrollRight.Text = "&Right";
            this.ScrollRight.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ScrollUp
            // 
            this.ScrollUp.Name = "ScrollUp";
            this.ScrollUp.ShortcutKeyDisplayString = "^Up";
            this.ScrollUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.ScrollUp.Size = new System.Drawing.Size(157, 22);
            this.ScrollUp.Text = "&Up";
            this.ScrollUp.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ScrollDown
            // 
            this.ScrollDown.Name = "ScrollDown";
            this.ScrollDown.ShortcutKeyDisplayString = "^Down";
            this.ScrollDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.ScrollDown.Size = new System.Drawing.Size(157, 22);
            this.ScrollDown.Text = "&Down";
            this.ScrollDown.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(154, 6);
            // 
            // ScrollCentre
            // 
            this.ScrollCentre.Name = "ScrollCentre";
            this.ScrollCentre.ShortcutKeyDisplayString = "^Home";
            this.ScrollCentre.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)));
            this.ScrollCentre.Size = new System.Drawing.Size(157, 22);
            this.ScrollCentre.Text = "&Centre";
            this.ScrollCentre.ToolTipText = "Return the origin (0,0) to the centre of the display area";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(180, 6);
            // 
            // ViewToolbar
            // 
            this.ViewToolbar.Name = "ViewToolbar";
            this.ViewToolbar.Size = new System.Drawing.Size(183, 22);
            this.ViewToolbar.Text = "&Toolbar";
            // 
            // ViewLegend
            // 
            this.ViewLegend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLegendFloat,
            this.ViewLegendHide,
            this.toolStripMenuItem4,
            this.ViewLegendTopLeft,
            this.ViewLegendTopRight,
            this.ViewLegendBottomLeft,
            this.ViewLegendBottomRight});
            this.ViewLegend.Image = global::ToyGraf.Properties.Resources.LegendHS;
            this.ViewLegend.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ViewLegend.Name = "ViewLegend";
            this.ViewLegend.Size = new System.Drawing.Size(183, 22);
            this.ViewLegend.Text = "&Legend";
            // 
            // ViewLegendFloat
            // 
            this.ViewLegendFloat.Name = "ViewLegendFloat";
            this.ViewLegendFloat.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendFloat.Text = "&Float";
            // 
            // ViewLegendHide
            // 
            this.ViewLegendHide.Name = "ViewLegendHide";
            this.ViewLegendHide.ShortcutKeyDisplayString = "";
            this.ViewLegendHide.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendHide.Text = "&Hide";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(142, 6);
            // 
            // ViewLegendTopLeft
            // 
            this.ViewLegendTopLeft.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.ViewLegendTopLeft.Name = "ViewLegendTopLeft";
            this.ViewLegendTopLeft.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendTopLeft.Tag = "";
            this.ViewLegendTopLeft.Text = "Top &Left";
            // 
            // ViewLegendTopRight
            // 
            this.ViewLegendTopRight.Name = "ViewLegendTopRight";
            this.ViewLegendTopRight.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendTopRight.Text = "Top &Right";
            // 
            // ViewLegendBottomLeft
            // 
            this.ViewLegendBottomLeft.Name = "ViewLegendBottomLeft";
            this.ViewLegendBottomLeft.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendBottomLeft.Text = "Bottom L&eft";
            // 
            // ViewLegendBottomRight
            // 
            this.ViewLegendBottomRight.Name = "ViewLegendBottomRight";
            this.ViewLegendBottomRight.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendBottomRight.Text = "Bottom R&ight";
            // 
            // ViewPropertyGrid
            // 
            this.ViewPropertyGrid.Name = "ViewPropertyGrid";
            this.ViewPropertyGrid.Size = new System.Drawing.Size(183, 22);
            this.ViewPropertyGrid.Text = "&Property Grid";
            // 
            // ViewTraceTable
            // 
            this.ViewTraceTable.Name = "ViewTraceTable";
            this.ViewTraceTable.Size = new System.Drawing.Size(183, 22);
            this.ViewTraceTable.Text = "T&race Table";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(180, 6);
            // 
            // ViewCoordinatesTooltip
            // 
            this.ViewCoordinatesTooltip.Name = "ViewCoordinatesTooltip";
            this.ViewCoordinatesTooltip.Size = new System.Drawing.Size(183, 22);
            this.ViewCoordinatesTooltip.Text = "&Co-ordinates Tooltip";
            this.ViewCoordinatesTooltip.ToolTipText = "Hide or show the x-y coordinates in a tooltip";
            // 
            // TimeMenu
            // 
            this.TimeMenu.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.TimeMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TimeDecelerate,
            this.TimeReverse,
            this.TimeStop,
            this.TimePause,
            this.TimeForward,
            this.TimeAccelerate});
            this.TimeMenu.Image = global::ToyGraf.Properties.Resources.ThinkTimenode_8848;
            this.TimeMenu.Name = "TimeMenu";
            this.TimeMenu.Size = new System.Drawing.Size(46, 20);
            this.TimeMenu.Text = "&Time";
            // 
            // TimeDecelerate
            // 
            this.TimeDecelerate.Image = global::ToyGraf.Properties.Resources.RewindHS;
            this.TimeDecelerate.ImageTransparentColor = System.Drawing.Color.White;
            this.TimeDecelerate.Name = "TimeDecelerate";
            this.TimeDecelerate.Size = new System.Drawing.Size(129, 22);
            this.TimeDecelerate.Text = "&Decelerate";
            // 
            // TimeReverse
            // 
            this.TimeReverse.Image = global::ToyGraf.Properties.Resources.BackHS;
            this.TimeReverse.ImageTransparentColor = System.Drawing.Color.White;
            this.TimeReverse.Name = "TimeReverse";
            this.TimeReverse.Size = new System.Drawing.Size(129, 22);
            this.TimeReverse.Text = "&Reverse";
            // 
            // TimeStop
            // 
            this.TimeStop.Image = global::ToyGraf.Properties.Resources.StopHS;
            this.TimeStop.ImageTransparentColor = System.Drawing.Color.White;
            this.TimeStop.Name = "TimeStop";
            this.TimeStop.Size = new System.Drawing.Size(129, 22);
            this.TimeStop.Text = "&Stop";
            // 
            // TimePause
            // 
            this.TimePause.Image = global::ToyGraf.Properties.Resources.PauseHS;
            this.TimePause.ImageTransparentColor = System.Drawing.Color.White;
            this.TimePause.Name = "TimePause";
            this.TimePause.Size = new System.Drawing.Size(129, 22);
            this.TimePause.Text = "&Pause";
            // 
            // TimeForward
            // 
            this.TimeForward.Image = global::ToyGraf.Properties.Resources.PlayHS;
            this.TimeForward.Name = "TimeForward";
            this.TimeForward.Size = new System.Drawing.Size(129, 22);
            this.TimeForward.Text = "&Forward";
            // 
            // TimeAccelerate
            // 
            this.TimeAccelerate.Image = global::ToyGraf.Properties.Resources.FFwdHS;
            this.TimeAccelerate.ImageTransparentColor = System.Drawing.Color.White;
            this.TimeAccelerate.Name = "TimeAccelerate";
            this.TimeAccelerate.Size = new System.Drawing.Size(129, 22);
            this.TimeAccelerate.Text = "&Accelerate";
            // 
            // HelpMenu
            // 
            this.HelpMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.HelpAbout});
            this.HelpMenu.Name = "HelpMenu";
            this.HelpMenu.Size = new System.Drawing.Size(44, 20);
            this.HelpMenu.Text = "&Help";
            // 
            // HelpAbout
            // 
            this.HelpAbout.Image = global::ToyGraf.Properties.Resources._3ph16A;
            this.HelpAbout.ImageTransparentColor = System.Drawing.Color.White;
            this.HelpAbout.Name = "HelpAbout";
            this.HelpAbout.Size = new System.Drawing.Size(107, 22);
            this.HelpAbout.Text = "&About";
            this.HelpAbout.ToolTipText = "Show version information";
            // 
            // PopupLegendMenu
            // 
            this.PopupLegendMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.PopupLegendFloat,
            this.hideToolStripMenuItem,
            this.toolStripMenuItem15,
            this.toolStripMenuItem11,
            this.toolStripMenuItem12,
            this.toolStripMenuItem13,
            this.toolStripMenuItem14});
            this.PopupLegendMenu.Name = "PopupLegendMenu";
            this.PopupLegendMenu.Size = new System.Drawing.Size(146, 142);
            // 
            // PopupLegendFloat
            // 
            this.PopupLegendFloat.Name = "PopupLegendFloat";
            this.PopupLegendFloat.Size = new System.Drawing.Size(145, 22);
            this.PopupLegendFloat.Text = "&Float";
            // 
            // hideToolStripMenuItem
            // 
            this.hideToolStripMenuItem.Name = "hideToolStripMenuItem";
            this.hideToolStripMenuItem.Size = new System.Drawing.Size(145, 22);
            this.hideToolStripMenuItem.Text = "&Hide";
            // 
            // toolStripMenuItem15
            // 
            this.toolStripMenuItem15.Name = "toolStripMenuItem15";
            this.toolStripMenuItem15.Size = new System.Drawing.Size(142, 6);
            // 
            // toolStripMenuItem11
            // 
            this.toolStripMenuItem11.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.toolStripMenuItem11.Name = "toolStripMenuItem11";
            this.toolStripMenuItem11.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem11.Tag = "";
            this.toolStripMenuItem11.Text = "Top &Left";
            // 
            // toolStripMenuItem12
            // 
            this.toolStripMenuItem12.Name = "toolStripMenuItem12";
            this.toolStripMenuItem12.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem12.Text = "Top &Right";
            // 
            // toolStripMenuItem13
            // 
            this.toolStripMenuItem13.Name = "toolStripMenuItem13";
            this.toolStripMenuItem13.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem13.Text = "Bottom L&eft";
            // 
            // toolStripMenuItem14
            // 
            this.toolStripMenuItem14.Name = "toolStripMenuItem14";
            this.toolStripMenuItem14.Size = new System.Drawing.Size(145, 22);
            this.toolStripMenuItem14.Text = "Bottom R&ight";
            // 
            // GraphForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(784, 561);
            this.ContextMenuStrip = this.PopupMenu;
            this.Controls.Add(this.ToolStripContainer);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "GraphForm";
            this.Text = "ToyGraf";
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.PopupPropertyGridMenu.ResumeLayout(false);
            this.SplitContainer1.Panel1.ResumeLayout(false);
            this.SplitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer1)).EndInit();
            this.SplitContainer1.ResumeLayout(false);
            this.SplitContainer2.Panel1.ResumeLayout(false);
            this.SplitContainer2.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer2)).EndInit();
            this.SplitContainer2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.TraceTable)).EndInit();
            this.PopupTraceTableMenu.ResumeLayout(false);
            this.ToolStripContainer.BottomToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer.BottomToolStripPanel.PerformLayout();
            this.ToolStripContainer.ContentPanel.ResumeLayout(false);
            this.ToolStripContainer.LeftToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer.LeftToolStripPanel.PerformLayout();
            this.ToolStripContainer.TopToolStripPanel.ResumeLayout(false);
            this.ToolStripContainer.ResumeLayout(false);
            this.ToolStripContainer.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.PopupLegendMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        internal System.Windows.Forms.ToolTip ToolTip;
        internal System.Windows.Forms.ContextMenuStrip PopupMenu;
        internal System.Windows.Forms.ErrorProvider ErrorProvider;
        internal System.Windows.Forms.Panel ClientPanel;
        internal System.Windows.Forms.PictureBox PictureBox;
        internal System.Windows.Forms.Panel LegendPanel;
        internal System.Windows.Forms.OpenFileDialog TextureDialog;
        internal System.Windows.Forms.PropertyGrid PropertyGrid;
        internal System.Windows.Forms.SplitContainer SplitContainer1;
        internal System.Windows.Forms.SplitContainer SplitContainer2;
        internal System.Windows.Forms.DataGridView TraceTable;
        internal System.Windows.Forms.DataGridViewTextBoxColumn colFormula;
        internal System.Windows.Forms.DataGridViewTextBoxColumn colDerivative;
        internal System.Windows.Forms.DataGridViewTextBoxColumn colProxy;
        internal System.Windows.Forms.ToolStripContainer ToolStripContainer;
        internal Controls.TgStatusStrip StatusBar;
        internal Controls.TgToolStrip Toolbar;
        internal System.Windows.Forms.ToolStripSplitButton tbNew;
        internal System.Windows.Forms.ToolStripMenuItem tbNewEmptyGraph;
        internal System.Windows.Forms.ToolStripMenuItem tbNewFromTemplate;
        internal System.Windows.Forms.ToolStripSplitButton tbOpen;
        internal System.Windows.Forms.ToolStripButton tbSave;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        internal System.Windows.Forms.ToolStripSplitButton tbUndo;
        internal System.Windows.Forms.ToolStripSplitButton tbRedo;
        internal System.Windows.Forms.ToolStripButton tbCut;
        internal System.Windows.Forms.ToolStripButton tbCopy;
        internal System.Windows.Forms.ToolStripButton tbPaste;
        internal System.Windows.Forms.ToolStripButton tbDelete;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        internal System.Windows.Forms.ToolStripButton tbAdd;
        internal System.Windows.Forms.ToolStripSplitButton tbPlotType;
        internal System.Windows.Forms.ToolStripMenuItem tbCartesian;
        internal System.Windows.Forms.ToolStripMenuItem tbPolar;
        internal System.Windows.Forms.ToolStripButton tbProperties;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        internal System.Windows.Forms.ToolStripButton tbFullScreen;
        internal System.Windows.Forms.ToolStripSplitButton tbLegend;
        internal System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        internal System.Windows.Forms.ToolStripSplitButton tbTool;
        internal System.Windows.Forms.ToolStripMenuItem tbToolArrow;
        internal System.Windows.Forms.ToolStripMenuItem tbToolCross;
        internal System.Windows.Forms.ToolStripMenuItem tbToolHand;
        internal Controls.TgMenuStrip MainMenu;
        internal System.Windows.Forms.ToolStripMenuItem FileMenu;
        internal System.Windows.Forms.ToolStripMenuItem FileNew;
        internal System.Windows.Forms.ToolStripMenuItem FileNewEmptyGraph;
        internal System.Windows.Forms.ToolStripMenuItem FileNewFromTemplate;
        internal System.Windows.Forms.ToolStripMenuItem FileOpen;
        internal System.Windows.Forms.ToolStripMenuItem FileReopen;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        internal System.Windows.Forms.ToolStripMenuItem FileSave;
        internal System.Windows.Forms.ToolStripMenuItem FileSaveAs;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        internal System.Windows.Forms.ToolStripMenuItem FileClose;
        internal System.Windows.Forms.ToolStripMenuItem FileExit;
        internal System.Windows.Forms.ToolStripMenuItem EditMenu;
        internal System.Windows.Forms.ToolStripMenuItem EditUndo;
        internal System.Windows.Forms.ToolStripMenuItem EditRedo;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        internal System.Windows.Forms.ToolStripMenuItem EditCut;
        internal System.Windows.Forms.ToolStripMenuItem EditCopy;
        internal System.Windows.Forms.ToolStripMenuItem EditPaste;
        internal System.Windows.Forms.ToolStripMenuItem EditDelete;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem9;
        internal System.Windows.Forms.ToolStripMenuItem EditSelectAll;
        internal System.Windows.Forms.ToolStripMenuItem EditInvertSelection;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem10;
        internal System.Windows.Forms.ToolStripMenuItem EditOptions;
        internal System.Windows.Forms.ToolStripMenuItem GraphMenu;
        internal System.Windows.Forms.ToolStripMenuItem GraphAddNewFunction;
        internal System.Windows.Forms.ToolStripMenuItem GraphType;
        internal System.Windows.Forms.ToolStripMenuItem GraphTypeCartesian;
        internal System.Windows.Forms.ToolStripMenuItem GraphTypePolar;
        internal System.Windows.Forms.ToolStripMenuItem GraphProperties;
        internal System.Windows.Forms.ToolStripMenuItem ViewMenu;
        internal System.Windows.Forms.ToolStripMenuItem ZoomMenu;
        internal System.Windows.Forms.ToolStripMenuItem ZoomIn;
        internal System.Windows.Forms.ToolStripMenuItem ZoomOut;
        internal System.Windows.Forms.ToolStripMenuItem ZoomReset;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        internal System.Windows.Forms.ToolStripMenuItem ZoomFullScreen;
        internal System.Windows.Forms.ToolStripMenuItem ScrollMenu;
        internal System.Windows.Forms.ToolStripMenuItem ScrollLeft;
        internal System.Windows.Forms.ToolStripMenuItem ScrollRight;
        internal System.Windows.Forms.ToolStripMenuItem ScrollUp;
        internal System.Windows.Forms.ToolStripMenuItem ScrollDown;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        internal System.Windows.Forms.ToolStripMenuItem ScrollCentre;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegend;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegendTopLeft;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegendTopRight;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegendBottomLeft;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegendBottomRight;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegendHide;
        internal System.Windows.Forms.ToolStripMenuItem ViewToolbar;
        internal System.Windows.Forms.ToolStripMenuItem ViewPropertyGrid;
        internal System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        internal System.Windows.Forms.ToolStripMenuItem ViewCoordinatesTooltip;
        internal System.Windows.Forms.ToolStripMenuItem TimeMenu;
        internal System.Windows.Forms.ToolStripMenuItem TimeDecelerate;
        internal System.Windows.Forms.ToolStripMenuItem TimeReverse;
        internal System.Windows.Forms.ToolStripMenuItem TimeStop;
        internal System.Windows.Forms.ToolStripMenuItem TimePause;
        internal System.Windows.Forms.ToolStripMenuItem TimeForward;
        internal System.Windows.Forms.ToolStripMenuItem TimeAccelerate;
        internal System.Windows.Forms.ToolStripMenuItem HelpMenu;
        internal System.Windows.Forms.ToolStripMenuItem HelpAbout;
        internal System.Windows.Forms.ToolStripDropDownButton tbDecelerate;
        internal System.Windows.Forms.ToolStripDropDownButton tbReverse;
        internal System.Windows.Forms.ToolStripDropDownButton tbStop;
        internal System.Windows.Forms.ToolStripDropDownButton tbPause;
        internal System.Windows.Forms.ToolStripDropDownButton tbForward;
        internal System.Windows.Forms.ToolStripDropDownButton tbAccelerate;
        internal System.Windows.Forms.ToolStripStatusLabel SpeedLabel;
        internal System.Windows.Forms.ToolStripStatusLabel Tlabel;
        internal System.Windows.Forms.ToolStripStatusLabel FPSlabel;
        internal System.Windows.Forms.ToolStripStatusLabel XYlabel;
        internal System.Windows.Forms.ToolStripStatusLabel Rϴlabel;
        internal System.Windows.Forms.ToolStripStatusLabel ModifiedLabel;
        internal System.Windows.Forms.ContextMenuStrip PopupTraceTableMenu;
        internal System.Windows.Forms.ToolStripMenuItem PopupTraceTableFloat;
        internal System.Windows.Forms.ToolStripMenuItem ViewTraceTable;
        internal System.Windows.Forms.ToolStripMenuItem PopupTraceTableHide;
        internal System.Windows.Forms.ContextMenuStrip PopupPropertyGridMenu;
        internal System.Windows.Forms.ToolStripMenuItem PopupPropertyGridFloat;
        internal System.Windows.Forms.ToolStripMenuItem PopupPropertyGridHide;
        internal System.Windows.Forms.ContextMenuStrip PopupLegendMenu;
        internal System.Windows.Forms.ToolStripMenuItem PopupLegendFloat;
        internal System.Windows.Forms.ToolStripMenuItem hideToolStripMenuItem;
        internal System.Windows.Forms.ToolStripMenuItem ViewLegendFloat;
        internal System.Windows.Forms.ToolStripMenuItem toolStripMenuItem11;
        internal System.Windows.Forms.ToolStripMenuItem toolStripMenuItem12;
        internal System.Windows.Forms.ToolStripMenuItem toolStripMenuItem13;
        internal System.Windows.Forms.ToolStripMenuItem toolStripMenuItem14;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem15;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
    }
}