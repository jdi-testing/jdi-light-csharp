using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Elements.Common
{
    public class Label : ClickableText, ILabel
    {
        public Label() : this(null)
        {
        }

        public Label(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}