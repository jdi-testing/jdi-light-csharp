using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;

namespace JDI.Light.Elements.Complex.Table
{
    public class Line
    {
        private Dictionary<string, string> _data;
        private readonly List<UIElement> _elements;
        private readonly List<string> _headers;
        private readonly List<string> _list;

        public Line(List<string> list, List<string> headers)
        {
            _list = list;
            _headers = headers;
        }

        public Line(List<UIElement> elements, List<string> headers)
        {
            _elements = elements;
            _headers = headers;
            _data = ZipTwoLists(_headers, _elements.Select(el => el.Text).ToList());
        }

        private static Dictionary<string, string> ZipTwoLists(IReadOnlyCollection<string> keys, IReadOnlyCollection<string> values)
        {
            if (keys == null || values == null || keys.Count != values.Count)
                throw new ArgumentException("Keys or values are null or has not equal count.");
            return keys.GroupBy(x => x)
                       .Where(g => g.Count() > 1)
                       .Select(y => y.Key)
                       .ToList().Count > 0
                ? throw new ArgumentException($"{keys} collection contains some duplicate elements")
                : keys.Zip(values, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
        }

        public static Line InitLine(List<string> list, List<string> headers)
        {
            var line = new Line(list, headers);
            return line;
        }

        private Dictionary<string, string> GetData(int minAmount)
        {
            if (_data == null || _data.Count < minAmount)
            {
                _data = ZipTwoLists(_headers, _elements.Select(el => el.Text).ToList());
            }
            return _data;
        }

        private IEnumerable<string> GetList(int minAmount)
        {
            return _list != null && _list.Count >= minAmount ? _list
                : GetData(minAmount).Select(pair => pair.Value).ToList();
        }

        public string GetValue()
        {
            return string.Join(";", GetList(0));
        }
    }
}
