using System;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Common
{
    public interface IDateTimeSelector : IBaseUIElement
    {
        string Format { get; set; }
        string Value();
        string Min();
        string Max();
        void SetDateTime(string dateTime);

        void SetDateTime(DateTime dateTime);
        DateTime GetDateTime();
    }
}