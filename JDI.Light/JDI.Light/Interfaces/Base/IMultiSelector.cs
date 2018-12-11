using System;
using System.Collections.Generic;

namespace JDI.Light.Interfaces.Base
{
    public interface IMultiSelector<TEnum> : IBaseElement, ISetValue<TEnum[]>
        where TEnum : IConvertible
    {
        IList<TEnum> Options { get; }
        IList<TEnum> Names { get; }
        IList<TEnum> Values { get; }
        void Select(params TEnum[] names);
        void Check(params TEnum[] names);
        void Uncheck(params TEnum[] names);
        IList<TEnum> AreSelected();
        void WaitSelected(params TEnum[] names);
        IList<TEnum> AreDeselected();
        void WaitDeselected(params TEnum[] names);
        void CheckAll();
        void SelectAll();
        void Clear();
        void UncheckAll();
    }
}