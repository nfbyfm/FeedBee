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
    public partial class EditFeedDialog : Form
    {
        public string feedTitle;
        public bool directlyLoadWebURL;

        public EditFeedDialog(string feedTitle, bool directlyLoad)
        {
            InitializeComponent();

            tB_FeedTitle.Text = feedTitle;
            cB_DirectlyLoadWebURL.Checked = directlyLoad;

            this.feedTitle = feedTitle;
            this.directlyLoadWebURL = directlyLoad;

            this.ActiveControl = tB_FeedTitle;
        }

        private void b_OK_Click(object sender, EventArgs e)
        {
            feedTitle = tB_FeedTitle.Text;
            directlyLoadWebURL = cB_DirectlyLoadWebURL.Checked;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
