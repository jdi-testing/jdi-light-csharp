using JDI.Light.Elements.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using System.Collections.Generic;
using JDI.Light.Extensions;
using JDI.Light.Tests.UIObjects.Sections.PseudoSections;

namespace JDI.Light.Tests.Tests.Composite.PseudoSiteTests
{
    public class ExtendedSectionUITests : TestBase
    {
        private static ExtendedSection ExtendedSectionUI => TestSite.PseudoSitePage.ExtendedSectionUI;

        [TestCaseSource(nameof(_extendedSectionUIWebElementData))]
        public void ExtendedSectionUIWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as UIElement;
            ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIListWebElementData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionUIListWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as List<IWebElement>;
            //ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_extendedSectionUiuiWebElementData))]
        public void ExtendedSectionUiuiElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as UIElement;
            ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_extendedSectionUIButtonElementData))]
        public void ExtendedSectionUIButtonTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as UIElement;
            ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIWebListData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionUIWebListTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as List<IWebElement>;
            //ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIListUIElementPublicData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionUIListUIElementPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as List<IWebElement>;
            //ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIListButtonPublicData))] //Fix initialization of List<IWebElement> element
        public void ExtendedSectionUIListButtonPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as List<IWebElement>;
            //ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIUIListSomeData))] //Uncomment when UIList<T> will be implemented
        public void ExtendedSectionUiuiListSomeDataTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as List<IWebElement>;
            //ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_extendedSectionUIUIListQuestionData))] //Uncomment when UIList<T> will be implemented
        public void ExtendedSectionUiuiListQuestionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as List<IWebElement>;
            //ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_extendedSectionUIDropDownData))]
        public void ExtendedSectionUIDropDownTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                ExtendedSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(ExtendedSectionUI) as UIElement;
            ExtendedSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        private static object[] _extendedSectionUIWebElementData =
        {
            new object[] { nameof(ExtendedSectionUI.WebElementPublic), "By.Id: webElementPublic", "WebElementPublic", null },
            new object[] { nameof(ExtendedSectionUI.WebElementPublicUI), "By.CssSelector: .webElementPublicUI", "WebElementPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.WebElementPublicXPath), "By.XPath: //*[@class='webElementPublicXPath']", "WebElementPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.WebElementPackage), "By.Id: webElementPackage", "WebElementPackage", null },
            new object[] { nameof(ExtendedSectionUI.WebElementPrivate), "By.Id: webElementPrivate", "WebElementPrivate", null },
            new object[] { nameof(ExtendedSectionUI.WebElementPackageUI), "By.CssSelector: .webElementPackageUI", "WebElementPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.WebElementPackageCss), "By.CssSelector: .webElementPackageCss", "WebElementPackageCss", null }
        };

        private static object[] _extendedSectionUIListWebElementData =
        {
            new object[] { nameof(ExtendedSectionUI.ListWebElementPublic), "By.Id: listWebElementPublic", "ListWebElementPublic", null },
            new object[] { nameof(ExtendedSectionUI.ListWebElementPublicUI), "By.CssSelector: .listWebElementPublicUI", "ListWebElementPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.ListWebElementPublicXPath), "By.XPath: //*[@class='listWebElementPublicXPath']", "ListWebElementPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.ListWebElementPackage), "By.Id: listWebElementPackage", "ListWebElementPackage", null },
            new object[] { nameof(ExtendedSectionUI.ListWebElementPrivate), "By.Id: listWebElementPrivate", "ListWebElementPrivate", null },
            new object[] { nameof(ExtendedSectionUI.ListWebElementPackageUI), "By.CssSelector: .listWebElementPackageUI", "ListWebElementPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.ListWebElementPackageCss), "By.CssSelector: .listWebElementPackageCss", "ListWebElementPackageCss", null }
        };

        private static object[] _extendedSectionUiuiWebElementData =
        {
            new object[] { nameof(ExtendedSectionUI.UIWebElementPublic), "By.Id: uielementPublic", "UIWebElementPublic", null },
            new object[] { nameof(ExtendedSectionUI.UIWebElementPublicUI), "By.CssSelector: .uielementPublicUI", "UIWebElementPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.UIWebElementPublicXPath), "By.XPath: //*[@class='uielementPublicXPath']", "UIWebElementPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.UIWebElementPackage), "By.Id: uielementPackage", "UIWebElementPackage", null },
            new object[] { nameof(ExtendedSectionUI.UIWebElementPrivate), "By.Id: uielementPrivate", "UIWebElementPrivate", null },
            new object[] { nameof(ExtendedSectionUI.UIWebElementPackageUI), "By.CssSelector: .uielementPackageUI", "UIWebElementPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.UIWebElementPackageCss), "By.CssSelector: .uielementPackageCss", "UIWebElementPackageCss", null }
        };

        private static object[] _extendedSectionUIButtonElementData =
        {
            new object[] { nameof(ExtendedSectionUI.ButtonPublic), "By.Id: buttonPublic", "ButtonPublic", null },
            new object[] { nameof(ExtendedSectionUI.ButtonPublicUI), "By.CssSelector: .buttonPublicUI", "ButtonPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.ButtonPublicXPath), "By.XPath: //*[@class='buttonPublicXPath']", "ButtonPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.ButtonPackage), "By.Id: buttonPackage", "ButtonPackage", null },
            new object[] { nameof(ExtendedSectionUI.ButtonPrivate), "By.Id: buttonPrivate", "ButtonPrivate", null },
            new object[] { nameof(ExtendedSectionUI.ButtonPackageUI), "By.CssSelector: .buttonPackageUI", "ButtonPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.ButtonPackageCss), "By.CssSelector: .buttonPackageCss", "ButtonPackageCss", null }
        };

        private static object[] _extendedSectionUIWebListData =
        {
            new object[] { nameof(ExtendedSectionUI.WebListPublic), "By.Id: webListPublic", "WebListPublic", null },
            new object[] { nameof(ExtendedSectionUI.WebListPublicUI), "By.CssSelector: .webListPublicUI", "WebListPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.WebListPublicXPath), "By.XPath: //*[@class='webListPublicXPath']", "WebListPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.WebListPackage), "By.Id: webListPackage", "WebListPackage", null },
            new object[] { nameof(ExtendedSectionUI.WebListPrivate), "By.Id: webListPrivate", "WebListPrivate", null },
            new object[] { nameof(ExtendedSectionUI.WebListPackageUI), "By.CssSelector: .webListPackageUI", "WebListPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.WebListPackageCss), "By.CssSelector: .webListPackageCss", "WebListPackageCss", null }
        };

        private static object[] _extendedSectionUIListUIElementPublicData =
        {
            new object[] { nameof(ExtendedSectionUI.ListUIElementPublic), "By.Id: listUIElementPublic", "ListUIElementPublic", null },
            new object[] { nameof(ExtendedSectionUI.ListUIElementPublicUI), "By.CssSelector: .listUIElementPublicUI", "ListUIElementPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.ListUIElementPublicXPath), "By.XPath: //*[@class='listUIElementPublicXPath']", "ListUIElementPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.ListUIElementPackage), "By.Id: listUIElementPackage", "ListUIElementPackage", null },
            new object[] { nameof(ExtendedSectionUI.ListUIElementPrivate), "By.Id: listUIElementPrivate", "ListUIElementPrivate", null },
            new object[] { nameof(ExtendedSectionUI.ListUIElementPackageUI), "By.CssSelector: .listUIElementPackageUI", "ListUIElementPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.ListUIElementPackageCss), "By.CssSelector: .listUIElementPackageCss", "ListUIElementPackageCss", null }
        };

        private static object[] _extendedSectionUIListButtonPublicData =
        {
            new object[] { nameof(ExtendedSectionUI.ListButtonPublic), "By.Id: listButtonPublic", "ListButtonPublic", null },
            new object[] { nameof(ExtendedSectionUI.ListButtonPublicUI), "By.CssSelector: .listButtonPublicUI", "ListButtonPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.ListButtonPublicXPath), "By.XPath: //*[@class='listButtonPublicXPath']", "ListButtonPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.ListButtonPackage), "By.Id: listButtonPackage", "ListButtonPackage", null },
            new object[] { nameof(ExtendedSectionUI.ListButtonPrivate), "By.Id: listButtonPrivate", "ListButtonPrivate", null },
            new object[] { nameof(ExtendedSectionUI.ListButtonPackageUI), "By.CssSelector: .listButtonPackageUI", "ListButtonPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.ListButtonPackageCss), "By.CssSelector: .listButtonPackageCss", "ListButtonPackageCss", null }
        };

        /*private static object[] _extendedSectionUIUIListSomeData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPublic), "By.Id: uilistSomedataPublic", "UilistSomedataPublic", null },
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPublicUI), "By.CssSelector: .uilistSomedataPublicUI", "UilistSomedataPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPublicXPath), "By.XPath: //*[@class='uilistSomedataPublicXPath']", "UilistSomedataPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPackage), "By.Id: uilistSomedataPackage", "DroplistPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPrivate), "By.Id: uilistSomedataPrivate", "UilistSomedataPrivate", null },
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPackageUI), "By.CssSelector: .uilistSomedataPackageUI", "UilistSomedataPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.UilistSomedataPackageCss), "By.CssSelector: .uilistSomedataPackageCss", "UilistSomedataPackageCss", null }
        };*/

        /*private static object[] _extendedSectionUIUIListQuestionData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPublic), "By.Id: uilistQuestionPublic", "UilistQuestionPublic", null },
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPublicUI), "By.CssSelector: .uilistQuestionPublicUI", "UilistQuestionPublicUI", null },
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPublicXPath), "By.XPath: //*[@class='uilistQuestionPublicXPath']", "UilistQuestionPublicXPath", null },
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPackage), "By.Id: uilistQuestionPackage", "UilistQuestionPackage", null },
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPrivate), "By.Id: uilistQuestionPrivate", "UilistQuestionPrivate", null },
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPackageUI), "By.CssSelector: .uilistQuestionPackageUI", "UilistQuestionPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.UilistQuestionPackageCss), "By.CssSelector: .uilistQuestionPackageCss", "UilistQuestionPackageCss", null }
        };*/

        private static object[] _extendedSectionUIDropDownData =
        {
            new object[] { nameof(ExtendedSectionUI.DropListPackage), "By.CssSelector: div[ui=droplistPackage]", "DropListPackage", null },
            new object[] { nameof(ExtendedSectionUI.DroplistPublic), "By.CssSelector: div[ui=droplistPublic]", "DroplistPublic", null },
            new object[] { nameof(ExtendedSectionUI.DroplistPrivate), "By.CssSelector: div[ui=droplistPrivate]", "DroplistPrivate", null },
            new object[] { nameof(ExtendedSectionUI.DroplistPackageUI), "By.CssSelector: .droplistPackageUI", "DroplistPackageUI", null },
            new object[] { nameof(ExtendedSectionUI.DroplistPublicUI), "By.CssSelector: .droplistPublicUI", "DroplistPublicUI", null },
        };
    }
}
