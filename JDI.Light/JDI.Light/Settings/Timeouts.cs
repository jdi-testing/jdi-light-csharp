namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public int CurrentTimeoutSec;
        public int RetryMSec = 100;

        public int WaitElementSec = 20;
        public int WaitPageLoadSec = 20;

        public Timeouts()
        {
            SetCurrentTimeoutSec(WaitPageLoadSec);
        }

        public void SetCurrentTimeoutSec(int timeoutSec)
        {
            CurrentTimeoutSec = timeoutSec;
        }

        public void DropTimeouts()
        {
            SetCurrentTimeoutSec(WaitPageLoadSec);
        }
    }
}