using JDI.Light.Tests.Entities;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Core
{
    public class WebDriverFactoryTests : TestBase
    {
        [Test]
        public void CanRecreateKilledDriver()
        {
            TestSite.HomePage.CheckOpened();
            Jdi.CloseDriver();
            Jdi.KillAllDrivers();
            TestSite.HomePage.Open();
            TestSite.HomePage.Profile.Click();
            TestSite.HomePage.LoginForm.Submit(User.DefaultUser);
            TestSite.HomePage.CheckOpened();
        }
    }
}