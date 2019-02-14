using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedRead
{
    public partial class AddFeedDialog : Form
    {
        private Controller controller;

        private const string errorNoFeedFound = "no feeds found";

        public string feedUrl;

        public AddFeedDialog(Controller parentController)
        {
            InitializeComponent();

            this.controller = parentController;
        }

        private void AddFeedDialog_Load(object sender, EventArgs e)
        {
            feedUrl = "";

            //check if there is text in the clipboard -> fill into the textbox
            if (Clipboard.ContainsText())
            {
                tB_Url.Text = Clipboard.GetText();
            }
        }

        private void b_checkFeeds_Click(object sender, EventArgs e)
        {
            CheckForFeeds();
        }


        private void b_Next_Click(object sender, EventArgs e)
        {
            //check if item in listbox has been selected
            if(lb_FoundFeeds.SelectedIndex >= 0)
            {
                feedUrl = lb_FoundFeeds.Items[lb_FoundFeeds.SelectedIndex].ToString();

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                //if there is only one possible feed -> use it
                if(lb_FoundFeeds.Items.Count == 1)
                {
                    feedUrl = lb_FoundFeeds.Items[0].ToString();

                    //check if only Item isn't a error-message
                    if(feedUrl == errorNoFeedFound)
                    {
                        MessageBox.Show("No feed found that could be added. Please check or change url.", "Add new Feed", MessageBoxButtons.OK,MessageBoxIcon.Asterisk);
                    }
                    else
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    
                }
                else
                {
                    MessageBox.Show("Please select one of the listed feeds.", "Add new Feed", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
                
            }
        }

        private void b_Cancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }





        private void CheckForFeeds()
        {
            if(controller != null && !string.IsNullOrEmpty(tB_Url.Text) && !string.IsNullOrWhiteSpace(tB_Url.Text))
            {
                List <string> results = controller.CheckUrlForFeeds(tB_Url.Text);

                lb_FoundFeeds.Items.Clear();

                if(results == null)
                {
                    //no feed found
                    lb_FoundFeeds.Items.Add(errorNoFeedFound);
                }
                else
                {
                    foreach(string newFeed in results)
                    {
                        lb_FoundFeeds.Items.Add(newFeed);
                    }
                }
            }
        }

        
    }
}
