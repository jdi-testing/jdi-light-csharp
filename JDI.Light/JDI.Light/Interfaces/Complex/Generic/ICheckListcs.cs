using System.Collections.Generic;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Complex.Generic
{
    public interface ICheckList<TCheckBox> : IGetValue<List<string>>, ISelector where TCheckBox : ICheckBox
    {
        List<TCheckBox> CheckBoxes { get; }
        By CheckListLocator { get; set; }
        By LabelLocator { get; set; }
        void Check(bool checkEnabled = true, params string[] values);
        void Check(bool checkEnabled = true, params int[] indexes);
        void Uncheck(bool checkEnabled = true, params string[] values);
        void Uncheck(bool checkEnabled = true, params int[] indexes);
        void Select(bool checkEnabled = true, params string[] values);
        void Select(bool checkEnabled = true, params int[] indexes);
        void UncheckAll(bool checkEnabled = true);
        void CheckAll(bool checkEnabled = true);
        bool IsChecked(string value);
        bool IsChecked(int index);
        bool IsDisabled(string value);
        bool IsDisabled(int index);
    }
}