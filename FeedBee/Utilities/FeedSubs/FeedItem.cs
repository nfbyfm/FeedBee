namespace FeedSubs.FeedReader
{
    using Feeds;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    /// <summary>
    /// Generic feed item object that contains some basic properties. The feed item is typically
    /// an article or a blog post. If a property is not available
    /// for a specific feed type (e.g. Rss 1.0), then the property is empty.
    /// If a feed item has more properties, like the Generator property for Rss 2.0, then you can use
    /// the <see cref="SpecificItem"/> property.
    /// </summary>
    public class FeedItem
    {
        /// <summary>
        /// The title of the feed item
        /// </summary>
        [XmlElement("Title")]
        public string Title { get; set; }

        /// <summary>
        /// The link (url) to the feed item
        /// </summary>
        [XmlElement("Link")]
        public string Link { get; set; }

        /// <summary>
        /// The description of the feed item
        /// </summary>
        [XmlElement("Description")]
        public string Description { get; set; }

        /// <summary>
        /// The publishing date as string. This is filled, if a publishing
        /// date is set - independent if it is a correct date or not
        /// </summary>
        [XmlElement("PublishingDateString")]
        public string PublishingDateString { get; set; }

        /// <summary>
        /// The published date as datetime. Null if parsing failed or if
        /// no publishing date is set. If null, please check <see cref="PublishingDateString"/> property.
        /// </summary>
        [XmlElement("PublishingDate")]
        public DateTime? PublishingDate { get; set; }

        /// <summary>
        /// The author of the feed item
        /// </summary>
        [XmlElement("Author")]
        public string Author { get; set; }

        /// <summary>
        /// The id of the feed item
        /// </summary>
        [XmlElement("ID")]
        public string Id { get; set; }

        /// <summary>
        /// The categories of the feeditem
        /// </summary>
        [XmlArray("Categories"), XmlArrayItem("Category")]
        public List<string> Categories { get; set; }

        /// <summary>
        /// The content of the feed item
        /// </summary>
        [XmlElement("Content")]
        public string Content { get; set; }

        /// <summary>
        /// if item has been read or not
        /// </summary>
        [XmlElement("Read")]
        public bool Read { get; set; }

        /// <summary>
        /// The parsed feed item element - e.g. of type <see cref="Rss20FeedItem"/> which contains
        /// e.g. the Enclosure property which does not exist in other feed types.
        /// </summary>
        [XmlIgnore]
        public BaseFeedItem SpecificItem { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedItem"/> class.
        /// Default constructor, just there for serialization.
        /// </summary>
        public FeedItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="FeedItem"/> class.
        /// Creates the generic feed item object based on a parsed <see cref="BaseFeedItem"/>
        /// </summary>
        /// <param name="feedItem">BaseFeedItem which is a <see cref="Rss20FeedItem"/> , <see cref="Rss10FeedItem"/>, or another.</param>
        public FeedItem(BaseFeedItem feedItem)
        {
            this.Title = feedItem.Title;
            this.Link = feedItem.Link;
            this.Categories = new List<string>();
            this.SpecificItem = feedItem;
            this.Read = feedItem.Read;
        }
    }
}
