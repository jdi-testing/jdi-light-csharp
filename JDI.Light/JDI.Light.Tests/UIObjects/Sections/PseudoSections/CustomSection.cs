using System.Collections.Generic;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Elements.Composite;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UIObjects.Sections.PseudoSections
{
    public class CustomSection : Section
    {
        #region CustomSectionWebElement

        [FindBy(Id = "webElementPackage")]
        public IWebElement WebElementPackage { get; set; }

        [FindBy(Id = "webElementPublic")] 
        public IWebElement WebElementPublic { get; set; }

        [FindBy(Id = "webElementPrivate")]
        public IWebElement WebElementPrivate { get; set; }

        [FindBy(Css = ".webElementPackageUI")]
        public IWebElement WebElementPackageUI { get; set; }

        [FindBy(Css = ".webElementPublicUI")]
        public IWebElement WebElementPublicUI { get; set; }

        [FindBy(Css = ".webElementPackageCss")]
        public IWebElement WebElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='webElementPublicXPath']")]
        public IWebElement WebElementPublicXPath { get; set; }

        #endregion

        #region CustomSectionListWebElement  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "listWebElementPackage")] 
        public List<IWebElement> ListWebElementPackage { get; set; }

        [FindBy(Id = "listWebElementPublic")]
        public List<IWebElement> ListWebElementPublic { get; set; }

        [FindBy(Id = "listWebElementPrivate")]
        public List<IWebElement> ListWebElementPrivate { get; set; }

        [FindBy(Css = ".listWebElementPackageUI")]
        public List<IWebElement> ListWebElementPackageUI { get; set; }

        [FindBy(Css = ".listWebElementPublicUI")]
        public List<IWebElement> ListWebElementPublicUI { get; set; }

        [FindBy(Css = ".listWebElementPackageCss")]
        public List<IWebElement> ListWebElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='listWebElementPublicXPath']")]
        public List<IWebElement> ListWebElementPublicXPath { get; set; }

        #endregion

        #region CustomSectionUIElement

        [FindBy(Id = "uielementPackage")]
        public UIElement UIWebElementPackage { get; set; }

        [FindBy(Id = "uielementPublic")]
        public UIElement UIWebElementPublic { get; set; }

        [FindBy(Id = "uielementPrivate")]
        public UIElement UIWebElementPrivate { get; set; }

        [FindBy(Css = ".uielementPackageUI")]
        public UIElement UIWebElementPackageUI { get; set; }

        [FindBy(Css = ".uielementPublicUI")]
        public UIElement UIWebElementPublicUI { get; set; }

        [FindBy(Css = ".uielementPackageCss")]
        public UIElement UIWebElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='uielementPublicXPath']")]
        public UIElement UIWebElementPublicXPath { get; set; }

        #endregion

        #region customSectionButtonElement

        [FindBy(Id = "buttonPackage")]
        public Button ButtonPackage { get; set; }

        [FindBy(Id = "buttonPublic")]
        public Button ButtonPublic { get; set; }

        [FindBy(Id = "buttonPrivate")]
        public Button ButtonPrivate { get; set; }

        [FindBy(Css = ".buttonPackageUI")]
        public Button ButtonPackageUI { get; set; }

        [FindBy(Css = ".buttonPublicUI")]
        public Button ButtonPublicUI { get; set; }

        [FindBy(Css = ".buttonPackageCss")]
        public UIElement ButtonPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='buttonPublicXPath']")]
        public UIElement ButtonPublicXPath { get; set; }

        #endregion

        #region CustomSectionListUIElement  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "listUIElementPackage")]
        public List<UIElement> ListUIElementPackage { get; set; }

        [FindBy(Id = "listUIElementPublic")]
        public List<UIElement> ListUIElementPublic { get; set; }

        [FindBy(Id = "listUIElementPrivate")]
        public List<UIElement> ListUIElementPrivate { get; set; }

        [FindBy(Css = ".listUIElementPackageUI")]
        public List<UIElement> ListUIElementPackageUI { get; set; }

        [FindBy(Css = ".listUIElementPublicUI")]
        public List<UIElement> ListUIElementPublicUI { get; set; }

        [FindBy(Css = ".listUIElementPackageCss")]
        public List<UIElement> ListUIElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='listUIElementPublicXPath']")]
        public List<UIElement> ListUIElementPublicXPath { get; set; }

        #endregion

        #region customSectionWebList  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "webListPackage")]
        public List<IBaseUIElement> WebListPackage { get; set; }

        [FindBy(Id = "webListPublic")]
        public List<IBaseUIElement> WebListPublic { get; set; }

        [FindBy(Id = "webListPrivate")]
        public List<IBaseUIElement> WebListPrivate { get; set; }

        [FindBy(Css = ".webListPackageUI")]
        public List<IBaseUIElement> WebListPackageUI { get; set; }

        [FindBy(Css = ".webListPublicUI")]
        public List<IBaseUIElement> WebListPublicUI { get; set; }

        [FindBy(Css = ".webListPackageCss")]
        public List<IBaseUIElement> WebListPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='webListPublicXPath']")]
        public List<IBaseUIElement> WebListPublicXPath { get; set; }

        #endregion

        #region CustomSectionListButtonPublic  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "listButtonPackage")]
        public List<Button> ListButtonPackage { get; set; }

        [FindBy(Id = "listButtonPublic")]
        public List<Button> ListButtonPublic { get; set; }

        [FindBy(Id = "listButtonPrivate")]
        public List<Button> ListButtonPrivate { get; set; }

        [FindBy(Css = ".listButtonPackageUI")]
        public List<Button> ListButtonPackageUI { get; set; }

        [FindBy(Css = ".listButtonPublicUI")]
        public List<Button> ListButtonPublicUI { get; set; }

        [FindBy(Css = ".listButtonPackageCss")]
        public List<Button> ListButtonPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='listButtonPublicXPath']")]
        public List<Button> ListButtonPublicXPath { get; set; }

        #endregion

        #region CustomSectionUIListSomeData

        /*[FindBy(Id = "uilistSomedataPackage")] TODO:// Implement UIList<T>
        public UIList<CustomSection, SomeData> UilistSomedataPackage { get; set; }

        [FindBy(Id = "uilistSomedataPublic")]
        public UIList<CustomSection, SomeData> UilistSomedataPublic { get; set; }

        [FindBy(Id = "uilistSomedataPrivate")]
        public UIList<CustomSection, SomeData> UilistSomedataPrivate { get; set; }

        [FindBy(Css = ".uilistSomedataPackageUI")]
        public UIList<CustomSection, SomeData> UilistSomedataPackageUI { get; set; }

        [FindBy(Css = ".uilistSomedataPublicUI")]
        public UIList<CustomSection, SomeData> UilistSomedataPublicUI { get; set; }

        [FindBy(Css = ".uilistSomedataPackageCss")]
        public UIList<CustomSection, SomeData> UilistSomedataPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='uilistSomedataPublicXPath']")]
        pulic UIList<CustomSection, SomeData> UilistSomedataPublicXPath { get; set; }*/

        #endregion

        #region CustomSectionUIListQuestion

        /*[FindBy(Id = "uilistQuestionPackage")] TODO:// Implement UIList<T>
        public UIList<CustomSection> UilistQuestionPackage { get; set; }

        [FindBy(Id = "uilistQuestionPublic")]
        public UIList<CustomSection> UilistQuestionPublic { get; set; }

        [FindBy(Id = "uilistQuestionPrivate")]
        public UIList<CustomSection> UilistQuestionPrivate { get; set; }

        [FindBy(Css = ".uilistQuestionPackageUI")]
        public UIList<CustomSection> UilistQuestionPackageUI { get; set; }

        [FindBy(Css = ".uilistQuestionPublicUI")]
        public UIList<CustomSection> UilistQuestionPublicUI { get; set; }

        [FindBy(Css = ".uilistQuestionPackageCss")]
        public UIList<CustomSection> UilistQuestionPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='uilistQuestionPublicXPath']")]
        public UIList<CustomSection> UilistQuestionPublicXPath { get; set; }*/

        #endregion

        #region CustomSectionDropDown

        [JDropDown("div[ui=droplistPackage]", "input", "li", ".expand")]
        public DropList DropListPackage { get; set; }

        [JDropDown("div[ui=droplistPublic]", "input", "li", ".expand")]
        public DropList DroplistPublic { get; set; }

        [JDropDown("div[ui=droplistPrivate]", "input", "li", ".expand")]
        public DropList DroplistPrivate { get; set; }

        [FindBy(Css = ".droplistPackageUI")]
        public DropList DroplistPackageUI { get; set; }

        [FindBy(Css = ".droplistPublicUI")]
        public DropList DroplistPublicUI { get; set; }

        #endregion
    }
}
