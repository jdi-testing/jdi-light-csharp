using System;
using JDI.Light.Tools;
using OpenQA.Selenium;
using static JDI.Light.Settings.WebSettings;

namespace JDI.Light.Elements.Base
{
    public class JDIBase : UIElement
    {
        protected JDIBase(By byLocator) : base(byLocator)
        {
        }

        protected CacheValue<Func<IWebElement, bool>> SearchRule =
            new CacheValue<Func<IWebElement, bool>>(() => SearchCondition);

        public T SetSearchRule<T>(Func<IWebElement, bool> rule) where T : JDIBase
        {
            SearchRule.SetForce(rule);
            return (T) this;
        }

        public T NoValidation<T>() where T : JDIBase
        {
            return SetSearchRule<T>(AnyElement);
        }

        public T OnlyVisible<T>() where T : JDIBase
        {
            return SetSearchRule<T>(VisibleElement);
        }

        public T OnlyEnabled<T>() where T : JDIBase
        {
            return SetSearchRule<T>(EnabledElement);
        }
    }
}
