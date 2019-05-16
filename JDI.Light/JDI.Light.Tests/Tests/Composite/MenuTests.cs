using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;

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
        }

        [Test]
        public void SelectEnumTest()
        {
            TestSite.SidebarMenu.Select(Navigation.MetalsColors);
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
            TestSite.SidebarMenu.Select(Navigation.Service, Navigation.Dates);
            TestSite.Dates.CheckOpened();
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