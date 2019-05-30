using JDI.Light.Attributes;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class JdiSearch : Search
    {
        [FindBy(Css = ".search>.icon-search")]
        public new Button SearchButton;

        [FindBy(Css = ".icon-search.active")]
        public Button SearchButtonActive;

        [FindBy(Css = ".search-field input")]
        public TextField SearchInput;
    }
}