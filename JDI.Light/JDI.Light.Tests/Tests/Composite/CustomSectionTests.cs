using JDI.Light.Elements.Base;
using JDI.Light.Tests.UIObjects.Sections;
using NUnit.Framework;
using JDI.Light.Extensions;

namespace JDI.Light.Tests.Tests.Composite
{
    public class CustomSectionTests : TestBase
    {
        public static Contact ContactSection => TestSite.ContactFormPage.ContactSection;
        public static Footer FooterSection => TestSite.Footer;
        public static Header HeaderSection => TestSite.Header;
        public static JdiSearch JdiSearchSection => TestSite.JdiSearch;
        public static Summary SummarySection => TestSite.MetalsColorsPage.SummaryBlock;

        [SetUp]
        public void SetUp()
        {
            Jdi.Logger.Info("Navigating to Contact page.");
            TestSite.ContactFormPage.Open();
            TestSite.ContactFormPage.CheckTitle();
            Jdi.Logger.Info("Setup method finished");
            Jdi.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [TestCaseSource(nameof(ContactSectionCases))]
        public void CustomContactSectionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            ContactSection.CheckInitializedElement(ContactSection.GetType().GetField(htmlElementToCheckName).GetValue(ContactSection) as UIElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [Test]
        public void CustomFooterSectionTest()
        {
            FooterSection.CheckInitializedElement(FooterSection.AboutLink, "By.PartialLinkText: About", "AboutLink", null);            
        }

        [TestCaseSource(nameof(HeaderSectionCases))]
        public void CustomHeaderSectionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            HeaderSection.CheckInitializedElement(HeaderSection.GetType().GetField(htmlElementToCheckName).GetValue(HeaderSection) as UIElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(JdiSearchSectionCases))]
        public void CustomJdiSearchSectionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            JdiSearchSection.CheckInitializedElement(JdiSearchSection.GetType().GetField(htmlElementToCheckName).GetValue(JdiSearchSection) as UIElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [Test]
        public void CustomSummarySectionTest()
        {
            TestSite.MetalsColorsPage.Open();
            SummarySection.CheckInitializedElement(SummarySection.Calculate, "By.Id: calculate-button", "Calculate", null);
        }

        public static object[] ContactSectionCases =
        {
            new object[] { nameof(ContactSection.DescriptionField), "By.CssSelector: textarea#description", "Description", null },
            new object[] { nameof(ContactSection.FirstRoller), "By.XPath: .//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=1]", "FirstRoller", null },
            new object[] { nameof(ContactSection.LastNameField), "By.CssSelector: input#last-name", "Last Name", null },
            new object[] { nameof(ContactSection.NameField), "By.CssSelector: input#name", "First Name", null },
            new object[] { nameof(ContactSection.SecondRoller), "By.XPath: .//a[@class='ui-slider-handle ui-state-default ui-corner-all' and position()=2]", "SecondRoller", null },
            new object[] { nameof(ContactSection.SubmitButton), "By.XPath: //button[@type='submit' and contains(., 'Submit')]", "SubmitButton", null },
        };

        public static object[] HeaderSectionCases =
        {
            new object[] { nameof(HeaderSection.Image), "By.XPath: //img[@src=\"label/Logo_Epam_Color.svg\"]", "Image", null },
            new object[] { nameof(HeaderSection.Menu), "By.CssSelector: ul.uui-navigation.nav", "Menu", null },
            new object[] { nameof(HeaderSection.UserIcon), "By.CssSelector: #user-icon", "UserIcon", null },
            new object[] { nameof(HeaderSection.Search), "By.CssSelector: input#last-name", "Search", "By.Id: search" }
        };

        public static object[] JdiSearchSectionCases =
        {
            new object[] { nameof(JdiSearchSection.SearchButton), "By.CssSelector: .search>.icon-search", "SearchButton", null },
            new object[] { nameof(JdiSearchSection.SearchButtonActive), "By.CssSelector: .icon-search.active", "SearchButtonActive", null },
            new object[] { nameof(JdiSearchSection.SearchInput), "By.CssSelector: .search-field input", "SearchInput", null },
        };
    }
}
