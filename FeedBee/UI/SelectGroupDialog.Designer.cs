﻿namespace FeedBee.UI
{
    partial class SelectGroupDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SelectGroupDialog));
            this.rB_addToGroup = new System.Windows.Forms.RadioButton();
            this.rB_CreateNewGroup = new System.Windows.Forms.RadioButton();
            this.cB_Groups = new System.Windows.Forms.ComboBox();
            this.b_AddToGroup = new System.Windows.Forms.Button();
            this.b_AddToNewGroup = new System.Windows.Forms.Button();
            this.tB_NewGroupName = new System.Windows.Forms.TextBox();
            this.b_Cancel = new System.Windows.Forms.Button();
            this.cB_GroupIsNSFW = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // rB_addToGroup
            // 
            this.rB_addToGroup.AutoSize = true;
            this.rB_addToGroup.Location = new System.Drawing.Point(12, 12);
            this.rB_addToGroup.Name = "rB_addToGroup";
            this.rB_addToGroup.Size = new System.Drawing.Size(88, 17);
            this.rB_addToGroup.TabIndex = 0;
            this.rB_addToGroup.TabStop = true;
            this.rB_addToGroup.Text = "add to group:";
            this.rB_addToGroup.UseVisualStyleBackColor = true;
            this.rB_addToGroup.CheckedChanged += new System.EventHandler(this.rB_addToGroup_CheckedChanged);
            // 
            // rB_CreateNewGroup
            // 
            this.rB_CreateNewGroup.AutoSize = true;
            this.rB_CreateNewGroup.Location = new System.Drawing.Point(12, 44);
            this.rB_CreateNewGroup.Name = "rB_CreateNewGroup";
            this.rB_CreateNewGroup.Size = new System.Drawing.Size(111, 17);
            this.rB_CreateNewGroup.TabIndex = 2;
            this.rB_CreateNewGroup.TabStop = true;
            this.rB_CreateNewGroup.Text = "create new group:";
            this.rB_CreateNewGroup.UseVisualStyleBackColor = true;
            this.rB_CreateNewGroup.CheckedChanged += new System.EventHandler(this.rB_CreateNewGroup_CheckedChanged);
            // 
            // cB_Groups
            // 
            this.cB_Groups.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cB_Groups.FormattingEnabled = true;
            this.cB_Groups.Location = new System.Drawing.Point(144, 12);
            this.cB_Groups.Name = "cB_Groups";
            this.cB_Groups.Size = new System.Drawing.Size(177, 21);
            this.cB_Groups.TabIndex = 1;
            // 
            // b_AddToGroup
            // 
            this.b_AddToGroup.Location = new System.Drawing.Point(327, 10);
            this.b_AddToGroup.Name = "b_AddToGroup";
            this.b_AddToGroup.Size = new System.Drawing.Size(75, 23);
            this.b_AddToGroup.TabIndex = 5;
            this.b_AddToGroup.Text = "add";
            this.b_AddToGroup.UseVisualStyleBackColor = true;
            this.b_AddToGroup.Click += new System.EventHandler(this.b_AddToGroup_Click);
            // 
            // b_AddToNewGroup
            // 
            this.b_AddToNewGroup.Location = new System.Drawing.Point(327, 41);
            this.b_AddToNewGroup.Name = "b_AddToNewGroup";
            this.b_AddToNewGroup.Size = new System.Drawing.Size(75, 23);
            this.b_AddToNewGroup.TabIndex = 6;
            this.b_AddToNewGroup.Text = "add";
            this.b_AddToNewGroup.UseVisualStyleBackColor = true;
            this.b_AddToNewGroup.Click += new System.EventHandler(this.b_AddToNewGroup_Click);
            // 
            // tB_NewGroupName
            // 
            this.tB_NewGroupName.Location = new System.Drawing.Point(144, 43);
            this.tB_NewGroupName.Name = "tB_NewGroupName";
            this.tB_NewGroupName.Size = new System.Drawing.Size(177, 20);
            this.tB_NewGroupName.TabIndex = 3;
            // 
            // b_Cancel
            // 
            this.b_Cancel.Location = new System.Drawing.Point(327, 70);
            this.b_Cancel.Name = "b_Cancel";
            this.b_Cancel.Size = new System.Drawing.Size(75, 23);
            this.b_Cancel.TabIndex = 7;
            this.b_Cancel.Text = "Cancel";
            this.b_Cancel.UseVisualStyleBackColor = true;
            this.b_Cancel.Click += new System.EventHandler(this.b_Cancel_Click);
            // 
            // cB_GroupIsNSFW
            // 
            this.cB_GroupIsNSFW.AutoSize = true;
            this.cB_GroupIsNSFW.Location = new System.Drawing.Point(144, 70);
            this.cB_GroupIsNSFW.Name = "cB_GroupIsNSFW";
            this.cB_GroupIsNSFW.Size = new System.Drawing.Size(90, 17);
            this.cB_GroupIsNSFW.TabIndex = 4;
            this.cB_GroupIsNSFW.Text = "Group is nsfw";
            this.cB_GroupIsNSFW.UseVisualStyleBackColor = true;
            // 
            // SelectGroupDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(416, 103);
            this.Controls.Add(this.cB_GroupIsNSFW);
            this.Controls.Add(this.b_Cancel);
            this.Controls.Add(this.tB_NewGroupName);
            this.Controls.Add(this.b_AddToNewGroup);
            this.Controls.Add(this.b_AddToGroup);
            this.Controls.Add(this.cB_Groups);
            this.Controls.Add(this.rB_CreateNewGroup);
            this.Controls.Add(this.rB_addToGroup);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "SelectGroupDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Select Feed-Group";
            this.Load += new System.EventHandler(this.SelectGroupDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rB_addToGroup;
        private System.Windows.Forms.RadioButton rB_CreateNewGroup;
        private System.Windows.Forms.ComboBox cB_Groups;
        private System.Windows.Forms.Button b_AddToGroup;
        private System.Windows.Forms.Button b_AddToNewGroup;
        private System.Windows.Forms.TextBox tB_NewGroupName;
        private System.Windows.Forms.Button b_Cancel;
        private System.Windows.Forms.CheckBox cB_GroupIsNSFW;
    }
}