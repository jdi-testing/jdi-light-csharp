namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public int CurrentTimeoutMSec;
        public int RetryMSec = 500;

        public int WaitElementSec = 5;
        public int WaitPageLoadSec = 10;

        public Timeouts()
        {
            CurrentTimeoutMSec = WaitPageLoadSec * 1000;
        }
    }
}