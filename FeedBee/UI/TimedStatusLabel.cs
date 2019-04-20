using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace FeedBee.UI
{
    class TimedStatusLabel : System.Windows.Forms.ToolStripStatusLabel
    {
        private static Timer timer;


        public void setTimedText(String text, double timeSpanMS)
        {
            timer = new Timer();

            timer.Elapsed += (sender, e) => CallBack(sender, e, this);
            timer.Interval = timeSpanMS;

            timer.Enabled = true;
            timer.Start();
            try
            {
                base.Text = text;
            }
            catch(Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private static void CallBack(object source, ElapsedEventArgs e, TimedStatusLabel stLabel)
        {
            timer.Enabled = false;
            timer.Stop();

            stLabel.Text = "...";
        }



    }
}
