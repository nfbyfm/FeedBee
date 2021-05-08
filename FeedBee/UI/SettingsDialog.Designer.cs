namespace FeedBee.UI
{
    partial class SettingsDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.b_Reset = new System.Windows.Forms.Button();
            this.b_Save = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.cB_LoadUponStartup = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.bSelectFeedList = new System.Windows.Forms.Button();
            this.tB_FeedListPath = new System.Windows.Forms.TextBox();
            this.cB_UpdateNSFW = new System.Windows.Forms.CheckBox();
            this.cB_UpdateUponLoad = new System.Windows.Forms.CheckBox();
            this.cB_DisplayFeedIcons = new System.Windows.Forms.CheckBox();
            this.cB_ExpandNodes = new System.Windows.Forms.CheckBox();
            this.tC_Main = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.mTB_autoUpdateTimeSpan = new System.Windows.Forms.MaskedTextBox();
            this.l_autoUpdateInterval = new System.Windows.Forms.Label();
            this.cB_automaticUpdate = new System.Windows.Forms.CheckBox();
            this.bSelectFeedDefFilePath = new System.Windows.Forms.Button();
            this.tB_FeedDefFilePath = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.b_SelectIconFolder = new System.Windows.Forms.Button();
            this.tB_IconFolder = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.toolTipUpdateTime = new System.Windows.Forms.ToolTip(this.components);
            this.tC_Main.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // b_Reset
            // 
            this.b_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_Reset.Location = new System.Drawing.Point(12, 228);
            this.b_Reset.Name = "b_Reset";
            this.b_Reset.Size = new System.Drawing.Size(75, 23);
            this.b_Reset.TabIndex = 0;
            this.b_Reset.Text = "reset";
            this.b_Reset.UseVisualStyleBackColor = true;
            this.b_Reset.Click += new System.EventHandler(this.b_Reset_Click);
            // 
            // b_Save
            // 
            this.b_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Save.Location = new System.Drawing.Point(404, 228);
            this.b_Save.Name = "b_Save";
            this.b_Save.Size = new System.Drawing.Size(75, 23);
            this.b_Save.TabIndex = 1;
            this.b_Save.Text = "Save";
            this.b_Save.UseVisualStyleBackColor = true;
            this.b_Save.Click += new System.EventHandler(this.b_Save_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.Location = new System.Drawing.Point(485, 228);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 23);
            this.b_Cancel.TabIndex = 2;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // cB_LoadUponStartup
            // 
            this.cB_LoadUponStartup.AutoSize = true;
            this.cB_LoadUponStartup.Location = new System.Drawing.Point(6, 6);
            this.cB_LoadUponStartup.Name = "cB_LoadUponStartup";
            this.cB_LoadUponStartup.Size = new System.Drawing.Size(214, 17);
            this.cB_LoadUponStartup.TabIndex = 6;
            this.cB_LoadUponStartup.Text = "load / save list upon startup / shutdown";
            this.cB_LoadUponStartup.UseVisualStyleBackColor = true;
            this.cB_LoadUponStartup.CheckedChanged += new System.EventHandler(this.cB_LoadUponStartup_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(29, 32);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(74, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "FeedList-path:";
            // 
            // bSelectFeedList
            // 
            this.bSelectFeedList.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelectFeedList.Location = new System.Drawing.Point(459, 27);
            this.bSelectFeedList.Name = "bSelectFeedList";
            this.bSelectFeedList.Size = new System.Drawing.Size(75, 23);
            this.bSelectFeedList.TabIndex = 1;
            this.bSelectFeedList.Text = "select ...";
            this.bSelectFeedList.UseVisualStyleBackColor = true;
            this.bSelectFeedList.Click += new System.EventHandler(this.bSelectFeedList_Click);
            // 
            // tB_FeedListPath
            // 
            this.tB_FeedListPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_FeedListPath.Location = new System.Drawing.Point(135, 29);
            this.tB_FeedListPath.Name = "tB_FeedListPath";
            this.tB_FeedListPath.Size = new System.Drawing.Size(318, 20);
            this.tB_FeedListPath.TabIndex = 0;
            // 
            // cB_UpdateNSFW
            // 
            this.cB_UpdateNSFW.AutoSize = true;
            this.cB_UpdateNSFW.Location = new System.Drawing.Point(6, 55);
            this.cB_UpdateNSFW.Name = "cB_UpdateNSFW";
            this.cB_UpdateNSFW.Size = new System.Drawing.Size(113, 17);
            this.cB_UpdateNSFW.TabIndex = 2;
            this.cB_UpdateNSFW.Text = "update nsfw-feeds";
            this.cB_UpdateNSFW.UseVisualStyleBackColor = true;
            // 
            // cB_UpdateUponLoad
            // 
            this.cB_UpdateUponLoad.AutoSize = true;
            this.cB_UpdateUponLoad.Location = new System.Drawing.Point(6, 78);
            this.cB_UpdateUponLoad.Name = "cB_UpdateUponLoad";
            this.cB_UpdateUponLoad.Size = new System.Drawing.Size(138, 17);
            this.cB_UpdateUponLoad.TabIndex = 3;
            this.cB_UpdateUponLoad.Text = "update feeds upon load";
            this.cB_UpdateUponLoad.UseVisualStyleBackColor = true;
            // 
            // cB_DisplayFeedIcons
            // 
            this.cB_DisplayFeedIcons.AutoSize = true;
            this.cB_DisplayFeedIcons.Location = new System.Drawing.Point(3, 29);
            this.cB_DisplayFeedIcons.Name = "cB_DisplayFeedIcons";
            this.cB_DisplayFeedIcons.Size = new System.Drawing.Size(183, 17);
            this.cB_DisplayFeedIcons.TabIndex = 1;
            this.cB_DisplayFeedIcons.Text = "display feed-icons where possible";
            this.cB_DisplayFeedIcons.UseVisualStyleBackColor = true;
            this.cB_DisplayFeedIcons.CheckedChanged += new System.EventHandler(this.cB_DisplayFeedIcons_CheckedChanged);
            // 
            // cB_ExpandNodes
            // 
            this.cB_ExpandNodes.AutoSize = true;
            this.cB_ExpandNodes.Location = new System.Drawing.Point(6, 6);
            this.cB_ExpandNodes.Name = "cB_ExpandNodes";
            this.cB_ExpandNodes.Size = new System.Drawing.Size(108, 17);
            this.cB_ExpandNodes.TabIndex = 0;
            this.cB_ExpandNodes.Text = "expand Treeview";
            this.cB_ExpandNodes.UseVisualStyleBackColor = true;
            // 
            // tC_Main
            // 
            this.tC_Main.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tC_Main.Controls.Add(this.tabPage1);
            this.tC_Main.Controls.Add(this.tabPage2);
            this.tC_Main.Location = new System.Drawing.Point(12, 12);
            this.tC_Main.Name = "tC_Main";
            this.tC_Main.SelectedIndex = 0;
            this.tC_Main.Size = new System.Drawing.Size(548, 210);
            this.tC_Main.TabIndex = 15;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.mTB_autoUpdateTimeSpan);
            this.tabPage1.Controls.Add(this.l_autoUpdateInterval);
            this.tabPage1.Controls.Add(this.cB_automaticUpdate);
            this.tabPage1.Controls.Add(this.bSelectFeedDefFilePath);
            this.tabPage1.Controls.Add(this.tB_FeedDefFilePath);
            this.tabPage1.Controls.Add(this.label4);
            this.tabPage1.Controls.Add(this.cB_LoadUponStartup);
            this.tabPage1.Controls.Add(this.cB_UpdateUponLoad);
            this.tabPage1.Controls.Add(this.label2);
            this.tabPage1.Controls.Add(this.cB_UpdateNSFW);
            this.tabPage1.Controls.Add(this.tB_FeedListPath);
            this.tabPage1.Controls.Add(this.bSelectFeedList);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(540, 184);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Feeds";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // mTB_autoUpdateTimeSpan
            // 
            this.mTB_autoUpdateTimeSpan.Culture = new System.Globalization.CultureInfo("");
            this.mTB_autoUpdateTimeSpan.Location = new System.Drawing.Point(135, 150);
            this.mTB_autoUpdateTimeSpan.Mask = "00:00:00";
            this.mTB_autoUpdateTimeSpan.Name = "mTB_autoUpdateTimeSpan";
            this.mTB_autoUpdateTimeSpan.Size = new System.Drawing.Size(61, 20);
            this.mTB_autoUpdateTimeSpan.TabIndex = 7;
            this.toolTipUpdateTime.SetToolTip(this.mTB_autoUpdateTimeSpan, "Updateinterval: HH:mm:ss => hours : minutes : seconds");
            this.mTB_autoUpdateTimeSpan.ValidatingType = typeof(System.DateTime);
            // 
            // l_autoUpdateInterval
            // 
            this.l_autoUpdateInterval.AutoSize = true;
            this.l_autoUpdateInterval.Location = new System.Drawing.Point(29, 153);
            this.l_autoUpdateInterval.Name = "l_autoUpdateInterval";
            this.l_autoUpdateInterval.Size = new System.Drawing.Size(79, 13);
            this.l_autoUpdateInterval.TabIndex = 18;
            this.l_autoUpdateInterval.Text = "Updateinterval:";
            // 
            // cB_automaticUpdate
            // 
            this.cB_automaticUpdate.AutoSize = true;
            this.cB_automaticUpdate.Location = new System.Drawing.Point(6, 127);
            this.cB_automaticUpdate.Name = "cB_automaticUpdate";
            this.cB_automaticUpdate.Size = new System.Drawing.Size(155, 17);
            this.cB_automaticUpdate.TabIndex = 6;
            this.cB_automaticUpdate.Text = "automatically update Feeds";
            this.cB_automaticUpdate.UseVisualStyleBackColor = true;
            this.cB_automaticUpdate.CheckedChanged += new System.EventHandler(this.cB_automaticUpdate_CheckedChanged);
            // 
            // bSelectFeedDefFilePath
            // 
            this.bSelectFeedDefFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelectFeedDefFilePath.Location = new System.Drawing.Point(459, 99);
            this.bSelectFeedDefFilePath.Name = "bSelectFeedDefFilePath";
            this.bSelectFeedDefFilePath.Size = new System.Drawing.Size(75, 23);
            this.bSelectFeedDefFilePath.TabIndex = 5;
            this.bSelectFeedDefFilePath.Text = "select ...";
            this.bSelectFeedDefFilePath.UseVisualStyleBackColor = true;
            this.bSelectFeedDefFilePath.Click += new System.EventHandler(this.bSelectFeedDefFilePath_Click);
            // 
            // tB_FeedDefFilePath
            // 
            this.tB_FeedDefFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_FeedDefFilePath.Location = new System.Drawing.Point(135, 101);
            this.tB_FeedDefFilePath.Name = "tB_FeedDefFilePath";
            this.tB_FeedDefFilePath.Size = new System.Drawing.Size(318, 20);
            this.tB_FeedDefFilePath.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(29, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(100, 13);
            this.label4.TabIndex = 12;
            this.label4.Text = "Feed-Definition-File:";
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.b_SelectIconFolder);
            this.tabPage2.Controls.Add(this.tB_IconFolder);
            this.tabPage2.Controls.Add(this.label3);
            this.tabPage2.Controls.Add(this.cB_DisplayFeedIcons);
            this.tabPage2.Controls.Add(this.cB_ExpandNodes);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(540, 184);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "View";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // b_SelectIconFolder
            // 
            this.b_SelectIconFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_SelectIconFolder.Location = new System.Drawing.Point(457, 44);
            this.b_SelectIconFolder.Name = "b_SelectIconFolder";
            this.b_SelectIconFolder.Size = new System.Drawing.Size(75, 23);
            this.b_SelectIconFolder.TabIndex = 3;
            this.b_SelectIconFolder.Text = "select ...";
            this.b_SelectIconFolder.UseVisualStyleBackColor = true;
            this.b_SelectIconFolder.Click += new System.EventHandler(this.b_SelectIconFolder_Click);
            // 
            // tB_IconFolder
            // 
            this.tB_IconFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_IconFolder.Location = new System.Drawing.Point(132, 46);
            this.tB_IconFolder.Name = "tB_IconFolder";
            this.tB_IconFolder.Size = new System.Drawing.Size(319, 20);
            this.tB_IconFolder.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 49);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(106, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Fodler for feed-icons:";
            // 
            // toolTipUpdateTime
            // 
            this.toolTipUpdateTime.ToolTipTitle = "HH:mm:ss";
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(572, 263);
            this.Controls.Add(this.tC_Main);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_Save);
            this.Controls.Add(this.b_Reset);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.tC_Main.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button b_Reset;
        private System.Windows.Forms.Button b_Save;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.CheckBox cB_LoadUponStartup;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button bSelectFeedList;
        private System.Windows.Forms.TextBox tB_FeedListPath;
        private System.Windows.Forms.CheckBox cB_UpdateNSFW;
        private System.Windows.Forms.CheckBox cB_UpdateUponLoad;
        private System.Windows.Forms.CheckBox cB_DisplayFeedIcons;
        private System.Windows.Forms.CheckBox cB_ExpandNodes;
        private System.Windows.Forms.TabControl tC_Main;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.Button b_SelectIconFolder;
        private System.Windows.Forms.TextBox tB_IconFolder;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button bSelectFeedDefFilePath;
        private System.Windows.Forms.TextBox tB_FeedDefFilePath;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.MaskedTextBox mTB_autoUpdateTimeSpan;
        private System.Windows.Forms.Label l_autoUpdateInterval;
        private System.Windows.Forms.CheckBox cB_automaticUpdate;
        private System.Windows.Forms.ToolTip toolTipUpdateTime;
    }
}