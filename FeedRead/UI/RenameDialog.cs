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
    public partial class RenameDialog : Form
    {
        public string newName;

        public RenameDialog()
        {
            InitializeComponent();


            this.ActiveControl = tB_NewName;
            newName = "";
        }


        public void SetOldName(string oldName)
        {
            tB_NewName.Text = oldName;
        }


        private void bOK_Click(object sender, EventArgs e)
        {
            newName = tB_NewName.Text;
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
