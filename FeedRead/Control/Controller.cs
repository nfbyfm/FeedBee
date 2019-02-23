﻿using CodeHollow.FeedReader;
using FeedRead.Model;
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

        private InternetCheck internetCheck;
        
        public Controller(MainForm mainForm, InternetCheck iCheck)
        {
            this.mainForm = mainForm;
            this.mainModel = new FeedGroup(mainModelID, "");
            this.internetCheck = iCheck;


            //load default-List upon startup
            if(Properties.Settings.Default.bLoadUponStartup)
            {
                try
                {
                    OpenListFromXML(Properties.Settings.Default.loadListPath);
                    

                }
                catch(Exception ex)
                {
                    Console.WriteLine("Error while loading list: " + ex.Message);
                }

                try
                {
                    UpdateTreeview();


                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error while updating treeview: " + ex.Message);
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
            odi.Title = "Import opml-file";
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
            if(CheckInternetConnectivity())
            {
                AddFeedDialog addFeedDialog = new AddFeedDialog(this);
                if (addFeedDialog.ShowDialog() == DialogResult.OK)
                {
                    //show next dialog (add Feed to a Group)
                    string newFeedUrl = addFeedDialog.feedUrl;

                    //Console.WriteLine("Controller.AddNewFeed: got new feed-source from user: " + newFeedUrl);

                    //show group-Dialog
                    SelectGroupDialog sGD = new SelectGroupDialog(GetGroupNames());

                    if (sGD.ShowDialog() == DialogResult.OK)
                    {
                        //get group-name
                        string groupName = sGD.groupName;

                        Feed newFeed = null;

                        GetFeed(newFeedUrl, ref newFeed);

                        if (newFeed != null)
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
            else
            {
                MessageBox.Show("No internet-connection could be detected. Can't add a new feed.", "Add new feed", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }


        public delegate void UpdateTreeViewCallback(FeedGroup mainModel);

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
                    mainForm.Invoke(new UpdateTreeViewCallback(mainForm.UpdateTreeViewUnlock), mainModel);
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

                        Console.WriteLine("RenameNode: Changeing Name of FeedGroup '" + selFeedGroup.Title + "' to: " + newName);

                        selFeedGroup.Title = newName;
                        UpdateTreeview();
                    }
                    else if (tmpResult.GetType() == typeof(Feed))
                    {
                        Feed selFeed = (Feed)tmpResult;

                        Console.WriteLine("RenameNode: Changeing Name of Feed '" + selFeed.Title + "' to: " + newName);

                        selFeed.Title = newName;
                        UpdateTreeview();
                    }
                    else
                    {
                        Console.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                    }
                }
                else
                {
                    Console.WriteLine("Couldn't find a Feed or FeedGroup in the current mainModel with the Hash-Code of the selected TreeNode-Tag-Object. HashCode: " + tagObject.GetHashCode().ToString());
                }

                */
        /// <summary>
        /// rename a Node (Group or Feed)
        /// </summary>
        /// <param name="tagObject"></param>
        /// <param name="newName"></param>
        public void RenameNode(object tagObject, string newName)
        {
            if(tagObject != null)
            {
                if (tagObject.GetType() == typeof(FeedGroup))
                {
                    FeedGroup selFeedGroup = (FeedGroup)tagObject;

                    selFeedGroup.Title = newName;
                    UpdateTreeview();
                }
                else if (tagObject.GetType() == typeof(Feed))
                {
                    Feed selFeed = (Feed)tagObject;

                    selFeed.Title = newName;
                    UpdateTreeview();
                }
                else
                {
                    Console.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                }

            }
            else
            {
                Console.WriteLine("RenameNode: tagObject of selected Treenode is null.");
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
                    Console.WriteLine("DeleteNode: couldn't find element of selected Treenode is null.");
                }
            }
            else
            {
                Console.WriteLine("DeleteNode: tagObject of selected Treenode is null.");
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
                        Console.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                    }

                }
                else
                {
                    Console.WriteLine("RenameNode: tagObject of selected Treenode is null.");
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
                    Console.WriteLine("Error while casting the found Element with the same Hash-Code of the treenode-tag-object. Unknown type: " + tagObject.GetType().ToString());
                }

            }
            else
            {
                Console.WriteLine("RenameNode: tagObject of selected Treenode is null.");
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

                Console.WriteLine("List of feed groups opened. Filename: " + filename);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error while loading FeedList:" + Environment.NewLine + ex.Message);
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

                    if (!sGD.addNewGroupName)
                    {
                        groupAdded = true;
                    }

                    mainForm.EnableFeedFunctionalities(false);

                    //start own new thread for importing
                    Thread t2 = new Thread(delegate ()
                    {
                        mainForm.SetStatusText("importing feeds ...", -1);

                        ImportFromTxtSubFunction(filename, ref groupAdded, groupName);

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
                        //Console.WriteLine("new List of threads created. Number of threads in list: " + numberOfThreads);

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
                            MarkFeedAsRead(feed, true);
                        }
                    }
                }
                Console.WriteLine("All feeditems marked as read.");
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
                if (url.ToLower().Contains("mangarock"))
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

        #endregion

    }


}
