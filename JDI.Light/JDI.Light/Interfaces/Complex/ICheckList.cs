using System.Collections.Generic;
using JDI.Light.Interfaces.Base;

namespace JDI.Light.Interfaces.Complex
{
    public interface ICheckList : IBaseUIElement, IGetValue<List<string>>
    {
        void Check(params string[] values);
        void Check(params int[] indexes);
        void Uncheck(params string[] values);
        void Uncheck(params int[] indexes);
        void Select(params string[] values);
        void Select(params int[] indexes);
        void UncheckAll();
        string[] Checked();
    }
}