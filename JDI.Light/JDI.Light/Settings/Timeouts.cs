namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public int RetryMSec { get; }
        public int WaitElementMSec { get; }
        public int WaitPageLoadMSec { get; }

        public Timeouts()
        {
            RetryMSec = 500;
            WaitElementMSec = 20000;
            WaitPageLoadMSec = 30000;
        }
    }
}
