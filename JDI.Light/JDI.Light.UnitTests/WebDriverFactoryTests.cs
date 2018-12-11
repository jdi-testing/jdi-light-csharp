using JDI.Light.Common;
using NUnit.Framework;

namespace JDI.Light.UnitTests
{
    [TestFixture]
    public class WebDriverFactoryTests
    {
        [Test]
        public void DriverIsNotNull()
        {
            var wd = new WebDriverFactory();
            wd.SetDefaultWebDriver(new MockWebDriver());
            Assert.IsTrue(wd.DefaultWebDriver is MockWebDriver);
        }
    }
}