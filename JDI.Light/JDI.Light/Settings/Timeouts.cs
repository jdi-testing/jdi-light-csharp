namespace JDI.Light.Settings
{
    public class Timeouts
    {
        private int RetryMSec { get; }
        private int WaitElementMSec { get; }
        private int WaitPageLoadMSec { get; }

        public Timeouts()
        {
            RetryMSec = 500;
            WaitElementMSec = 20000;
            WaitPageLoadMSec = 30000;
        }
    }
}