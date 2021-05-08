using FeedBee.Controlling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedBee.UI
{
    public partial class SettingsDialog : Form
    {
        #region UI-functions
        private Controller parentController;

        public SettingsDialog(Controller controller)
        {
            this.parentController = controller;

            InitializeComponent();

            this.AcceptButton = b_Save;
            this.CancelButton = b_Cancel;
        }

        /// <summary>
        /// method called during loading -> load Settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SettingsDialog_Load(object sender, EventArgs e)
        {
            LoadSettings();
        }


        #region Button-clicks and Checkboxes
        /// <summary>
        /// reset settings
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Reset_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Do you really want to reset the current settings?","Reset settings",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                Properties.Settings.Default.Reset();
                LoadSettings();
            }
        }

        /// <summary>
        /// save settings and then close the dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Save_Click(object sender, EventArgs e)
        {
            //save changes and then close the dialog
            SaveSettings();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// don't save any changes, close dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_Cancel_Click(object sender, EventArgs e)
        {
            //don't save any changes
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        /// <summary>
        /// set enable-properties depending upon checkbox-control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cB_LoadUponStartup_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// set enable-properties of FeedIcon-Folder-Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cB_DisplayFeedIcons_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }

        /// <summary>
        /// select feed-list file
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bSelectFeedList_Click(object sender, EventArgs e)
        {
            OpenFileDialog odi = new OpenFileDialog();
            odi.Multiselect = false;
            odi.RestoreDirectory = true;
            odi.Filter = "xml-file|*.xml";
            odi.Title = "select feed-list";

            if(odi.ShowDialog() == DialogResult.OK)
            {
                tB_FeedListPath.Text = odi.FileName;
            }
        }

        /// <summary>
        /// select path of the webpagefeed-definition-list
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void bSelectFeedDefFilePath_Click(object sender, EventArgs e)
        {
            OpenFileDialog odi = new OpenFileDialog();
            odi.Multiselect = false;
            odi.RestoreDirectory = true;
            odi.Filter = "xml-file|*.xml";
            odi.Title = "select feed-list";

            if (odi.ShowDialog() == DialogResult.OK)
            {
                tB_FeedDefFilePath.Text = odi.FileName;
            }
        }

        /// <summary>
        /// set the default-folder for treeview-icons
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_SelectIconFolder_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fdia = new FolderBrowserDialog();
            fdia.ShowNewFolderButton = true;
            fdia.SelectedPath = tB_IconFolder.Text;

            if (fdia.ShowDialog() == DialogResult.OK)
            {
                tB_IconFolder.Text = fdia.SelectedPath;
            }
        }


        private void cB_automaticUpdate_CheckedChanged(object sender, EventArgs e)
        {
            UpdateControls();
        }
        #endregion

        private void UpdateControls()
        {
            bool enabled;

            //Feedlist-Path
            enabled = cB_LoadUponStartup.Checked;
            tB_FeedListPath.Enabled = enabled;
            bSelectFeedList.Enabled = enabled;

            //display Feedicons
            enabled = cB_DisplayFeedIcons.Checked;
            label3.Enabled = enabled;
            tB_IconFolder.Enabled = enabled;
            b_SelectIconFolder.Enabled = enabled;


            //automatic Updates
            enabled = cB_automaticUpdate.Checked;
            l_autoUpdateInterval.Enabled = enabled;
            mTB_autoUpdateTimeSpan.Enabled = enabled;
        }
        #endregion


        #region Settings-functions
        /// <summary>
        /// load current settings into form-elements
        /// </summary>
        private void LoadSettings()
        {
            tB_FeedListPath.Text = Properties.Settings.Default.loadListPath;
            cB_LoadUponStartup.Checked = Properties.Settings.Default.bLoadUponStartup;
            cB_LoadUponStartup_CheckedChanged(null,null);
            tB_FeedDefFilePath.Text = Properties.Settings.Default.WebpageFeedDefPath;
            cB_UpdateNSFW.Checked = Properties.Settings.Default.updateNSFW;


            

            cB_DisplayFeedIcons.Checked = Properties.Settings.Default.displayFeedIcons;
            cB_UpdateUponLoad.Checked = Properties.Settings.Default.updateUponLoad;

            cB_ExpandNodes.Checked = Properties.Settings.Default.expandNodes;

            tB_IconFolder.Text = Properties.Settings.Default.iconFolderPath;

            cB_automaticUpdate.Checked = Properties.Settings.Default.automaticUpdateEnabled;
            mTB_autoUpdateTimeSpan.Text = Properties.Settings.Default.automaticUpdateTime.ToString(@"hh\:mm\:ss");

            UpdateControls();
        }

        /// <summary>
        /// get values from form-elements and save them
        /// </summary>
        private void SaveSettings()
        {
            
            Properties.Settings.Default.loadListPath = tB_FeedListPath.Text;
            Properties.Settings.Default.bLoadUponStartup = cB_LoadUponStartup.Checked;
            Properties.Settings.Default.updateNSFW = cB_UpdateNSFW.Checked;
            Properties.Settings.Default.WebpageFeedDefPath = tB_FeedDefFilePath.Text;
            
            Properties.Settings.Default.displayFeedIcons = cB_DisplayFeedIcons.Checked;
            Properties.Settings.Default.updateUponLoad = cB_UpdateUponLoad.Checked;
            Properties.Settings.Default.expandNodes = cB_ExpandNodes.Checked;

            Properties.Settings.Default.iconFolderPath = tB_IconFolder.Text;

            Properties.Settings.Default.automaticUpdateEnabled = cB_automaticUpdate.Checked;

            string timespanStr = "";

            timespanStr = mTB_autoUpdateTimeSpan.Text;
            timespanStr = timespanStr.Replace("_", "0");
            timespanStr = timespanStr.Replace(" ", "0");

            int hours = Int32.Parse(timespanStr.Split(':')[0]);
            int minutes = Int32.Parse(timespanStr.Split(':')[1]);
            int seconds = Int32.Parse(timespanStr.Split(':')[2]);            

            try
            {
                TimeSpan span = new TimeSpan(hours,minutes,seconds);
                Properties.Settings.Default.automaticUpdateTime = span;
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error generating new timespan: " + ex);
            }
        }







        #endregion

        
    }
}
