using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    public class DataList : UIElement, IDataList
    {
        public DataList(By byLocator) : base(byLocator)
        {
        }

        public bool Value { get; set; }
        public void Select(string value)
        {
           
        }

        public void Select(int index)
        {
            throw new System.NotImplementedException();
        }
    }
}