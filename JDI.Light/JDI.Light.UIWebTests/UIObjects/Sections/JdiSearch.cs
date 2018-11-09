using JDI.Core.Interfaces.Common;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Sections
{
    public sealed class JdiSearch:Search
    {        
        [FindBy(Css = ".search-field input")]
        public ITextField SearchInput;

        [FindBy(Css = ".search>.icon-search")]
        public new IButton SearchButton;

        [FindBy(Css = ".icon-search.active")]
        public IButton SearchButtonActive;
    }
}
