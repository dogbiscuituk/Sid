namespace Sid.Views
{
    partial class AppForm
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
            this.GraphMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphAddNewFunction = new System.Windows.Forms.ToolStripMenuItem();
            this.GraphProperties = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomRight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewLegendHide = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewCoordinatesTooltip = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem5 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewMathboard = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomIn = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomOut = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomReset = new System.Windows.Forms.ToolStripMenuItem();
            this.ZoomIsotropic = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem8 = new System.Windows.Forms.ToolStripSeparator();
            this.ZoomFullScreen = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollUp = new System.Windows.Forms.ToolStripMenuItem();
            this.ScrollDown = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem6 = new System.Windows.Forms.ToolStripSeparator();
            this.ScrollCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerRunPause = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerReset = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripSeparator();
            this.TimerInterval = new System.Windows.Forms.ToolStripComboBox();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.XYlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.LegendPanel = new System.Windows.Forms.Panel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.Tlabel = new System.Windows.Forms.ToolStripStatusLabel();
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
            this.GraphMenu,
            this.ViewMenu,
            this.ZoomMenu,
            this.ScrollMenu,
            this.TimerMenu,
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
            // GraphMenu
            // 
            this.GraphMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.GraphAddNewFunction,
            this.GraphProperties});
            this.GraphMenu.Name = "GraphMenu";
            this.GraphMenu.Size = new System.Drawing.Size(51, 20);
            this.GraphMenu.Text = "&Graph";
            // 
            // GraphAddNewFunction
            // 
            this.GraphAddNewFunction.Name = "GraphAddNewFunction";
            this.GraphAddNewFunction.ShortcutKeys = System.Windows.Forms.Keys.F2;
            this.GraphAddNewFunction.Size = new System.Drawing.Size(201, 22);
            this.GraphAddNewFunction.Text = "&Add a New Function";
            // 
            // GraphProperties
            // 
            this.GraphProperties.Name = "GraphProperties";
            this.GraphProperties.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.GraphProperties.Size = new System.Drawing.Size(201, 22);
            this.GraphProperties.Text = "&Properties...";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLegend,
            this.ViewCoordinatesTooltip,
            this.toolStripMenuItem5,
            this.ViewMathboard});
            this.ViewMenu.Name = "ViewMenu";
            this.ViewMenu.Size = new System.Drawing.Size(44, 20);
            this.ViewMenu.Text = "&View";
            // 
            // ViewLegend
            // 
            this.ViewLegend.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ViewLegendTopLeft,
            this.ViewLegendTopRight,
            this.ViewLegendBottomLeft,
            this.ViewLegendBottomRight,
            this.toolStripMenuItem4,
            this.ViewLegendHide});
            this.ViewLegend.Name = "ViewLegend";
            this.ViewLegend.Size = new System.Drawing.Size(190, 22);
            this.ViewLegend.Text = "&Legend";
            // 
            // ViewLegendTopLeft
            // 
            this.ViewLegendTopLeft.Name = "ViewLegendTopLeft";
            this.ViewLegendTopLeft.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendTopLeft.Tag = "";
            this.ViewLegendTopLeft.Text = "&Top Left";
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
            this.ViewLegendBottomLeft.Text = "&Bottom Left";
            // 
            // ViewLegendBottomRight
            // 
            this.ViewLegendBottomRight.Name = "ViewLegendBottomRight";
            this.ViewLegendBottomRight.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendBottomRight.Text = "Botto&m Right";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(142, 6);
            // 
            // ViewLegendHide
            // 
            this.ViewLegendHide.Name = "ViewLegendHide";
            this.ViewLegendHide.Size = new System.Drawing.Size(145, 22);
            this.ViewLegendHide.Text = "&Hide";
            // 
            // ViewCoordinatesTooltip
            // 
            this.ViewCoordinatesTooltip.Name = "ViewCoordinatesTooltip";
            this.ViewCoordinatesTooltip.Size = new System.Drawing.Size(190, 22);
            this.ViewCoordinatesTooltip.Text = "&Co-ordinates Tooltip";
            this.ViewCoordinatesTooltip.ToolTipText = "Hide or show the x-y coordinates in a tooltip";
            // 
            // toolStripMenuItem5
            // 
            this.toolStripMenuItem5.Name = "toolStripMenuItem5";
            this.toolStripMenuItem5.Size = new System.Drawing.Size(187, 6);
            // 
            // ViewMathboard
            // 
            this.ViewMathboard.Name = "ViewMathboard";
            this.ViewMathboard.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.ViewMathboard.Size = new System.Drawing.Size(183, 22);
            this.ViewMathboard.Text = "&Mathboard";
            // 
            // ZoomMenu
            // 
            this.ZoomMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomIn,
            this.ZoomOut,
            this.toolStripMenuItem7,
            this.ZoomReset,
            this.ZoomIsotropic,
            this.toolStripMenuItem8,
            this.ZoomFullScreen});
            this.ZoomMenu.Name = "ZoomMenu";
            this.ZoomMenu.Size = new System.Drawing.Size(51, 20);
            this.ZoomMenu.Text = "&Zoom";
            // 
            // ZoomIn
            // 
            this.ZoomIn.Name = "ZoomIn";
            this.ZoomIn.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.PageUp)));
            this.ZoomIn.Size = new System.Drawing.Size(157, 22);
            this.ZoomIn.Text = "&In";
            this.ZoomIn.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // ZoomOut
            // 
            this.ZoomOut.Name = "ZoomOut";
            this.ZoomOut.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Next)));
            this.ZoomOut.Size = new System.Drawing.Size(157, 22);
            this.ZoomOut.Text = "&Out";
            this.ZoomOut.ToolTipText = "Graphs can also be zoomed using the mouse wheel";
            // 
            // toolStripMenuItem7
            // 
            this.toolStripMenuItem7.Name = "toolStripMenuItem7";
            this.toolStripMenuItem7.Size = new System.Drawing.Size(154, 6);
            // 
            // ZoomReset
            // 
            this.ZoomReset.Name = "ZoomReset";
            this.ZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)));
            this.ZoomReset.Size = new System.Drawing.Size(157, 22);
            this.ZoomReset.Text = "&Reset";
            this.ZoomReset.ToolTipText = "To the viewport when the graph was created or last saved";
            // 
            // ZoomIsotropic
            // 
            this.ZoomIsotropic.Name = "ZoomIsotropic";
            this.ZoomIsotropic.Size = new System.Drawing.Size(157, 22);
            this.ZoomIsotropic.Text = "I&sotropic";
            this.ZoomIsotropic.ToolTipText = "The Graph is isotropic when its X and Y scales are equal";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(154, 6);
            // 
            // ZoomFullScreen
            // 
            this.ZoomFullScreen.Name = "ZoomFullScreen";
            this.ZoomFullScreen.ShortcutKeys = System.Windows.Forms.Keys.F11;
            this.ZoomFullScreen.Size = new System.Drawing.Size(157, 22);
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
            this.ScrollMenu.Size = new System.Drawing.Size(48, 20);
            this.ScrollMenu.Text = "&Scroll";
            // 
            // ScrollLeft
            // 
            this.ScrollLeft.Name = "ScrollLeft";
            this.ScrollLeft.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Left)));
            this.ScrollLeft.Size = new System.Drawing.Size(176, 22);
            this.ScrollLeft.Text = "&Left";
            this.ScrollLeft.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ScrollRight
            // 
            this.ScrollRight.Name = "ScrollRight";
            this.ScrollRight.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Right)));
            this.ScrollRight.Size = new System.Drawing.Size(176, 22);
            this.ScrollRight.Text = "&Right";
            this.ScrollRight.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ScrollUp
            // 
            this.ScrollUp.Name = "ScrollUp";
            this.ScrollUp.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Up)));
            this.ScrollUp.Size = new System.Drawing.Size(176, 22);
            this.ScrollUp.Text = "&Up";
            this.ScrollUp.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // ScrollDown
            // 
            this.ScrollDown.Name = "ScrollDown";
            this.ScrollDown.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Down)));
            this.ScrollDown.Size = new System.Drawing.Size(176, 22);
            this.ScrollDown.Text = "&Down";
            this.ScrollDown.ToolTipText = "Graphs can also be scrolled by dragging with the left mouse button";
            // 
            // toolStripMenuItem6
            // 
            this.toolStripMenuItem6.Name = "toolStripMenuItem6";
            this.toolStripMenuItem6.Size = new System.Drawing.Size(173, 6);
            // 
            // ScrollCentre
            // 
            this.ScrollCentre.Name = "ScrollCentre";
            this.ScrollCentre.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Home)));
            this.ScrollCentre.Size = new System.Drawing.Size(176, 22);
            this.ScrollCentre.Text = "&Centre";
            this.ScrollCentre.ToolTipText = "Return the origin (0,0) to the centre of the display area";
            // 
            // TimerMenu
            // 
            this.TimerMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TimerRunPause,
            this.TimerReset,
            this.toolStripMenuItem3,
            this.TimerInterval});
            this.TimerMenu.Name = "TimerMenu";
            this.TimerMenu.Size = new System.Drawing.Size(50, 20);
            this.TimerMenu.Text = "&Timer";
            // 
            // TimerRunPause
            // 
            this.TimerRunPause.Name = "TimerRunPause";
            this.TimerRunPause.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.TimerRunPause.Size = new System.Drawing.Size(181, 22);
            this.TimerRunPause.Text = "&Run/Pause";
            // 
            // TimerReset
            // 
            this.TimerReset.Name = "TimerReset";
            this.TimerReset.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.TimerReset.Size = new System.Drawing.Size(181, 22);
            this.TimerReset.Text = "R&eset";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(178, 6);
            // 
            // TimerInterval
            // 
            this.TimerInterval.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.TimerInterval.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.TimerInterval.Items.AddRange(new object[] {
            "25",
            "30",
            "40",
            "50",
            "60",
            "75",
            "100",
            "120",
            "150",
            "200",
            "250",
            "300",
            "400",
            "500",
            "600",
            "750",
            "1000"});
            this.TimerInterval.Name = "TimerInterval";
            this.TimerInterval.Size = new System.Drawing.Size(121, 23);
            this.TimerInterval.ToolTipText = "Timer Interval (ms)";
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
            this.XYlabel,
            this.Tlabel,
            this.ModifiedLabel});
            this.StatusBar.Location = new System.Drawing.Point(0, 479);
            this.StatusBar.Name = "StatusBar";
            this.StatusBar.Size = new System.Drawing.Size(944, 22);
            this.StatusBar.TabIndex = 2;
            this.StatusBar.Text = "statusStrip1";
            // 
            // XYlabel
            // 
            this.XYlabel.AutoSize = false;
            this.XYlabel.Name = "XYlabel";
            this.XYlabel.Size = new System.Drawing.Size(200, 17);
            this.XYlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.ClientPanel.BackColor = System.Drawing.Color.White;
            this.ClientPanel.Controls.Add(this.LegendPanel);
            this.ClientPanel.Controls.Add(this.PictureBox);
            this.ClientPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClientPanel.Location = new System.Drawing.Point(0, 24);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(944, 455);
            this.ClientPanel.TabIndex = 6;
            // 
            // LegendPanel
            // 
            this.LegendPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LegendPanel.Location = new System.Drawing.Point(0, 0);
            this.LegendPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LegendPanel.Name = "LegendPanel";
            this.LegendPanel.Size = new System.Drawing.Size(0, 0);
            this.LegendPanel.TabIndex = 9;
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(12, 3);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(244, 104);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            // 
            // Tlabel
            // 
            this.Tlabel.AutoSize = false;
            this.Tlabel.Name = "Tlabel";
            this.Tlabel.Size = new System.Drawing.Size(118, 17);
            this.Tlabel.Text = "T";
            this.Tlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // AppForm
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
            this.Name = "AppForm";
            this.Text = "Sid";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
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
        public System.Windows.Forms.ToolStripMenuItem ViewMenu;
        public System.Windows.Forms.ToolStripMenuItem ViewCoordinatesTooltip;
        public System.Windows.Forms.StatusStrip StatusBar;
        public System.Windows.Forms.ContextMenuStrip PopupMenu;
        public System.Windows.Forms.MenuStrip MainMenu;
        public System.Windows.Forms.ErrorProvider ErrorProvider;
        public System.Windows.Forms.ToolStripMenuItem ViewLegend;
        public System.Windows.Forms.Panel ClientPanel;
        public System.Windows.Forms.PictureBox PictureBox;
        public System.Windows.Forms.Panel LegendPanel;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem4;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendTopLeft;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendTopRight;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendBottomLeft;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendBottomRight;
        public System.Windows.Forms.ToolStripMenuItem ViewLegendHide;
        public System.Windows.Forms.ToolStripMenuItem GraphMenu;
        public System.Windows.Forms.ToolStripMenuItem GraphAddNewFunction;
        public System.Windows.Forms.ToolStripMenuItem HelpMenu;
        public System.Windows.Forms.ToolStripMenuItem ZoomMenu;
        public System.Windows.Forms.ToolStripMenuItem ZoomIn;
        public System.Windows.Forms.ToolStripMenuItem ZoomOut;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        public System.Windows.Forms.ToolStripMenuItem ZoomReset;
        public System.Windows.Forms.ToolStripMenuItem ZoomIsotropic;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        public System.Windows.Forms.ToolStripMenuItem ZoomFullScreen;
        public System.Windows.Forms.ToolStripMenuItem ScrollMenu;
        public System.Windows.Forms.ToolStripMenuItem ScrollLeft;
        public System.Windows.Forms.ToolStripMenuItem ScrollRight;
        public System.Windows.Forms.ToolStripMenuItem ScrollUp;
        public System.Windows.Forms.ToolStripMenuItem ScrollDown;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        public System.Windows.Forms.ToolStripMenuItem ScrollCentre;
        public System.Windows.Forms.ToolStripStatusLabel XYlabel;
        public System.Windows.Forms.ToolStripMenuItem GraphProperties;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem3;
        public System.Windows.Forms.ToolStripMenuItem TimerMenu;
        public System.Windows.Forms.ToolStripMenuItem TimerRunPause;
        public System.Windows.Forms.ToolStripMenuItem TimerReset;
        public System.Windows.Forms.ToolStripComboBox TimerInterval;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem5;
        public System.Windows.Forms.ToolStripMenuItem ViewMathboard;
        public System.Windows.Forms.ToolStripStatusLabel Tlabel;
    }
}