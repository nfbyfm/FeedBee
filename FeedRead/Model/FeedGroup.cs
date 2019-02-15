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
