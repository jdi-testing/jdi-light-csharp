using System;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class FrameAttribute : Attribute
    {
        public By FrameLocator { get; private set; }

        public FrameAttribute()
        {
        }

        public FrameAttribute(string frameId)
        {
            FrameLocator = By.Id(frameId);
        }

        public string Id
        {
            set => FrameLocator = By.Id(value);
            get => "";
        }

        public string Name
        {
            set => FrameLocator = By.Name(value);
            get => "";
        }

        public string Class
        {
            set => FrameLocator = By.ClassName(value);
            get => "";
        }

        public string Css
        {
            set => FrameLocator = By.CssSelector(value);
            get => "";
        }

        public string XPath
        {
            set => FrameLocator = By.XPath(value);
            get => "";
        }

        public string Tag
        {
            set => FrameLocator = By.TagName(value);
            get => "";
        }

        public string LinkText
        {
            set => FrameLocator = By.LinkText(value);
            get => "";
        }

        public string PartialLinkText
        {
            set => FrameLocator = By.PartialLinkText(value);
            get => "";
        }
    }
}