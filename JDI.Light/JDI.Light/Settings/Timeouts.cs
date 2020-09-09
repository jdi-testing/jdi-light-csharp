using System.Security.Cryptography.X509Certificates;

namespace JDI.Light.Settings
{
    public class Timeouts
    {
        private readonly int RetryMSec;
        private readonly int WaitElementMSec;
        private readonly int WaitPageLoadMSec;

        public Timeouts()
        {
            RetryMSec = 500;
            WaitElementMSec = 20000;
            WaitPageLoadMSec = 30000;

        }
        public int GetRetryMSec()
        {
            return RetryMSec;
        }
        public int GeWaitElementMSec()
        {
            return WaitElementMSec;
        }
        public int GetPageLoadMSec()
        {
            return WaitPageLoadMSec;
        }
    }
}