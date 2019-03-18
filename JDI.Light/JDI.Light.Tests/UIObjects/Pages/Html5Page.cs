using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Pages
{
    public class Html5Page : WebPage
    {
        [FindBy(Css = "#ages")]
        public MultiSelector AgeSelector;
    }
}