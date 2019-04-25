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
        void Check(params string[] values);
        void Check(params int[] indexes);
        void Uncheck(params string[] values);
        void Uncheck(params int[] indexes);
        void Select(params string[] values);
        void Select(params int[] indexes);
        void UncheckAll();
        void CheckAll();
        bool IsChecked(string value);
        bool IsChecked(int index);
        bool IsDisabled(string value);
        bool IsDisabled(int index);
    }
}