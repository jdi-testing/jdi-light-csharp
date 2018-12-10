using JDI.Light.Interfaces.Common;

namespace JDI.Light.Elements.Common
{
    public class DatePicker : TextField, IDatePicker
    {
        public DatePicker() : this(null)
        {
        }

        public DatePicker(By byLocator = null)
            : base(byLocator)
        {
        }
    }
}