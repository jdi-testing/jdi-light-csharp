using System;
using JDI.Light.Elements.Base;

namespace JDI.Light.Asserts
{
    public class BaseAssert
    {
        public string Name { get; }
        public string FailElement { get; }
        public UIElement Element { get; }

        // todo: use fail element from JDIBase, when implemented
        public BaseAssert(UIElement element) : this(element.Name, element.Name)
        {
            Element = element;
        }

        public BaseAssert(string name) : this(name, name)
        {
        }

        public BaseAssert(string name, string failElement)
        {
            Name = name;
            FailElement = failElement;
        }

        //todo implemented as in java but found no usage of it
        public void Soft<T>(Func<T> isAssert) where T : BaseAssert
        {
            isAssert();
            Verify();
        }

        protected virtual void Verify()
        {
        }
    }
}