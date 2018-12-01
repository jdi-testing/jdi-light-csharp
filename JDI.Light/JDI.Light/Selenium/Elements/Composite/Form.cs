using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Complex;
using JDI.Light.Utils;
using OpenQA.Selenium;

namespace JDI.Light.Selenium.Elements.Composite
{
    public class Form : CompositeUIElement, IForm
    {
        public By LocatorTemplate;

        public string Value
        {
            get
            {
                return this.GetFields(typeof(IHasValue)).Select(field => ((IHasValue) field.GetValue(this)).Value)
                    .FormattedJoin();
            }
            set => Submit(value.ParseAsString());
        }

        public string GetValue()
        {
            return Value;
        }

        public void Fill(Dictionary<string, string> map)
        {
            this.GetFields(typeof(ISetValue)).ForEach(fieldInfo =>
            {
                var fieldValue = map.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(fieldInfo.GetElementName())).Value;
                if (fieldValue == null) return;
                var setValueElement = (ISetValue) fieldInfo.GetValue(this);
                setValueElement.Value = fieldValue;
            });
        }

        public void Submit(Dictionary<string, string> objStrings)
        {
            Fill(objStrings);
            GetButton("Submit").Click();
        }

        private void SetText(string text)
        {
            var field = this.GetFields(typeof(ISetValue))[0];
            var setValueElement = (ISetValue) field.GetValue(this);
            setValueElement.Value = text;
        }

        public void Submit(string text)
        {
            SetText(text);
            GetButton("Submit").Click();
        }

        public void Submit(string text, string buttonName)
        {
            SetText(text);
            GetButton(buttonName).Click();
        }

        public void Login(string text)
        {
            Submit(text, "Login");
        }

        public void Add(string text)
        {
            Submit(text, "Add");
        }

        public void Publish(string text)
        {
            Submit(text, "Publish");
        }

        public void Save(string text)
        {
            Submit(text, "Save");
        }

        public void Update(string text)
        {
            Submit(text, "Update");
        }

        public void Cancel(string text)
        {
            Submit(text, "Cancel");
        }

        public void Close(string text)
        {
            Submit(text, "Close");
        }

        public void Back(string text)
        {
            Submit(text, "Back");
        }

        public void Select(string text)
        {
            Submit(text, "Select");
        }

        public void Next(string text)
        {
            Submit(text, "Next");
        }

        public void Search(string text)
        {
            Submit(text, "Search");
        }

        public IList<string> Verify(Dictionary<string, string> objStrings)
        {
            var compareFalse = new List<string>();
            this.GetFields(typeof(IHasValue)).ForEach(field =>
            {
                var fieldValue = objStrings.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(field.GetElementName())).Value;
                if (fieldValue == null) return;
                var valueField = (IHasValue) field.GetValue(this);
                var actual = valueField.Value.Trim();
                if (!actual.Equals(fieldValue))
                {
                    compareFalse.Add($"Field '{field.Name}' (Actual: '{actual}' <> Expected: '{fieldValue}')");
                }
            });
            return compareFalse;
        }

        public void Check(Dictionary<string, string> objStrings)
        {
            var result = Verify(objStrings);
            if (result.Count > 0)
                throw JDI.Assert.Exception("Check form failed:" + result.FormattedJoin("".FromNewLine()).FromNewLine());
        }
    }

    public class Form<T> : Form, IForm<T>
    {
        public void Fill(T entity)
        {
            Fill(entity.ToSetValue());
        }

        public IList<string> Verify(T entity)
        {
            return Verify(entity.ToSetValue());
        }

        public void Search(T entity)
        {
            Submit(entity, "Search");
        }

        public void Submit(T entity, string buttonName)
        {
            Fill(entity.ToSetValue());
            GetButton(buttonName).Click();
        }

        public void Submit(T entity)
        {
            Submit(entity, "Submit");
        }

        public void Login(T entity)
        {
            Submit(entity, "Login");
        }

        public void Add(T entity)
        {
            Submit(entity, "Add");
        }

        public void Publish(T entity)
        {
            Submit(entity, "Publish");
        }

        public void Save(T entity)
        {
            Submit(entity, "Save");
        }

        public void Update(T entity)
        {
            Submit(entity, "Update");
        }

        public void Cancel(T entity)
        {
            Submit(entity, "Cancel");
        }

        public void Close(T entity)
        {
            Submit(entity, "Close");
        }

        public void Back(T entity)
        {
            Submit(entity, "Back");
        }

        public void Select(T entity)
        {
            Submit(entity, "Select");
        }

        public void Next(T entity)
        {
            Submit(entity, "Next");
        }

        public void Submit(T entity, Enum buttonName)
        {
            Fill(entity.ToSetValue());
            GetButton(buttonName.ToString().ToLower()).Click();
        }

        public void Check(T entity)
        {
            Check(entity.ToSetValue());
        }
    }
}