﻿namespace Sid.Views
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
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.ViewZoom = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScroll = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewScrollDown = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewIsotropic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
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
            // EditMenu
            // 
            this.EditMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.EditUndo,
            this.EditRedo});
            this.EditMenu.Name = "EditMenu";
            this.EditMenu.Size = new System.Drawing.Size(39, 20);
            this.EditMenu.Text = "&Edit";
            // 
            // EditUndo
            // 
            this.EditUndo.Name = "EditUndo";
            this.EditUndo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Z)));
            this.EditUndo.Size = new System.Drawing.Size(180, 22);
            this.EditUndo.Text = "&Undo";
            // 
            // EditRedo
            // 
            this.EditRedo.Name = "EditRedo";
            this.EditRedo.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Y)));
            this.EditRedo.Size = new System.Drawing.Size(180, 22);
            this.EditRedo.Text = "&Redo";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewIsotropic,
            this.toolStripMenuItem3,
            this.ViewZoom,
            this.ViewScroll});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.ViewMenu.Text = "&View";
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
            // ViewZoom
            // 
            this.ViewZoom.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewZoomIn,
            this.ViewZoomOut});
            this.ViewZoom.Name = "ViewZoom";
            this.ViewZoom.Size = new System.Drawing.Size(180, 22);
            this.ViewZoom.Text = "&Zoom";
            // 
            // ViewScroll
            // 
            this.ViewScroll.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewScrollLeft,
            this.ViewScrollRight,
            this.ViewScrollUp,
            this.ViewScrollDown});
            this.ViewScroll.Name = "ViewScroll";
            this.ViewScroll.Size = new System.Drawing.Size(180, 22);
            this.ViewScroll.Text = "&Scroll";
            // 
            // ViewZoomIn
            // 
            this.ViewZoomIn.Name = "ViewZoomIn";
            this.ViewZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.I)));
            this.ViewZoomIn.Size = new System.Drawing.Size(180, 22);
            this.ViewZoomIn.Text = "&In";
            // 
            // ViewZoomOut
            // 
            this.ViewZoomOut.Name = "ViewZoomOut";
            this.ViewZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.O)));
            this.ViewZoomOut.Size = new System.Drawing.Size(180, 22);
            this.ViewZoomOut.Text = "&Out";
            // 
            // ViewScrollLeft
            // 
            this.ViewScrollLeft.Name = "ViewScrollLeft";
            this.ViewScrollLeft.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Left)));
            this.ViewScrollLeft.Size = new System.Drawing.Size(202, 22);
            this.ViewScrollLeft.Text = "&Left";
            // 
            // ViewScrollRight
            // 
            this.ViewScrollRight.Name = "ViewScrollRight";
            this.ViewScrollRight.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Right)));
            this.ViewScrollRight.Size = new System.Drawing.Size(202, 22);
            this.ViewScrollRight.Text = "&Right";
            // 
            // ViewScrollUp
            // 
            this.ViewScrollUp.Name = "ViewScrollUp";
            this.ViewScrollUp.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Up)));
            this.ViewScrollUp.Size = new System.Drawing.Size(202, 22);
            this.ViewScrollUp.Text = "&Up";
            // 
            // ViewScrollDown
            // 
            this.ViewScrollDown.Name = "ViewScrollDown";
            this.ViewScrollDown.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.Down)));
            this.ViewScrollDown.Size = new System.Drawing.Size(202, 22);
            this.ViewScrollDown.Text = "&Down";
            // 
            // ViewIsotropic
            // 
            this.ViewIsotropic.Name = "ViewIsotropic";
            this.ViewIsotropic.Size = new System.Drawing.Size(180, 22);
            this.ViewIsotropic.Text = "&Isotropic";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(177, 6);
            // 
            // ClientPanel
            // 
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
    }
}