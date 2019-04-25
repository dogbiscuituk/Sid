namespace Sid.Views
{
    partial class MainForm
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
            this.ToolTip = new System.Windows.Forms.ToolTip(this.components);
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.FileMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.FileNew = new System.Windows.Forms.ToolStripMenuItem();
            this.FileOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.FileReopen = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripSeparator();
            this.FileSave = new System.Windows.Forms.ToolStripMenuItem();
            this.FileSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
            this.FileExit = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewScrollCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewIsotropic = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMouseCoordinates = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.SplitContainer = new System.Windows.Forms.SplitContainer();
            this.seXmax = new System.Windows.Forms.NumericUpDown();
            this.lblXmax = new System.Windows.Forms.Label();
            this.seXmin = new System.Windows.Forms.NumericUpDown();
            this.lblXmin = new System.Windows.Forms.Label();
            this.seYmax = new System.Windows.Forms.NumericUpDown();
            this.seYmin = new System.Windows.Forms.NumericUpDown();
            this.lblYmax = new System.Windows.Forms.Label();
            this.lblYmin = new System.Windows.Forms.Label();
            this.FlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddNewFunction = new System.Windows.Forms.Button();
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ViewEditor = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).BeginInit();
            this.SplitContainer.Panel1.SuspendLayout();
            this.SplitContainer.Panel2.SuspendLayout();
            this.SplitContainer.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.ViewMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(944, 24);
            this.MainMenu.TabIndex = 1;
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
            this.FileExit});
            this.FileMenu.Name = "FileMenu";
            this.FileMenu.Size = new System.Drawing.Size(37, 20);
            this.FileMenu.Text = "&File";
            // 
            // FileNew
            // 
            this.FileNew.Name = "FileNew";
            this.FileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNew.Size = new System.Drawing.Size(180, 22);
            this.FileNew.Text = "&New";
            // 
            // FileOpen
            // 
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpen.Size = new System.Drawing.Size(180, 22);
            this.FileOpen.Text = "&Open";
            // 
            // FileReopen
            // 
            this.FileReopen.Name = "FileReopen";
            this.FileReopen.Size = new System.Drawing.Size(180, 22);
            this.FileReopen.Text = "&Reopen";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(177, 6);
            // 
            // FileSave
            // 
            this.FileSave.Name = "FileSave";
            this.FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSave.Size = new System.Drawing.Size(180, 22);
            this.FileSave.Text = "&Save";
            // 
            // FileSaveAs
            // 
            this.FileSaveAs.Name = "FileSaveAs";
            this.FileSaveAs.Size = new System.Drawing.Size(180, 22);
            this.FileSaveAs.Text = "Save &As...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(177, 6);
            // 
            // FileExit
            // 
            this.FileExit.Name = "FileExit";
            this.FileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.FileExit.Size = new System.Drawing.Size(180, 22);
            this.FileExit.Text = "E&xit";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewZoom,
            this.ViewScroll,
            this.toolStripMenuItem3,
            this.ViewEditor,
            this.ViewFullScreen,
            this.ViewIsotropic,
            this.ViewMouseCoordinates});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.ViewMenu.Text = "&View";
            // 
            // ViewZoom
            // 
            this.ViewZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewZoomIn,
            this.ViewZoomOut,
            this.toolStripMenuItem6,
            this.ViewZoomReset});
            this.ViewZoom.Name = "ViewZoom";
            this.ViewZoom.Size = new System.Drawing.Size(182, 22);
            this.ViewZoom.Text = "&Zoom";
            this.ViewZoom.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ViewZoomIn
            // 
            this.ViewZoomIn.Name = "ViewZoomIn";
            this.ViewZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.PageUp)));
            this.ViewZoomIn.Size = new System.Drawing.Size(157, 22);
            this.ViewZoomIn.Text = "&In";
            this.ViewZoomIn.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ViewZoomOut
            // 
            this.ViewZoomOut.Name = "ViewZoomOut";
            this.ViewZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Next)));
            this.ViewZoomOut.Size = new System.Drawing.Size(157, 22);
            this.ViewZoomOut.Text = "&Out";
            this.ViewZoomOut.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(154, 6);
            // 
            // ViewZoomReset
            // 
            this.ViewZoomReset.Name = "ViewZoomReset";
            this.ViewZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)));
            this.ViewZoomReset.Size = new System.Drawing.Size(157, 22);
            this.ViewZoomReset.Text = "&Reset";
            this.ViewZoomReset.ToolTipText = "To the viewport when the graph was created or last saved";
            // 
            // ViewScroll
            // 
            this.ViewScroll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewScrollLeft,
            this.ViewScrollRight,
            this.ViewScrollUp,
            this.ViewScrollDown,
            this.toolStripMenuItem5,
            this.ViewScrollCentre});
            this.ViewScroll.Name = "ViewScroll";
            this.ViewScroll.Size = new System.Drawing.Size(182, 22);
            this.ViewScroll.Text = "&Scroll";
            this.ViewScroll.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollLeft
            // 
            this.ViewScrollLeft.Name = "ViewScrollLeft";
            this.ViewScrollLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.ViewScrollLeft.Size = new System.Drawing.Size(176, 22);
            this.ViewScrollLeft.Text = "&Left";
            this.ViewScrollLeft.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollRight
            // 
            this.ViewScrollRight.Name = "ViewScrollRight";
            this.ViewScrollRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.ViewScrollRight.Size = new System.Drawing.Size(176, 22);
            this.ViewScrollRight.Text = "&Right";
            this.ViewScrollRight.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollUp
            // 
            this.ViewScrollUp.Name = "ViewScrollUp";
            this.ViewScrollUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.ViewScrollUp.Size = new System.Drawing.Size(176, 22);
            this.ViewScrollUp.Text = "&Up";
            this.ViewScrollUp.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollDown
            // 
            this.ViewScrollDown.Name = "ViewScrollDown";
            this.ViewScrollDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.ViewScrollDown.Size = new System.Drawing.Size(176, 22);
            this.ViewScrollDown.Text = "&Down";
            this.ViewScrollDown.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(173, 6);
            // 
            // ViewScrollCentre
            // 
            this.ViewScrollCentre.Name = "ViewScrollCentre";
            this.ViewScrollCentre.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)));
            this.ViewScrollCentre.Size = new System.Drawing.Size(176, 22);
            this.ViewScrollCentre.Text = "&Centre";
            this.ViewScrollCentre.ToolTipText = "Return the origin (0,0) to the centre of the display area";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(179, 6);
            // 
            // ViewFullScreen
            // 
            this.ViewFullScreen.Name = "ViewFullScreen";
            this.ViewFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.ViewFullScreen.Size = new System.Drawing.Size(182, 22);
            this.ViewFullScreen.Text = "&Full Screen";
            // 
            // ViewIsotropic
            // 
            this.ViewIsotropic.Name = "ViewIsotropic";
            this.ViewIsotropic.Size = new System.Drawing.Size(182, 22);
            this.ViewIsotropic.Text = "&Isotropic";
            this.ViewIsotropic.ToolTipText = "The Graph is isotropic when its X and Y scales are equal";
            // 
            // ViewMouseCoordinates
            // 
            this.ViewMouseCoordinates.Checked = true;
            this.ViewMouseCoordinates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewMouseCoordinates.Name = "ViewMouseCoordinates";
            this.ViewMouseCoordinates.Size = new System.Drawing.Size(182, 22);
            this.ViewMouseCoordinates.Text = "&Mouse Co-ordinates";
            this.ViewMouseCoordinates.ToolTipText = "Hide or show the x-y coordinates in a tooltip";
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
            this.HelpAbout.Name = "HelpAbout";
            this.HelpAbout.Size = new System.Drawing.Size(180, 22);
            this.HelpAbout.Text = "&About";
            this.HelpAbout.ToolTipText = "Show version information";
            // 
            // StatusBar
            // 
            this.StatusBar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ModifiedLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 479);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(944, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "statusStrip1";
            // 
            // ModifiedLabel
            // 
            this.ModifiedLabel.Name = "ModifiedLabel";
            this.ModifiedLabel.Size = new System.Drawing.Size(55, 17);
            this.ModifiedLabel.Text = "Modified";
            this.ModifiedLabel.Visible = false;
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 3);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(244, 104);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            // 
            // ClientPanel
            // 
            this.ClientPanel.BackColor = System.Drawing.Color.LightYellow;
            this.ClientPanel.Controls.Add(this.PictureBox);
            this.ClientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientPanel.Location = new System.Drawing.Point(0, 0);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(531, 455);
            this.ClientPanel.TabIndex = 4;
            // 
            // PopupMenu
            // 
            this.PopupMenu.Name = "PopupMenu";
            this.PopupMenu.Size = new System.Drawing.Size(61, 4);
            this.PopupMenu.Text = "Popup Menu";
            // 
            // SplitContainer
            // 
            this.SplitContainer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SplitContainer.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
            this.SplitContainer.IsSplitterFixed = true;
            this.SplitContainer.Location = new System.Drawing.Point(0, 24);
            this.SplitContainer.Name = "SplitContainer";
            // 
            // SplitContainer.Panel1
            // 
            this.SplitContainer.Panel1.Controls.Add(this.ClientPanel);
            // 
            // SplitContainer.Panel2
            // 
            this.SplitContainer.Panel2.Controls.Add(this.btnAddNewFunction);
            this.SplitContainer.Panel2.Controls.Add(this.FlowLayoutPanel);
            this.SplitContainer.Panel2.Controls.Add(this.seYmax);
            this.SplitContainer.Panel2.Controls.Add(this.seYmin);
            this.SplitContainer.Panel2.Controls.Add(this.lblYmax);
            this.SplitContainer.Panel2.Controls.Add(this.lblYmin);
            this.SplitContainer.Panel2.Controls.Add(this.seXmax);
            this.SplitContainer.Panel2.Controls.Add(this.lblXmax);
            this.SplitContainer.Panel2.Controls.Add(this.seXmin);
            this.SplitContainer.Panel2.Controls.Add(this.lblXmin);
            this.SplitContainer.Size = new System.Drawing.Size(944, 455);
            this.SplitContainer.SplitterDistance = 531;
            this.SplitContainer.TabIndex = 5;
            // 
            // seXmax
            // 
            this.seXmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seXmax.DecimalPlaces = 4;
            this.seXmax.Location = new System.Drawing.Point(211, 15);
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
            this.seXmax.Size = new System.Drawing.Size(85, 16);
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
            this.lblXmax.Location = new System.Drawing.Point(159, 14);
            this.lblXmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXmax.Name = "lblXmax";
            this.lblXmax.Size = new System.Drawing.Size(36, 13);
            this.lblXmax.TabIndex = 2;
            this.lblXmax.Text = "X max";
            // 
            // seXmin
            // 
            this.seXmin.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seXmin.DecimalPlaces = 4;
            this.seXmin.Location = new System.Drawing.Point(66, 15);
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
            this.seXmin.Size = new System.Drawing.Size(85, 16);
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
            this.lblXmin.Location = new System.Drawing.Point(18, 14);
            this.lblXmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblXmin.Name = "lblXmin";
            this.lblXmin.Size = new System.Drawing.Size(33, 13);
            this.lblXmin.TabIndex = 1;
            this.lblXmin.Text = "X min";
            // 
            // seYmax
            // 
            this.seYmax.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.seYmax.DecimalPlaces = 4;
            this.seYmax.Location = new System.Drawing.Point(212, 39);
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
            this.seYmax.Size = new System.Drawing.Size(85, 16);
            this.seYmax.TabIndex = 8;
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
            this.seYmin.Location = new System.Drawing.Point(66, 39);
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
            this.seYmin.Size = new System.Drawing.Size(85, 16);
            this.seYmin.TabIndex = 7;
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
            this.lblYmax.Location = new System.Drawing.Point(159, 38);
            this.lblYmax.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYmax.Name = "lblYmax";
            this.lblYmax.Size = new System.Drawing.Size(36, 13);
            this.lblYmax.TabIndex = 6;
            this.lblYmax.Text = "Y max";
            // 
            // lblYmin
            // 
            this.lblYmin.AutoSize = true;
            this.lblYmin.Location = new System.Drawing.Point(17, 38);
            this.lblYmin.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.lblYmin.Name = "lblYmin";
            this.lblYmin.Size = new System.Drawing.Size(33, 13);
            this.lblYmin.TabIndex = 5;
            this.lblYmin.Text = "Y min";
            // 
            // FlowLayoutPanel
            // 
            this.FlowLayoutPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.FlowLayoutPanel.AutoScroll = true;
            this.FlowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.FlowLayoutPanel.Location = new System.Drawing.Point(10, 59);
            this.FlowLayoutPanel.Margin = new System.Windows.Forms.Padding(0);
            this.FlowLayoutPanel.Name = "FlowLayoutPanel";
            this.FlowLayoutPanel.Size = new System.Drawing.Size(390, 344);
            this.FlowLayoutPanel.TabIndex = 9;
            // 
            // btnAddNewFunction
            // 
            this.btnAddNewFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNewFunction.Location = new System.Drawing.Point(21, 407);
            this.btnAddNewFunction.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewFunction.Name = "btnAddNewFunction";
            this.btnAddNewFunction.Size = new System.Drawing.Size(137, 28);
            this.btnAddNewFunction.TabIndex = 10;
            this.btnAddNewFunction.Text = "&Add a new function";
            this.btnAddNewFunction.UseVisualStyleBackColor = true;
            // 
            // ErrorProvider
            // 
            this.ErrorProvider.ContainerControl = this;
            // 
            // ViewEditor
            // 
            this.ViewEditor.Checked = true;
            this.ViewEditor.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewEditor.Name = "ViewEditor";
            this.ViewEditor.ShortcutKeys = System.Windows.Forms.Keys.F12;
            this.ViewEditor.Size = new System.Drawing.Size(182, 22);
            this.ViewEditor.Text = "&Editor";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.ContextMenuStrip = this.PopupMenu;
            this.Controls.Add(this.SplitContainer);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.Name = "MainForm";
            this.Text = "Sid";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            this.SplitContainer.Panel1.ResumeLayout(false);
            this.SplitContainer.Panel2.ResumeLayout(false);
            this.SplitContainer.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.SplitContainer)).EndInit();
            this.SplitContainer.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.seXmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seXmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.seYmin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ToolStripMenuItem HelpMenu;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem2;
        public System.Windows.Forms.ToolStripMenuItem FileSave;
        public System.Windows.Forms.ToolStripMenuItem FileSaveAs;
        public System.Windows.Forms.ToolStripMenuItem FileReopen;
        public System.Windows.Forms.ToolStripMenuItem FileMenu;
        public System.Windows.Forms.ToolStripMenuItem FileExit;
        public System.Windows.Forms.ToolStripMenuItem FileNew;
        public System.Windows.Forms.ToolStripMenuItem FileOpen;
        public System.Windows.Forms.ToolStripMenuItem HelpAbout;
        public System.Windows.Forms.ToolStripStatusLabel ModifiedLabel;
        public System.Windows.Forms.ToolTip ToolTip;
        public System.Windows.Forms.PictureBox PictureBox;
        private System.Windows.Forms.ToolStripMenuItem ViewZoom;
        private System.Windows.Forms.ToolStripMenuItem ViewScroll;
        public System.Windows.Forms.ToolStripMenuItem ViewZoomIn;
        public System.Windows.Forms.ToolStripMenuItem ViewZoomOut;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollLeft;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollRight;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollUp;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollDown;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        public System.Windows.Forms.ToolStripMenuItem ViewIsotropic;
        public System.Windows.Forms.ToolStripMenuItem ViewMenu;
        public System.Windows.Forms.Panel ClientPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollCentre;
        public System.Windows.Forms.ToolStripMenuItem ViewMouseCoordinates;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        public System.Windows.Forms.ToolStripMenuItem ViewZoomReset;
        public System.Windows.Forms.ToolStripMenuItem ViewFullScreen;
        public System.Windows.Forms.StatusStrip StatusBar;
        public System.Windows.Forms.ContextMenuStrip PopupMenu;
        public System.Windows.Forms.MenuStrip MainMenu;
        public System.Windows.Forms.NumericUpDown seXmax;
        private System.Windows.Forms.Label lblXmax;
        public System.Windows.Forms.NumericUpDown seXmin;
        private System.Windows.Forms.Label lblXmin;
        public System.Windows.Forms.NumericUpDown seYmax;
        public System.Windows.Forms.NumericUpDown seYmin;
        private System.Windows.Forms.Label lblYmax;
        private System.Windows.Forms.Label lblYmin;
        public System.Windows.Forms.FlowLayoutPanel FlowLayoutPanel;
        public System.Windows.Forms.Button btnAddNewFunction;
        public System.Windows.Forms.ErrorProvider ErrorProvider;
        public System.Windows.Forms.ToolStripMenuItem ViewEditor;
        public System.Windows.Forms.SplitContainer SplitContainer;
    }
}