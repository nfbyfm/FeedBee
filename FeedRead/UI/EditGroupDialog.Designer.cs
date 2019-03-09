namespace FeedRead.UI
{
    partial class EditGroupDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditGroupDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.bOK = new System.Windows.Forms.Button();
            this.bCancel = new System.Windows.Forms.Button();
            this.tB_NewName = new System.Windows.Forms.TextBox();
            this.cB_MarkNSFW = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_IconPath = new System.Windows.Forms.TextBox();
            this.bSelectIconpath = new System.Windows.Forms.Button();
            this.cB_SetIconForFeeds = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // bOK
            // 
            this.bOK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bOK.Location = new System.Drawing.Point(208, 130);
            this.bOK.Name = "bOK";
            this.bOK.Size = new System.Drawing.Size(75, 23);
            this.bOK.TabIndex = 1;
            this.bOK.Text = "OK";
            this.bOK.UseVisualStyleBackColor = true;
            this.bOK.Click += new System.EventHandler(this.bOK_Click);
            // 
            // bCancel
            // 
            this.bCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.bCancel.Location = new System.Drawing.Point(289, 130);
            this.bCancel.Name = "bCancel";
            this.bCancel.Size = new System.Drawing.Size(75, 23);
            this.bCancel.TabIndex = 2;
            this.bCancel.Text = "Cancel";
            this.bCancel.UseVisualStyleBackColor = true;
            this.bCancel.Click += new System.EventHandler(this.bCancel_Click);
            // 
            // tB_NewName
            // 
            this.tB_NewName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_NewName.Location = new System.Drawing.Point(48, 21);
            this.tB_NewName.Name = "tB_NewName";
            this.tB_NewName.Size = new System.Drawing.Size(316, 20);
            this.tB_NewName.TabIndex = 3;
            // 
            // cB_MarkNSFW
            // 
            this.cB_MarkNSFW.AutoSize = true;
            this.cB_MarkNSFW.Location = new System.Drawing.Point(12, 77);
            this.cB_MarkNSFW.Name = "cB_MarkNSFW";
            this.cB_MarkNSFW.Size = new System.Drawing.Size(58, 17);
            this.cB_MarkNSFW.TabIndex = 4;
            this.cB_MarkNSFW.Text = "is nsfw";
            this.cB_MarkNSFW.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 52);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Icon-path:";
            // 
            // tB_IconPath
            // 
            this.tB_IconPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_IconPath.Location = new System.Drawing.Point(73, 49);
            this.tB_IconPath.Name = "tB_IconPath";
            this.tB_IconPath.Size = new System.Drawing.Size(210, 20);
            this.tB_IconPath.TabIndex = 6;
            // 
            // bSelectIconpath
            // 
            this.bSelectIconpath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bSelectIconpath.Location = new System.Drawing.Point(289, 47);
            this.bSelectIconpath.Name = "bSelectIconpath";
            this.bSelectIconpath.Size = new System.Drawing.Size(75, 23);
            this.bSelectIconpath.TabIndex = 7;
            this.bSelectIconpath.Text = "select ...";
            this.bSelectIconpath.UseVisualStyleBackColor = true;
            this.bSelectIconpath.Click += new System.EventHandler(this.bSelectIconpath_Click);
            // 
            // cB_SetIconForFeeds
            // 
            this.cB_SetIconForFeeds.AutoSize = true;
            this.cB_SetIconForFeeds.Location = new System.Drawing.Point(12, 100);
            this.cB_SetIconForFeeds.Name = "cB_SetIconForFeeds";
            this.cB_SetIconForFeeds.Size = new System.Drawing.Size(149, 17);
            this.cB_SetIconForFeeds.TabIndex = 8;
            this.cB_SetIconForFeeds.Text = "set same Icon for all feeds";
            this.cB_SetIconForFeeds.UseVisualStyleBackColor = true;
            // 
            // EditGroupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(376, 165);
            this.Controls.Add(this.cB_SetIconForFeeds);
            this.Controls.Add(this.bSelectIconpath);
            this.Controls.Add(this.tB_IconPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cB_MarkNSFW);
            this.Controls.Add(this.tB_NewName);
            this.Controls.Add(this.bCancel);
            this.Controls.Add(this.bOK);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditGroupDialog";
            this.Text = "Edit Feedgroup";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button bOK;
        private System.Windows.Forms.Button bCancel;
        private System.Windows.Forms.TextBox tB_NewName;
        private System.Windows.Forms.CheckBox cB_MarkNSFW;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tB_IconPath;
        private System.Windows.Forms.Button bSelectIconpath;
        private System.Windows.Forms.CheckBox cB_SetIconForFeeds;
    }
}