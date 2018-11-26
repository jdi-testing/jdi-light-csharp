using JDI.Core.Attributes;
using JDI.Core.Interfaces.Common;
using JDI.Core.Selenium.Elements.Composite;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class JdiPaginator : Pagination
    {
        [FindBy(Css = "[class=first] a")] public new IButton First;

        [FindBy(Css = "[class=last]  a")] public new IButton Last;

        [FindBy(Css = "[class=next]  a")] public new IButton Next;

        [FindBy(Css = ".uui-pagination li")] public IButton Page;

        [FindBy(Css = "[class=prev]  a")] public IButton Prev;
    }
}