using FeedBee.Controlling;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FeedBee.UI
{
    public partial class AddFeedDialog : Form
    {
        private Controller controller;

        private const string errorNoFeedFound = "no feeds found";

        public string feedUrl;

        public string groupName;
        public bool addNewGroupName;
        public bool newGroupIsNSFW;

        private List<string> groupNames;


        public AddFeedDialog(Controller parentController, List<string> groupNames)
        {
            InitializeComponent();

            this.controller = parentController;
            this.groupNames = groupNames;
        }

        private void AddFeedDialog_Load(object sender, EventArgs e)
        {

            bool allowAddToGroup = false;

            //set items of comboBox
            if (groupNames != null)
            {
                if (groupNames.Count() > 0)
                {
                    cB_Groups.Items.Clear();

                    foreach (string groupName in groupNames)
                    {
                        cB_Groups.Items.Add(groupName);
                    }
                    cB_Groups.SelectedIndex = 0;

                    allowAddToGroup = true;

                }

            }

            rB_addToGroup.Enabled = allowAddToGroup;
            cB_Groups.Enabled = allowAddToGroup;
            
            rB_addToGroup.Checked = allowAddToGroup;
            rB_CreateNewGroup.Checked = !allowAddToGroup;


            feedUrl = "";

            //check if there is text in the clipboard -> fill into the textbox
            if (Clipboard.ContainsText())
            {
                tB_Url.Text = Clipboard.GetText();
                CheckForFeeds();
            }
        }

        #region ui-methods (clicks, change-events, ...)
        private void b_checkFeeds_Click(object sender, EventArgs e)
        {
            CheckForFeeds();
        }


        private void b_Add_Click(object sender, EventArgs e)
        {
            addNewGroupName = rB_CreateNewGroup.Checked;
            newGroupIsNSFW = cB_GroupIsNSFW.Checked;

            if(addNewGroupName)
            {
                groupName = tB_NewGroupName.Text;
            }
            else
            {
                groupName = cB_Groups.Text;
            }

            
            //check if item in listbox has been selected
            if (lb_FoundFeeds.SelectedIndex >= 0)
            {
                feedUrl = lb_FoundFeeds.Items[lb_FoundFeeds.SelectedIndex].ToString();

                if(feedUrl == errorNoFeedFound)
                {
                    this.DialogResult = DialogResult.Cancel;
                    this.Close();
                }
                else
                {
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
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
            groupName = "";
            addNewGroupName = false;
            newGroupIsNSFW = false;

            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void tB_Url_MouseClick(object sender, MouseEventArgs e)
        {
            tB_Url.SelectAll();
        }


        private void rB_addToGroup_CheckedChanged(object sender, EventArgs e)
        {
            rBSelectionChanged();
        }

        private void rB_CreateNewGroup_CheckedChanged(object sender, EventArgs e)
        {
            rBSelectionChanged();
        }

        /// <summary>
        /// radio-button-selection has changed
        /// </summary>
        private void rBSelectionChanged()
        {
            cB_Groups.Enabled = rB_addToGroup.Checked;

            tB_NewGroupName.Enabled = rB_CreateNewGroup.Checked;
            cB_GroupIsNSFW.Enabled = rB_CreateNewGroup.Checked;
        }

        #endregion


        private void CheckForFeeds()
        {
            gB_FeedGroup.Enabled = false;

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

                    if (results.Count() > 0)
                    {
                        //select first item
                        lb_FoundFeeds.SelectedIndex = 0;

                        //select fitting group
                        if (cB_Groups.Items!=null && rB_addToGroup.Checked)
                        {
                            int numberOfCBItems = cB_Groups.Items.Count;
                            if (numberOfCBItems > 0)
                            {
                                for(int i =0; i< numberOfCBItems; i++)
                                {
                                    string itemString = cB_Groups.Items[i].ToString();

                                    if (results[0].ToLower().Contains(itemString.ToLower()))
                                    {
                                        cB_Groups.SelectedIndex = i;
                                        break;
                                    }
                                }
                            }
                        }
                    }
                    gB_FeedGroup.Enabled = true;
                }
            }
        }

        
    }
}
