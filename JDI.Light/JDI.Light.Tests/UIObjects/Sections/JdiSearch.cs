using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class JdiSearch : Search
    {
        [FindBy(Css = ".search>.icon-search")]
        public new Button SearchButton { get; set; }

        [FindBy(Css = ".icon-search.active")]
        public Button SearchButtonActive { get; set; }

        [FindBy(Css = ".search-field input")]
        public TextField SearchInput { get; set; }
    }
}