using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Form<T> : UIElement, IForm<T>
    {
        public Form() : base(null)
        {
        }

        public Form(By locator) : base(locator)
        {
        }

        public T Value
        {
            get
            {
                return (T) this.GetMembers(typeof(IGetValue<>))
                    .Select(field => ((IGetValue<T>) field.GetMemberValue(this)).Value);
            }
            set => Fill(value);
        }

        public T GetValue()
        {
            return Value;
        }

        public void Fill(T entity)
        {
            Fill(entity.PropertiesToDictionary());
        }

        public void Fill(Dictionary<string, string> map)
        {
            var fieldsToSet = this.GetMembers(typeof(ISetValue<string>));
            foreach (var fieldInfo in fieldsToSet)
            {
                var fieldValue = map.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(fieldInfo.GetElementName())).Value;
                if (fieldValue == null) return;
                var setValueElement = (ISetValue<string>)fieldInfo.GetMemberValue(this);
                setValueElement.Value = fieldValue;
            }
        }

        public void Check(T entity)
        {
            Check(entity.PropertiesToDictionary());
        }

        public void Submit(T entity)
        {
            Fill(entity.PropertiesToDictionary());
            Get<IButton>(By.XPath("//button[@type='submit']")).Click();
        }

        public void Submit(Dictionary<string, string> objStrings)
        {
            Fill(objStrings);
            Get<IButton>(By.XPath("//button[@type='submit']")).Click();
        }

        public void Submit(T entity, string buttonText)
        {
            Fill(entity.PropertiesToDictionary());
            Get<IButton>(By.XPath($"//button[text()='{buttonText}']")).Click();
        }

        public void Submit(T entity, By locator)
        {
            Fill(entity.PropertiesToDictionary());
            Get<IButton>(locator).Click();
        }

        public IList<string> Verify(Dictionary<string, string> objStrings)
        {
            var compareFalse = new List<string>();
            this.GetMembers(typeof(IGetValue<>)).ToList().ForEach(member =>
            {
                var fieldValue = objStrings.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(member.GetElementName())).Value;
                if (fieldValue == null) return;
                var valueMember = (IGetValue<string>)member.GetMemberValue(this);
                var actual = valueMember.Value.Trim();
                if (!actual.Equals(fieldValue))
                    compareFalse.Add($"Field '{member.Name}' (Actual: '{actual}' <> Expected: '{fieldValue}')");
            });
            return compareFalse;
        }

        public IList<string> Verify(T entity)
        {
            return Verify(entity.PropertiesToDictionary());
        }

        public void Check(Dictionary<string, string> objStrings)
        {
            var result = Verify(objStrings);
            if (result.Count > 0)
                throw Jdi.Assert.Exception($"Check form failed: {string.Join(Environment.NewLine, result)}");
        }
    }
}