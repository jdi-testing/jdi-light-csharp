using System.Collections.Generic;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Tests.UIObjects;
using JDI.Light.Tests.UIObjects.Sections.PseudoSections;
using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Composite.PseudoSiteTests
{
    public class CustomSectionTests : TestBase
    {
        private static CustomSection CustomSection => TestSite.PseudoSitePage.CustomSection;

        [SetUp]
        public override void SetUpTest()
        {
            Jdi.Logger.Info("Test Base Set up started...");
            TestSite = Jdi.InitSite<TestSite>();
            Jdi.Logger.Info("Test Base Set up done.");
            Jdi.Logger.Info("Run test...");
        }

        [TestCaseSource(nameof(_customSectionWebElementData))]
        public void CustomSectionWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as UIElement;
            CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionListWebElementData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionListWebElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionUIWebElementData))]
        public void CustomSectionUIElementTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as UIElement;
            CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionButtonElementData))]
        public void CustomSectionButtonTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as UIElement;
            CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionWebListData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionWebListTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionListUIElementPublicData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionListUIElementPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionListButtonPublicData))] //Fix initialization of List<IWebElement> element
        public void CustomSectionListButtonPublicTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIListSomeData))] //Uncomment when UIList<T> will be implemented
        public void CustomSectionUIListSomeDataTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        //[TestCaseSource(nameof(_customSectionUIListQuestionData))] //Uncomment when UIList<T> will be implemented
        public void CustomSectionUIListQuestionTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as List<IWebElement>;
            //CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TestCaseSource(nameof(_customSectionDropDownData))]
        public void CustomSectionDropDownTest(string htmlElementToCheckName, string expectedLocator, string expectedName, string expectedSmartLocator)
        {
            var targetElement =
                CustomSection.GetType().GetMember(htmlElementToCheckName)[0].GetMemberValue(CustomSection) as UIElement;
            CustomSection.CheckInitializedElement(targetElement, expectedLocator, expectedName, expectedSmartLocator);
        }

        [TearDown]
        public override void TestTearDown()
        {
            Jdi.Logger.Info("Run test tear down done.");
        }

        private static object[] _customSectionWebElementData =
        {
            new object[] { nameof(CustomSection.WebElementPublic), "By.Id: webElementPublic", "WebElementPublic", null },
            new object[] { nameof(CustomSection.WebElementPublicUI), "By.CssSelector: .webElementPublicUI", "WebElementPublicUI", null },
            new object[] { nameof(CustomSection.WebElementPublicXPath), "By.XPath: //*[@class='webElementPublicXPath']", "WebElementPublicXPath", null },
            new object[] { nameof(CustomSection.WebElementPackage), "By.Id: webElementPackage", "WebElementPackage", null },
            new object[] { nameof(CustomSection.WebElementPrivate), "By.Id: webElementPrivate", "WebElementPrivate", null },
            new object[] { nameof(CustomSection.WebElementPackageUI), "By.CssSelector: .webElementPackageUI", "WebElementPackageUI", null },
            new object[] { nameof(CustomSection.WebElementPackageCss), "By.CssSelector: .webElementPackageCss", "WebElementPackageCss", null }
        };

        private static object[] _customSectionListWebElementData =
        {
            new object[] { nameof(CustomSection.ListWebElementPublic), "By.Id: listWebElementPublic", "ListWebElementPublic", null },
            new object[] { nameof(CustomSection.ListWebElementPublicUI), "By.CssSelector: .listWebElementPublicUI", "ListWebElementPublicUI", null },
            new object[] { nameof(CustomSection.ListWebElementPublicXPath), "By.XPath: //*[@class='listWebElementPublicXPath']", "ListWebElementPublicXPath", null },
            new object[] { nameof(CustomSection.ListWebElementPackage), "By.Id: listWebElementPackage", "ListWebElementPackage", null },
            new object[] { nameof(CustomSection.ListWebElementPrivate), "By.Id: listWebElementPrivate", "ListWebElementPrivate", null },
            new object[] { nameof(CustomSection.ListWebElementPackageUI), "By.CssSelector: .listWebElementPackageUI", "ListWebElementPackageUI", null },
            new object[] { nameof(CustomSection.ListWebElementPackageCss), "By.CssSelector: .listWebElementPackageCss", "ListWebElementPackageCss", null }
        };

        private static object[] _customSectionUIWebElementData =
        {
            new object[] { nameof(CustomSection.UIWebElementPublic), "By.Id: uielementPublic", "UIWebElementPublic", null },
            new object[] { nameof(CustomSection.UIWebElementPublicUI), "By.CssSelector: .uielementPublicUI", "UIWebElementPublicUI", null },
            new object[] { nameof(CustomSection.UIWebElementPublicXPath), "By.XPath: //*[@class='uielementPublicXPath']", "UIWebElementPublicXPath", null },
            new object[] { nameof(CustomSection.UIWebElementPackage), "By.Id: uielementPackage", "UIWebElementPackage", null },
            new object[] { nameof(CustomSection.UIWebElementPrivate), "By.Id: uielementPrivate", "UIWebElementPrivate", null },
            new object[] { nameof(CustomSection.UIWebElementPackageUI), "By.CssSelector: .uielementPackageUI", "UIWebElementPackageUI", null },
            new object[] { nameof(CustomSection.UIWebElementPackageCss), "By.CssSelector: .uielementPackageCss", "UIWebElementPackageCss", null }
        };

        private static object[] _customSectionButtonElementData =
        {
            new object[] { nameof(CustomSection.ButtonPublic), "By.Id: buttonPublic", "ButtonPublic", null },
            new object[] { nameof(CustomSection.ButtonPublicUI), "By.CssSelector: .buttonPublicUI", "ButtonPublicUI", null },
            new object[] { nameof(CustomSection.ButtonPublicXPath), "By.XPath: //*[@class='buttonPublicXPath']", "ButtonPublicXPath", null },
            new object[] { nameof(CustomSection.ButtonPackage), "By.Id: buttonPackage", "ButtonPackage", null },
            new object[] { nameof(CustomSection.ButtonPrivate), "By.Id: buttonPrivate", "ButtonPrivate", null },
            new object[] { nameof(CustomSection.ButtonPackageUI), "By.CssSelector: .buttonPackageUI", "ButtonPackageUI", null },
            new object[] { nameof(CustomSection.ButtonPackageCss), "By.CssSelector: .buttonPackageCss", "ButtonPackageCss", null }
        };

        private static object[] _customSectionListUIElementPublicData =
        {
            new object[] { nameof(CustomSection.ListUIElementPublic), "By.Id: listUIElementPublic", "ListUIElementPublic", null },
            new object[] { nameof(CustomSection.ListUIElementPublicUI), "By.CssSelector: .listUIElementPublicUI", "ListUIElementPublicUI", null },
            new object[] { nameof(CustomSection.ListUIElementPublicXPath), "By.XPath: //*[@class='listUIElementPublicXPath']", "ListUIElementPublicXPath", null },
            new object[] { nameof(CustomSection.ListUIElementPackage), "By.Id: listUIElementPackage", "ListUIElementPackage", null },
            new object[] { nameof(CustomSection.ListUIElementPrivate), "By.Id: listUIElementPrivate", "ListUIElementPrivate", null },
            new object[] { nameof(CustomSection.ListUIElementPackageUI), "By.CssSelector: .listUIElementPackageUI", "ListUIElementPackageUI", null },
            new object[] { nameof(CustomSection.ListUIElementPackageCss), "By.CssSelector: .listUIElementPackageCss", "ListUIElementPackageCss", null }
        };

        private static object[] _customSectionWebListData =
        {
            new object[] { nameof(CustomSection.WebListPublic), "By.Id: webListPublic", "WebListPublic", null },
            new object[] { nameof(CustomSection.WebListPublicUI), "By.CssSelector: .webListPublicUI", "WebListPublicUI", null },
            new object[] { nameof(CustomSection.WebListPublicXPath), "By.XPath: //*[@class='webListPublicXPath']", "WebListPublicXPath", null },
            new object[] { nameof(CustomSection.WebListPackage), "By.Id: webListPackage", "WebListPackage", null },
            new object[] { nameof(CustomSection.WebListPrivate), "By.Id: webListPrivate", "WebListPrivate", null },
            new object[] { nameof(CustomSection.WebListPackageUI), "By.CssSelector: .webListPackageUI", "WebListPackageUI", null },
            new object[] { nameof(CustomSection.WebListPackageCss), "By.CssSelector: .webListPackageCss", "WebListPackageCss", null }
        };

        private static object[] _customSectionListButtonPublicData =
        {
            new object[] { nameof(CustomSection.ListButtonPublic), "By.Id: listButtonPublic", "ListButtonPublic", null },
            new object[] { nameof(CustomSection.ListButtonPublicUI), "By.CssSelector: .listButtonPublicUI", "ListButtonPublicUI", null },
            new object[] { nameof(CustomSection.ListButtonPublicXPath), "By.XPath: //*[@class='listButtonPublicXPath']", "ListButtonPublicXPath", null },
            new object[] { nameof(CustomSection.ListButtonPackage), "By.Id: listButtonPackage", "ListButtonPackage", null },
            new object[] { nameof(CustomSection.ListButtonPrivate), "By.Id: listButtonPrivate", "ListButtonPrivate", null },
            new object[] { nameof(CustomSection.ListButtonPackageUI), "By.CssSelector: .listButtonPackageUI", "ListButtonPackageUI", null },
            new object[] { nameof(CustomSection.ListButtonPackageCss), "By.CssSelector: .listButtonPackageCss", "ListButtonPackageCss", null }
        };

        /*private static object[] _customSectionUIListSomeData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(CustomSection.UilistSomedataPublic), "By.Id: uilistSomedataPublic", "UilistSomedataPublic", null },
            new object[] { nameof(CustomSection.UilistSomedataPublicUI), "By.CssSelector: .uilistSomedataPublicUI", "UilistSomedataPublicUI", null },
            new object[] { nameof(CustomSection.UilistSomedataPublicXPath), "By.XPath: //*[@class='uilistSomedataPublicXPath']", "UilistSomedataPublicXPath", null },
            new object[] { nameof(CustomSection.UilistSomedataPackage), "By.Id: uilistSomedataPackage", "DroplistPackageUI", null },
            new object[] { nameof(CustomSection.UilistSomedataPrivate), "By.Id: uilistSomedataPrivate", "UilistSomedataPrivate", null },
            new object[] { nameof(CustomSection.UilistSomedataPackageUI), "By.CssSelector: .uilistSomedataPackageUI", "UilistSomedataPackageUI", null },
            new object[] { nameof(CustomSection.UilistSomedataPackageCss), "By.CssSelector: .uilistSomedataPackageCss", "UilistSomedataPackageCss", null }
        };*/

        /*private static object[] _customSectionUIListQuestionData =  TODO: //Uncomment when UIList<T> will be implemented 
        {
            new object[] { nameof(CustomSection.UilistQuestionPublic), "By.Id: uilistQuestionPublic", "UilistQuestionPublic", null },
            new object[] { nameof(CustomSection.UilistQuestionPublicUI), "By.CssSelector: .uilistQuestionPublicUI", "UilistQuestionPublicUI", null },
            new object[] { nameof(CustomSection.UilistQuestionPublicXPath), "By.XPath: //*[@class='uilistQuestionPublicXPath']", "UilistQuestionPublicXPath", null },
            new object[] { nameof(CustomSection.UilistQuestionPackage), "By.Id: uilistQuestionPackage", "UilistQuestionPackage", null },
            new object[] { nameof(CustomSection.UilistQuestionPrivate), "By.Id: uilistQuestionPrivate", "UilistQuestionPrivate", null },
            new object[] { nameof(CustomSection.UilistQuestionPackageUI), "By.CssSelector: .uilistQuestionPackageUI", "UilistQuestionPackageUI", null },
            new object[] { nameof(CustomSection.UilistQuestionPackageCss), "By.CssSelector: .uilistQuestionPackageCss", "UilistQuestionPackageCss", null }
        };*/

        private static object[] _customSectionDropDownData =
        {
            new object[] { nameof(CustomSection.DropListPackage), "By.CssSelector: div[ui=droplistPackage]", "DropListPackage", null },
            new object[] { nameof(CustomSection.DroplistPublic), "By.CssSelector: div[ui=droplistPublic]", "DroplistPublic", null },
            new object[] { nameof(CustomSection.DroplistPrivate), "By.CssSelector: div[ui=droplistPrivate]", "DroplistPrivate", null },
            new object[] { nameof(CustomSection.DroplistPackageUI), "By.CssSelector: .droplistPackageUI", "DroplistPackageUI", null },
            new object[] { nameof(CustomSection.DroplistPublicUI), "By.CssSelector: .droplistPublicUI", "DroplistPublicUI", null },
        };
    }
}
