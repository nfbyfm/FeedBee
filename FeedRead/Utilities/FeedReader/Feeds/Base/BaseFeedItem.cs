﻿namespace CodeHollow.FeedReader.Feeds
{
    using System.Xml.Linq;
    using System.Xml.Serialization;

    /// <summary>
    /// The base object for all feed items
    /// </summary>
    public abstract class BaseFeedItem
    {
        /// <summary>
        /// The "title" element
        /// </summary>
        [XmlElement("Title")]
        public string Title { get; set; } // title

        /// <summary>
        /// The "link" element
        /// </summary>
        [XmlElement("Link")]
        public string Link { get; set; } // link

        /// <summary>
        /// Gets the underlying XElement in order to allow reading properties that are not available in the class itself
        /// </summary>
        [XmlIgnore]
        public XElement Element { get; }

        /// <summary>
        /// if item has been read or not
        /// </summary>
        [XmlElement("Read")]
        public bool Read { get; set; }

        internal abstract FeedItem ToFeedItem();

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeedItem"/> class.
        /// default constructor (for serialization)
        /// </summary>
        protected BaseFeedItem()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseFeedItem"/> class.
        /// Reads a base feed item based on the xml given in element
        /// </summary>
        /// <param name="item">feed item as xml</param>
        protected BaseFeedItem(XElement item)
        {
            this.Title = item.GetValue("title");
            this.Link = item.GetValue("link");
            this.Element = item;
        }
    }
}
