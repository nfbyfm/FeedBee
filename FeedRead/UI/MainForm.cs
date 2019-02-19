using CodeHollow.FeedReader;
using FeedRead.Model;
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
                //list of all the primary nodes of the treeview
                List<TreeNode> mainNodes = new List<TreeNode>();

                //add feedgroup-Nodes to the list
                if (mainModel.FeedGroups != null)
                {
                    if (mainModel.FeedGroups.Count() > 0)
                    {
                        for (int i = 0; i < mainModel.FeedGroups.Count(); i++)
                        {
                            TreeNode tmpNode = GetNode(mainModel.FeedGroups[i]);
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
                            TreeNode feedNode = new TreeNode(mainModel.FeedList[i].Title);
                            feedNode.Tag = mainModel.FeedList[i];

                            mainNodes.Add(feedNode);

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
               
               
            }
        }

        

        /// <summary>
        /// method for (recursively) getting the TreeNode (and subnodes) of a feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private TreeNode GetNode(FeedGroup group)
        {
            TreeNode groupNode = null;

            if(group != null)
            {
                groupNode = new TreeNode(group.Title);
                groupNode.Tag = group;

                //check for sub-Feedgroups and add them to this group's node
                if(group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count > 0)
                    {
                        for(int i=0; i< group.FeedGroups.Count(); i++)
                        {
                            TreeNode subNode = GetNode(group.FeedGroups[i]);
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
                        //add feeds to this group's node
                        for(int i = 0; i< group.FeedList.Count(); i++)
                        {
                            TreeNode feedNode = new TreeNode(group.FeedList[i].Title);
                            feedNode.Tag = group.FeedList[i];

                            groupNode.Nodes.Add(feedNode);
                        }
                    }
                }
            }

            return groupNode;
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

                lVFeedItems.Columns.AddRange(new ColumnHeader[] { columnHeader1, columnHeader2, columnHeader3 });

                if (currentFeed.Items != null)
                {
                    foreach (FeedItem feedItem in currentFeed.Items)
                    {
                        string[] itemProperties = new string[] {     feedItem.Title,
                                                            feedItem.PublishingDate.Value.ToShortDateString(),
                                                            feedItem.Author
                                                        };

                        ListViewItem item = new ListViewItem(itemProperties);
                        item.Tag = feedItem;
                        lVFeedItems.Items.Add(item);
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
