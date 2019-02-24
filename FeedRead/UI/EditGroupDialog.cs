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
    public partial class EditGroupDialog : Form
    {
        public string newName;
        public bool IsNSFW;

        /// <summary>
        /// Constructor for Rename-Dialog
        /// </summary>
        /// <param name="oldName">name/title of current element</param>
        /// <param name="isNSFW">if current element is nsfw set to true</param>
        public EditGroupDialog(string oldName, bool isNSFW)
        {
            InitializeComponent();


            tB_NewName.Text = oldName;
            cB_MarkNSFW.Checked = IsNSFW;

            this.IsNSFW = isNSFW;
            this.newName = oldName;

            this.ActiveControl = tB_NewName;

        }
        

        private void bOK_Click(object sender, EventArgs e)
        {
            newName = tB_NewName.Text;
            IsNSFW = cB_MarkNSFW.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }


        
    }
}
