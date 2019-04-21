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
            this.EditMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.EditUndo = new System.Windows.Forms.ToolStripMenuItem();
            this.EditRedo = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.EditProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewScrollCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewIsotropic = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMouseCoordinates = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.MainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.ClientPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.EditMenu,
            this.ViewMenu,
            this.HelpMenu});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(944, 24);
            this.MainMenu.TabIndex = 1;
            this.MainMenu.Text = "menuStrip1";
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
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditUndo,
            this.EditRedo,
            this.toolStripMenuItem4,
            this.EditProperties});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(39, 20);
            this.EditMenu.Text = "&Edit";
            // 
            // EditUndo
            // 
            this.EditUndo.Name = "EditUndo";
            this.EditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.EditUndo.Size = new System.Drawing.Size(155, 22);
            this.EditUndo.Text = "&Undo";
            // 
            // EditRedo
            // 
            this.EditRedo.Name = "EditRedo";
            this.EditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.EditRedo.Size = new System.Drawing.Size(155, 22);
            this.EditRedo.Text = "&Redo";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 6);
            // 
            // EditProperties
            // 
            this.EditProperties.Name = "EditProperties";
            this.EditProperties.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.EditProperties.Size = new System.Drawing.Size(155, 22);
            this.EditProperties.Text = "&Properties...";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewZoom,
            this.ViewScroll,
            this.toolStripMenuItem3,
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
            this.ViewZoomOut});
            this.ViewZoom.Name = "ViewZoom";
            this.ViewZoom.Size = new System.Drawing.Size(182, 22);
            this.ViewZoom.Text = "&Zoom";
            this.ViewZoom.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ViewZoomIn
            // 
            this.ViewZoomIn.Name = "ViewZoomIn";
            this.ViewZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.PageUp)));
            this.ViewZoomIn.Size = new System.Drawing.Size(189, 22);
            this.ViewZoomIn.Text = "&In";
            this.ViewZoomIn.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ViewZoomOut
            // 
            this.ViewZoomOut.Name = "ViewZoomOut";
            this.ViewZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Next)));
            this.ViewZoomOut.Size = new System.Drawing.Size(189, 22);
            this.ViewZoomOut.Text = "&Out";
            this.ViewZoomOut.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
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
            this.ViewScrollLeft.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Left)));
            this.ViewScrollLeft.Size = new System.Drawing.Size(208, 22);
            this.ViewScrollLeft.Text = "&Left";
            this.ViewScrollLeft.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollRight
            // 
            this.ViewScrollRight.Name = "ViewScrollRight";
            this.ViewScrollRight.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Right)));
            this.ViewScrollRight.Size = new System.Drawing.Size(208, 22);
            this.ViewScrollRight.Text = "&Right";
            this.ViewScrollRight.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollUp
            // 
            this.ViewScrollUp.Name = "ViewScrollUp";
            this.ViewScrollUp.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Up)));
            this.ViewScrollUp.Size = new System.Drawing.Size(208, 22);
            this.ViewScrollUp.Text = "&Up";
            this.ViewScrollUp.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ViewScrollDown
            // 
            this.ViewScrollDown.Name = "ViewScrollDown";
            this.ViewScrollDown.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Down)));
            this.ViewScrollDown.Size = new System.Drawing.Size(208, 22);
            this.ViewScrollDown.Text = "&Down";
            this.ViewScrollDown.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(205, 6);
            // 
            // ViewScrollCentre
            // 
            this.ViewScrollCentre.Name = "ViewScrollCentre";
            this.ViewScrollCentre.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Home)));
            this.ViewScrollCentre.Size = new System.Drawing.Size(208, 22);
            this.ViewScrollCentre.Text = "&Centre";
            this.ViewScrollCentre.ToolTipText = "Return the origin (0,0) to the centre of the display area";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(179, 6);
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
            // PictureBox
            // 
            this.PictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.PictureBox.Location = new System.Drawing.Point(0, 0);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(944, 455);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            // 
            // ClientPanel
            // 
            this.ClientPanel.BackColor = System.Drawing.SystemColors.Control;
            this.ClientPanel.Controls.Add(this.PictureBox);
            this.ClientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientPanel.Location = new System.Drawing.Point(0, 24);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(944, 455);
            this.ClientPanel.TabIndex = 4;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.Controls.Add(this.ClientPanel);
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
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem EditMenu;
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
        public System.Windows.Forms.ToolStripMenuItem EditUndo;
        public System.Windows.Forms.ToolStripMenuItem EditRedo;
        public System.Windows.Forms.ToolStripMenuItem HelpAbout;
        private System.Windows.Forms.StatusStrip StatusBar;
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
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        public System.Windows.Forms.ToolStripMenuItem EditProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem ViewScrollCentre;
        public System.Windows.Forms.ToolStripMenuItem ViewMouseCoordinates;
    }
}