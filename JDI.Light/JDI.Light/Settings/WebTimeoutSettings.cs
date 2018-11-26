namespace JDI.Light.Settings
{
    public class WebTimeoutSettings
    {
        public int CurrentTimeoutSec;
        public int RetryMSec = 100;

        public int WaitElementSec = 20;
        public int WaitPageLoadSec = 20;

        public WebTimeoutSettings()
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