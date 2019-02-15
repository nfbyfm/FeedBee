using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedRead.UI
{
    public partial class SelectGroupDialog : Form
    {
        public string groupName;
        public bool addNewGroupName;

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

        
    }
}
