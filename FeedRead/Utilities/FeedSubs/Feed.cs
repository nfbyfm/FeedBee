

namespace FeedSubs.FeedReader
{
    using Feeds;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// Generic Feed object that contains some basic properties. If a property is not available
    /// for a specific feed type (e.g. Rss 1.0), then the property is empty.
    /// If a feed has more properties, like the Generator property for Rss 2.0, then you can use
    /// the <see cref="SpecificFeed"/> property.
    /// </summary>
    public class Feed
    {
        /// <summary>
        /// The Type of the feed - Rss 2.0, 1.0, 0.92, Atom or others
        /// </summary>
        [XmlElement("Feedtype")]
        public FeedType Type { get; set; }

        /// <summary>
        /// The title of the field
        /// </summary>
        [XmlElement("Title")]
        public string Title { get; set; }

        /// <summary>
        /// The link (url) to the feed
        /// </summary>
        [XmlElement("Link")]
        public string Link { get; set; }

        /// <summary>
        /// The description of the feed
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// The language of the feed
        /// </summary>
        [XmlElement("Language")]
        public string Language { get; set; }

        /// <summary>
        /// The copyright of the feed
        /// </summary>
        [XmlElement("Copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// The last updated date as string. This is filled, if a last updated
        /// date is set - independent if it is a correct date or not
        /// </summary>
        [XmlElement("LastUpdate")]
        public string LastUpdatedDateString { get; set; }

        /// <summary>
        /// The last updated date as datetime. Null if parsing failed or if
        /// no last updated date is set. If null, please check <see cref="LastUpdatedDateString"/> property.
        /// </summary>
        [XmlElement("LastUpdateDate")]
        public DateTime? LastUpdatedDate { get; set; }

        /// <summary>
        /// The url of the image
        /// </summary>
        [XmlElement("ImageURL")]
        public string ImageUrl { get; set; }

        /// <summary>
        /// tells whether the feed has been updated or not
        /// </summary>
        [XmlElement("Updated")]
        public bool Updated { get; set; }

        /// <summary>
        /// original url of the feed
        /// </summary>
        [XmlElement("FeedURL")]
        public string FeedURL { get; set; }

        /// <summary>
        /// when an item gets displayed, either load the description (value = false) or the webURL (value = true)
        /// </summary>
        [XmlElement("DirectlyLoadWebpage")]
        public bool DirectlyLoadWebpage { get; set; }


        /// <summary>
        /// List of items
        /// </summary>
        [XmlArray("FeedItems"), XmlArrayItem("Item")]
        public List<FeedItem> Items { get; set; }

        /// <summary>
        /// Gets the whole, original feed as string
        /// </summary>
        [XmlIgnore]
        public string OriginalDocument
        {
            get { return SpecificFeed.OriginalDocument; }
        }

        /// <summary>
        /// The parsed feed element - e.g. of type <see cref="Rss20Feed"/> which contains
        /// e.g. the Generator property which does not exist in others.
        /// </summary>
        [XmlIgnore]
        public BaseFeed SpecificFeed { get; set; }

        

        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class.
        /// Default constructor, just there for serialization.
        /// </summary>
        public Feed()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Feed"/> class.
        /// Creates the generic feed object based on a parsed BaseFeed
        /// </summary>
        /// <param name="feed">BaseFeed which is a <see cref="Rss20Feed"/> , <see cref="Rss10Feed"/>, or another.</param>
        public Feed(BaseFeed feed)
        {
            SpecificFeed = feed;

            Title = feed.Title;
            Link = feed.Link;

            Items = feed.Items.Select(x => x.ToFeedItem()).ToList();
        }

        /// <summary>
        /// get Text for Treeview-Node
        /// </summary>
        /// <returns>Title and appendices/prefixes (number of unread Feeditems and so on)</returns>
        public string GetNodeText()
        {
            string result = Title;

            //get number of unread feed-items
            int unreadCount = GetNoOfUnreadItems();

            if(unreadCount > 0)
            {
                result = "(" + unreadCount.ToString() + ") " + Title;
            }
            
            if(Updated == false)
            {
                result = "* " + result;
            }

            return result;
        }

        /// <summary>
        /// get number of unread feeditems
        /// </summary>
        /// <returns>returns -1 if no feeditems exists, otherwise the nubmer of unread items</returns>
        public int GetNoOfUnreadItems()
        {
            int result= -1; 

            if(Items != null)
            {
                if(Items.Count() > 0)
                {
                    result = 0;

                    foreach(FeedItem item in Items)
                    {
                        if (item.Read == false)
                        {
                            result++;
                        }
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// get whole feed-object-properties as one string (for debugging)
        /// </summary>
        /// <returns></returns>
        public string GetDebugInfo()
        {
            string result = "Feedtitle = '" + Title + "'" + Environment.NewLine;

            result += "Link: '" + Link + "' ";
            result += "FeedUrl: '" + FeedURL + "' ";
            result += "Description: '" + Description + "' ";
            result += "ImageURL: '" + ImageUrl + "' ";

            return result;
        }
    }
}
