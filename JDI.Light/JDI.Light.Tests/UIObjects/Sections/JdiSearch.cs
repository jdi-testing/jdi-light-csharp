using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public sealed class JdiSearch : Search
    {
        [FindBy(Css = ".search>.icon-search")] public new IButton SearchButton;

        [FindBy(Css = ".icon-search.active")] public IButton SearchButtonActive;

        [FindBy(Css = ".search-field input")] public ITextField SearchInput;
    }
}