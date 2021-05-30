using JDI.Light.Exceptions;
using NUnit.Framework;
using static JDI.Light.Matchers.Is;
using static JDI.Light.Tests.Enums.Navigation;

namespace JDI.Light.Tests.Tests.Composite
{
    [TestFixture]
    public class MenuTests : TestBase
    {
        [Test]
        public void MultiLevelNegativeTest()
        {
            TestSite.HomePage.Open();
            Assert.Throws<ElementNotFoundException>(() => TestSite.Header.Menu.Select("SERVICE", "A111222"));
        }

        [Test]
        public void SelectTest()
        {
            TestSite.SidebarMenu.Select("Contact form");
            TestSite.ContactFormPage.CheckOpened();
            Assert.AreEqual("Contact form", TestSite.SidebarMenu.Selected());
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.SidebarMenu.Select(MetalsColors);
            TestSite.MetalsColorsPage.CheckOpened();
        }

        [Test]
        public void SelectTestList()
        {
            /* TODO
            leftMenuList.select("Contact form");
            contactFormPage.checkOpened();
            */
        }

        [Test]
        public void GetTestList()
        {
            /* TODO
            HtmlElement item = leftMenuList.get("Contact form");
            item.show();
            item.is ().deselected();
            item.click();
            item.is ().selected();
            contactFormPage.checkOpened();
            */
        }

        [Test]
        public void SelectEnumTestList()
        {
            /* TODO
            leftMenuList.select(MetalsColors);
            metalAndColorsPage.checkOpened();
            */
        }

        [Test]
        public void SelectListTest()
        {
            TestSite.SidebarMenu.Select("Service", "Dates");
            TestSite.Dates.CheckOpened();
        }

        [Test]
        public void SelectEnumListTest()
        {
            TestSite.SidebarMenu.Select(Service, Dates);
            TestSite.Dates.CheckOpened();
        }

        [Test]
        public void RefreshIndexSelectTest()
        {
            TestSite.Header.Menu.Select(2);
            TestSite.Header.Menu.Select(8);
            TestSite.UsersPage.CheckOpened();
        }

        [Test]
        public void RefreshAssertMenuTest()
        {
            TestSite.SidebarMenu.AssertThat.Size(EqualTo(22));
        }

        [Test]
        public void HoverAndClickTest()
        {
            TestSite.SidebarMenu.HoverAndClick("Elements packs", "HTML 5");
            TestSite.Html5Page.CheckOpened();
        }

        [Test]
        public void IsValidationTest()
        {
            TestSite.SidebarMenu.Select("Elements packs", "HTML 5");
            TestSite.SidebarMenu.Is.Selected("HTML 5");
        }

        [Test]
        public void AssertValidationTest()
        {
            TestSite.SidebarMenu.Select("Elements packs", "HTML 5");
            TestSite.SidebarMenu.AssertThat.Selected("HTML 5");
        }
    }
}