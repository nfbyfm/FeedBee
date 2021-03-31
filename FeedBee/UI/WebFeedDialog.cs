using FeedBee.Controlling;
using FeedBee.Utilities.FeedSubs;
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
    public partial class WebFeedDialog : Form
    {
        public List<WebPageFeedDef> webPageFeedDefinitionList;

        private Controller parentController;

        public WebFeedDialog(List<WebPageFeedDef> webPageFeedDefList, Controller parentController)
        {
            InitializeComponent();

            this.webPageFeedDefinitionList = webPageFeedDefList;

            this.parentController = parentController;
        }

        private void WebFeedDialog_Load(object sender, EventArgs e)
        {
            UpdateDefListview();
        }

        private void UpdateDefListview()
        {
            if (webPageFeedDefinitionList != null)
            {
                lV_Definitions.Items.Clear();
                ColumnHeader columnHeader1 = new ColumnHeader();
                columnHeader1.Text = "Name";
                /*
                ColumnHeader columnHeader2 = new ColumnHeader();
                columnHeader2.Text = "publishing date";

                ColumnHeader columnHeader3 = new ColumnHeader();
                columnHeader3.Text = "Author";

                ColumnHeader columnHeader4 = new ColumnHeader();
                columnHeader4.Text = "read";
                */

                lV_Definitions.Columns.AddRange(new ColumnHeader[] { columnHeader1 });//, columnHeader2, columnHeader3, columnHeader4 });

                foreach (WebPageFeedDef definition in webPageFeedDefinitionList)
                {
                    ListViewItem listViewItem = new ListViewItem(definition.Name);
                    listViewItem.Tag = definition;

                    lV_Definitions.Items.Add(listViewItem);
                }
                lV_Definitions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
                lV_Definitions.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                tb_DateID.Text = "";
                tB_FeeditemID.Text = "";
                tB_BaseURL.Text = "";
                tB_Name.Text = "";
                tB_TestpageUrl.Text = "";

                rTB_Testresults.Text = "";
            }
            else
            {
                Console.WriteLine("WebpageFeedDefinitions are null");
            }
        }



        private void lV_Definitions_SelectedIndexChanged(object sender, EventArgs e)
        {
            object ob = null;

            if (lV_Definitions.SelectedItems != null)
            {
                if(lV_Definitions.SelectedItems.Count > 0)
                {
                    ob = lV_Definitions.SelectedItems[0].Tag;
                    if(ob.GetType() == typeof(WebPageFeedDef))
                    {
                        WebPageFeedDef def = (WebPageFeedDef)ob;
                        if(def != null)
                        {
                            tb_DateID.Text = def.ClassID_UpdateTime;
                            tB_FeeditemID.Text = def.ClassID_Title;
                            tB_BaseURL.Text = def.BaseURL;
                            tB_Name.Text = def.Name;
                            tB_TestpageUrl.Text = "";
                        }
                    }
                    
                }
                
            }
            
            rTB_Testresults.Text = "";
        }

        private void bSave_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void bCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void bTest_Click(object sender, EventArgs e)
        {
            string result = "";

            WebPageFeedDef newDef = GetWebPageDef();
            string testpage = tB_TestpageUrl.Text;
            bool b_testPage = !string.IsNullOrEmpty(testpage) && !string.IsNullOrWhiteSpace(testpage);

            if (newDef != null && (b_testPage))
            {
                result = parentController.GetDefTestResults(newDef, testpage);
            }
            else
            {
                result = "No valid input for definition and/or testpage given.";
            }
            rTB_Testresults.Text = result;
        }

        private WebPageFeedDef GetWebPageDef()
        {
            WebPageFeedDef result = null;

            string dateID = tb_DateID.Text;
            string feeditemID = tB_FeeditemID.Text;
            string baseURL = tB_BaseURL.Text;
            string name = tB_Name.Text;

            bool b_dateID = !string.IsNullOrEmpty(dateID) && !string.IsNullOrWhiteSpace(dateID);
            bool b_baseURL = !string.IsNullOrEmpty(baseURL) && !string.IsNullOrWhiteSpace(baseURL);
            bool b_feedID = !string.IsNullOrEmpty(feeditemID) && !string.IsNullOrWhiteSpace(feeditemID);
            bool b_Name = !string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name);

            if (b_Name && b_feedID && b_baseURL && b_dateID)
            {
                result = new WebPageFeedDef(name, name.ToLower().Replace(" ", ""), baseURL, feeditemID, dateID);
            }

            return result;
        }

        private void bAdd_Click(object sender, EventArgs e)
        {
            WebPageFeedDef newDef = GetWebPageDef();
            
            if (newDef != null)
            {
                if(webPageFeedDefinitionList == null)
                {
                    webPageFeedDefinitionList = new List<WebPageFeedDef>();
                }

                webPageFeedDefinitionList.Add(newDef);
                UpdateDefListview();
            }
            else
            {
                MessageBox.Show("No valid input for definition and/or testpage given.","Add new Definition",MessageBoxButtons.OK,MessageBoxIcon.Exclamation);
            }

        }

        private void lV_Definitions_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Delete && lV_Definitions.SelectedItems != null)
            {
                if(lV_Definitions.SelectedItems.Count > 0)
                {
                    if (lV_Definitions.SelectedItems[0] != null)
                    {
                        if(MessageBox.Show("Do you really want to remove the selected definition?","delete definition", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            try
                            {
                                this.webPageFeedDefinitionList.RemoveAt(lV_Definitions.SelectedIndices[0]);
                                UpdateDefListview();
                            }
                            catch(Exception ex)
                            {
                                Console.WriteLine("Error while trying to remove the selected webpagefeed-Definition: " + ex.Message);
                            }
                        }
                    }
                }
            }
        }
    }
}
