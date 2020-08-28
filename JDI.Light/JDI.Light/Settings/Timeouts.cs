using System.Security.Cryptography.X509Certificates;

namespace JDI.Light.Settings
{
    public class Timeouts
    {
        private int RetryMSec;
        private int WaitElementMSec;
        private int WaitPageLoadMSec;

        public Timeouts()
        {
            RetryMSec = 500;
            WaitElementMSec = 20000;
            WaitPageLoadMSec = 30000;

        }
        public int getRetryMSec()
        {
            return RetryMSec;
        }
        public int geWaitElementMSec()
        {
            return WaitElementMSec;
        }
        public int getPageLoadMSec()
        {
            return WaitPageLoadMSec;
        }
    }
}