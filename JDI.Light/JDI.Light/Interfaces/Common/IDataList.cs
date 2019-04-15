using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDataList : IBaseUIElement
    {
        void Select(string value);
        void Select(Enum value);
        void Select(int index);
        string Selected();
        List<string> Values();
        List<string> ListEnabled();
        List<string> ListDisabled();
    }
}