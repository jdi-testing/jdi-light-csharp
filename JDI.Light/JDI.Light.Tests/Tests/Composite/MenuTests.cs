using JDI.Light.Exceptions;
using JDI.Light.Tests.Enums;
using NUnit.Framework;
using OpenQA.Selenium;

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
            throw new NotFoundException("Test requires JList<HtmlElement> leftMenuList");
            /*
            leftMenuList.select("Contact form");
            contactFormPage.checkOpened();
            */
        }

        [Test]
        public void GetTestList()
        {
            throw new NotFoundException("Test requires JList<HtmlElement> leftMenuList");
            /*
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
            throw new NotFoundException("Test requires JList<HtmlElement> leftMenuList");
            /*
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
    }
}