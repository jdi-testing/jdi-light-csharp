using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;

namespace JDI.Light.Elements.Complex.Table
{
    public class NameNum : DataClass<NameNum>
    {
        public int num = 0;
        public string name ="";

        public int GetIndex(List<UIElement> headers)
        {
            try
            {
                return name.Equals("") ? num : headers.IndexOf(headers.First(h => h.Text.Equals(name))) + 1;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("There is no column in a table with name " + name + ". Exception: " + e.Message);
            }
        }
    }
}
