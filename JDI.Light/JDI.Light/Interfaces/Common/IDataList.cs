using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDataList : IBaseUIElement
    {
        void Select(string value, bool checkEnabled = true);
        void Select(Enum value, bool checkEnabled = true);
        void Select(int index, bool checkEnabled = true);
        string Selected();
        List<string> Values();
    }
}