using CodeHollow.FeedReader;
using FeedRead.Model;
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

namespace FeedRead.UI
{
    public partial class MainForm : Form
    {
        private Controller controller;

        #region Constructor(-s) and setup-functions

        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            controller = new Controller(this);

            ClearPropertyDisplays();            
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
        #endregion

        #region draw-functions

        
        /// <summary>
        /// triggers redraw of the treeview (feeds and feed-groups)
        /// </summary>
        /// <param name="mainModel"></param>
        public void UpdateTreeView(FeedGroup mainModel)
        {
            tVMain.Nodes.Clear();

            
            if(mainModel != null)
            {
                //list of icons for the feed-nodes
                ImageList icons = new ImageList();
                icons.Images.Add(Properties.Resources.defaultTreeNodeIcon);
                Console.WriteLine("Length of imagelist for treeview before update: " + icons.Images.Count);

                //list of all the primary nodes of the treeview
                List<TreeNode> mainNodes = new List<TreeNode>();

                //add feedgroup-Nodes to the list
                if (mainModel.FeedGroups != null)
                {
                    if (mainModel.FeedGroups.Count() > 0)
                    {
                        for (int i = 0; i < mainModel.FeedGroups.Count(); i++)
                        {
                            TreeNode tmpNode = GetNode(mainModel.FeedGroups[i], ref icons);
                            if (tmpNode != null)
                            {
                                mainNodes.Add(tmpNode);
                            }
                        }
                    }
                }

                //add feeds
                if (mainModel.FeedList != null)
                {
                    if (mainModel.FeedList.Count > 0)
                    {
                        for (int i = 0; i < mainModel.FeedList.Count(); i++)
                        {
                            mainNodes.Add(GetFeedNode(mainModel.FeedList[i], ref icons));
                        }
                    }
                }


                //add Nodes to the control
                if (mainNodes.Count() > 0)
                {
                    for(int i = 0; i < mainNodes.Count(); i++)
                    {
                        tVMain.Nodes.Add(mainNodes[i]);
                    }
                }


                tVMain.ImageList = icons;
                
                Console.WriteLine("Length of imagelist for treeview after update: " + icons.Images.Count);
            }
        }

        

        /// <summary>
        /// method for (recursively) getting the TreeNode (and subnodes) of a feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private TreeNode GetNode(FeedGroup group, ref ImageList icons)
        {
            TreeNode groupNode = null;

            if(group != null)
            {
                groupNode = new TreeNode(group.GetNodeText());
                groupNode.Tag = group;

                //check for sub-Feedgroups and add them to this group's node
                if(group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count > 0)
                    {
                        for(int i=0; i< group.FeedGroups.Count(); i++)
                        {
                            TreeNode subNode = GetNode(group.FeedGroups[i], ref icons);
                            if(subNode != null)
                            {
                                groupNode.Nodes.Add(subNode);
                            }
                        }
                    }
                }

                //check if there are feeds in this group
                if(group.FeedList != null)
                {
                    if(group.FeedList.Count() > 0)
                    {
                        /*
                        foreach(Feed feed in group.FeedList)
                        {
                            TreeNode feedNode = new TreeNode(feed.GetNodeText());
                            feedNode.Tag = feed;

                            string imageUrl = feed.ImageUrl;

                            try
                            {
                                if (!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrWhiteSpace(imageUrl))
                                {

                                    Image feedIcon = null;
                                    controller.GetImageFromURL(imageUrl, ref feedIcon);

                                    if (feedIcon != null)
                                    {
                                        icons.Images.Add(feedIcon);

                                        feedNode.ImageIndex = icons.Images.Count - 1;
                                        feedNode.SelectedImageIndex = icons.Images.Count - 1;
                                    }
                                    else
                                    {
                                        Console.WriteLine("Image for " + feed.Title + "'. Image-URL = " + imageUrl + " is null.");
                                    }

                                }
                            }
                            catch (Exception ex)
                            {
                                Console.WriteLine("Error while trying to get the image for feed '" + feed.Title + "'. Image-URL = " + imageUrl + "   Error: " + ex.Message);
                            }
                            groupNode.Nodes.Add(feedNode);

                        }
                        */
                        //add feeds to this group's node
                        for(int i = 0; i< group.FeedList.Count(); i++)
                        {

                            groupNode.Nodes.Add(GetFeedNode(group.FeedList[i], ref icons));
                        }
                        
                    }
                }
            }

            return groupNode;
        }

        /// <summary>
        /// get TreeNode out of feed
        /// </summary>
        /// <param name="feed"></param>
        /// <param name="icons"></param>
        /// <returns></returns>
        private TreeNode GetFeedNode(Feed feed, ref ImageList icons)
        {
            TreeNode feedNode = new TreeNode(feed.GetNodeText());
            feedNode.Tag = feed;

            string imageUrl = feed.ImageUrl;

            try
            {
                if (!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrWhiteSpace(imageUrl))
                {

                    Image feedIcon = null;
                    GetImageFromURL(imageUrl, ref feedIcon);

                    if (feedIcon != null)
                    {
                        icons.Images.Add(feedIcon);

                        feedNode.ImageIndex = icons.Images.Count - 1;
                        feedNode.SelectedImageIndex = icons.Images.Count - 1;
                    }
                    else
                    {
                        Console.WriteLine("Image for " + feed.Title + "'. Image-URL = " + imageUrl + " is null.");
                    }

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while trying to get the image for feed '" + feed.Title + "'. Image-URL = " + imageUrl + "   Error: " + ex.Message);
            }

            return feedNode;
        }

        /// <summary>
        /// Gets the image from URL.
        /// </summary>
        /// <param name="url">the url of the image</param>
        /// <param name="resultImage">image which gets loaded</param>
        public void GetImageFromURL(string url, ref Image resultImage)
        {
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebReponse.GetResponseStream();
                resultImage = Image.FromStream(stream);
                stream.Close();
                httpWebReponse.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine("There was a problem downloading the image from '" + url + "': " + ex.Message);
            }
        }

        /// <summary>
        /// clears and hides controls which show information about a feed-item
        /// </summary>
        private void ClearPropertyDisplays()
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

        /// <summary>
        /// lists feeditems in listview
        /// </summary>
        /// <param name="currentFeed"></param>
        private void ShowFeedList(ref Feed currentFeed)
        {
            if (currentFeed != null)
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

                if (currentFeed.Items != null)
                {
                    foreach (FeedItem feedItem in currentFeed.Items)
                    {
                        if(feedItem != null)
                        {
                            //try
                            //{
                            string update = "";

                            if (feedItem.PublishingDate != null)
                            {
                                if (feedItem.PublishingDate.Value != null)
                                {
                                    update = feedItem.PublishingDate.Value.ToShortDateString();
                                }
                            }

                            string read = "no";

                            if(feedItem.Read)
                            {
                                read = "yes";
                            }

                            string[] itemProperties = new string[] {     feedItem.Title,
                                                        update,
                                                        feedItem.Author,
                                                        read
                                                    };

                            ListViewItem item = new ListViewItem(itemProperties);
                            item.Tag = feedItem;
                            lVFeedItems.Items.Add(item);
                        }
                    }
                }
                else
                {
                    Console.WriteLine("selected feed has no items");
                }
                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lVFeedItems.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
                lVFeedItems.View = View.Details;

            }
            else
            {
                Console.WriteLine("selected Feed equals null");
            }
        }

        /// <summary>
        /// set text (timed) of the statusStripLabel
        /// </summary>
        /// <param name="text"></param>
        /// <param name="milliseconds"></param>
        public void SetStatusText(string text, int milliseconds)
        {
            statusLabel.Text = text;
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

            if (tVMain.SelectedNode != null)
            {
                object tagObj = tVMain.SelectedNode.Tag;
                if(tagObj != null)
                {
                    if(tagObj.GetType() == typeof(Feed))
                    {
                        Feed selFeed = (Feed)tagObj;

                        ShowFeedList(ref selFeed);
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
                //Console.WriteLine("number of selected items: " + listView1.SelectedItems.Count);

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
                            else
                            {
                                browser.Navigate(item.Link);
                            }
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

        
    }
}
