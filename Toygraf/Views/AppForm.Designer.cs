﻿namespace ToyGraf.Views
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
            this.ViewLegend = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendTopRight = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomLeft = new System.Windows.Forms.ToolStripMenuItem();
            this.ViewLegendBottomRight = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewLegendHide = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem7 = new System.Windows.Forms.ToolStripSeparator();
            this.ViewCoordinatesTooltip = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerRunPause = new System.Windows.Forms.ToolStripMenuItem();
            this.TimerReset = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.HelpAbout = new System.Windows.Forms.ToolStripMenuItem();
            this.StatusBar = new System.Windows.Forms.StatusStrip();
            this.XYlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Rϴlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.Tlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.FPSlabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ModifiedLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.PopupMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ErrorProvider = new System.Windows.Forms.ErrorProvider(this.components);
            this.ClientPanel = new System.Windows.Forms.Panel();
            this.LegendPanel = new System.Windows.Forms.Panel();
            this.PictureBox = new System.Windows.Forms.PictureBox();
            this.Toolbar = new System.Windows.Forms.ToolStrip();
            this.tbNew = new System.Windows.Forms.ToolStripButton();
            this.tbOpen = new System.Windows.Forms.ToolStripButton();
            this.tbSave = new System.Windows.Forms.ToolStripButton();
            this.tbAdd = new System.Windows.Forms.ToolStripButton();
            this.tbCartesian = new System.Windows.Forms.ToolStripButton();
            this.tbPolar = new System.Windows.Forms.ToolStripButton();
            this.tbProperties = new System.Windows.Forms.ToolStripButton();
            this.tbFullScreen = new System.Windows.Forms.ToolStripButton();
            this.tbTimer = new System.Windows.Forms.ToolStripButton();
            this.MainMenu.SuspendLayout();
            this.StatusBar.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).BeginInit();
            this.ClientPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).BeginInit();
            this.Toolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileMenu,
            this.GraphMenu,
            this.ViewMenu,
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
            this.FileNew.Image = global::ToyGraf.Properties.Resources.New;
            this.FileNew.Name = "FileNew";
            this.FileNew.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.FileNew.Size = new System.Drawing.Size(146, 22);
            this.FileNew.Text = "&New";
            // 
            // FileOpen
            // 
            this.FileOpen.Image = global::ToyGraf.Properties.Resources.Open;
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
            this.FileSave.Image = global::ToyGraf.Properties.Resources.Save;
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
            this.GraphType,
            this.GraphProperties});
            this.GraphMenu.Name = "GraphMenu";
            this.GraphMenu.Size = new System.Drawing.Size(51, 20);
            this.GraphMenu.Text = "&Graph";
            // 
            // GraphAddNewFunction
            // 
            this.GraphAddNewFunction.Image = global::ToyGraf.Properties.Resources.Add;
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
            this.GraphType.Text = "&Plot Type";
            // 
            // GraphTypeCartesian
            // 
            this.GraphTypeCartesian.Image = global::ToyGraf.Properties.Resources.Cartesian;
            this.GraphTypeCartesian.Name = "GraphTypeCartesian";
            this.GraphTypeCartesian.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.C)));
            this.GraphTypeCartesian.Size = new System.Drawing.Size(197, 22);
            this.GraphTypeCartesian.Text = "&Cartesian";
            // 
            // GraphTypePolar
            // 
            this.GraphTypePolar.Image = global::ToyGraf.Properties.Resources.Polar;
            this.GraphTypePolar.Name = "GraphTypePolar";
            this.GraphTypePolar.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.P)));
            this.GraphTypePolar.Size = new System.Drawing.Size(197, 22);
            this.GraphTypePolar.Text = "&Polar";
            // 
            // GraphProperties
            // 
            this.GraphProperties.Image = global::ToyGraf.Properties.Resources.Properties;
            this.GraphProperties.Name = "GraphProperties";
            this.GraphProperties.ShortcutKeys = System.Windows.Forms.Keys.F3;
            this.GraphProperties.Size = new System.Drawing.Size(201, 22);
            this.GraphProperties.Text = "Pr&operties...";
            // 
            // ViewMenu
            // 
            this.ViewMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ZoomMenu,
            this.ScrollMenu,
            this.ViewLegend,
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
            // ZoomReset
            // 
            this.ZoomReset.Name = "ZoomReset";
            this.ZoomReset.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.End)));
            this.ZoomReset.Size = new System.Drawing.Size(157, 22);
            this.ZoomReset.Text = "&Reset";
            this.ZoomReset.ToolTipText = "To the viewport when the graph was created or last saved";
            // 
            // toolStripMenuItem8
            // 
            this.toolStripMenuItem8.Name = "toolStripMenuItem8";
            this.toolStripMenuItem8.Size = new System.Drawing.Size(154, 6);
            // 
            // ZoomFullScreen
            // 
            this.ZoomFullScreen.Image = global::ToyGraf.Properties.Resources.FullScreen;
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
            this.ScrollMenu.Size = new System.Drawing.Size(183, 22);
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
            this.ViewLegend.Size = new System.Drawing.Size(183, 22);
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
            // TimerMenu
            // 
            this.TimerMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.TimerRunPause,
            this.TimerReset});
            this.TimerMenu.Name = "TimerMenu";
            this.TimerMenu.Size = new System.Drawing.Size(50, 20);
            this.TimerMenu.Text = "&Timer";
            // 
            // TimerRunPause
            // 
            this.TimerRunPause.Image = global::ToyGraf.Properties.Resources.Timer;
            this.TimerRunPause.Name = "TimerRunPause";
            this.TimerRunPause.ShortcutKeys = System.Windows.Forms.Keys.F9;
            this.TimerRunPause.Size = new System.Drawing.Size(150, 22);
            this.TimerRunPause.Text = "&Run/Pause";
            // 
            // TimerReset
            // 
            this.TimerReset.Name = "TimerReset";
            this.TimerReset.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.TimerReset.Size = new System.Drawing.Size(150, 22);
            this.TimerReset.Text = "R&eset";
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
            this.Rϴlabel,
            this.Tlabel,
            this.FPSlabel,
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
            this.XYlabel.Size = new System.Drawing.Size(192, 17);
            this.XYlabel.Text = "{x=0, y=0}";
            this.XYlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Rϴlabel
            // 
            this.Rϴlabel.AutoSize = false;
            this.Rϴlabel.Name = "Rϴlabel";
            this.Rϴlabel.Size = new System.Drawing.Size(192, 17);
            this.Rϴlabel.Text = "{r=0, θ=0}";
            this.Rϴlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // Tlabel
            // 
            this.Tlabel.AutoSize = false;
            this.Tlabel.Name = "Tlabel";
            this.Tlabel.Size = new System.Drawing.Size(64, 17);
            this.Tlabel.Text = "t=0.0";
            this.Tlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // FPSlabel
            // 
            this.FPSlabel.AutoSize = false;
            this.FPSlabel.Name = "FPSlabel";
            this.FPSlabel.Size = new System.Drawing.Size(64, 17);
            this.FPSlabel.Text = "fps=0.0";
            this.FPSlabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
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
            this.ClientPanel.Location = new System.Drawing.Point(23, 24);
            this.ClientPanel.Name = "ClientPanel";
            this.ClientPanel.Size = new System.Drawing.Size(921, 455);
            this.ClientPanel.TabIndex = 6;
            // 
            // LegendPanel
            // 
            this.LegendPanel.BackColor = System.Drawing.SystemColors.Control;
            this.LegendPanel.Location = new System.Drawing.Point(0, 0);
            this.LegendPanel.Margin = new System.Windows.Forms.Padding(0);
            this.LegendPanel.Name = "LegendPanel";
            this.LegendPanel.Size = new System.Drawing.Size(16, 16);
            this.LegendPanel.TabIndex = 9;
            // 
            // PictureBox
            // 
            this.PictureBox.Location = new System.Drawing.Point(19, 19);
            this.PictureBox.Name = "PictureBox";
            this.PictureBox.Size = new System.Drawing.Size(16, 16);
            this.PictureBox.TabIndex = 3;
            this.PictureBox.TabStop = false;
            // 
            // Toolbar
            // 
            this.Toolbar.Dock = System.Windows.Forms.DockStyle.Left;
            this.Toolbar.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.Toolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tbNew,
            this.tbOpen,
            this.tbSave,
            this.tbAdd,
            this.tbCartesian,
            this.tbPolar,
            this.tbProperties,
            this.tbFullScreen,
            this.tbTimer});
            this.Toolbar.Location = new System.Drawing.Point(0, 24);
            this.Toolbar.Name = "Toolbar";
            this.Toolbar.Padding = new System.Windows.Forms.Padding(0);
            this.Toolbar.Size = new System.Drawing.Size(23, 455);
            this.Toolbar.TabIndex = 11;
            // 
            // tbNew
            // 
            this.tbNew.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbNew.Image = global::ToyGraf.Properties.Resources.New;
            this.tbNew.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbNew.Name = "tbNew";
            this.tbNew.Size = new System.Drawing.Size(22, 20);
            this.tbNew.ToolTipText = "Create a new file (Ctrl+N)";
            // 
            // tbOpen
            // 
            this.tbOpen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbOpen.Image = global::ToyGraf.Properties.Resources.Open;
            this.tbOpen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbOpen.Name = "tbOpen";
            this.tbOpen.Size = new System.Drawing.Size(22, 20);
            this.tbOpen.ToolTipText = "Open an existing file (Ctrl+O)";
            // 
            // tbSave
            // 
            this.tbSave.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbSave.Image = global::ToyGraf.Properties.Resources.Save;
            this.tbSave.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbSave.Name = "tbSave";
            this.tbSave.Size = new System.Drawing.Size(22, 20);
            this.tbSave.ToolTipText = "Save to file (Ctrl+S)";
            // 
            // tbAdd
            // 
            this.tbAdd.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbAdd.Image = global::ToyGraf.Properties.Resources.Add;
            this.tbAdd.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbAdd.Name = "tbAdd";
            this.tbAdd.Size = new System.Drawing.Size(22, 20);
            this.tbAdd.ToolTipText = "Add a new function (F2)";
            // 
            // tbCartesian
            // 
            this.tbCartesian.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbCartesian.Image = global::ToyGraf.Properties.Resources.Cartesian;
            this.tbCartesian.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbCartesian.Name = "tbCartesian";
            this.tbCartesian.Size = new System.Drawing.Size(22, 20);
            this.tbCartesian.ToolTipText = "Graph type = Cartesian (Ctrl+Shift+C)";
            // 
            // tbPolar
            // 
            this.tbPolar.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbPolar.Image = global::ToyGraf.Properties.Resources.Polar;
            this.tbPolar.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbPolar.Name = "tbPolar";
            this.tbPolar.Size = new System.Drawing.Size(22, 20);
            this.tbPolar.ToolTipText = "Graph type = Polar (Ctrl+Shift+P)";
            // 
            // tbProperties
            // 
            this.tbProperties.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbProperties.Image = global::ToyGraf.Properties.Resources.Properties;
            this.tbProperties.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbProperties.Name = "tbProperties";
            this.tbProperties.Size = new System.Drawing.Size(22, 20);
            this.tbProperties.ToolTipText = "Graph properties (F3)";
            // 
            // tbFullScreen
            // 
            this.tbFullScreen.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbFullScreen.Image = global::ToyGraf.Properties.Resources.FullScreen;
            this.tbFullScreen.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbFullScreen.Name = "tbFullScreen";
            this.tbFullScreen.Size = new System.Drawing.Size(22, 20);
            this.tbFullScreen.ToolTipText = "Full screen (F11)";
            // 
            // tbTimer
            // 
            this.tbTimer.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.tbTimer.Image = global::ToyGraf.Properties.Resources.Timer;
            this.tbTimer.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.tbTimer.Name = "tbTimer";
            this.tbTimer.Size = new System.Drawing.Size(22, 20);
            this.tbTimer.ToolTipText = "Timer run/pause (F9)";
            // 
            // AppForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(944, 501);
            this.ContextMenuStrip = this.PopupMenu;
            this.Controls.Add(this.ClientPanel);
            this.Controls.Add(this.Toolbar);
            this.Controls.Add(this.StatusBar);
            this.Controls.Add(this.MainMenu);
            this.MainMenuStrip = this.MainMenu;
            this.MinimumSize = new System.Drawing.Size(640, 480);
            this.Name = "AppForm";
            this.Text = "ToyGraf";
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.StatusBar.ResumeLayout(false);
            this.StatusBar.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ErrorProvider)).EndInit();
            this.ClientPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.PictureBox)).EndInit();
            this.Toolbar.ResumeLayout(false);
            this.Toolbar.PerformLayout();
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
        public System.Windows.Forms.ToolStripStatusLabel XYlabel;
        public System.Windows.Forms.ToolStripStatusLabel Rϴlabel;
        public System.Windows.Forms.ToolStripMenuItem GraphProperties;
        public System.Windows.Forms.ToolStripMenuItem TimerMenu;
        public System.Windows.Forms.ToolStripMenuItem TimerRunPause;
        public System.Windows.Forms.ToolStripMenuItem TimerReset;
        public System.Windows.Forms.ToolStripStatusLabel Tlabel;
        public System.Windows.Forms.ToolStripMenuItem ZoomMenu;
        public System.Windows.Forms.ToolStripMenuItem ZoomIn;
        public System.Windows.Forms.ToolStripMenuItem ZoomOut;
        public System.Windows.Forms.ToolStripMenuItem ZoomReset;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem8;
        public System.Windows.Forms.ToolStripMenuItem ZoomFullScreen;
        public System.Windows.Forms.ToolStripMenuItem ScrollMenu;
        public System.Windows.Forms.ToolStripMenuItem ScrollLeft;
        public System.Windows.Forms.ToolStripMenuItem ScrollRight;
        public System.Windows.Forms.ToolStripMenuItem ScrollUp;
        public System.Windows.Forms.ToolStripMenuItem ScrollDown;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem6;
        public System.Windows.Forms.ToolStripMenuItem ScrollCentre;
        public System.Windows.Forms.ToolStripMenuItem GraphType;
        public System.Windows.Forms.ToolStripMenuItem GraphTypeCartesian;
        public System.Windows.Forms.ToolStripMenuItem GraphTypePolar;
        public System.Windows.Forms.ToolStrip Toolbar;
        public System.Windows.Forms.ToolStripButton tbNew;
        public System.Windows.Forms.ToolStripButton tbOpen;
        public System.Windows.Forms.ToolStripButton tbSave;
        public System.Windows.Forms.ToolStripButton tbAdd;
        public System.Windows.Forms.ToolStripButton tbCartesian;
        public System.Windows.Forms.ToolStripButton tbPolar;
        public System.Windows.Forms.ToolStripButton tbProperties;
        public System.Windows.Forms.ToolStripButton tbTimer;
        private System.Windows.Forms.ToolStripSeparator toolStripMenuItem7;
        public System.Windows.Forms.ToolStripStatusLabel FPSlabel;
        public System.Windows.Forms.ToolStripButton tbFullScreen;
    }
}