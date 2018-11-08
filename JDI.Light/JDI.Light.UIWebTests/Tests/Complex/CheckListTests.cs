using System.Collections.Generic;
using System.Linq;
using JDI.Core.Interfaces.Complex;
using JDI.Core.Settings;
using JDI.Matchers.NUnit;
using JDI.UIWebTests.Enums;
using JDI.UIWebTests.UIObjects;
using NUnit.Framework;
using OpenQA.Selenium;
using Assert = JDI.Matchers.NUnit.Assert;

namespace JDI.UIWebTests.Tests.Complex
{
    public class CheckListTests
    {
        private static readonly IList<string> NATURE_OPTIONS = new List<string>{"Water", "Earth", "Wind", "Fire" };           
        private static readonly string ALL_VALUES = "Water, Earth, Wind, Fire";

        private ICheckList<Elements> _nature()
        {
            return TestSite.MetalsColorsPage.Elements;
        }

        private void _checkAllIsChecked(bool isChecked)
        {
            IList<bool> checkedElems = TestSite.HomePage.WebDriver.
                FindElements(By.CssSelector("#elements-checklist input")).
                Select(e => e.GetAttribute("checked") != null).ToList();
            new Check("Check that all checkbox elements checked = " + isChecked).
                IsTrue(checkedElems.All(e => e == isChecked));            
        }

        private void _clearCheckBoxBlock() {
            IList<IWebElement> inputElems = TestSite.HomePage.WebDriver.FindElements(By.CssSelector("#elements-checklist input"));
            IList<IWebElement> labelsElems = TestSite.HomePage.WebDriver.FindElements(By.CssSelector(".checkbox>label"));

            for (int i = 0; i < inputElems.Count; i++)
            {
                if ((inputElems[i].GetAttribute("checked") != null) && (inputElems[i].GetAttribute("checked") == "true"))
                {
                    labelsElems[i].Click();
                }
            }
        }

        private void _cheсkAllLogMessages(IList<string> logLines) {
            //TO_DO: replace with TextList.Texts when will be fixed
            //var texts = ActionsLog.Texts; 
            IList<string> log = TestSite.HomePage.WebDriver.FindElements(By.CssSelector(".logs li")).Select(e => e.Text).ToList();            
            for (int i = 0; i < log.Count; i++)
            {
                Assert.Contains(log[i], logLines[i] + ": condition changed to true");
            }            
        }

        [SetUp]
        public void SetUp()
        {
            JDISettings.Logger.Info("Navigating to Metals and Colors page.");
            TestSite.MetalsColorsPage.Open();
            TestSite.MetalsColorsPage.CheckTitle();
            TestSite.MetalsColorsPage.IsOpened();
            JDISettings.Logger.Info("Setup method finished");
            JDISettings.Logger.Info("Start test: " + TestContext.CurrentContext.Test.Name);
        }

        [Test]
        public void SelectStringTest()
        {
            _nature().Select("Fire");
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void SelectIndexTest()
        {
            _nature().Select(4);
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void SelectEnumTest()
        {
            _nature().Select(Elements.Fire);
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void Select2StringTest()
        {
            _nature().Select("Water", "Fire");
            CommonActionsData.CheckAction("Fire: condition changed to true");            
            string asd = TestSite.ActionsLog.TextElements.First().Value;
            _cheсkAllLogMessages(new List<string>() { "Fire", "Water" });
        }

        [Test]
        public void Select2IndexTest()
        {
            _nature().Select(1, 4);
            CommonActionsData.CheckAction("Fire: condition changed to true");
            _cheсkAllLogMessages(new List<string>() { "Fire", "Water" });
        }

        [Test]
        public void Select2EnumTest()
        {
            _nature().Select(Elements.Water, Elements.Fire);
            CommonActionsData.CheckAction("Fire: condition changed to true");
            _cheсkAllLogMessages(new List<string>() { "Fire", "Water" });
        }

        [Test]
        public void CheckStringTest()
        {
            _nature().Check("Fire");
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void CheckIndexTest()
        {
            _nature().Check(4);
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void CheckEnumTest()
        {
            _nature().Check(Elements.Fire);
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void Check2StringTest()
        {
            _nature().Check("Water", "Fire");
            CommonActionsData.CheckAction("Fire: condition changed to true");
            _cheсkAllLogMessages(new List<string>() { "Fire", "Water" });
        }

        [Test]
        public void Check2IndexTest()
        {
            _nature().Check(1, 4);
            CommonActionsData.CheckAction("Fire: condition changed to true");
            _cheсkAllLogMessages(new List<string>() { "Fire", "Water" });
        }

        [Test]
        public void Check2EnumTest()
        {
            _nature().Check(Elements.Water, Elements.Fire);
            CommonActionsData.CheckAction("Fire: condition changed to true");
            _cheсkAllLogMessages(new List<string>() { "Fire", "Water" });
        }

        [Test]
        public void SelectAllTest()
        {
            _nature().SelectAll();            
            _cheсkAllLogMessages(new List<string>() { "Fire", "Wind", "Earth", "Water" });
            _checkAllIsChecked(true);         

        }

        [Test]
        public void CheckAllTest()
        {
            _nature().CheckAll();
            _cheсkAllLogMessages(new List<string>() { "Fire", "Wind", "Earth", "Water" });
            _checkAllIsChecked(true);
        }

        [Test]
        public void ClearAllTest()
        {
            _nature().CheckAll();
            _checkAllIsChecked(true);            
            _clearCheckBoxBlock();            
            _checkAllIsChecked(false);  
        }

        [Test]
        public void GetOptionsTest()
        {
            new Check().CollectionEquals(_nature().Options, NATURE_OPTIONS);            
        }

        [Test]
        public void GetNamesTest()
        {
            new Check().CollectionEquals(_nature().Names, NATURE_OPTIONS);
        }

        [Test]
        public void GetValuesTest()
        {
            new Check().CollectionEquals(_nature().Values, NATURE_OPTIONS);
        }

        [Test]
        public void GetOptionsAsTextTest()
        {
            new Check().AreEquals(_nature().OptionsAsText.ToString(), ALL_VALUES);            
        }

        [Test]
        public void SetValueTest()
        {
            _nature().Value = "Fire";                
            CommonActionsData.CheckAction("Fire: condition changed to true");
        }

        [Test]
        public void GetNameTest()
        {            
            new Check().AreEquals(_nature().Name, "Elements");            
        }

        // TODO: fix incorrect work of AreSelected method. It is always return empty collection
        /*
        [Test]
        public void AreSelectedTest()
        {
            new Check().CollectionEquals(_nature().AreSelected(), new List<string>() );
        }


        //TO_DO fix incorrect work of AreSelected method.It is always return empty collection
        [Test]
        public void AreDeselectedTest()
        {
            new Check().CollectionEquals(_nature().AreDeselected(), NATURE_OPTIONS);
        }
        */

        [Test]
        public void GetValueTest()
        {
            new Check().AreEquals(_nature().Value, "");                  
        }
    }
}
