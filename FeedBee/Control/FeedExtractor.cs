using FeedBee.Utilities.FeedSubs;
using FeedSubs.FeedReader;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;


namespace FeedBee.Control
{
    

    /// <summary>
    /// class for creating a pseudo-feed from a webpage without a rss-feed
    /// </summary>
    public class FeedExtractor
    {
        /// <summary>
        /// List of possible markers which indicate the title of a possible feeditem
        /// </summary>
        private List<string> titleMarkers;



        public FeedExtractor()
        {
            titleMarkers = new List<string>();
            titleMarkers.Add("alt=\"");
            titleMarkers.Add("title=\"");
        }


        /// <summary>
        /// try to get a feed form a url using the given definition
        /// </summary>
        /// <param name="pageUrl"></param>
        /// <param name="feed"></param>
        /// <param name="webPageFeedDef"></param>
        public void Read(string pageUrl, ref Feed feed, WebPageFeedDef webPageFeedDef)
        {
            feed = null;

            if(webPageFeedDef != null)
            {
                if(!pageUrl.ToLower().Contains(webPageFeedDef.BaseURL.ToLower()))
                {
                    return;
                }
            }
            else
            {
                return;
            }

            bool loadSuccess = false;

            string content = "";

            try
            {
                //try to download the main page
                content = new System.Net.WebClient().DownloadString(pageUrl);
                loadSuccess = true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Error while getting feed-webpage: " + Environment.NewLine + ex.Message);
            }

            if(loadSuccess)
            {
                var htmlDoc = new HtmlAgilityPack.HtmlDocument()
                {
                    OptionAutoCloseOnEnd = true,
                    OptionFixNestedTags = true
                };

                htmlDoc.LoadHtml(content);

                if (htmlDoc.DocumentNode != null)
                {
                    feed = new Feed();
                    feed.FeedURL = pageUrl;
                    
                    //get title (if not properly loaded already)
                    HtmlNode titleNode = htmlDoc.DocumentNode.Descendants("title").FirstOrDefault();
                    if (titleNode != null)
                    {
                        feed.Title = System.Web.HttpUtility.HtmlDecode(titleNode.InnerText);
                    }
                    else
                    {
                        Debug.WriteLine("titlenode is null!");
                    }

                    //get feeditems
                    string classID_Title = webPageFeedDef.ClassID_Title;
                    string classID_UpdateTime = webPageFeedDef.ClassID_UpdateTime;

                    HtmlNodeCollection titleNodes = htmlDoc.DocumentNode.SelectNodes(classID_Title);
                    HtmlNodeCollection timeNodes = htmlDoc.DocumentNode.SelectNodes(classID_UpdateTime);

                    

                    try
                    {
                        if (titleNodes != null && timeNodes != null)
                        {
                            if (timeNodes.Count > 0 && titleNodes.Count > 0)
                            {
                                feed.Items = new List<FeedItem>();

                                //create List of items from detected entries with their upload-Dates
                                for (int i = 0; i < Math.Min(timeNodes.Count, titleNodes.Count); i++)
                                {

                                    string suburl = titleNodes[i].OuterHtml;

                                    if (suburl.Contains("/"))
                                    {
                                        suburl = suburl.Remove(0, titleNodes[i].OuterHtml.IndexOf("/"));
                                    }


                                    string url = webPageFeedDef.BaseURL;

                                    if (suburl.Contains("\""))
                                    {
                                        url += suburl.Remove(suburl.IndexOf("\""));
                                    }

                                    
                                    string title = System.Web.HttpUtility.HtmlDecode(titleNodes[i].InnerHtml);

                                    //search for feeditem-title in the innerHtml
                                    foreach(string titleMarker in titleMarkers)
                                    {
                                        if (title.ToLower().Contains(titleMarker))
                                        {
                                            title = title.Remove(0, title.ToLower().LastIndexOf(titleMarker) + titleMarker.Length);
                                            title = title.Remove(title.IndexOf("\""));
                                        }
                                    }
                                    

                                    DateTime updateTime = getDateTime(timeNodes[i].InnerHtml);

                                    FeedItem item = new FeedItem();
                                    item.Id = url;
                                    item.Link = url;
                                    item.PublishingDate = updateTime;
                                    item.Read = false;
                                    item.Title = title;

                                    feed.Items.Add(item);
                                }

                                feed.Updated = true;
                            }
                        }
                        else
                        {
                            Debug.WriteLine(feed.Title + ": no Chapter-Nodes detected.");
                        }
                    }
                    catch (Exception ex)
                    {
                        Debug.WriteLine("Innerexception 1: " + ex.Message);
                    }
                    
                }




                
                

                
            }
             
        }

        public string TestRead(string pageUrl, WebPageFeedDef webPageFeedDef)
        {
            string result = "";

            if (webPageFeedDef != null)
            {
                if (!pageUrl.ToLower().Contains(webPageFeedDef.BaseURL.ToLower()))
                {
                    return result;
                }
            }
            else
            {
                return result;
            }

            bool loadSuccess = false;

            string content = "";

            try
            {
                //try to download the main page
                content = new System.Net.WebClient().DownloadString(pageUrl);
                loadSuccess = true;
            }
            catch (Exception ex)
            {
                result = "Error while getting feed-webpage: " + Environment.NewLine + ex.Message;
            }

            if (loadSuccess)
            {
                var htmlDoc = new HtmlAgilityPack.HtmlDocument()
                {
                    OptionAutoCloseOnEnd = true,
                    OptionFixNestedTags = true
                };

                htmlDoc.LoadHtml(content);

                if (htmlDoc.DocumentNode != null)
                {
                    string feedTitle = "";

                    //get title (if not properly loaded already)
                    HtmlNode titleNode = htmlDoc.DocumentNode.Descendants("title").FirstOrDefault();
                    if (titleNode != null)
                    {
                        feedTitle = System.Web.HttpUtility.HtmlDecode(titleNode.InnerText);
                        result += "Title = " + feedTitle + Environment.NewLine;
                    }
                    else
                    {
                        result += "No Title found" + Environment.NewLine;
                    }

                    //get feeditems
                    string classID_Title = webPageFeedDef.ClassID_Title;
                    string classID_UpdateTime = webPageFeedDef.ClassID_UpdateTime;

                    HtmlNodeCollection titleNodes = htmlDoc.DocumentNode.SelectNodes(classID_Title);
                    HtmlNodeCollection timeNodes = htmlDoc.DocumentNode.SelectNodes(classID_UpdateTime);
                    try
                    {
                        if (titleNodes != null && timeNodes != null)
                        {
                            if (timeNodes.Count > 0 && titleNodes.Count > 0)
                            {
                                
                                //create List of feeditems from detected entries with their upload-Dates
                                for (int i = 0; i < Math.Min(timeNodes.Count, titleNodes.Count); i++)
                                {

                                    string suburl = titleNodes[i].OuterHtml;

                                    if (suburl.Contains("/"))
                                    {
                                        suburl = suburl.Remove(0, titleNodes[i].OuterHtml.IndexOf("/"));
                                    }


                                    string url = webPageFeedDef.BaseURL;

                                    if (suburl.Contains("\""))
                                    {
                                        url += suburl.Remove(suburl.IndexOf("\""));
                                    }

                                    //Debug.WriteLine("suburl = " + suburl + "   url = " + url);


                                    string title = System.Web.HttpUtility.HtmlDecode(titleNodes[i].InnerHtml);

                                    //search for feeditem-title in the innerHtml
                                    foreach (string titleMarker in titleMarkers)
                                    {
                                        if (title.ToLower().Contains(titleMarker))
                                        {
                                            title = title.Remove(0, title.ToLower().LastIndexOf(titleMarker) + titleMarker.Length);
                                            title = title.Remove(title.IndexOf("\""));
                                        }
                                    }

                                    DateTime updateTime = getDateTime(timeNodes[i].InnerHtml);

                                    result += "Feeditem: Title: '" + title + "' Link: '" + url + "' publishing-Date: '" + updateTime.ToString("dd.MM.yyyy hh:mm") + "' " + Environment.NewLine;
                                    
                                }

                                
                            }
                        }
                        else
                        {
                            result += feedTitle + ": no Feeditems detected." + Environment.NewLine;
                        }
                    }
                    catch (Exception ex)
                    {
                        result += "Error getting Feed: " + ex.Message;
                    }
                    
                }
            }


            return result;
        }

        private DateTime getDateTime(string innerHtml)
        {
            DateTime dateTime = DateTime.Today;

            try
            {
                if (innerHtml.ToLower().Contains("ago"))
                {
                    string[] splits = innerHtml.Split();
                    int days = Convert.ToInt32(splits[0]);
                    dateTime = DateTime.Today.AddDays(-1 * days);
                }
                else
                {
                    dateTime = DateTime.Parse(innerHtml);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }

            return dateTime;
        }


        
    }

    

}
