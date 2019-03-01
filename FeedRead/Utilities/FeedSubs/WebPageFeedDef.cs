﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace FeedRead.Utilities.FeedSubs
{
    /// <summary>
    /// class for defining properties of a feed, derived from a webpage without any direct rss-feed
    /// </summary>
    public class WebPageFeedDef
    {

        public WebPageFeedDef() { }

        public WebPageFeedDef(string name, string keyID, string baseURL, string classID_Title, string classID_UpdateTime)
        {
            Name = name;
            KeyID = keyID;
            BaseURL = baseURL;
            ClassID_Title = classID_Title;
            ClassID_UpdateTime = classID_UpdateTime;
        }



        #region properties
        [XmlElement("Name")]
        public string Name { get; set; }

        [XmlElement("KeyID")]
        public string KeyID { get; set; }

        [XmlElement("BaseURL")]
        public string BaseURL { get; set; }

        [XmlElement("ClassIDTitle")]
        public string ClassID_Title { get; set; }// = "_2dU-m _1qbNn";

        [XmlElement("ClassIDUpdateTime")]
        public string ClassID_UpdateTime { get; set; }//= "_1D0de col-4 col-md-3";


        #endregion
    }
}
