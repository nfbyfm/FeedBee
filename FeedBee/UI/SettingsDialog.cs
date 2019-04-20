﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedBee.UI
{
    public partial class SettingsDialog : Form
    {
        #region UI-functions

        public SettingsDialog()
        {
            InitializeComponent();
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

        /// <summary>
        /// select the folder of youtube-dl.exe
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_SelectYTdl_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            folderBrowserDialog.ShowNewFolderButton = true;

            if(!(string.IsNullOrEmpty(tB_youtubedlFolder.Text) || string.IsNullOrWhiteSpace(tB_youtubedlFolder.Text)))
            {
                try
                {
                    folderBrowserDialog.SelectedPath = tB_youtubedlFolder.Text;
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Error setting default-path for youtube-dl.exe-path: " + ex.Message);
                }
            }

            if(folderBrowserDialog.ShowDialog() == DialogResult.OK)
            {
                tB_youtubedlFolder.Text = folderBrowserDialog.SelectedPath;
            }
        }

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
            tB_FeedListPath.Enabled = cB_LoadUponStartup.Checked;
            bSelectFeedList.Enabled = cB_LoadUponStartup.Checked;
        }

        /// <summary>
        /// set enable-properties of FeedIcon-Folder-Controls
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cB_DisplayFeedIcons_CheckedChanged(object sender, EventArgs e)
        {
            bool enabled = cB_DisplayFeedIcons.Checked;

            label3.Enabled = enabled;
            tB_IconFolder.Enabled = enabled;
            b_SelectIconFolder.Enabled = enabled;
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

        #endregion


        #region Settings-functions
        /// <summary>
        /// load current settings into form-elements
        /// </summary>
        private void LoadSettings()
        {
            tB_youtubedlFolder.Text = Properties.Settings.Default.youtubedlFolder;
            tB_FeedListPath.Text = Properties.Settings.Default.loadListPath;
            cB_LoadUponStartup.Checked = Properties.Settings.Default.bLoadUponStartup;
            cB_LoadUponStartup_CheckedChanged(null,null);
            tB_FeedDefFilePath.Text = Properties.Settings.Default.WebpageFeedDefPath;
            cB_UpdateNSFW.Checked = Properties.Settings.Default.updateNSFW;




            cB_DisplayFeedIcons.Checked = Properties.Settings.Default.displayFeedIcons;
            cB_FilterIFrames.Checked = Properties.Settings.Default.filterIFrames;
            cB_UpdateUponLoad.Checked = Properties.Settings.Default.updateUponLoad;

            cB_ExpandNodes.Checked = Properties.Settings.Default.expandNodes;

            tB_IconFolder.Text = Properties.Settings.Default.iconFolderPath;

            cB_DisplayFeedIcons_CheckedChanged(null, null);
        }

        /// <summary>
        /// get values from form-elements and save them
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings.Default.youtubedlFolder = tB_youtubedlFolder.Text;
            Properties.Settings.Default.loadListPath = tB_FeedListPath.Text;
            Properties.Settings.Default.bLoadUponStartup = cB_LoadUponStartup.Checked;
            Properties.Settings.Default.updateNSFW = cB_UpdateNSFW.Checked;
            Properties.Settings.Default.WebpageFeedDefPath = tB_FeedDefFilePath.Text;
            
            Properties.Settings.Default.displayFeedIcons = cB_DisplayFeedIcons.Checked;
            Properties.Settings.Default.filterIFrames = cB_FilterIFrames.Checked;
            Properties.Settings.Default.updateUponLoad = cB_UpdateUponLoad.Checked;
            Properties.Settings.Default.expandNodes = cB_ExpandNodes.Checked;

            Properties.Settings.Default.iconFolderPath = tB_IconFolder.Text;
        }





        #endregion

        
    }
}
