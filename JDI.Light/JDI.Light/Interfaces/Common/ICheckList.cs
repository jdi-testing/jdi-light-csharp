using System;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface ICheckList : IBaseUIElement, ISetValue<bool>
    {
        void Check(string[] values);
        void Check(int[] indexes);
        string[] GetChecked(Array values);
    }
}