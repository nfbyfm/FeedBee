using FeedSubs.FeedReader;
using FeedRead.UI;
using FeedRead.Utilities;
using FeedRead.Utilities.OPML;
//using FeedLister;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;
using Utilities.FeedSubs;
using FeedRead.Utilities.FeedSubs;

namespace FeedRead.Control
{
    /// <summary>
    /// main Class for controlling everything
    /// </summary>
    public class Controller
    {
        
        private MainForm mainForm;
        private FeedGroup mainModel;

        private WebPageFeedDefList webPageFeedDefList;

        public const string mainModelID = "mainModel";
        private const string youtubeID = "Youtube";

        private InternetCheck internetCheck;
        
        public Controller(MainForm mainForm, InternetCheck iCheck)
        {
            this.mainForm = mainForm;
            this.mainModel = new FeedGroup(mainModelID, false, "");
            this.internetCheck = iCheck;

            //load list of webpageFeed-definitions
            LoadWebPageFeedList();

            

            //load default-List upon startup
            if (Properties.Settings.Default.bLoadUponStartup)
            {
                try
                {
                    OpenListFromXML(Properties.Settings.Default.loadListPath);
                }
                catch(Exception ex)
                {
                    Debug.WriteLine("Error while loading list: " + ex.Message);
                }

            }

            //update feeds upon load
            if(Properties.Settings.Default.updateUponLoad)
            {
                UpdateFeeds();
            }
        }

        #region UI-Functions

        /// <summary>
        /// open an exisiting list (xml-file) of feeds
        /// </summary>
        public void OpenList()
        {
            OpenFileDialog odi = new OpenFileDialog();
            odi.Title = "Open Feed-List";
            odi.RestoreDirectory = true;
            odi.Multiselect = false;
            odi.Filter = "xml-File|*.xml";
            
            if(odi.ShowDialog() == DialogResult.OK)
            {
                OpenListFromXML(odi.FileName);

                UpdateTreeview();
            }
        }

        /// <summary>
        /// save current list as xml-file
        /// </summary>
        public void SaveList()
        {
            SaveFileDialog sadi = new SaveFileDialog();
            sadi.Title = "Save Feed-List";
            sadi.RestoreDirectory = true;
            sadi.Filter = "xml-File|*.xml";

            if (sadi.ShowDialog() == DialogResult.OK)
            {
                if (mainModel != null)
                {
                    SaveListAsXML(sadi.FileName);
                }

            }
        }


        /// <summary>
        /// import a list of feeds either as a txt- or ompl-file
        /// </summary>
        public void ImportFeedList()
        {
            OpenFileDialog odi = new OpenFileDialog();
            odi.Title = "Import feeds-file";
            odi.RestoreDirectory = true;
            odi.Multiselect = false;
            odi.Filter = "ompl-file|*.opml";

            //only import from txt-file if internet-connectivity is guaranteed
            if (CheckInternetConnectivity())
            {
                odi.Filter += "|txt-file|*.txt";
            }

            if(odi.ShowDialog() == DialogResult.OK)
            {
                switch(odi.FilterIndex)
                {
                    case 1:
                        //open opml-file
                        ImportFromOPML(odi.FileName);
                        break;
                    case 2:
                        //open txt-file
                        ImportFromTxt(odi.FileName);
                        break;
                    default:
                        //do nothing
                        break;
                }
            }
        }

        

        /// <summary>
        /// export the current mainModel / FeedGroup
        /// </summary>
        public void ExportFeedList()
        {
            SaveFileDialog sadi = new SaveFileDialog();
            sadi.Title = "Export feeds";
            sadi.RestoreDirectory = true;
            sadi.Filter = "txt-File|*.txt|ompl-File|*.opml";

            if (sadi.ShowDialog() == DialogResult.OK)
            {
                switch(sadi.FilterIndex)
                {
                    case 1:
                        //export as txt-file
                        SaveListAsTxt(sadi.FileName);
                        break;
                    case 2:
                        //export as opml-file
                        SaveListAsOPML(sadi.FileName);
                        break;
                    default:
                        break;
                }
                
                
            }
        }

       

        /// <summary>
        /// add a new feed to the current mainModel / FeedGroup
        /// </summary>
        public void AddNewFeed()
        {
            if(CheckInternetConnectivity())
            {
                AddFeedDialog addFeedDialog = new AddFeedDialog(this);
                if (addFeedDialog.ShowDialog() == DialogResult.OK)
                {
                    //show next dialog (add Feed to a Group)
                    string newFeedUrl = addFeedDialog.feedUrl;

                    //Debug.WriteLine("Controller.AddNewFeed: got new feed-source from user: " + newFeedUrl);

                    //show group-Dialog
                    SelectGroupDialog sGD = new SelectGroupDialog(GetGroupNames());

                    if (sGD.ShowDialog() == DialogResult.OK)
                    {
                        //get group-name
                        string groupName = sGD.groupName;
                        bool groupIsNSFW = sGD.newGroupIsNSFW;

                        Feed newFeed = null;

                        

                        
                        //check if it's a new group
                        if (sGD.addNewGroupName)
                        {
                            //create a new group and add the feed to it
                            //Debug.WriteLine("Controller.AddNewFeed: add feed '" + newFeedUrl + "' to new group '" + groupName + "'.");
                            GetFeed(newFeedUrl, ref newFeed, true);

                            if (newFeed != null)
                            {
                                mainModel.AddFeedAndGroup(newFeed, groupName, groupIsNSFW, "");

                                UpdateTreeview();
                            }
                        }
                        else
                        {
                            //find selected group
                            //check if feed already exists and if not, add the new feed to it
                            //Debug.WriteLine("Controller.AddNewFeed: add feed '" + newFeedUrl + "' to existing group '" + groupName + "'");
                            FeedGroup parentGroup = GetGroupByName(mainModel, groupName);

                            if(IsFeedInGroup(parentGroup,newFeedUrl))
                            {
                                MessageBox.Show("Feed already exists in this Feedgroup!", "Add Feed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                            }
                            else
                            {
                                GetFeed(newFeedUrl, ref newFeed, true);
                                if (newFeed != null)
                                {
                                    mainModel.AddFeed(newFeed, groupName);

                                    UpdateTreeview();
                                }
                            }
                                
                        }
                 


                    }
                }
            }
            else
            {
                MessageBox.Show("No internet-connection could be detected. Can't add a new feed.", "Add new feed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// get list of unread feeditems of a specific feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        public List<FeedItem> GetUnreadFeedItems(FeedGroup group)
        {
            List<FeedItem> result = new List<FeedItem>();

            if(group!=null)
            {
                if(group.FeedGroups!=null)
                {
                    if(group.FeedGroups.Count > 0)
                    {
                        foreach(FeedGroup subgroup in group.FeedGroups)
                        {
                            result.AddRange(GetUnreadFeedItems(subgroup));
                        }
                    }
                }

                if(group.FeedList!=null)
                {
                    if(group.FeedList.Count > 0)
                    {
                        foreach(Feed feed in group.FeedList)
                        {
                            if(feed.GetNoOfUnreadItems() > 0)
                            {
                                foreach(FeedItem item in feed.Items)
                                {
                                    if(item.Read == false)
                                    {
                                        result.Add(item);
                                    }
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }



        /// <summary>
        /// recursively search for group with given name
        /// </summary>
        /// <param name="parentGroup"></param>
        /// <param name="groupName"></param>
        /// <returns></returns>
        private FeedGroup GetGroupByName(FeedGroup parentGroup, string groupName)
        {
            FeedGroup result = null;

            if (parentGroup != null)
            {
                if(parentGroup.Title.ToLower() == groupName.ToLower())
                {
                    result = parentGroup;
                }
                else
                {
                    if(parentGroup.FeedGroups != null)
                    {
                        if(parentGroup.FeedGroups.Count > 0)
                        {
                            for(int i = 0; i< parentGroup.FeedGroups.Count(); i++)
                            {
                                result = GetGroupByName(parentGroup.FeedGroups[i], groupName);
                                if(result != null)
                                {
                                    //Debug.WriteLine("Group with name '" + groupName + "' as been found.");
                                    break;
                                }
                            }
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// checks if a feed is already in the given feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <param name="feedurl"></param>
        /// <returns></returns>
        private bool IsFeedInGroup(FeedGroup group, string feedurl)
        {
            bool result = false;

            string cleanFeedURL = feedurl.ToLower().Replace(" ", "").Replace("https","").Replace("http","");

            //bool continueSearch = true;

            if (group!=null)
            {
                //check FeedList first
                if(group.FeedList != null)
                {
                    if(group.FeedList.Count > 0 )
                    {
                        for(int i = 0; i <group.FeedList.Count; i++)
                        {
                            string groupUrl = group.FeedList[i].FeedURL.ToLower().Replace(" ", "").Replace("https", "").Replace("http", "");

                            if (groupUrl == cleanFeedURL)
                            {
                                //continueSearch = false;
                                result = true;
                                break;
                            }
                        }
                    }
                }
                /*
                if(group.FeedGroups != null && continueSearch)
                {
                    //search in sub-groups
                    if(group.FeedGroups.Count > 0)
                    {
                        for(int i = 0; i<group.FeedGroups.Count; i++)
                        {
                            
                        }
                    }
                }
                */
            }

            return result;
        }

        public delegate void UpdateTreeViewCallback();

        /// <summary>
        /// update feed-list / get new feeditems in separate thread
        /// </summary>
        public void UpdateFeeds()
        {
            //check if update is even possible
            if(CheckInternetConnectivity())
            {
                //start the update-thread
                mainForm.EnableFeedFunctionalities(false);
                Thread t2 = new Thread(delegate ()
                {
                    mainForm.SetStatusText("updating feeds ...", -1);
                    UpdateFeed(!Properties.Settings.Default.updateNSFW);
                    mainForm.Invoke(new UpdateTreeViewCallback(mainForm.UpdateTreeViewUnlock));
                    mainForm.SetStatusText("feeds updated", 2000);
                });
                t2.Start();
            }
            else
            {
                mainForm.SetStatusText("no connection to internet detected ...", 5000);
            }
            
        }

        


        /// <summary>
        /// open all unread feeds in external browser
        /// </summary>
        public void OpenUnreadFeeds()
        {
            if(mainModel != null)
            {
                OpenUnreadFeeds(mainModel, false);
            }
        }

        /// <summary>
        /// show the "about"-Dialog
        /// </summary>
        public void ShowAboutDialog()
        {
            AboutBoxFeedRead aboutWindow = new AboutBoxFeedRead();
            aboutWindow.ShowDialog();
        }

        /// <summary>
        /// show settings-dialog
        /// </summary>
        public void ShowSettings()
        {
            SettingsDialog sedi = new SettingsDialog();
            if(sedi.ShowDialog() == DialogResult.OK)
            {
                //save settings
                Properties.Settings.Default.Save();
            }
        }

        /// <summary>
        /// close the whole application
        /// </summary>
        public void CloseApplication()
        {
            //try to save the webpagedefinition-list
            SaveWebPageFeedDefs();

            //check if Model has any changes that should get saved
            //opmlDoc = null;
            //mainModel = null;
            if (Properties.Settings.Default.bLoadUponStartup)
            {
                if(mainModel != null)
                {
                    if(mainModel.FeedGroups != null && mainModel.FeedList != null)
                    {
                        if(mainModel.FeedGroups.Count()>0 || mainModel.FeedList.Count() >0)
                        {
                            try
                            {
                                SaveListAsXML(Properties.Settings.Default.loadListPath);
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show("Error while saving current list to xml-file: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
            }

            //close Application / MainForm
            mainForm.Close();
        }





        /// <summary>
        /// update the treeview-control of the main form
        /// </summary>
        private void UpdateTreeview()
        {
            mainForm.UpdateTreeView();
        }
        

        /// <summary>
        /// get the string-ID for youtube
        /// </summary>
        /// <returns></returns>
        public string GetYoutubeID()
        {
            return youtubeID;
        }

        /// <summary>
        /// get the ID of the mainModel
        /// </summary>
        /// <returns></returns>
        public string GetMainModelID()
        {
            return mainModelID;
        }

        /// <summary>
        /// mark all feeds as read
        /// </summary>
        public void MarkAllAsRead()
        {
            if (mainModel != null)
            {
                MarkGroupAsRead(mainModel);
                UpdateTreeview();
            }
        }

        

        /// <summary>
        /// download a (youtube-) video 
        /// </summary>
        /// <param name="url"></param>
        public void DownloadVideo(string url)
        {
            try
            {
                //call youtube-dl.exe
                Process p = new Process();
                ProcessStartInfo pi = new ProcessStartInfo();

                string argument = "/c start " + Properties.Settings.Default.youtubedlFolder + "\\youtube-dl.exe " + url + " -o \"" + Properties.Settings.Default.youtubedlFolder.Replace("\\", "/") + "/%(title)s.%(ext)s\" && exit";


                pi.Arguments = argument;
                pi.FileName = "cmd.exe";
                p.StartInfo = pi;
                p.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while downloading the video. Errormessage: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        #endregion


        #region get / delete Feeds and Groups

        /// <summary>
        /// search for a feed / feedgroup with the given hash-code
        /// </summary>
        /// <param name="hash">HasCode we're looking for</param>
        /// <returns>null, Feed or FeedGroup-Object if it could be found</returns>
        private object GetFeedElementFromHash(FeedGroup subGroup, int hash)
        {
            object result = null;

            if(subGroup != null)
            {
                bool continueSearch = true;

                //check current Group
                if(subGroup.GetHashCode() == hash)
                {
                    result = subGroup;
                    continueSearch = false;
                }

                //search in feedlist first
                if ((subGroup.FeedList != null) && (continueSearch == true))
                {
                    if(subGroup.FeedList.Count() > 0)
                    {
                        for(int i =0; i< subGroup.FeedList.Count(); i++)
                        {
                            if(subGroup.FeedList[i].GetHashCode() == hash)
                            {
                                continueSearch = false;
                                result = subGroup.FeedList[i];
                                break;
                            }
                        }
                    }
                }

                //search in sub-groups
                if ((subGroup.FeedGroups != null) && (continueSearch == true))
                {
                    if(subGroup.FeedGroups.Count() > 0)
                    {
                        for (int i = 0; i < subGroup.FeedGroups.Count(); i++)
                        {
                            //search in subgroup
                            object tmpResult = GetFeedElementFromHash(subGroup.FeedGroups[i], hash);
                            if(tmpResult != null)
                            {
                                result = tmpResult;
                                break;
                            }
                        }
                    }
                }

                
            }

            return result;
        }

        /// <summary>
        /// identify and delete an element in the mainModel by it's Hash-Code
        /// </summary>
        /// <param name="hash"></param>
        /// <returns>returns true if item could be found and deleted</returns>
        private bool DeleteFeedElementByHash(FeedGroup subGroup, int hash)
        {
            bool result = false;

            if (subGroup != null)
            {
                bool continueSearch = true;

                //search in feedlist first
                if ((subGroup.FeedList != null) && (continueSearch == true))
                {
                    if (subGroup.FeedList.Count() > 0)
                    {
                        for (int i = 0; i < subGroup.FeedList.Count(); i++)
                        {
                            if (subGroup.FeedList[i].GetHashCode() == hash)
                            {
                                continueSearch = false;
                                subGroup.FeedList.RemoveAt(i);
                                result = true;
                                break;
                            }
                        }
                    }
                }

                //search in sub-groups
                if ((subGroup.FeedGroups != null) && (continueSearch == true))
                {
                    if (subGroup.FeedGroups.Count() > 0)
                    {
                        bool groupfound = false;

                        //search in group-list herself first
                        for (int i = 0; i < subGroup.FeedGroups.Count(); i++)
                        {
                            if(subGroup.FeedGroups[i].GetHashCode() == hash)
                            {
                                groupfound = true;
                                subGroup.FeedGroups.RemoveAt(i);
                                result = true;
                                break;
                            }
                        }

                        //start search in subgroups themselfes
                        if(groupfound == false)
                        {
                            for (int i = 0; i < subGroup.FeedGroups.Count(); i++)
                            {
                                //search in subgroup
                                result = DeleteFeedElementByHash(subGroup.FeedGroups[i], hash);

                                if (result == true)
                                {
                                    break;
                                }
                            }
                            
                        }
                    }
                }


            }

            return result;
        }

        #endregion

        #region Context-Menu-GUI-functions
        //expensive-finder-function:
                /*
                object tmpResult = GetFeedElementFromHash(mainModel, tagObject.GetHashCode());

                if(tmpResult != null)
                {
                    if (tmpResult.GetType() == typeof(FeedGroup))
                    {
                        FeedGroup selFeedGroup = (FeedGroup)tmpResult;

                        Debug.WriteLine("RenameNode: Changeing Name of FeedGroup '" + selFeedGroup.Title + "' to: " + newName);

                        selFeedGroup.Title = newName;
                        UpdateTreeview();
                    }
                    else if (tmpResult.GetType() == typeof(Feed))
                    {
                        Feed selFeed = (Feed)tmpResult;

                        Debug.WriteLine("RenameNode: Changeing Name of Feed '" + selFeed.Title + "' to: " + newName);

                        selFeed.Title = newName;
                        UpdateTreeview();
                    }
                    else
                    {
                        Debug.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                    }
                }
                else
                {
                    Debug.WriteLine("Couldn't find a Feed or FeedGroup in the current mainModel with the Hash-Code of the selected TreeNode-Tag-Object. HashCode: " + tagObject.GetHashCode().ToString());
                }

                */
        /// <summary>
        /// rename a Node (Group or Feed)
        /// </summary>
        /// <param name="tagObject"></param>
        /// <param name="newName"></param>
        public void RenameNode(object tagObject)
        {
            if(tagObject != null)
            {
                if (tagObject.GetType() == typeof(FeedGroup))
                {
                    FeedGroup selFeedGroup = (FeedGroup)tagObject;

                    //get new name
                    EditGroupDialog rd = new EditGroupDialog(selFeedGroup.Title, selFeedGroup.IsNSFWGroup, selFeedGroup.ImagePath);

                    if (rd.ShowDialog() == DialogResult.OK)
                    {
                        string newNodeName = rd.newName;
                        
                        if (!string.IsNullOrEmpty(newNodeName) && !string.IsNullOrWhiteSpace(newNodeName))
                        {
                            //set feedgroup-properties
                            selFeedGroup.Title = newNodeName;
                            selFeedGroup.IsNSFWGroup = rd.IsNSFW;
                            selFeedGroup.ImagePath = rd.iconPath;

                            UpdateTreeview();
                        }
                    }

                    rd.Dispose();

                }
                else if (tagObject.GetType() == typeof(Feed))
                {
                    Feed selFeed = (Feed)tagObject;

                    //get new name
                    EditFeedDialog fd = new EditFeedDialog(selFeed.Title, selFeed.DirectlyLoadWebpage, selFeed.ImageUrl);
                    
                    if (fd.ShowDialog() == DialogResult.OK)
                    {
                        string newFeedName = fd.feedTitle;

                        
                        if (!string.IsNullOrEmpty(newFeedName) && !string.IsNullOrWhiteSpace(newFeedName))
                        {
                            //set the feed-properties
                            selFeed.Title = fd.feedTitle;
                            selFeed.DirectlyLoadWebpage = fd.directlyLoadWebURL;
                            selFeed.ImageUrl = fd.iconPath;
                            UpdateTreeview();
                        }
                    }
                    fd.Dispose();
                }
                else
                {
                    Debug.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                }

            }
            else
            {
                Debug.WriteLine("RenameNode: tagObject of selected Treenode is null.");
            }
        }

        

        /// <summary>
        /// delete a feed or feedgroup by it's hashcode
        /// </summary>
        /// <param name="tagObject"></param>
        public void DeleteNode(object tagObject)
        {
            if (tagObject != null)
            {
                bool result = DeleteFeedElementByHash(mainModel, tagObject.GetHashCode());

                if (result == true)
                {
                    UpdateTreeview();
                }
                else
                {
                    Debug.WriteLine("DeleteNode: couldn't find element of selected Treenode is null.");
                }
            }
            else
            {
                Debug.WriteLine("DeleteNode: tagObject of selected Treenode is null.");
            }
        }

        /// <summary>
        /// update a selected feed or feedgroup
        /// </summary>
        /// <param name="tagObject"></param>
        public void UpdateNode(object tagObject)
        {
            //check if update is even possible
            if (CheckInternetConnectivity())
            {

                if (tagObject != null)
                {
                    if (tagObject.GetType() == typeof(FeedGroup))
                    {
                        FeedGroup selFeedGroup = (FeedGroup)tagObject;

                        //start the update-thread
                        mainForm.EnableFeedFunctionalities(false);

                        Thread t2 = new Thread(delegate ()
                        {
                            mainForm.SetStatusText("updating feeds of '" + selFeedGroup.Title + "' ...", -1);

                            UpdateFeedList(selFeedGroup, !Properties.Settings.Default.updateNSFW);

                            mainForm.Invoke(new UpdateTreeViewCallback(mainForm.UpdateTreeViewUnlock), mainModel);
                            mainForm.SetStatusText("feeds of '" + selFeedGroup.Title + "' updated.", 2000);
                        });
                        t2.Start();
                    
                    }
                    else if (tagObject.GetType() == typeof(Feed))
                    {
                        Feed selFeed = (Feed)tagObject;

                        mainForm.SetStatusText("Updating '" + selFeed.Title + "' ...", 1000);

                        UpdateFeed(selFeed);

                        UpdateTreeview();

                        mainForm.SetStatusText("Feed '" + selFeed.Title + "' updated.", 2000);

                    }
                    else
                    {
                        Debug.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                    }

                }
                else
                {
                    Debug.WriteLine("RenameNode: tagObject of selected Treenode is null.");
                }
            }
            else
            {
                mainForm.SetStatusText("no connection to internet detected ...", 5000);
            }
        }

        /// <summary>
        /// mark feed or feedgroup as read
        /// </summary>
        /// <param name="tagObject"></param>
        public void MarkNodeAsRead(object tagObject)
        {
            if (tagObject != null)
            {
                if (tagObject.GetType() == typeof(FeedGroup))
                {
                    FeedGroup selFeedGroup = (FeedGroup)tagObject;

                    MarkGroupAsRead(selFeedGroup);

                    UpdateTreeview();
                }
                else if (tagObject.GetType() == typeof(Feed))
                {
                    Feed selFeed = (Feed)tagObject;

                    MarkFeedAsRead(selFeed, true);

                    UpdateTreeview();
                }
                else
                {
                    Debug.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                }

            }
            else
            {
                Debug.WriteLine("RenameNode: tagObject of selected Treenode is null.");
            }
        }

        /// <summary>
        /// open unread feeds of a specific feedgroup
        /// </summary>
        /// <param name="subGroup"></param>
        public void OpenUnreadFeeds(FeedGroup subGroup)
        {
            OpenUnreadFeeds(subGroup, false);
        }


        #endregion

        #region webpagefeed-definitonlist functions
        /// <summary>
        /// tries to load defintions from file
        /// </summary>
        private void LoadWebPageFeedList()
        {
            bool loadSuccess = false;

            webPageFeedDefList = new WebPageFeedDefList();
            webPageFeedDefList.Definitions = new List<WebPageFeedDef>();

            if(File.Exists(Properties.Settings.Default.WebpageFeedDefPath))
            {
                try
                {
                    StreamReader reader = new StreamReader(Properties.Settings.Default.WebpageFeedDefPath);

                    XmlSerializer ser = new XmlSerializer(typeof(WebPageFeedDefList));
                    webPageFeedDefList = (WebPageFeedDefList)ser.Deserialize(reader);

                    reader.Close();
                    reader.Dispose();
                    loadSuccess = true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while loading FeedList:" + Environment.NewLine + ex.Message);
                }
            }

            if(loadSuccess == false)
            {
                //couldn't load the definitions -> define at least the default-entries

                //mangarock:
                WebPageFeedDef mangarockDef = new WebPageFeedDef("Mangarock", "mangarock", "https://mangarock.com", "//*[@class='_2dU-m _1qbNn']", "//*[@class='_1D0de col-4 col-md-3']");
                

                webPageFeedDefList = new WebPageFeedDefList(mangarockDef);

                Console.WriteLine("Couldn't load webpageDefinitions from Settings-file -> set default entries");
            }
        }

        /// <summary>
        /// try to save webpagefeeddefinitions
        /// </summary>
        private void SaveWebPageFeedDefs()
        {
            try
            {
                if (webPageFeedDefList != null)
                {
                    XmlSerializer ser = new XmlSerializer(typeof(WebPageFeedDefList));
                    TextWriter writer = new StreamWriter(Properties.Settings.Default.WebpageFeedDefPath);
                    ser.Serialize(writer, webPageFeedDefList);
                    writer.Close();

                    Console.WriteLine("Saved current list of webpagefeed-definitions to: " + Properties.Settings.Default.WebpageFeedDefPath);
                }
                else
                {
                    Console.WriteLine("Controller.SaveList: webpagefeedList is null. Can't save.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while saving to xml-file: " + Environment.NewLine + ex.Message);
            }

        }
        
        #endregion

        #region Feed-related functions

        /// <summary>
        /// get List of all the group-names
        /// </summary>
        /// <returns></returns>
        private List<string> GetGroupNames()
                {
                    List<string> result = null;

                    if(mainModel != null)
                    {
                        if(mainModel.FeedGroups != null)
                        {
                            if(mainModel.FeedGroups.Count >0)
                            {
                                result = new List<string>();

                                //go through list recursively (?)

                                //simple version:
                                foreach(FeedGroup group in mainModel.FeedGroups)
                                {
                                    result.Add(group.Title);
                                }
                            }
                        }
                    }

                    return result;
                }

        /// <summary>
        /// load current model from the given xml-file
        /// </summary>
        /// <param name="filename"></param>
        private void OpenListFromXML(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);

                XmlSerializer ser = new XmlSerializer(typeof(FeedGroup));
                mainModel = (FeedGroup)ser.Deserialize(reader);

                reader.Close();
                reader.Dispose();

                Debug.WriteLine("List of feed groups opened. Filename: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading FeedList:" + Environment.NewLine + ex.Message);
            }
        }


        private void ImportFromOPML(string filename)
        {
            Console.WriteLine("import opml-file");

            try
            {
                OPML opmlDoc = new OPML(filename);
                

                if (opmlDoc != null)
                {
                    if (opmlDoc.Body != null)
                    {
                        Console.WriteLine(opmlDoc.Body.ToString());
                        /*
                        List<Outline> outlineList = opmlDoc.Body.Outlines;
                        if (outlineList != null)
                        {
                            foreach (Outline oline in outlineList)
                            {
                                if (oline.IsFinalNode())
                                {
                                    Console.WriteLine(oline.ToString());
                                }
                                else
                                {
                                    Console.WriteLine("Outline '" + oline.Title + "' has " + oline.Outlines.Count().ToString() + " sub items:");
                                    foreach (Outline o2line in oline.Outlines)
                                    {
                                        Console.WriteLine(oline.Title + " -> " + o2line.ToString());
                                    }
                                }
                            }
                        }
                        else
                        {
                            Console.WriteLine("Outline-List is null.");
                        }
                        */
                    }
                    else
                    {
                        Console.WriteLine("Body of ompl-File is null.");
                    }

                }
                else
                {
                    Console.WriteLine("OPML-File is null.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error while importing opml-file. Error: " + ex.Message);
            }
        }

        /// <summary>
        /// import a list of feeds from a txt-file and add them to a group
        /// </summary>
        /// <param name="filename"></param>
        private void ImportFromTxt(string filename)
        {
            if(File.Exists(filename))
            {
                //show group-Dialog
                SelectGroupDialog sGD = new SelectGroupDialog(GetGroupNames());

                if (sGD.ShowDialog() == DialogResult.OK)
                {
                    //get group-name
                    string groupName = sGD.groupName;

                    //check if group has to get added first
                    bool groupAdded = false;
                    bool groupIsNSFW = sGD.newGroupIsNSFW;

                    if (!sGD.addNewGroupName)
                    {
                        groupAdded = true;
                    }

                    mainForm.EnableFeedFunctionalities(false);

                    //start own new thread for importing
                    Thread t2 = new Thread(delegate ()
                    {
                        mainForm.SetStatusText("importing feeds ...", -1);

                        ImportFromTxtSubFunction(filename, ref groupAdded, groupName, groupIsNSFW);

                        mainForm.Invoke(new UpdateTreeViewCallback(mainForm.UpdateTreeViewUnlock), mainModel);
                        mainForm.SetStatusText("import done", 2000);
                    });
                    t2.Start();


                }

            }
            else
            {
                MessageBox.Show("Error: Selected file doesn't exist.", "Import feed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// add each line in a text-file to the specified group
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="groupAdded"></param>
        /// <param name="groupName"></param>
        private void ImportFromTxtSubFunction(string filename, ref bool groupAdded, string groupName, bool groupIsNSFW)
        {
            var feedFile = File.ReadAllLines(filename);
            List<string> lines = new List<string>(feedFile);

            if (lines != null)
            {
                if (lines.Count() > 0)
                {
                    foreach (string line in lines)
                    {
                        Feed newFeed = new Feed();

                        GetFeed(line, ref newFeed, true);

                        if (newFeed != null)
                        {
                            if (groupAdded == false)
                            {
                                //add new group and the the feed to it
                                mainModel.AddFeedAndGroup(newFeed, groupName, groupIsNSFW, "");
                                groupAdded = true;
                            }
                            else
                            {
                                //add feed to existing grooup
                                mainModel.AddFeed(newFeed, groupName);
                            }
                        }
                    }

                }
            }
        }

        /// <summary>
        /// get feed from a url
        /// </summary>
        /// <param name="url"></param>
        /// <param name="newFeed"></param>
        private void GetFeed(string url, ref Feed newFeed, bool getImage)
        {
            bool foundInWebPageDef = false;
            foreach(WebPageFeedDef webdef in webPageFeedDefList.Definitions)
            {
                if(url.ToLower().Contains(webdef.KeyID))
                {
                    newFeed = new Feed();
                    ComicFeedReader reader = new ComicFeedReader();
                    reader.Read(url, ref newFeed, webdef);

                    if (newFeed != null)
                    {
                        foundInWebPageDef = true;
                        break;
                    }
                }
            }

            if(!foundInWebPageDef)
            {
                newFeed = FeedReader.Read(url);
                newFeed.FeedURL = url;
                newFeed.Updated = true;
            }
            

            //get icon for treeview
            if (!IsValidURL(newFeed.ImageUrl) && Properties.Settings.Default.displayFeedIcons && getImage)
            {
                //try to get the image-path from the main webpage first
                string tmpImageUrl = GetFaviconOfWebpage(newFeed.Link);

                if (string.IsNullOrEmpty(tmpImageUrl) || string.IsNullOrWhiteSpace(tmpImageUrl))
                {
                    //try to get image-path from one of the feed-item-pages
                    if (newFeed.Items.Count > 0)
                    {
                        newFeed.ImageUrl = GetFaviconOfWebpage(newFeed.Items[0].Link);
                    }
                }
                else
                {
                    newFeed.ImageUrl = tmpImageUrl;
                }


            }
        }

        /// <summary>
        /// save current Feedgroup as xml-file
        /// </summary>
        /// <param name="filename"></param>
        private void SaveListAsXML(string filename)
        {
            try
            {
                if(mainModel != null)
                {
                    XmlSerializer ser = new XmlSerializer(typeof(FeedGroup));
                    TextWriter writer = new StreamWriter(filename);
                    ser.Serialize(writer, mainModel);
                    writer.Close();

                    Debug.WriteLine("Saved current list of feed-groups to: " + filename);
                }
                else
                {
                    Debug.WriteLine("Controller.SaveList: main Model is null. Can't save.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while saving to xml-file: " + Environment.NewLine + ex.Message, "Error while saving data", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        /// <summary>
        /// save every feed-url in a txt-file
        /// </summary>
        /// <param name="filename"></param>
        private void SaveListAsTxt(string filename)
        {
            string result = "";
            GetFeedsFromGroup(mainModel, ref result);

            StreamWriter writer = new StreamWriter(filename);
            writer.Write(result);
            writer.Close();
            writer.Dispose();

            mainForm.SetStatusText("Successfully exported txt-file to: " + filename, 2000);
        }

        /// <summary>
        /// save current Model as an opml-file
        /// </summary>
        /// <param name="filename"></param>
        private void SaveListAsOPML(string filename)
        {
            OPML opmlDoc = GetOPMLFromModel();
            if (opmlDoc != null)
            {
                StreamWriter writer = new StreamWriter(filename);
                writer.Write(opmlDoc.ToString());
                writer.Close();
                writer.Dispose();

                mainForm.SetStatusText("Successfully exported opml-file to: " + filename, 2000);
            }
        }

        /// <summary>
        /// update each feed of the main model
        /// </summary>
        /// <param name="atWork">if true nsfw-feeds won't get checked</param>
        private void UpdateFeed(bool atWork)
        {
            if(mainModel != null)
            {
                UpdateFeedList(mainModel, atWork);
            }
        }

        /// <summary>
        /// get updates for a specific feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <param name="atWork"></param>
        private void UpdateFeedList(FeedGroup group, bool atWork)
        {
            if(group != null)
            {
                //create a list of threads
                List<Thread> tList = new List<Thread>();

                if (group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count() > 0 )
                    {
                        for (int i = 0; i < group.FeedGroups.Count(); i++)
                        {
                            tList.AddRange(GetThreadsFromGroup(group.FeedGroups[i], atWork));
                        }
                    }
                }

                if(group.FeedList != null)
                {
                    bool checkNSFW = true;

                    //if "atWork" = true -> check if group has nsfw-flag -> ignore / don't update if so
                    if(atWork)
                    {
                        checkNSFW = !group.IsNSFWGroup;//.Title.ToLower().Contains("nsfw");
                    }

                    if(group.FeedList.Count > 0 && checkNSFW)
                    {
                        for (int i =0; i< group.FeedList.Count(); i++)
                        {
                            Feed fed = group.FeedList[i];
                            //define own new thread for importing
                            Thread t1 = new Thread(delegate ()
                            {
                                UpdateFeed(fed);
                            });
                            
                            //add thread to list
                            tList.Add(t1);
                        } 
                    }
                }


                //actually run all the threads

                if(tList.Count > 0)
                {
                    int numberOfThreads = tList.Count();
                    //Debug.WriteLine("new List of threads created. Number of threads in list: " + numberOfThreads);

                    //start every thread at once
                    foreach (Thread th in tList)
                    {
                        th.Start();
                    }

                    bool updateFinished = false;
                    int counter_before = 0;

                    //wait until each thread ist finished
                    while (!updateFinished)
                    {
                        int counter = 0;

                        foreach (Thread thr in tList)
                        {
                            if (thr.IsAlive == false)
                            {
                                counter++;
                            }
                        }

                        if (counter != counter_before)
                        {
                            mainForm.SetStatusText("Updated " + counter.ToString() + " feeds out of " + numberOfThreads + " ...", -1);
                            //Debug.WriteLine(counter.ToString() + " threads out of " + numberOfThreads + " are done.");
                            counter_before = counter;
                        }

                        if (counter == numberOfThreads)
                        {
                            updateFinished = true;
                        }
                    }

                    tList.Clear();
                }
                
            }
        }

        /// <summary>
        /// get update-threads from a feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <param name="atWork"></param>
        /// <returns></returns>
        private List<Thread> GetThreadsFromGroup(FeedGroup group, bool atWork)
        {
            List<Thread> result = new List<Thread>();

            if(group != null)
            {
                if(atWork && group.IsNSFWGroup)
                {
                    //do nothing / don't update group
                }
                else
                {
                    if (group.FeedGroups != null)
                    {
                        if (group.FeedGroups.Count() > 0)
                        {
                            for(int i = 0; i < group.FeedGroups.Count(); i++)
                            {
                                result.AddRange(GetThreadsFromGroup(group.FeedGroups[i], atWork));
                            }
                        }
                    }

                    if (group.FeedList != null)
                    {
                        if (group.FeedList.Count() > 0)
                        {
                            for (int i = 0; i < group.FeedList.Count(); i++)
                            {
                                Feed fed = group.FeedList[i];
                                //define own new thread for importing
                                Thread t1 = new Thread(delegate ()
                                {
                                    UpdateFeed(fed);
                                });

                                //add thread to list
                                result.Add(t1);
                            }
                        }
                    }
                }
                

            }

            return result;
        }
        

        /// <summary>
        /// update a single feed
        /// </summary>
        /// <param name="origFeed"></param>
        private void UpdateFeed(Feed origFeed)
        {
            try
            {
                //Debug.WriteLine("Starting update on '" + origFeed.Title + "' ...");

                //get Feeditems
                Feed tmpFeed = null;

                bool updateSuccess = false;

                string url = origFeed.FeedURL;

                bool updateImage = !File.Exists(origFeed.ImageUrl);

                //try to get current feed
                GetFeed(url, ref tmpFeed, updateImage);

                if (tmpFeed != null)
                {
                    if (tmpFeed.Items != null)
                    {
                        if (tmpFeed.Items.Count() > 0)
                        {
                            List<FeedItem> updateList = tmpFeed.Items.OrderByDescending(o => o.PublishingDate).ToList();

                            //update the image-url (if not saved to file already)
                            if(updateImage)
                            {
                                origFeed.ImageUrl = tmpFeed.ImageUrl;
                            }
                            

                            origFeed.Items = origFeed.Items.OrderByDescending(o => o.PublishingDate).ToList();

                            int itemCountNew = updateList.Count();
                            int itemCountOld = origFeed.Items.Count();

                            if (itemCountNew > 0) //if ((itemCountNew != itemCountOld) && (itemCountNew > 0))
                            {
                                //Debug.WriteLine("'" + group.FeedList[i].Title + "': New: " + itemCountNew.ToString() + "  old: " + itemCountOld.ToString());

                                List<FeedItem> tmpSwapList = new List<FeedItem>();

                                //count number of matches
                                int matchCounter = 0;
                                int maxMatches = 5;

                                //compare both lists
                                foreach (FeedItem newItem in updateList)
                                {
                                    newItem.Read = false;


                                    bool found = false;

                                    //compare new item with all the old items
                                    foreach (FeedItem oldItem in origFeed.Items)
                                    {
                                        //compare id's
                                        if (newItem.Id == oldItem.Id)
                                        {
                                            found = true;
                                            matchCounter++;
                                            break;
                                        }
                                    }

                                    //if item couldn't be found in list -> add to temp. List
                                    if (found == false)
                                    {
                                        tmpSwapList.Add(newItem);
                                        matchCounter = 0;
                                    }

                                    if(matchCounter > maxMatches)
                                    {
                                        break;
                                    }
                                }

                                if (tmpSwapList.Count > 0)
                                {
                                    //append old List
                                    origFeed.Items.AddRange(tmpSwapList);

                                    //Debug.WriteLine("'" + group.FeedList[i].Title + "': Added: " + tmpSwapList.Count().ToString() + "  new items to feed-list.");

                                    //order list again
                                    origFeed.Items = origFeed.Items.OrderByDescending(o => o.PublishingDate).ToList();
                                }

                                updateSuccess = true;

                            }
                            else
                            {
                                if (itemCountNew == itemCountOld)
                                {
                                    updateSuccess = true;
                                    Debug.WriteLine("no updates found for '" + origFeed.Title + "'. Old: " + itemCountOld.ToString() + "   new: " + itemCountNew.ToString());
                                }
                            }


                        }
                    }

                }

                //mark Feed as updated or not
                origFeed.Updated = updateSuccess;

             }
            catch (Exception ex)
            {
                Debug.WriteLine("Error when getting update for feed '" + origFeed + "': " + ex.Message);
            }
        }

        
        /// <summary>
        /// mark all feeds in feedgroup (and sub-groups) as read
        /// </summary>
        /// <param name="group"></param>
        private void MarkGroupAsRead(FeedGroup group)
        {
            if(group != null)
            {
                if(group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count() > 0)
                    {
                        foreach(FeedGroup subgroup in group.FeedGroups)
                        {
                            MarkGroupAsRead(subgroup);
                        }
                    }
                }

                if(group.FeedList != null)
                {
                    if(group.FeedList.Count() > 0)
                    {
                        foreach(Feed feed in group.FeedList)
                        {
                            MarkFeedAsRead(feed, true);
                        }
                    }
                }
                Debug.WriteLine("All feeditems marked as read.");
            }
        }

        /// <summary>
        /// mark a single feed as read
        /// </summary>
        /// <param name="feed">the feed that has to get marked</param>
        /// <param name="read">true = feed-items are read, false = feed-items are unread</param>
        private void MarkFeedAsRead(Feed feed, bool read)
        {
            if (feed.Items != null)
            {
                if (feed.Items.Count() > 0)
                {
                    foreach (FeedItem item in feed.Items)
                    {
                        item.Read = read;
                    }
                }
            }
        }

        /// <summary>
        /// get a "list" of all feeds in a group (each url on a new line)
        /// </summary>
        /// <param name="group"></param>
        /// <param name="result">each url on a new line</param>
        private void GetFeedsFromGroup(FeedGroup group, ref string result)
        {
            if (group != null)
            {
                if (group.FeedGroups != null)
                {
                    if (group.FeedGroups.Count() > 0)
                    {
                        foreach (FeedGroup subgroup in group.FeedGroups)
                        {
                            GetFeedsFromGroup(subgroup, ref result);
                        }
                    }
                }

                if (group.FeedList != null)
                {
                    if (group.FeedList.Count() > 0)
                    {
                        foreach (Feed feed in group.FeedList)
                        {
                            result += feed.FeedURL + Environment.NewLine;
                        }
                    }
                }
            }
        }


        /// <summary>
        /// open unread feeds in external browser (by choice only this group's feeds)
        /// </summary>
        /// <param name="group"></param>
        /// <param name="thisGroupOnly"></param>
        private void OpenUnreadFeeds(FeedGroup group, bool thisGroupOnly)
        {
            if (group != null)
            {
                if (group.FeedGroups != null && !thisGroupOnly)
                {
                    if (group.FeedGroups.Count() > 0)
                    {
                        foreach (FeedGroup subgroup in group.FeedGroups)
                        {
                            OpenUnreadFeeds(subgroup, thisGroupOnly);
                        }
                    }
                }

                if (group.FeedList != null)
                {
                    if (group.FeedList.Count() > 0)
                    {
                        foreach (Feed feed in group.FeedList)
                        {
                            if (feed.Items != null)
                            {
                                if (feed.Items.Count() > 0)
                                {
                                    foreach (FeedItem item in feed.Items)
                                    {
                                        if(item.Read == false)
                                        {
                                            System.Diagnostics.Process.Start(item.Link);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }


        

        

        #endregion


        #region public utility-functions

        /// <summary>
        /// checks if a internet-connection is available
        /// </summary>
        /// <returns></returns>
        public bool CheckInternetConnectivity()
        {
            if (internetCheck == null)
                internetCheck = new InternetCheck();

            return internetCheck.ConnectedToInternet();
        }

        public bool IsValidURL(string url)
        {
            Uri uriResult;
            bool result = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            return result;
        }

        /// <summary>
        /// checks a url for feeds. returns null if none could be found
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public List<string> CheckUrlForFeeds(string url)
        {
            List<string> result = null;

            Uri uriResult;
            bool validURL = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url) && validURL)
            {
                bool foundInWebDefs = false;

                foreach(WebPageFeedDef webPageDef in webPageFeedDefList.Definitions)
                {
                    if(url.ToLower().Contains(webPageDef.KeyID))
                    {
                        result = new List<string>();
                        result.Add(url);
                        foundInWebDefs = true;
                    }
                }

                if (!foundInWebDefs)
                {
                    var urls = FeedReader.GetFeedUrlsFromUrl(url);

                    if (urls.Count() < 1) // no url - probably the url is already the right feed url
                    {

                        //try to get the actual Feed-Items from the url
                        try
                        {
                            var feed = FeedReader.Read(url);

                            //if successful: add to list
                            result = new List<string>();
                            result.Add(url);

                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine("Controller.CheckUrlForFeeds: Couldn't get feed from '" + url + "'. Errormessage: " + ex.Message);
                        }
                    }
                    else
                    {
                        result = new List<string>();

                        //add each found feed to list
                        foreach (HtmlFeedLink feedLink in urls)
                        {
                            result.Add(feedLink.Url);
                        }
                    }
                }
                
            }


            return result;
        }


        public string GetFaviconOfWebpage(string url)
        {
            string result = "";

            bool loadSuccess = false;

            string content = "";

            try
            {
                //try to download the main page
                content = new System.Net.WebClient().DownloadString(url);
                loadSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while getting comic-webpage: " + Environment.NewLine + ex.Message);
            }

            if (loadSuccess)
            {
                result = GetFaviconUrl(content,url);
            }

            return result;
        }

        public string GetFaviconUrl(string htmlText, string url)
        {
            string result = "";
            bool imageFound = false;

            List<string> regexList = new List<string>();

            regexList.Add("<link.*?href=\"(.*?.ico)\"");
            regexList.Add("href=\"(.*?.ico)\"");
            regexList.Add("icon\".*?href=\"(.*?.pnj)\"");    //tumblr
            regexList.Add("icon\".*?href=\"(.*?.png)\"");
            regexList.Add("<link.*?href='(.*?.ico)'");      //blogger

            foreach (string regEx in regexList)
            {
                MatchCollection mCollection = Regex.Matches(htmlText, regEx);

                if (mCollection != null)
                {
                    //Debug.WriteLine("Find favicon: " + mCollection.Count.ToString() + " matches found.");

                    if (mCollection.Count > 0)
                    {
                        result = mCollection[0].Groups[1].Value;

                        if(result.ToLower().Contains("href="))
                        {
                            result = result.Remove(0, result.LastIndexOf("href=\"") + 5);
                        }
                        if(result.Contains("\""))
                        {
                            result = result.Replace("\"", "");
                        }
                        
                        
                        if (!result.Contains("http:") && !result.Contains("https:"))
                        {
                            if(result.ToLower().Contains(".com"))
                            {
                                result = "http:" + result;

                            }
                            else
                            {
                                Uri uriAddress = new Uri(url);

                                result = uriAddress.GetLeftPart(UriPartial.Path) + result;
                            }
                        }

                        //Debug.WriteLine("Found favicon: " + result);
                        imageFound = true;
                        break;
                    }
                }
            }

            if(imageFound == false)
            {
                Debug.WriteLine("No image found for: " + url);
            }

            

            return result;
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
                Debug.WriteLine("There was a problem downloading the image from '" + url + "': " + ex.Message);
            }
        }
        #endregion


        #region opml import and export


        /// <summary>
        /// generates OPML-Document from mainModel
        /// </summary>
        /// <returns></returns>
        private OPML GetOPMLFromModel()
        {
            OPML result = new OPML();

            Head newHead = new Head();
            newHead.DateCreated = DateTime.Now;
            newHead.DateModified = DateTime.Now;
            newHead.Docs = "";
            newHead.OwnerEmail = "";
            newHead.OwnerName = "FeedRead";
            newHead.Title = "FeedRead Subscription-List";

            Body newBody = new Body();
            newBody.Outlines = GetOutLinesFromModel();

            result.Body = newBody;

            result.Head = newHead;


            return result;
        }

        /// <summary>
        /// generate list of outlines from mainModel
        /// </summary>
        /// <returns></returns>
        private List<Outline> GetOutLinesFromModel()
        {
            List<Outline> outlines = new List<Outline>();

            if (mainModel != null)
            {
                //create outline for current group

                if (mainModel.FeedGroups != null)
                {
                    if (mainModel.FeedGroups.Count > 0)
                    {
                        foreach (FeedGroup subgroup in mainModel.FeedGroups)
                        {
                            outlines.AddRange(GetOutLinesFromFeedGroup(subgroup));
                        }
                    }
                }

                if (mainModel.FeedList != null)
                {
                    if (mainModel.FeedList.Count > 0)
                    {
                        foreach (Feed feed in mainModel.FeedList)
                        {
                            Outline feedOutline = GetOutLinesFromFeed(feed);
                            if (feedOutline != null)
                            {
                                outlines.Add(feedOutline);
                            }
                        }
                    }
                }

            }

            return outlines;
        }

        /// <summary>
        /// generate List of outlines from a specific feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private List<Outline> GetOutLinesFromFeedGroup(FeedGroup group)
        {
            List<Outline> outlines = new List<Outline>();

            if (group != null)
            {
                //create outline for current group
                Outline groupOutline = new Outline();

                groupOutline.HTMLUrl = group.Url;
                groupOutline.Text = group.Title;
                groupOutline.Title = group.Title;

                groupOutline.Outlines = new List<Outline>();

                if (group.FeedGroups != null)
                {
                    if (group.FeedGroups.Count > 0)
                    {
                        foreach (FeedGroup subgroup in group.FeedGroups)
                        {
                            groupOutline.Outlines.AddRange(GetOutLinesFromFeedGroup(subgroup));
                        }
                    }
                }

                if (group.FeedList != null)
                {
                    if (group.FeedList.Count > 0)
                    {
                        foreach (Feed feed in group.FeedList)
                        {
                            Outline feedOutline = GetOutLinesFromFeed(feed);
                            if (feedOutline != null)
                            {
                                groupOutline.Outlines.Add(feedOutline);
                            }
                        }
                    }
                }

                outlines.Add(groupOutline);
            }

            return outlines;
        }

        /// <summary>
        /// generate Outline for a specific feed
        /// </summary>
        /// <param name="feed"></param>
        /// <returns></returns>
        private Outline GetOutLinesFromFeed(Feed feed)
        {
            Outline fOutline = null;

            if (feed != null)
            {
                fOutline = new Outline();
                fOutline.Description = feed.Description;
                fOutline.HTMLUrl = feed.FeedURL;
                fOutline.Language = feed.Language;
                fOutline.Outlines = new List<Outline>();
                fOutline.Text = feed.Title;
                fOutline.Title = feed.Title;
                fOutline.Type = feed.Type.ToString();
                fOutline.Version = feed.Type.ToString();
                fOutline.XMLUrl = feed.Link;
            }

            return fOutline;
        }


        #endregion



        #region treeView-functions

        /// <summary>
        /// get the full List of Treeview-Nodes from the main-Model (plus an imageList for the node-icons)
        /// </summary>
        /// <param name="mainNodes"></param>
        /// <param name="imageList"></param>
        public void GetTreeNodes(ref List<TreeNode> mainNodes, ref ImageList imageList)
        {
            //list of all the primary nodes of the treeview
            mainNodes = new List<TreeNode>();
            imageList = null;

            bool internetOK = internetCheck.ConnectedToInternet();

            //to display icons the setting has to be true and a internet-connection has to be available
            bool displayIcons = Properties.Settings.Default.displayFeedIcons;// && internetOK;

            
            if (mainModel != null)
            {
                //list of icons for the feed-nodes
                //ImageList imageList = null;
                if (displayIcons)
                {
                    imageList = new ImageList();
                    imageList.Images.Add(Properties.Resources.defaultTreeNodeIcon);

                }

                

                //add feedgroup-Nodes to the list
                if (mainModel.FeedGroups != null)
                {
                    if (mainModel.FeedGroups.Count() > 0)
                    {
                        for (int i = 0; i < mainModel.FeedGroups.Count(); i++)
                        {
                            TreeNode tmpNode = GetNode(mainModel.FeedGroups[i], ref imageList, displayIcons);
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
                            mainNodes.Add(GetFeedNode(mainModel.FeedList[i], ref imageList, displayIcons));
                        }
                    }
                }


                

                //Debug.WriteLine("Length of imagelist for treeview after update: " + icons.Images.Count);
            }
            else
            {
                Debug.WriteLine("get-TreeNodes: mainModel is null");
            }

        }


        /// <summary>
        /// method for (recursively) getting the TreeNode (and subnodes) of a feedgroup
        /// </summary>
        /// <param name="group"></param>
        /// <returns></returns>
        private TreeNode GetNode(FeedGroup group, ref ImageList icons, bool getIcons)
        {
            TreeNode groupNode = null;

            if (group != null)
            {
                groupNode = new TreeNode(group.GetNodeText());
                groupNode.Tag = group;

                //if wanted, try to get image
                if (getIcons)
                {
                    string imagePath = group.ImagePath;

                    try
                    {
                        if (!string.IsNullOrEmpty(imagePath) && !string.IsNullOrWhiteSpace(imagePath))
                        {

                            Image groupIcon = null;

                            if (File.Exists(imagePath))
                            {
                                //load image from file
                                groupIcon = Image.FromFile(imagePath);
                            }
                            
                            if (groupIcon != null)
                            {
                                icons.Images.Add(groupIcon);

                                groupNode.ImageIndex = icons.Images.Count - 1;
                                groupNode.SelectedImageIndex = icons.Images.Count - 1;
                            }
                            
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Error while trying to get the image for feed '" + group.Title + "'. Image-URL = " + imagePath + "   Error: " + ex.Message);
                    }
                }

                //change node-color depending upon porperty(-ies) of the FeedGroup
                if (group.GetNoOfUnreadFeedItems() > 0)
                {
                    groupNode.ForeColor = Color.Blue;
                }

                //check for sub-Feedgroups and add them to this group's node
                if (group.FeedGroups != null)
                {
                    if (group.FeedGroups.Count > 0)
                    {
                        for (int i = 0; i < group.FeedGroups.Count(); i++)
                        {
                            TreeNode subNode = GetNode(group.FeedGroups[i], ref icons, getIcons);
                            if (subNode != null)
                            {
                                groupNode.Nodes.Add(subNode);
                            }
                        }
                    }
                }

                //check if there are feeds in this group
                if (group.FeedList != null)
                {
                    if (group.FeedList.Count() > 0)
                    {
                        //add feeds to this group's node
                        for (int i = 0; i < group.FeedList.Count(); i++)
                        {
                            groupNode.Nodes.Add(GetFeedNode(group.FeedList[i], ref icons, getIcons));
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
        private TreeNode GetFeedNode(Feed feed, ref ImageList icons, bool getIcon)
        {
            TreeNode feedNode = new TreeNode(feed.GetNodeText());
            feedNode.Tag = feed;

            //change text-color depending on various properties of the feed
            if (!feed.Updated)
            {
                feedNode.ForeColor = Color.Red;
            }
            else if (feed.GetNoOfUnreadItems() > 0)
            {
                feedNode.ForeColor = Color.Blue;
            }



            //if wanted, try to get image
            if (getIcon)
            {
                string imageUrl = feed.ImageUrl;

                try
                {
                    if (!string.IsNullOrEmpty(imageUrl) && !string.IsNullOrWhiteSpace(imageUrl))
                    {

                        Image feedIcon = null;

                        if(File.Exists(imageUrl))
                        {
                            //load image from file
                            feedIcon = Image.FromFile(imageUrl);
                        }
                        else
                        {
                            //get image from webpage and save it to file
                            GetImageFromURL(imageUrl, ref feedIcon);

                            if(feedIcon != null)
                            {
                                string newFileName = Properties.Settings.Default.iconFolderPath + Path.DirectorySeparatorChar + feed.Title.Replace(" ", "_") + imageUrl.Remove(0, imageUrl.LastIndexOf("."));

                                feedIcon.Save(newFileName);
                                
                                feed.ImageUrl = newFileName;
                            }
                        }

                        


                        if (feedIcon != null)
                        {
                            icons.Images.Add(feedIcon);

                            feedNode.ImageIndex = icons.Images.Count - 1;
                            feedNode.SelectedImageIndex = icons.Images.Count - 1;
                        }
                        else
                        {
                            if (getIcon)
                            {
                                Debug.WriteLine("Image for " + feed.Title + "'. Image-URL = " + imageUrl + " is null.");
                            }

                        }

                    }
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Error while trying to get the image for feed '" + feed.Title + "'. Image-URL = " + imageUrl + "   Error: " + ex.Message);
                }
            }
            return feedNode;
        }

        
        #endregion
    }


}
