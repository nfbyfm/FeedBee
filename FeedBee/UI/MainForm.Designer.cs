namespace FeedBee.UI
{
    partial class MainForm
    {
        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveListToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.importToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.feedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.addToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
            this.updateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.cancelUpdateToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator5 = new System.Windows.Forms.ToolStripSeparator();
            this.markToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MarkAllAsReadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openExternallyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.OpenAllUnreadToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator9 = new System.Windows.Forms.ToolStripSeparator();
            this.resetAllEntriesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.extrasToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.webpageFeedDefinitionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator8 = new System.Windows.Forms.ToolStripSeparator();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.mainProgressBar = new System.Windows.Forms.ToolStripProgressBar();
            this.statusLabel = new FeedBee.UI.TimedStatusLabel();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.tVMain = new System.Windows.Forms.TreeView();
            this.cMS_Treeview = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openAllUnreadToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_MarkAsRead = new System.Windows.Forms.ToolStripMenuItem();
            this.cMS_Update = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator6 = new System.Windows.Forms.ToolStripSeparator();
            this.cMS_Edit = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator7 = new System.Windows.Forms.ToolStripSeparator();
            this.cMS_Delete = new System.Windows.Forms.ToolStripMenuItem();
            this.lVFeedItems = new System.Windows.Forms.ListView();
            this.cH_Title = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.menuStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.cMS_Treeview.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.feedToolStripMenuItem,
            this.extrasToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(800, 24);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openListToolStripMenuItem,
            this.saveListToolStripMenuItem,
            this.toolStripSeparator3,
            this.importToolStripMenuItem,
            this.exportToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(31, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // openListToolStripMenuItem
            // 
            this.openListToolStripMenuItem.Name = "openListToolStripMenuItem";
            this.openListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.openListToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.openListToolStripMenuItem.Text = "&Open List";
            this.openListToolStripMenuItem.Click += new System.EventHandler(this.openListToolStripMenuItem_Click);
            // 
            // saveListToolStripMenuItem
            // 
            this.saveListToolStripMenuItem.Name = "saveListToolStripMenuItem";
            this.saveListToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift) 
            | System.Windows.Forms.Keys.S)));
            this.saveListToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.saveListToolStripMenuItem.Text = "&Save List";
            this.saveListToolStripMenuItem.Click += new System.EventHandler(this.saveListToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(206, 6);
            // 
            // importToolStripMenuItem
            // 
            this.importToolStripMenuItem.Name = "importToolStripMenuItem";
            this.importToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.importToolStripMenuItem.Text = "&Import";
            this.importToolStripMenuItem.Click += new System.EventHandler(this.importToolStripMenuItem_Click);
            // 
            // exportToolStripMenuItem
            // 
            this.exportToolStripMenuItem.Name = "exportToolStripMenuItem";
            this.exportToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exportToolStripMenuItem.Text = "&Export";
            this.exportToolStripMenuItem.Click += new System.EventHandler(this.exportToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(206, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Q)));
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(209, 22);
            this.exitToolStripMenuItem.Text = "&Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // feedToolStripMenuItem
            // 
            this.feedToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addToolStripMenuItem,
            this.toolStripSeparator4,
            this.updateToolStripMenuItem,
            this.cancelUpdateToolStripMenuItem,
            this.toolStripSeparator5,
            this.markToolStripMenuItem,
            this.openExternallyToolStripMenuItem,
            this.toolStripSeparator9,
            this.resetAllEntriesToolStripMenuItem});
            this.feedToolStripMenuItem.Name = "feedToolStripMenuItem";
            this.feedToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.feedToolStripMenuItem.Text = "&Feeds";
            // 
            // addToolStripMenuItem
            // 
            this.addToolStripMenuItem.Name = "addToolStripMenuItem";
            this.addToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N)));
            this.addToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.addToolStripMenuItem.Text = "add ...";
            this.addToolStripMenuItem.Click += new System.EventHandler(this.addToolStripMenuItem_Click);
            // 
            // toolStripSeparator4
            // 
            this.toolStripSeparator4.Name = "toolStripSeparator4";
            this.toolStripSeparator4.Size = new System.Drawing.Size(144, 6);
            // 
            // updateToolStripMenuItem
            // 
            this.updateToolStripMenuItem.Name = "updateToolStripMenuItem";
            this.updateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.updateToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.updateToolStripMenuItem.Text = "update";
            this.updateToolStripMenuItem.Click += new System.EventHandler(this.updateToolStripMenuItem_Click);
            // 
            // cancelUpdateToolStripMenuItem
            // 
            this.cancelUpdateToolStripMenuItem.Enabled = false;
            this.cancelUpdateToolStripMenuItem.Name = "cancelUpdateToolStripMenuItem";
            this.cancelUpdateToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F4;
            this.cancelUpdateToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.cancelUpdateToolStripMenuItem.Text = "cancel update";
            this.cancelUpdateToolStripMenuItem.Click += new System.EventHandler(this.cancelUpdateToolStripMenuItem_Click);
            // 
            // toolStripSeparator5
            // 
            this.toolStripSeparator5.Name = "toolStripSeparator5";
            this.toolStripSeparator5.Size = new System.Drawing.Size(144, 6);
            // 
            // markToolStripMenuItem
            // 
            this.markToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MarkAllAsReadToolStripMenuItem});
            this.markToolStripMenuItem.Name = "markToolStripMenuItem";
            this.markToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.markToolStripMenuItem.Text = "mark ...";
            // 
            // MarkAllAsReadToolStripMenuItem
            // 
            this.MarkAllAsReadToolStripMenuItem.Name = "MarkAllAsReadToolStripMenuItem";
            this.MarkAllAsReadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F7;
            this.MarkAllAsReadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.MarkAllAsReadToolStripMenuItem.Text = "all as read";
            this.MarkAllAsReadToolStripMenuItem.Click += new System.EventHandler(this.MarkAllAsReadToolStripMenuItem_Click);
            // 
            // openExternallyToolStripMenuItem
            // 
            this.openExternallyToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.OpenAllUnreadToolStripMenuItem});
            this.openExternallyToolStripMenuItem.Name = "openExternallyToolStripMenuItem";
            this.openExternallyToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.openExternallyToolStripMenuItem.Text = "open externally";
            // 
            // OpenAllUnreadToolStripMenuItem
            // 
            this.OpenAllUnreadToolStripMenuItem.Name = "OpenAllUnreadToolStripMenuItem";
            this.OpenAllUnreadToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F6;
            this.OpenAllUnreadToolStripMenuItem.Size = new System.Drawing.Size(128, 22);
            this.OpenAllUnreadToolStripMenuItem.Text = "all unread";
            this.OpenAllUnreadToolStripMenuItem.Click += new System.EventHandler(this.OpenAllUnreadToolStripMenuItem_Click);
            // 
            // toolStripSeparator9
            // 
            this.toolStripSeparator9.Name = "toolStripSeparator9";
            this.toolStripSeparator9.Size = new System.Drawing.Size(144, 6);
            // 
            // resetAllEntriesToolStripMenuItem
            // 
            this.resetAllEntriesToolStripMenuItem.Name = "resetAllEntriesToolStripMenuItem";
            this.resetAllEntriesToolStripMenuItem.Size = new System.Drawing.Size(147, 22);
            this.resetAllEntriesToolStripMenuItem.Text = "reset all entries";
            this.resetAllEntriesToolStripMenuItem.Click += new System.EventHandler(this.ResetAllEntries);
            // 
            // extrasToolStripMenuItem
            // 
            this.extrasToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator2,
            this.webpageFeedDefinitionsToolStripMenuItem,
            this.toolStripSeparator8,
            this.settingsToolStripMenuItem});
            this.extrasToolStripMenuItem.Name = "extrasToolStripMenuItem";
            this.extrasToolStripMenuItem.Size = new System.Drawing.Size(42, 20);
            this.extrasToolStripMenuItem.Text = "&Extras";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F1;
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(184, 6);
            // 
            // webpageFeedDefinitionsToolStripMenuItem
            // 
            this.webpageFeedDefinitionsToolStripMenuItem.Name = "webpageFeedDefinitionsToolStripMenuItem";
            this.webpageFeedDefinitionsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.webpageFeedDefinitionsToolStripMenuItem.Text = "Webpage-Feed-Definitions";
            this.webpageFeedDefinitionsToolStripMenuItem.Click += new System.EventHandler(this.webpageFeedDefinitionsToolStripMenuItem_Click);
            // 
            // toolStripSeparator8
            // 
            this.toolStripSeparator8.Name = "toolStripSeparator8";
            this.toolStripSeparator8.Size = new System.Drawing.Size(184, 6);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(187, 22);
            this.settingsToolStripMenuItem.Text = "&Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mainProgressBar,
            this.statusLabel});
            this.statusStrip1.Location = new System.Drawing.Point(0, 428);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(800, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // mainProgressBar
            // 
            this.mainProgressBar.Name = "mainProgressBar";
            this.mainProgressBar.Size = new System.Drawing.Size(100, 16);
            // 
            // statusLabel
            // 
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(11, 17);
            this.statusLabel.Text = "...";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 24);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.tVMain);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.lVFeedItems);
            this.splitContainer1.Size = new System.Drawing.Size(800, 404);
            this.splitContainer1.SplitterDistance = 157;
            this.splitContainer1.TabIndex = 2;
            // 
            // tVMain
            // 
            this.tVMain.ContextMenuStrip = this.cMS_Treeview;
            this.tVMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tVMain.Location = new System.Drawing.Point(0, 0);
            this.tVMain.Name = "tVMain";
            this.tVMain.Size = new System.Drawing.Size(157, 404);
            this.tVMain.TabIndex = 0;
            this.tVMain.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.TreeView_FeedGroupAfterSelect);
            // 
            // cMS_Treeview
            // 
            this.cMS_Treeview.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openAllUnreadToolStripMenuItem1,
            this.cMS_MarkAsRead,
            this.cMS_Update,
            this.toolStripSeparator6,
            this.cMS_Edit,
            this.toolStripSeparator7,
            this.cMS_Delete});
            this.cMS_Treeview.Name = "cMS_Treeview";
            this.cMS_Treeview.Size = new System.Drawing.Size(142, 126);
            // 
            // openAllUnreadToolStripMenuItem1
            // 
            this.openAllUnreadToolStripMenuItem1.Name = "openAllUnreadToolStripMenuItem1";
            this.openAllUnreadToolStripMenuItem1.Size = new System.Drawing.Size(141, 22);
            this.openAllUnreadToolStripMenuItem1.Text = "open all unread";
            this.openAllUnreadToolStripMenuItem1.Click += new System.EventHandler(this.openAllUnreadToolStripMenuItem1_Click);
            // 
            // cMS_MarkAsRead
            // 
            this.cMS_MarkAsRead.Name = "cMS_MarkAsRead";
            this.cMS_MarkAsRead.RightToLeftAutoMirrorImage = true;
            this.cMS_MarkAsRead.ShortcutKeys = System.Windows.Forms.Keys.F8;
            this.cMS_MarkAsRead.Size = new System.Drawing.Size(141, 22);
            this.cMS_MarkAsRead.Text = "mark as read";
            this.cMS_MarkAsRead.Click += new System.EventHandler(this.cMS_MarkAsRead_Click);
            // 
            // cMS_Update
            // 
            this.cMS_Update.Name = "cMS_Update";
            this.cMS_Update.Size = new System.Drawing.Size(141, 22);
            this.cMS_Update.Text = "update";
            this.cMS_Update.Click += new System.EventHandler(this.cMS_Update_Click);
            // 
            // toolStripSeparator6
            // 
            this.toolStripSeparator6.Name = "toolStripSeparator6";
            this.toolStripSeparator6.Size = new System.Drawing.Size(138, 6);
            // 
            // cMS_Edit
            // 
            this.cMS_Edit.Name = "cMS_Edit";
            this.cMS_Edit.Size = new System.Drawing.Size(141, 22);
            this.cMS_Edit.Text = "edit";
            this.cMS_Edit.Click += new System.EventHandler(this.cMS_Rename_Click);
            // 
            // toolStripSeparator7
            // 
            this.toolStripSeparator7.Name = "toolStripSeparator7";
            this.toolStripSeparator7.Size = new System.Drawing.Size(138, 6);
            // 
            // cMS_Delete
            // 
            this.cMS_Delete.Name = "cMS_Delete";
            this.cMS_Delete.Size = new System.Drawing.Size(141, 22);
            this.cMS_Delete.Text = "delete";
            this.cMS_Delete.Click += new System.EventHandler(this.cMS_Delete_Click);
            // 
            // lVFeedItems
            // 
            this.lVFeedItems.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.cH_Title});
            this.lVFeedItems.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lVFeedItems.Location = new System.Drawing.Point(0, 0);
            this.lVFeedItems.MultiSelect = false;
            this.lVFeedItems.Name = "lVFeedItems";
            this.lVFeedItems.Size = new System.Drawing.Size(639, 404);
            this.lVFeedItems.TabIndex = 0;
            this.lVFeedItems.UseCompatibleStateImageBehavior = false;
            this.lVFeedItems.View = System.Windows.Forms.View.Details;
            this.lVFeedItems.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lVFeedItems_MouseDoubleClick);
            // 
            // cH_Title
            // 
            this.cH_Title.Text = "Title";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "Feed Bee";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.cMS_Treeview.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem importToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem feedToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem addToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem extrasToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.TreeView tVMain;
        private System.Windows.Forms.ListView lVFeedItems;
        private System.Windows.Forms.ToolStripMenuItem openListToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveListToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
        private System.Windows.Forms.ToolStripMenuItem updateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator5;
        private System.Windows.Forms.ToolStripMenuItem markToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem MarkAllAsReadToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openExternallyToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem OpenAllUnreadToolStripMenuItem;
        private TimedStatusLabel statusLabel;
        private System.Windows.Forms.ContextMenuStrip cMS_Treeview;
        private System.Windows.Forms.ToolStripMenuItem cMS_MarkAsRead;
        private System.Windows.Forms.ToolStripMenuItem cMS_Update;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator6;
        private System.Windows.Forms.ToolStripMenuItem cMS_Edit;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator7;
        private System.Windows.Forms.ToolStripMenuItem cMS_Delete;
        private System.Windows.Forms.ToolStripMenuItem openAllUnreadToolStripMenuItem1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator8;
        private System.Windows.Forms.ToolStripMenuItem webpageFeedDefinitionsToolStripMenuItem;
        private System.Windows.Forms.ToolStripProgressBar mainProgressBar;
        private System.Windows.Forms.ToolStripMenuItem cancelUpdateToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator9;
        private System.Windows.Forms.ToolStripMenuItem resetAllEntriesToolStripMenuItem;
        private System.Windows.Forms.ColumnHeader cH_Title;
    }
}

