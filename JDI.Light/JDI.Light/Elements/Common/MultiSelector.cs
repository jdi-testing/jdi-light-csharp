using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Exceptions;
using JDI.Light.Interfaces.Common;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;

namespace JDI.Light.Elements.Common
{
    public class MultiSelector : Selector, IMultiSelector
    {
        public SelectElement SelectElement => new SelectElement(this);

        public MultiSelector(By byLocator) : base(byLocator)
        {
        }

        public void Check(string value)
        {
            SelectElement.DeselectAll();

            CheckEnabled(true, SelectElement, value);
            SelectElement.SelectByText(value);
        }

        public void Check(int index)
        {
            SelectElement.DeselectAll();

            CheckEnabled(true, SelectElement, null, index);
            SelectElement.SelectByIndex(index);
        }

        public void Check(string[] values)
        {
            SelectElement.DeselectAll();
            foreach (var value in values)
            {
                CheckEnabled(true, SelectElement, value);
                SelectElement.SelectByText(value);
            }
        }

        public void Check(Enum[] values)
        {
            SelectElement.DeselectAll();
            foreach (var value in values)
            {
                CheckEnabled(true, SelectElement, value.ToString());
                SelectElement.SelectByText(value.ToString());
            }
        }

        public void Check(int[] indexes)
        {
            SelectElement.DeselectAll();
            foreach (var index in indexes)
            {
                CheckEnabled(true, SelectElement, null, index);
                SelectElement.SelectByIndex(index);
            }
        }

        public void Uncheck(string[] values)
        {
            foreach (var value in values)
            {
                CheckEnabled(true, SelectElement, value);
                SelectElement.DeselectByText(value);
            }
        }

        public void Uncheck(Enum[] values)
        {
            foreach (var value in values)
            {
                CheckEnabled(true, SelectElement, value.ToString());
                SelectElement.DeselectByText(value.ToString());
            }
        }

        public void Uncheck(int[] values)
        {
            foreach (var value in values)
            {
                CheckEnabled(true, SelectElement, null, value);
                SelectElement.DeselectByIndex(value);
            }
        }

        public string Selected()
        {
            return SelectElement.AllSelectedOptions.First().Text;
        }

        public List<string> Checked()
        {
            var toReturn = new List<string>();
            foreach (var iWebElement in SelectElement.AllSelectedOptions)
            {
                toReturn.Add(iWebElement.Text);
            }
            return toReturn;
        }
    }
}