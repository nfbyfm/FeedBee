using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Imaging;
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
        public string iconPath;

        public EditFeedDialog(string feedTitle, bool directlyLoad, string iconPath)
        {
            InitializeComponent();


            tB_FeedTitle.Text = feedTitle;
            cB_DirectlyLoadWebURL.Checked = directlyLoad;
            tB_IconPath.Text = iconPath;

            this.feedTitle = feedTitle;
            this.directlyLoadWebURL = directlyLoad;

            this.ActiveControl = tB_FeedTitle;
        }

        private void b_OK_Click(object sender, EventArgs e)
        {
            feedTitle = tB_FeedTitle.Text;
            directlyLoadWebURL = cB_DirectlyLoadWebURL.Checked;
            iconPath = tB_IconPath.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void b_SelectIconPath_Click(object sender, EventArgs e)
        {
            OpenFileDialog odi = new OpenFileDialog();
            odi.Multiselect = false;
            odi.Filter = String.Format("All Files|*.*|");
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = "";


            foreach (ImageCodecInfo c in codecs)
            {
                string codecName = c.CodecName.Substring(8).Replace("Codec", "Files").Trim();
                odi.Filter = String.Format("{0}{1}{2} ({3})|{3}", odi.Filter, sep, codecName.ToLower(), c.FilenameExtension.ToLower());
                sep = "|";
            }
            //odi.Filter = String.Format("{0}{1}{2} ({3})|{3}", odi.Filter, sep, "All Files", "*.*");


            odi.RestoreDirectory = true;

            if (odi.ShowDialog() == DialogResult.OK)
            {
                tB_IconPath.Text = odi.FileName;
            }
        }
    }
}
