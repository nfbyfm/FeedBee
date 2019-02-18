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


        public MainForm()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            controller = new Controller(this);

            ClearPropertyDisplays();            
        }

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

        
        /// <summary>
        /// triggers redraw of the treeview (feeds and feed-groups)
        /// </summary>
        /// <param name="mainModel"></param>
        public void UpdateTreeView(FeedGroup mainModel)
        {
            tVMain.Nodes.Clear();


            if(mainModel != null)
            {
                List<TreeNode> mainNodes = new List<TreeNode>();
                BuildFeedTree(null, ref mainNodes, mainModel);

                if(mainNodes.Count() > 0)
                {
                    for(int i = 0; i < mainNodes.Count(); i++)
                    {
                        tVMain.Nodes.Add(mainNodes[i]);
                    }
                }
               
               
            }
        }

        /// <summary>
        /// method for recursively building the treeview with the feeds and Feed-Groups
        /// </summary>
        /// <param name="parentNode"></param>
        /// <param name="mainNodes"></param>
        /// <param name="group"></param>
        private void BuildFeedTree(TreeNode parentNode, ref List<TreeNode> mainNodes, FeedGroup group)
        {
            if(group != null)
            {
                TreeNode groupNode = new TreeNode(group.Title);
                groupNode.Tag = group;

                //handle main-Node (don't show in Treeview)
                if (parentNode == null)
                {
                    groupNode = null;
                }

                //add sub-groups
                if (group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count() > 0 )
                    {
                        for (int i = 0; i < group.FeedGroups.Count(); i++)
                        {
                            if(groupNode == null)
                            {
                                //add groups to list
                                TreeNode newGroupNode = new TreeNode(group.FeedGroups[i].Title);
                                newGroupNode.Tag = group.FeedGroups[i];
                                mainNodes.Add(newGroupNode);
                                BuildFeedTree(newGroupNode, ref mainNodes, group.FeedGroups[i]);
                            }
                            else
                            {
                                BuildFeedTree(groupNode, ref mainNodes, group.FeedGroups[i]);
                            }
                            
                        }
                    }
                }

                //add feeds
                if (group.FeedList != null)
                {
                    if (group.FeedList.Count > 0)
                    {
                        for (int i = 0; i < group.FeedList.Count(); i++)
                        {
                            TreeNode feedNode = new TreeNode(group.FeedList[i].Title);
                            feedNode.Tag = group.FeedList[i];

                            if(groupNode == null)
                            {
                                //add to list instead of groupNode
                                mainNodes.Add(feedNode);
                            }
                            else
                            {
                                groupNode.Nodes.Add(feedNode);
                            }
                            
                        }
                    }
                }

                

                if (parentNode == null)
                {
                    if (groupNode != null)
                    {
                        //add group-Node to list
                        mainNodes.Add(groupNode);
                    }
                }
                else
                {
                    if (groupNode != null)
                    {
                        //add group-Node to parent-Node
                        parentNode.Nodes.Add(groupNode);
                    }
                }
            }
        }

        /// <summary>
        /// method for reacting on users click upon treeview-Node and displaying / loading lists accordingly
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tVMain_AfterSelect(object sender, TreeViewEventArgs e)
        {
            if(tVMain.SelectedNode != null)
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
                else
                {
                    ClearPropertyDisplays();
                }
            }
            else
            {
                ClearPropertyDisplays();
            }
        }

        /// <summary>
        /// clears and hides controls which show information about a feed-item
        /// </summary>
        private void ClearPropertyDisplays()
        {
            sCMainBrowse.Panel1Collapsed = true;
            browser.Navigate("about:blank");

            l_Title.Text = "...";
            l_Update.Text = "...";
            lL_Url.Text = "";
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
                lVFeedItems.Columns.AddRange(new ColumnHeader[] { columnHeader1 });

                if (currentFeed.Items != null)
                {
                    foreach (FeedItem feedItem in currentFeed.Items)
                    {
                        ListViewItem item = new ListViewItem(feedItem.Title);
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
                            l_Title.Text = item.Title;
                            l_Update.Text = item.PublishingDateString;//.ToString("dd.MM.yyyy");
                            lL_Url.Text = item.Link;
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
            System.Diagnostics.Process.Start(lL_Url.Text);
        }

        /// <summary>
        /// download a (youtube-) video
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void b_DownloadVideo_Click(object sender, EventArgs e)
        {
            controller.DownloadVideo(lL_Url.Text);
        }
    }
}
