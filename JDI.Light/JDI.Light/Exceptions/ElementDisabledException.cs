using System;

namespace JDI.Light.Exceptions
{
    public class ElementDisabledException : Exception
    {
        public ElementDisabledException(string message) : base(message)
        {
        }
    }
}