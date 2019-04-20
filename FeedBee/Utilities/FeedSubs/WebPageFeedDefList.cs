using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedBee.Utilities.FeedSubs
{
    public class WebPageFeedDefList
    {
        public WebPageFeedDefList() { }

        public WebPageFeedDefList(WebPageFeedDef firstDef)
        {
            Definitions = new List<WebPageFeedDef>();
            Definitions.Add(firstDef);
        }

        #region properties
        /// <summary>
        /// List of WebPageFeedDefinitions
        /// </summary>
        [XmlArray("WebPageFeedDefinitions"), XmlArrayItem("WebPageFeedDefinition")]
        public List<WebPageFeedDef> Definitions { get; set; }

        #endregion
    }
}
