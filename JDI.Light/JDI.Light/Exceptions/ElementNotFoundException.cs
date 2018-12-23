using System;

namespace JDI.Light.Exceptions
{
    public class ElementNotFoundException : Exception
    {
        public ElementNotFoundException()
        {
        }

        public ElementNotFoundException(string message) : base(message)
        {
        }

        public ElementNotFoundException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}