namespace FeedRead.UI
{
    partial class AddFeedDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AddFeedDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.tB_Url = new System.Windows.Forms.TextBox();
            this.lb_FoundFeeds = new System.Windows.Forms.ListBox();
            this.b_checkFeeds = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.b_Next = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.cB_GroupIsNSFW = new System.Windows.Forms.CheckBox();
            this.tB_NewGroupName = new System.Windows.Forms.TextBox();
            this.cB_Groups = new System.Windows.Forms.ComboBox();
            this.rB_CreateNewGroup = new System.Windows.Forms.RadioButton();
            this.rB_addToGroup = new System.Windows.Forms.RadioButton();
            this.gB_FeedGroup = new System.Windows.Forms.GroupBox();
            this.groupBox1.SuspendLayout();
            this.gB_FeedGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Url:";
            // 
            // tB_Url
            // 
            this.tB_Url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Url.Location = new System.Drawing.Point(35, 24);
            this.tB_Url.Name = "tB_Url";
            this.tB_Url.Size = new System.Drawing.Size(291, 20);
            this.tB_Url.TabIndex = 1;
            this.tB_Url.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tB_Url_MouseClick);
            // 
            // lb_FoundFeeds
            // 
            this.lb_FoundFeeds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_FoundFeeds.FormattingEnabled = true;
            this.lb_FoundFeeds.Location = new System.Drawing.Point(78, 60);
            this.lb_FoundFeeds.Name = "lb_FoundFeeds";
            this.lb_FoundFeeds.Size = new System.Drawing.Size(366, 69);
            this.lb_FoundFeeds.TabIndex = 2;
            // 
            // b_checkFeeds
            // 
            this.b_checkFeeds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_checkFeeds.Location = new System.Drawing.Point(332, 22);
            this.b_checkFeeds.Name = "b_checkFeeds";
            this.b_checkFeeds.Size = new System.Drawing.Size(112, 23);
            this.b_checkFeeds.TabIndex = 3;
            this.b_checkFeeds.Text = "check for feeds";
            this.b_checkFeeds.UseVisualStyleBackColor = true;
            this.b_checkFeeds.Click += new System.EventHandler(this.b_checkFeeds_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "found feeds:";
            // 
            // b_Next
            // 
            this.b_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Next.Location = new System.Drawing.Point(306, 254);
            this.b_Next.Name = "b_Next";
            this.b_Next.Size = new System.Drawing.Size(75, 23);
            this.b_Next.TabIndex = 5;
            this.b_Next.Text = "Add";
            this.b_Next.UseVisualStyleBackColor = true;
            this.b_Next.Click += new System.EventHandler(this.b_Add_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.Location = new System.Drawing.Point(387, 254);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 23);
            this.b_Cancel.TabIndex = 6;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.tB_Url);
            this.groupBox1.Controls.Add(this.lb_FoundFeeds);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.b_checkFeeds);
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(450, 143);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Feed";
            // 
            // cB_GroupIsNSFW
            // 
            this.cB_GroupIsNSFW.AutoSize = true;
            this.cB_GroupIsNSFW.Location = new System.Drawing.Point(341, 52);
            this.cB_GroupIsNSFW.Name = "cB_GroupIsNSFW";
            this.cB_GroupIsNSFW.Size = new System.Drawing.Size(90, 17);
            this.cB_GroupIsNSFW.TabIndex = 15;
            this.cB_GroupIsNSFW.Text = "Group is nsfw";
            this.cB_GroupIsNSFW.UseVisualStyleBackColor = true;
            // 
            // tB_NewGroupName
            // 
            this.tB_NewGroupName.Location = new System.Drawing.Point(138, 50);
            this.tB_NewGroupName.Name = "tB_NewGroupName";
            this.tB_NewGroupName.Size = new System.Drawing.Size(177, 20);
            this.tB_NewGroupName.TabIndex = 13;
            // 
            // cB_Groups
            // 
            this.cB_Groups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_Groups.FormattingEnabled = true;
            this.cB_Groups.Location = new System.Drawing.Point(138, 19);
            this.cB_Groups.Name = "cB_Groups";
            this.cB_Groups.Size = new System.Drawing.Size(177, 21);
            this.cB_Groups.TabIndex = 10;
            // 
            // rB_CreateNewGroup
            // 
            this.rB_CreateNewGroup.AutoSize = true;
            this.rB_CreateNewGroup.Location = new System.Drawing.Point(6, 51);
            this.rB_CreateNewGroup.Name = "rB_CreateNewGroup";
            this.rB_CreateNewGroup.Size = new System.Drawing.Size(111, 17);
            this.rB_CreateNewGroup.TabIndex = 9;
            this.rB_CreateNewGroup.TabStop = true;
            this.rB_CreateNewGroup.Text = "create new group:";
            this.rB_CreateNewGroup.UseVisualStyleBackColor = true;
            this.rB_CreateNewGroup.CheckedChanged += new System.EventHandler(this.rB_CreateNewGroup_CheckedChanged);
            // 
            // rB_addToGroup
            // 
            this.rB_addToGroup.AutoSize = true;
            this.rB_addToGroup.Location = new System.Drawing.Point(6, 19);
            this.rB_addToGroup.Name = "rB_addToGroup";
            this.rB_addToGroup.Size = new System.Drawing.Size(88, 17);
            this.rB_addToGroup.TabIndex = 8;
            this.rB_addToGroup.TabStop = true;
            this.rB_addToGroup.Text = "add to group:";
            this.rB_addToGroup.UseVisualStyleBackColor = true;
            this.rB_addToGroup.CheckedChanged += new System.EventHandler(this.rB_addToGroup_CheckedChanged);
            // 
            // gB_FeedGroup
            // 
            this.gB_FeedGroup.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.gB_FeedGroup.Controls.Add(this.rB_addToGroup);
            this.gB_FeedGroup.Controls.Add(this.cB_GroupIsNSFW);
            this.gB_FeedGroup.Controls.Add(this.rB_CreateNewGroup);
            this.gB_FeedGroup.Controls.Add(this.tB_NewGroupName);
            this.gB_FeedGroup.Controls.Add(this.cB_Groups);
            this.gB_FeedGroup.Location = new System.Drawing.Point(12, 161);
            this.gB_FeedGroup.Name = "gB_FeedGroup";
            this.gB_FeedGroup.Size = new System.Drawing.Size(450, 87);
            this.gB_FeedGroup.TabIndex = 16;
            this.gB_FeedGroup.TabStop = false;
            this.gB_FeedGroup.Text = "Feedgroup";
            // 
            // AddFeedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(474, 289);
            this.Controls.Add(this.gB_FeedGroup);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_Next);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddFeedDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Add new Feed";
            this.Load += new System.EventHandler(this.AddFeedDialog_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gB_FeedGroup.ResumeLayout(false);
            this.gB_FeedGroup.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_Url;
        private System.Windows.Forms.ListBox lb_FoundFeeds;
        private System.Windows.Forms.Button b_checkFeeds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button b_Next;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.CheckBox cB_GroupIsNSFW;
        private System.Windows.Forms.TextBox tB_NewGroupName;
        private System.Windows.Forms.ComboBox cB_Groups;
        private System.Windows.Forms.RadioButton rB_CreateNewGroup;
        private System.Windows.Forms.RadioButton rB_addToGroup;
        private System.Windows.Forms.GroupBox gB_FeedGroup;
    }
}