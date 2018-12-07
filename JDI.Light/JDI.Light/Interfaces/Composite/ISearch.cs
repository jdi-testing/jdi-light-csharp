using System.Collections.Generic;
using JDI.Light.Interfaces.Common;

namespace JDI.Light.Interfaces.Composite
{
    public interface ISearch : ITextField
    {
        void ChooseSuggestion(string text, int selectIndex);
        void Find(string text);
        IList<string> GetSuggestions(string text);
    }
}