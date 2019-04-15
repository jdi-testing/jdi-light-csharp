using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Interfaces.Common
{
    public interface ICheckList : IBaseUIElement
    {
        By CheckListLocator { get; set; }
        void Check(params string[] values);
        void Check(params int[] indexes);
        void Uncheck(params string[] values);
        void Uncheck(params int[] indexes);
        void UncheckAll();
        string[] GetChecked();
    }
}