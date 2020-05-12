using JDI.Light.Interfaces.Base;
using JDI.Light.Interfaces.Composite;
using System.Collections.Generic;

namespace JDI.Light.Elements.Init
{
    public class EntitiesCollection
    {
        public static Dictionary<string, IPage> Pages = new Dictionary<string, IPage>();
        public static Dictionary<string, List<IBaseElement>> Elements = new Dictionary<string, List<IBaseElement>>();
    }
}
