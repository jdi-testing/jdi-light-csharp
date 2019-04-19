using JDI.Light.Elements.Complex.Generic;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Complex;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex
{
    public class CheckList : CheckList<ICheckBox>, ICheckList
    {
        public CheckList(By byLocator) : base(byLocator)
        {
        }
    }
}