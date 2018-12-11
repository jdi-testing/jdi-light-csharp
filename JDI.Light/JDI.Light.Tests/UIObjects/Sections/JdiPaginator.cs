using JDI.Light.Attributes;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Tests.UIObjects.Sections
{
    public class JdiPaginator : Pagination
    {
        [FindBy(Css = "[class=first] a")]
        public new IButton First;

        [FindBy(Css = "[class=last]  a")]
        public new IButton Last;

        [FindBy(Css = "[class=next]  a")]
        public new IButton Next;

        [FindBy(Css = ".uui-pagination li")]
        public IButton Page;

        [FindBy(Css = "[class=prev]  a")]
        public IButton Prev;
    }
}