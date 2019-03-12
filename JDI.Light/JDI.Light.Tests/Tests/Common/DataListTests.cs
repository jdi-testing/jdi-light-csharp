using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class DataListTests : TestBase
    {

        [Test]
        public void DataListTest()
        {
            TestSite.Html5Page.Open();
            TestSite.Html5Page.IceCreamDataList.DropDownItemLocator = By.XPath("//option");
            TestSite.Html5Page.IceCreamDataList.Select("Ice cream");
        }

    }
}