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
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(23, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Url:";
            // 
            // tB_Url
            // 
            this.tB_Url.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_Url.Location = new System.Drawing.Point(42, 10);
            this.tB_Url.Name = "tB_Url";
            this.tB_Url.Size = new System.Drawing.Size(267, 20);
            this.tB_Url.TabIndex = 1;
            this.tB_Url.MouseClick += new System.Windows.Forms.MouseEventHandler(this.tB_Url_MouseClick);
            // 
            // lb_FoundFeeds
            // 
            this.lb_FoundFeeds.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lb_FoundFeeds.FormattingEnabled = true;
            this.lb_FoundFeeds.Location = new System.Drawing.Point(85, 46);
            this.lb_FoundFeeds.Name = "lb_FoundFeeds";
            this.lb_FoundFeeds.Size = new System.Drawing.Size(343, 147);
            this.lb_FoundFeeds.TabIndex = 2;
            // 
            // b_checkFeeds
            // 
            this.b_checkFeeds.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_checkFeeds.Location = new System.Drawing.Point(316, 8);
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
            this.label2.Location = new System.Drawing.Point(13, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(66, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "found feeds:";
            // 
            // b_Next
            // 
            this.b_Next.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Next.Location = new System.Drawing.Point(272, 202);
            this.b_Next.Name = "b_Next";
            this.b_Next.Size = new System.Drawing.Size(75, 23);
            this.b_Next.TabIndex = 5;
            this.b_Next.Text = "Next";
            this.b_Next.UseVisualStyleBackColor = true;
            this.b_Next.Click += new System.EventHandler(this.b_Next_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.Location = new System.Drawing.Point(353, 202);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 23);
            this.b_Cancel.TabIndex = 6;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // AddFeedDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(440, 237);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_Next);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.b_checkFeeds);
            this.Controls.Add(this.lb_FoundFeeds);
            this.Controls.Add(this.tB_Url);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "AddFeedDialog";
            this.Text = "Add new Feed";
            this.Load += new System.EventHandler(this.AddFeedDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox tB_Url;
        private System.Windows.Forms.ListBox lb_FoundFeeds;
        private System.Windows.Forms.Button b_checkFeeds;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button b_Next;
        private System.Windows.Forms.Button b_Cancel;
    }
}