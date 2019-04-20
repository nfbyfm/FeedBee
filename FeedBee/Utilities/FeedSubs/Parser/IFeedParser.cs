using FeedSubs.FeedReader.Feeds;

namespace FeedSubs.FeedReader.Parser
{
    internal interface IFeedParser
    {
        BaseFeed Parse(string feedXml);
    }
}
