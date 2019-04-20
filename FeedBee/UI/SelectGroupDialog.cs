using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedBee.UI
{
    public partial class SelectGroupDialog : Form
    {
        public string groupName;
        public bool addNewGroupName;
        public bool newGroupIsNSFW;

        private List<string> groupNames;

        public SelectGroupDialog(List<string> groupNames) 
        {
            InitializeComponent();

            this.groupNames = groupNames;
        }

        private void SelectGroupDialog_Load(object sender, EventArgs e)
        {
            bool allowAddToGroup = false;

            //set items of comboBox
            if(groupNames != null)
            {
                if(groupNames.Count() > 0)
                {
                    cB_Groups.Items.Clear();

                    foreach(string groupName in groupNames)
                    {
                        cB_Groups.Items.Add(groupName);
                    }
                    cB_Groups.SelectedIndex = 0;

                    allowAddToGroup = true;
                    
                }

            }

            rB_addToGroup.Enabled = allowAddToGroup;
            cB_Groups.Enabled = allowAddToGroup;
            b_AddToGroup.Enabled = allowAddToGroup;

            rB_addToGroup.Checked = allowAddToGroup;
            rB_CreateNewGroup.Checked = !allowAddToGroup;

            
        }

        private void b_AddToGroup_Click(object sender, EventArgs e)
        {
            groupName = cB_Groups.Text;
            addNewGroupName = false;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_AddToNewGroup_Click(object sender, EventArgs e)
        {
            groupName = tB_NewGroupName.Text;
            addNewGroupName = true;
            newGroupIsNSFW = cB_GroupIsNSFW.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            groupName = "";
            addNewGroupName = false;
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void rB_addToGroup_CheckedChanged(object sender, EventArgs e)
        {
            rBSelectionChanged();
        }

        private void rB_CreateNewGroup_CheckedChanged(object sender, EventArgs e)
        {
            rBSelectionChanged();
        }

        /// <summary>
        /// radio-button-selection has changed
        /// </summary>
        private void rBSelectionChanged()
        {
            cB_Groups.Enabled = rB_addToGroup.Checked;
            b_AddToGroup.Enabled = rB_addToGroup.Checked;

            tB_NewGroupName.Enabled = rB_CreateNewGroup.Checked;
            b_AddToNewGroup.Enabled = rB_CreateNewGroup.Checked;
            cB_GroupIsNSFW.Enabled = rB_CreateNewGroup.Checked;
        }
    }
}
