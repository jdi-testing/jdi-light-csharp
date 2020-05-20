using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;
using static JDI.Light.Extensions.ObjectExtensions;

namespace JDI.Light.Elements.Composite
{
    public class Form<T> : UIElement, IForm<T>
    {
        public Form() : base(null)
        {
            pageObject = this;
        }

        public Form(By locator) : base(locator)
        {
            pageObject = this;
        }

        private object pageObject;
        public Form<T> SetPageObject(object obj)
        {
            pageObject = obj;
            return this;
        }
        public T Value
        {
            get => (T) pageObject.GetMembers(typeof(IGetValue<>))
                .Select(field => ((IGetValue<T>) field.GetMemberValue(pageObject)).Value);
            set => Fill(value);
        }       

        public void Fill(T entity)
        {
            Fill(FieldsAsDictionary(entity));
        }

        public void Fill(Dictionary<string, string> dataMap)
        {
            var fieldsToSet = pageObject.GetMembers(typeof(ISetValue<string>));
            foreach (var data in dataMap)
            {
                if (data.Value.Equals("null"))
                {
                    continue;
                }
                var fieldValue = fieldsToSet.FirstOrDefault(f =>
                    data.Key.SimplifiedEqual(f.GetElementName()));
                if (fieldValue == null)
                {
                    continue;
                }
                var setValueElement = (ISetValue<string>) fieldValue.GetMemberValue(pageObject);
                setValueElement.Value = data.Value;
            }
        }

        public void Check(T entity)
        {
            Check(FieldsAsDictionary(entity));
        }

        public void Submit(T entity)
        {
            Fill(FieldsAsDictionary(entity));
            GetButton.Invoke(pageObject, "Submit").Click();
        }

        public void Submit(Dictionary<string, string> objStrings)
        {
            Fill(objStrings);
            GetButton.Invoke(pageObject, "Submit").Click();
        }

        public void Submit(T entity, string buttonText)
        {
            Fill(FieldsAsDictionary(entity));
            GetButton.Invoke(pageObject, buttonText).Click();
        }

        public void Submit(T entity, By locator)
        {
            Fill(FieldsAsDictionary(entity));
            GetChild<IButton>(locator).Click();
        }

        public IList<string> Verify(Dictionary<string, string> objStrings)
        {
            var compareFalse = new List<string>();
            pageObject.GetMembers(typeof(IGetValue<>)).ToList().ForEach(member =>
            {
                var fieldValue = objStrings.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(member.GetElementName())).Value;
                if (fieldValue == null) return;
                var valueMember = (IGetValue<string>) member.GetMemberValue(pageObject);
                var actual = valueMember.Value.Trim();
                if (!actual.Equals(fieldValue))
                    compareFalse.Add($"Field '{member.Name}' (Actual: '{actual}' <> Expected: '{fieldValue}')");
            });
            return compareFalse;
        }

        public IList<string> Verify(T entity)
        {
            return Verify(FieldsAsDictionary(entity));
        }

        public void Check(Dictionary<string, string> objStrings)
        {
            var result = Verify(objStrings);
            if (result.Count > 0)
            {
                throw Jdi.Assert.Exception($"Check form failed: {string.Join(Environment.NewLine, result)}");
            }
        }

        public void Login(T entity)
        {
            Submit(entity, "Login");
        }

        public static Func<object, string, IButton> GetButton = (obj, buttonName) =>
        {
            var fields = GetFieldsOfType(obj, typeof(IButton)).ToList();
            if (!fields.Any())
            {
                fields = GetFieldsOfType(obj, typeof(IWebElement)).ToList();
            }
            switch (fields.Count)
            {
                case 0:
                    if (obj.GetType().Name.Equals("Form"))
                    {
                        return new Button(By.CssSelector("[type=submit]"));
                    }
                    throw Jdi.Assert.Exception($"Can't find any buttons on form '{obj}.");
                case 1:
                    return (IButton) fields.First().GetMemberValue(obj);
                default:
                    return GetButtonByName(fields, obj, buttonName);
            }
        };
        public static IButton GetButtonByName(IEnumerable<MemberInfo> fields, object obj, string buttonName)
        {using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;
using static JDI.Light.Extensions.ObjectExtensions;

namespace JDI.Light.Elements.Composite
{
    public class Form<T> : UIElement, IForm<T>
    {
        public Form() : base(null)
        {
            pageObject = this;
        }

        public Form(By locator) : base(locator)
        {
            pageObject = this;
        }

        private object pageObject;
        public Form<T> SetPageObject(object obj)
        {
            pageObject = obj;
            return this;
        }
        public T Value
        {
            get => (T) pageObject.GetMembers(typeof(IGetValue<>))
                .Select(field => ((IGetValue<T>) field.GetMemberValue(pageObject)).Value);
            set => Fill(value);
        }       

        public void Fill(T entity)
        {
            Fill(FieldsAsDictionary(entity));
        }

        public void Fill(Dictionary<string, string> dataMap)
        {
            var fieldsToSet = pageObject.GetMembers(typeof(ISetValue<string>));
            foreach (var data in dataMap)
            {
                if (data.Value.Equals("null"))
                {
                    continue;
                }
                var fieldValue = fieldsToSet.FirstOrDefault(f =>
                    data.Key.SimplifiedEqual(f.GetElementName()));
                if (fieldValue == null)
                {
                    continue;
                }
                var setValueElement = (ISetValue<string>) fieldValue.GetMemberValue(pageObject);
                setValueElement.Value = data.Value;
            }
        }

        public void Check(T entity)
        {
            Check(FieldsAsDictionary(entity));
        }

        public void Submit(T entity)
        {
            Fill(FieldsAsDictionary(entity));
            GetButton.Invoke(pageObject, "Submit").Click();
        }

        public void Submit(Dictionary<string, string> objStrings)
        {
            Fill(objStrings);
            GetButton.Invoke(pageObject, "Submit").Click();
        }

        public void Submit(T entity, string buttonText)
        {
            Fill(FieldsAsDictionary(entity));
            GetButton.Invoke(pageObject, buttonText).Click();
        }

        public void Submit(T entity, By locator)
        {
            Fill(FieldsAsDictionary(entity));
            GetChild<IButton>(locator).Click();
        }

        public IList<string> Verify(Dictionary<string, string> objStrings)
        {
            var compareFalse = new List<string>();
            pageObject.GetMembers(typeof(IGetValue<>)).ToList().ForEach(member =>
            {
                var fieldValue = objStrings.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(member.GetElementName())).Value;
                if (fieldValue == null)
                {
                    return;
                }
                var valueMember = (IGetValue<string>) member.GetMemberValue(pageObject);
                var actual = valueMember.Value.Trim();
                if (!actual.Equals(fieldValue))
                {
                    compareFalse.Add($"Field '{member.Name}' (Actual: '{actual}' <> Expected: '{fieldValue}')");
                }
            });
            return compareFalse;
        }

        public IList<string> Verify(T entity)
        {
            return Verify(FieldsAsDictionary(entity));
        }

        public void Check(Dictionary<string, string> objStrings)
        {
            var result = Verify(objStrings);
            if (result.Count > 0)
            {
                throw Jdi.Assert.Exception($"Check form failed: {string.Join(Environment.NewLine, result)}");
            }
        }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using JDI.Light.Elements.Base;
using JDI.Light.Elements.Common;
using JDI.Light.Extensions;
using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Common;
using JDI.Light.Interfaces.Composite;
using JDI.Light.Utils;
using OpenQA.Selenium;
using static JDI.Light.Extensions.ObjectExtensions;

namespace JDI.Light.Elements.Composite
{
    public class Form<T> : UIElement, IForm<T>
    {
        public Form() : base(null)
        {
            pageObject = this;
        }

        public Form(By locator) : base(locator)
        {
            pageObject = this;
        }

        private object pageObject;
        public Form<T> SetPageObject(object obj)
        {
            pageObject = obj;
            return this;
        }
        public T Value
        {
            get => (T) pageObject.GetMembers(typeof(IGetValue<>))
                .Select(field => ((IGetValue<T>) field.GetMemberValue(pageObject)).Value);
            set => Fill(value);
        }       

        public void Fill(T entity)
        {
            Fill(FieldsAsDictionary(entity));
        }

        public void Fill(Dictionary<string, string> dataMap)
        {
            var fieldsToSet = pageObject.GetMembers(typeof(ISetValue<string>));
            foreach (var data in dataMap)
            {
                if (data.Value.Equals("null"))
                {
                    continue;
                }
                var fieldValue = fieldsToSet.FirstOrDefault(f =>
                    data.Key.SimplifiedEqual(f.GetElementName()));
                if (fieldValue == null)
                {
                    continue;
                }
                var setValueElement = (ISetValue<string>) fieldValue.GetMemberValue(pageObject);
                setValueElement.Value = data.Value;
            }
        }

        public void Check(T entity)
        {
            Check(FieldsAsDictionary(entity));
        }

        public void Submit(T entity)
        {
            Fill(FieldsAsDictionary(entity));
            GetButton.Invoke(pageObject, "Submit").Click();
        }

        public void Submit(Dictionary<string, string> objStrings)
        {
            Fill(objStrings);
            GetButton.Invoke(pageObject, "Submit").Click();
        }

        public void Submit(T entity, string buttonText)
        {
            Fill(FieldsAsDictionary(entity));
            GetButton.Invoke(pageObject, buttonText).Click();
        }

        public void Submit(T entity, By locator)
        {
            Fill(FieldsAsDictionary(entity));
            GetChild<IButton>(locator).Click();
        }

        public IList<string> Verify(Dictionary<string, string> objStrings)
        {
            var compareFalse = new List<string>();
            pageObject.GetMembers(typeof(IGetValue<>)).ToList().ForEach(member =>
            {
                var fieldValue = objStrings.FirstOrDefault(pair =>
                    pair.Key.SimplifiedEqual(member.GetElementName())).Value;
                if (fieldValue == null)
                {
                    return;
                }
                var valueMember = (IGetValue<string>) member.GetMemberValue(pageObject);
                var actual = valueMember.Value.Trim();
                if (!actual.Equals(fieldValue))
                {
                    compareFalse.Add($"Field '{member.Name}' (Actual: '{actual}' <> Expected: '{fieldValue}')");
                }
            });
            return compareFalse;
        }

        public IList<string> Verify(T entity)
        {
            return Verify(FieldsAsDictionary(entity));
        }

        public void Check(Dictionary<string, string> objStrings)
        {
            var result = Verify(objStrings);
            if (result.Count > 0)
            {
                throw Jdi.Assert.Exception($"Check form failed: {string.Join(Environment.NewLine, result)}");
            }
        }

        public void Login(T entity)
        {
            Submit(entity, "Login");
        }

        public static Func<object, string, IButton> GetButton = (obj, buttonName) =>
        {
            var fields = GetFieldsOfType(obj, typeof(IButton)).ToList();
            if (!fields.Any())
            {
                fields = GetFieldsOfType(obj, typeof(IWebElement)).ToList();
            }
            switch (fields.Count)
            {
                case 0:
                    if (obj.GetType().Name.Equals("Form"))
                    {
                        return new Button(By.CssSelector("[type=submit]"));
                    }
                    throw Jdi.Assert.Exception($"Can't find any buttons on form '{obj}.");
                case 1:
                    return (IButton) fields.First().GetMemberValue(obj);
                default:
                    return GetButtonByName(fields, obj, buttonName);
            }
        };
        public static IButton GetButtonByName(IEnumerable<MemberInfo> fields, object obj, string buttonName)
        {
            var buttons = fields.Where(f => f.GetMemberValue(obj) is IButton).Select(f => (IButton)f);
            var button = buttons.First(b => b.Name.Replace("Button", "").SimplifiedEqual(buttonName));
            if (button == null)
            {
                throw Jdi.Assert.Exception($"Can't find button '{buttonName}' for Element '{obj}'");
            }

            return button;
        }
    }
}
