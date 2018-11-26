using System;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Interfaces.Complex
{
    public interface IDropDown : IDropDown<IConvertible>
    {
    }

    public interface IDropDown<in TEnum> : ISelector<TEnum>, IText, IClickable
        where TEnum : IConvertible
    {
        /**
     * Expanding DropDown
     */
        //TODO[JDIAction]
        void Expand();

        /**
         * Closing DropDown
         */
        //TODO[JDIAction]
        void Close();
    }
}