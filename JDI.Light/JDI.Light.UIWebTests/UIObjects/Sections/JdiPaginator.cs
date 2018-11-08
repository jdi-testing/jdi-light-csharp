using JDI.Core.Interfaces.Common;
using JDI.Web.Attributes;
using JDI.Web.Selenium.Elements.Composite;

namespace JDI.UIWebTests.UIObjects.Sections
{
    public class JdiPaginator:Pagination
    {
       [FindBy(Css = "[class=next]  a")]
        public IButton Next;

        [FindBy(Css = "[class=prev]  a")]
        public IButton Prev;

        [FindBy(Css = "[class=first] a")]
        public IButton First;

        [FindBy(Css = "[class=last]  a")]
        public IButton Last;

        [FindBy(Css = ".uui-pagination li")]
        public IButton Page;
    }
}
