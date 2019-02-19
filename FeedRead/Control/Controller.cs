﻿using CodeHollow.FeedReader;
using FeedRead.Model;
using FeedRead.UI;
using FeedRead.Utilities.OPML;
//using FeedLister;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.Xml.Serialization;

namespace FeedRead
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
                        var feeds = System.IO.File.ReadAllLines(odi.FileName);
                        Parallel.ForEach<string>(feeds, x =>
                        {
                            try
                            {
                                Do(x);

                            }
                            catch { }
                        });
                        
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
            sadi.Title = "Export opml-File";
            sadi.RestoreDirectory = true;
            sadi.Filter = "ompl-File|*.opml";

            if (sadi.ShowDialog() == DialogResult.OK)
            {
                if(opmlDoc != null)
                {
                    StreamWriter writer = new StreamWriter(sadi.FileName);
                    writer.Write(opmlDoc.ToString());
                    writer.Close();
                    writer.Dispose();

                    MessageBox.Show("Successfully exported opml-file to: " + sadi.FileName);
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

                    //check if it's anew group
                    if(sGD.addNewGroupName)
                    {
                        //create a new group and add the feed to it
                        //Console.WriteLine("Controller.AddNewFeed: add feed '" + newFeedUrl + "' to new group '" + groupName + "'.");
                        Feed newFeed = FeedReader.Read(newFeedUrl);
                        mainModel.AddFeedAndGroup(newFeed, groupName, "");

                        UpdateTreeview();
                    }
                    else
                    {
                        //find selected group
                        //check if feed already exists and if not, add the new feed to it
                        //Console.WriteLine("Controller.AddNewFeed: add feed '" + newFeedUrl + "' to existing group '" + groupName + "'");
                        Feed newFeed = FeedReader.Read(newFeedUrl);
                        mainModel.AddFeed(newFeed, groupName);

                        UpdateTreeview();
                    }
                   
                }
            }
        }

        /// <summary>
        /// update feed-list / get new feeditems
        /// </summary>
        public void UpdateFeeds()
        {
            UpdateFeed();
            UpdateTreeview();
        }

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

        //checks a url for feeds. returns null if none could be found
        public List<string> CheckUrlForFeeds(string url)
        {
            List<string> result = null;

            Uri uriResult;
            bool validURL = Uri.TryCreate(url, UriKind.Absolute, out uriResult) && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);

            if (!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url) && validURL)
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
                    catch(Exception ex)
                    {
                        Console.WriteLine("Controller.CheckUrlForFeeds: Couldn't get feed from '" + url + "'. Errormessage: " + ex.Message);
                    }
                }
                else
                {
                    result = new List<string>();

                    //add each found feed to list
                    foreach(HtmlFeedLink feedLink in urls)
                    {
                        result.Add(feedLink.Url);
                    }
                }

            }


            return result;
        }

        static void Do(string url)
        {
            Console.WriteLine("trying to get feed from: " + url);

            var linksTask = FeedReader.GetFeedUrlsFromUrlAsync(url);
            linksTask.ConfigureAwait(false);

            foreach (var link in linksTask.Result)
            {
                try
                {
                    string title = link.Title;
                    if (string.IsNullOrEmpty(title))
                    {
                        title = url.Replace("https", "").Replace("http", "").Replace("www.", "");

                    }
                    title = Regex.Replace(title.ToLower(), "[^a-z]*", "");
                    var curl = FeedReader.GetAbsoluteFeedUrl(url, link);

                    var contentTask = Helpers.DownloadAsync(curl.Url);
                    contentTask.ConfigureAwait(false);

                    //System.IO.File.WriteAllText("d:\\feeds\\" + title + "_" + Guid.NewGuid().ToString() + ".xml", contentTask.Result);
                    Console.Write("+");
                }
                catch (Exception ex)
                {
                    Console.WriteLine(link.Title + " - " + link.Url + ": " + ex.ToString());
                }
            }
        }


        /// <summary>
        /// update each feed of the main model
        /// </summary>
        private void UpdateFeed()
        {
            if(mainModel != null)
            {
                UpdateFeedList(mainModel);
            }
        }


        private void UpdateFeedList(FeedGroup group)
        {
            if(group != null)
            {
                if(group.FeedGroups != null)
                {
                    if(group.FeedGroups.Count() > 0 )
                    {
                        foreach(FeedGroup subGroup in group.FeedGroups)
                        {
                            UpdateFeedList(subGroup);
                        }
                    }
                }

                if(group.FeedList != null)
                {
                    if(group.FeedList.Count > 0)
                    {

                    }
                }
            }
        }
        #endregion
    }
}
