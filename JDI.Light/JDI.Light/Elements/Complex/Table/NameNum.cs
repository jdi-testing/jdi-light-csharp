using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;

namespace JDI.Light.Elements.Complex.Table
{
    public class NameNum : DataClass<NameNum>
    {
        public int Num { get; set; } = 0;

        public string Name { get; set; } = "";

        public int GetIndex(List<UIElement> headers)
        {
            try
            {
                return Name.Equals("") ? Num : headers.IndexOf(headers.First(h => h.Text.Equals(Name))) + 1;
            }
            catch (Exception e)
            {
                throw new InvalidOperationException("There is no column in a table with name " + Name + ". Exception: " + e.Message);
            }
        }
    }
}
