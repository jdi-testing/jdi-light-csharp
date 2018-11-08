using JDI.Core.Interfaces.Base;

namespace JDI.Core.Interfaces.Common
{
    public interface ICheckBox : IClickable, ISetValue
    {
        /**
         * Set checkbox checked
         */
        //TODO[JDIAction]
        void Check();

        /**
         * Set checkbox unchecked
         */
        //TODO[JDIAction]
        void Uncheck();

        /**
         * return: Verify is checkbox checked
         */
        //TODO[JDIAction]
        bool IsChecked();
    }
}
