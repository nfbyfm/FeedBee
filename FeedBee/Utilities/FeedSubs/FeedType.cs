using System.Xml.Serialization;

namespace FeedSubs.FeedReader
{
    /// <summary>
    /// The type of the feed (Rss 0.91, Rss 2.0, Atom, ...)
    /// </summary>
    public enum FeedType
    {
        /// <summary>
        /// Atom Feed
        /// </summary>
        [XmlEnum(Name = "Atom")]
        Atom,

        /// <summary>
        /// Rss 0.91 feed
        /// </summary>
        [XmlEnum(Name = "RSS091")]
        Rss_0_91,

        /// <summary>
        /// Rss 0.92 feed
        /// </summary>
        [XmlEnum(Name = "RSS092")]
        Rss_0_92,

        /// <summary>
        /// Rss 1.0 feed
        /// </summary>
        [XmlEnum(Name = "RSS10")]
        Rss_1_0,

        /// <summary>
        /// Rss 2.0 feed
        /// </summary>
        [XmlEnum(Name = "RSS20")]
        Rss_2_0,

        /// <summary>
        /// Media Rss feed
        /// </summary>
        [XmlEnum(Name = "MediaRSS")]
        MediaRss,


        /// <summary>
        /// Rss feed - is used for <see cref="HtmlFeedLink"/> type
        /// </summary>
        [XmlEnum(Name = "RSS")]
        Rss,

        /// <summary>
        /// Unknown - default type
        /// </summary>
        [XmlEnum(Name = "Unknown")]
        Unknown
    }
}
