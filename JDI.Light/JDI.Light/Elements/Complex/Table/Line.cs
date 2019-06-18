using System;
using System.Collections.Generic;
using System.Linq;
using JDI.Light.Elements.Base;
using OpenQA.Selenium;

namespace JDI.Light.Elements.Complex.Table
{
    public class Line
    {
        private Dictionary<string, string> _data;
        private static IEnumerable<IWebElement> _elements;
        private static List<string> _headers;
        private readonly List<string> _list;

        public Line(List<string> list, List<string> headers)
        {
            _list = list;
            _headers = headers;
        }

        public Line(IEnumerable<IWebElement> elements, List<string> headers)
        {
            _elements = elements;
            _headers = headers;
            _data = ZipTwoLists(_headers, _elements.Select(el => el.Text));
        }

        private static Dictionary<string, string> ZipTwoLists(IEnumerable<string> keys, IEnumerable<string> values)
        {
            var enumerable = keys as IList<string> ?? keys.ToList();
            var second = values as IList<string> ?? values.ToList();
            if (keys == null || values == null || enumerable.Count != second.Count)
            {
                throw new ArgumentException("Keys or values are null or has not equal count.");
            }
            return
                enumerable.Distinct().Count() < enumerable.Count
                ? throw new ArgumentException($"{keys} collection contains some duplicate elements")
                : enumerable.Zip(second, (k, v) => new { k, v }).ToDictionary(x => x.k, x => x.v);
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
