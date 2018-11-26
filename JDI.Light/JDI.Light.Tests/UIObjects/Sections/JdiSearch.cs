using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public sealed class JdiSearch : Search
    {
        [FindBy(Css = ".search>.icon-search")] public new IButton SearchButton;

        [FindBy(Css = ".icon-search.active")] public IButton SearchButtonActive;

        [FindBy(Css = ".search-field input")] public ITextField SearchInput;
    }
}