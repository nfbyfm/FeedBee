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
    public partial class EditGroupDialog : Form
    {
        public string newName;
        public bool IsNSFW;
        public string iconPath;

        /// <summary>
        /// Constructor for Rename-Dialog
        /// </summary>
        /// <param name="oldName">name/title of current element</param>
        /// <param name="isNSFW">if current element is nsfw set to true</param>
        public EditGroupDialog(string oldName, bool isNSFW, string IconPath)
        {
            InitializeComponent();

            this.IsNSFW = isNSFW;
            this.newName = oldName;
            this.iconPath = IconPath;

            tB_NewName.Text = oldName;
            cB_MarkNSFW.Checked = isNSFW;
            tB_IconPath.Text = IconPath;


            this.ActiveControl = tB_NewName;

        }
        

        private void bOK_Click(object sender, EventArgs e)
        {
            newName = tB_NewName.Text;
            IsNSFW = cB_MarkNSFW.Checked;
            iconPath = tB_IconPath.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bSelectIconpath_Click(object sender, EventArgs e)
        {
            OpenFileDialog odi = new OpenFileDialog();
            odi.Multiselect = false;
            odi.Filter = String.Format("All Files|*.*");
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageEncoders();
            string sep = "|";
            

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
