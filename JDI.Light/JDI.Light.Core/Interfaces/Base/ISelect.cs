using JDI.Core.Interfaces.Common;

namespace JDI.Core.Interfaces.Base
{
    public interface ISelect: IClickable, IText, ISetValue
    {
        /**
     * Selects WebElement. Similar to click()
     */
       //TODO [JDIAction]
        void Select();

        /**
         * return - Checks is WebElement selected
         */
        //TODO [JDIAction]
        bool Selected { get; }
    }
}
