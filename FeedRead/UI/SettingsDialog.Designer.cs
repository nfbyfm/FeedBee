namespace FeedRead.UI
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingsDialog));
            this.label1 = new System.Windows.Forms.Label();
            this.b_SelectYTdl = new System.Windows.Forms.Button();
            this.tB_youtubedlFolder = new System.Windows.Forms.TextBox();
            this.b_Reset = new System.Windows.Forms.Button();
            this.b_Save = new System.Windows.Forms.Button();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "folder of youtube-dl.exe:";
            // 
            // b_SelectYTdl
            // 
            this.b_SelectYTdl.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.b_SelectYTdl.Location = new System.Drawing.Point(352, 4);
            this.b_SelectYTdl.Name = "b_SelectYTdl";
            this.b_SelectYTdl.Size = new System.Drawing.Size(75, 23);
            this.b_SelectYTdl.TabIndex = 1;
            this.b_SelectYTdl.Text = "select ...";
            this.b_SelectYTdl.UseVisualStyleBackColor = true;
            this.b_SelectYTdl.Click += new System.EventHandler(this.b_SelectYTdl_Click);
            // 
            // tB_youtubedlFolder
            // 
            this.tB_youtubedlFolder.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tB_youtubedlFolder.Location = new System.Drawing.Point(138, 6);
            this.tB_youtubedlFolder.Name = "tB_youtubedlFolder";
            this.tB_youtubedlFolder.Size = new System.Drawing.Size(208, 20);
            this.tB_youtubedlFolder.TabIndex = 2;
            // 
            // b_Reset
            // 
            this.b_Reset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.b_Reset.Location = new System.Drawing.Point(12, 85);
            this.b_Reset.Name = "b_Reset";
            this.b_Reset.Size = new System.Drawing.Size(75, 23);
            this.b_Reset.TabIndex = 3;
            this.b_Reset.Text = "reset";
            this.b_Reset.UseVisualStyleBackColor = true;
            this.b_Reset.Click += new System.EventHandler(this.b_Reset_Click);
            // 
            // b_Save
            // 
            this.b_Save.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Save.Location = new System.Drawing.Point(271, 85);
            this.b_Save.Name = "b_Save";
            this.b_Save.Size = new System.Drawing.Size(75, 23);
            this.b_Save.TabIndex = 4;
            this.b_Save.Text = "Save";
            this.b_Save.UseVisualStyleBackColor = true;
            this.b_Save.Click += new System.EventHandler(this.b_Save_Click);
            // 
            // b_Cancel
            // 
            this.b_Cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.b_Cancel.Location = new System.Drawing.Point(352, 85);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 23);
            this.b_Cancel.TabIndex = 5;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // SettingsDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(439, 120);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.b_Save);
            this.Controls.Add(this.b_Reset);
            this.Controls.Add(this.tB_youtubedlFolder);
            this.Controls.Add(this.b_SelectYTdl);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SettingsDialog";
            this.Text = "Settings";
            this.Load += new System.EventHandler(this.SettingsDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button b_SelectYTdl;
        private System.Windows.Forms.TextBox tB_youtubedlFolder;
        private System.Windows.Forms.Button b_Reset;
        private System.Windows.Forms.Button b_Save;
        private System.Windows.Forms.Button b_Cancel;
    }
}