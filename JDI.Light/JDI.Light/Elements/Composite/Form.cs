using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Composite
{
    public class Form<T> : CompositeUIElement, IForm<T>
    {
        public By LocatorTemplate;

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
                return (T) this.GetFields(typeof(IGetValue<>))
                    .Select(field => ((IGetValue<T>) field.GetValue(this)).Value);
            }
            set => throw new Exception("This is not supported at the moment");
        }

        public T GetValue()
        {
            return Value;
        }

        public void Fill(T entity)
        {
            throw new NotImplementedException();
        }

        public void Fill(Dictionary<string, string> map)
        {
            this.GetFields(typeof(ISetValue<T>)).ForEach(fieldInfo =>
            {
                var fieldValue = map.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(fieldInfo.GetElementName())).Value;
                if (fieldValue == null) return;
                var setValueElement = (ISetValue<string>) fieldInfo.GetValue(this);
                setValueElement.Value = fieldValue;
            });
        }

        public void Check(T entity)
        {
            throw new NotImplementedException();
        }

        public void Submit(T entity, Enum buttonName)
        {
            throw new NotImplementedException();
        }

        public void Submit(T entity)
        {
            Fill(entity.PropertiesToDictionary());
            GetButton("Submit").Click();
        }

        public void Submit(Dictionary<string, string> objStrings)
        {
            Fill(objStrings);
            GetButton("Submit").Click();
        }

        public IList<string> Verify(Dictionary<string, string> objStrings)
        {
            var compareFalse = new List<string>();
            this.GetFields(typeof(IGetValue<>)).ForEach(field =>
            {
                var fieldValue = objStrings.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(field.GetElementName())).Value;
                if (fieldValue == null) return;
                var valueField = (IGetValue<string>)field.GetValue(this);
                var actual = valueField.Value.Trim();
                if (!actual.Equals(fieldValue))
                    compareFalse.Add($"Field '{field.Name}' (Actual: '{actual}' <> Expected: '{fieldValue}')");
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
                throw JDI.Assert.Exception("Check form failed:" +
                                           result.FormattedJoin("".FromNewLine()).FromNewLine());
        }

        public void Submit(T entity, string buttonName)
        {
            throw new NotImplementedException();
        }
    }
}