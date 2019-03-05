#region Usings
using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
#endregion

namespace FeedRead.Utilities.OPML
{
    /// <summary>
    /// Outline class
    /// </summary>
    public class Outline
    {
        #region Constructors
        /// <summary>
        /// Constructor
        /// </summary>
        public Outline()
        {
            Outlines = new List<Outline>();
        }
        /// <summary>
        /// Constructor: parse outline from xmlElement
        /// </summary>
        /// <param name="Element">Element containing outline information</param>
        public Outline(XmlElement Element)
        {
            if (Element.Name.Equals("outline", StringComparison.CurrentCultureIgnoreCase))
            {

                foreach(XmlAttribute atr in Element.Attributes)
                {
                    switch (atr.Name.ToLower())
                    {
                        case "text":
                            Text = atr.Value;
                            break;
                        case "description":
                            Description = atr.Value;
                            break;
                        case "htmlurl":
                            HTMLUrl = atr.Value;
                            break;
                        case "type":
                            Type = atr.Value;
                            break;
                        case "language":
                            Language = atr.Value;
                            break;
                        case "title":
                            Title = atr.Value;
                            break;
                        case "version":
                            Version = atr.Value;
                            break;
                        case "xmlurl":
                            XMLUrl = atr.Value;
                            break;
                        
                        default:
                            break;
                    }
                }

                Outlines = new List<Outline>();

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
        /// Outline list
        /// </summary>
        public List<Outline> Outlines { get; set; }
        /// <summary>
        /// Url of the XML file
        /// </summary>
        public string XMLUrl { get; set; }
        /// <summary>
        /// Version number
        /// </summary>
        public string Version { get; set; }
        /// <summary>
        /// Title of the item
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// Language used
        /// </summary>
        public string Language { get; set; }
        /// <summary>
        /// Type
        /// </summary>
        public string Type { get; set; }
        /// <summary>
        /// HTML Url
        /// </summary>
        public string HTMLUrl { get; set; }
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }
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

            if(level > 0)
            {
                for (int i = 0; i < level; i++)
                {
                    prefix += "\t";
                }
            }

            StringBuilder OutlineString = new StringBuilder();

            OutlineString.Append(prefix + "<outline text=\"" + Text + "\"");

            if (!string.IsNullOrEmpty(XMLUrl))
            {
                OutlineString.Append(" xmlUrl=\"" + XMLUrl + "\"");
            }

            if (!string.IsNullOrEmpty(Version))
            {
                OutlineString.Append(" version=\"" + Version + "\"");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                OutlineString.Append(" title=\"" + Title + "\"");
            }

            if (!string.IsNullOrEmpty(Language))
            {
                OutlineString.Append(" language=\"" + Language + "\"");
            }

            if (!string.IsNullOrEmpty(Type))
            {
                OutlineString.Append(" type=\"" + Type + "\"");
            }

            if (!string.IsNullOrEmpty(HTMLUrl))
            {
                OutlineString.Append(" htmlUrl=\"" + HTMLUrl + "\"");
            }
            
            if (!string.IsNullOrEmpty(Description))
            {
                OutlineString.Append(" description=\"" + Description + "\"");
            }

            if (Outlines != null)
            {
                if (Outlines.Count > 0)
                {
                    OutlineString.Append(">\r");
                    foreach (Outline Outline in Outlines)
                    {
                        OutlineString.Append(Outline.GetOPMLString(level +1));// + Environment.NewLine);
                    }
                    OutlineString.Append(prefix + "</outline>" + Environment.NewLine);
                }
                else
                {
                    OutlineString.Append("></outline>" + Environment.NewLine);
                }
            }
            else
            {
                OutlineString.Append("></outline>" + Environment.NewLine);
            }


            return OutlineString.ToString();


        }

        #endregion
        
        #region Overridden Functions
        /*
        public override string ToString()
        {
            StringBuilder OutlineString = new StringBuilder();
            OutlineString.Append("<outline text=\"" + Text + "\"");
            if (!string.IsNullOrEmpty(XMLUrl))
            {
                OutlineString.Append(" xmlUrl=\"" + XMLUrl + "\"");
            }
            if (!string.IsNullOrEmpty(Version))
            {
                OutlineString.Append(" version=\"" + Version + "\"");
            }
            if (!string.IsNullOrEmpty(Title))
            {
                OutlineString.Append(" title=\"" + Title + "\"");
            }
            if (!string.IsNullOrEmpty(Language))
            {
                OutlineString.Append(" language=\"" + Language + "\"");
            }
            if (!string.IsNullOrEmpty(Type))
            {
                OutlineString.Append(" type=\"" + Type + "\"");
            }
            if (!string.IsNullOrEmpty(HTMLUrl))
            {
                OutlineString.Append(" htmlUrl=\"" + HTMLUrl + "\"");
            }
            if (!string.IsNullOrEmpty(Text))
            {
                OutlineString.Append(" text=\"" + Text + "\"");
            }
            if (!string.IsNullOrEmpty(Description))
            {
                OutlineString.Append(" description=\"" + Description + "\"");
            }

            if (Outlines!=null)
            {
                if (Outlines.Count > 0)
                {
                    OutlineString.Append(">\r" );
                    foreach (Outline Outline in Outlines)
                    {
                        OutlineString.Append("\t\t" + Outline.ToString());// + Environment.NewLine);
                    }
                    OutlineString.Append("\t</outline>" + Environment.NewLine);
                }
                else
                {
                    OutlineString.Append("/></outline>" + Environment.NewLine);
                }
            }
            else
            {
                OutlineString.Append("/></outline>" + Environment.NewLine);
            }
            return OutlineString.ToString();
        }
        */
        public bool IsFinalNode()
        {
            bool result = true;

            if(Outlines != null)
            {
                if(Outlines.Count>0)
                {
                    result = false;
                }
            }

            return result;
        }
        #endregion
    }
}
