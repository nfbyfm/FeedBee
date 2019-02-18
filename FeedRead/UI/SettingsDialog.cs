﻿using System;
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
                    Console.WriteLine("Error setting default-path for youtube-dl.exe-path: " + ex.Message);
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

        #endregion


        #region Settings-functions
        /// <summary>
        /// load current settings into form-elements
        /// </summary>
        private void LoadSettings()
        {
            tB_youtubedlFolder.Text = Properties.Settings.Default.youtubedlFolder;
        }

        /// <summary>
        /// get values from form-elements and save them
        /// </summary>
        private void SaveSettings()
        {
            Properties.Settings.Default.youtubedlFolder = tB_youtubedlFolder.Text;



            
        }

        #endregion

    }
}
