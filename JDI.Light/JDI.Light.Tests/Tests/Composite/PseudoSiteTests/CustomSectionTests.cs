using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Tests.UIObjects.PseudoSections;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite.PseudoSiteTests
{
    public class CustomSectionTests : TestBase
    {
        private static CustomSection CustomSection => TestSite.PseudoSitePage.CustomSection;

        [TestCaseSource(nameof(_customSectionWebElement))]
        public void CustomSectionWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as UIElement;
            CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionListWebElement))] //Fix initialization of List<IWebElement> element
        public void CustomSectionListWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionUIWebElement))]
        public void CustomSectionUIElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as UIElement;
            CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        private static object[] _customSectionWebElement =
        {
            new object[] { nameof(CustomSection.WebElementPublic), "By.Id: webElementPublic", "WebElementPublic", null },
            new object[] { nameof(CustomSection.WebElementPublicUI), "By.CssSelector: .webElementPublicUI", "WebElementPublicUI", null },
            new object[] { nameof(CustomSection.WebElementPublicXPath), "By.XPath: //*[@class='webElementPublicXPath']", "WebElementPublicXPath", null },
            new object[] { nameof(CustomSection.WebElementPackage), "By.Id: webElementPackage", "WebElementPackage", null },
            new object[] { nameof(CustomSection.WebElementPrivate), "By.Id: webElementPrivate", "WebElementPrivate", null },
            new object[] { nameof(CustomSection.WebElementPackageUI), "By.CssSelector: .webElementPackageUI", "WebElementPackageUI", null },
            new object[] { nameof(CustomSection.WebElementPackageCss), "By.CssSelector: .webElementPackageCss", "WebElementPackageCss", null }
        };

        private static object[] _customSectionListWebElement =
        {
            new object[] { nameof(CustomSection.ListWebElementPublic), "By.Id: listWebElementPublic", "ListWebElementPublic", null },
            new object[] { nameof(CustomSection.ListWebElementPublicUI), "By.CssSelector: .listWebElementPublicUI", "ListWebElementPublicUI", null },
            new object[] { nameof(CustomSection.ListWebElementPublicXPath), "By.XPath: //*[@class='listWebElementPublicXPath']", "ListWebElementPublicXPath", null },
            new object[] { nameof(CustomSection.ListWebElementPackage), "By.Id: listWebElementPackage", "ListWebElementPackage", null },
            new object[] { nameof(CustomSection.ListWebElementPrivate), "By.Id: listWebElementPrivate", "ListWebElementPrivate", null },
            new object[] { nameof(CustomSection.ListWebElementPackageUI), "By.CssSelector: .listWebElementPackageUI", "ListWebElementPackageUI", null },
            new object[] { nameof(CustomSection.ListWebElementPackageCss), "By.CssSelector: .listWebElementPackageCss", "ListWebElementPackageCss", null }
        };

        private static object[] _customSectionUIWebElement =
        {
            new object[] { nameof(CustomSection.UIWebElementPublic), "By.Id: uielementPublic", "UIWebElementPublic", null },
            new object[] { nameof(CustomSection.UIWebElementPublicUI), "By.CssSelector: .uielementPublicUI", "UIWebElementPublicUI", null },
            new object[] { nameof(CustomSection.UIWebElementPublicXPath), "By.XPath: //*[@class='uielementPublicXPath']", "UIWebElementPublicXPath", null },
            new object[] { nameof(CustomSection.UIWebElementPackage), "By.Id: uielementPackage", "UIWebElementPackage", null },
            new object[] { nameof(CustomSection.UIWebElementPrivate), "By.Id: uielementPrivate", "UIWebElementPrivate", null },
            new object[] { nameof(CustomSection.UIWebElementPackageUI), "By.CssSelector: .uielementPackageUI", "UIWebElementPackageUI", null },
            new object[] { nameof(CustomSection.UIWebElementPackageCss), "By.CssSelector: .uielementPackageCss", "UIWebElementPackageCss", null }
        };
    }
}
