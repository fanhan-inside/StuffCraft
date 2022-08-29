namespace StuffCraft
{
    partial class frmStuff
    {
        /// <summary>
        /// 必要なデザイナ変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージ リソースが破棄される場合 true、破棄されない場合は false です。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナで生成されたコード

        /// <summary>
        /// デザイナ サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディタで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStuff));
            this.splitLR = new System.Windows.Forms.SplitContainer();
            this.mainTree = new System.Windows.Forms.TreeView();
            this.menuTree = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newCaseTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCaseTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameCaseTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCaseTreeMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.imageList16 = new System.Windows.Forms.ImageList(this.components);
            this.splitUD = new System.Windows.Forms.SplitContainer();
            this.mainList = new System.Windows.Forms.ListView();
            this.chCategory = new System.Windows.Forms.ColumnHeader();
            this.chTitle = new System.Windows.Forms.ColumnHeader();
            this.chWordCount = new System.Windows.Forms.ColumnHeader();
            this.chUpdated = new System.Windows.Forms.ColumnHeader();
            this.menuList = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.newNoteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editNoteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNoteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportNoteListMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainText = new System.Windows.Forms.TextBox();
            this.mainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.caseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newCaseMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteCaseMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.renameCaseMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportCaseMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newNoteMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.editNoteMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deleteNoteMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportNoteMainMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mainToolbar = new System.Windows.Forms.ToolStrip();
            this.newCaseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteCaseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.renameCaseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportCaseToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.newNoteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.editNoteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.deleteNoteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.exportNoteToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.mainStatus = new System.Windows.Forms.StatusStrip();
            this.sfd = new System.Windows.Forms.SaveFileDialog();
            this.splitLR.Panel1.SuspendLayout();
            this.splitLR.Panel2.SuspendLayout();
            this.splitLR.SuspendLayout();
            this.menuTree.SuspendLayout();
            this.splitUD.Panel1.SuspendLayout();
            this.splitUD.Panel2.SuspendLayout();
            this.splitUD.SuspendLayout();
            this.menuList.SuspendLayout();
            this.mainMenu.SuspendLayout();
            this.mainToolbar.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitLR
            // 
            this.splitLR.AccessibleDescription = null;
            this.splitLR.AccessibleName = null;
            resources.ApplyResources(this.splitLR, "splitLR");
            this.splitLR.BackgroundImage = null;
            this.splitLR.Font = null;
            this.splitLR.Name = "splitLR";
            // 
            // splitLR.Panel1
            // 
            this.splitLR.Panel1.AccessibleDescription = null;
            this.splitLR.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitLR.Panel1, "splitLR.Panel1");
            this.splitLR.Panel1.BackgroundImage = null;
            this.splitLR.Panel1.Controls.Add(this.mainTree);
            this.splitLR.Panel1.Font = null;
            // 
            // splitLR.Panel2
            // 
            this.splitLR.Panel2.AccessibleDescription = null;
            this.splitLR.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitLR.Panel2, "splitLR.Panel2");
            this.splitLR.Panel2.BackgroundImage = null;
            this.splitLR.Panel2.Controls.Add(this.splitUD);
            this.splitLR.Panel2.Font = null;
            // 
            // mainTree
            // 
            this.mainTree.AccessibleDescription = null;
            this.mainTree.AccessibleName = null;
            this.mainTree.AllowDrop = true;
            resources.ApplyResources(this.mainTree, "mainTree");
            this.mainTree.BackgroundImage = null;
            this.mainTree.ContextMenuStrip = this.menuTree;
            this.mainTree.HideSelection = false;
            this.mainTree.HotTracking = true;
            this.mainTree.ImageList = this.imageList16;
            this.mainTree.LabelEdit = true;
            this.mainTree.Name = "mainTree";
            this.mainTree.ShowNodeToolTips = true;
            this.mainTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.mainTree_DragDrop);
            this.mainTree.DragOver += new System.Windows.Forms.DragEventHandler(this.mainTree_DragOver);
            this.mainTree.Enter += new System.EventHandler(this.mainTree_Enter);
            this.mainTree.AfterLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.mainTree_AfterLabelEdit);
            this.mainTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.mainTree_AfterSelect);
            this.mainTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.mainTree_DragEnter);
            this.mainTree.BeforeLabelEdit += new System.Windows.Forms.NodeLabelEditEventHandler(this.mainTree_BeforeLabelEdit);
            this.mainTree.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainTree_KeyDown);
            this.mainTree.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.mainTree_ItemDrag);
            this.mainTree.Leave += new System.EventHandler(this.mainTree_Leave);
            // 
            // menuTree
            // 
            this.menuTree.AccessibleDescription = null;
            this.menuTree.AccessibleName = null;
            resources.ApplyResources(this.menuTree, "menuTree");
            this.menuTree.BackgroundImage = null;
            this.menuTree.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCaseTreeMenuItem,
            this.deleteCaseTreeMenuItem,
            this.renameCaseTreeMenuItem,
            this.exportCaseTreeMenuItem});
            this.menuTree.Name = "menuTree";
            this.menuTree.Opening += new System.ComponentModel.CancelEventHandler(this.menuTree_Opening);
            // 
            // newCaseTreeMenuItem
            // 
            this.newCaseTreeMenuItem.AccessibleDescription = null;
            this.newCaseTreeMenuItem.AccessibleName = null;
            resources.ApplyResources(this.newCaseTreeMenuItem, "newCaseTreeMenuItem");
            this.newCaseTreeMenuItem.BackgroundImage = null;
            this.newCaseTreeMenuItem.Name = "newCaseTreeMenuItem";
            this.newCaseTreeMenuItem.ShortcutKeyDisplayString = null;
            this.newCaseTreeMenuItem.Click += new System.EventHandler(this.newCaseCommand_Click);
            // 
            // deleteCaseTreeMenuItem
            // 
            this.deleteCaseTreeMenuItem.AccessibleDescription = null;
            this.deleteCaseTreeMenuItem.AccessibleName = null;
            resources.ApplyResources(this.deleteCaseTreeMenuItem, "deleteCaseTreeMenuItem");
            this.deleteCaseTreeMenuItem.BackgroundImage = null;
            this.deleteCaseTreeMenuItem.Name = "deleteCaseTreeMenuItem";
            this.deleteCaseTreeMenuItem.ShortcutKeyDisplayString = null;
            this.deleteCaseTreeMenuItem.Click += new System.EventHandler(this.deleteCaseCommand_Click);
            // 
            // renameCaseTreeMenuItem
            // 
            this.renameCaseTreeMenuItem.AccessibleDescription = null;
            this.renameCaseTreeMenuItem.AccessibleName = null;
            resources.ApplyResources(this.renameCaseTreeMenuItem, "renameCaseTreeMenuItem");
            this.renameCaseTreeMenuItem.BackgroundImage = null;
            this.renameCaseTreeMenuItem.Name = "renameCaseTreeMenuItem";
            this.renameCaseTreeMenuItem.ShortcutKeyDisplayString = null;
            this.renameCaseTreeMenuItem.Click += new System.EventHandler(this.renameCaseCommand_Click);
            // 
            // exportCaseTreeMenuItem
            // 
            this.exportCaseTreeMenuItem.AccessibleDescription = null;
            this.exportCaseTreeMenuItem.AccessibleName = null;
            resources.ApplyResources(this.exportCaseTreeMenuItem, "exportCaseTreeMenuItem");
            this.exportCaseTreeMenuItem.BackgroundImage = null;
            this.exportCaseTreeMenuItem.Name = "exportCaseTreeMenuItem";
            this.exportCaseTreeMenuItem.ShortcutKeyDisplayString = null;
            this.exportCaseTreeMenuItem.Click += new System.EventHandler(this.exportCaseCommand_Click);
            // 
            // imageList16
            // 
            this.imageList16.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList16.ImageStream")));
            this.imageList16.TransparentColor = System.Drawing.Color.Transparent;
            this.imageList16.Images.SetKeyName(0, "CaseOpen");
            this.imageList16.Images.SetKeyName(1, "CaseClose");
            this.imageList16.Images.SetKeyName(2, "Note");
            // 
            // splitUD
            // 
            this.splitUD.AccessibleDescription = null;
            this.splitUD.AccessibleName = null;
            resources.ApplyResources(this.splitUD, "splitUD");
            this.splitUD.BackgroundImage = null;
            this.splitUD.Font = null;
            this.splitUD.Name = "splitUD";
            // 
            // splitUD.Panel1
            // 
            this.splitUD.Panel1.AccessibleDescription = null;
            this.splitUD.Panel1.AccessibleName = null;
            resources.ApplyResources(this.splitUD.Panel1, "splitUD.Panel1");
            this.splitUD.Panel1.BackgroundImage = null;
            this.splitUD.Panel1.Controls.Add(this.mainList);
            this.splitUD.Panel1.Font = null;
            // 
            // splitUD.Panel2
            // 
            this.splitUD.Panel2.AccessibleDescription = null;
            this.splitUD.Panel2.AccessibleName = null;
            resources.ApplyResources(this.splitUD.Panel2, "splitUD.Panel2");
            this.splitUD.Panel2.BackgroundImage = null;
            this.splitUD.Panel2.Controls.Add(this.mainText);
            this.splitUD.Panel2.Font = null;
            // 
            // mainList
            // 
            this.mainList.AccessibleDescription = null;
            this.mainList.AccessibleName = null;
            this.mainList.Activation = System.Windows.Forms.ItemActivation.OneClick;
            resources.ApplyResources(this.mainList, "mainList");
            this.mainList.AllowDrop = true;
            this.mainList.BackgroundImage = null;
            this.mainList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.chCategory,
            this.chTitle,
            this.chWordCount,
            this.chUpdated});
            this.mainList.ContextMenuStrip = this.menuList;
            this.mainList.FullRowSelect = true;
            this.mainList.HideSelection = false;
            this.mainList.Name = "mainList";
            this.mainList.SmallImageList = this.imageList16;
            this.mainList.Sorting = System.Windows.Forms.SortOrder.Ascending;
            this.mainList.UseCompatibleStateImageBehavior = false;
            this.mainList.View = System.Windows.Forms.View.Details;
            this.mainList.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.mainList_MouseDoubleClick);
            this.mainList.SelectedIndexChanged += new System.EventHandler(this.mainList_SelectedIndexChanged);
            this.mainList.KeyDown += new System.Windows.Forms.KeyEventHandler(this.mainList_KeyDown);
            this.mainList.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.mainList_ColumnClick);
            this.mainList.ItemDrag += new System.Windows.Forms.ItemDragEventHandler(this.mainList_ItemDrag);
            // 
            // chCategory
            // 
            resources.ApplyResources(this.chCategory, "chCategory");
            // 
            // chTitle
            // 
            resources.ApplyResources(this.chTitle, "chTitle");
            // 
            // chWordCount
            // 
            resources.ApplyResources(this.chWordCount, "chWordCount");
            // 
            // chUpdated
            // 
            resources.ApplyResources(this.chUpdated, "chUpdated");
            // 
            // menuList
            // 
            this.menuList.AccessibleDescription = null;
            this.menuList.AccessibleName = null;
            resources.ApplyResources(this.menuList, "menuList");
            this.menuList.BackgroundImage = null;
            this.menuList.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newNoteListMenuItem,
            this.editNoteListMenuItem,
            this.deleteNoteListMenuItem,
            this.exportNoteListMenuItem});
            this.menuList.Name = "menuList";
            // 
            // newNoteListMenuItem
            // 
            this.newNoteListMenuItem.AccessibleDescription = null;
            this.newNoteListMenuItem.AccessibleName = null;
            resources.ApplyResources(this.newNoteListMenuItem, "newNoteListMenuItem");
            this.newNoteListMenuItem.BackgroundImage = null;
            this.newNoteListMenuItem.Name = "newNoteListMenuItem";
            this.newNoteListMenuItem.ShortcutKeyDisplayString = null;
            this.newNoteListMenuItem.Click += new System.EventHandler(this.newNoteCommand_Click);
            // 
            // editNoteListMenuItem
            // 
            this.editNoteListMenuItem.AccessibleDescription = null;
            this.editNoteListMenuItem.AccessibleName = null;
            resources.ApplyResources(this.editNoteListMenuItem, "editNoteListMenuItem");
            this.editNoteListMenuItem.BackgroundImage = null;
            this.editNoteListMenuItem.Name = "editNoteListMenuItem";
            this.editNoteListMenuItem.ShortcutKeyDisplayString = null;
            this.editNoteListMenuItem.Click += new System.EventHandler(this.editNoteCommand_Click);
            // 
            // deleteNoteListMenuItem
            // 
            this.deleteNoteListMenuItem.AccessibleDescription = null;
            this.deleteNoteListMenuItem.AccessibleName = null;
            resources.ApplyResources(this.deleteNoteListMenuItem, "deleteNoteListMenuItem");
            this.deleteNoteListMenuItem.BackgroundImage = null;
            this.deleteNoteListMenuItem.Name = "deleteNoteListMenuItem";
            this.deleteNoteListMenuItem.ShortcutKeyDisplayString = null;
            this.deleteNoteListMenuItem.Click += new System.EventHandler(this.deleteNoteCommand_Click);
            // 
            // exportNoteListMenuItem
            // 
            this.exportNoteListMenuItem.AccessibleDescription = null;
            this.exportNoteListMenuItem.AccessibleName = null;
            resources.ApplyResources(this.exportNoteListMenuItem, "exportNoteListMenuItem");
            this.exportNoteListMenuItem.BackgroundImage = null;
            this.exportNoteListMenuItem.Name = "exportNoteListMenuItem";
            this.exportNoteListMenuItem.ShortcutKeyDisplayString = null;
            this.exportNoteListMenuItem.Click += new System.EventHandler(this.exportNoteCommand_Click);
            // 
            // mainText
            // 
            this.mainText.AccessibleDescription = null;
            this.mainText.AccessibleName = null;
            resources.ApplyResources(this.mainText, "mainText");
            this.mainText.BackColor = System.Drawing.SystemColors.Window;
            this.mainText.BackgroundImage = null;
            this.mainText.Name = "mainText";
            this.mainText.ReadOnly = true;
            // 
            // mainMenu
            // 
            this.mainMenu.AccessibleDescription = null;
            this.mainMenu.AccessibleName = null;
            resources.ApplyResources(this.mainMenu, "mainMenu");
            this.mainMenu.BackgroundImage = null;
            this.mainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.caseToolStripMenuItem,
            this.noteToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.mainMenu.Name = "mainMenu";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.AccessibleDescription = null;
            this.fileToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.fileToolStripMenuItem, "fileToolStripMenuItem");
            this.fileToolStripMenuItem.BackgroundImage = null;
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exitMainMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // exitMainMenuItem
            // 
            this.exitMainMenuItem.AccessibleDescription = null;
            this.exitMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.exitMainMenuItem, "exitMainMenuItem");
            this.exitMainMenuItem.BackgroundImage = null;
            this.exitMainMenuItem.Name = "exitMainMenuItem";
            this.exitMainMenuItem.ShortcutKeyDisplayString = null;
            this.exitMainMenuItem.Click += new System.EventHandler(this.exitMainMenuItem_Click);
            // 
            // caseToolStripMenuItem
            // 
            this.caseToolStripMenuItem.AccessibleDescription = null;
            this.caseToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.caseToolStripMenuItem, "caseToolStripMenuItem");
            this.caseToolStripMenuItem.BackgroundImage = null;
            this.caseToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCaseMainMenuItem,
            this.deleteCaseMainMenuItem,
            this.renameCaseMainMenuItem,
            this.exportCaseMainMenuItem});
            this.caseToolStripMenuItem.Name = "caseToolStripMenuItem";
            this.caseToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // newCaseMainMenuItem
            // 
            this.newCaseMainMenuItem.AccessibleDescription = null;
            this.newCaseMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.newCaseMainMenuItem, "newCaseMainMenuItem");
            this.newCaseMainMenuItem.BackgroundImage = null;
            this.newCaseMainMenuItem.Name = "newCaseMainMenuItem";
            this.newCaseMainMenuItem.ShortcutKeyDisplayString = null;
            this.newCaseMainMenuItem.Click += new System.EventHandler(this.newCaseCommand_Click);
            // 
            // deleteCaseMainMenuItem
            // 
            this.deleteCaseMainMenuItem.AccessibleDescription = null;
            this.deleteCaseMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.deleteCaseMainMenuItem, "deleteCaseMainMenuItem");
            this.deleteCaseMainMenuItem.BackgroundImage = null;
            this.deleteCaseMainMenuItem.Name = "deleteCaseMainMenuItem";
            this.deleteCaseMainMenuItem.ShortcutKeyDisplayString = null;
            this.deleteCaseMainMenuItem.Click += new System.EventHandler(this.deleteCaseCommand_Click);
            // 
            // renameCaseMainMenuItem
            // 
            this.renameCaseMainMenuItem.AccessibleDescription = null;
            this.renameCaseMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.renameCaseMainMenuItem, "renameCaseMainMenuItem");
            this.renameCaseMainMenuItem.BackgroundImage = null;
            this.renameCaseMainMenuItem.Name = "renameCaseMainMenuItem";
            this.renameCaseMainMenuItem.ShortcutKeyDisplayString = null;
            this.renameCaseMainMenuItem.Click += new System.EventHandler(this.renameCaseCommand_Click);
            // 
            // exportCaseMainMenuItem
            // 
            this.exportCaseMainMenuItem.AccessibleDescription = null;
            this.exportCaseMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.exportCaseMainMenuItem, "exportCaseMainMenuItem");
            this.exportCaseMainMenuItem.BackgroundImage = null;
            this.exportCaseMainMenuItem.Name = "exportCaseMainMenuItem";
            this.exportCaseMainMenuItem.ShortcutKeyDisplayString = null;
            this.exportCaseMainMenuItem.Click += new System.EventHandler(this.exportCaseCommand_Click);
            // 
            // noteToolStripMenuItem
            // 
            this.noteToolStripMenuItem.AccessibleDescription = null;
            this.noteToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.noteToolStripMenuItem, "noteToolStripMenuItem");
            this.noteToolStripMenuItem.BackgroundImage = null;
            this.noteToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newNoteMainMenuItem,
            this.editNoteMainMenuItem,
            this.deleteNoteMainMenuItem,
            this.exportNoteMainMenuItem});
            this.noteToolStripMenuItem.Name = "noteToolStripMenuItem";
            this.noteToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // newNoteMainMenuItem
            // 
            this.newNoteMainMenuItem.AccessibleDescription = null;
            this.newNoteMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.newNoteMainMenuItem, "newNoteMainMenuItem");
            this.newNoteMainMenuItem.BackgroundImage = null;
            this.newNoteMainMenuItem.Name = "newNoteMainMenuItem";
            this.newNoteMainMenuItem.ShortcutKeyDisplayString = null;
            this.newNoteMainMenuItem.Click += new System.EventHandler(this.newNoteCommand_Click);
            // 
            // editNoteMainMenuItem
            // 
            this.editNoteMainMenuItem.AccessibleDescription = null;
            this.editNoteMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.editNoteMainMenuItem, "editNoteMainMenuItem");
            this.editNoteMainMenuItem.BackgroundImage = null;
            this.editNoteMainMenuItem.Name = "editNoteMainMenuItem";
            this.editNoteMainMenuItem.ShortcutKeyDisplayString = null;
            this.editNoteMainMenuItem.Click += new System.EventHandler(this.editNoteCommand_Click);
            // 
            // deleteNoteMainMenuItem
            // 
            this.deleteNoteMainMenuItem.AccessibleDescription = null;
            this.deleteNoteMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.deleteNoteMainMenuItem, "deleteNoteMainMenuItem");
            this.deleteNoteMainMenuItem.BackgroundImage = null;
            this.deleteNoteMainMenuItem.Name = "deleteNoteMainMenuItem";
            this.deleteNoteMainMenuItem.ShortcutKeyDisplayString = null;
            this.deleteNoteMainMenuItem.Click += new System.EventHandler(this.deleteNoteCommand_Click);
            // 
            // exportNoteMainMenuItem
            // 
            this.exportNoteMainMenuItem.AccessibleDescription = null;
            this.exportNoteMainMenuItem.AccessibleName = null;
            resources.ApplyResources(this.exportNoteMainMenuItem, "exportNoteMainMenuItem");
            this.exportNoteMainMenuItem.BackgroundImage = null;
            this.exportNoteMainMenuItem.Name = "exportNoteMainMenuItem";
            this.exportNoteMainMenuItem.ShortcutKeyDisplayString = null;
            this.exportNoteMainMenuItem.Click += new System.EventHandler(this.exportNoteCommand_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.AccessibleDescription = null;
            this.helpToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.BackgroundImage = null;
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.ShortcutKeyDisplayString = null;
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.AccessibleDescription = null;
            this.aboutToolStripMenuItem.AccessibleName = null;
            resources.ApplyResources(this.aboutToolStripMenuItem, "aboutToolStripMenuItem");
            this.aboutToolStripMenuItem.BackgroundImage = null;
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeyDisplayString = null;
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // mainToolbar
            // 
            this.mainToolbar.AccessibleDescription = null;
            this.mainToolbar.AccessibleName = null;
            resources.ApplyResources(this.mainToolbar, "mainToolbar");
            this.mainToolbar.BackgroundImage = null;
            this.mainToolbar.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newCaseToolStripButton,
            this.deleteCaseToolStripButton,
            this.renameCaseToolStripButton,
            this.exportCaseToolStripButton,
            this.toolStripSeparator1,
            this.newNoteToolStripButton,
            this.editNoteToolStripButton,
            this.deleteNoteToolStripButton,
            this.exportNoteToolStripButton});
            this.mainToolbar.Name = "mainToolbar";
            // 
            // newCaseToolStripButton
            // 
            this.newCaseToolStripButton.AccessibleDescription = null;
            this.newCaseToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.newCaseToolStripButton, "newCaseToolStripButton");
            this.newCaseToolStripButton.BackgroundImage = null;
            this.newCaseToolStripButton.Name = "newCaseToolStripButton";
            this.newCaseToolStripButton.Click += new System.EventHandler(this.newCaseCommand_Click);
            // 
            // deleteCaseToolStripButton
            // 
            this.deleteCaseToolStripButton.AccessibleDescription = null;
            this.deleteCaseToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.deleteCaseToolStripButton, "deleteCaseToolStripButton");
            this.deleteCaseToolStripButton.BackgroundImage = null;
            this.deleteCaseToolStripButton.Name = "deleteCaseToolStripButton";
            this.deleteCaseToolStripButton.Click += new System.EventHandler(this.deleteCaseCommand_Click);
            // 
            // renameCaseToolStripButton
            // 
            this.renameCaseToolStripButton.AccessibleDescription = null;
            this.renameCaseToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.renameCaseToolStripButton, "renameCaseToolStripButton");
            this.renameCaseToolStripButton.BackgroundImage = null;
            this.renameCaseToolStripButton.Name = "renameCaseToolStripButton";
            this.renameCaseToolStripButton.Click += new System.EventHandler(this.renameCaseCommand_Click);
            // 
            // exportCaseToolStripButton
            // 
            this.exportCaseToolStripButton.AccessibleDescription = null;
            this.exportCaseToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.exportCaseToolStripButton, "exportCaseToolStripButton");
            this.exportCaseToolStripButton.BackgroundImage = null;
            this.exportCaseToolStripButton.Name = "exportCaseToolStripButton";
            this.exportCaseToolStripButton.Click += new System.EventHandler(this.exportCaseCommand_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.AccessibleDescription = null;
            this.toolStripSeparator1.AccessibleName = null;
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // newNoteToolStripButton
            // 
            this.newNoteToolStripButton.AccessibleDescription = null;
            this.newNoteToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.newNoteToolStripButton, "newNoteToolStripButton");
            this.newNoteToolStripButton.BackgroundImage = null;
            this.newNoteToolStripButton.Name = "newNoteToolStripButton";
            this.newNoteToolStripButton.Click += new System.EventHandler(this.newNoteCommand_Click);
            // 
            // editNoteToolStripButton
            // 
            this.editNoteToolStripButton.AccessibleDescription = null;
            this.editNoteToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.editNoteToolStripButton, "editNoteToolStripButton");
            this.editNoteToolStripButton.BackgroundImage = null;
            this.editNoteToolStripButton.Name = "editNoteToolStripButton";
            this.editNoteToolStripButton.Click += new System.EventHandler(this.editNoteCommand_Click);
            // 
            // deleteNoteToolStripButton
            // 
            this.deleteNoteToolStripButton.AccessibleDescription = null;
            this.deleteNoteToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.deleteNoteToolStripButton, "deleteNoteToolStripButton");
            this.deleteNoteToolStripButton.BackgroundImage = null;
            this.deleteNoteToolStripButton.Name = "deleteNoteToolStripButton";
            this.deleteNoteToolStripButton.Click += new System.EventHandler(this.deleteNoteCommand_Click);
            // 
            // exportNoteToolStripButton
            // 
            this.exportNoteToolStripButton.AccessibleDescription = null;
            this.exportNoteToolStripButton.AccessibleName = null;
            resources.ApplyResources(this.exportNoteToolStripButton, "exportNoteToolStripButton");
            this.exportNoteToolStripButton.BackgroundImage = null;
            this.exportNoteToolStripButton.Name = "exportNoteToolStripButton";
            this.exportNoteToolStripButton.Click += new System.EventHandler(this.exportNoteCommand_Click);
            // 
            // mainStatus
            // 
            this.mainStatus.AccessibleDescription = null;
            this.mainStatus.AccessibleName = null;
            resources.ApplyResources(this.mainStatus, "mainStatus");
            this.mainStatus.BackgroundImage = null;
            this.mainStatus.Name = "mainStatus";
            // 
            // sfd
            // 
            resources.ApplyResources(this.sfd, "sfd");
            // 
            // frmStuff
            // 
            this.AccessibleDescription = null;
            this.AccessibleName = null;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = null;
            this.Controls.Add(this.splitLR);
            this.Controls.Add(this.mainStatus);
            this.Controls.Add(this.mainToolbar);
            this.Controls.Add(this.mainMenu);
            this.MainMenuStrip = this.mainMenu;
            this.Name = "frmStuff";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmStuff_FormClosed);
            this.Load += new System.EventHandler(this.frmStuff_Load);
            this.splitLR.Panel1.ResumeLayout(false);
            this.splitLR.Panel2.ResumeLayout(false);
            this.splitLR.ResumeLayout(false);
            this.menuTree.ResumeLayout(false);
            this.splitUD.Panel1.ResumeLayout(false);
            this.splitUD.Panel2.ResumeLayout(false);
            this.splitUD.Panel2.PerformLayout();
            this.splitUD.ResumeLayout(false);
            this.menuList.ResumeLayout(false);
            this.mainMenu.ResumeLayout(false);
            this.mainMenu.PerformLayout();
            this.mainToolbar.ResumeLayout(false);
            this.mainToolbar.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip mainMenu;
        private System.Windows.Forms.ToolStrip mainToolbar;
        private System.Windows.Forms.StatusStrip mainStatus;
        private System.Windows.Forms.SplitContainer splitLR;
        private System.Windows.Forms.TreeView mainTree;
        private System.Windows.Forms.SplitContainer splitUD;
        private System.Windows.Forms.ListView mainList;
        private System.Windows.Forms.ContextMenuStrip menuTree;
        private System.Windows.Forms.ContextMenuStrip menuList;
        private System.Windows.Forms.TextBox mainText;
        private System.Windows.Forms.ColumnHeader chCategory;
        private System.Windows.Forms.ColumnHeader chTitle;
        private System.Windows.Forms.ColumnHeader chWordCount;
        private System.Windows.Forms.ColumnHeader chUpdated;
        private System.Windows.Forms.ToolStripMenuItem newCaseTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCaseTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newNoteListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteNoteListMenuItem;
        private System.Windows.Forms.ImageList imageList16;
        private System.Windows.Forms.ToolStripMenuItem editNoteListMenuItem;
        private System.Windows.Forms.ToolStripButton newNoteToolStripButton;
        private System.Windows.Forms.ToolStripButton editNoteToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteNoteToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem caseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newNoteMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editNoteMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteNoteMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripButton renameCaseToolStripButton;
        private System.Windows.Forms.ToolStripButton deleteCaseToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem renameCaseTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem newCaseMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem renameCaseMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem deleteCaseMainMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton newCaseToolStripButton;
        private System.Windows.Forms.ToolStripMenuItem exportCaseTreeMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportNoteListMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportCaseMainMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportNoteMainMenuItem;
        private System.Windows.Forms.SaveFileDialog sfd;
        private System.Windows.Forms.ToolStripButton exportCaseToolStripButton;
        private System.Windows.Forms.ToolStripButton exportNoteToolStripButton;
    }
}

