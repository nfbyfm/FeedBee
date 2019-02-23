using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace FeedRead.Utilities
{
    /// <summary>
    /// class for finding out if a internet-connection is available
    /// </summary>
    public class InternetCheck
    {
        private bool internetIsConnected;

        /// <summary>
        /// constructor, runs first check
        /// </summary>
        public InternetCheck()
        {
            internetIsConnected = false;
        }

        /// <summary>
        /// checks for internet-connection (only once)
        /// </summary>
        /// <returns>returns true if connection is avalable</returns>
        public bool ConnectedToInternet()
        {
            if (!internetIsConnected)
            {
                internetIsConnected = ForceCheckConnection();
            }
            return internetIsConnected;
        }


        /// <summary>
        /// checks for internet-connection without considering previous results
        /// </summary>
        /// <returns></returns>
        public bool ForceCheckConnection()
        {
            try
            {
                Ping myPing = new Ping();
                String host = "google.com";
                byte[] buffer = new byte[32];
                int timeout = 1000;
                PingOptions pingOptions = new PingOptions();
                PingReply reply = myPing.Send(host, timeout, buffer, pingOptions);

                return (reply.Status == IPStatus.Success);
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
