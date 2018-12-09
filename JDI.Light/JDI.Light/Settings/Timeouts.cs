namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public int CurrentTimeoutMSec;
        public int RetryMSec = 500;

        public int WaitElementSec = 40;
        public int WaitPageLoadSec = 120;

        public Timeouts()
        {
            CurrentTimeoutMSec = WaitPageLoadSec * 1000;
        }
    }
}