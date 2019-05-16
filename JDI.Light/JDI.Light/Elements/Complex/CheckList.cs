using JDI.Light.Elements.Complex.Generic;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;
using ICheckList = JDI.Light.Interfaces.Complex.ICheckList;

namespace JDI.Light.Elements.Complex
{
    public class CheckList : CheckList<ICheckBox>, ICheckList
    {
        public CheckList(By byLocator) : base(byLocator)
        {
        }
    }
}