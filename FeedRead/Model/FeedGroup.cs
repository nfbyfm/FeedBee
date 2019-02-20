using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedRead.Model
{
    /// <summary>
    /// Class 
    /// </summary>
    public class FeedGroup
    {
        #region Properties

        /// <summary>
        /// Name/ Title of the Group
        /// </summary>
        [XmlElement("Title")]
        public string Title { get; set; }

        /// <summary>
        /// Url the Group (if any is available)
        /// </summary>
        [XmlElement("URL")]
        public string Url { get; set; }

        /// <summary>
        /// List of ancillary FeedGroups
        /// </summary>
        [XmlArray("GroupList"), XmlArrayItem("Group")]
        public List<FeedGroup> FeedGroups { get; set; }


        /// <summary>
        /// List of Feeds
        /// </summary>
        [XmlArray("FeedList"), XmlArrayItem("Feed")]
        public List<Feed> FeedList { get; set; }
        
                

        #endregion

        #region Constructors
        /// <summary>
        /// default Constructor (needed for xml-serialization)
        /// </summary>
        public FeedGroup() { }

        /// <summary>
        /// Constructor (needed for xml-serialization)
        /// </summary>
        /// /// <param name="title">Name / shown title of the group.</param>
        public FeedGroup(string title, string url ="")
        {
            this.Title = title;
            this.Url = url;
            FeedGroups = new List<FeedGroup>();
            FeedList = new List<Feed>();
        }

        #endregion

        /// <summary>
        /// tells whether sub-Groups exist or not
        /// </summary>
        public bool IsLeaf()
        {
            bool result = true;

            if (FeedGroups != null)
            {
                if(FeedGroups.Count() > 0)
                {
                    result = false;
                }
            }
            return result;
        }

        
        /// <summary>
        /// method for adding a new feed to the list
        /// </summary>
        /// <param name="newFeed">the Feed to add to the list</param>
        public void AddFeed(Feed newFeed)
        {
            if (FeedList == null)
                FeedList = new List<Feed>();

            FeedList.Add(newFeed);
        }

        /// <summary>
        /// method for adding a feed to an existing group
        /// </summary>
        /// <param name="newFeed">the feed to be added to the group</param>
        /// <param name="groupName">name of the(sub-)group</param>
        /// <returns>returns false if group couldn't be found</returns>
        public bool AddFeed(Feed newFeed, string groupName)
        {
            bool result = false;

            FeedGroup searchGroup = null;

            //try to find the group we're looking for
            searchGroup = FindGroup(groupName);

            //if search was successfull -> add the new feed
            if(searchGroup != null)
            {
                searchGroup.AddFeed(newFeed);
                result = true;
            }

            return result;
        }

        

        /// <summary>
        /// method for adding a new group to the list
        /// </summary>
        /// <param name="name">name of the new feed</param>
        /// <param name="url">url of the group (optional)</param>
        public void AddGroup(string name, string url="")
        {
            if (FeedGroups == null)
                FeedGroups = new List<FeedGroup>();

            FeedGroups.Add(new FeedGroup(name, url));
        }

        /// <summary>
        /// method for adding a new group and a feed to this group at the same time
        /// </summary>
        /// <param name="newFeed">the feed to be added to the group</param>
        /// <param name="groupName">name of the new group</param>
        /// <param name="groupurl">URL of the new group (optional)</param>
        public void AddFeedAndGroup(Feed newFeed, string groupName, string groupUrl = "")
        {
            if (FeedList == null)
                FeedList = new List<Feed>();

            if (FeedGroups == null)
                FeedGroups = new List<FeedGroup>();

            FeedGroup newGroup = new FeedGroup(groupName, groupUrl);
            newGroup.AddFeed(newFeed);

            FeedGroups.Add(newGroup);
        }

        /// <summary>
        /// method for finding a group within the group-tree
        /// </summary>
        /// <param name="name">name of the group</param>
        /// <returns>returns null if nothing can be found</returns>
        private FeedGroup FindGroup(string name)
        {
            FeedGroup result = null;

            if (this.Title == name)
            {
                result = this;
            }
            else
            {
                if (!IsLeaf())
                {
                    foreach (FeedGroup fGroup in FeedGroups)
                    {
                        FeedGroup tmpGr = fGroup.FindGroup(name);
                        if (tmpGr != null)
                        {
                            result = tmpGr;
                            break;
                        }
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// get title for Treenode
        /// </summary>
        /// <returns>returns title with prefixes (number of unread items and so on)</returns>
        public string GetNodeText()
        {
            string result = Title;

            int unreadFeedItems = GetNoOfUnreadFeedItems();

            if(unreadFeedItems > 0)
            {
                result = "(" + unreadFeedItems.ToString() + ") " + Title;
            }

            return result;
        }


        /// <summary>
        /// get number of unread feed-items (including subgroups)
        /// </summary>
        /// <returns>returns number of unread feeditems</returns>
        public int GetNoOfUnreadFeedItems()
        {
            int result = 0;

            if(FeedGroups != null)
            {
                if(FeedGroups.Count() > 0)
                {
                    foreach(FeedGroup fGroup in FeedGroups)
                    {
                        result += fGroup.GetNoOfUnreadFeedItems();
                    }
                }
            }

            if(FeedList != null)
            {
                if(FeedList.Count() > 0)
                {
                    foreach(Feed feed in FeedList)
                    {
                        result += feed.GetNoOfUnreadItems();
                    }
                }
            }

            return result;
        }

        #region Overridden Functions
        public override string ToString()
        {
            StringBuilder FeedGroupString = new StringBuilder();
            FeedGroupString.Append("<FeedGroup>");

            /*
            BodyString.Append("<body>");
            foreach (Outline Outline in Outlines)
            {
                BodyString.Append(Outline.ToString());
            }
            BodyString.Append("</body>");*/

            FeedGroupString.Append("</FeedGroup>");
            return FeedGroupString.ToString();

        }
        #endregion
    }
}
