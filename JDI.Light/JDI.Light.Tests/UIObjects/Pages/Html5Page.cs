using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class Html5Page : WebPage
    {
        [FindBy(Id = "blue-button")]
        public IButton BlueButton { get; set; }

        [FindBy(Css = "h1")]
        public ILabel JdiLabel { get; set; }
    }
}