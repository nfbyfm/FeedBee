#region Usings
using System;
using System.Text;
using System.Xml;
#endregion

namespace FeedRead.Utilities.OPML
{
    /// <summary>
    /// OPML class
    /// </summary>
    public class OPML
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public OPML()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Location">Location of the OPML file</param>
        public OPML(string Location)
        {
            XmlDocument Document = new XmlDocument();
            Document.Load(Location);
            foreach (XmlNode Children in Document.ChildNodes)
            {
                if (Children.Name.Equals("opml", StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (XmlNode Child in Children.ChildNodes)
                    {
                        if (Child.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Body = new Body((XmlElement)Child);
                        }
                        else if (Child.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Head = new Head((XmlElement)Child);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Document">XmlDocument containing the OPML file</param>
        public OPML(XmlDocument Document)
        {
            foreach (XmlNode Children in Document.ChildNodes)
            {
                if (Children.Name.Equals("opml", StringComparison.CurrentCultureIgnoreCase))
                {
                    foreach (XmlNode Child in Children.ChildNodes)
                    {
                        if (Child.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Body = new Body((XmlElement)Child);
                        }
                        else if (Child.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Head = new Head((XmlElement)Child);
                        }
                    }
                }
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Body of the file
        /// </summary>
        public Body Body { get; set; }
        /// <summary>
        /// Header information
        /// </summary>
        public Head Head { get; set; }
        #endregion
        #region Overridden Functions
        public override string ToString()
        {
            StringBuilder OPMLString = new StringBuilder();

            OPMLString.Append("<opml version=\"1.0\" xmlns: fz = \"urn:forumzilla:\" > " + Environment.NewLine);// "<?xml version=\"1.0\" encoding=\"ISO-8859-1\"?><opml version=\"2.0\">");
            OPMLString.Append(Head.GetOPMLString(1));
            OPMLString.Append(Body.GetOPMLString(1) + Environment.NewLine);
            OPMLString.Append("</opml>");

            return OPMLString.ToString();
        }
        #endregion
    }
}
