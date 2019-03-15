using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class TextList : UIElement
    {
        public TextList(By locator) : base(locator)
        {
        }

        public IList<Label> TextElements
        {
            get
            {
                var res = WebElements.Select(e =>
                {
                    var l = new Label(Locator) { WebElement = e, Parent = Parent};
                    return l;
                }).ToList();
                return res;
            }
        }

        public IList<string> Texts => TextElements.Select(el => el.GetText()).ToList();
    }
}