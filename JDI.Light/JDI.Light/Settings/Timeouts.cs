namespace JDI.Light.Settings
{
    public class Timeouts
    {
        public readonly int RetryMSec;
        public readonly int WaitElementMSec;
        public readonly int WaitPageLoadMSec;

        public Timeouts()
        {
            RetryMSec = 500;
            WaitElementMSec = 20000;
            WaitPageLoadMSec = 30000;
        }
    }
}