using JDI.Core.Interfaces.Common;

namespace JDI.Web.Selenium.Elements.Complex.Table
{
    public class FilterDsl
    {
        public static string TextOf(IText textElement)
        {
            return textElement.GetText;
        }
    }
}
