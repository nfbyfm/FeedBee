using FeedSubs.FeedReader;
using FeedBee.Controlling;
using FeedBee.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Utilities.FeedSubs;
using System.Diagnostics;

namespace FeedBee.UI
{
    public partial class MainForm : Form
    {
        private Controller controller;
        private InternetCheck internetCheck;

        #region Constructor(-s) and setup-functions

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

            internetCheck = new InternetCheck();

            controller = new Controller(this, internetCheck);

            UpdateTreeView();

            SetProgress(101, 100);
         }

        #endregion

        #region Input-functions

        private void openListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.OpenList();
        }

        private void saveListToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.SaveList();
        }

        private void importToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.ImportFeedList();
        }

        private void exportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.ExportFeedList();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.CloseApplication();
        }

        private void addToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.AddNewFeed();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.ShowAboutDialog();
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.ShowSettings();
        }

        private void updateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.UpdateFeeds(sender, e);
        }

        private void MarkAllAsReadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.MarkAllAsRead();
        }

        private void OpenAllUnreadToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.OpenUnreadFeeds();
        }

        private void webpageFeedDefinitionsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.EditWebPageFeedDefinitions();
        }

        private void cancelUpdateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            controller.CancelUpdate();
        }

        private void ResetAllEntries(object sender, EventArgs e)
        {
            if (MessageBox.Show("This resets all feeds. Are you sure you want to delete all entries?", "reset all feeds", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
            {
                controller.ResetAllFeeds();
            }
        }
        #endregion

        #region draw-functions

        
        /// <summary>
        /// dis-/enable gui-functions that let the user manipulate the feeds
        /// </summary>
        /// <param name="enable"></param>
        public void EnableFeedFunctionalities(bool enable)
        {
            //feedToolStripMenuItem.Enabled = enable;
            addToolStripMenuItem.Enabled = enable;
            updateToolStripMenuItem.Enabled = enable;
            cancelUpdateToolStripMenuItem.Enabled = !enable;
            markToolStripMenuItem.Enabled = enable;
            openExternallyToolStripMenuItem.Enabled = enable;

            importToolStripMenuItem.Enabled = enable;
            exportToolStripMenuItem.Enabled = enable;
            openListToolStripMenuItem.Enabled = enable;
            saveListToolStripMenuItem.Enabled = enable;
            settingsToolStripMenuItem.Enabled = enable;
            webpageFeedDefinitionsToolStripMenuItem.Enabled = enable;
                       

            //context-menu for tVMain
            cMS_Treeview.Enabled = enable;
            cMS_Update.Enabled = enable;
            cMS_Edit.Enabled = enable;
            cMS_Delete.Enabled = enable;
            cMS_MarkAsRead.Enabled = enable;
        }

        /// <summary>
        /// method for updating treeview after update of feeds
        /// </summary>
        public void UpdateTreeViewUnlock()
        {
            UpdateTreeView();
            EnableFeedFunctionalities(true);
        }

        /// <summary>
        /// triggers redraw of the treeview (feeds and feed-groups)
        /// </summary>
        public void UpdateTreeView()
        {
            lVFeedItems.Items.Clear();

            tVMain.Nodes.Clear();

            List<TreeNode> treeNodes = new List<TreeNode>();
            ImageList iList = new ImageList();
            
            controller.GetTreeNodes(ref treeNodes, ref iList);


            if(treeNodes != null)
            {
                //add Nodes to the control
                if (treeNodes.Count() > 0)
                {
                    for (int i = 0; i < treeNodes.Count(); i++)
                    {
                        tVMain.Nodes.Add(treeNodes[i]);
                    }
                }
                else
                {
                    Console.WriteLine("MainForm: UpdateTreeview: Got no Nodes from controller.");
                }
            }
            else
            {
                Console.WriteLine("MainForm: UpdateTreeview: Got no Nodes from controller.");
            }

            tVMain.ImageList = iList;

            if (iList == null)
            {
                Console.WriteLine("List of Icons for treeview is null");
            }

            if (Properties.Settings.Default.expandNodes)
            {
                tVMain.ExpandAll();
            }
        }

        /// <summary>
        /// set the value of the progressbar, if currentValue bigger maximum: hides progressbar
        /// </summary>
        /// <param name="currentValue"></param>
        /// <param name="maximum"></param>
        /// <param name="minimum"></param>
        public void SetProgress(int currentValue, int maximum, int minimum =0)
        {
            try
            {
                if (currentValue > maximum)
                {
                    this.mainProgressBar.Visible = false;
                }
                else
                {
                    if (currentValue >= minimum)
                    {
                        this.mainProgressBar.Visible = true;
                        this.mainProgressBar.Value = currentValue;
                        this.mainProgressBar.Maximum = maximum;
                        this.mainProgressBar.Minimum = minimum;
                    }

                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Error setting Progressbar-value: " + ex.Message);
            }
            
        }

        

        /// <summary>
        /// lists feeditems in listview
        /// </summary>
        /// <param name="currentFeed"></param>
        private void ShowFeedList(Feed currentFeed)
        {
            if (currentFeed != null)
            {
                UpdateListView(currentFeed.Items);
            }
            else
            {
                Debug.WriteLine("selected Feed equals null");
            }
        }

        
        /// <summary>
        /// show list of specific feeditems
        /// </summary>
        /// <param name="feedItems"></param>
        private void UpdateListView(List<FeedItem> feedItems)
        {
            lVFeedItems.Items.Clear();

            if (feedItems != null)
            {
                foreach (FeedItem feedItem in feedItems)
                {
                    if (feedItem != null)
                    {
                        ListViewItem item = new ListViewItem(feedItem.Title);
                        item.Tag = feedItem;

                        if (!feedItem.Read)
                        {
                            item.BackColor = Color.DarkOrange;
                        }

                        lVFeedItems.Items.Add(item);
                    }
                }


                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            }
        }




        /// <summary>
        /// set text (timed) of the statusStripLabel
        /// </summary>
        /// <param name="text"></param>
        /// <param name="milliseconds">if smaller than 1 -> text gets shown permanently</param>
        public void SetStatusText(string text, int milliseconds)
        {
            this.UIThreadAsync(delegate
            {

                if (milliseconds > 0)
                {
                    statusLabel.setTimedText(text, milliseconds);
                }
                else
                {
                    try
                    {
                        statusLabel.Text = text;
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error setting statuslabel-text: " + ex.Message);
                    }
                }
            });
        }
        #endregion

        #region selection-reaction-functions

        /// <summary>
        /// method for reacting on users click upon treeview-Node and displaying / loading lists accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TreeView_FeedGroupAfterSelect(object sender, TreeViewEventArgs e)
        {
            lVFeedItems.Items.Clear();

            if (tVMain.SelectedNode != null)
            {
                object tagObj = tVMain.SelectedNode.Tag;

                if(tagObj != null)
                {
                    if(tagObj.GetType() == typeof(Feed))
                    {
                        Feed selFeed = (Feed)tagObj;

                        ShowFeedList(selFeed);
                    }

                    if(tagObj.GetType() == typeof(FeedGroup))
                    {
                        FeedGroup selGroup = (FeedGroup)tagObj;

                        List<FeedItem> items = controller.GetUnreadFeedItems(selGroup);

                        UpdateListView(items);
                    }
                }
            }
        }

        

        

        /// <summary>
        /// open link if double-clicked on listview-item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lVFeedItems_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (lVFeedItems.SelectedItems != null)
            {
                if (lVFeedItems.SelectedItems.Count > 0)
                {
                    if (lVFeedItems.SelectedItems[0].Tag != null)
                    {
                        if (lVFeedItems.SelectedItems[0].Tag.GetType() == typeof(FeedItem))
                        {
                            FeedItem item = (FeedItem)(lVFeedItems.SelectedItems[0].Tag);
                            if (item != null)
                            {
                                System.Diagnostics.Process.Start(item.Link);
                            }
                        }
                    }
                }
            }
        }
        #endregion

        #region context-menu-functions

        /// <summary>
        /// rename a treeview-node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cMS_Rename_Click(object sender, EventArgs e)
        {
            if(tVMain.SelectedNode != null)
            {
                object ob = tVMain.SelectedNode.Tag;
                if(ob != null)
                {
                    controller.RenameNode(ob);
                }
            }
        }

        /// <summary>
        /// delete  a treeview-node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cMS_Delete_Click(object sender, EventArgs e)
        {
            if (tVMain.SelectedNode != null)
            {
                object ob = tVMain.SelectedNode.Tag;
                if (ob != null)
                {
                    if(MessageBox.Show("Do you really want to delete the selected item?","Delete item",MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        controller.DeleteNode(ob);
                    }
                }
            }
        }

        /// <summary>
        /// mark a treeview-node as read
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cMS_MarkAsRead_Click(object sender, EventArgs e)
        {
            if (tVMain.SelectedNode != null)
            {
                object ob = tVMain.SelectedNode.Tag;
                if (ob != null)
                {
                    controller.MarkNodeAsRead(ob);
                }
            }
        }

        /// <summary>
        /// update a treeview-node
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cMS_Update_Click(object sender, EventArgs e)
        {
            if (tVMain.SelectedNode != null)
            {
                object ob = tVMain.SelectedNode.Tag;
                if (ob != null)
                {
                    controller.UpdateNode(ob);
                }
            }
        }

        private void openAllUnreadToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (tVMain.SelectedNode != null)
            {
                object ob = tVMain.SelectedNode.Tag;
                if (ob != null)
                {
                    if(ob.GetType() == typeof(FeedGroup))
                    {
                        FeedGroup subGroup = (FeedGroup)ob;
                        controller.OpenUnreadFeeds(subGroup);
                    }

                    if(ob.GetType() == typeof(Feed))
                    {
                        Feed selFeed = (Feed)ob;
                        controller.OpenUnreadFeeds(selFeed);
                    }
                }
            }
        }







        #endregion

        
    }
}
