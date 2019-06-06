using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Tests.UIObjects.PseudoSections;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite.PseudoSiteTests
{
    public class ExtendedSectionTests : TestBase
    {
        private static ExtendedSection ExtendedSection => TestSite.PseudoSitePage.ExtendedSection;

        [TestCaseSource(nameof(_extendedSectionWebElementData))]
        public void CustomSectionWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as UIElement;
            ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionListWebElementData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionListWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as List<IWebElement>;
            //ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_extendedSectionUIWebElementData))]
        public void CustomSectionUIElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as UIElement;
            ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_extendedSectionButtonElementData))]
        public void CustomSectionButtonTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as UIElement;
            ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionWebListData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionWebListTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as List<IWebElement>;
            //ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionListUIElementPublicData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionListUIElementPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as List<IWebElement>;
            //ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionListButtonPublicData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionListButtonPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as List<IWebElement>;
            //ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIListSomeData))] //Uncomment when UIList<T> will be implemented
        public void ExtendedSectionUIListSomeDataTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as List<IWebElement>;
            //ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIListQuestionData))] //Uncomment when UIList<T> will be implemented
        public void ExtendedSectionUIListQuestionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as List<IWebElement>;
            //ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_extendedSectionDropDownData))]
        public void CustomSectionDropDownTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSection) as UIElement;
            ExtendedSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        private static object[] _extendedSectionWebElementData =
        {
            new object[] { nameof(ExtendedSection.WebElementPublic), "By.Id: webElementPublic", "WebElementPublic", null },
            new object[] { nameof(ExtendedSection.WebElementPublicUI), "By.CssSelector: .webElementPublicUI", "WebElementPublicUI", null },
            new object[] { nameof(ExtendedSection.WebElementPublicXPath), "By.XPath: //*[@class='webElementPublicXPath']", "WebElementPublicXPath", null },
            new object[] { nameof(ExtendedSection.WebElementPackage), "By.Id: webElementPackage", "WebElementPackage", null },
            new object[] { nameof(ExtendedSection.WebElementPrivate), "By.Id: webElementPrivate", "WebElementPrivate", null },
            new object[] { nameof(ExtendedSection.WebElementPackageUI), "By.CssSelector: .webElementPackageUI", "WebElementPackageUI", null },
            new object[] { nameof(ExtendedSection.WebElementPackageCss), "By.CssSelector: .webElementPackageCss", "WebElementPackageCss", null }
        };

        private static object[] _extendedSectionListWebElementData =
        {
            new object[] { nameof(ExtendedSection.ListWebElementPublic), "By.Id: listWebElementPublic", "ListWebElementPublic", null },
            new object[] { nameof(ExtendedSection.ListWebElementPublicUI), "By.CssSelector: .listWebElementPublicUI", "ListWebElementPublicUI", null },
            new object[] { nameof(ExtendedSection.ListWebElementPublicXPath), "By.XPath: //*[@class='listWebElementPublicXPath']", "ListWebElementPublicXPath", null },
            new object[] { nameof(ExtendedSection.ListWebElementPackage), "By.Id: listWebElementPackage", "ListWebElementPackage", null },
            new object[] { nameof(ExtendedSection.ListWebElementPrivate), "By.Id: listWebElementPrivate", "ListWebElementPrivate", null },
            new object[] { nameof(ExtendedSection.ListWebElementPackageUI), "By.CssSelector: .listWebElementPackageUI", "ListWebElementPackageUI", null },
            new object[] { nameof(ExtendedSection.ListWebElementPackageCss), "By.CssSelector: .listWebElementPackageCss", "ListWebElementPackageCss", null }
        };

        private static object[] _extendedSectionUIWebElementData =
        {
            new object[] { nameof(ExtendedSection.UIWebElementPublic), "By.Id: uielementPublic", "UIWebElementPublic", null },
            new object[] { nameof(ExtendedSection.UIWebElementPublicUI), "By.CssSelector: .uielementPublicUI", "UIWebElementPublicUI", null },
            new object[] { nameof(ExtendedSection.UIWebElementPublicXPath), "By.XPath: //*[@class='uielementPublicXPath']", "UIWebElementPublicXPath", null },
            new object[] { nameof(ExtendedSection.UIWebElementPackage), "By.Id: uielementPackage", "UIWebElementPackage", null },
            new object[] { nameof(ExtendedSection.UIWebElementPrivate), "By.Id: uielementPrivate", "UIWebElementPrivate", null },
            new object[] { nameof(ExtendedSection.UIWebElementPackageUI), "By.CssSelector: .uielementPackageUI", "UIWebElementPackageUI", null },
            new object[] { nameof(ExtendedSection.UIWebElementPackageCss), "By.CssSelector: .uielementPackageCss", "UIWebElementPackageCss", null }
        };

        private static object[] _extendedSectionButtonElementData =
        {
            new object[] { nameof(ExtendedSection.ButtonPublic), "By.Id: buttonPublic", "ButtonPublic", null },
            new object[] { nameof(ExtendedSection.ButtonPublicUI), "By.CssSelector: .buttonPublicUI", "ButtonPublicUI", null },
            new object[] { nameof(ExtendedSection.ButtonPublicXPath), "By.XPath: //*[@class='buttonPublicXPath']", "ButtonPublicXPath", null },
            new object[] { nameof(ExtendedSection.ButtonPackage), "By.Id: buttonPackage", "ButtonPackage", null },
            new object[] { nameof(ExtendedSection.ButtonPrivate), "By.Id: buttonPrivate", "ButtonPrivate", null },
            new object[] { nameof(ExtendedSection.ButtonPackageUI), "By.CssSelector: .buttonPackageUI", "ButtonPackageUI", null },
            new object[] { nameof(ExtendedSection.ButtonPackageCss), "By.CssSelector: .buttonPackageCss", "ButtonPackageCss", null }
        };

        private static object[] _extendedSectionWebListData =
        {
            new object[] { nameof(ExtendedSection.WebListPublic), "By.Id: webListPublic", "WebListPublic", null },
            new object[] { nameof(ExtendedSection.WebListPublicUI), "By.CssSelector: .webListPublicUI", "WebListPublicUI", null },
            new object[] { nameof(ExtendedSection.WebListPublicXPath), "By.XPath: //*[@class='webListPublicXPath']", "WebListPublicXPath", null },
            new object[] { nameof(ExtendedSection.WebListPackage), "By.Id: webListPackage", "WebListPackage", null },
            new object[] { nameof(ExtendedSection.WebListPrivate), "By.Id: webListPrivate", "WebListPrivate", null },
            new object[] { nameof(ExtendedSection.WebListPackageUI), "By.CssSelector: .webListPackageUI", "WebListPackageUI", null },
            new object[] { nameof(ExtendedSection.WebListPackageCss), "By.CssSelector: .webListPackageCss", "WebListPackageCss", null }
        };

        private static object[] _extendedSectionListUIElementPublicData =
        {
            new object[] { nameof(ExtendedSection.ListUIElementPublic), "By.Id: listUIElementPublic", "ListUIElementPublic", null },
            new object[] { nameof(ExtendedSection.ListUIElementPublicUI), "By.CssSelector: .listUIElementPublicUI", "ListUIElementPublicUI", null },
            new object[] { nameof(ExtendedSection.ListUIElementPublicXPath), "By.XPath: //*[@class='listUIElementPublicXPath']", "ListUIElementPublicXPath", null },
            new object[] { nameof(ExtendedSection.ListUIElementPackage), "By.Id: listUIElementPackage", "ListUIElementPackage", null },
            new object[] { nameof(ExtendedSection.ListUIElementPrivate), "By.Id: listUIElementPrivate", "ListUIElementPrivate", null },
            new object[] { nameof(ExtendedSection.ListUIElementPackageUI), "By.CssSelector: .listUIElementPackageUI", "ListUIElementPackageUI", null },
            new object[] { nameof(ExtendedSection.ListUIElementPackageCss), "By.CssSelector: .listUIElementPackageCss", "ListUIElementPackageCss", null }
        };

        private static object[] _extendedSectionListButtonPublicData =
        {
            new object[] { nameof(ExtendedSection.ListButtonPublic), "By.Id: listButtonPublic", "ListButtonPublic", null },
            new object[] { nameof(ExtendedSection.ListButtonPublicUI), "By.CssSelector: .listButtonPublicUI", "ListButtonPublicUI", null },
            new object[] { nameof(ExtendedSection.ListButtonPublicXPath), "By.XPath: //*[@class='listButtonPublicXPath']", "ListButtonPublicXPath", null },
            new object[] { nameof(ExtendedSection.ListButtonPackage), "By.Id: listButtonPackage", "ListButtonPackage", null },
            new object[] { nameof(ExtendedSection.ListButtonPrivate), "By.Id: listButtonPrivate", "ListButtonPrivate", null },
            new object[] { nameof(ExtendedSection.ListButtonPackageUI), "By.CssSelector: .listButtonPackageUI", "ListButtonPackageUI", null },
            new object[] { nameof(ExtendedSection.ListButtonPackageCss), "By.CssSelector: .listButtonPackageCss", "ListButtonPackageCss", null }
        };

        /*private static object[] _extendedSectionUIListSomeData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(ExtendedSection.UilistSomedataPublic), "By.Id: uilistSomedataPublic", "UilistSomedataPublic", null },
            new object[] { nameof(ExtendedSection.UilistSomedataPublicUI), "By.CssSelector: .uilistSomedataPublicUI", "UilistSomedataPublicUI", null },
            new object[] { nameof(ExtendedSection.UilistSomedataPublicXPath), "By.XPath: //*[@class='uilistSomedataPublicXPath']", "UilistSomedataPublicXPath", null },
            new object[] { nameof(ExtendedSection.UilistSomedataPackage), "By.Id: uilistSomedataPackage", "DroplistPackageUI", null },
            new object[] { nameof(ExtendedSection.UilistSomedataPrivate), "By.Id: uilistSomedataPrivate", "UilistSomedataPrivate", null },
            new object[] { nameof(ExtendedSection.UilistSomedataPackageUI), "By.CssSelector: .uilistSomedataPackageUI", "UilistSomedataPackageUI", null },
            new object[] { nameof(ExtendedSection.UilistSomedataPackageCss), "By.CssSelector: .uilistSomedataPackageCss", "UilistSomedataPackageCss", null }
        };*/

        /*private static object[] _extendedSectionUIListQuestionData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(ExtendedSection.UilistQuestionPublic), "By.Id: uilistQuestionPublic", "UilistQuestionPublic", null },
            new object[] { nameof(ExtendedSection.UilistQuestionPublicUI), "By.CssSelector: .uilistQuestionPublicUI", "UilistQuestionPublicUI", null },
            new object[] { nameof(ExtendedSection.UilistQuestionPublicXPath), "By.XPath: //*[@class='uilistQuestionPublicXPath']", "UilistQuestionPublicXPath", null },
            new object[] { nameof(ExtendedSection.UilistQuestionPackage), "By.Id: uilistQuestionPackage", "UilistQuestionPackage", null },
            new object[] { nameof(ExtendedSection.UilistQuestionPrivate), "By.Id: uilistQuestionPrivate", "UilistQuestionPrivate", null },
            new object[] { nameof(ExtendedSection.UilistQuestionPackageUI), "By.CssSelector: .uilistQuestionPackageUI", "UilistQuestionPackageUI", null },
            new object[] { nameof(ExtendedSection.UilistQuestionPackageCss), "By.CssSelector: .uilistQuestionPackageCss", "UilistQuestionPackageCss", null }
        };*/

        private static object[] _extendedSectionDropDownData =
        {
            new object[] { nameof(ExtendedSection.DropListPackage), "By.CssSelector: div[ui=droplistPackage]", "DropListPackage", null },
            new object[] { nameof(ExtendedSection.DroplistPublic), "By.CssSelector: div[ui=droplistPublic]", "DroplistPublic", null },
            new object[] { nameof(ExtendedSection.DroplistPrivate), "By.CssSelector: div[ui=droplistPrivate]", "DroplistPrivate", null },
            new object[] { nameof(ExtendedSection.DroplistPackageUI), "By.CssSelector: .droplistPackageUI", "DroplistPackageUI", null },
            new object[] { nameof(ExtendedSection.DroplistPublicUI), "By.CssSelector: .droplistPublicUI", "DroplistPublicUI", null },
        };
    }
}
