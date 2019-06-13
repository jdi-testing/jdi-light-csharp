using System.Collections.Generic;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Interfaces.Base;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UIObjects.Sections.PseudoSections
{
    public class ExtendedSection : CustomSection
    {
        #region ExtendedSectionWebElement

        [FindBy(Id = "webElementPackage")]
        public new IWebElement WebElementPackage { get; set; }

        [FindBy(Id = "webElementPublic")]
        public new IWebElement WebElementPublic { get; set; }

        [FindBy(Id = "webElementPrivate")]
        public new IWebElement WebElementPrivate { get; set; }

        [FindBy(Css = ".webElementPackageUI")]
        public new IWebElement WebElementPackageUI { get; set; }

        [FindBy(Css = ".webElementPublicUI")]
        public new IWebElement WebElementPublicUI { get; set; }

        [FindBy(Css = ".webElementPackageCss")]
        public new IWebElement WebElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='webElementPublicXPath']")]
        public new IWebElement WebElementPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionListWebElement  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "listWebElementPackage")]
        public new List<IWebElement> ListWebElementPackage { get; set; }

        [FindBy(Id = "listWebElementPublic")]
        public new List<IWebElement> ListWebElementPublic { get; set; }

        [FindBy(Id = "listWebElementPrivate")]
        public new List<IWebElement> ListWebElementPrivate { get; set; }

        [FindBy(Css = ".listWebElementPackageUI")]
        public new List<IWebElement> ListWebElementPackageUI { get; set; }

        [FindBy(Css = ".listWebElementPublicUI")]
        public new List<IWebElement> ListWebElementPublicUI { get; set; }

        [FindBy(Css = ".listWebElementPackageCss")]
        public new List<IWebElement> ListWebElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='listWebElementPublicXPath']")]
        public new List<IWebElement> ListWebElementPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionUIElement

        [FindBy(Id = "uielementPackage")]
        public new UIElement UIWebElementPackage { get; set; }

        [FindBy(Id = "uielementPublic")]
        public new UIElement UIWebElementPublic { get; set; }

        [FindBy(Id = "uielementPrivate")]
        public new UIElement UIWebElementPrivate { get; set; }

        [FindBy(Css = ".uielementPackageUI")]
        public new UIElement UIWebElementPackageUI { get; set; }

        [FindBy(Css = ".uielementPublicUI")]
        public new UIElement UIWebElementPublicUI { get; set; }

        [FindBy(Css = ".uielementPackageCss")]
        public new UIElement UIWebElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='uielementPublicXPath']")]
        public new UIElement UIWebElementPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionButtonElement

        [FindBy(Id = "buttonPackage")]
        public new Button ButtonPackage { get; set; }

        [FindBy(Id = "buttonPublic")]
        public new Button ButtonPublic { get; set; }

        [FindBy(Id = "buttonPrivate")]
        public new Button ButtonPrivate { get; set; }

        [FindBy(Css = ".buttonPackageUI")]
        public new Button ButtonPackageUI { get; set; }

        [FindBy(Css = ".buttonPublicUI")]
        public new Button ButtonPublicUI { get; set; }

        [FindBy(Css = ".buttonPackageCss")]
        public new UIElement ButtonPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='buttonPublicXPath']")]
        public new UIElement ButtonPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionListUIElement  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "listUIElementPackage")]
        public new List<UIElement> ListUIElementPackage { get; set; }

        [FindBy(Id = "listUIElementPublic")]
        public new List<UIElement> ListUIElementPublic { get; set; }

        [FindBy(Id = "listUIElementPrivate")]
        public new List<UIElement> ListUIElementPrivate { get; set; }

        [FindBy(Css = ".listUIElementPackageUI")]
        public new List<UIElement> ListUIElementPackageUI { get; set; }

        [FindBy(Css = ".listUIElementPublicUI")]
        public new List<UIElement> ListUIElementPublicUI { get; set; }

        [FindBy(Css = ".listUIElementPackageCss")]
        public new List<UIElement> ListUIElementPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='listUIElementPublicXPath']")]
        public new List<UIElement> ListUIElementPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionWebList  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "webListPackage")]
        public new List<IBaseUIElement> WebListPackage { get; set; }

        [FindBy(Id = "webListPublic")]
        public new List<IBaseUIElement> WebListPublic { get; set; }

        [FindBy(Id = "webListPrivate")]
        public new List<IBaseUIElement> WebListPrivate { get; set; }

        [FindBy(Css = ".webListPackageUI")]
        public new List<IBaseUIElement> WebListPackageUI { get; set; }

        [FindBy(Css = ".webListPublicUI")]
        public new List<IBaseUIElement> WebListPublicUI { get; set; }

        [FindBy(Css = ".webListPackageCss")]
        public new List<IBaseUIElement> WebListPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='webListPublicXPath']")]
        public new List<IBaseUIElement> WebListPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionListButtonPublic  TODO:// Add initialization of List<IWebElement>

        [FindBy(Id = "listButtonPackage")]
        public new List<Button> ListButtonPackage { get; set; }

        [FindBy(Id = "listButtonPublic")]
        public new List<Button> ListButtonPublic { get; set; }

        [FindBy(Id = "listButtonPrivate")]
        public new List<Button> ListButtonPrivate { get; set; }

        [FindBy(Css = ".listButtonPackageUI")]
        public new List<Button> ListButtonPackageUI { get; set; }

        [FindBy(Css = ".listButtonPublicUI")]
        public new List<Button> ListButtonPublicUI { get; set; }

        [FindBy(Css = ".listButtonPackageCss")]
        public new List<Button> ListButtonPackageCss { get; set; }

        [FindBy(XPath = "//*[@class='listButtonPublicXPath']")]
        public new List<Button> ListButtonPublicXPath { get; set; }

        #endregion

        #region ExtendedSectionUIListSomeData

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

        #region ExtendedSectionUIListQuestion

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

        #region ExtendedSectionDropDown

        [JDropDown("div[ui=droplistPackage]", "input", "li", ".expand")]
        public new DropList DropListPackage { get; set; }

        [JDropDown("div[ui=droplistPublic]", "input", "li", ".expand")]
        public new DropList DroplistPublic { get; set; }

        [JDropDown("div[ui=droplistPrivate]", "input", "li", ".expand")]
        public new DropList DroplistPrivate { get; set; }

        [FindBy(Css = ".droplistPackageUI")]
        public new DropList DroplistPackageUI { get; set; }

        [FindBy(Css = ".droplistPublicUI")]
        public new DropList DroplistPublicUI { get; set; }

        #endregion
    }
}
