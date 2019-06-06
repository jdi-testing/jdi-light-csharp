using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Tests.UIObjects.PseudoSections;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite.PseudoSiteTests
{
    public class CustomSectionUITests : TestBase
    {
        private static CustomSection CustomSectionUI => TestSite.PseudoSitePage.CustomSectionUI;

        [TestCaseSource(nameof(_customSectionUIWebElementData))]
        public void CustomSectionWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as UIElement;
            CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIListWebElementData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionListWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as List<IWebElement>;
            //CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionUiuiWebElementData))]
        public void CustomSectionUIElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as UIElement;
            CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionUIButtonElementData))]
        public void CustomSectionButtonTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as UIElement;
            CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIWebListData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionUIWebListTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as List<IWebElement>;
            //CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIListUIElementPublicData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionUIListUIElementPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as List<IWebElement>;
            //CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIListButtonPublicData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionUIListButtonPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as List<IWebElement>;
            //CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIUIListSomeData))] //Uncomment when UIList<T> will be implemented
        public void CustomSectionUiuiListSomeDataTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as List<IWebElement>;
            //CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIUIListQuestionData))] //Uncomment when UIList<T> will be implemented
        public void CustomSectionUiuiListQuestionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as List<IWebElement>;
            //CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionUIDropDownData))]
        public void CustomSectionDropDownTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSectionUI.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSectionUI) as UIElement;
            CustomSectionUI.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        private static object[] _customSectionUIWebElementData =
        {
            new object[] { nameof(CustomSectionUI.WebElementPublic), "By.Id: webElementPublic", "WebElementPublic", null },
            new object[] { nameof(CustomSectionUI.WebElementPublicUI), "By.CssSelector: .webElementPublicUI", "WebElementPublicUI", null },
            new object[] { nameof(CustomSectionUI.WebElementPublicXPath), "By.XPath: //*[@class='webElementPublicXPath']", "WebElementPublicXPath", null },
            new object[] { nameof(CustomSectionUI.WebElementPackage), "By.Id: webElementPackage", "WebElementPackage", null },
            new object[] { nameof(CustomSectionUI.WebElementPrivate), "By.Id: webElementPrivate", "WebElementPrivate", null },
            new object[] { nameof(CustomSectionUI.WebElementPackageUI), "By.CssSelector: .webElementPackageUI", "WebElementPackageUI", null },
            new object[] { nameof(CustomSectionUI.WebElementPackageCss), "By.CssSelector: .webElementPackageCss", "WebElementPackageCss", null }
        };

        private static object[] _customSectionUIListWebElementData =
        {
            new object[] { nameof(CustomSectionUI.ListWebElementPublic), "By.Id: listWebElementPublic", "ListWebElementPublic", null },
            new object[] { nameof(CustomSectionUI.ListWebElementPublicUI), "By.CssSelector: .listWebElementPublicUI", "ListWebElementPublicUI", null },
            new object[] { nameof(CustomSectionUI.ListWebElementPublicXPath), "By.XPath: //*[@class='listWebElementPublicXPath']", "ListWebElementPublicXPath", null },
            new object[] { nameof(CustomSectionUI.ListWebElementPackage), "By.Id: listWebElementPackage", "ListWebElementPackage", null },
            new object[] { nameof(CustomSectionUI.ListWebElementPrivate), "By.Id: listWebElementPrivate", "ListWebElementPrivate", null },
            new object[] { nameof(CustomSectionUI.ListWebElementPackageUI), "By.CssSelector: .listWebElementPackageUI", "ListWebElementPackageUI", null },
            new object[] { nameof(CustomSectionUI.ListWebElementPackageCss), "By.CssSelector: .listWebElementPackageCss", "ListWebElementPackageCss", null }
        };

        private static object[] _customSectionUiuiWebElementData =
        {
            new object[] { nameof(CustomSectionUI.UIWebElementPublic), "By.Id: uielementPublic", "UIWebElementPublic", null },
            new object[] { nameof(CustomSectionUI.UIWebElementPublicUI), "By.CssSelector: .uielementPublicUI", "UIWebElementPublicUI", null },
            new object[] { nameof(CustomSectionUI.UIWebElementPublicXPath), "By.XPath: //*[@class='uielementPublicXPath']", "UIWebElementPublicXPath", null },
            new object[] { nameof(CustomSectionUI.UIWebElementPackage), "By.Id: uielementPackage", "UIWebElementPackage", null },
            new object[] { nameof(CustomSectionUI.UIWebElementPrivate), "By.Id: uielementPrivate", "UIWebElementPrivate", null },
            new object[] { nameof(CustomSectionUI.UIWebElementPackageUI), "By.CssSelector: .uielementPackageUI", "UIWebElementPackageUI", null },
            new object[] { nameof(CustomSectionUI.UIWebElementPackageCss), "By.CssSelector: .uielementPackageCss", "UIWebElementPackageCss", null }
        };

        private static object[] _customSectionUIButtonElementData =
        {
            new object[] { nameof(CustomSectionUI.ButtonPublic), "By.Id: buttonPublic", "ButtonPublic", null },
            new object[] { nameof(CustomSectionUI.ButtonPublicUI), "By.CssSelector: .buttonPublicUI", "ButtonPublicUI", null },
            new object[] { nameof(CustomSectionUI.ButtonPublicXPath), "By.XPath: //*[@class='buttonPublicXPath']", "ButtonPublicXPath", null },
            new object[] { nameof(CustomSectionUI.ButtonPackage), "By.Id: buttonPackage", "ButtonPackage", null },
            new object[] { nameof(CustomSectionUI.ButtonPrivate), "By.Id: buttonPrivate", "ButtonPrivate", null },
            new object[] { nameof(CustomSectionUI.ButtonPackageUI), "By.CssSelector: .buttonPackageUI", "ButtonPackageUI", null },
            new object[] { nameof(CustomSectionUI.ButtonPackageCss), "By.CssSelector: .buttonPackageCss", "ButtonPackageCss", null }
        };

        private static object[] _customSectionUIWebListData =
        {
            new object[] { nameof(CustomSectionUI.WebListPublic), "By.Id: webListPublic", "WebListPublic", null },
            new object[] { nameof(CustomSectionUI.WebListPublicUI), "By.CssSelector: .webListPublicUI", "WebListPublicUI", null },
            new object[] { nameof(CustomSectionUI.WebListPublicXPath), "By.XPath: //*[@class='webListPublicXPath']", "WebListPublicXPath", null },
            new object[] { nameof(CustomSectionUI.WebListPackage), "By.Id: webListPackage", "WebListPackage", null },
            new object[] { nameof(CustomSectionUI.WebListPrivate), "By.Id: webListPrivate", "WebListPrivate", null },
            new object[] { nameof(CustomSectionUI.WebListPackageUI), "By.CssSelector: .webListPackageUI", "WebListPackageUI", null },
            new object[] { nameof(CustomSectionUI.WebListPackageCss), "By.CssSelector: .webListPackageCss", "WebListPackageCss", null }
        };

        private static object[] _customSectionUIListUIElementPublicData =
        {
            new object[] { nameof(CustomSectionUI.ListUIElementPublic), "By.Id: listUIElementPublic", "ListUIElementPublic", null },
            new object[] { nameof(CustomSectionUI.ListUIElementPublicUI), "By.CssSelector: .listUIElementPublicUI", "ListUIElementPublicUI", null },
            new object[] { nameof(CustomSectionUI.ListUIElementPublicXPath), "By.XPath: //*[@class='listUIElementPublicXPath']", "ListUIElementPublicXPath", null },
            new object[] { nameof(CustomSectionUI.ListUIElementPackage), "By.Id: listUIElementPackage", "ListUIElementPackage", null },
            new object[] { nameof(CustomSectionUI.ListUIElementPrivate), "By.Id: listUIElementPrivate", "ListUIElementPrivate", null },
            new object[] { nameof(CustomSectionUI.ListUIElementPackageUI), "By.CssSelector: .listUIElementPackageUI", "ListUIElementPackageUI", null },
            new object[] { nameof(CustomSectionUI.ListUIElementPackageCss), "By.CssSelector: .listUIElementPackageCss", "ListUIElementPackageCss", null }
        };

        private static object[] _customSectionUIListButtonPublicData =
        {
            new object[] { nameof(CustomSectionUI.ListButtonPublic), "By.Id: listButtonPublic", "ListButtonPublic", null },
            new object[] { nameof(CustomSectionUI.ListButtonPublicUI), "By.CssSelector: .listButtonPublicUI", "ListButtonPublicUI", null },
            new object[] { nameof(CustomSectionUI.ListButtonPublicXPath), "By.XPath: //*[@class='listButtonPublicXPath']", "ListButtonPublicXPath", null },
            new object[] { nameof(CustomSectionUI.ListButtonPackage), "By.Id: listButtonPackage", "ListButtonPackage", null },
            new object[] { nameof(CustomSectionUI.ListButtonPrivate), "By.Id: listButtonPrivate", "ListButtonPrivate", null },
            new object[] { nameof(CustomSectionUI.ListButtonPackageUI), "By.CssSelector: .listButtonPackageUI", "ListButtonPackageUI", null },
            new object[] { nameof(CustomSectionUI.ListButtonPackageCss), "By.CssSelector: .listButtonPackageCss", "ListButtonPackageCss", null }
        };

        /*private static object[] _customSectionUIUIListSomeData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(CustomSectionUI.UilistSomedataPublic), "By.Id: uilistSomedataPublic", "UilistSomedataPublic", null },
            new object[] { nameof(CustomSectionUI.UilistSomedataPublicUI), "By.CssSelector: .uilistSomedataPublicUI", "UilistSomedataPublicUI", null },
            new object[] { nameof(CustomSectionUI.UilistSomedataPublicXPath), "By.XPath: //*[@class='uilistSomedataPublicXPath']", "UilistSomedataPublicXPath", null },
            new object[] { nameof(CustomSectionUI.UilistSomedataPackage), "By.Id: uilistSomedataPackage", "DroplistPackageUI", null },
            new object[] { nameof(CustomSectionUI.UilistSomedataPrivate), "By.Id: uilistSomedataPrivate", "UilistSomedataPrivate", null },
            new object[] { nameof(CustomSectionUI.UilistSomedataPackageUI), "By.CssSelector: .uilistSomedataPackageUI", "UilistSomedataPackageUI", null },
            new object[] { nameof(CustomSectionUI.UilistSomedataPackageCss), "By.CssSelector: .uilistSomedataPackageCss", "UilistSomedataPackageCss", null }
        };*/

        /*private static object[] _customSectionUIUIListQuestionData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(CustomSectionUI.UilistQuestionPublic), "By.Id: uilistQuestionPublic", "UilistQuestionPublic", null },
            new object[] { nameof(CustomSectionUI.UilistQuestionPublicUI), "By.CssSelector: .uilistQuestionPublicUI", "UilistQuestionPublicUI", null },
            new object[] { nameof(CustomSectionUI.UilistQuestionPublicXPath), "By.XPath: //*[@class='uilistQuestionPublicXPath']", "UilistQuestionPublicXPath", null },
            new object[] { nameof(CustomSectionUI.UilistQuestionPackage), "By.Id: uilistQuestionPackage", "UilistQuestionPackage", null },
            new object[] { nameof(CustomSectionUI.UilistQuestionPrivate), "By.Id: uilistQuestionPrivate", "UilistQuestionPrivate", null },
            new object[] { nameof(CustomSectionUI.UilistQuestionPackageUI), "By.CssSelector: .uilistQuestionPackageUI", "UilistQuestionPackageUI", null },
            new object[] { nameof(CustomSectionUI.UilistQuestionPackageCss), "By.CssSelector: .uilistQuestionPackageCss", "UilistQuestionPackageCss", null }
        };*/

        private static object[] _customSectionUIDropDownData =
        {
            new object[] { nameof(CustomSectionUI.DropListPackage), "By.CssSelector: div[ui=droplistPackage]", "DropListPackage", null },
            new object[] { nameof(CustomSectionUI.DroplistPublic), "By.CssSelector: div[ui=droplistPublic]", "DroplistPublic", null },
            new object[] { nameof(CustomSectionUI.DroplistPrivate), "By.CssSelector: div[ui=droplistPrivate]", "DroplistPrivate", null },
            new object[] { nameof(CustomSectionUI.DroplistPackageUI), "By.CssSelector: .droplistPackageUI", "DroplistPackageUI", null },
            new object[] { nameof(CustomSectionUI.DroplistPublicUI), "By.CssSelector: .droplistPublicUI", "DroplistPublicUI", null },
        };
    }
}
