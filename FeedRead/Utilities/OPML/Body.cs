 #region Usings
 using System;
 using System.Collections.Generic;
 using System.Text;
 using System.Xml;
#endregion

namespace FeedRead.Utilities.OPML
{
    /// <summary>
    /// Body class
    /// </summary>
    public class Body
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Body()
        {
            Outlines = new List<Outline>();
        }
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="Element">XmlElement containing the body information</param>
        public Body(XmlElement Element)
        {
            Outlines = new List<Outline>();
            if (Element.Name.Equals("body", StringComparison.CurrentCultureIgnoreCase))
            {
                foreach (XmlNode Child in Element.ChildNodes)
                {
                    try
                    {
                        if (Child.Name.Equals("outline", StringComparison.CurrentCultureIgnoreCase))
                        {
                            Outlines.Add(new Outline((XmlElement)Child));
                        }
                    }
                    catch { }
                }
            }
        }
        #endregion
        #region Properties
        /// <summary>
        /// List of outlines
        /// </summary>
        public List<Outline> Outlines { get; set; }
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

            StringBuilder BodyString = new StringBuilder();

            BodyString.Append(prefix + "<body>" + Environment.NewLine);
            foreach (Outline Outline in Outlines)
            {
                BodyString.Append(Outline.GetOPMLString(level+1));
            }
            BodyString.Append(prefix + "</body>");

            return BodyString.ToString();
        }

        #endregion

        #region Overridden Functions
        public override string ToString()
        {
            StringBuilder BodyString = new StringBuilder();
            BodyString.Append("<body>" + Environment.NewLine);
            foreach (Outline Outline in Outlines)
            {
                BodyString.Append("\t" + Outline.ToString());
            }
            BodyString.Append("</body>");
            return BodyString.ToString();
        }
        #endregion
    }
}