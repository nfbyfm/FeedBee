namespace FeedRead.UI
{
    partial class EditFeedDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EditFeedDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.tB_FeedTitle = new System.Windows.Forms.TextBox();
            this.cB_DirectlyLoadWebURL = new System.Windows.Forms.CheckBox();
            this.b_OK = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.tB_IconPath = new System.Windows.Forms.TextBox();
            this.b_SelectIconPath = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(30, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Title:";
            // 
            // tB_FeedTitle
            // 
            this.tB_FeedTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_FeedTitle.Location = new System.Drawing.Point(49, 10);
            this.tB_FeedTitle.Name = "tB_FeedTitle";
            this.tB_FeedTitle.Size = new System.Drawing.Size(373, 20);
            this.tB_FeedTitle.TabIndex = 1;
            // 
            // cB_DirectlyLoadWebURL
            // 
            this.cB_DirectlyLoadWebURL.AutoSize = true;
            this.cB_DirectlyLoadWebURL.Location = new System.Drawing.Point(16, 36);
            this.cB_DirectlyLoadWebURL.Name = "cB_DirectlyLoadWebURL";
            this.cB_DirectlyLoadWebURL.Size = new System.Drawing.Size(190, 17);
            this.cB_DirectlyLoadWebURL.TabIndex = 2;
            this.cB_DirectlyLoadWebURL.Text = "directly load Webpage of feed-item";
            this.cB_DirectlyLoadWebURL.UseVisualStyleBackColor = true;
            // 
            // b_OK
            // 
            this.b_OK.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_OK.Location = new System.Drawing.Point(266, 106);
            this.b_OK.Name = "b_OK";
            this.b_OK.Size = new System.Drawing.Size(75, 23);
            this.b_OK.TabIndex = 3;
            this.b_OK.Text = "OK";
            this.b_OK.UseVisualStyleBackColor = true;
            this.b_OK.Click += new System.EventHandler(this.b_OK_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.Location = new System.Drawing.Point(347, 106);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 23);
            this.b_Cancel.TabIndex = 4;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 69);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(55, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Icon-path:";
            // 
            // tB_IconPath
            // 
            this.tB_IconPath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_IconPath.Location = new System.Drawing.Point(73, 66);
            this.tB_IconPath.Name = "tB_IconPath";
            this.tB_IconPath.Size = new System.Drawing.Size(268, 20);
            this.tB_IconPath.TabIndex = 6;
            // 
            // b_SelectIconPath
            // 
            this.b_SelectIconPath.Location = new System.Drawing.Point(347, 64);
            this.b_SelectIconPath.Name = "b_SelectIconPath";
            this.b_SelectIconPath.Size = new System.Drawing.Size(75, 23);
            this.b_SelectIconPath.TabIndex = 7;
            this.b_SelectIconPath.Text = "select ...";
            this.b_SelectIconPath.UseVisualStyleBackColor = true;
            this.b_SelectIconPath.Click += new System.EventHandler(this.b_SelectIconPath_Click);
            // 
            // EditFeedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(434, 141);
            this.Controls.Add(this.b_SelectIconPath);
            this.Controls.Add(this.tB_IconPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_OK);
            this.Controls.Add(this.cB_DirectlyLoadWebURL);
            this.Controls.Add(this.tB_FeedTitle);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "EditFeedDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Edit Feed";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_FeedTitle;
        private System.Windows.Forms.CheckBox cB_DirectlyLoadWebURL;
        private System.Windows.Forms.Button b_OK;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox tB_IconPath;
        private System.Windows.Forms.Button b_SelectIconPath;
    }
}