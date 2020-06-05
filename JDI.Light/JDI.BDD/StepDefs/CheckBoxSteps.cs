using JDI.Light.Elements.Init;
using TechTalk.SpecFlow;
using JDI.Light.Elements.Common;

namespace JDI.BDD.StepDefs
{
    [Binding]
    public class CheckBoxSteps
    {
        [When(@"I check (.*)")]
        public void Check(string name)
        {
            EntitiesCollection.GetWebElement<CheckBox>(name).Check();
        }

        [When(@"I uncheck (.*)")]
        public void Uncheck(string name)
        {
            EntitiesCollection.GetWebElement<CheckBox>(name).Uncheck();
        }

        [Then(@"the (.*) is checked")]
        public void IsChecked(string name)
        {
            EntitiesCollection.GetWebElement<CheckBox>(name).Is.Selected();
        }

        [Then(@"the (.*) is not checked")]
        public void IsNotChecked(string name)
        {
            EntitiesCollection.GetWebElement<CheckBox>(name).Is.Deselected();
        }

        [Then(@"the (.*) is enabled")]
        public void IsEnabled(string name)
        {
            EntitiesCollection.GetWebElement<CheckBox>(name).Is.Enabled();
        }

        [Then(@"the (.*) is displayed")]
        public void IsDisplayed(string name)
        {
            EntitiesCollection.GetWebElement<CheckBox>(name).Is.Displayed();
        }
    }
}
