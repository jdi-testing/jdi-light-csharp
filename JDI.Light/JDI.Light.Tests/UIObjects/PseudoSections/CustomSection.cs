using System.Collections.Generic;
using JDI.Light.Attributes;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Composite;
using OpenQA.Selenium;

namespace JDI.Light.Tests.UIObjects.PseudoSections
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

        #region CustomSectionListWebElement

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
    }
}
