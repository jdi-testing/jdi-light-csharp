using System;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDateTimeSelector : IBaseUIElement
    {
        string Format { get; set; }
        void SetDateTime(string value);
        void SetDateTime(DateTime dateTime);
        string GetValue();
        DateTime GetDateTime();
    }
}