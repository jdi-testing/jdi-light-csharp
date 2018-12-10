namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public int CurrentTimeoutMSec;
        public int RetryMSec = 250;

        public int WaitElementSec = 10;
        public int WaitPageLoadSec = 20;

        public Timeouts()
        {
            CurrentTimeoutMSec = WaitPageLoadSec * 1000;
        }
    }
}