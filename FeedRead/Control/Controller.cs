using CodeHollow.FeedReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeedRead
{
    public class Controller
    {
        public Controller(MainForm mainForm)
        {

        }

        #region UI-Fucntions
        public void ImportFeedList()
        {

        }

        public void ExportFeedList()
        {

        }

        public void AddNewFeed()
        {
            AddFeedDialog addFeedDialog = new AddFeedDialog(this);
            if(addFeedDialog.ShowDialog()==System.Windows.Forms.DialogResult.OK)
            {
                //show next dialog (add Feed to a Group)
                string newFeedUrl = addFeedDialog.feedUrl;

                Console.WriteLine("Controller.AddNewFeed: got new feed-source from user: " + newFeedUrl);

                //check if feed already exists in one of the feed-groups
            }
        }

        public void ShowAboutDialog()
        {

        }

        public void ShowSettings()
        {

        }

        #endregion


        #region Feed-related functions
        //checks a url for feeds. returns null if none could be found
        public List<string> CheckUrlForFeeds(string url)
        {
            List<string> result = null;

            if(!string.IsNullOrEmpty(url) && !string.IsNullOrWhiteSpace(url))
            {
                var urls = FeedReader.GetFeedUrlsFromUrl(url);

                

                if (urls.Count() < 1) // no url - probably the url is already the right feed url
                {
                    
                    //try to get the actual Feed-Items from the url
                    try
                    {
                        var feed = FeedReader.Read(url);

                        //if successful: add to list
                        result = new List<string>();
                        result.Add(url);

                    }
                    catch(Exception ex)
                    {
                        Console.WriteLine("Controller.CheckUrlForFeeds: Couldn't get feed from '" + url + "'. Errormessage: " + ex.Message);
                    }
                }
                else
                {
                    result = new List<string>();

                    //add each found feed to list
                    foreach(HtmlFeedLink feedLink in urls)
                    {
                        result.Add(feedLink.Url);
                    }
                }

            }


            return result;
        }


        #endregion
    }
}
