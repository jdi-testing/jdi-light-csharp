namespace JDI.Light.Settings
{
    public class HighlightSettings
    {
        public HighlightSettings(string bgColor = "yellow", string frameColor = "red", int timeoutInSec = 2)
        {
            BgColor = bgColor;
            FrameColor = frameColor;
            TimeoutInSec = timeoutInSec;
        }

        public string BgColor { get; private set; }

        public string FrameColor { get; private set; }

        public int TimeoutInSec { get; private set; }

        public HighlightSettings SetBgColor(string bgColor)
        {
            BgColor = bgColor;
            return this;
        }

        public HighlightSettings SetFrameColor(string frameColor)
        {
            FrameColor = frameColor;
            return this;
        }

        public HighlightSettings SetTimeoutInSec(int timeoutInSec)
        {
            TimeoutInSec = timeoutInSec;
            return this;
        }
    }
}