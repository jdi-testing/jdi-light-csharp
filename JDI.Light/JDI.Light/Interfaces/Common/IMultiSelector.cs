using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IMultiSelector : IBaseUIElement
    {
        void Check(string value);
        void Check(int index);
        void Check(string[] values);
        void Check(Enum[] values);
        void Check(int[] values);

        void Uncheck(string[] values);
        void Uncheck(Enum[] values);
        void Uncheck(int[] values);

        string Selected();
        List<string> Checked();
    }
}