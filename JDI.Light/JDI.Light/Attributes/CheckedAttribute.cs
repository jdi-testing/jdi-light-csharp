using System;
using JDI.Light.Elements.Base;

namespace JDI.Light.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Field)]
    public class CheckedAttribute : Attribute
    {
        public Func<UIElement, bool> CheckedDelegate { get; set; }

        public CheckedAttribute(Type delegateType, string delegateName)
        {
            CheckedDelegate = (Func<UIElement, bool>)Delegate.CreateDelegate(typeof(Func<UIElement, bool>), delegateType, delegateName, true, false);
        }
    }
}