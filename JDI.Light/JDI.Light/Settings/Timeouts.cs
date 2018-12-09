namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public int CurrentTimeoutMSec;
        public int RetryMSec = 500;

        public int WaitElementSec = 20;
        public int WaitPageLoadSec = 20;

        public Timeouts()
        {
            CurrentTimeoutMSec = WaitPageLoadSec * 1000;
        }
    }
}