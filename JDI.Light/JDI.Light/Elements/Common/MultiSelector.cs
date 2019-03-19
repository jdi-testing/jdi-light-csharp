using System;
using System.Collections.Generic;
using System.Linq;
using OpenQA.Selenium;
using static JDI.Light.Extensions.ArrayExtensions;

namespace JDI.Light.Elements.Common
{
    public class MultiSelector : Selector
    {
        public By MultiItemLocator { get; set; }
        public List<By> MultiItemLocators { get; set; } = new List<By>();

        private readonly Action<MultiSelector, string> _unselectAll = (multiSelector, item) =>
        {
            var els = multiSelector.WebElement.FindElements(multiSelector.MultiItemLocator);
            var itemsList = els.FirstOrDefault(e => e.Text.Equals(item));
            if (itemsList?.GetAttribute("selected") != null)
            {
                itemsList.Click();
            }
        };

        public MultiSelector(By byLocator) : base(byLocator)
        {
        }

        public void Select(string[] values)
        {
            ItemLocator = MultiItemLocator;
            Select(values, this);
        }

        public void Select(int[] indexes)
        {
            for (var i = 0; i < indexes.Length; i++)
            {
                ItemLocator = MultiItemLocators[i];
                Select(indexes[i], this);
            }
        }

        public string[] GetSelected(Array values)
        {
            return _getAllSelected(values);
        }

        public void UnselectAll(Array allValues)
        {
            foreach (var value in ToStringExtension(allValues))
            {
                Invoker.DoAction($"Unselect item '{string.Join(" -> ", value)}'",
                    () => _unselectAll.Invoke(this, value));
            }
        }
        
        private string[] _getAllSelected(Array values)
        {
            var selectedItems = new List<string>();
            foreach (var value in ToStringExtension(values))
            {
                var els = WebElement.FindElements(MultiItemLocator);
                var itemsList = els.FirstOrDefault(e => e.Text.Equals(value));
                if (itemsList?.GetAttribute("selected") != null)
                {
                    selectedItems.Add(value);
                }
            }

            selectedItems.Reverse();
            return selectedItems.ToArray();
        }
    }
}