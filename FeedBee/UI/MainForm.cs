using FeedSubs.FeedReader;
using FeedBee.Control;
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
            ClearPropertyDisplays();

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
            controller.UpdateFeeds();
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

        #endregion

        #region draw-functions

        /// <summary>
        /// edit the displayed html if setting "filerIFrames" is set to true
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void browser_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            if (Properties.Settings.Default.filterIFrames)
            {
                //try to get rid of ads -> filter iframes
                foreach (HtmlElement x in ((WebBrowser)sender).Document.GetElementsByTagName("iframe"))
                {
                    if (x.OuterHtml.ToLower().Contains("adtrue") || x.OuterHtml.ToLower().Contains("zeus"))
                    {
                        x.OuterHtml = String.Empty;
                    }
                }
            }
        }



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
            ClearPropertyDisplays();
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
                    Debug.WriteLine("MainForm: UpdateTreeview: Got no Nodes from controller.");
                }
            }
            else
            {
                Debug.WriteLine("MainForm: UpdateTreeview: Got no Nodes from controller.");
            }

            tVMain.ImageList = iList;

            if (iList == null)
                Debug.WriteLine("List of Icons for treeview is null");

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
        /// clears and hides controls which show information about a feed-item
        /// </summary>
        private void ClearPropertyDisplays()
        {
            try
            {
                sCMainBrowse.Panel1Collapsed = true;
                browser.Navigate("about:blank");

                lL_Url.Text = "";
                lL_Url.Links.Clear();
                lL_Url.LinkVisited = false;

                b_DownloadVideo.Visible = false;
                b_DownloadVideo.Enabled = false;

                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lVFeedItems.View = View.Details;
            }
            catch(Exception ex)
            {
                Debug.WriteLine("Error in 'ClearPropertyDisplays': " + ex.Message);
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
        /// show unread feeditems of a whole group in the listview 
        /// </summary>
        /// <param name="currentGroup"></param>
        private void ShowFeedList(FeedGroup currentGroup)
        {
            if(currentGroup != null)
            {
                List<FeedItem> items = controller.GetUnreadFeedItems(currentGroup);

                UpdateListView(items);

            }
        }


        /// <summary>
        /// show list of specific feeditems
        /// </summary>
        /// <param name="feedItems"></param>
        private void UpdateListView(List<FeedItem> feedItems)
        {
            if(feedItems != null)
            {
                lVFeedItems.Clear();

                ColumnHeader columnHeader1 = new ColumnHeader();
                columnHeader1.Text = "Title";
                ColumnHeader columnHeader2 = new ColumnHeader();
                columnHeader2.Text = "publishing date";

                ColumnHeader columnHeader3 = new ColumnHeader();
                columnHeader3.Text = "Author";

                ColumnHeader columnHeader4 = new ColumnHeader();
                columnHeader4.Text = "read";


                lVFeedItems.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3, columnHeader4 });


                foreach (FeedItem feedItem in feedItems)
                {
                    if (feedItem != null)
                    {
                        string update = "";

                        if (feedItem.PublishingDate != null)
                        {
                            if (feedItem.PublishingDate.Value != null)
                            {
                                update = feedItem.PublishingDate.Value.ToShortDateString();
                            }
                        }

                        string read = "no";

                        if (feedItem.Read)
                        {
                            read = "yes";
                        }


                        string[] itemProperties = new string[] {    feedItem.Title,
                                                                        update,
                                                                        feedItem.Author,
                                                                        read
                                                                    };

                        ListViewItem item = new ListViewItem(itemProperties);
                        item.Tag = feedItem;

                        if (!feedItem.Read)
                        {
                            item.BackColor = Color.DarkOrange;
                        }

                        lVFeedItems.Items.Add(item);
                    }
                }


                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lVFeedItems.View = View.Details;
            }
    
        }




        /// <summary>
        /// set text (timed) of the statusStripLabel
        /// </summary>
        /// <param name="text"></param>
        /// <param name="milliseconds">if smaller than 1 -> text gets shown permanently</param>
        public void SetStatusText(string text, int milliseconds)
        {
            if(milliseconds > 0)
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
        }
        #endregion

        #region selection-reaction-functions

        /// <summary>
        /// method for reacting on users click upon treeview-Node and displaying / loading lists accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tVMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            ClearPropertyDisplays();
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

                        ShowFeedList(selGroup);
                    }
                }
            }
        }

        

        /// <summary>
        /// method for displaying information about a selected feed-item
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lVFeedItems_SelectedIndexChanged(object sender, EventArgs e)
        {

            ClearPropertyDisplays();

            if (lVFeedItems.SelectedItems.Count > 0)
            {
                //Debug.WriteLine("number of selected items: " + listView1.SelectedItems.Count);

                ListViewItem ob = lVFeedItems.SelectedItems[0];
                if (ob != null && ob.Tag != null)
                {
                    /*
                    if (ob.Tag.GetType() == typeof(ComicChapter))
                    {

                        ComicChapter chapter = (ComicChapter)(ob.Tag);
                        if (chapter != null)
                        {
                            //show properties, navigate to webpage
                            l_Title.Text = chapter.Title;
                            l_Update.Text = chapter.DateTime.ToString("dd.MM.yyyy");
                            lL_Url.Text = chapter.ChapterUrl;
                            sCMainBrowse.Panel1Collapsed = false;
                            browser.Navigate(chapter.ChapterUrl);
                        }
                        else
                        {
                            browser.Navigate("about:blank");
                        }
                    }
                    else */
                    if (ob.Tag.GetType() == typeof(FeedItem))
                    {
                        FeedItem item = (FeedItem)(ob.Tag);
                        if (item != null)
                        {
                            lL_Url.Text = item.Title;
                            lL_Url.Links.Add(0, item.Title.Length, item.Link);

                            sCMainBrowse.Panel1Collapsed = false;

                            if (item.Link.ToLower().Contains(controller.GetYoutubeID().ToLower()))
                            {
                                b_DownloadVideo.Visible = true;
                                b_DownloadVideo.Enabled = true;
                            }
                            
                            //try to display the description of the feed -> if not possible: load webpage
                            bool loadWebpage = false;

                            //find out if webpage should get loaded directly (by getting the propertiy of the parent-feed)
                            if(tVMain.SelectedNode != null)
                            {
                                object fob = tVMain.SelectedNode.Tag;
                                if(fob != null)
                                {
                                    if(fob.GetType() == typeof(Feed))
                                    {
                                        Feed parentFeed = (Feed)fob;
                                        if(parentFeed != null)
                                        {
                                            loadWebpage = parentFeed.DirectlyLoadWebpage;
                                        }
                                    }
                                }
                            }

                            //try to load the description
                            if(item.Description != null && (loadWebpage == false))
                            {
                                if(!string.IsNullOrEmpty(item.Description) && !string.IsNullOrWhiteSpace(item.Description))
                                {
                                    browser.DocumentText = item.Description;
                                    loadWebpage = false;
                                }
                                else
                                {
                                    loadWebpage = true;
                                }
                            }

                            //if description couldn't or shouldn't get loaded, show the webpage
                            if(loadWebpage)
                            {
                                browser.Navigate(item.Link);
                            }
                                
                            //}
                        }
                    }
                }
            }
            
        }

        /// <summary>
        /// method for starting the loading of the webpage / url aof the Link-Label in the default browser
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lL_Url_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            lL_Url.LinkVisited = true;
            System.Diagnostics.Process.Start(e.Link.LinkData.ToString());
        }

        /// <summary>
        /// download a (youtube-) video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_DownloadVideo_Click(object sender, EventArgs e)
        {
            if(lL_Url.Links != null)
            {
                if (lL_Url.Links[0] != null)
                {
                    controller.DownloadVideo(lL_Url.Links[0].LinkData.ToString());
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
