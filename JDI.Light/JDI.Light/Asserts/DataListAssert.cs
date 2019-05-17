using JDI.Light.Asserts.Generic;
using JDI.Light.Elements.Common;

namespace JDI.Light.Asserts
{
    public class DataListAssert : IsAssert<DataListAssert>
    {
        protected DataList DataList { get; }

        public DataListAssert(DataList dataList) : base(dataList)
        {
            DataList = dataList;
        }
    }
}
