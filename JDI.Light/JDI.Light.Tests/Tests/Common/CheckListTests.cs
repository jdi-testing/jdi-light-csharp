using NUnit.Framework;
using OpenQA.Selenium;

namespace JDI.Light.Tests.Tests.Common
{
    [TestFixture]
    public class CheckListTests : TestBase
    {
        [SetUp]
        public void SetUp()
        {
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckOpened();
            TestSite.MetalsColorsPage.ElementsCheckList.ItemLocator = By.CssSelector(".checkbox > label");
        }

        [Test]
        public void CheckCheckList()
        {
           var toCheck = new[] {"Water", "Wind"};
           TestSite.MetalsColorsPage.ElementsCheckList.Check(toCheck);
            

           Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Wind: condition changed to true");
           Jdi.Assert.Contains(TestSite.ActionsLog.Texts[1], "Water: condition changed to true");
        }

        [Test]
        public void CheckByIndexes()
        {
            var toCheck = new[] { 1, 3};
            foreach (var index in toCheck)
            {
                TestSite.MetalsColorsPage.ElementsCheckList.CheckListLocators.Add(
                    By.CssSelector($".checkbox:nth-child({index.ToString()})"));
            }
            TestSite.MetalsColorsPage.ElementsCheckList.Check(toCheck);

            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[0], "Wind: condition changed to true");
            Jdi.Assert.Contains(TestSite.ActionsLog.Texts[1], "Water: condition changed to true");
        }

        [Test]
        public void CheckUncheckTest()
        {
            var toCheck = new[] { "Water", "Wind" };
            TestSite.MetalsColorsPage.ElementsCheckList.Check(toCheck);

            var toUncheck = new[] { "Water" };
            TestSite.MetalsColorsPage.ElementsCheckList.Uncheck(toUncheck);
        }
    }
}