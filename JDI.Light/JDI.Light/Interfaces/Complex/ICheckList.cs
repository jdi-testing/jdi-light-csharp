using System.Collections.Generic;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Complex
{
    public interface ICheckList : IBaseUIElement, IGetValue<List<string>>
    {
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
        string[] Checked();
    }
}