
 #region Usings
 using System;
 using System.Text;
 using System.Xml;
 #endregion

 namespace FeedBee.Utilities.OPML
{
    /// <summary>
    /// Head class
    /// </summary>
    public class Head
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Head()
        {
            Docs = "http://www.opml.org/spec2";
            DateCreated = DateTime.Now;
            DateModified = DateTime.Now;
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Element">XmlElement containing the header information</param>
        public Head(XmlElement Element)
        {
            if (Element.Name.Equals("head", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (XmlNode Child in Element.ChildNodes)
                {
                    try
                    {
                        if (Child.Name.Equals("title", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Title = Child.InnerText;
                        }
                        else if (Child.Name.Equals("ownerName", StringComparison.CurrentCultureIgnoreCase))
                        {
                            OwnerName = Child.InnerText;
                        }
                        else if (Child.Name.Equals("ownerEmail", StringComparison.CurrentCultureIgnoreCase))
                        {
                            OwnerEmail = Child.InnerText;
                        }
                        else if (Child.Name.Equals("dateCreated", StringComparison.CurrentCultureIgnoreCase))
                        {
                            DateCreated = DateTime.Parse(Child.InnerText);
                        }
                        else if (Child.Name.Equals("dateModified", StringComparison.CurrentCultureIgnoreCase))
                        {
                            DateModified = DateTime.Parse(Child.InnerText);
                        }
                        else if (Child.Name.Equals("docs", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Docs = Child.InnerText;
                        }
                    }
                    catch { }
                }
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// Title of the OPML document
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Date it was created
        /// </summary>
        public DateTime DateCreated { get; set; }
        /// <summary>
        /// Date it was last modified
        /// </summary>
        public DateTime DateModified { get; set; }
        /// <summary>
        /// Owner of the file
        /// </summary>
        public string OwnerName { get; set; }
        /// <summary>
        /// Owner's email address
        /// </summary>
        public string OwnerEmail { get; set; }
        /// <summary>
        /// Location of the OPML spec
        /// </summary>
        public string Docs { get; set; }
        #endregion

        #region OPML-Export

        /// <summary>
        /// get the string for the opml-export
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public string GetOPMLString(int level)
        {
            //used for tabs
            string prefix = "";

            if (level > 0)
            {
                for (int i = 0; i < level; i++)
                {
                    prefix += "\t";
                }
            }

            StringBuilder HeadString = new StringBuilder();

            HeadString.Append(prefix + "<head>" + Environment.NewLine);
            HeadString.Append(prefix + "\t<title>" + Title + "</title>" + Environment.NewLine);
            HeadString.Append(prefix + "\t<dateCreated>" + DateCreated.ToString("R") + "</dateCreated>" + Environment.NewLine);
            HeadString.Append(prefix + "\t<dateModified>" + DateModified.ToString("R") + "</dateModified>" + Environment.NewLine);
            HeadString.Append(prefix + "\t<ownerName>" + OwnerName + "</ownerName>" + Environment.NewLine);
            HeadString.Append(prefix + "\t<ownerEmail>" + OwnerEmail + "</ownerEmail>" + Environment.NewLine);
            HeadString.Append(prefix + "\t<docs>" + Docs + "</docs>" + Environment.NewLine);
            HeadString.Append(prefix + "</head>" + Environment.NewLine);

            return HeadString.ToString();
        }


        #endregion

        #region Overridden Functions
        public override string ToString()
        {
            StringBuilder HeadString = new StringBuilder();
            HeadString.Append("<head>" + Environment.NewLine);
            HeadString.Append("\t<title>" + Title + "</title>" + Environment.NewLine);
            HeadString.Append("\t<dateCreated>" + DateCreated.ToString("R") + "</dateCreated>" + Environment.NewLine);
            HeadString.Append("\t<dateModified>" + DateModified.ToString("R") + "</dateModified>" + Environment.NewLine);
            HeadString.Append("\t<ownerName>" + OwnerName + "</ownerName>" + Environment.NewLine);
            HeadString.Append("\t<ownerEmail>" + OwnerEmail + "</ownerEmail>" + Environment.NewLine);
            HeadString.Append("\t<docs>" + Docs + "</docs>" + Environment.NewLine);
            HeadString.Append("</head>" + Environment.NewLine);
            return HeadString.ToString();
        }
        #endregion
    }
}