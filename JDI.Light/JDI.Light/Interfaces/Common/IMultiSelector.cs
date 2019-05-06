using System;
using System.Collections.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IMultiSelector : IBaseUIElement
    {
        void Check(string value, bool checkEnabled = true);
        void Check(int index, bool checkEnabled = true);
        void Check(string[] values, bool checkEnabled = true);
        void Check(Enum[] values, bool checkEnabled = true);
        void Check(int[] values, bool checkEnabled = true);

        void Uncheck(string[] values, bool checkEnabled = true);
        void Uncheck(Enum[] values, bool checkEnabled = true);
        void Uncheck(int[] values, bool checkEnabled = true);

        string Selected();
        List<string> Checked();
    }
}