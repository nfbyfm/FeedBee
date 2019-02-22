using CodeHollow.FeedReader;
using FeedRead.Model;
using FeedRead.UI;
using FeedRead.Utilities.OPML;
//using FeedLister;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FeedRead.Control
{
    
    /// <summary>
    /// main Class for controlling everything
    /// </summary>
    public class Controller
    {
        private OPML opmlDoc;       //only for testing (import / export)
        private MainForm mainForm;
        private FeedGroup mainModel;

        public const string mainModelID = "mainModel";
        private const string youtubeID = "Youtube";

        public Controller(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.mainModel = new FeedGroup(mainModelID, ""); 

            if(Properties.Settings.Default.bLoadUponStartup)
            {
                OpenListFromXML(Properties.Settings.Default.loadListPath);
                UpdateTreeview();
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
            odi.Title = "Import opml-file";
            odi.RestoreDirectory = true;
            odi.Multiselect = false;
            odi.Filter = "ompl-file|*.opml|txt-file|*.txt";

            if(odi.ShowDialog() == DialogResult.OK)
            {
                switch(odi.FilterIndex)
                {
                    case 1:
                        //open opml-file
                        //Code from http://www.gutgames.com/post/OPML-in-CASPNet.aspx
                        //doesn't seem to be able to handle nestes outlines well

                        try
                        {
                            opmlDoc = new OPML(odi.FileName);

                            if (opmlDoc != null)
                            {
                                if (opmlDoc.Body != null)
                                {
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
                        break;
                    case 2:
                        //open txt-file
                        ImportFromTxt(odi.FileName);
                        break;
                    default:
                        //do nothing
                        break;
                }
                /*
                try
                {
                    var opmlData = XDocument.Load(odi.FileName, LoadOptions.SetLineInfo);

                    OpmlDocument opmlDocument = OpmlDocument.Create(opmlData, false);

                    
                    if (opmlDocument != null)
                    {
                        foreach (Outline oline in opmlDocument.Body.Outlines)
                        {
                            if(oline.IsBreakPoint)
                            {
                                Console.WriteLine(oline.Title);
                            }
                            else
                            {
                                foreach(Outline o2line in oline.Outlines)
                                {
                                    Console.WriteLine(oline.Title + " -> " + o2line.Title);
                                }
                            }

                        }
                    }
                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error while importing opml-file. Error: " + ex.Message);
                }
                */

                
                

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
                        if (opmlDoc != null)
                        {
                            StreamWriter writer = new StreamWriter(sadi.FileName);
                            writer.Write(opmlDoc.ToString());
                            writer.Close();
                            writer.Dispose();

                            MessageBox.Show("Successfully exported opml-file to: " + sadi.FileName);
                        }
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
            AddFeedDialog addFeedDialog = new AddFeedDialog(this);
            if(addFeedDialog.ShowDialog() == DialogResult.OK)
            {
                //show next dialog (add Feed to a Group)
                string newFeedUrl = addFeedDialog.feedUrl;

                //Console.WriteLine("Controller.AddNewFeed: got new feed-source from user: " + newFeedUrl);

                //show group-Dialog
                SelectGroupDialog sGD = new SelectGroupDialog(GetGroupNames());

                if(sGD.ShowDialog() == DialogResult.OK)
                {
                    //get group-name
                    string groupName = sGD.groupName;

                    Feed newFeed = null;

                    GetFeed(newFeedUrl, ref newFeed);

                    if(newFeed != null)
                    {
                        //check if it's anew group
                        if (sGD.addNewGroupName)
                        {
                            //create a new group and add the feed to it
                            //Console.WriteLine("Controller.AddNewFeed: add feed '" + newFeedUrl + "' to new group '" + groupName + "'.");

                            mainModel.AddFeedAndGroup(newFeed, groupName, "");

                            UpdateTreeview();
                        }
                        else
                        {
                            //find selected group
                            //check if feed already exists and if not, add the new feed to it
                            //Console.WriteLine("Controller.AddNewFeed: add feed '" + newFeedUrl + "' to existing group '" + groupName + "'");
                            mainModel.AddFeed(newFeed, groupName);

                            UpdateTreeview();
                        }
                    }
                    
                   
                }
            }
        }


        public delegate void UpdateTreeViewCallback(FeedGroup mainModel);

        /// <summary>
        /// update feed-list / get new feeditems in separate thread
        /// </summary>
        public void UpdateFeeds()
        {
            
            Thread t2 = new Thread(delegate ()
            {
                mainForm.SetStatusText("updating feeds ...", -1);
                UpdateFeed(!Properties.Settings.Default.updateNSFW);
                mainForm.Invoke(new UpdateTreeViewCallback(mainForm.UpdateTreeView), mainModel);
                mainForm.SetStatusText("feeds updated", 2000);
            });
            t2.Start();
            
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

        public void ShowSettings()
        {
            SettingsDialog sedi = new SettingsDialog();
            if(sedi.ShowDialog() == DialogResult.OK)
            {
                //save settings
                Properties.Settings.Default.Save();
            }
        }

        public void CloseApplication()
        {
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
            mainForm.UpdateTreeView(mainModel);
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



        #region Feed-related functions
        //get List of all the group-names
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

        private void OpenListFromXML(string filename)
        {
            try
            {
                StreamReader reader = new StreamReader(filename);

                XmlSerializer ser = new XmlSerializer(typeof(FeedGroup));
                mainModel = (FeedGroup)ser.Deserialize(reader);

                reader.Close();
                reader.Dispose();

                Console.WriteLine("List of feed groups opened. Filename: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading FeedList:" + Environment.NewLine + ex.Message);
            }
        }

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

                    if (!sGD.addNewGroupName)
                    {
                        groupAdded = true;
                    }

                    //start own new thread for importing
                    Thread t2 = new Thread(delegate ()
                    {
                        mainForm.SetStatusText("importing feeds ...", -1);

                        ImportFromTxtSubFunction(filename, ref groupAdded, groupName);

                        mainForm.Invoke(new UpdateTreeViewCallback(mainForm.UpdateTreeView), mainModel);
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

        private void ImportFromTxtSubFunction(string filename, ref bool groupAdded, string groupName)
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

                        GetFeed(line, ref newFeed);

                        if (newFeed != null)
                        {
                            if (groupAdded == false)
                            {
                                //add new group and the the feed to it
                                mainModel.AddFeedAndGroup(newFeed, groupName, "");
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
        private void GetFeed(string url, ref Feed newFeed)
        {
            
            if (url.ToLower().Contains("mangarock"))
            {
                newFeed = new Feed();
                ComicFeedReader reader = new ComicFeedReader();
                reader.Read(url, ref newFeed);

                if (newFeed == null)
                {
                    Console.WriteLine("Error while getting feed from '" + url + "' to list.");
                    return;
                }
            }
            else
            {
                newFeed = FeedReader.Read(url);
                newFeed.FeedURL = url;
            }
        }

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

                    Console.WriteLine("Saved current list of feed-groups to: " + filename);
                }
                else
                {
                    Console.WriteLine("Controller.SaveList: main Model is null. Can't save.");
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
                if(url.ToLower().Contains("mangarock"))
                {
                    result = new List<string>();
                    result.Add(url);
                }
                else
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
                            Console.WriteLine("Controller.CheckUrlForFeeds: Couldn't get feed from '" + url + "'. Errormessage: " + ex.Message);
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
        /// get update for a specific feed
        /// </summary>
        /// <param name="group"></param>
        /// <param name="atWork"></param>
        private void UpdateFeedList(FeedGroup group, bool atWork)
        {
            if(group != null)
            {
                if(group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count() > 0 )
                    {
                        foreach(FeedGroup subGroup in group.FeedGroups)
                        {
                            UpdateFeedList(subGroup, atWork);
                        }
                    }
                }

                if(group.FeedList != null)
                {
                    bool checkNSFW = true;

                    //if "atWork" = true -> check if group has nsfw in the title -> ignore / don't update if so
                    if(atWork)
                    {
                        checkNSFW = !group.Title.ToLower().Contains("nsfw");
                    }


                    

                    if(group.FeedList.Count > 0 && checkNSFW)
                    {
                        //create a list of threads
                        List<Thread> tList = new List<Thread>();

                        for (int i =0; i< group.FeedList.Count(); i++)
                        {
                            Feed fed = group.FeedList[i];
                            //start own new thread for importing
                            Thread t1 = new Thread(delegate ()
                            {
                                
                                UpdateFeed(fed);
                            });
                            
                            //add thread to list
                            tList.Add(t1);
                        }

                        int numberOfThreads = tList.Count();
                        Console.WriteLine("new List of threads created. Number of threads in list: " + numberOfThreads);

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
                                mainForm.SetStatusText("Updating " + counter.ToString() + " feeds out of " + numberOfThreads + " ...", -1);
                                //Console.WriteLine(counter.ToString() + " threads out of " + numberOfThreads + " are done.");
                                counter_before = counter;
                            }

                            if (counter == numberOfThreads)
                            {
                                updateFinished = true;
                            }
                        }
                    }
                    

                    //Console.WriteLine("update finished");
                    //MessageBox.Show("Update finished");
                }
            }
        }

        

        /// <summary>
        /// update a single feed
        /// </summary>
        /// <param name="origFeed"></param>
        private void UpdateFeed(Feed origFeed)
        {

            //Console.WriteLine("Starting update on '" + origFeed.Title + "' ...");

            //get Feeditems
            Feed tmpFeed = null;

            bool updateSuccess = false;

            string url = origFeed.FeedURL;


            //try to get current feed
            GetFeed(url, ref tmpFeed);

            if (tmpFeed != null)
            {
                if (tmpFeed.Items != null)
                {
                    if (tmpFeed.Items.Count() > 0)
                    {
                        List<FeedItem> updateList = tmpFeed.Items.OrderByDescending(o => o.PublishingDate).ToList();



                        origFeed.Items = origFeed.Items.OrderByDescending(o => o.PublishingDate).ToList();

                        int itemCountNew = updateList.Count();
                        int itemCountOld = origFeed.Items.Count();

                        if (itemCountNew > 0) //if ((itemCountNew != itemCountOld) && (itemCountNew > 0))
                        {
                            //Console.WriteLine("'" + group.FeedList[i].Title + "': New: " + itemCountNew.ToString() + "  old: " + itemCountOld.ToString());

                            List<FeedItem> tmpSwapList = new List<FeedItem>();

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
                                        break;
                                    }
                                }

                                //if item couldn't be found in list -> add to temp. List
                                if (found == false)
                                {
                                    tmpSwapList.Add(newItem);
                                }
                            }

                            if (tmpSwapList.Count > 0)
                            {
                                //append old List
                                origFeed.Items.AddRange(tmpSwapList);

                                //Console.WriteLine("'" + group.FeedList[i].Title + "': Added: " + tmpSwapList.Count().ToString() + "  new items to feed-list.");

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
                                Console.WriteLine("no updates found for '" + origFeed.Title + "'. Old: " + itemCountOld.ToString() + "   new: " + itemCountNew.ToString());
                            }
                        }


                    }
                }

            }

            //mark Feed as updated or not
            origFeed.Updated = updateSuccess;

            //Console.WriteLine("Update of '" + origFeed.Title + "' is done.");
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
                            if(feed.Items != null)
                            {
                                if(feed.Items.Count() > 0)
                                {
                                    foreach(FeedItem item in feed.Items)
                                    {
                                        item.Read = true;
                                    }
                                }
                            }
                        }
                    }
                }
                Console.WriteLine("All feeditems marked as read.");
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

        #endregion




        
    }

    
}
