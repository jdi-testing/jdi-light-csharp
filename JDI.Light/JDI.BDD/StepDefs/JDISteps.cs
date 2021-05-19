using JDI.Light.Elements.Base;
using JDI.Light.Elements.Init;
using TechTalk.SpecFlow;
using static JDI.Light.Jdi;
using static JDI.Light.Matchers.StringMatchers.EqualToMatcher;
using static JDI.Light.Matchers.StringMatchers.ContainsStringMatcher;

namespace JDI.BDD.StepDefs
{
    [Binding]
    public class JDISteps
    {

    #region WHEN
        [When(@"I send keys (.*) to (.*)")]
        public void SendKeys(string value, string name)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).SendKeys(value);
        }

        [When(@"I click on (.*)")]
        public void Click(string name)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Click();
        }

        [When(@"I highlight (.*)")]
        public void Highlight(string name)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Highlight();
        }

        [When(@"I set (.*) attribute (.*) with value (.*) element")]
        public void SetAttribute(string name, string attrName, string attrValue)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).SetAttribute(attrName, attrValue);
        }

        [When(@"I clear (.*)")]
        public void Clear(string name)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Clear();
        }

        [When(@"I submit (.*)")]
        public void Submit(string name)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Submit();
        }

        [When(@"I show (.*)")]
        public void Show(string name)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Show();
        }

    #endregion

        [Then(@"the (.*) is disabled")]
        public void IsDisabled(string name)
        {
            Assert.IsTrue(EntitiesCollection.GetWebElement<UIElement>(name).Disabled);
        }

        [Then(@"the (.*) is enabled")]
        public void IsEnabled(string name)
        {
            Assert.IsTrue(EntitiesCollection.GetWebElement<UIElement>(name).Enabled);
        }

        [Then(@"the (.*) is enabled")]
        public void IsDisplayed(string name)
        {
            Assert.IsTrue(EntitiesCollection.GetWebElement<UIElement>(name).Displayed);
        }

        [Then(@"the (.*) is hidden")]
        public void IsHidden(string name)
        {
            Assert.IsTrue(EntitiesCollection.GetWebElement<UIElement>(name).Hidden);
        }

        [Then(@"the (.*) is selected")]
        public void IsSelected(string name)
        {
            Assert.IsTrue(EntitiesCollection.GetWebElement<UIElement>(name).Selected);
        }

        [Then(@"the (.*) text equals to (.*)")]
        public void TextEquals(string name, string value)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Is.Text(EqualTo(value));
        }

        [Then(@"the (.*) text contains (.*)")]
        public void TextContains(string name, string value)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Is.Text(ContainsString(value));
        }

        [Then(@"the (.*) attribute (.*) equals to (.*)")]
        public void AttributeEquals(string name, string attrName, string attrValue)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Is.Attr(attrName, EqualTo(attrValue));
        }

        [Then(@"the (.*) attribute (.*) contains (.*)")]
        public void AttributeContains(string name, string attrName, string attrValue)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Is.Attr(attrName, ContainsString(attrValue));
        }

        [Then(@"the (.*) css (.*) equals to (.*)")]
        public void CssEquals(string name, string css, string cssValue)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Is.Css(css, EqualTo(cssValue));
        }

        [Then(@"the (.*) css (.*) contains (.*)")]
        public void CssContains(string name, string css, string cssValue)
        {
            EntitiesCollection.GetWebElement<UIElement>(name).Is.Css(css, ContainsString(cssValue));
        }
    }
}
