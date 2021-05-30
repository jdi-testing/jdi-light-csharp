using JDI.Light.Tests.Entities;
using JDI.Light.Tests.UIObjects;
using NUnit.Framework;

namespace JDI.Light.Tests.Tests.Core
{
    public class WebDriverFactoryTests : TestBase
    {
        [Test]
        public void CanRecreateKilledDriver()
        {
            Client client = new Client();
            TestSite.HomePage.CheckOpened();
            Jdi.CloseDriver();
            Jdi.KillAllDrivers();
            TestSite.HomePage.Open();
            TestSite.HomePage.Profile.Click();
            TestSite.LoginFormPage.AsForm<Client>().Login(client.DefaultClient);
            TestSite.HomePage.CheckOpened();
        }
    }
}