namespace Sid.Views
{
    partial class AppView
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
            this.ViewIsotropic = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomRight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewLegendFloating = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendNone = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMouseCoordinates = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.LegendPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.btnAddNewFunction = new System.Windows.Forms.Button();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.MainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
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
            this.FileNew.Size = new System.Drawing.Size(146, 22);
            this.FileNew.Text = "&New";
            // 
            // FileOpen
            // 
            this.FileOpen.Name = "FileOpen";
            this.FileOpen.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.FileOpen.Size = new System.Drawing.Size(146, 22);
            this.FileOpen.Text = "&Open";
            // 
            // FileReopen
            // 
            this.FileReopen.Name = "FileReopen";
            this.FileReopen.Size = new System.Drawing.Size(146, 22);
            this.FileReopen.Text = "&Reopen";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(143, 6);
            // 
            // FileSave
            // 
            this.FileSave.Name = "FileSave";
            this.FileSave.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.FileSave.Size = new System.Drawing.Size(146, 22);
            this.FileSave.Text = "&Save";
            // 
            // FileSaveAs
            // 
            this.FileSaveAs.Name = "FileSaveAs";
            this.FileSaveAs.Size = new System.Drawing.Size(146, 22);
            this.FileSaveAs.Text = "Save &As...";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(143, 6);
            // 
            // FileExit
            // 
            this.FileExit.Name = "FileExit";
            this.FileExit.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Alt | System.Windows.Forms.Keys.F4)));
            this.FileExit.Size = new System.Drawing.Size(146, 22);
            this.FileExit.Text = "E&xit";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewZoom,
            this.ViewScroll,
            this.toolStripMenuItem3,
            this.ViewIsotropic,
            this.ViewLegend,
            this.ViewMouseCoordinates,
            this.ViewFullScreen});
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
            this.ViewZoom.Size = new System.Drawing.Size(183, 22);
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
            this.ViewScroll.Size = new System.Drawing.Size(183, 22);
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
            this.toolStripMenuItem3.Size = new System.Drawing.Size(180, 6);
            // 
            // ViewIsotropic
            // 
            this.ViewIsotropic.Name = "ViewIsotropic";
            this.ViewIsotropic.Size = new System.Drawing.Size(183, 22);
            this.ViewIsotropic.Text = "&Isotropic";
            this.ViewIsotropic.ToolTipText = "The Graph is isotropic when its X and Y scales are equal";
            // 
            // ViewLegend
            // 
            this.ViewLegend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLegendTopLeft,
            this.ViewLegendTopRight,
            this.ViewLegendBottomLeft,
            this.ViewLegendBottomRight,
            this.toolStripMenuItem4,
            this.ViewLegendFloating,
            this.ViewLegendNone});
            this.ViewLegend.Name = "ViewLegend";
            this.ViewLegend.Size = new System.Drawing.Size(183, 22);
            this.ViewLegend.Text = "&Legend";
            // 
            // ViewLegendTopLeft
            // 
            this.ViewLegendTopLeft.Name = "ViewLegendTopLeft";
            this.ViewLegendTopLeft.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendTopLeft.Tag = "";
            this.ViewLegendTopLeft.Text = "Top Left";
            // 
            // ViewLegendTopRight
            // 
            this.ViewLegendTopRight.Name = "ViewLegendTopRight";
            this.ViewLegendTopRight.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendTopRight.Text = "Top Right";
            // 
            // ViewLegendBottomLeft
            // 
            this.ViewLegendBottomLeft.Name = "ViewLegendBottomLeft";
            this.ViewLegendBottomLeft.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendBottomLeft.Text = "Bottom Left";
            // 
            // ViewLegendBottomRight
            // 
            this.ViewLegendBottomRight.Name = "ViewLegendBottomRight";
            this.ViewLegendBottomRight.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendBottomRight.Text = "Bottom Right";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(142, 6);
            // 
            // ViewLegendFloating
            // 
            this.ViewLegendFloating.Name = "ViewLegendFloating";
            this.ViewLegendFloating.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendFloating.Text = "&Floating";
            // 
            // ViewLegendNone
            // 
            this.ViewLegendNone.Name = "ViewLegendNone";
            this.ViewLegendNone.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendNone.Text = "&None";
            // 
            // ViewMouseCoordinates
            // 
            this.ViewMouseCoordinates.Checked = true;
            this.ViewMouseCoordinates.CheckState = System.Windows.Forms.CheckState.Checked;
            this.ViewMouseCoordinates.Name = "ViewMouseCoordinates";
            this.ViewMouseCoordinates.Size = new System.Drawing.Size(183, 22);
            this.ViewMouseCoordinates.Text = "&Co-ordinates Tooltip";
            this.ViewMouseCoordinates.ToolTipText = "Hide or show the x-y coordinates in a tooltip";
            // 
            // ViewFullScreen
            // 
            this.ViewFullScreen.Name = "ViewFullScreen";
            this.ViewFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.ViewFullScreen.Size = new System.Drawing.Size(183, 22);
            this.ViewFullScreen.Text = "&Full Screen";
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
            this.HelpAbout.Size = new System.Drawing.Size(107, 22);
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
            this.ClientPanel.BackColor = System.Drawing.Color.LightYellow;
            this.ClientPanel.Controls.Add(this.LegendPanel);
            this.ClientPanel.Controls.Add(this.btnAddNewFunction);
            this.ClientPanel.Controls.Add(this.PictureBox);
            this.ClientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientPanel.Location = new System.Drawing.Point(0, 24);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(944, 455);
            this.ClientPanel.TabIndex = 6;
            // 
            // LegendPanel
            // 
            this.LegendPanel.AutoScroll = true;
            this.LegendPanel.AutoScrollMargin = new System.Drawing.Size(4, 4);
            this.LegendPanel.AutoSize = true;
            this.LegendPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.LegendPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LegendPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.LegendPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.LegendPanel.Location = new System.Drawing.Point(0, 0);
            this.LegendPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LegendPanel.MaximumSize = new System.Drawing.Size(480, 384);
            this.LegendPanel.Name = "LegendPanel";
            this.LegendPanel.Size = new System.Drawing.Size(2, 2);
            this.LegendPanel.TabIndex = 9;
            this.LegendPanel.WrapContents = false;
            // 
            // btnAddNewFunction
            // 
            this.btnAddNewFunction.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.btnAddNewFunction.Location = new System.Drawing.Point(484, 0);
            this.btnAddNewFunction.Margin = new System.Windows.Forms.Padding(4);
            this.btnAddNewFunction.Name = "btnAddNewFunction";
            this.btnAddNewFunction.Size = new System.Drawing.Size(137, 28);
            this.btnAddNewFunction.TabIndex = 10;
            this.btnAddNewFunction.Text = "&Add a new function";
            this.btnAddNewFunction.UseVisualStyleBackColor = true;
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(410, 180);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(244, 104);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            // 
            // AppView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.ContextMenuStrip = this.PopupMenu;
            this.Controls.Add(this.ClientPanel);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "AppView";
            this.Text = "Sid";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            this.ClientPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollCentre;
        public System.Windows.Forms.ToolStripMenuItem ViewMouseCoordinates;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        public System.Windows.Forms.ToolStripMenuItem ViewZoomReset;
        public System.Windows.Forms.ToolStripMenuItem ViewFullScreen;
        public System.Windows.Forms.StatusStrip StatusBar;
        public System.Windows.Forms.ContextMenuStrip PopupMenu;
        public System.Windows.Forms.MenuStrip MainMenu;
        public System.Windows.Forms.ErrorProvider ErrorProvider;
        public System.Windows.Forms.ToolStripMenuItem ViewLegend;
        public System.Windows.Forms.Panel ClientPanel;
        public System.Windows.Forms.Button btnAddNewFunction;
        public System.Windows.Forms.PictureBox PictureBox;
        public System.Windows.Forms.FlowLayoutPanel LegendPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendTopLeft;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendTopRight;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendBottomLeft;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendBottomRight;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendFloating;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendNone;
    }
}